using Inventory.Model;
using PC.Model;
using PC.UI;
using Shop.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Inventory.Model.InventorySO;
//using static UnityEditor.Progress;

namespace PC.Model
{
    [CreateAssetMenu]
    public class PCInventSO2 : ScriptableObject
    {
        [SerializeField]
        public List<Computer2> ComputerItems;

        public event Action<Dictionary<int, Computer2>> OnInventoryUpdated;
        [field: SerializeField]
        public int Size { get; private set; } = 10;


        public void Initialize()
        {
            ComputerItems = new List<Computer2>();
            for (int i = 0; i < Size; i++)
            {
                ComputerItems.Add(Computer2.GetEmptyItem());
                //Debug.LogError("Initialize");
            }
        }
        public void AddItem(PCSO2 PCitems)
        {
            for (int i = 0; i < ComputerItems.Count; i++)
            {
                if (ComputerItems[i].isEmpty)
                {
                    ComputerItems[i] = new Computer2
                    {
                        PC = PCitems
                    };
                }
            }
            InformAboutChange();
        }
        public void AddItem(Computer2 item)
        {
            AddItem(item.PC);
        }
        internal void RemoveComputer2(int itemIndex)
        {
            try {
                // Remove the entire the pc
                ComputerItems.RemoveAt(itemIndex);
                InformAboutChange();

            }
            catch { }
                   
        }

        public void AddPCSO2List(PCSO2 pcso)
        {
            // Clear the existing ComputerItems list
            //ComputerItems.Clear();
            // Iterate through the pcsoList and add new Computer2 items
            // Create a new Computer2 item
                Computer2 computer = new Computer2
                {
                    PC = pcso
                };
                // Add the Computer2 item to the ComputerItems list
                ComputerItems.Add(computer);
            

            InformAboutChange();
        }
        public Dictionary<int, Computer2> GetCurrentInventoryState()
        {
            Dictionary<int, Computer2> returnValue = new Dictionary<int, Computer2>();
            for (int i = 0; i < ComputerItems.Count; i++)
            {
                if (ComputerItems[i].isEmpty)
                {
                    continue;
                }
                returnValue[i] = ComputerItems[i];
            }
            return returnValue;
        }

        internal Computer2 GetItemAt(int pCindex)
        {
            return ComputerItems[pCindex];
        }
        public void SavePCItem2s()
        {
            GameManager2.Instance.SaveComputerItems(ComputerItems);
            //SaveInitialItems(inventoryItems);
        }
        public void InformAboutChange()
        {
            //Debug.LogError("Informed");
            OnInventoryUpdated?.Invoke(GetCurrentInventoryState());
        }
    }

    [Serializable]
    public struct Computer2
    {
        public PCSO2 PC;

        public bool isEmpty => PC == null;

        public static Computer2 GetEmptyItem() => new Computer2
        {
            PC = null
        };
    }

}

