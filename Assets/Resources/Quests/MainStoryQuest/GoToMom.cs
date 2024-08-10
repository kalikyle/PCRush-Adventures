using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoToMom : QuestStep
{
    // Start is called before the first frame update
    private GameObject Ian;
    public Vector3 targetPosition;
    void Start()
    {
        GameManager.instance.GoDownStairsQuest = true;
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

        GameManager.instance.GoDownStairsQuest = true;
        DialogueManager.GetInstance().EnterDialogueMode(GameManager.instance.MainStory);
        DialogueManager.GetInstance().TriggerSection("Nine");
        
        GameManager.instance.PlayerDeskUI.SetActive(false);
        GameManager.instance.UIPanel.SetActive(false);
        GameManager.instance.UIExplore.SetActive(true);

        GameManager.instance.HasInitialize = true;
        GameManager.instance.PlayerDeskRoom.SetActive(false);
        GameManager.instance.BuildingDesk.SetActive(true);
        GameManager.instance.HouseDoor.SetActive(true);
        GameManager.instance.packagescollected = 8;// need to have this in the rest of the quest step
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.GoDownStairsQuest == true && GameManager.instance.DoneDownStairsQuets == true)
        {
            FinishQuestStep();
            ChangeState("Finish", "Finish");

            GameManager.instance.GoDownStairsQuest = false;
            GameManager.instance.DoneDownStairsQuets = false;
        }
    }

    protected override void SetQuestStepState(string state)
    {


    }
}
