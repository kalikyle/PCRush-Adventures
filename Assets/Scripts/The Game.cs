using Orders.Model;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TheGame : NetworkBehaviour
{

    public GameObject resultPanel;
    public TMP_Text resultText;
    public Image pcImage;
    public Camera MainCamera;
    public static TheGame instance;
    public Button RematchBTN;
    public Slider rematchTimerSlider;
    public GameObject image1;
    public GameObject image2;

    //public bool Easy = false;
    //public bool Normal = false;
    //public bool Hard = false;

    public NetworkVariable<bool> InGame = new NetworkVariable<bool>();

    public NetworkVariable<bool> IsHardMode = new NetworkVariable<bool>();
    public NetworkVariable<bool> IsNormalMode = new NetworkVariable<bool>();
    public NetworkVariable<bool> IsEasyMode = new NetworkVariable<bool>();

    public NetworkVariable<bool> gameEnded = new NetworkVariable<bool>(false);
    public NetworkVariable<ulong> winnerClientId = new NetworkVariable<ulong>(0);
    public NetworkVariable<bool> hostRematchRequested = new NetworkVariable<bool>(false);
    public NetworkVariable<bool> clientRematchRequested = new NetworkVariable<bool>(false);
    private Coroutine rematchCoroutine;


    public Missions currentMission;

    public void Awake()
    {
        //questEvents = new QuestEvent();
        if (instance == null)
        {

            instance = this;
            DontDestroyOnLoad(gameObject);

        }
        else
        {
            // If another instance already exists, destroy this one
            Destroy(gameObject);
        }
        //questEvents = new QuestEvent();
    }

    public void SetGameMode(bool easy, bool normal, bool hard)
    {
        //Easy = easy;
        //Normal = normal;
        //Hard = hard;
        IsHardMode.Value = hard;
        IsNormalMode.Value = normal;
        IsEasyMode.Value = easy;
        InGame.Value = true;// Sync the hard mode status
    }
    void Start()
    {
        //gameButton.onClick.AddListener(OnGameButtonClick);
        resultPanel.SetActive(false);

        // Subscribe to the NetworkVariable changed event
        gameEnded.OnValueChanged += OnGameEndedChanged;
        winnerClientId.OnValueChanged += OnWinnerClientIdChanged;


        hostRematchRequested.OnValueChanged += OnHostRematchRequestedChanged;
        clientRematchRequested.OnValueChanged += OnClientRematchRequestedChanged;

        RematchBTN.onClick.AddListener(OnRematchButtonClick);
    }

    private void OnGameEndedChanged(bool oldValue, bool newValue)
    {
        if (newValue)
        {
            if (NetworkManager.Singleton.LocalClientId == winnerClientId.Value)
            {
                ShowResult("You Win");
            }
            else
            {
                ShowResult("You Lose");
            }
        }
    }

    private void OnWinnerClientIdChanged(ulong oldValue, ulong newValue)
    {
        if (gameEnded.Value)
        {
            if (NetworkManager.Singleton.LocalClientId == newValue)
            {
                ShowResult("You Win");
            }
            else
            {
                ShowResult("You Lose");
            }
        }
    }
    private void OnHostRematchRequestedChanged(bool oldValue, bool newValue)
    {
        if (newValue)
        {
            image1.SetActive(true);
           
        }
        ShowRematchUIClientRpc();
        CheckRematchStatus();
    }

    private void OnClientRematchRequestedChanged(bool oldValue, bool newValue)
    {
        if (newValue)
        {
            image2.SetActive(true);
           
        }
        ShowRematchUIClientRpc();
        CheckRematchStatus();
    }

    [ClientRpc]
    private void ShowRematchUIClientRpc(ClientRpcParams clientRpcParams = default)
    {
        if (hostRematchRequested.Value)
        {
            image1.SetActive(true);
        }

        if (clientRematchRequested.Value)
        {
            image2.SetActive(true);
        }

        if (rematchCoroutine == null)
        {
            rematchCoroutine = StartCoroutine(RematchTimerCoroutine());
        }
    }

    public void OnFinishButtonClick()
    {
        if (gameEnded.Value)
        {
            Debug.LogError("chupapi");
            return;
            
        }


        // If client, request the server to end the game
        if (!IsHost)
        {
            RequestGameEndServerRpc(NetworkManager.Singleton.LocalClientId);
        }
        else
        {
            // If host, end the game directly
            EndGame(NetworkManager.Singleton.LocalClientId);
        }
    }

    public void OnSurrenderButtonClick()
    {
        if (gameEnded.Value)
        {
            Debug.LogError("Game has already ended.");
            return;
        }

        // Notify the server that this client is surrendering
        SurrenderServerRpc(NetworkManager.Singleton.LocalClientId);
    }

    [ServerRpc(RequireOwnership = false)]
    void SurrenderServerRpc(ulong surrenderingClientId)
    {
        // Call the method to declare the winner based on who surrendered
        DeclareWinner(surrenderingClientId);
    } 
 

    [ServerRpc(RequireOwnership = false)]
    void RequestGameEndServerRpc(ulong clientId, ServerRpcParams rpcParams = default) 
    {
        // Ensure only the first client to click wins
        if (!gameEnded.Value)
        {
            gameEnded.Value = true;
            winnerClientId.Value = clientId;
        }
    }

    void EndGame(ulong clientId)
    {
        gameEnded.Value = true;
        winnerClientId.Value = clientId;
        //IsHardMode.Value = false;
        //IsNormalMode.Value = false;
        //IsEasyMode.Value = false;
        //Easy = false;
        //Normal = false;
        //Hard = false;
    }

    void ShowResult(string result)
    {
        MainCamera.gameObject.SetActive(true);
        SceneManager.UnloadSceneAsync("PCRush");
        resultPanel.SetActive(true);
        resultText.text = result;
        
        
    }

    public void OnRematchButtonClick()
    {
        if (IsHost)
        {
            hostRematchRequested.Value = true;
        }
        else
        {
            RequestClientRematchServerRpc();
        }

        CheckRematchStatus();
        }

    
        private void CheckRematchStatus()
    {
        if (hostRematchRequested.Value && clientRematchRequested.Value)
        {
            StartRematchNowClientRpc();
        }
    }
    [ClientRpc]
    private void StartRematchNowClientRpc(ClientRpcParams clientRpcParams = default)
    {
        StartRematch();
    }

    [ServerRpc(RequireOwnership = false)]
    void RequestClientRematchServerRpc(ServerRpcParams rpcParams = default)
    {
        clientRematchRequested.Value = true;

        CheckRematchStatus();
    }

    IEnumerator RematchTimerCoroutine()
    {
        float timer = 10f;
        rematchTimerSlider.gameObject.SetActive(true);
        rematchTimerSlider.maxValue = timer;
        rematchTimerSlider.value = timer;

        while (timer > 0)
        {
            yield return new WaitForSeconds(1f);
            timer -= 1f;
            rematchTimerSlider.value = timer;
        }

        image1.SetActive(false);
        image2.SetActive(false);

        ResetGameServerRpc();
    }


    void StartRematch()
    {
        if (rematchCoroutine != null)
        {
            StopCoroutine(rematchCoroutine);
        }
        if (IsHardMode.Value == true)
        {
            TheGame.instance.currentMission = ClientController.instance.AddRandomMissionToGame();
        }

        rematchTimerSlider.maxValue = 10f;
        rematchTimerSlider.value = 10f;
        // Restart the game (reload the scene)
        resultPanel.SetActive(false);
        image1.SetActive(false);
        image2.SetActive(false);
        SceneManager.LoadScene("PCRush", LoadSceneMode.Additive);

       


        if (IsHost)
        {

            gameEnded.Value = false;
            winnerClientId.Value = 0;
            hostRematchRequested.Value = false;
            clientRematchRequested.Value = false;
            //IsHardMode.Value = false;
            //IsNormalMode.Value = false;
            //IsEasyMode.Value = false;
            //Easy = false;
            //Normal = false;
            //Hard = false;

        }



    }

    public void OnCloseButton()
    {
        if (IsClient)
        {
            NotifyClientDisconnectServerRpc(NetworkManager.Singleton.LocalClientId);
        }
        
        
        if (IsHost)
        {
            NotifyHostDisconnectServerRpc();
        }
    }
    private void OnApplicationQuit()
    {
        // Debug.LogError("quitssss");
        //
        try
        {

            if (InGame.Value == true)
            {
                OnSurrenderButtonClick();
            }
            else
            {
                OnCloseButton();
            }
        }
        catch { }


    }

    public void Update()
    {
        
    }

    [ServerRpc(RequireOwnership = false)]
    void NotifyClientDisconnectServerRpc(ulong clientId, ServerRpcParams rpcParams = default)
    {
        // Handle client disconnect: Close result panels and reset NetworkVariables
        if (clientId != NetworkManager.Singleton.LocalClientId)
        {
            // Notify all clients to close the result panel and reset
           
            ResetGameServerRpc();
        }
    }

    [ServerRpc(RequireOwnership = false)]
    void NotifyHostDisconnectServerRpc(ServerRpcParams rpcParams = default)
    {
        // Handle host disconnect: Close result panels and reset
        
        ResetGameServerRpc();
    }

    [ServerRpc(RequireOwnership = false)]
    void ResetGameServerRpc(ServerRpcParams rpcParams = default)
    {
        // Reset all NetworkVariables
       
        gameEnded.Value = false;
        winnerClientId.Value = 0;
        hostRematchRequested.Value = false;
        clientRematchRequested.Value = false;

        IsHardMode.Value = false;
        IsNormalMode.Value = false;
        IsEasyMode.Value = false;
        //Easy = false;
        //Normal = false;
        //Hard = false;
        InGame.Value = false;



       

        // Notify all clients to close result panels and reset game state
        CloseResultPanelsClientRpc();
    }

    [ClientRpc]
    void CloseResultPanelsClientRpc(ClientRpcParams rpcParams = default)
    {
        // Close the result panel and reset NetworkVariables on clients
        
        resultPanel.SetActive(false);
        image1.SetActive(false);
        image2.SetActive(false);

        gameEnded.Value = false;
        winnerClientId.Value = 0;
        hostRematchRequested.Value = false;
        clientRematchRequested.Value = false;

        IsHardMode.Value = false;
        IsNormalMode.Value = false;
        IsEasyMode.Value = false;
        //Easy = false;
        //Normal = false;
        //Hard = false;
        InGame.Value = false;

        NetworkManager.Singleton.Shutdown();
        
       
    }

    void DeclareWinner(ulong loserClientId)
    {
        // Logic to determine and declare the winner
        // For simplicity, assume the other client is the winner
        ulong winnerClientId = NetworkManager.Singleton.ConnectedClientsIds.FirstOrDefault(id => id != loserClientId);

        TheGame.instance.winnerClientId.Value = winnerClientId;

        // Optionally, notify players about the winner
        Debug.Log($"Player {winnerClientId} is the winner!");

        // End the game
        EndGame(winnerClientId);
    }

}
