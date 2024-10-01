using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Threading.Tasks;

public class GameListener : MonoBehaviour
{
    public GameObject buttonPrefab;
    public Transform buttonContainer;
    private const int BroadcastPort = 47777;
    private UdpClient udpClient;
    private bool listening = true;

    public NewGameManager NewGameManager;

    void Start()
    {
        udpClient = new UdpClient(BroadcastPort);
        StartListening();
    }

    //private async void Update()
    //{
    //    await Task.Delay(1000);
    //    StartListening();
    //}

    void StartListening()
    {

        if (udpClient == null)
        {
            Debug.LogWarning("UdpClient is null, cannot start listening.");
            return;
        }

        try
        {
            udpClient.BeginReceive(OnDataReceived, null);
        }
        catch (ObjectDisposedException)
        {
            Debug.LogWarning("UdpClient has been disposed, cannot start listening.");
        }
    }

    void OnDataReceived(IAsyncResult ar)
    {
        if (!listening) return;

        try
        {
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, BroadcastPort);
            byte[] data = udpClient.EndReceive(ar, ref endPoint);
            string message = Encoding.UTF8.GetString(data);

            Debug.Log($"Received message: {message}");

            // Parse the message and handle it on the main thread
            //MainThreadDispatcher.Enqueue(() =>
            //{
            //    string[] parts = message.Split(':');
            //    if (parts.Length == 2)
            //    {
            //        string type = parts[0];
            //        string ipAddress = parts[1];

            //        Debug.Log($"Type: {type}, IP: {ipAddress}");

            //        if (type == "GAME")
            //        {
            //            AddGameButton(ipAddress);
            //        }
            //        else if (type == "CANCEL")
            //        {
            //            RemoveGameButton(ipAddress);
            //        }
            //    }
            //});

            MainThreadDispatcher.Enqueue(() =>
            {
                // Split by space first to get each key-value pair
                string[] parts = message.Split(' ');

                // Initialize variables to store values
                string type = null;
                string ipAddress = null;
                string gameName = null;
                string gameMode = null;

                // Process each key-value pair
                foreach (string part in parts)
                {
                    string[] keyValue = part.Split(':');
                    if (keyValue.Length == 2)
                    {
                        switch (keyValue[0])
                        {
                            case "GAME":
                                type = keyValue[0]; // Store "GAME"
                                ipAddress = keyValue[1]; // Store the IP address
                                break;
                            case "GAMENAME":
                                gameName = keyValue[1]; // Store the game name
                                break;
                            case "GAMEMODE":
                                gameMode = keyValue[1]; // Store the game mode
                                break;
                        }
                    }
                }

                // Debug log to verify the parsed values
                Debug.Log($"Type: {type}, IP: {ipAddress}, Game Name: {gameName}, Game Mode: {gameMode}");

                // Handle the "GAME" type and add/remove game buttons accordingly
                if (type == "GAME")
                {
                    AddGameButton(ipAddress, gameName, gameMode);
                }
                else if (type == "CANCEL")
                {
                    RemoveGameButton(ipAddress);
                }
            });
         }
        catch (Exception ex)
        {
            Debug.LogError($"Error in OnDataReceived: {ex.Message}");
        }
        finally
        {
            StartListening(); // Restart listening
        }

    }

    void AddGameButton(string ipAddress,string gamename, string gamemode)
    {
        GameObject buttonObject = Instantiate(buttonPrefab, buttonContainer);
        Button button = buttonObject.GetComponent<Button>();


        TMP_Text[] texts = button.GetComponentsInChildren<TMP_Text>();

        foreach (var text in texts)
        {
           
            if (text.gameObject.name == "Name") 
            {
                text.text = gamename;
            }
            else if (text.gameObject.name == "Ipaddress") 
            {
                text.text = ipAddress;
            }
            else if (text.gameObject.name == "Mode")
            {

                text.text = gamemode;
            }

            //button.GetComponentInChildren<TMP_Text>().text = ipAddress;
            
        }

        button.onClick.AddListener(() => OnJoinButtonClick(ipAddress));
    }

    void RemoveGameButton(string ipAddress)
    {
        foreach (Transform child in buttonContainer)
        {
            Button button = child.GetComponent<Button>();
            TMP_Text[] texts = button.GetComponentsInChildren<TMP_Text>();

            foreach (var text in texts)
            {

                if (text.gameObject.name == "Ipaddress")
                {

                    if (text.text == ipAddress)
                    {

                        Destroy(child.gameObject);
                        break;
                    }
                }

            }


                
        }
    }

    void OnJoinButtonClick(string ipAddress)
    {
        NewGameManager.hostIpInput.text = ipAddress;
        NewGameManager.JoinGame();
    }

    void OnApplicationQuit()
    {
        listening = false;
        udpClient.Close();
    }
}
