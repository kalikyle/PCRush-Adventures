using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class InternetChecker : MonoBehaviour
{
    public Canvas targetGameObject; // The GameObject to set active

    private void Start()
    {
        if (targetGameObject == null)
        {
            Debug.LogError("Target GameObject is not assigned.");
            return;
        }

        StartCoroutine(CheckInternetPeriodically());
    }

  
    private IEnumerator CheckInternetPeriodically()
    {
        while (true)
        {
            CheckInternet();
            yield return new WaitForSeconds(3f);
        }
    }

    private void CheckInternet()
    {
        NetworkReachability reachability = Application.internetReachability;
        Debug.Log("Internet Reachability: " + reachability);

        if (reachability == NetworkReachability.NotReachable)
        {
            targetGameObject.gameObject.SetActive(true);
            Debug.Log("No internet connection.");
        }
        else
        {
            targetGameObject.gameObject.SetActive(false);
            Debug.Log("Internet connection available.");
        }
    }
}