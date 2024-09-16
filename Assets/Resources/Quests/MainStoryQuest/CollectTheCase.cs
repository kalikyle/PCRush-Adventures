using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectTheCase : QuestStep
{
    // Start is called before the first frame update

    void Start()
    {
        GameManager.instance.OnCollectCaseQuest = true;

        if (GameManager.instance.HasInitialize == false)
        {
            DialogueManager.GetInstance().EnterDialogueMode(GameManager.instance.MainStory);
            DialogueManager.GetInstance().TriggerSection("ThirtyFour");

        }
        else
        {
            DialogueManager.GetInstance().EnterDialogueMode(GameManager.instance.MainStory);
            DialogueManager.GetInstance().TriggerSection("ThirtyThree");
        }

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
        if (GameManager.instance.OnCollectCaseQuest == true && GameManager.instance.CaseCollected == true)
        {
            FinishQuestStep();
            ChangeState("Finish", "Finish");

            GameManager.instance.OnCollectCaseQuest = false;
            GameManager.instance.CaseCollected = false;
        }
    }

    protected override void SetQuestStepState(string state)
    {


    }
}
