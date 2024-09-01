using Inventory.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using UnityEngine;
[CreateAssetMenu]
public class PCSO2 : ScriptableObject
{
    
    public int ID => GetInstanceID();

    [field: SerializeField]
    public string PCName { get; set; }

    [field: SerializeField]
    public Sprite PCImage { get; set; }

    [field: SerializeField]
    public double PCPrice { get; set; }

    [field: SerializeField]
    public ItemSO Case { get; set; }

    [field: SerializeField]
    public ItemSO Motherboard { get; set; }


    [field: SerializeField]
    public ItemSO CPU { get; set; }

    [field: SerializeField]
    public ItemSO CPUFan { get; set; }

    [field: SerializeField]
    public ItemSO RAM { get; set; }

    [field: SerializeField]
    public ItemSO GPU { get; set; }

    [field: SerializeField]
    public ItemSO STORAGE { get; set; }


    [field: SerializeField]
    public ItemSO PSU { get; set; }

    [field: SerializeField]
    public string TestStatus { get; set; }


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
    //        // Create a new instance of PCSO2Data to store the data
    //        PCSO2Data pcsoData = new PCSO2Data
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
    //                PCSO2 loadedPCSO2 = ScriptableObject.CreateInstance<PCSO2>();

    //                // Apply the loaded data to the ScriptableObject
    //                JsonUtility.FromJsonOverwrite(savedData, loadedPCSO2);

    //                // Now, loadedPCSO2 contains the data from the saved file
    //                // You can use loadedPCSO2 as needed
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
//public class PCSO2Data
//{
//    public string PCName;
//    public Sprite PCImage;
//    public double PCPrice;
//    public ItemSO Case;
//    public ItemSO Motherboard;
//    public ItemSO CPU;
//    public ItemSO CPUFan;
//    public ItemSO RAM;
//    public ItemSO GPU;
//    public ItemSO STORAGE;
//    public ItemSO PSU;
//    // ... other properties ...
//}
