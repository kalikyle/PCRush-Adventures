using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkToStranger : QuestStep
{
    // Start is called before the first frame update
    void Start()
    {
        
        if (GameManager.instance.HasInitialize == false)
        {
            GameManager.instance.playerTeleport.ToCPUWorld();
        }

        DialogueManager.GetInstance().EnterDialogueMode(GameManager.instance.MainStory);
        DialogueManager.GetInstance().TriggerSection("Thirteen");


        GameManager.instance.CPUSpawn.SetActive(false);
        GameManager.instance.HasInitialize = true;
        GameManager.instance.PlayerDeskRoom.SetActive(true);
        GameManager.instance.BuildingDesk.SetActive(true);
        GameManager.instance.HouseDoor.SetActive(true);
        GameManager.instance.packagescollected = 8;
        GameManager.instance.LTA.CloseCPUTwirl();
        GameManager.instance.TheLostAdventurer.SetActive(true);
        GameManager.instance.ArenaWall.gameObject.SetActive(true);

        DialogueManager.GetInstance().talktoBTN.onClick.AddListener(TalkToStrangers);
    }

    public void TalkToStrangers()
    {
        if (GameManager.instance.OnQuest == true)
        {

            StartCoroutine(HandleDialogueAndQuestCompletion());
        }
        
    }

    private IEnumerator HandleDialogueAndQuestCompletion()
    {
        // Start the dialogue section "ThirteenTwo"
        DialogueManager.GetInstance().EnterDialogueMode(GameManager.instance.MainStory);
        DialogueManager.GetInstance().TriggerSection("ThirteenTwo");

        // Wait until the dialogue is no longer playing
        yield return new WaitUntil(() => !DialogueManager.GetInstance().dialogueIsPlaying);

        // Finish the quest step
        FinishQuestStep();

        // Change the state and update the quest status
        ChangeState("Finish", "Finish");
        GameManager.instance.OnQuest = false;
    }


    // Update is called once per frame
    public void Update()
    {
        if (GameManager.instance.CurrentNPC == "Lost Adventurer")
        {
            GameManager.instance.OnQuest = true;
        }
        else
        {
            GameManager.instance.OnQuest = false;
        }

        //if (DialogueManager.GetInstance().dialogueIsPlaying == false && GameManager.instance.OnQuest == true)
        //{
        //    FinishQuestStep();

        //    //// Change the state and update the quest status
        //    ChangeState("Finish", "Finish");
        //    GameManager.instance.OnQuest = false;
        //}
    }

    protected override void SetQuestStepState(string state)
    {


    }
}
