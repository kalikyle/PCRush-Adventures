using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuilldFirstPC : QuestStep
{
    private GameObject Ian;
    public Vector3 targetPosition;
    public string StepInfo = "Just follow what says";
    // Start is called before the first frame update
    void Start()
    {
        
        if (Ian == null)
        {
            // Try to find the GameObject by name if it's not assigned in the inspector
            Ian = GameObject.Find("Ian");

            if (Ian == null)
            {
                Debug.LogError("Ian GameObject not found. Make sure it's assigned or exists in the scene.");
                return;
            }
        }

        Ian.transform.position = targetPosition;

        Transform childTransform = Ian.transform.Find("BoxCollideTrigger");

        if (childTransform != null)
        {
            // Disable the child GameObject
            childTransform.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogError("Child GameObject with name  BoxCollideTrigger  not found.");
        }
        GameManager.instance.OnBuildingQuest = true;
        GameManager.instance.BackButton.gameObject.SetActive(false);
        GameManager.instance.HasInitialize = true;
        GameManager.instance.BuildingDesk.SetActive(true);
        GameManager.instance.HouseDoor.SetActive(true);
        GameManager.instance.packagescollected = 8;// need to have this in the rest of the quest step

        DialogueManager.GetInstance().EnterDialogueMode(GameManager.instance.MainStory);
        DialogueManager.GetInstance().TriggerSection("sixth");

        GameManager.instance.BackButton.onClick.AddListener(OnClickBackButton);
    }

    // Update is called once per frame
    void Update()
    {
        //GameManager.instance.CurrentQuestInfo = StepInfo;


    }
    public void OnClickBackButton()
    {
        if (GameManager.instance.OnBuildingQuest == true && GameManager.instance.DoneRename == true)
        {
        FinishQuestStep();
        ChangeState("Finish", "Finish");

        GameManager.instance.OnBuildingQuest = false;
        GameManager.instance.DoneRename = false;

        AchievementManager.instance.BuildfirstPCs = true;
        AchievementManager.instance.CheckAchievements();
        }
    }
    protected override void SetQuestStepState(string state)
    {
        // You can implement this if you need to load or set state for this step
    }

    public void OnApplicationQuit()
    {
        if (GameManager.instance.OnBuildingQuest == true && GameManager.instance.DoneRename == true)
        {
            OnClickBackButton();
        }


        else if (GameManager.instance.OnBuildingQuest == true)
        {

            if(GameManager.instance.pcsoDocumentIds.Count > 0)
            {
                GameManager.instance.PPC.ModifyComputer(0);
                GameManager.instance.PIC.YesDisAssemblePC();
            }

        }
    }
}
