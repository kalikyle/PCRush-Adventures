using Inventory.Model;
using PC.Model;
using PC.UI;
using Shop.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Inventory.Model.PartsInventorySO;
//using static Inventory.Model.InventorySO;
//

namespace PC.Model
{
    [CreateAssetMenu]
    public class PCInventSO : ScriptableObject
    {
        [SerializeField]
        public List<Computer> ComputerItems;

        public event Action<Dictionary<int, Computer>> OnInventoryUpdated;
        [field: SerializeField]
        public int Size { get; private set; } = 10;


        public void Initialize()
        {
            ComputerItems = new List<Computer>();
            for (int i = 0; i < Size; i++)
            {
                ComputerItems.Add(Computer.GetEmptyItem());
                //Debug.LogError("Initialize");
            }
        }

        public async void ComputerSave(PCSO PC)
        {
            
           await GameManager.instance.SavePCSO(PC);
            //SaveInitialItems(DecorationItems);
        }
        public void AddItem(PCSO PCitems)
        {
            for (int i = 0; i < ComputerItems.Count; i++)
            {
                if (ComputerItems[i].isEmpty)
                {
                    ComputerItems[i] = new Computer
                    {
                        PC = PCitems
                    };
                }
            }
            InformAboutChange();
        }
        public void AddItem(Computer item)
        {
            AddItem(item.PC);
        }
        internal void RemoveComputer(int itemIndex)
        {
            try {
                // Remove the entire the pc
                ComputerItems.RemoveAt(itemIndex);
                InformAboutChange();

            }
            catch { }
                   
        }

        public void AddPCSOList(PCSO pcso)
        {
            // Clear the existing ComputerItems list
            //ComputerItems.Clear();
            // Iterate through the pcsoList and add new Computer items
            // Create a new Computer item
                Computer computer = new Computer
                {
                    PC = pcso
                };
                // Add the Computer item to the ComputerItems list
                ComputerItems.Add(computer);

           
            InformAboutChange();
        }
        public Dictionary<int, Computer> GetCurrentInventoryState()
        {
            Dictionary<int, Computer> returnValue = new Dictionary<int, Computer>();
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

        internal Computer GetItemAt(int pCindex)
        {
            return ComputerItems[pCindex];
        }
        public void SavePCItems()
        {
            //GameManager.Instance.SaveComputerItems(ComputerItems);
            //SaveInitialItems(inventoryItems);
        }
        public void InformAboutChange()
        {
            //Debug.LogError("Informed");
            OnInventoryUpdated?.Invoke(GetCurrentInventoryState());
        }
    }

    [Serializable]
    public struct Computer
    {
        public PCSO PC;

        public bool isEmpty => PC == null;

        public static Computer GetEmptyItem() => new Computer
        {
            PC = null
        };
    }

}

