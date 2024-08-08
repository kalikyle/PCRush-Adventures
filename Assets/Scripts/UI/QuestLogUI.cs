using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestLogUI : MonoBehaviour
{
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
    [SerializeField] private GameObject QuestPanel;


    private Button firstselectedButton;

    public void Start()
    {
        GameManager.instance.questEvents.onQuestStateChange += QuestStateChange;
        GameManager.instance.questEvents.onQuestStateChange += UpdateQuest;
        GameManager.instance.questEvents.onQuestStateChange += UpdateUI;

        

    }

    public void Update()
    {
        
    }
    private void OnDisable()
    {
       GameManager.instance.questEvents.onQuestStateChange -= QuestStateChange;
    }

    public void UpdateQuest(Quest quest)
    {
        SetQuestLogInfo(quest);
    }

    private void QuestStateChange(Quest quest)
    {
        QuestLogButton questLogButton = scrollinglist.CreateButtonIfNotExists(quest, () => { 
             SetQuestLogInfo(quest);
        
        });

        if (firstselectedButton == null)
        {
            firstselectedButton = questLogButton.button;
            //firstselectedButton.Select();
            //Debug.LogWarning("dasdasdasdasds");
        }

        questLogButton.SetState(quest.state);
      

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





    private void SetQuestLogInfo(Quest quest)
    {
        
        questDisplayNameText.text = quest.info.displayName;
        

      
        questStatusText.text = quest.GetFullStatusText();



        QuestName.text = quest.currentQuestStep();
        questStatus.text = quest.currentStatus();

        questInfo.text = quest.currentQuestStepInfo();
        
        levelRequirementsText.text = "Level " + quest.info.levelRequirement;
        questRequirementsText.text = "";

        foreach(QuestInfoSO prerequisisteQuestInfo in quest.info.questPrerequisites){
            questRequirementsText.text += prerequisisteQuestInfo.name + "\n";
        }


        goldRewardsText.text = quest.info.goldReward + " Gold";
        experienceRewardsText.text = quest.info.experiencereward + " XP";
    }


    public void UpdateUI(Quest quest)
    {

        if(quest.state == QuestState.IN_PROGRESS)
        {
            QuestPanel.gameObject.SetActive(true);
        }else if (quest.state == QuestState.FINISHED || quest.state == QuestState.CAN_START)
        {
            QuestPanel.gameObject.SetActive(false);
        }
        
    }

}
