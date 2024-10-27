using Firebase;
using Firebase.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.NetworkInformation;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

public class InternetChecker : MonoBehaviour
{
    
    public static InternetChecker Instance;
    private bool isInternetAvailable;
    private float checkInterval = 2f;
    public TMP_Text internetStatusText;// How often to check internet
    //private float noInternetTimer = 10f;  // Time before going back to Main Menu if internet is not restored
    public GameObject noInternetPopup;
    private Coroutine checkInternetCoroutine;
    public string internetCheckUrl = "https://google.com"; // Google's public DNS server
    private void Start()
    {
        //StartCoroutine(CheckInternetConnectionRoutine());
    }

    public void Awake()
    {
        if (Instance == null)
        {

            Instance = this;
            DontDestroyOnLoad(gameObject);

        }
        else
        {
            // If another instance already exists, destroy this one
            Destroy(gameObject);
        }
    }

    public void StartCheck()
    {
        if (checkInternetCoroutine == null) // Only start if not already running
        {
            checkInternetCoroutine = StartCoroutine(CheckInternetConnectionRoutine());
        }
    }

    public void StopCheck()
    {
        if (checkInternetCoroutine != null)
        {
            StopCoroutine(checkInternetCoroutine);
            checkInternetCoroutine = null;
        }
    }

    public IEnumerator CheckInternetConnectionRoutine()
    {
        //Debug.Log("Started");
        while (true)
        {
            isInternetAvailable = CheckInternetConnection();

            if (!isInternetAvailable)
            {
                // Show the no internet UI popup if internet is lost
                noInternetPopup.SetActive(true);

            }
            else
            {
                noInternetPopup.SetActive(false);
            }

            yield return new WaitForSeconds(checkInterval);
        }
    }

    //public bool CheckInternetConnection()
    //{
    //    var reachability = Application.internetReachability;
    //    //Debug.Log("Internet Reachability: " + reachability);

    //    if (reachability == NetworkReachability.NotReachable)
    //    {
    //        return false;
    //    }
    //    return true;
    //}

    //public bool CheckInternetConnection()
    //{
    //    var reachability = Application.internetReachability;

    //    // Check if there is no internet at all
    //    if (reachability == NetworkReachability.NotReachable)
    //    {
    //        internetStatusText.text = "No Internet"; // Update UI text
    //        internetStatusText.color = Color.red; // Set text color to red
    //        //Debug.LogWarning("No Internet Connection");
    //        return false; // Return false if no internet
    //    }

    //    // Start a ping to check connection quality
    //    Ping ping = new Ping("8.8.8.8"); // Google's DNS server for testing
    //    float startTime = Time.time;

    //    // Wait for the ping to finish or time out
    //    while (!ping.isDone)
    //    {
    //        if (Time.time - startTime > 1f) // Timeout after 1 second
    //        {
    //            //Debug.LogWarning("Ping timed out, weak internet connection");
    //            internetStatusText.text = "1000ms"; // Timeout value shown as 1000ms
    //            internetStatusText.color = Color.red; // Set text color to red
    //            return true; // Return true for weak internet (still connected)
    //        }
    //    }

    //    // If ping result is weak
    //    if (ping.time >= 300) // Consider weak if ping time > 300ms
    //    {
    //        internetStatusText.text = ping.time + "ms"; // Display ping time in ms
    //        internetStatusText.color = Color.red; // Set text color to red
    //        //Debug.LogWarning("Weak internet connection, ping: " + ping.time + "ms");
    //        return true; // Return true (still connected but weak)
    //    }

    //    // If internet connection is good
    //    internetStatusText.text = ping.time + "ms"; // Display ping time in ms
    //    internetStatusText.color = Color.green; // Set text color to green
    //    //Debug.Log("Good internet connection, ping: " + ping.time + "ms");
    //    return true; // Return true for good connection
    //}

    //    public bool CheckInternetConnection()
    //    {
    //#if !UNITY_WSA || UNITY_EDITOR

    //        var reachability = Application.internetReachability;

    //        if (reachability == NetworkReachability.NotReachable)
    //        {
    //            internetStatusText.text = "No Internet";
    //            internetStatusText.color = Color.red;
    //            return false;
    //        }
    //#else
    //     var connectionProfile = Windows.Networking.Connectivity.NetworkInformation.GetInternetConnectionProfile();
    //    if (connectionProfile == null)
    //      return false;
    //    return connectionProfile.GetNetworkConnectivityLevel() == Windows.Networking.Connectivity.NetworkConnectivityLevel.InternetAccess;

    //#endif


    //        // Start a coroutine to perform the ping operation asynchronously
    //        StartCoroutine(CheckInternetConnectionAsync());
    //        return true;
    //    }

    //private IEnumerator CheckInternetConnectionAsync()
    //{
    //    Ping ping = new Ping("8.8.8.8");
    //    float startTime = Time.time;

    //    while (!ping.isDone)
    //    {
    //        if (Time.time - startTime > 1f) // Timeout after 1 second
    //        {
    //            internetStatusText.text = "1000ms";
    //            internetStatusText.color = Color.red;
    //            SendMessage("UpdateInternetConnectionResult", false);
    //            yield break; // Cancel the coroutine
    //        }
    //        yield return null; // Wait for the next frame
    //    }

    //    // Process the ping result
    //    if (ping.time >= 300)
    //    {
    //        internetStatusText.text = ping.time + "ms";
    //        internetStatusText.color = Color.red;

    //        SendMessage("UpdateInternetConnectionResult", false);
    //    }
    //    else
    //    {
    //        internetStatusText.text = ping.time + "ms";
    //        internetStatusText.color = Color.green;

    //        SendMessage("UpdateInternetConnectionResult", true);
    //    }
    //}

    //private void UpdateInternetConnectionResult(bool isConnected)
    //{
    //    // Update the result of CheckInternetConnection()
    //    CheckInternetConnectionResult = isConnected;
    //}



    //private bool CheckInternetConnectionResult { get; set; }
    ////Start a ping to check connection quality
    //Ping ping = new Ping("8.8.8.8"); // Google's DNS server for testing
    //float startTime = Time.time;

    //// Wait for the ping to finish or time out
    //while (!ping.isDone)
    //{
    //    if (Time.time - startTime > 1f) // Timeout after 1 second
    //    {
    //        //Debug.LogWarning("Ping timed out, weak internet connection");
    //        internetStatusText.text = "1000ms"; // Timeout value shown as 1000ms
    //        internetStatusText.color = Color.red; // Set text color to red
    //        return true; // Return true for weak internet (still connected)
    //    }
    //}

    //// If ping result is weak
    //if (ping.time >= 300) // Consider weak if ping time > 300ms
    //{
    //    internetStatusText.text = ping.time + "ms"; // Display ping time in ms
    //    internetStatusText.color = Color.red; // Set text color to red
    //                                          //Debug.LogWarning("Weak internet connection, ping: " + ping.time + "ms");
    //    return true; // Return true (still connected but weak)
    //}

    //// If internet connection is good
    //internetStatusText.text = ping.time + "ms"; // Display ping time in ms
    //internetStatusText.color = Color.green; // Set text color to green
    public bool CheckInternetConnection()
    {

#if UNITY_ANDROID || UNITY_EDITOR

      return CheckAndroidAndEditorConnection();
#else
   //put the internet connectivity here if its windows
   return CheckWindowsInternetConnection();
#endif
    }


    private bool CheckAndroidAndEditorConnection()
    {
        var reachability = Application.internetReachability;

        if (reachability == NetworkReachability.NotReachable)
        {
            internetStatusText.text = "No Internet";
            internetStatusText.color = Color.red;
            return false;
        }
        else
        {
            internetStatusText.text = "";
        }

     return true;
    }

    private bool CheckWindowsInternetConnection()
    {
        UnityEngine.Debug.LogError("kantits");
        const string GOOGLE_DNS = "8.8.8.8"; // Google's DNS server

        try
        {
            using (System.Net.NetworkInformation.Ping ping = new System.Net.NetworkInformation.Ping())
            {
                PingReply reply = ping.Send(GOOGLE_DNS);

                if (reply.Status == IPStatus.Success)
                {
                    string text = reply.RoundtripTime + "ms";
                    Color color;

                    if (reply.RoundtripTime > 200)
                    {
                        color = Color.red;
                    }
                    else if (reply.RoundtripTime > 100)
                    {
                        color = Color.yellow;
                    }
                    else
                    {
                        color = Color.green;
                    }
                    internetStatusText.text = text;
                    internetStatusText.color = color;
                    return true; // Internet is available
                }
                else
                {
                    internetStatusText.text = "No Internet";
                    internetStatusText.color = Color.red;
                    return false; // No internet
                }
            }
        }
        catch (Exception)
        {
            //Debug.LogError("Error checking internet connection: " + e.Message);
            internetStatusText.text = "No Internet";
            internetStatusText.color = Color.red; // Indicate an error
            return false; // Assume no internet on error
        }
    }

    public bool TryStartGame()
    {
        if (!CheckInternetConnection())
        {
            return false; // Internet is not available, don't start the game
        }
        return true; // Internet is available, proceed to start the game
    }


}
