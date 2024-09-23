using Firebase.Extensions;
using Firebase.Firestore;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using static OtherWorld.Model.OWInvSO;

public class AchievementManager : MonoBehaviour
{
    public List<AchievementSO> achievements;
    public static AchievementManager instance;
    public Transform contentPanel;                   
    public GameObject achievementPrefab;
    public GameObject achievepopup;

    [Header("Achievements")]
    public bool BuildfirstPCs;
    public bool FinishStory = false;

    public void Awake()
    {
        if (instance == null)
        {

            instance = this;
            DontDestroyOnLoad(gameObject);

        }
        else
        {
            // If another instance already exists, destroy this one
            Destroy(gameObject);
        }
    }

    void Start()
    {
        //await Task.Delay(1000);
        //CheckAchievements();
        //DisplayAchievements();
    }

    public void CheckAchievements()
    {
        
        foreach (var achievement in achievements)
        {
            if (!achievement.isUnlocked && achievement.CheckCompletion())
            {
                UnlockAchievement(achievement);
            }

        }

        //await Task.Delay(500);
        //DisplayAchievements();
       
    }

    public void UnlockAchievement(AchievementSO achievement)
    {
        if (!achievement.isUnlocked)
        {
            achievement.isUnlocked = true;
            Debug.Log($"Achievement Unlocked: {achievement.achievementName}");
            // Display UI, notify the player, etc.
            ShowPopUpAchievement(achievement);
            DisplayAchievements();
            // Refresh UI to reflect changes
            SaveAchievementToFirebase(achievement);
            GameManager.instance.AddPlayerExp(achievement.ExpReward);
            GameManager.instance.PlayerMoney += achievement.MoneyReward;
            GameManager.instance.PlayerTotalMoney += achievement.MoneyReward;
            AchievementManager.instance.CheckAchievements();
            GameManager.instance.SaveCharInfo(GameManager.instance.UserID, GameManager.instance.PlayerName);
        }
    }


    private void SaveAchievementToFirebase(AchievementSO achievement)
    {
        string UserCollection = "users"; // Your Firestore collection for users
        string UserID = GameManager.instance.UserID; // User ID from your game manager

        // Get a reference to the user's document
        DocumentReference docRef = FirebaseFirestore.DefaultInstance.Collection(UserCollection).Document(UserID);

        // Update Firestore with the unlocked achievement
        Dictionary<string, object> achievementData = new Dictionary<string, object>
        {
            { "AchievementName", achievement.achievementName },
            { "IsUnlocked", achievement.isUnlocked },
            { "CurrentValue", achievement.currentValue },
            { "UnlockDate", FieldValue.ServerTimestamp } // optional: track when it was unlocked
        };

        // Add the unlocked achievement to a subcollection called "UnlockedAchievements"
        docRef.Collection("UnlockedAchievements").Document(achievement.achievementName).SetAsync(achievementData).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log($"Achievement {achievement.achievementName} saved to Firebase.");
            }
            else
            {
                Debug.LogError("Error saving achievement: " + task.Exception);
            }
        });
    }






    public void LoadAchievementsFromFirebase()
    {
        //Debug.LogError("EXECUTEDDDDD");
        string UserCollection = "users";
        string UserID = GameManager.instance.UserID;

        // Get a reference to the user's unlocked achievements
        DocumentReference docRef = FirebaseFirestore.DefaultInstance.Collection(UserCollection).Document(UserID);
        CollectionReference unlockedAchievementsRef = docRef.Collection("UnlockedAchievements");

        unlockedAchievementsRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                QuerySnapshot snapshot = task.Result;

                foreach (DocumentSnapshot document in snapshot.Documents)
                {
                    string achievementName = document.GetValue<string>("AchievementName");
                    bool isUnlocked = document.GetValue<bool>("IsUnlocked");
                    int currentvalue = document.GetValue<int>("CurrentValue");
                    // Update the local achievement list based on the data loaded from Firebase
                    AchievementSO achievement = achievements.Find(a => a.achievementName == achievementName);
                    if (achievement != null && isUnlocked)
                    {
                        achievement.isUnlocked = true;
                        achievement.currentValue = currentvalue;
                    }
                }

                // After loading achievements, update the UI
                //await Task.Delay(1000);
                DisplayAchievements();
            }
            else
            {
                Debug.LogError("Error loading achievements: " + task.Exception);
            }
        });
    }


    public void DisplayAchievements()
    {
        foreach (Transform child in contentPanel)
        {
            Destroy(child.gameObject); // Clear the existing list before displaying
        }

        foreach (var achievement in achievements)
        {
            GameObject achievementGO = Instantiate(achievementPrefab, contentPanel);
            AchievementDisplay display = achievementGO.GetComponent<AchievementDisplay>();
            display.Setup(achievement);
        }
    }


    public void ShowPopUpAchievement(AchievementSO achievement)
    {
        GameObject newShopPopup = Instantiate(achievepopup, GameManager.instance.notifpPopUpParent); // Instantiate the popup as a child of the designated parent

        LeanTween.moveLocal(newShopPopup, new Vector3(0f, 85f, 0f), 2f)
            .setEase(LeanTweenType.easeOutExpo)
            .setOnComplete(() => HidePopUpEquipments(newShopPopup));

        UpdateShopPopupEquipments(newShopPopup, achievement);
    }

    private void HidePopUpEquipments(GameObject shopPopup)
    {
        LeanTween.moveLocal(shopPopup, new Vector3(0f, 250f, 0f), 1f)
            .setDelay(4f)
            .setEase(LeanTweenType.easeOutExpo)
            .setOnComplete(() => Destroy(shopPopup));
    }

    private void UpdateShopPopupEquipments(GameObject shopPopup, AchievementSO achievement)
    {
        Image[] images = shopPopup.GetComponentsInChildren<Image>();
        TMP_Text[] texts = shopPopup.GetComponentsInChildren<TMP_Text>();

        foreach (var image in images)
        {
            // Check conditions or naming conventions to identify the image elements you need to update
            if (image.gameObject.name == "Icon") // Assuming the GameObject name is set in the editor
            {
                image.sprite = achievement.icon;
                break; // Assuming there's only one image to update
            }
        }

        foreach (var text in texts)
        {
            // Check conditions or naming conventions to identify the text elements you need to update
            if (text.gameObject.name == "Title") // Assuming the GameObject name is set in the editor
            {
                text.text = achievement.achievementName;
            }
            else if (text.gameObject.name == "Desc") // Assuming the GameObject name is set in the editor
            {
                text.text = achievement.description;
            }

            }
        }
    private void OnApplicationQuit()
    {
        foreach (var achievement in achievements)
        {
            achievement.isUnlocked = false;
            achievement.currentValue = 0;
        }
    }
}
