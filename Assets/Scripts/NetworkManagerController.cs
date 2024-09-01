using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class NetworkManagerController : MonoBehaviour
{
    public Button createGameButton;
    public Button joinGameButton;
    public TMP_Text feedbackText;

    void Start()
    {
        createGameButton.onClick.AddListener(CreateGame);
        joinGameButton.onClick.AddListener(JoinGame);

        // Register event handlers
        NetworkManager.Singleton.OnServerStarted += OnServerStarted;
        NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
    }

    void CreateGame()
    {
        NetworkManager.Singleton.StartHost();
        feedbackText.text = "Creating game...";
    }

    void JoinGame()
    {
        NetworkManager.Singleton.StartClient();
        feedbackText.text = "Joining game...";
    }

    void OnServerStarted()
    {
        feedbackText.text = "Game created successfully!";
    }
    
    void OnClientConnected(ulong clientId)
    {
        if (NetworkManager.Singleton.IsHost && clientId == NetworkManager.Singleton.LocalClientId)
        {
            feedbackText.text = "Game created successfully!";
        }
        else
        {
            feedbackText.text = "Joined game successfully!";
        }
    }

    void OnDestroy()
    {
        // Unregister event handlers
        if (NetworkManager.Singleton != null)
        {
            NetworkManager.Singleton.OnServerStarted -= OnServerStarted;
            NetworkManager.Singleton.OnClientConnectedCallback -= OnClientConnected;
        }
    }
}
