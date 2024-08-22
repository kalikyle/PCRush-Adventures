using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpSystem : MonoBehaviour
{
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


            GameManager.instance.SaveCharInfo(GameManager.instance.UserID, GameManager.instance.PlayerName);
        }
    }

    public void Update()
    {
        LevelUp();
    }

    public void CheckLevel()
    {
        if(GameManager.instance.PlayerLevel == 3)
        {

        }
    }
}
