using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindandMeetExchanger : QuestStep
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.CPUSpawn.SetActive(false);
        GameManager.instance.HasInitialize = true;
        GameManager.instance.PlayerDeskRoom.SetActive(true);
        GameManager.instance.BuildingDesk.SetActive(true);
        GameManager.instance.HouseDoor.SetActive(true);
        GameManager.instance.packagescollected = 8;


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void SetQuestStepState(string state)
    {


    }
}
