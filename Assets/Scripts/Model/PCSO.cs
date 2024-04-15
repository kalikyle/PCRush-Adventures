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

    [field: SerializeField]
    public double Attack { get; set; }

    [field: SerializeField]
    public double Health { get; set; }

    [field: SerializeField]
    public double Mana { get; set; }


    



    /*public void Save()
    {
        string savePath = Path.Combine(Application.persistentDataPath, "COMPUTERS");

        try
        {
            string savedData = JsonUtility.ToJson(this, true);
            BinaryFormatter bf = new BinaryFormatter();
            using (FileStream file = File.Create(savePath))
            {
                bf.Serialize(file, savedData);
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Save failed: {e.Message}");
        }

    }*/
    //public void Save()
    //{
    //    string savePath = Path.Combine(Application.persistentDataPath, "COMPUTERS");

    //    try
    //    {
    //        // Create a new instance of PCSOData to store the data
    //        PCSOData pcsoData = new PCSOData
    //        {
    //            PCName = this.PCName,
    //            PCImage = this.PCImage,
    //            PCPrice = this.PCPrice,
    //            Case = this.Case,
    //            Motherboard = this.Motherboard,
    //            CPU = this.CPU,
    //            CPUFan = this.CPUFan,
    //            RAM = this.RAM,
    //            GPU = this.GPU,
    //            STORAGE = this.STORAGE,
    //            PSU = this.PSU
    //            // ... copy other properties ...
    //        };

    //        // Convert to JSON
    //        string savedData = JsonUtility.ToJson(pcsoData, true);

    //        // Save to a file
    //        File.WriteAllText(savePath, savedData);
    //    }
    //    catch (Exception e)
    //    {
    //        Debug.LogError($"Save failed: {e.Message}");
    //    }
    //}
    //public void Load()
    //{
    //    string filePath = Path.Combine(Application.persistentDataPath, Application.persistentDataPath, "COMPUTERS");

    //    if (File.Exists(filePath))
    //    {
    //        BinaryFormatter bf = new BinaryFormatter();

    //        try
    //        {
    //            using (FileStream file = File.Open(filePath, FileMode.Open))
    //            {
    //                // Deserialize the saved data as a string
    //                string savedData = (string)bf.Deserialize(file);

    //                // Create a new instance of your ScriptableObject
    //                PCSO loadedPCSO = ScriptableObject.CreateInstance<PCSO>();

    //                // Apply the loaded data to the ScriptableObject
    //                JsonUtility.FromJsonOverwrite(savedData, loadedPCSO);

    //                // Now, loadedPCSO contains the data from the saved file
    //                // You can use loadedPCSO as needed
    //            }
    //        }
    //        catch (Exception e)
    //        {
    //            Debug.LogError($"Load failed: {e.Message}");
    //        }
    //    }
    //    else
    //    {
    //        Debug.LogWarning("Save file does not exist.");
    //    }
    //}

}

//[System.Serializable]
//public class PCSOData
//{
//    public string PCName;
//    public Sprite PCImage;
//    public double PCPrice;
//    public PartsSO Case;
//    public PartsSO Motherboard;
//    public PartsSO CPU;
//    public PartsSO CPUFan;
//    public PartsSO RAM;
//    public PartsSO GPU;
//    public PartsSO STORAGE;
//    public PartsSO PSU;
//    // ... other properties ...
//}
