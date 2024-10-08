using Firebase;
using Firebase.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

public class InternetChecker : MonoBehaviour
{
    
    public static InternetChecker Instance;
    private bool isInternetAvailable;
    private float checkInterval = 2f;  // How often to check internet
    //private float noInternetTimer = 10f;  // Time before going back to Main Menu if internet is not restored
    public GameObject noInternetPopup;
    private Coroutine checkInternetCoroutine;
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

    public bool CheckInternetConnection()
    {
        var reachability = Application.internetReachability;
        //Debug.Log("Internet Reachability: " + reachability);

        if (reachability == NetworkReachability.NotReachable)
        {
            return false;
        }
        return true;
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
