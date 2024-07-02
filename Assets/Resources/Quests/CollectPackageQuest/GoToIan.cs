using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToIan : QuestStep
{
    [Header("Target GameObject Name")]
    [SerializeField] private string targetGameObjectName;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;


    private GameObject targetGameObject;
    private bool hasReachedTarget = false;

    public void Start()
    {
        // Find the target GameObject by name in the scene
        if (!string.IsNullOrEmpty(targetGameObjectName))
        {
            targetGameObject = GameObject.Find(targetGameObjectName);
            if (targetGameObject == null)
            {
                Debug.LogError("Target GameObject '" + targetGameObjectName + "' not found in the scene.");
                FinishQuestStep();
            }
            else
            {
                Debug.Log("Navigate to " + targetGameObject.name + ".");
                DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
                DialogueManager.GetInstance().TriggerSection("second");
            }
        }
        else
        {
            Debug.LogError("Target GameObject name is not specified in NavigateToGameObjectStep.");
            FinishQuestStep();
        }
    }

    public void Update()
    {
        // Check if the player has reached the target GameObject
        if (!hasReachedTarget && IsPlayerNearTarget())
        {
            hasReachedTarget = true;
            Debug.Log("Reached " + targetGameObject.name + ".");
            FinishQuestStep();
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
            return distance < 1.0f; // Adjust the distance threshold as needed
        }
        return false;
    }

    protected override void SetQuestStepState(string state)
    {
        // You can implement this if you need to load or set state for this step
    }
}
