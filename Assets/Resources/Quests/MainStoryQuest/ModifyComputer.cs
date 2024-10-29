using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifyComputer : QuestStep
{
    // Start is called before the first frame update
    public string StepInfo = "Modify Your computer, just follow the instructions";
    void Start()
    {
       
        GameManager.instance.OnModifyQuest = true;


        DialogueManager.GetInstance().EnterDialogueMode(GameManager.instance.MainStory);
        DialogueManager.GetInstance().TriggerSection("TwentyTwo");

        GameManager.instance.HasInitialize = true;
        GameManager.instance.CPUSpawn.SetActive(true);
        GameManager.instance.ExitRoom.SetActive(true);
        GameManager.instance.PlayerDeskRoom.SetActive(false);
        GameManager.instance.BuildingDesk.SetActive(true);
        GameManager.instance.HouseDoor.SetActive(true);
        GameManager.instance.packagescollected = 8;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (GameManager.instance.OnModifyQuest == true && GameManager.instance.DoneModify == true)
        {
            FinishQuestStep();
            ChangeState("Finish", "Finish");

            GameManager.instance.OnModifyQuest = false;
            GameManager.instance.DoneModify = false;
        }
    }
    protected override void SetQuestStepState(string state)
    {


    }
}
