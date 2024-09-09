using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TalkToIan : QuestStep
{
    // Start is called before the first frame update
    private GameObject Ian;
    private GameObject Player;
    public Vector3 targetPosition;
    public string StepInfo = "Click The Talk Button";
    private void Awake()
    {
       
    }

    // Reference to the "Talk" button
    void Start()
    {
        if (Ian == null)
        {
            // Try to find the GameObject by name if it's not assigned in the inspector
            Ian = GameObject.Find("Ian");

            if (Ian == null)
            {
                Debug.LogError("Ian GameObject not found. Make sure it's assigned or exists in the scene.");
                return;
            }
        }

        if (GameManager.instance.HasInitialize == false)
        {
            if (Player == null)
            {
                // Try to find the GameObject by name if it's not assigned in the inspector
                Player = GameObject.Find("PCRushCharacter");

                if (Player == null)
                {
                    Debug.LogError("PCRushCharacter GameObject not found. Make sure it's assigned or exists in the scene.");
                    return;
                }
            }

            Player.transform.position = targetPosition;
        }

        GameManager.instance.HasInitialize = true;
        GameManager.instance.HouseDoor.SetActive(true);
        GameManager.instance.packagescollected = 8;// need to have this in the rest of the quest step
        DialogueManager.GetInstance().talktoBTN.onClick.AddListener(OnTalkButtonClick);



        //await Task.Delay(1000);
        //GameObject button = GameObject.Find("TalkBTN");

        //talkButton = button.GetComponent<Button>();

        //if (talkButton != null)
        //{
        //    talkButton.onClick.AddListener(OnTalkButtonClick);
        //}
        //else
        //{
        //    Debug.LogError("Talk button is not assigned.");
        //}




    }

    public void OnTalkButtonClick()
    {
        if (GameManager.instance.OnQuest == true)
        {
            DialogueManager.GetInstance().EnterDialogueMode(GameManager.instance.MainStory);
            DialogueManager.GetInstance().TriggerSection("third");
        }
    }

    public void Update()
    {
        if (GameManager.instance.CurrentNPC == "Ian")
        {
            GameManager.instance.OnQuest = true;
        }
        else
        {
            GameManager.instance.OnQuest = false;
        }



        if (GameManager.instance.CutScene2Open)
        {
            FinishQuestStep();
            
            Debug.Log("Talk button clicked. Finishing quest step.");
            ChangeState("Finish", "Finish");
            GameManager.instance.CutScene2Open = false;
            GameManager.instance.OnQuest = false;
            GameManager.instance.LTA.CloseTalkBTN();

            Transform childTransform = Ian.transform.Find("BoxCollideTrigger");

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
    }
    //public void OnApplicationQuit()
    //{
    //    FinishQuestStep();
    //}

    // Update is called once per frame

    protected override void SetQuestStepState(string state)
    {
        
        
    }
}
