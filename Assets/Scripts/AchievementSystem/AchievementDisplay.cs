using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class AchievementDisplay : MonoBehaviour
{
    public Image iconImage;
    public Image iconImage2;
    public TMP_Text Title;
    public TMP_Text Description;
    public TMP_Text XPReward;
    public TMP_Text CoinReward;
    public GameObject lockImage;
    private AchievementSO achievement;

    public TMP_Text ProgressText; // Text to show current progress (e.g., 500 / 1000)
    public Slider progressSlider; // Slider to show progress

    public void Setup(AchievementSO achievement)
    {
        this.achievement = achievement;
        iconImage.sprite = achievement.icon;
        iconImage2.sprite = achievement.icon;
        Title.text = achievement.achievementName;
        Description.text = achievement.description;

        if(achievement.ExpReward == 0)
        {
            XPReward.gameObject.SetActive(false);
        }
        else
        {
            XPReward.gameObject.SetActive(true);
            XPReward.text = "EXP: " + achievement.ExpReward;
        }

        if (achievement.MoneyReward == 0) {

            CoinReward.gameObject.SetActive(false);
        }
        else
        {
            CoinReward.gameObject.SetActive(true);
            CoinReward.text = "Coin: " + achievement.MoneyReward;
        }

        UpdateProgress();

        lockImage.SetActive(!achievement.isUnlocked);

    }

    public void UpdateProgress()
    {
        if (achievement.achievementGoal > 0) // Check if this achievement tracks progress with integers
        {
            progressSlider.gameObject.SetActive(true);
            progressSlider.maxValue = achievement.achievementGoal;
            progressSlider.value = achievement.currentValue;

            // Update the progress text (e.g., "500 / 1000")
            ProgressText.text = achievement.currentValue + " / " + achievement.achievementGoal;
        }
        else
        {
            // If the achievement is boolean-based, hide the slider and progress text
            progressSlider.gameObject.SetActive(false);
            ProgressText.gameObject.SetActive(false);
        }
    }
}
