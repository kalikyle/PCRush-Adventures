using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
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
        DisplayAchievements();
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
    }

    public void UnlockAchievement(AchievementSO achievement)
    {
        if (!achievement.isUnlocked)
        {
            achievement.isUnlocked = true;
            Debug.Log($"Achievement Unlocked: {achievement.achievementName}");
            // Display UI, notify the player, etc.
            ShowPopUpAchievement(achievement);
            DisplayAchievements(); // Refresh UI to reflect changes
        }
    }

    void DisplayAchievements()
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

        LeanTween.moveLocal(newShopPopup, new Vector3(0f, 137.7f, 0f), 2f)
            .setEase(LeanTweenType.easeOutExpo)
            .setOnComplete(() => HidePopUpEquipments(newShopPopup));

        UpdateShopPopupEquipments(newShopPopup, achievement);
    }

    private void HidePopUpEquipments(GameObject shopPopup)
    {
        LeanTween.moveLocal(shopPopup, new Vector3(0f, 292.7f, 0f), 1f)
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
}
