using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpSystem : MonoBehaviour
{

    public GameObject RAMWorldCover;
    public GameObject RAMWorldButton;
    public GameObject CPUFWorldCover;
    public GameObject CPUFWorldButton;
    public GameObject GPUWorldCover;
    public GameObject GPUWorldButton;
    public GameObject StorageWorldCover;
    public GameObject StorageWorldButton;
    public GameObject PSUWorldCover;
    public GameObject PSUWorldButton;
    public GameObject MBWorldCover;
    public GameObject MBWorldButton;
    public GameObject CASEWorldCover;
    public GameObject CASEWorldButton;



    public Sprite RamMap;
    public Sprite CPUFMap;
    public Sprite GPUMap;
    public Sprite StorageMap;
    public Sprite PSUMap;
    public Sprite MBMap;
    public Sprite CaseMap;



    public TMP_Text LevelUpNumber;
    public TMP_Text MapText;
    public Image MapImage;
    
    public void LevelUp()
    {
        if(GameManager.instance.PlayerEXP >= GameManager.instance.PlayerExpToLevelUp)
        {
            if(GameManager.instance.PlayerEXP > GameManager.instance.PlayerExpToLevelUp)
            {
                GameManager.instance.PlayerEXP = Math.Abs(GameManager.instance.PlayerExpToLevelUp - GameManager.instance.PlayerEXP);
                

            }
            else
            {
                GameManager.instance.PlayerEXP = 0;
                
            }
            
            GameManager.instance.PlayerLevel += 1;
            GameManager.instance.PlayerExpToLevelUp *= 2;

            GameManager.instance.PlayerHealth += 100;
            GameManager.instance.PlayerMana += 100;
            GameManager.instance.PlayerArmor += 50;

            AchievementManager.instance.CheckAchievements();
            LevelUpNumber.text = GameManager.instance.PlayerLevel.ToString();
            GameManager.instance.SaveCharInfo(GameManager.instance.UserID, GameManager.instance.PlayerName);
            GameManager.instance.LTA.PlayLevelUp();
            
        }
    }

    public void Update()
    {
        LevelUp();
        CheckLevel();
        UnlockRegionQuest();
    }
    bool RAMcover = false;
    bool CPUFcover = false;
    bool GPUcover = false;
    bool STORAGEcover = false;
    bool PSUcover = false;
    bool MBcover = false;
    bool CASEcover = false;
    public void CheckLevel()
    {
        if(GameManager.instance.PlayerLevel >= 3 && RAMcover == false)
        {
            RAMWorldCover.gameObject.SetActive(false);
            RAMWorldButton.gameObject.SetActive(true);
            RAMcover = true;

            MapImage.gameObject.SetActive(true);
            MapImage.sprite = RamMap;
            MapText.text = "You Unlock the RAM Region";
        }

        else if(GameManager.instance.PlayerLevel >= 6 && CPUFcover == false)
        {
           CPUFWorldCover.gameObject.SetActive(false);
           CPUFWorldButton.gameObject.SetActive(true);
           CPUFcover = true;

            MapImage.gameObject.SetActive(true);
            MapImage.sprite = CPUFMap;
            MapText.text = "You Unlock the CPU Fan Region";
        }

        else if (GameManager.instance.PlayerLevel >= 9 && GPUcover == false)
        {
            GPUWorldCover.gameObject.SetActive(false);
            GPUWorldButton.gameObject.SetActive(true);
            GPUcover = true;

            MapImage.gameObject.SetActive(true);
            MapImage.sprite = GPUMap;
            MapText.text = "You Unlock the GPU Region";
        }

        else if (GameManager.instance.PlayerLevel >= 11 && STORAGEcover == false)
        {
            StorageWorldCover.gameObject.SetActive(false);
            StorageWorldButton.gameObject.SetActive(true);
            STORAGEcover = true;

            MapImage.gameObject.SetActive(true);
            MapImage.sprite = StorageMap;
            MapText.text = "You Unlock the Storage Region";
        }

        else if (GameManager.instance.PlayerLevel >= 13 && PSUcover == false)
        {
            PSUWorldCover.gameObject.SetActive(false);
            PSUWorldButton.gameObject.SetActive(true);
            PSUcover = true;

            MapImage.gameObject.SetActive(true);
            MapImage.sprite = PSUMap;
            MapText.text = "You Unlock the PSU Region";
        }

        else if (GameManager.instance.PlayerLevel >= 14 && MBcover == false)
        {
            MBWorldCover.gameObject.SetActive(false);
            MBWorldButton.gameObject.SetActive(true);
            MBcover = true;

            MapImage.gameObject.SetActive(true);
            MapImage.sprite = MBMap;
            MapText.text = "You Unlock the Motherboard Region";
        }

        else if (GameManager.instance.PlayerLevel >= 15 && CASEcover == false)
        {
            CASEWorldCover.gameObject.SetActive(false);
             CASEWorldButton.gameObject.SetActive(true);
            CASEcover = true;

            MapImage.gameObject.SetActive(true);
            MapImage.sprite = CaseMap;
            MapText.text = "You Unlock the Case Region";
        }
        else
        {
            MapImage.gameObject.SetActive(false);
            MapText.text = "";
        }

    }
    bool unlock3 = false;
    bool unlock6 = false;
    bool unlock9 = false;
    bool unlock11 = false;
    bool unlock13 = false;
    bool unlock14 =  false;
    bool unlock15 =  false;
    public void UnlockRegionQuest()
    {
            
            if (GameManager.instance.PlayerLevel >= 3 && unlock3 == false)
            {
                GameManager.instance.regionsunlocked++;
                unlock3 = true;
            }
            if (GameManager.instance.PlayerLevel >= 6 && unlock6 == false)
            {
                GameManager.instance.regionsunlocked++;
                unlock6 = true;
            }

            if (GameManager.instance.PlayerLevel >= 9 && unlock9 == false)
            {
                GameManager.instance.regionsunlocked++;
                unlock9 = true;
            }
             
            if (GameManager.instance.PlayerLevel >= 11 && unlock11 == false)
            {
                GameManager.instance.regionsunlocked++;
                unlock11 = true;
            }

            if (GameManager.instance.PlayerLevel >= 13 && unlock13 == false)
            {
                GameManager.instance.regionsunlocked++;
                unlock13 = true;
            }

            if (GameManager.instance.PlayerLevel >= 14 && unlock14 == false)
            {
                GameManager.instance.regionsunlocked++;
                 unlock14 = true;
            }

            if (GameManager.instance.PlayerLevel >= 15 && unlock15 == false)
            {
                GameManager.instance.regionsunlocked++;
                unlock15 = true;
            }
        
    }
}
