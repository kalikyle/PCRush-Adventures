using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ArmorRefill : MonoBehaviour
{
    private int currentArmor;
    private int MaxArmor;

    public TMP_Text cost25PercentText;
    public TMP_Text cost50PercentText;
    public TMP_Text cost100PercentText;


    public TMP_Text armortoadd25;
    public TMP_Text armortoadd50;
    public TMP_Text armortoadd100;

    public TMP_Text MaxArmor25;
    public TMP_Text MaxArmor50;
    public TMP_Text MaxArmor100;

    public TMP_Text Added25;
    public TMP_Text Added50;
    public TMP_Text Added100;

    void Start()
    {
    }

    private void Update()
    {
        currentArmor = GameManager.instance.PlayerArmorScript.currentArmor;
        MaxArmor = GameManager.instance.PlayerArmorScript.maxArmor;

        DisplayCost(cost25PercentText, GetCost(0.25));
        DisplayCost(cost50PercentText, GetCost(0.50));
        DisplayCost(cost100PercentText, GetCost(1.00));

        DisplayArmorToAdd(armortoadd25, GetArmorToAdd(0.25));
        DisplayArmorToAdd(armortoadd50, GetArmorToAdd(0.50));
        DisplayArmorToAdd(armortoadd100, GetArmorToAdd(1.00));

        MaxArmor25.text = "/ " + MaxArmor.ToString();
        MaxArmor50.text = "/ " + MaxArmor.ToString();
        MaxArmor100.text = "/ " + MaxArmor.ToString();


        Added25.text = (currentArmor + GetArmorToAdd(0.25) > MaxArmor ? MaxArmor : currentArmor + GetArmorToAdd(0.25)).ToString();
        Added50.text = (currentArmor + GetArmorToAdd(0.50) > MaxArmor ? MaxArmor : currentArmor + GetArmorToAdd(0.50)).ToString();
        Added100.text = (currentArmor + GetArmorToAdd(1.00) > MaxArmor ? MaxArmor : currentArmor + GetArmorToAdd(1.00)).ToString();



    }

    //public void Buy25Percent()
    //{
    //    RefillArmor(0.25);
    //}

    //public void Buy50Percent()
    //{
    //    RefillArmor(0.50);
    //}
    //public void Buy100Percent()
    //{
    //    RefillArmor(1);
    //}

    //private void RefillArmor(double percentage)
    //{
    //    double armorToAdd = MaxArmor * percentage;
    //    int currentArmortoAdd= Mathf.Min(currentArmor + (int)armorToAdd, MaxArmor);

    //    // Update the GameManager with the new armor value
    //    GameManager.instance.PlayerArmorScript.currentArmor = currentArmortoAdd;
    //    GameManager.instance.PlayerArmorScript.isEmpty = false;

    //}

    public void Buy25Percent()
    {
        double cost = GetCost(0.25);
        AttemptRefillArmor(0.25, cost);
    }

    public void Buy50Percent()
    {
        double cost = GetCost(0.50);
        AttemptRefillArmor(0.50, cost);
    }

    public void Buy100Percent()
    {
        double cost = GetCost(1.00);
        AttemptRefillArmor(1.00, cost);
    }

    private void AttemptRefillArmor(double percentage, double cost)
    {
        if (GameManager.instance.PlayerMoney >= cost)
        {
            RefillArmor(percentage);
            
        }
        else
        {
            GameManager.instance.ShowFloatingText("Not enough coins to refill armor");
            SoundManager.instance.PlayNotEnough();

        }
    }

    private void RefillArmor(double percentage)
    {
        double armorToAdd = MaxArmor * percentage;
        int currentArmortoAdd = Mathf.Min(currentArmor + (int)armorToAdd, MaxArmor);

        // Update the GameManager with the new armor value
        GameManager.instance.PlayerArmorScript.currentArmor = currentArmortoAdd;
        GameManager.instance.PlayerArmorScript.isEmpty = false;

        // Deduct coins
        double costInCoins = GetCost(percentage);
        DeductCoins(costInCoins);
    }

    private double GetCost(double percentage)
    {
        double armorToAdd = MaxArmor * percentage;
        return armorToAdd * 0.5;
    }

    private int  GetArmorToAdd(double percentage)
    {
        double armorToAdd = MaxArmor * percentage;
        return (int)armorToAdd;
    }

    private void DisplayCost(TMP_Text textElement, double cost)
    {
        textElement.text = Mathf.FloorToInt((float)cost).ToString();
    }

    private void DisplayArmorToAdd(TMP_Text textElement, double cost)
    {
        textElement.text = "+" +cost.ToString();
    }

    private void DeductCoins(double amount)
    {
        // Implement your coin deduction logic here
        GameManager.instance.ShowFloatingText("<color=green>Your armor has been Fixed</color>");
        GameManager.instance.PlayerMoney -= (int)amount;
        GameManager.instance.SaveCharInfo(GameManager.instance.UserID, GameManager.instance.PlayerName);
        SoundManager.instance.PlayBuyNSell();
    }


}
