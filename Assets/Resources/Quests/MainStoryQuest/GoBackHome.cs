using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoBackHome : QuestStep
{
    // Start is called before the first frame update
    private GameObject Player;
    public Vector3 targetPosition;
    void Start()
        
    {
        GameManager.instance.GoBackHomeQuest = true;
        DialogueManager.GetInstance().EnterDialogueMode(GameManager.instance.MainStory);
        DialogueManager.GetInstance().TriggerSection("Nineteen");

        if (GameManager.instance.HasInitialize == false)
        {
            GameManager.instance.playerTeleport.ToCPUWorld();
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

        GameManager.instance.HasInitialize = true;
        GameManager.instance.ArenaWall.gameObject.SetActive(false);
        GameManager.instance.CPUSpawn.SetActive(false);
        GameManager.instance.PlayerDeskRoom.SetActive(false);
        GameManager.instance.BuildingDesk.SetActive(true);
        GameManager.instance.HouseDoor.SetActive(true);
        GameManager.instance.packagescollected = 8;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.GoBackHomeQuest == true && GameManager.instance.OnCutScene9Finish == true)
        {
            FinishQuestStep();
            ChangeState("Finish", "Finish");

           
            GameManager.instance.playerTeleport.TheTeleporter();
            
 
            GameManager.instance.GoBackHomeQuest = false;
            GameManager.instance.OnCutScene9Finish = false;
        }
    }
    protected override void SetQuestStepState(string state)
    {


    }
}
