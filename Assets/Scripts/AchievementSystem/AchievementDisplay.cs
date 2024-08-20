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
    public GameObject lockImage;
    private AchievementSO achievement;

    public void Setup(AchievementSO achievement)
    {
        this.achievement = achievement;
        iconImage.sprite = achievement.icon;
        iconImage2.sprite = achievement.icon;
        Title.text = achievement.achievementName;
        Description.text = achievement.description;
        lockImage.SetActive(!achievement.isUnlocked);
    }
}
