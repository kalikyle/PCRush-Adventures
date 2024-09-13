using Orders.Model;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TheGame : NetworkBehaviour
{

    public GameObject resultPanel;
    public TMP_Text resultText;
    public TMP_Text resultFeedback;
    public Image pcImage;
    public Camera MainCamera;
    public static TheGame instance;
    public Button RematchBTN;
    public Slider rematchTimerSlider;
    public GameObject image1;
    public GameObject image2;

    public GameObject image1Ready;
    public GameObject image2Ready;

    public GameObject ReadyPanel;
    public GameObject ReadyUI;
    public GameObject CountdownUI;
    public GameObject ReadyBTN;
    public GameObject three;
    public GameObject two;
    public GameObject one;
    public GameObject Build;



    public TMP_Text timeResult;
    public float Time = 0f;

    //playerinfo in LAN
    public float BestTimeEasyMode;
    public float BestTimeNormalMode;
    public float BestTimeHardMode;
    public float WinsEasyMode;
    public float WinsNormalMode;
    public float WinsHardMode;

    public bool Surrendering = false;
    
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
    public NetworkVariable<bool> hostReadyRequested = new NetworkVariable<bool>(false);
    public NetworkVariable<bool> clientReadyRequested = new NetworkVariable<bool>(false);
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

        hostReadyRequested.OnValueChanged += OnHostReadyRequestedChanged;
        clientReadyRequested.OnValueChanged += OnClientReadyRequestedChanged;
        NetworkManager.Singleton.OnClientDisconnectCallback += OnPlayerDisconnecteds;
        

        RematchBTN.onClick.AddListener(OnRematchButtonClick);
    }

    private void OnGameEndedChanged(bool oldValue, bool newValue)
    {
        if (newValue)
        {
            if (NetworkManager.Singleton.LocalClientId == winnerClientId.Value)
            {
                ShowResult("You Win", "Your Opponent Just Surrendered");
            }
            else
            {
                ShowResult("You Lose", "Why?");
            }
        }
    }

    private void OnWinnerClientIdChanged(ulong oldValue, ulong newValue)
    {
        if (gameEnded.Value)
        {
            if (NetworkManager.Singleton.LocalClientId == newValue)
            {
                ShowResult("You Win", "Congratulations, You finish building the PC first!");
            }
            else
            {
                ShowResult("You Lose", "Your Opponent Just finished the PC first");
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


    private void OnHostReadyRequestedChanged(bool oldValue, bool newValue)
    {
        if (newValue)
        {
            image1Ready.SetActive(true);

        }
        ShowReadyUIClientRpc();
        CheckReadyStatus();
    }

    private void OnClientReadyRequestedChanged(bool oldValue, bool newValue)
    {
        if (newValue)
        {
            image2Ready.SetActive(true);

        }
        ShowReadyUIClientRpc();
        CheckReadyStatus();
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

    [ClientRpc]
    private void ShowReadyUIClientRpc(ClientRpcParams clientRpcParams = default)
    {
        if (hostRematchRequested.Value)
        {
            image1Ready.SetActive(true);
        }

        if (clientRematchRequested.Value)
        {
            image2Ready.SetActive(true);
        }
    }
    public override void OnDestroy()
    {
        if (NetworkManager.Singleton != null)
        {
            NetworkManager.Singleton.OnClientDisconnectCallback -= OnPlayerDisconnecteds;
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

    // private void OnPlayerDisconnecteds(ulong disconnectedClientId)
    //{
    //    //Debug.Log($"Player {disconnectedClientId} disconnected.");

    //    // Check if the game has already ended
    //    if (gameEnded.Value)
    //    {
    //        Debug.LogError("Game has already ended.");
    //        return;
    //    }
    //    if (NetworkManager.Singleton.IsServer && disconnectedClientId == NetworkManager.Singleton.LocalClientId)
    //    {
    //        //Debug.LogError("Host Disconnected");
    //        ShowResult("You Win", "Just Disconnected");
    //    }
    //    else
    //    {
    //        // Notify the server that this client is surrendering due to disconnection
    //        SurrenderServerRpc(disconnectedClientId);
    //        //Debug.LogError("Client Disconnected");
    //    }


    //}

    private void OnPlayerDisconnecteds(ulong disconnectedClientId)
    {
        // Check if the game has already ended
        if (gameEnded.Value)
        {
            Debug.LogError("Game has already ended.");
            return;
        }
        // Check if the disconnected client is the local client
        if (disconnectedClientId != NetworkManager.Singleton.LocalClientId)
        {
            
            ShowResult("You Win", "Opponent Disconnected");
            RematchBTN.gameObject.SetActive(false);
            Debug.LogError("Host Disconnecteds");
        }

        // Local player has disconnected
        //if (NetworkManager.Singleton.IsServer)
        //{
        //    // Host disconnected
        //    Debug.LogError("Client Disconnected");
        //    SurrenderServerRpc(disconnectedClientId);
        //}
        //else
        //{
        //    // Client disconnected
        //    Debug.Log("Client Disconnected");
        //}

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

    }
    public GameObject resText, resText2, ResPCImage, closebtn, rembtn, circling1, imagerem1, imagerem2; 
    void ShowResult(string result, string resultfeed)
    {
        
        MainCamera.gameObject.SetActive(true);
        SceneManager.UnloadSceneAsync("PCRush");
        resultPanel.SetActive(true);
        resultText.text = result;
        resultFeedback.text = resultfeed;
        image1.SetActive(false);
        image2.SetActive(false);


        int minutes = Mathf.FloorToInt(Time / 60f);
        int seconds = Mathf.FloorToInt(Time % 60);
        int milliseconds = Mathf.FloorToInt((Time * 1000f) % 1000);

        if (result == "You Win")
        {
            timeResult.text = $"You Finished in: {minutes:00}:{seconds:00}:{milliseconds:00}";
        }
        else
        {
            timeResult.text = $"Your Opponent Finished in:  {minutes:00}:{seconds:00}:{milliseconds:00}";
        }


        //leantween animate

        // Start rotating circling1 to the right
        LeanTween.rotateAround(circling1, Vector3.forward, -360f, 5f).setLoopClamp();

        // Scale resText to 1 (cubic ease)
        LeanTween.delayedCall(1f, () =>
        {
            LeanTween.scale(resText, new Vector2(3.6f, 3.6f), 2f).setEase(LeanTweenType.easeInOutCubic).setOnComplete(() =>
            {
                // Scale resText2 to 1 (cubic ease)
                LeanTween.scale(resText2, new Vector2(3.6f, 3.6f), 0.5f).setEase(LeanTweenType.easeInOutCubic).setOnComplete(() =>
                {
                    // Alpha the ResPCImage to 1
                    LeanTween.alpha(ResPCImage.GetComponent<RectTransform>(), 1f, 0.5f).setOnComplete(() =>
                    {
                        // Wait for 1 second
                        LeanTween.delayedCall(1f, () =>
                        {
                            // Scale closebtn to 1 (elastic ease)
                            LeanTween.scale(closebtn, new Vector2(1.2f,1.2f), 0.5f).setEase(LeanTweenType.easeOutElastic);

                            // Scale rembtn to 1 (elastic ease)
                            LeanTween.scale(rembtn, new Vector2(1.2f, 1.2f), 0.5f).setEase(LeanTweenType.easeOutElastic).setOnComplete(() =>
                            {
                                // Alpha imagerem1 to 1
                                LeanTween.alpha(imagerem1.GetComponent<RectTransform>(), 1f, 0.5f);

                                // Alpha imagerem2 to 1
                                LeanTween.alpha(imagerem2.GetComponent<RectTransform>(), 1f, 0.5f);
                            });
                        });
                    });
                });
            });
        });

    }

    public void ResetUIElements()
    {
        // Reset the scale of the text elements
        resText.transform.localScale = Vector3.zero;
        resText2.transform.localScale = Vector3.zero;

        // Reset the alpha of the images
        LeanTween.alpha(ResPCImage.GetComponent<RectTransform>(), 0f, 0.5f);
        LeanTween.alpha(imagerem1.GetComponent<RectTransform>(), 0f, 0.5f);
        LeanTween.alpha(imagerem2.GetComponent<RectTransform>(), 0f, 0.5f);

        // Reset the scale of the buttons
        closebtn.transform.localScale = Vector3.zero;
        rembtn.transform.localScale = Vector3.zero;

        // Stop any rotations
        LeanTween.cancel(circling1);
        circling1.transform.rotation = Quaternion.identity;
       
    }


    public async void OpenReadyPanel()
    {
        await Task.Delay(1000);
        LeanTween.scale(ReadyPanel, new Vector3(1f, 1f, 1f), .5f).setEase(LeanTweenType.easeInCubic);
        LeanTween.scale(ReadyBTN.gameObject, new Vector3(1f, 1f, 1f), 2f).setDelay(1.5f).setEase(LeanTweenType.easeOutElastic);
        ReadyUI.SetActive(true);
        CountdownUI.SetActive(false);
    }

    public void StartCountDown()
    {
        // Start the countdown sequence
        ReadyUI.SetActive(false);
        CountdownUI.SetActive(true);
        ScaleDown(three, () =>
        {
            ScaleDown(two, () =>
            {
                ScaleDown(one, showBuild);
            });
        });
    }

    private void ScaleDown(GameObject obj, System.Action onComplete)
    {
        // Start by scaling the object from 0 to 1
        obj.transform.localScale = Vector3.zero; // Ensure the object starts at scale 0

        LeanTween.scale(obj, Vector3.one, 0.5f).setEase(LeanTweenType.easeInCubic).setOnComplete(() =>
        {
            // Once it's scaled to 1, scale it back down to 0
            LeanTween.scale(obj, Vector3.zero, 0.5f).setEase(LeanTweenType.easeInCubic).setOnComplete(() =>
            {
                // Wait for a moment, then invoke the next action if needed
                LeanTween.delayedCall(0.5f, () =>
                {
                    if (onComplete != null)
                    {
                        onComplete();
                    }
                });
            });
        });
    }

    public void showBuild()
    {
        LeanTween.scale(Build, Vector3.one, 0.5f).setEase(LeanTweenType.easeInCubic).setOnComplete(CloseReadyPanel);
        
    }
    public async void CloseReadyPanel()
    {
        LeanTween.scale(ReadyPanel, new Vector3(0f, 0f, 0f), .5f).setDelay(1f).setEase(LeanTweenType.easeInCubic);
        LeanTween.scale(ReadyBTN.gameObject, new Vector3(0f, 0f, 0f), .5f).setDelay(1f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.scale(Build, new Vector3(0f, 0f, 0f), .5f).setDelay(1f).setEase(LeanTweenType.easeInCubic);
        await Task.Delay(2000);
        ReadyUI.SetActive(true);
        CountdownUI.SetActive(false);
        GameManager2.Instance.StartTimer();
    }





    //for Rematch

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
            currentMission = ClientController.instance.AddRandomMissionToGame();
        }


        ResetUIElements();
        rematchTimerSlider.maxValue = 10f;
        rematchTimerSlider.value = 10f;
        // Restart the game (reload the scene)
        resultPanel.SetActive(false);
        image1.SetActive(false);
        image2.SetActive(false);
        MainCamera.gameObject.SetActive(false);
        GameManager.instance.scene.manualLoading();
        SceneManager.LoadScene("PCRush", LoadSceneMode.Additive);
        OpenReadyPanel();




        if (IsHost)
        {

            gameEnded.Value = false;
            winnerClientId.Value = 0;
            hostRematchRequested.Value = false;
            clientRematchRequested.Value = false;
            hostReadyRequested.Value = false;
            clientReadyRequested.Value = false;
            //IsHardMode.Value = false;
            //IsNormalMode.Value = false;
            //IsEasyMode.Value = false;
            //Easy = false;
            //Normal = false;
            //Hard = false;

        }



    }

    //for Ready

    public void OnReadyButtonClick()
    {
        if (IsHost)
        {
            hostReadyRequested.Value = true;
        }
        else
        {
            RequestClientReadyServerRpc();
        }

        CheckReadyStatus();
    }


    private void CheckReadyStatus()
    {
        if (hostReadyRequested.Value && clientReadyRequested.Value)
        {
            StartReadyNowClientRpc();
        }
    }
    [ClientRpc]
    private void StartReadyNowClientRpc(ClientRpcParams clientRpcParams = default)
    {
        StartReady();
    }

    [ServerRpc(RequireOwnership = false)]
    void RequestClientReadyServerRpc(ServerRpcParams rpcParams = default)
    {
        clientReadyRequested.Value = true;

        CheckReadyStatus();
    }


    void StartReady()
    {



        //startCountdown logic here
        StartCountDown();
        image1Ready.SetActive(false);
        image2Ready.SetActive(false);


        if (IsHost)
        {

            gameEnded.Value = false;
            winnerClientId.Value = 0;
            hostRematchRequested.Value = false;
            clientRematchRequested.Value = false;
            hostReadyRequested.Value = false;
            clientReadyRequested.Value = false;

            //IsHardMode.Value = false;
            //IsNormalMode.Value = false;
            //IsEasyMode.Value = false;
            //Easy = false;
            //Normal = false;
            //Hard = false;

        }



    }




    //for closing
    public void OnCloseButton()
    {
        ResetUIElements();
        if (IsClient)
        {
            NotifyClientDisconnectServerRpc(NetworkManager.Singleton.LocalClientId);
        }
        
        
        if (IsHost)
        {
            NotifyHostDisconnectServerRpc();
        }
    }
    //private void OnApplicationQuit()
    //{
    //    // Debug.LogError("quitssss");
    //    //
    //    try
    //    {

    //        if (InGame.Value == true)
    //        {
    //            OnSurrenderButtonClick();
    //        }
    //        else
    //        {
    //            OnCloseButton();
    //        }
    //    }
    //    catch { }


    //}

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
        hostReadyRequested.Value = false;
        clientReadyRequested.Value = false;

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
        hostReadyRequested.Value = false;
        clientReadyRequested.Value = false;

        IsHardMode.Value = false;
        IsNormalMode.Value = false;
        IsEasyMode.Value = false;
        //Easy = false;
        //Normal = false;
        //Hard = false;
        InGame.Value = false;
        ResetUIElements();
        NetworkManager.Singleton.Shutdown();
        
       
    }

    void DeclareWinner(ulong loserClientId)
    {
        // Logic to determine and declare the winner
        // For simplicity, assume the other client is the winner

        Surrendering = false;
        ulong winnerClientId = NetworkManager.Singleton.ConnectedClientsIds.FirstOrDefault(id => id != loserClientId);

        TheGame.instance.winnerClientId.Value = winnerClientId;

        // Optionally, notify players about the winner
        Debug.Log($"Player {winnerClientId} is the winner!");

        // End the game
        EndGame(winnerClientId);
    }

}
