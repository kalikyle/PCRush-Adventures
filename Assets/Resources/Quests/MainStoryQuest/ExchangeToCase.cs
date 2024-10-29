using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExchangeToCase : QuestStep
{
    // Start is called before the first frame update

    [Header("Target GameObject Name")]
    [SerializeField] private string targetGameObjectName;
    private GameObject targetGameObject;
    private bool hasReachedTarget = false;

    public string StepInfo = "Go back to Case Exchanger to Exchange the Collected Materials";

    void Start()
    {
        
        GameManager.instance.ExchangeToCaseQuest = true;
        if (GameManager.instance.HasInitialize == false)
        {
            DialogueManager.GetInstance().EnterDialogueMode(GameManager.instance.MainStory);
            DialogueManager.GetInstance().TriggerSection("ThirtyIntro");

        }
        else
        {
            DialogueManager.GetInstance().EnterDialogueMode(GameManager.instance.MainStory);
            DialogueManager.GetInstance().TriggerSection("Thirty");
        }

        GameManager.instance.CaseExchangeXButton.gameObject.SetActive(false);
        GameManager.instance.CaseExchangeXButton.onClick.AddListener(OnXClick);
        GameManager.instance.HasInitialize = true;
        GameManager.instance.CPUSpawn.SetActive(true);
        GameManager.instance.ExitRoom.SetActive(true);
        GameManager.instance.PlayerDeskRoom.SetActive(true);
        GameManager.instance.BuildingDesk.SetActive(true);
        GameManager.instance.HouseDoor.SetActive(true);
        GameManager.instance.packagescollected = 8;

        if (!string.IsNullOrEmpty(targetGameObjectName))
        {
            targetGameObject = GameObject.Find(targetGameObjectName);
            if (targetGameObject == null)
            {
                Debug.LogError("Target GameObject '" + targetGameObjectName + "' not found in the scene.");

            }
        }
        else
        {
            Debug.LogError("Target GameObject name is not specified in NavigateToGameObjectStep.");

        }

        Transform childTransform = targetGameObject.transform.Find("BoxCollideTrigger");

        if (childTransform != null)
        {
            // Disable the child GameObject
            childTransform.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogError("Child GameObject with name  BoxCollideTrigger  not found.");
        }
    }

    private bool IsPlayerNearTarget()
    {
        // Example logic: Check distance between player (assuming player is tagged as "Player")
        // and the target GameObject
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null && targetGameObject != null)
        {
            float distance = Vector3.Distance(player.transform.position, targetGameObject.transform.position);
            return distance < 1.3f; // Adjust the distance threshold as needed
        }
        return false;
    }

    public void OnXClick()
    {
        if (GameManager.instance.ExchangeToCaseQuest == true)
        {

            FinishQuestStep();
            ChangeState("Finish", "Finish");

            GameManager.instance.ExchangeToCaseQuest = false;
            GameManager.instance.CaseExchangeXButton.gameObject.SetActive(true);

            if (!string.IsNullOrEmpty(targetGameObjectName))
            {
                targetGameObject = GameObject.Find(targetGameObjectName);
                if (targetGameObject == null)
                {
                    Debug.LogError("Target GameObject '" + targetGameObjectName + "' not found in the scene.");

                }
            }
            else
            {
                Debug.LogError("Target GameObject name is not specified in NavigateToGameObjectStep.");

            }

            Transform childTransform = targetGameObject.transform.Find("BoxCollideTrigger");

            if (childTransform != null)
            {
                // Disable the child GameObject
                childTransform.gameObject.SetActive(true);
            }
            else
            {
                Debug.LogError("Child GameObject with name  BoxCollideTrigger  not found.");
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!hasReachedTarget && IsPlayerNearTarget())
        {
            hasReachedTarget = true;
            Debug.Log("Reached " + targetGameObject.name + ".");
            DialogueManager.GetInstance().EnterDialogueMode(GameManager.instance.MainStory);
            DialogueManager.GetInstance().TriggerSection("ThirtyTwo");
        }
    }

    protected override void SetQuestStepState(string state)
    {


    }
}
