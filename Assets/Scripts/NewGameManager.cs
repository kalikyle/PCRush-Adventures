using System.Collections;
using System.Collections.Generic;
using System.Net;
using TMPro;
using Unity.Netcode.Transports.UTP;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using Orders.Model;
using UnityEngine.XR;

public class NewGameManager : MonoBehaviour
{
    public TMP_InputField gameNameInput;
    public TMP_InputField hostIpInput; // New input field for host IP address
    public TMP_Dropdown gameModeDropdown;

    public Button lobbyJoinButton;
    public Button lobbyCreateButton;
    public Button CancelButton;
    public TMP_Text feedbackText;

    public Button createGameButton;
    public Button joinGameButton;

    //public GameObject GamePanel;
    public GameBroadcaster broadcaster;
    public UnityTransport transport;


    public GameObject EasyInstruc;
    public GameObject NormalInstruc;
    public GameObject HardInstruc;

    private Coroutine connectionTimeoutCoroutine;
    void Start()
    {
        createGameButton.onClick.AddListener(CreateGame);
        joinGameButton.onClick.AddListener(JoinGame);
        CancelButton.onClick.AddListener(CancelGame);
        NetworkManager.Singleton.OnServerStarted += OnServerStarted;
        NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
    }
    public void HandleInstruc(int val)
    {

        switch (val)
        {
            case 0://all
                EasyInstruc.SetActive(true);
                NormalInstruc.SetActive(false);
                HardInstruc.SetActive(false);
                break;
            case 1:
                EasyInstruc.SetActive(false);
                NormalInstruc.SetActive(true);
                HardInstruc.SetActive(false);
                break;
            case 2:
                EasyInstruc.SetActive(false);
                NormalInstruc.SetActive(false);
                HardInstruc.SetActive(true);
                break;

        }
    }
    void CreateGame()
    {
        string gameName = gameNameInput.text;
        string gameMode = gameModeDropdown.options[gameModeDropdown.value].text;

        if (string.IsNullOrEmpty(gameName))
        {
            feedbackText.text = "Please enter a game name.";
            return;
        }

        NetworkManager.Singleton.StartHost();
        ResetGameState();
        string hostIpAddress = GetLocalIPAddress();
        feedbackText.text = $"Creating game '{gameName}' in {gameMode} mode... Host IP: {hostIpAddress}";
        CancelButton.gameObject.SetActive(true);
        
        lobbyCreateButton.interactable = false;
        lobbyJoinButton.interactable = false;

        broadcaster.BroadcastGameCreation(hostIpAddress);

        switch (gameModeDropdown.value)
        {
            case 0: // Easy
                SetupEasyMode();
                break;
            case 1: // Normal
                SetupNormalMode();
                break;
            case 2: // Hard
                SetupHardMode();
                break;
            default:
                feedbackText.text = "Invalid game mode selected.";
                return;
        }

         

        //GameBroadcaster broadcaster = new GameBroadcaster();
        
    }

    void SetupEasyMode()
    {
        // Code for setting up the game in Easy mode
        Debug.Log("Easy mode selected.");
       
        TheGame.instance.SetGameMode(true, false, false);
        TheGame.instance.currentMission = Missions.Empty;
        // Add your easy mode setup code here
    }

    void SetupNormalMode()
    {
        // Code for setting up the game in Normal mode
        Debug.Log("Normal mode selected.");
       
        TheGame.instance.SetGameMode(false, true, false);
        TheGame.instance.currentMission = Missions.Empty;
        // Add your normal mode setup code here
    }

    void SetupHardMode()
    {
        // Code for setting up the game in Hard mode
        Debug.Log("Hard mode selected.");
        
        TheGame.instance.SetGameMode(false, false, true);
        TheGame.instance.currentMission = ClientController.instance.AddRandomMissionToGame();
        // Add your hard mode setup code here
    }

    public void JoinGame()
    {
        string hostIpAddress = hostIpInput.text;
        broadcaster.BroadcastGameCancellation(hostIpAddress);

        if (string.IsNullOrEmpty(hostIpAddress))
        {
            feedbackText.text = "Please enter the host's IP address.";
            return;
        }
        if (!IPAddress.TryParse(hostIpAddress, out IPAddress parsedAddress))
        {
            feedbackText.text = "Please enter a valid IP address.";
            return;
        }

        transport.ConnectionData.Address = hostIpAddress;

        NetworkManager.Singleton.StartClient();
        feedbackText.text = $"Joining game at '{hostIpAddress}'...";

        if (connectionTimeoutCoroutine != null)
        {
            StopCoroutine(connectionTimeoutCoroutine);
        }

        connectionTimeoutCoroutine = StartCoroutine(ConnectionTimeoutCoroutine(10f));
    }

    void OnClientConnected(ulong clientId)
    {
        //if (NetworkManager.Singleton.IsHost && clientId == NetworkManager.Singleton.LocalClientId)
        //{
        //    feedbackText.text = "Game created successfully!";
        //}
        //else
        //{
        //    feedbackText.text = "Joined game successfully!";
        //    OpenGame();
        //}
        if (NetworkManager.Singleton.IsHost && clientId == NetworkManager.Singleton.LocalClientId)
        {
            feedbackText.text = "Game created successfully!";
        }
        else
        {
            //if (connectionTimeoutCoroutine != null)
            //{
            //    StopCoroutine(connectionTimeoutCoroutine);
            //    connectionTimeoutCoroutine = null;
            //}

            
            //for Hardmode client side
            if (TheGame.instance.IsHardMode.Value && TheGame.instance.currentMission.isEmpty)
            {
                Debug.LogError(TheGame.instance.IsHardMode.Value);
                //TheGame.instance.Hard = true;
                TheGame.instance.currentMission = ClientController.instance.AddRandomMissionToGame();
            }

            else if(TheGame.instance.IsNormalMode.Value){

                //TheGame.instance.Normal = true;
                
            }

            else if (TheGame.instance.IsEasyMode.Value) { 
            
              // TheGame.instance.Easy = true;
            }



            feedbackText.text = "Joined game successfully!";
            lobbyCreateButton.interactable = true;
            lobbyJoinButton.interactable = true;
            CancelButton.gameObject.SetActive(false);
            OpenGame();
        }
    }

    void OnServerStarted()
    {
        feedbackText.text = "Game created successfully!";
    }

    IEnumerator ConnectionTimeoutCoroutine(float timeout)
    {
        yield return new WaitForSeconds(timeout);

        if (!NetworkManager.Singleton.IsConnectedClient)
        {
            NetworkManager.Singleton.Shutdown();
            feedbackText.text = "Failed to join game. Host may be unreachable.";
        }
    }

    string GetLocalIPAddress()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
            {
                return ip.ToString();
            }
        }
        throw new System.Exception("No network adapters with an IPv4 address in the system!");
    }

    public void OpenGame()
    {
        //GamePanel.SetActive(true);
        TheGame.instance.MainCamera.gameObject.SetActive(false);
        SceneManager.LoadScene("PCRush", LoadSceneMode.Additive);
    }

   
    public void CancelGame()
    {

        //TheGame.instance.gameEnded.Value = false;
        //TheGame.instance.winnerClientId.Value = 0;
        //TheGame.instance.hostRematchRequested.Value = false;
        //TheGame.instance.clientRematchRequested.Value = false;
       // GameBroadcaster broadcaster = new GameBroadcaster();
        string hostIpAddress = GetLocalIPAddress();
        broadcaster.BroadcastGameCancellation(hostIpAddress);

        NetworkManager.Singleton.Shutdown();
        feedbackText.text = "Game has been cancelled.";

            // Reset UI elements
            gameNameInput.text = "";
            hostIpInput.text = "";
            gameModeDropdown.value = 0;
            CancelButton.gameObject.SetActive(false);
            lobbyCreateButton.interactable = true;
            lobbyJoinButton.interactable = true;

     
            
        

    }

    void ResetGameState()
    {
        // Reset relevant NetworkVariables
        var gameManager = TheGame.instance;
        if (gameManager != null)
        {
            gameManager.gameEnded.Value = false;
            gameManager.winnerClientId.Value = 0;
            gameManager.hostRematchRequested.Value = false;
            gameManager.clientRematchRequested.Value = false;
            gameManager.IsHardMode.Value = false;
            gameManager.IsNormalMode.Value = false;
            gameManager.IsEasyMode.Value = false;
            gameManager.InGame.Value = false;
            //gameManager.Easy = false;
            //gameManager.Normal = false;
            //gameManager.Hard = false;
        }
    }
}
