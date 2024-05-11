using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest 
{
    public QuestInfoSO info;

    public QuestState state;

    private int currentQuestStepIndex;


    public Quest(QuestInfoSO questinfo)
    {
        this.info = questinfo;
        this.state = QuestState.REQUIREMENT_NOT_MET;
        this.currentQuestStepIndex = 0;
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
           questStep.InitializeQuestStep(info.id);
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
}
