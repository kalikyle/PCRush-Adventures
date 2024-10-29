using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExploreDesktop : QuestStep
{
    // Start is called before the first frame update
    public string StepInfo = "Just Click The Desktop Icons";
    void Start()
    {
        
        GameManager.instance.OnExploreDesktopQuest = true;
        
        if (GameManager.instance.HasInitialize == false){

            DialogueManager.GetInstance().EnterDialogueMode(GameManager.instance.MainStory);
            DialogueManager.GetInstance().TriggerSection("TwelveInitial");
        }

        GameManager.instance.HasInitialize = true;
        GameManager.instance.PlayerDeskRoom.SetActive(true);
        GameManager.instance.BuildingDesk.SetActive(true);
        GameManager.instance.HouseDoor.SetActive(true);
        GameManager.instance.packagescollected = 8;
        GameManager.instance.TheLostAdventurer.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (GameManager.instance.OnExploreDesktopQuest == true && GameManager.instance.OnExploreDeskDone == true && GameManager.instance.OnCutScene7Open == true)
        {
            FinishQuestStep();
            ChangeState("Finish", "Finish");

            GameManager.instance.OnExploreDesktopQuest = false;
            GameManager.instance.OnExploreDeskDone = false;
            GameManager.instance.OnCutScene7Open = false;

            
        }
    }
    protected override void SetQuestStepState(string state)
    {


    }
}
