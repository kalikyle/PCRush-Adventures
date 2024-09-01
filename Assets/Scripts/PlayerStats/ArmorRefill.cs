using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorRefill : MonoBehaviour
{
    private int currentArmor;
    private int MaxArmor;
    void Start()
    {
        
    }

    private void Update()
    {
        currentArmor = GameManager.instance.PlayerArmorScript.currentArmor;
        MaxArmor = GameManager.instance.PlayerArmorScript.maxArmor;
    }

    public void Buy25Percent()
    {
        RefillArmor(0.25);
    }

    public void Buy50Percent()
    {
        RefillArmor(0.50);
    }
    public void Buy100Percent()
    {
        RefillArmor(1);
    }

    private void RefillArmor(double percentage)
    {
        double armorToAdd = MaxArmor * percentage;
        int currentArmortoAdd= Mathf.Min(currentArmor + (int)armorToAdd, MaxArmor);

        // Update the GameManager with the new armor value
        GameManager.instance.PlayerArmorScript.currentArmor = currentArmortoAdd;
        GameManager.instance.PlayerArmorScript.isEmpty = false;
        
    }
}
