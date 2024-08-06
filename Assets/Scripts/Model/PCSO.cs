using Exchanger.Model.CaseWorld;
using Exchanger.Model.CPUFWorld;
using Exchanger.Model.CPUWorld;
using Exchanger.Model.GPUWorld;
using Inventory.Model;
using PartsInventory.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using UnityEngine;
[CreateAssetMenu]
public class PCSO : ScriptableObject
{

    public int ID => GetInstanceID();

    [field: SerializeField]
    public string PCName { get; set; }

    [field: SerializeField]
    public Sprite PCImage { get; set; }

    [field: SerializeField]
    public PartsSO Case { get; set; }

    [field: SerializeField]
    public PartsSO Motherboard { get; set; }


    [field: SerializeField]
    public PartsSO CPU { get; set; }

    [field: SerializeField]
    public PartsSO CPUFan { get; set; }

    [field: SerializeField]
    public PartsSO RAM { get; set; }

    [field: SerializeField]
    public PartsSO GPU { get; set; }

    [field: SerializeField]
    public PartsSO STORAGE { get; set; }


    [field: SerializeField]
    public PartsSO PSU { get; set; }

    [field: SerializeField]
    public bool inUse { get; set; }

    //Perks

    [field: SerializeField]
    public double AttackDamage { get; set; }

    [field: SerializeField]
    public double Health { get; set; }

    [field: SerializeField]
    public double Mana { get; set; }

    [field: SerializeField]
    public double HealthRegen { get; set; }

    [field: SerializeField]
    public double WalkSpeed { get; set; }

    [field: SerializeField]
    public double Armor { get; set; }

    [field: SerializeField]
    public double AttackSpeed { get; set; }

    [field: SerializeField]
    public double CriticalHit { get; set; }

    [field: SerializeField]
    public double CriticalChance { get; set; }

    

}


