using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoBackToLostAdventurer : QuestStep
{
    // Start is called before the first frame update
    private GameObject CPUExchanger;
    void Start()
    {
        DialogueManager.GetInstance().EnterDialogueMode(GameManager.instance.MainStory);
        DialogueManager.GetInstance().TriggerSection("Fifteen");

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

        }

        GameManager.instance.CPUSpawn.SetActive(false);
        GameManager.instance.HasInitialize = true;
        GameManager.instance.PlayerDeskRoom.SetActive(true);
        GameManager.instance.BuildingDesk.SetActive(true);
        GameManager.instance.HouseDoor.SetActive(true);
        GameManager.instance.packagescollected = 8;
        GameManager.instance.PlayerMoney = 0;
        GameManager.instance.CpuTwirl.SetActive(false);
        GameManager.instance.TheLostAdventurer.SetActive(true);
        GameManager.instance.ArenaWall.gameObject.SetActive(true);
        GameManager.instance.SaveCharInfo(GameManager.instance.UserID, GameManager.instance.PlayerName);
        DialogueManager.GetInstance().talktoBTN.onClick.AddListener(TalkToLostAdventurer);

    }

    public void TalkToLostAdventurer()
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
        DialogueManager.GetInstance().TriggerSection("FifteenTwo");

        // Wait until the dialogue is no longer playing
        yield return new WaitUntil(() => !DialogueManager.GetInstance().dialogueIsPlaying);

        // Finish the quest step
        FinishQuestStep();

        // Change the state and update the quest status
        ChangeState("Finish", "Finish");
        GameManager.instance.PlayerMoney += 100;
        GameManager.instance.SaveCharInfo(GameManager.instance.UserID, GameManager.instance.PlayerName);
        GameManager.instance.OnQuest = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.CurrentNPC == "Lost Adventurer")
        {
            GameManager.instance.OnQuest = true;
        }
        else
        {
            GameManager.instance.OnQuest = false;
        }
    }

    protected override void SetQuestStepState(string state)
    {


    }
}
