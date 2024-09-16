using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenYourDesktop : QuestStep
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.OpenYourDesktopFinish = true;

        if (GameManager.instance.HasInitialize == false)
        {

            DialogueManager.GetInstance().EnterDialogueMode(GameManager.instance.MainStory);
            DialogueManager.GetInstance().TriggerSection("ThirtySixIntro");
        }
        else
        {

            DialogueManager.GetInstance().EnterDialogueMode(GameManager.instance.MainStory);
            DialogueManager.GetInstance().TriggerSection("ThirtySixIntroTwo");
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
        if (GameManager.instance.OpenYourDesktopFinish == true && GameManager.instance.DesktopOpenedFinish == true)
        {

            //FinishQuestStep();
            //ChangeState("Finish", "Finish");
            StartCoroutine(HandleDialogueAndQuestCompletion());

            GameManager.instance.OpenYourDesktopFinish = false;
            GameManager.instance.DesktopOpenedFinish = false;
        }
    }

    private IEnumerator HandleDialogueAndQuestCompletion()
    {
        // Start the dialogue section "ThirteenTwo"
        DialogueManager.GetInstance().EnterDialogueMode(GameManager.instance.MainStory);
        DialogueManager.GetInstance().TriggerSection("ThirtySix");

        // Wait until the dialogue is no longer playing
        yield return new WaitUntil(() => !DialogueManager.GetInstance().dialogueIsPlaying);

        // Finish the quest step
        //FinishQuestStep();

        //// Change the state and update the quest status
        //ChangeState("Finish", "Finish");

        // Cutscene Credits and Unlocked

    }

    protected override void SetQuestStepState(string state)
    {


    }
}
