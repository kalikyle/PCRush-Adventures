using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TalkToIan : QuestStep
{
    // Start is called before the first frame update
    private GameObject Player;
    public Vector3 targetPosition;
    public string StepInfo = "Click The Talk Button";

   
    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;
    private void Awake()
    {
       
    }

    // Reference to the "Talk" button
    void Start()
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

        GameManager.instance.HouseDoor.SetActive(true);
        GameManager.instance.packagescollected = 8;// need to have this in the rest of the quest step

        GameManager.instance.OnQuest = true;
        GameManager.instance.TalkBTN.onClick.AddListener(OnTalkButtonClick);

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
        DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
        DialogueManager.GetInstance().TriggerSection("third");

    }

    public void Update()
    {
        if (GameManager.instance.CutScene2Open)
        {
            FinishQuestStep();

            Debug.Log("Talk button clicked. Finishing quest step.");
            ChangeState("finish", "finish");
            GameManager.instance.CutScene2Open = false;
            GameManager.instance.OnQuest = false;
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
