using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    private Dictionary<string, Quest> questMap;
    //private int PlayerLevel = 0;

    private void Awake()
    {
        questMap = CreateQuestMap();

        //Quest quest = GetQuestId("PackageQuest");
        //Debug.Log(quest.info.displayName);
        //Debug.Log(quest.info.levelRequirement);
        //Debug.Log(quest.state);
        //Debug.Log(quest.Currentstepexist());
    }
  
    private void OnEnable()
    {
       
    }

    public void GetPlayerLevel()
    {
        //PlayerLevel = GameManager.instance.PlayerLevel;
    }
    public void Update()
    {
        GetPlayerLevel();


        foreach (Quest quest in questMap.Values)
        {
            if(quest.state == QuestState.REQUIREMENT_NOT_MET && CheckRequirements(quest))
            {
                ChangeQuestState(quest.info.id, QuestState.CAN_START);
            }
        }
    }

    private bool CheckRequirements(Quest quest)
    {
        bool meetrequirment = true;

        if(GameManager.instance.PlayerLevel < quest.info.levelRequirement)
        {
            meetrequirment = false;
        }

        foreach(QuestInfoSO prerequisitequestinfo in quest.info.questPrerequisites)
        {
            if(GetQuestId(prerequisitequestinfo.id).state != QuestState.FINISHED)
            {
                meetrequirment = false;
            }
        }
        return meetrequirment;
    }

    private void OnDisable()
    {
        GameManager.instance.questEvents.onStartQuest -= StartQuest;
        GameManager.instance.questEvents.onAdvanceQuest -= AdvanceQuest;
        GameManager.instance.questEvents.onFinishQuest -= FinishQuest;
    }

    private void Start()
    {
        GameManager.instance.questEvents.onStartQuest += StartQuest;
        GameManager.instance.questEvents.onAdvanceQuest += AdvanceQuest;
        GameManager.instance.questEvents.onFinishQuest += FinishQuest;
        foreach (Quest quest in questMap.Values)
        {
            GameManager.instance.questEvents.QuestStateChange(quest);
        }
    }

    private void ChangeQuestState(string id, QuestState state)
    {
        Quest quest = GetQuestId(id);
        quest.state = state;
        GameManager.instance.questEvents.QuestStateChange(quest);
    }

    private void StartQuest(string id)
    {
        //Debug.Log("Start Quest" + id); 

        //you can add ui here
        Quest quest = GetQuestId(id);
        quest.InstantiateCurrentQuestStep(this.transform);
        ChangeQuestState(quest.info.id, QuestState.IN_PROGRESS);
    }

    private void AdvanceQuest(string id)
    {
        //Debug.Log("Advance Quest" + id);

        Quest quest = GetQuestId(id);
        quest.MovetoNextStep();

        if (quest.Currentstepexist())
        {
            quest.InstantiateCurrentQuestStep(this.transform);

        }
        else
        {
            ChangeQuestState(quest.info.id, QuestState.CAN_FINISH);
        }
    }

    private void FinishQuest(string id)
    {
        //Debug.Log("Finish Quest" + id);
        Quest quest = GetQuestId(id);

        //ClaimRewards();
        ChangeQuestState(quest.info.id, QuestState.FINISHED);
       
    }

    private Dictionary<string, Quest> CreateQuestMap()
    {
        QuestInfoSO[] allquest = Resources.LoadAll<QuestInfoSO>("Quests");

        Dictionary<string, Quest> idToQuestMap = new Dictionary<string, Quest>();

        foreach(QuestInfoSO questInfo in allquest)
        {
            if (idToQuestMap.ContainsKey(questInfo.id))
            {
                Debug.LogWarning("Duplicated ID!");
            }
            idToQuestMap.Add(questInfo.id, new Quest(questInfo));
        }
        return idToQuestMap;
    }

    private Quest GetQuestId(string id)
    {
        Quest quest = questMap[id];
        if(quest == null)
        {
            Debug.LogError("ID not Found");
        }
        return quest;
    }
}
