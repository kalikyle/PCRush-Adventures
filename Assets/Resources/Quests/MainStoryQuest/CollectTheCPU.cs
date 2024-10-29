using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectTheCPU : QuestStep
{
    // Start is called before the first frame update
    public string StepInfo = "Go Downstairs and Collect the CPU you exchanged";
    void Start()
    {
        
        DialogueManager.GetInstance().EnterDialogueMode(GameManager.instance.MainStory);
        DialogueManager.GetInstance().TriggerSection("TwentyOne");

        GameManager.instance.OnCollectCPUQuest = true; 

        GameManager.instance.HasInitialize = true;
        GameManager.instance.CPUSpawn.SetActive(true);
        GameManager.instance.ExitRoom.SetActive(true);
        GameManager.instance.PlayerDeskRoom.SetActive(false);
        GameManager.instance.BuildingDesk.SetActive(false);
        GameManager.instance.HouseDoor.SetActive(true);
        GameManager.instance.packagescollected = 8;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (GameManager.instance.OnCollectCPUQuest == true && GameManager.instance.CPUCollected == true)
        {
            FinishQuestStep();
            ChangeState("Finish", "Finish");

            GameManager.instance.OnCollectCPUQuest = false;
            GameManager.instance.CPUCollected = false;
        }
    }
    protected override void SetQuestStepState(string state)
    {


    }
}
