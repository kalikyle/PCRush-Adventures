using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindandStartHorde : QuestStep
{
    // Start is called before the first frame update
    private GameObject CPUExchanger;
    void Start()
    {
        GameManager.instance.OnStartFightQuest = true;

        if (CPUExchanger == null)
        {
            // Try to find the GameObject by name if it's not assigned in the inspector
            CPUExchanger = GameObject.Find("The CPU Exchanger");

            if (CPUExchanger == null)
            {
                Debug.LogError("Ian GameObject not found. Make sure it's assigned or exists in the scene.");
                return;
            }
        }

        Transform childTransform = CPUExchanger.transform.Find("BoxCollideTrigger");

        if (childTransform != null)
        {
            // Disable the child GameObject
            childTransform.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogError("Child GameObject with name  BoxCollideTrigger  not found.");
        }

        if (GameManager.instance.HasInitialize == false)
        {
            GameManager.instance.playerTeleport.ToCPUWorld();
            DialogueManager.GetInstance().EnterDialogueMode(GameManager.instance.MainStory);
            DialogueManager.GetInstance().TriggerSection("SeventeenIntro");

        }


        GameManager.instance.ArenaWall.gameObject.SetActive(false);
        GameManager.instance.CPUSpawn.SetActive(false);
        GameManager.instance.HasInitialize = true;
        GameManager.instance.PlayerDeskRoom.SetActive(true);
        GameManager.instance.BuildingDesk.SetActive(true);
        GameManager.instance.HouseDoor.SetActive(true);
        GameManager.instance.packagescollected = 8;
        GameManager.instance.CpuTwirl.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.OnStartFightQuest == true && GameManager.instance.OnTheArea == true && GameManager.instance.HordeFinished == true) { 
        
        
            FinishQuestStep();
            ChangeState("Finish", "Finish");

            GameManager.instance.OnStartFightQuest = false;
            GameManager.instance.OnTheArea = false;
            GameManager.instance.HordeFinished = false;

            Transform childTransform = CPUExchanger.transform.Find("BoxCollideTrigger");

        }
    }

    protected override void SetQuestStepState(string state)
    {


    }
}
