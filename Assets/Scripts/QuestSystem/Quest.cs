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
            TalkToIan talkToIan = questStepPrefab.GetComponent<TalkToIan>();
            OpenBuildingDesk openBuildingDesk = questStepPrefab.GetComponent<OpenBuildingDesk>();
            BuilldFirstPC buildFirstPC = questStepPrefab.GetComponent<BuilldFirstPC>();
            GoToYouDesk goToYouDesk = questStepPrefab.GetComponent<GoToYouDesk>();
            GoToMom goToMom = questStepPrefab.GetComponent<GoToMom>();
            GoToDeskAgain goToDeskAgain = questStepPrefab.GetComponent<GoToDeskAgain>();
            TurnOnComputer turnOnComputer = questStepPrefab.GetComponent<TurnOnComputer>();
            ExploreDesktop exploreDesktop = questStepPrefab.GetComponent<ExploreDesktop>();
            TalkToStranger talkToStranger = questStepPrefab.GetComponent<TalkToStranger>();
            FindandMeetExchanger findandMeetExchanger = questStepPrefab.GetComponent<FindandMeetExchanger>();
            GoBackToLostAdventurer goBackToLostAdventurer = questStepPrefab.GetComponent<GoBackToLostAdventurer>();
            BuyASword buyASword = questStepPrefab.GetComponent<BuyASword>();
            FindandStartHorde findandStartHorde = questStepPrefab.GetComponent<FindandStartHorde>();
            HeadBackToExchanger headBackToExchanger = questStepPrefab.GetComponent<HeadBackToExchanger>();
            GoBackHome goBackHome = questStepPrefab.GetComponent<GoBackHome>();
            GetSomeSleep getSomeSleep = questStepPrefab.GetComponent<GetSomeSleep>();
            CollectTheCPU collectTheCPU = questStepPrefab.GetComponent<CollectTheCPU>();
            ModifyComputer modifyComputer = questStepPrefab.GetComponent<ModifyComputer>();
            MeetExchangerAgain meetExchangerAgain = questStepPrefab.GetComponent<MeetExchangerAgain>();
            UnlockTheRegions unlockTheRegions = questStepPrefab.GetComponent<UnlockTheRegions>();
            MeetTheCaseExchanger meetTheCaseExchanger = questStepPrefab.GetComponent<MeetTheCaseExchanger>();
            FightTheCaseHorde fightTheCaseHorde = questStepPrefab.GetComponent<FightTheCaseHorde>();
            CollectTheCase collectTheCase = questStepPrefab.GetComponent<CollectTheCase>();
            ExchangeToCase exchangeToCase = questStepPrefab.GetComponent<ExchangeToCase>();
            ModifyYourPC modifyYourPC = questStepPrefab.GetComponent<ModifyYourPC>();
            OpenYourDesktop openYourDesktop = questStepPrefab.GetComponent<OpenYourDesktop>();

            if (collectPackageStep != null)
            {
                cstatus = collectPackageStep.StepInfo;
            }
            if (goToIan != null)
            {
                cstatus = goToIan.StepInfo;
            }
            if (talkToIan != null)
            {
                cstatus = talkToIan.StepInfo;
            }
            if (openBuildingDesk != null)
            {
                cstatus = openBuildingDesk.StepInfo;
            }
            if (buildFirstPC != null)
            {
                cstatus = buildFirstPC.StepInfo;
            }
            if (goToYouDesk != null)
            {
                cstatus = goToYouDesk.StepInfo;
            }
            if (goToMom != null)
            {
                cstatus = goToMom.StepInfo;
            }
            if (goToDeskAgain != null)
            {
                cstatus = goToDeskAgain.StepInfo;
            }
            if (turnOnComputer != null)
            {
                cstatus = turnOnComputer.StepInfo;
            }
            if (exploreDesktop != null)
            {
                cstatus = exploreDesktop.StepInfo;
            }
            if (talkToStranger != null)
            {
                cstatus = talkToStranger.StepInfo;
            }
            if (findandMeetExchanger != null)
            {
                cstatus = findandMeetExchanger.StepInfo;
            }
            if (goBackToLostAdventurer != null)
            {
                cstatus = goBackToLostAdventurer.StepInfo;
            }
            if (buyASword != null)
            {
                cstatus = buyASword.StepInfo;
            }
            if (findandStartHorde != null)
            {
                cstatus = findandStartHorde.StepInfo;
            }
            if (headBackToExchanger != null)
            {
                cstatus = headBackToExchanger.StepInfo;
            }
            if (goBackHome != null)
            {
                cstatus = goBackHome.StepInfo;
            }
            if (getSomeSleep != null)
            {
                cstatus = getSomeSleep.StepInfo;
            }
            if (collectTheCPU != null)
            {
                cstatus = collectTheCPU.StepInfo;
            }
            if (modifyComputer != null)
            {
                cstatus = modifyComputer.StepInfo;
            }
            if (meetExchangerAgain != null)
            {
                cstatus = meetExchangerAgain.StepInfo;
            }
            if (unlockTheRegions != null)
            {
                cstatus = unlockTheRegions.StepInfo;
            }
            if (meetTheCaseExchanger != null)
            {
                cstatus = meetTheCaseExchanger.StepInfo;
            }
            if (fightTheCaseHorde != null)
            {
                cstatus = fightTheCaseHorde.StepInfo;
            }
            if (collectTheCase != null)
            {
                cstatus = collectTheCase.StepInfo;
            }
            if (exchangeToCase != null)
            {
                cstatus = exchangeToCase.StepInfo;
            }
            if (modifyYourPC != null)
            {
                cstatus = modifyYourPC.StepInfo;
            }
            if (openYourDesktop != null)
            {
                cstatus = openYourDesktop.StepInfo;
            }

            if (cstatus == "")
            {
                cstatus = "No Info on this Objective";
            }
        }

        return cstatus;
    }
}


  
