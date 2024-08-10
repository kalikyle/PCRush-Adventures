using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToDeskAgain : QuestStep
{
    // Start is called before the first frame update
    private GameObject Player;
    public Vector3 targetPosition;
    void Start()
    {
        GameManager.instance.OnGoToDeskQuestAgain = true;
        if (GameManager.instance.HasInitialize == false)
        {
            if (Player == null)
            {
                // Try to find the GameObject by name if it's not assigned in the inspector
                Player = GameObject.Find("PCRushCharacter");

                if (Player == null)
                {
                    Debug.LogError("PCRushCharacter GameObject not found. Make sure it's assigned or exists in the scene.");
                    return;
                }
            }

            Player.transform.position = targetPosition;
        }

        DialogueManager.GetInstance().EnterDialogueMode(GameManager.instance.MainStory);
        DialogueManager.GetInstance().TriggerSection("Ten");

        // need to have this in the rest of the quest step
        GameManager.instance.HasInitialize = true;
        GameManager.instance.PlayerDeskRoom.SetActive(true);
        GameManager.instance.BuildingDesk.SetActive(true);
        GameManager.instance.HouseDoor.SetActive(true);
        GameManager.instance.packagescollected = 8;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.OnGoToDeskQuestAgain == true && GameManager.instance.OntheDesk == true) {

            FinishQuestStep();
            ChangeState("Finish", "Finish");

            GameManager.instance.OnGoToDeskQuestAgain = false;
            GameManager.instance.OntheDesk = false;
        }

    }

    protected override void SetQuestStepState(string state)
    {


    }
}
