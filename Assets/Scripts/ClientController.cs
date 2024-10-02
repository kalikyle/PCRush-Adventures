using Orders.Model;
using Orders.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ClientController : MonoBehaviour
{
    public static ClientController instance;

    [SerializeField]
    private MissionConSO OrderData;

    
    void Awake()
    {
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
    }
    void Start()
    {
      
        //OrderData.ShuffleMissions();
     
    }

    public Missions AddRandomMissionToGame()
    {
        int randomIndex = UnityEngine.Random.Range(0, OrderData.missionOrders.Count-1);
        Missions randomMission = OrderData.GetItemAt(randomIndex);
        //feedbackText.text = "Random mission added for hard mode.";
        return randomMission;
        
    }
}

