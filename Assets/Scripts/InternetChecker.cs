using Firebase;
using Firebase.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

public class InternetChecker : MonoBehaviour
{
    public GameObject targetGameObject; // The GameObject to set active
    private bool isCheckingConnection = false;
    private void Start()
    {
        isCheckingConnection = true;
    }

    [System.Obsolete]
    private IEnumerator CheckInternetConnection()
    {
        // Check for internet connectivity using UnityWebRequest
        using (UnityWebRequest www = UnityWebRequest.Get("https://www.google.com"))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log("No internet connection.");
                HandleConnectionLost();
            }
            else
            {
                Debug.Log("Internet connection available.");
                //HandleConnectionAvailable();
            }
        }
    }

    [System.Obsolete]
    private void HandleConnectionLost()
    {
        Time.timeScale = 0f;
        targetGameObject.SetActive(true);
        Debug.Log("Internet connection lost. Pausing game.");

        StartCoroutine(WaitForConnectionOrExit());
    }

    //private void HandleConnectionAvailable()
    //{
    //    Time.timeScale = 1f;
    //    targetGameObject.SetActive(false);
    //    Debug.Log("Internet connection available. Resuming game.");
    //}

    [System.Obsolete]
    private IEnumerator WaitForConnectionOrExit()
    {
        float timer = 0f;

        while (timer < 10f)
        {
            using (UnityWebRequest www = UnityWebRequest.Get("https://www.google.com"))
            {
                yield return www.SendWebRequest();

                if (!(www.isNetworkError || www.isHttpError))
                {
                    Time.timeScale = 1f;
                    targetGameObject.SetActive(false);
                    Debug.Log("Internet connection restored. Resuming game.");
                    yield break;
                }
            }

            timer += Time.unscaledDeltaTime;
            yield return null;
        }

        Debug.Log("Connection not restored. Exiting game.");
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    [System.Obsolete]
    private void Update()
    {
        if (isCheckingConnection && Time.timeScale > 0)
        {
            StartCoroutine(CheckInternetConnection());
        }
    }
}
