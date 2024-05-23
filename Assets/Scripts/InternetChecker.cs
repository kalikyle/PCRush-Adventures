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
    }

    private void Update()
    {
        CheckInternet();
    }

    private void CheckInternet()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            targetGameObject.gameObject.SetActive(true);
        }
        else
        {
            targetGameObject.gameObject.SetActive(false);
        }
    }
}