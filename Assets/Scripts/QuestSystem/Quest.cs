using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class Quest 
{
    public QuestInfoSO info;

    public QuestState state;

    public int currentQuestStepIndex;
    private QuestStepState[] questStepStates;


    public List<string> Steps = new List<string>();

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
            questStepStates[stepIndex].status = questStepState.status;
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

    public string GetFullStatusText()
    {
        string fullStatus = "";

        if(state == QuestState.REQUIREMENT_NOT_MET)
        {
            Steps.Clear();
            fullStatus = "Requirements are not yet met to start this quest.";
            Steps.Add(fullStatus);
        }
        else if (state == QuestState.CAN_START)
        {
            Steps.Clear();
            fullStatus = "This quest Can be Started!";
            Steps.Add(fullStatus);
        }
        else
        {
            Steps.Clear();
            for (int i = 0; i < currentQuestStepIndex; i++)
            {
                
                fullStatus += "<s>" + info.questStepsPrefab[i].name + ": " + questStepStates[i].status + "</s>\n";
                Steps.Add($"<color=green>{info.questStepsPrefab[i].name}: {questStepStates[i].status}</color>");
            }

            if (Currentstepexist())
            {
                fullStatus += info.questStepsPrefab[currentQuestStepIndex].name + ": " + questStepStates[currentQuestStepIndex].status;
                Steps.Add(info.questStepsPrefab[currentQuestStepIndex].name + " " + questStepStates[currentQuestStepIndex].status);
            }

            if(state == QuestState.CAN_FINISH)
            {
                fullStatus += "The quest is ready to Finish!";
                Steps.Add("The quest is ready to Finish!");
            }
            else if (state == QuestState.FINISHED)
            {
                fullStatus += "This Quest has been Finished!";
                Steps.Add("This Quest has been Finished!");
            }
        }
        return fullStatus;
    }

    public string currentStatus()
    {
        string cstatus = "";
        if (Currentstepexist())
        {
            cstatus = questStepStates[currentQuestStepIndex].status;
          
        }
        else
        {
            if (state == QuestState.CAN_FINISH)
            {
                cstatus = "The quest is ready to Finish!";
            }
            else if (state == QuestState.FINISHED)
            {
                cstatus = "This Quest has been Finished!";
            }
        }
        return cstatus;
    }

    public string currentQuestStep()
    {
        string cstatus = "";
        if (Currentstepexist())
        {
            cstatus = info.questStepsPrefab[currentQuestStepIndex].name;
        }
        
        return cstatus;
    }

    public string currentQuestStepInfo()
    {
        string cstatus = "";
        if (Currentstepexist())
        {
            GameObject questStepPrefab = info.questStepsPrefab[currentQuestStepIndex];
            CollectPackageStep collectPackageStep = questStepPrefab.GetComponent<CollectPackageStep>();
            GoToIan goToIan = questStepPrefab.GetComponent<GoToIan>();
            TalkToIan TalkToIan = questStepPrefab.GetComponent<TalkToIan>();


            if (collectPackageStep != null)
            {
                cstatus = collectPackageStep.StepInfo;
            }

            if(goToIan != null)
            {
                cstatus = goToIan.StepInfo;
            }

            if (TalkToIan != null)
            {
                cstatus = TalkToIan.StepInfo;
            }

            if(cstatus == "")
            {
                cstatus = "No Info on this Objective";
            }

        }

        return cstatus;
    }

    


    }


  
