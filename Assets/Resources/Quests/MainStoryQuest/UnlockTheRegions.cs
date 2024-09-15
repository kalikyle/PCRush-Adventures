using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using static Unity.Collections.AllocatorManager;

public class UnlockTheRegions : QuestStep
{
    public int RegionsUnlocked = 1;
    private int RegionsToBeUnlocked = 8;
    private int previousRegionsToBeUnlocked = 1;


    [Header("Target GameObject Name")]
    [SerializeField] private string targetGameObjectName;
    private GameObject targetGameObject;

    void Start()
    {
        GameManager.instance.OnRegionQuest = true;

        DialogueManager.GetInstance().EnterDialogueMode(GameManager.instance.MainStory);
        DialogueManager.GetInstance().TriggerSection("TwentySeven");

        GameManager.instance.HasInitialize = true;
        GameManager.instance.CPUSpawn.SetActive(true);
        GameManager.instance.ExitRoom.SetActive(true);
        GameManager.instance.PlayerDeskRoom.SetActive(true);
        GameManager.instance.BuildingDesk.SetActive(true);
        GameManager.instance.HouseDoor.SetActive(true);
        GameManager.instance.packagescollected = 8;


        RegionsUnlockeds();

        RegionsUnlocked = GameManager.instance.regionsunlocked;
        checkDialog(RegionsUnlocked);


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


    public void checkDialog(int regionsunlocked)
    {
        if (regionsunlocked == 1)
        {

            DialogueManager.GetInstance().EnterDialogueMode(GameManager.instance.MainStory);
            DialogueManager.GetInstance().TriggerSection("TwentySevenRam");
        }


        if (regionsunlocked == 2)
        {

            DialogueManager.GetInstance().EnterDialogueMode(GameManager.instance.MainStory);
            DialogueManager.GetInstance().TriggerSection("TwentySevenCPUF");
        }

        if (regionsunlocked == 3)
        {

            DialogueManager.GetInstance().EnterDialogueMode(GameManager.instance.MainStory);
            DialogueManager.GetInstance().TriggerSection("TwentySevenGPU");
        }

        if (regionsunlocked == 4)
        {

            DialogueManager.GetInstance().EnterDialogueMode(GameManager.instance.MainStory);
            DialogueManager.GetInstance().TriggerSection("TwentySevenStorage");
        }

        if (regionsunlocked == 5)
        {

            DialogueManager.GetInstance().EnterDialogueMode(GameManager.instance.MainStory);
            DialogueManager.GetInstance().TriggerSection("TwentySevenPSU");
        }

        if (regionsunlocked == 6)
        {
            DialogueManager.GetInstance().EnterDialogueMode(GameManager.instance.MainStory);
            DialogueManager.GetInstance().TriggerSection("TwentySevenMB");
        }

        if (regionsunlocked == 7)
        {
            DialogueManager.GetInstance().EnterDialogueMode(GameManager.instance.MainStory);
            DialogueManager.GetInstance().TriggerSection("TwentySevenCase");
        }

        if (regionsunlocked == 8)
        {
            DialogueManager.GetInstance().EnterDialogueMode(GameManager.instance.MainStory);
            DialogueManager.GetInstance().TriggerSection("TwentySevenUnlocked");
        }
    }


    // Update is called once per frame
    void Update()
    {
        RegionsUnlocked = GameManager.instance.regionsunlocked;

        if (RegionsUnlocked != previousRegionsToBeUnlocked)
        {
            // If it has changed, call packageCollected method
            RegionsUnlockeds();
            // Update the previousPackageCollected to the new value
            previousRegionsToBeUnlocked = RegionsUnlocked;
        }
    }
    public void RegionsUnlockeds()
    {
        if (RegionsUnlocked >= RegionsToBeUnlocked)
        {
            FinishQuestStep();
            //Debug.LogError("dasdasdasdasdas");
            GameManager.instance.OnRegionQuest = false;

        }
        UpdateState();

    }
    private void UpdateState()
    {

        string status = RegionsUnlocked + " / " + RegionsToBeUnlocked;
        string state = RegionsUnlocked.ToString();
        ChangeState(state, status);
    }
    protected override void SetQuestStepState(string state)
    {

        //Debug.LogError(state);
        GameManager.instance.regionsunlocked = System.Int32.Parse(state);
        RegionsUnlocked = GameManager.instance.regionsunlocked;
        UpdateState();

        checkDialog(RegionsUnlocked);
    }
}
