using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnComputer : QuestStep
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.OnTurnOnQuest = true;
        DialogueManager.GetInstance().EnterDialogueMode(GameManager.instance.MainStory);
        DialogueManager.GetInstance().TriggerSection("Eleven");

        GameManager.instance.HasInitialize = true;
        GameManager.instance.PlayerDeskRoom.SetActive(true);
        GameManager.instance.BuildingDesk.SetActive(true);
        GameManager.instance.HouseDoor.SetActive(true);
        GameManager.instance.packagescollected = 8;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.OnTurnOnQuest == true && GameManager.instance.PCTurnOn == true)
        {
            FinishQuestStep();
            ChangeState("Finish", "Finish");

            GameManager.instance.OnTurnOnQuest = false;
            GameManager.instance.PCTurnOn = false;
        }
    }

    protected override void SetQuestStepState(string state)
    {


    }
}
