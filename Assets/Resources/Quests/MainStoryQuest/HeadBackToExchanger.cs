using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBackToExchanger : QuestStep
{
    [Header("Target GameObject Name")]
    [SerializeField] private string targetGameObjectName;

    //public string StepInfo = "Go Outside your house, and find your friend Ian. His house have Orange roof located at the southeast of the Map";


    private GameObject targetGameObject;
    private bool hasReachedTarget = false;
    void Start()
    {

        if (GameManager.instance.HasInitialize == false)
        {
            GameManager.instance.playerTeleport.ToCPUWorld();

        }

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


        DialogueManager.GetInstance().EnterDialogueMode(GameManager.instance.MainStory);
        DialogueManager.GetInstance().TriggerSection("Eighteen");


        GameManager.instance.HasInitialize = true;
        GameManager.instance.ArenaWall.gameObject.SetActive(false);
        GameManager.instance.CPUSpawn.SetActive(false);
        GameManager.instance.PlayerDeskRoom.SetActive(true);
        GameManager.instance.BuildingDesk.SetActive(true);
        GameManager.instance.HouseDoor.SetActive(true);
        GameManager.instance.packagescollected = 8;
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

    // Update is called once per frame
    void Update()
    {
        if (!hasReachedTarget && IsPlayerNearTarget())
        {
            hasReachedTarget = true;
            Debug.Log("Reached " + targetGameObject.name + ".");
           // StartCoroutine(HandleDialogueAndQuestCompletion());
        }
    }

    //private IEnumerator HandleDialogueAndQuestCompletion()
    //{
    //    // Start the dialogue section "ThirteenTwo"
    //    //DialogueManager.GetInstance().EnterDialogueMode(GameManager.instance.MainStory);
    //    //DialogueManager.GetInstance().TriggerSection("Fourteen");

    //    // Wait until the dialogue is no longer playing
    //    yield return new WaitUntil(() => !DialogueManager.GetInstance().dialogueIsPlaying);

    //    // Finish the quest step
    //   // FinishQuestStep();

    //    // Change the state and update the quest status
    //    ChangeState("Finish", "Finish");



    //}
    protected override void SetQuestStepState(string state)
    {


    }
}
