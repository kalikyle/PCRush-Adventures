using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class GoToIan : QuestStep
{
    [Header("Target GameObject Name")]
    [SerializeField] private string targetGameObjectName;

    public string StepInfo = "Go Outside your house, and find your friend Ian. His house have Orange roof located at the southeast of the Map";


    private GameObject targetGameObject;
    private bool hasReachedTarget = false;

    public void Start()
    {
        GameManager.instance.packagescollected = 8;// need to have this in the rest of the quest step
        GameManager.instance.HouseDoor.SetActive(true);

        DialogueManager.GetInstance().EnterDialogueMode(GameManager.instance.MainStory);
        DialogueManager.GetInstance().TriggerSection("second");



        // Find the target GameObject by name in the scene
        if (!string.IsNullOrEmpty(targetGameObjectName))
        {
            targetGameObject = GameObject.Find(targetGameObjectName);
            if (targetGameObject == null)
            {
                Debug.LogError("Target GameObject '" + targetGameObjectName + "' not found in the scene.");
                FinishQuestStep();
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
            ChangeState("finish", "finish");
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
