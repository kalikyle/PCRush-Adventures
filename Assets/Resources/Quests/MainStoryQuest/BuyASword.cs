using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyASword : QuestStep
{
    // Start is called before the first frame update
    private GameObject CPUExchanger;
    private GameObject ArmorFixer;
    private GameObject ArmorDealer;


    [Header("Target GameObject Name")]
    [SerializeField] private string targetGameObjectName;

    //public string StepInfo = "Go Outside your house, and find your friend Ian. His house have Orange roof located at the southeast of the Map";


    private GameObject targetGameObject;
    private bool hasReachedTarget = false;
    void Start()
    {
        ///////////////////////////////////////////
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
        ////////////////////////////////////////////
        if (ArmorFixer == null)
        {
            // Try to find the GameObject by name if it's not assigned in the inspector
            ArmorFixer = GameObject.Find("Armor Fixer");

            if (ArmorFixer == null)
            {
                Debug.LogError("Ian GameObject not found. Make sure it's assigned or exists in the scene.");
                return;
            }
        }

        Transform childTransformFixer = ArmorFixer.transform.Find("BoxCollideTrigger");

        if (childTransformFixer != null)
        {
            // Disable the child GameObject
            childTransformFixer.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogError("Child GameObject with name  BoxCollideTrigger  not found.");
        }
        ///////////////////////////////////////////////////
        ///

        if (ArmorDealer == null)
        {
            // Try to find the GameObject by name if it's not assigned in the inspector
            ArmorDealer = GameObject.Find("Armor Dealer");

            if (ArmorFixer == null)
            {
                Debug.LogError("Ian GameObject not found. Make sure it's assigned or exists in the scene.");
                return;
            }
        }

        Transform childTransformDealer = ArmorDealer.transform.Find("BoxCollideTrigger");

        if (childTransformDealer != null)
        {
            // Disable the child GameObject
            childTransformDealer.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogError("Child GameObject with name  BoxCollideTrigger  not found.");
        }



        if (GameManager.instance.HasInitialize == false)
        {
            GameManager.instance.playerTeleport.ToCPUWorld();
            DialogueManager.GetInstance().EnterDialogueMode(GameManager.instance.MainStory);
            DialogueManager.GetInstance().TriggerSection("SixteenIntro");

        }

        GameManager.instance.CPUSpawn.SetActive(false);
        GameManager.instance.HasInitialize = true;
        GameManager.instance.PlayerDeskRoom.SetActive(true);
        GameManager.instance.BuildingDesk.SetActive(true);
        GameManager.instance.HouseDoor.SetActive(true);
        GameManager.instance.packagescollected = 8;
        GameManager.instance.OnBuySwordQuest = true;
        GameManager.instance.PlayerMoney = 100;
        GameManager.instance.CpuTwirl.SetActive(false);
        GameManager.instance.SaveCharInfo(GameManager.instance.UserID, GameManager.instance.PlayerName);

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

    }

    // Update is called once per frame

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
    public bool hasclose = false;
    void Update()
    {
        if (!hasReachedTarget && IsPlayerNearTarget())
        {
            hasReachedTarget = true;
            Debug.Log("Reached " + targetGameObject.name + ".");
            DialogueManager.GetInstance().EnterDialogueMode(GameManager.instance.MainStory);
            DialogueManager.GetInstance().TriggerSection("Sixteen");
        }



        if(GameManager.instance.OnBuySwordQuest == true && GameManager.instance.OnBuyDone == true && hasclose == false)
        {
            hasclose = true;
            GameManager.instance.OnBuySwordQuest = false;
            GameManager.instance.OnBuyDone = false;

            StartCoroutine(HandleDialogueAndQuestCompletion());
        }


    }

    private IEnumerator HandleDialogueAndQuestCompletion()
    {
        // Start the dialogue section "ThirteenTwo"
        DialogueManager.GetInstance().EnterDialogueMode(GameManager.instance.MainStory);
        DialogueManager.GetInstance().TriggerSection("SixteenTwo");

        // Wait until the dialogue is no longer playing
        yield return new WaitUntil(() => !DialogueManager.GetInstance().dialogueIsPlaying);

        // Finish the quest step
        FinishQuestStep();

        // Change the state and update the quest status
        ChangeState("Finish", "Finish");
        GameManager.instance.OnQuest = false;
        GameManager.instance.ArenaWall.gameObject.SetActive(false);

        Transform childTransformFixer = ArmorFixer.transform.Find("BoxCollideTrigger");

        if (childTransformFixer != null)
        {
            // Disable the child GameObject
            childTransformFixer.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogError("Child GameObject with name  BoxCollideTrigger  not found.");
        }
        ///////////////////////////////////////////////////

        Transform childTransformDealer = ArmorDealer.transform.Find("BoxCollideTrigger");

        if (childTransformDealer != null)
        {
            // Disable the child GameObject
            childTransformDealer.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogError("Child GameObject with name  BoxCollideTrigger  not found.");
        }
    }
    protected override void SetQuestStepState(string state)
    {


    }
}
