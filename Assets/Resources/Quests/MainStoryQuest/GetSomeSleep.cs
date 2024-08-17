using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetSomeSleep : QuestStep
{
    // Start is called before the first frame update

    [Header("Target GameObject Name")]
    [SerializeField] private string targetGameObjectName;

    //public string StepInfo = "Go Outside your house, and find your friend Ian. His house have Orange roof located at the southeast of the Map";


    private GameObject targetGameObject;
    void Start()
    {

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


        GameManager.instance.OnSleepQuest = true;
        GameManager.instance.HasInitialize = true;
        GameManager.instance.ArenaWall.gameObject.SetActive(false);
        GameManager.instance.CPUSpawn.SetActive(true);
        GameManager.instance.ExitRoom.SetActive(false);
        GameManager.instance.PlayerDeskRoom.SetActive(false);
        GameManager.instance.BuildingDesk.SetActive(false);
        GameManager.instance.HouseDoor.SetActive(true);
        GameManager.instance.packagescollected = 8;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void SetQuestStepState(string state)
    {


    }
}
