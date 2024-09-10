using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Decoration.Model.DecorSO;

public class QuestLogUI : MonoBehaviour
{
    public static QuestLogUI instance;
    [Header("Components")]
    [SerializeField] private GameObject contentParent;

    [SerializeField] private QuestLogScrollingList scrollinglist;

    [SerializeField] private TextMeshProUGUI questDisplayNameText;

    [SerializeField] private TextMeshProUGUI questStatusText;

    [SerializeField] private TextMeshProUGUI goldRewardsText;

    [SerializeField] private TextMeshProUGUI experienceRewardsText;

    [SerializeField] private TextMeshProUGUI levelRequirementsText;

    [SerializeField] private TextMeshProUGUI questRequirementsText;


    [SerializeField] private TMP_Text QuestName;
    [SerializeField] private TMP_Text questStatus;
    [SerializeField] private TMP_Text questInfo;


    [SerializeField] private GameObject questStepPrefab; // Assign the QuestStepPrefab in the Inspector
    [SerializeField] private Transform scrollViewContent;

    [SerializeField] private GameObject questUIPopup; // Assign the QuestStepPrefab in the Inspector
    [SerializeField] private Transform questUIParent;
    private GameObject currentQuestStepUI = null;


    private Button firstselectedButton;

    public void Start()
    {


        OnStart();

    }
    public void Awake()
    {
        instance = this;
    }


    public void Update()
    {
        
    }
    public void OnDisables()
    {
        GameManager.instance.questEvents.onQuestStateChange -= QuestStateChange;
        GameManager.instance.questEvents.onQuestStateChange -= SetQuestLogInfo;
        GameManager.instance.questEvents.onQuestStateChange -= UpdateQuestStepUI;
    }

    public void OnStart()
    {
        GameManager.instance.questEvents.onQuestStateChange += QuestStateChange;
        GameManager.instance.questEvents.onQuestStateChange += SetQuestLogInfo;
        GameManager.instance.questEvents.onQuestStateChange += UpdateQuestStepUI;
    }

    //public void UpdateQuest(Quest quest)
    //{
    //   SetQuestLogInfo(quest);
    //}

    public void QuestStateChange(Quest quest)
    {
        QuestLogButton questLogButton = scrollinglist.CreateButtonIfNotExists(quest, () => {
            SetQuestLogInfo(quest);
            //UpdateQuestStepUI(quest);
        });

        if (firstselectedButton == null)
        {
            firstselectedButton = questLogButton.button;
            //firstselectedButton.Select();
            //Debug.LogWarning("dasdasdasdasds");
        }

        questLogButton.SetState(quest.state);

        //if (quest.state == QuestState.FINISHED)
        //{
        //    GameManager.instance.QuestUI.gameObject.SetActive(false);
        //}
      

    }

    public void ShowUI()
    {
        contentParent.gameObject.SetActive(true);

        if (firstselectedButton != null)
        {
           
            firstselectedButton.Select();
           
        }
    }

    public void HideUI()
    {
        contentParent.gameObject.SetActive(false);

       
    }





    public void SetQuestLogInfo(Quest quest)
    {
        List<string> list = new List<string>();
        list = quest.Steps;
        if (quest == null)
        {
            Debug.LogError("quest is null");
        }

        questDisplayNameText.text = quest.info.displayName;


        questStatusText.text = quest.GetFullStatusText();

        foreach (Transform child in scrollViewContent)
        {
            Destroy(child.gameObject);
        }

        // Instantiate a new prefab for each step in the list
        foreach (string step in list)
        {
            //Debug.LogError(step);
            GameObject stepObj = Instantiate(questStepPrefab, scrollViewContent);
            TMP_Text stepText = stepObj.GetComponentInChildren<TMP_Text>();

            if (stepText != null)
            {
                stepText.text = step;
            }
            else
            {
                Debug.LogError("TMP_Text component not found in the prefab's children.");
            }
        }




        //QuestName.text = quest.currentQuestStep();
        //questStatus.text = quest.currentStatus();
        //questInfo.text = quest.currentQuestStepInfo();



        
        levelRequirementsText.text = "Level " + quest.info.levelRequirement;
        questRequirementsText.text = "";

        foreach(QuestInfoSO prerequisisteQuestInfo in quest.info.questPrerequisites){
            questRequirementsText.text += prerequisisteQuestInfo.name + "\n";
        }


        goldRewardsText.text = quest.info.goldReward.ToString();
        experienceRewardsText.text = quest.info.experiencereward + " XP";
    }


    //public void UpdateUI(Quest quest)
    //{

    //    if(quest.state == QuestState.IN_PROGRESS)
    //    {
    //        GameManager.instance.QuestUI.gameObject.SetActive(true);
    //    }else if (quest.state == QuestState.FINISHED || quest.state == QuestState.CAN_START)
    //    {
    //        GameManager.instance.QuestUI.gameObject.SetActive(false);
    //    }

    //}


    //private void UpdateQuestStepUI(Quest quest)
    //{
    //    // Check if there's an active quest step
    //    if (quest.state == QuestState.IN_PROGRESS && quest.Currentstepexist())
    //    {
    //        // If the UI is already active, update its content
    //        if (currentQuestStepUI != null)
    //        {
    //            Debug.LogError("triggeres");
    //            UpdateQuestStepUIContent(quest);
    //        }
    //        else
    //        {
    //            // Instantiate the UI prefab and update its content
    //            currentQuestStepUI = Instantiate(questUIPopup, questUIParent);

    //            LeanTween.moveLocal(currentQuestStepUI, new Vector3(-347f, 106f, 0f), 3f)
    //            .setEase(LeanTweenType.easeOutExpo);

    //             UpdateQuestStepUIContent(quest);
    //        }
    //    }
    //    else if (quest.state == QuestState.FINISHED || !quest.Currentstepexist())
    //    {
    //        // If the quest is finished or there is no active step, hide or destroy the UI
    //        if (currentQuestStepUI != null)
    //        {
    //            LeanTween.moveLocal(currentQuestStepUI, new Vector3(-500f, 106f, 0f), 1f)
    //           .setDelay(1f)
    //           .setEase(LeanTweenType.easeOutExpo)
    //           .setOnComplete(() => Destroy(currentQuestStepUI));
    //            currentQuestStepUI = null;
    //        }
    //    }
    //}
    private int lastQuestStepIndex = -1;
    private void UpdateQuestStepUI(Quest quest)
    {
        // Check if there's an active quest step
        if (quest.state == QuestState.IN_PROGRESS)
        {
            if (quest.currentQuestStepIndex != lastQuestStepIndex)
            {
                // If the step index has changed, destroy the existing UI
                if (currentQuestStepUI != null)
                {
                    LeanTween.moveLocal(currentQuestStepUI, new Vector3(-500f, 106f, 0f), 1f)
                    .setEase(LeanTweenType.easeOutExpo);
                    Destroy(currentQuestStepUI);
                    currentQuestStepUI = null;
                }
                
                // Instantiate the UI prefab and update its content
                currentQuestStepUI = Instantiate(questUIPopup, questUIParent);

                LeanTween.moveLocal(currentQuestStepUI, new Vector3(-347f, 106f, 0f), 2f)
                    .setEase(LeanTweenType.easeOutExpo);

                UpdateQuestStepUIContent(quest);
            }
            else if (currentQuestStepUI != null)
            {
                // If the step index has not changed, just update the content
                UpdateQuestStepUIContent(quest);
            }

            // Store the current step index
            lastQuestStepIndex = quest.currentQuestStepIndex;
        }
        else if (quest.state == QuestState.FINISHED || quest.currentQuestStepIndex == -1)
        {
            // If the quest is finished or there's no valid step, hide or destroy the UI
            if (currentQuestStepUI != null)
            {
                LeanTween.moveLocal(currentQuestStepUI, new Vector3(-500f, 106f, 0f), 1f)
                    .setEase(LeanTweenType.easeOutExpo)
                    .setOnComplete(() => Destroy(currentQuestStepUI));

                currentQuestStepUI = null;
                lastQuestStepIndex = -1; // Reset the index to indicate no active step
            }
        }
    }


    private void UpdateQuestStepUIContent(Quest quest)
    {
        // Update the text fields in the UI prefab
        TMP_Text objectiveText = currentQuestStepUI.transform.Find("Name").GetComponent<TMP_Text>();
        TMP_Text progressText = currentQuestStepUI.transform.Find("status").GetComponent<TMP_Text>();

        objectiveText.text = quest.currentQuestStep(); // Update the quest step name
        progressText.text = quest.currentStatus();     // Update the progress (e.g., 8/8)
    }



}
