using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;


[RequireComponent(typeof(CircleCollider2D))]
public class QuestPoint : MonoBehaviour
{
    private bool playerIsNear = false;


    [Header("Quest")]
    [SerializeField] private QuestInfoSO questinfoForPoint;

    private string questId;

    private QuestState currentQuestState;


    [Header("Config")]
    [SerializeField] private bool StartPoint = true;
    [SerializeField] private bool finishPoint = true;

    private void Awake()
    {
        questId = questinfoForPoint.id;
       
    }

    private void Start()
    {
        GameManager.instance.questEvents.onQuestStateChange += QuestStateChange;

       
    }

    private void OnEnable()
    {
       
    }

    private void OnDisable()
    {
        GameManager.instance.questEvents.onQuestStateChange -= QuestStateChange;
    }

    private void startQuest()
    {
        //if (!playerIsNear)
        //{
        //    return;
        //}

        if (currentQuestState.Equals(QuestState.CAN_START) && StartPoint)
        {
            GameManager.instance.questEvents.StartQuest(questId);
        }
        else if (currentQuestState.Equals(QuestState.CAN_FINISH) && finishPoint)
        {
            GameManager.instance.questEvents.FinishQuest(questId);
        }


        //GameManager.instance.questEvents.StartQuest(questId);
        //GameManager.instance.questEvents.AdvanceQuest(questId);
        //GameManager.instance.questEvents.FinishQuest(questId);
    }

    public async void Update()
    {
        await Task.Delay(2000);

        startQuest();
    }

    private void QuestStateChange(Quest quest)
    {
        if(quest.info.id.Equals(questId))
        {
            currentQuestState = quest.state;
           // Debug.Log("Quest with id:" + questId + "updated to state: " + currentQuestState);
        }
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.CompareTag("Player"))
        {
            playerIsNear = true;
        }
    }

    private void OnTriggerExit2D(Collider2D otherCollider)
    {
        if (otherCollider.CompareTag("Player"))
        {
            playerIsNear = false;
        }
    }
}
