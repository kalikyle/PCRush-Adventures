using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest 
{
    public QuestInfoSO info;

    public QuestState state;

    private int currentQuestStepIndex;
    private QuestStepState[] questStepStates;


    public Quest(QuestInfoSO questinfo)
    {
        this.info = questinfo;
        this.state = QuestState.REQUIREMENT_NOT_MET;
        this.currentQuestStepIndex = 0;
        this.questStepStates = new QuestStepState[info.questStepsPrefab.Length];

        for(int i = 0; i < questStepStates.Length; i++)
        {
            questStepStates[i] = new QuestStepState();
        }
    }

    public Quest(QuestInfoSO questInfo, QuestState questState, int currentQuestStepIndex, QuestStepState[] questStepStates)
    {
        this.info = questInfo;
        this.state = questState;
        this.currentQuestStepIndex = currentQuestStepIndex;
        this.questStepStates = questStepStates;

        if(this.questStepStates.Length != this.info.questStepsPrefab.Length)
        {
            Debug.LogWarning("Quest step prefabs and quest step states are of different lengths. this indicates something is changed, need to reset your data");
        }
    }

    public void MovetoNextStep()
    {
        currentQuestStepIndex++;
    }



    public bool Currentstepexist()
    {
        return (currentQuestStepIndex < info.questStepsPrefab.Length);
        
    }

    public void InstantiateCurrentQuestStep(Transform parentTransform)
    {
        GameObject questStepPrefab = GetcurrentQuestPrefab();
        if (questStepPrefab != null)
        {
           QuestStep questStep = Object.Instantiate<GameObject>(questStepPrefab, parentTransform).GetComponent<QuestStep>();
           questStep.InitializeQuestStep(info.id, currentQuestStepIndex, questStepStates[currentQuestStepIndex].state);
        }
    }


    private GameObject GetcurrentQuestPrefab()
    {
        GameObject questStepPrefab = null;

        if (Currentstepexist())
        {
            questStepPrefab = info.questStepsPrefab[currentQuestStepIndex];
        }
        else
        {
            Debug.LogWarning("No Quest Steps Available");
        }
        return questStepPrefab;
    }


    public void StoreQuestStepState(QuestStepState questStepState, int stepIndex)
    {
        if(stepIndex < questStepStates.Length)
        {
            questStepStates[stepIndex].state = questStepState.state;
        }
        else
        {
            Debug.LogWarning("Error access quest step data, step index was out of range");
        }
    }
    
    public QuestData GetQuestData()
    {
        return new QuestData(state, currentQuestStepIndex, questStepStates);
    }

    
}
