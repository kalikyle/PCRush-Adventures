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

        
       

        lockImage.SetActive(!achievement.isUnlocked);

    }
}
