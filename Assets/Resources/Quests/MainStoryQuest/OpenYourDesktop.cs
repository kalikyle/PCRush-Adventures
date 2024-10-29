using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenYourDesktop : QuestStep
{
    // Start is called before the first frame update
    public string StepInfo = "Go to your Desktop to see your Hardwork";
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
    bool dialogueOpened = false;
    void Update()
    {
       
        if (GameManager.instance.OpenYourDesktopFinish == true && GameManager.instance.DesktopOpenedFinish == true && dialogueOpened == false)
        {

            //FinishQuestStep();
            //ChangeState("Finish", "Finish");
            StartCoroutine(HandleDialogueAndQuestCompletion());
            dialogueOpened = true;
        }


        if(GameManager.instance.OpenYourDesktopFinish == true && GameManager.instance.DesktopOpenedFinish == true && GameManager.instance.OverallFinish == true)
        {
            FinishQuestStep();
            ChangeState("Finish", "Finish");
            //GameManager.instance.CurrentQuestInfo = "";
            GameManager.instance.OpenYourDesktopFinish = false;
            GameManager.instance.DesktopOpenedFinish = false;
            GameManager.instance.OverallFinish = false;
            GameManager.instance.HomeWorldFence.SetActive(false);  

            AchievementManager.instance.FinishStory = true;
            AchievementManager.instance.CheckAchievements();
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
        GameManager.instance.CutScene11.SetActive(true);

    }

    protected override void SetQuestStepState(string state)
    {


    }
}
