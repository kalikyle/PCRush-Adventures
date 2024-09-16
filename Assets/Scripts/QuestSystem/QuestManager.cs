using Firebase.Firestore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using static Decoration.Model.DecorSO;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;
    [Header("Config")]
    [SerializeField] private bool LoadQuestState = true;
    private Dictionary<string, Quest> questMap;
    //private int PlayerLevel = 0;

    private void Awake()
    {
        
       
        Instance = this;
        //Quest quest = GetQuestId("PackageQuest");
        //Debug.Log(quest.info.displayName);
        //Debug.Log(quest.info.levelRequirement);
        //Debug.Log(quest.state);
        //Debug.Log(quest.Currentstepexist());
    }
  
    private void OnEnables()
    {
        GameManager.instance.questEvents.onStartQuest += StartQuest;
        GameManager.instance.questEvents.onAdvanceQuest += AdvanceQuest;
        GameManager.instance.questEvents.onFinishQuest += FinishQuest;
        GameManager.instance.questEvents.onQuestStepStateChange += QuestStepStateChange;
    }

    
    public void Update()
    {
       // GetPlayerLevel();

        try
        {
            foreach (Quest quest in questMap.Values)
            {
                if (quest.state == QuestState.REQUIREMENT_NOT_MET && CheckRequirements(quest))
                {
                    ChangeQuestState(quest.info.id, QuestState.CAN_START);
                }
            }
        }
        catch (Exception ) { }
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

    private void OnDisables()
    {
        GameManager.instance.questEvents.onStartQuest -= StartQuest;
        GameManager.instance.questEvents.onAdvanceQuest -= AdvanceQuest;
        GameManager.instance.questEvents.onFinishQuest -= FinishQuest;
        GameManager.instance.questEvents.onQuestStepStateChange -= QuestStepStateChange;
    }



    private void Start()
    {
        OnEnables();


        //await Task.Delay(1000);
        //if (GameManager.instance.UserID == "")
        //{
        //    ForNewUsers();
        //}

        //await Task.Delay(1000);
        //questMap = await CreateQuestMap();

        //GameManager.instance.questEvents.onStartQuest += StartQuest;
        //GameManager.instance.questEvents.onAdvanceQuest += AdvanceQuest;
        //GameManager.instance.questEvents.onFinishQuest += FinishQuest;
        //GameManager.instance.questEvents.onQuestStepStateChange += QuestStepStateChange;

        //foreach (Quest quest in questMap.Values)
        //{

        //    if (quest.state == QuestState.IN_PROGRESS)
        //    {
        //        quest.InstantiateCurrentQuestStep(this.transform);
        //    }
        //    else if (quest.state == QuestState.FINISHED)
        //    {
        //        GameManager.instance.PlayerDeskRoom.SetActive(true);
        //        GameManager.instance.BuildingDesk.SetActive(true);
        //        GameManager.instance.HouseDoor.SetActive(true);
        //        GameManager.instance.packagescollected = 8;


        //        GameManager.instance.QuestUI.gameObject.SetActive(false);
        //        // need to have this in the rest of the quest step
        //    }

        //    GameManager.instance.questEvents.QuestStateChange(quest);
        //}
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

    private async Task<Dictionary<string, Quest>> CreateQuestMap()
    {
       
            QuestInfoSO[] allquest = Resources.LoadAll<QuestInfoSO>("Quests");

            Dictionary<string, Quest> idToQuestMap = new Dictionary<string, Quest>();

            foreach (QuestInfoSO questInfo in allquest)
            {
                if (idToQuestMap.ContainsKey(questInfo.id))
                {
                    Debug.LogWarning("Duplicated ID!");
                }
                Quest quest = await LoadQuest(questInfo);
                idToQuestMap.Add(questInfo.id, quest);
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

    private void QuestStepStateChange(string id, int stepIndex, QuestStepState questStepState)
    {
        Quest quest = GetQuestId(id);
        quest.StoreQuestStepState(questStepState, stepIndex);
        ChangeQuestState(id, quest.state);
    }


    private void OnApplicationQuit()
    {
        SaveQuests();
    }

    public void SaveQuests()
    {
        try
        {

            foreach (Quest quest in questMap.Values)
            {
                if (quest != null)
                {
                    SaveQuest(quest);

                }

            }
        }
        catch { }
    }

    // to be saved in firebase
    //private void SaveQuest(Quest quest)
    //{
    //    try
    //    {
    //        QuestData questData = quest.GetQuestData();
    //        string serializeData = JsonUtility.ToJson(questData);
    //        PlayerPrefs.SetString(quest.info.id, serializeData);

    //    }catch(Exception e)
    //    {
    //        Debug.LogError("failed to save data;");
    //    }
    //}

    public async void SaveQuest(Quest quest)
    {

        try
        {
            if (GameManager.instance.UserID != "")
            {
                QuestData questData = quest.GetQuestData();
                // Convert the list of items to JSON
                string serializeData = JsonUtility.ToJson(questData);

                DocumentReference docRef = FirebaseFirestore.DefaultInstance.Collection(GameManager.instance.UserCollection).Document(GameManager.instance.UserID);

                CollectionReference SubDocRef = docRef.Collection("Quests");

                DocumentReference DecordocRef = SubDocRef.Document(quest.info.id);
                // Create a dictionary to store the data
                Dictionary<string, object> dataDict = new Dictionary<string, object>
    {
        { "questData", serializeData }
    };

                // Set the data of the document
                await DecordocRef.SetAsync(dataDict);

                //Debug.Log(serializeData + "has been saved");

            }
        }
        catch (Exception ex)
        {
            Debug.LogError("failed to save data;" + ex);
        }
    }

    //private Quest LoadQuest(QuestInfoSO questInfo)
    //{
    //    Quest quest = null;
    //    try
    //    {

    //        if (PlayerPrefs.HasKey(questInfo.id))
    //        {
    //            string serializedData = PlayerPrefs.GetString(questInfo.id);
    //            QuestData questData = JsonUtility.FromJson<QuestData>(serializedData);
    //            quest = new Quest(questInfo, questData.state, questData.questStepIndex, questData.questStepStates);
    //        }
    //        else
    //        {
    //            quest = new Quest(questInfo);
    //        }


    //    }catch (Exception ex)
    //    {
    //        Debug.LogError("failed to load data;");
    //    }
    //    return quest;
    //}

    private async Task<Quest> LoadQuest(QuestInfoSO questInfo)
    {
       
        Quest quest = null;
        try
        {
           
            await Task.Delay(2000);
            if (GameManager.instance.UserID == "")
            {
                
                //Debug.LogWarning("new User " + questInfo.id);
                // Create a new quest if the document does not exist
                quest = new Quest(questInfo);
            }
            else
            {
                // Get a reference to the Firestore document
                DocumentReference docRef = FirebaseFirestore.DefaultInstance.Collection(GameManager.instance.UserCollection)
                                                    .Document(GameManager.instance.UserID).Collection("Quests")
                                                    .Document(questInfo.id);

                // Fetch the document snapshot
                DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

                // Check if the document exists
                if (snapshot.Exists && LoadQuestState)
                {
                    // Deserialize the JSON data from Firestore into QuestData
                    string serializedData = snapshot.GetValue<string>("questData");
                    QuestData questData = JsonUtility.FromJson<QuestData>(serializedData);

                    // Create the quest object
                    quest = new Quest(questInfo, questData.state, questData.questStepIndex, questData.questStepStates);
                }
                else
                {
                    Debug.LogWarning("Quest document does not exist for ID: " + questInfo.id);
                    // Create a new quest if the document does not exist
                    quest = new Quest(questInfo);
                }
            }
        
            
        }
        catch (Exception ex)
        {
            Debug.LogError("Failed to load quest data: " + ex.Message);
        }
        return quest;
    }

    //public void ResetQuests()
    //{
    //    // Unsubscribe from all even
    //    OnDisables();
        
    //    // Clear the quest map
    //    questMap.Clear();

    //    Debug.LogError("questmapCleared");


       
    //}

    //public async Task LoadQuestsForNewUser()
    //{
    //    // Ensure the UserID is correctly reset or set for new user scenarios
    //    if (string.IsNullOrEmpty(GameManager.instance.UserID))
    //    {
    //        Debug.LogError("UserID is empty, cannot load quests.");
    //        return;
    //    }

        
    //    OnEnables();

    //    Debug.LogError("questmapCreated");
    //    // Load quests for the new user
    //    questMap = await CreateQuestMap();
        
    //    // Re-subscribe to the events


    //    // Reinitialize quests
    //    await Task.Delay(500);
    //    foreach (Quest quest in questMap.Values)
    //    {
    //        if (quest.state == QuestState.IN_PROGRESS)
    //        {
    //            quest.InstantiateCurrentQuestStep(this.transform);
    //        }
    //        else if (quest.state == QuestState.FINISHED)
    //        {
    //            GameManager.instance.PlayerDeskRoom.SetActive(true);
    //            GameManager.instance.BuildingDesk.SetActive(true);
    //            GameManager.instance.HouseDoor.SetActive(true);
    //            GameManager.instance.packagescollected = 8;

    //            GameManager.instance.QuestUI.gameObject.SetActive(false);
    //        }

    //        GameManager.instance.questEvents.QuestStateChange(quest);
    //    }
    //}

    public async void ForExistingUsers()
    {
        // Ensure the UserID is correctly reset or set for new user scenarios
        if (string.IsNullOrEmpty(GameManager.instance.UserID))
        {
            Debug.LogError("UserID is empty, cannot load quests.");
            return;
        }
       
        // Load quests for the new user
        questMap = await CreateQuestMap();
        // Re-subscribe to the events
        foreach (Quest quest in questMap.Values)
        {
            if (quest.state == QuestState.IN_PROGRESS)
            {
                quest.InstantiateCurrentQuestStep(this.transform);
            }
            else if (quest.state == QuestState.FINISHED)
            {
                //defaults if any quest are finished
                GameManager.instance.CPUSpawn.SetActive(true);
                GameManager.instance.ExitRoom.SetActive(true);
                GameManager.instance.PlayerDeskRoom.SetActive(true);
                GameManager.instance.BuildingDesk.SetActive(true);
                GameManager.instance.HouseDoor.SetActive(true);
                GameManager.instance.packagescollected = 8;
            }

            GameManager.instance.questEvents.QuestStateChange(quest);
        }
    }

    public async void ForNewUsers()
    {
        // Ensure the UserID is correctly reset or set for new user scenario
        //Debug.LogError("questmapCreated");
        // Load quests for the new user
        questMap = await CreateQuestMap();

        // Re-subscribe to the events
        foreach (Quest quest in questMap.Values)
        {
            if (quest.state == QuestState.IN_PROGRESS)
            {
                quest.InstantiateCurrentQuestStep(this.transform);
            }
            else if (quest.state == QuestState.FINISHED)
            {
                GameManager.instance.PlayerDeskRoom.SetActive(true);
                GameManager.instance.BuildingDesk.SetActive(true);
                GameManager.instance.HouseDoor.SetActive(true);
                GameManager.instance.packagescollected = 8;

                GameManager.instance.QuestUI.gameObject.SetActive(false);
            }

            GameManager.instance.questEvents.QuestStateChange(quest);
        }
    }
    //public async void OnUserChanged()
    //{
    //    // Assume the user has already been changed in GameManager
    //    questMap.Clear();
    //    await LoadQuestsForNewUser();

    //}

}
