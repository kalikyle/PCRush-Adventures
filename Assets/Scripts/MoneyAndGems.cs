using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyAndGems : MonoBehaviour
{
    public Button Money;
    public Button Gems;


    public void Start()
    {
        Money.onClick.AddListener(AddMoney);
        Gems.onClick.AddListener(AddGems);
    }

    public void AddMoney()
    {
        GameManager.instance.PlayerMoney += 1000;
        GameManager.instance.SaveCharInfo(GameManager.instance.UserID, GameManager.instance.PlayerName);
    }
    public void AddGems()
    {
        GameManager.instance.PlayerGems += 1000;
        GameManager.instance.SaveCharInfo(GameManager.instance.UserID, GameManager.instance.PlayerName);
    }

}
