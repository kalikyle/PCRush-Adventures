using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightTheCaseHorde : QuestStep
{
    // Start is called before the first frame update

    [Header("Target GameObject Name")]
    [SerializeField] private string targetGameObjectName;

    //public string StepInfo = "Go Outside your house, and find your friend Ian. His house have Orange roof located at the southeast of the Map";


    private GameObject targetGameObject;
    //private bool hasReachedTarget = false;

    void Start()
    {
        GameManager.instance.OnStartCaseFightQuest = true;

        if (GameManager.instance.HasInitialize == false)
        {
            DialogueManager.GetInstance().EnterDialogueMode(GameManager.instance.MainStory);
            DialogueManager.GetInstance().TriggerSection("TwentyNineIntro");

        }

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

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.OnStartCaseFightQuest == true  && GameManager.instance.CaseHordeFinished == true)
        {

            FinishQuestStep();
            ChangeState("Finish", "Finish");

            GameManager.instance.OnStartCaseFightQuest = false;
            GameManager.instance.CaseHordeFinished = false;

        }
    }
    protected override void SetQuestStepState(string state)
    {


    }
}
