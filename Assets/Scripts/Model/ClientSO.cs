using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class ClientSO : ScriptableObject
{
    // Start is called before the first frame update

    public int ID => GetInstanceID();

    [field: SerializeField]
    public Sprite ClientImage { get; set; }


    [field: SerializeField]
    public string ClientName { get; set; }

    [field: SerializeField]
    [field: TextArea]
    public string Description { get; set; }


    [field: SerializeField]
    public double CaseStrength { get; set; }

    // FOR MOTHERBOARD
    [field: SerializeField]
    public double MotherboardStrength { get; set; }

    //for CPU
    [field: SerializeField]
    public double BaseSpeed { get; set; }

    //for RAM
    [field: SerializeField]
    public double Memory { get; set; }

    //for CPU Fan
    [field: SerializeField]
    public double CoolingPower { get; set; }

    //for GPU 
    [field: SerializeField]
    public double ClockSpeed { get; set; }

    //for Storage
    [field: SerializeField]
    public double Storage { get; set; }

    // for PSU
    [field: SerializeField]
    public double WattagePower { get; set; }


    [field: SerializeField]
    public float OrderPrice { get; set; }

    [field: SerializeField]
    public float TimeLimit { get; set; }

    [field: SerializeField]
    public int EXP { get; set; }

    [field: SerializeField]
    public int Level { get; set; }
}
