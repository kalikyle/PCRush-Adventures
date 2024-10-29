using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifyYourPC : QuestStep
{
    // Start is called before the first frame update
    public string StepInfo = "Just Modify Your PC";
    void Start()
    {
       
        GameManager.instance.OnModifyCaseQuest = true;

        DialogueManager.GetInstance().EnterDialogueMode(GameManager.instance.MainStory);
        DialogueManager.GetInstance().TriggerSection("ThirtyFive");

        GameManager.instance.HasInitialize = true;
        GameManager.instance.CPUSpawn.SetActive(true);
        GameManager.instance.ExitRoom.SetActive(true);
        GameManager.instance.PlayerDeskRoom.SetActive(true);
        GameManager.instance.BuildingDesk.SetActive(true);
        GameManager.instance.HouseDoor.SetActive(true);
        GameManager.instance.packagescollected = 8;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (GameManager.instance.OnModifyCaseQuest == true && GameManager.instance.DoneModifyCase == true)
        {
            FinishQuestStep();
            ChangeState("Finish", "Finish");

            GameManager.instance.OnModifyCaseQuest = false;
            GameManager.instance.DoneModifyCase = false;
        }
    }
    protected override void SetQuestStepState(string state)
    {


    }
}
