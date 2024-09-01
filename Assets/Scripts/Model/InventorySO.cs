using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Inventory.Model {
    [CreateAssetMenu]
    public class InventorySO : ScriptableObject
    {
        
        
        [SerializeField]
        public List<InventoryItem> inventoryItems;
        public event Action<Dictionary<int, InventoryItem>> OnInventoryUpdated;
        [field:SerializeField]
        public int Size { get; private set; }//this will add a size field in unity//sets to 999
        
       
        public void Initialize()
        {
           
            inventoryItems = new List<InventoryItem>();
            for (int i = 0; i < Size; i++)
            {
                inventoryItems.Add(InventoryItem.GetEmptyItem());
            }
        }

       public void SaveItems()
        {
            GameManager2.Instance.SaveInitialItems(inventoryItems);
            //SaveInitialItems(inventoryItems);
        }
        
        public void AddItem(InventoryItem item)
        {
            AddItem(item.item, item.quantity);
        }

        public void AddItem(ItemSO item, int quantity)
        {
            if (item.IsStackable == false)
            {
                for (int i = 0; i < inventoryItems.Count;)//i++
                {
                    while (quantity > 0)
                    {
                        quantity -= AddItemToFirstFreeSlot(item, quantity);
                    }
                    InformAboutChange();
                    return;
                }
            }
            quantity = AddStackebleItem(item, quantity);
            InformAboutChange();
        }


        private int AddItemToFirstFreeSlot(ItemSO item, int quantity)
        {
            InventoryItem newItem = new InventoryItem
            {
                item = item,
                quantity = quantity
            };

            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i].isEmpty)
                {
                    inventoryItems[i] = newItem;
                    return quantity;
                }
            }
            return 0;
        }
       

        private bool IsInventoryFull() => inventoryItems.Where(item => item.isEmpty).Any() == false;
        
        public int AddStackebleItem(ItemSO item, int quantity)
        {
            
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i].isEmpty) { continue; }
                if (inventoryItems[i].item.ID == item.ID) {

                    int amountPossibleToTake = inventoryItems[i].item.MaxStackableSize - inventoryItems[i].quantity;

                    if(quantity > amountPossibleToTake)
                    {
                        inventoryItems[i] = inventoryItems[i].ChangeQuantity(inventoryItems[i].item.MaxStackableSize);
                        quantity -= amountPossibleToTake;
                    }
                    else
                    {
                        inventoryItems[i] = inventoryItems[i].ChangeQuantity(inventoryItems[i].quantity + quantity);
                        InformAboutChange();
                        return 0;
                    }
                }
                
            }
            while (quantity > 0 && IsInventoryFull() == false)
            {
                int newQuantity = Mathf.Clamp(quantity, 0, item.MaxStackableSize);
                quantity -= newQuantity;
                AddItemToFirstFreeSlot(item, newQuantity);
               

            }
            return quantity;
        }
       

        internal void RemoveItem(int itemIndex, int amount)
        {
            try {
                if (inventoryItems.Count > itemIndex)
                {
                    if (inventoryItems[itemIndex].isEmpty)
                    {
                        return;
                    }

                    int currentQuantity = inventoryItems[itemIndex].quantity;

                    if (currentQuantity <= amount)
                    {
                        // Remove the entire item.
                        inventoryItems.RemoveAt(itemIndex);
                        GameManager2.Instance.itemsToTransfer.RemoveAt(itemIndex);
                        InformAboutChange();

                    }
                    else
                    {
                        // Decrease the item quantity.
                        inventoryItems[itemIndex] = inventoryItems[itemIndex].ChangeQuantity(currentQuantity - amount);
                        GameManager2.Instance.itemsToTransfer[itemIndex] = GameManager2.Instance.itemsToTransfer[itemIndex].ChangeQuantity(currentQuantity - amount);
                        InformAboutChange();
                    }
                    
                }
            } catch (Exception) { }
           
        }

        public Dictionary<int, InventoryItem> GetCurrentInventoryState()
        {
            Dictionary<int, InventoryItem> returnValue = new Dictionary<int, InventoryItem>();
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i].isEmpty)
                {
                    continue;
                }
                returnValue[i] = inventoryItems[i];
            }
            return returnValue;
        }
        public List<InventoryItem> GetItemsByCategory(string category)
        {
            return inventoryItems.Where(item => !item.isEmpty && item.item.Category.Equals(category)).ToList();
        }

        public InventoryItem GetItemAt(int itemIndex)
        {
            return inventoryItems[itemIndex];
        }

        public void SwapItems(int itemIndex1, int itemIndex2)
        {
            try
            {
                InventoryItem item1 = inventoryItems[itemIndex1];
                inventoryItems[itemIndex1] = inventoryItems[itemIndex2];
                inventoryItems[itemIndex2] = item1;
                InformAboutChange();
            }catch (Exception ex)
            {
                Console.WriteLine("Error:" + ex.Message);
            }

        }

        public void InformAboutChange()
        {
            
            OnInventoryUpdated?.Invoke(GetCurrentInventoryState());
        }
        

        [Serializable]
        public struct InventoryItem
        {
            public int quantity;
            public ItemSO item;
            public bool isEmpty => item == null;

            public InventoryItem ChangeQuantity(int newQuantity)
            {
                return new InventoryItem
                {
                    item = this.item,
                    quantity = newQuantity,
                };
            }
            public static InventoryItem GetEmptyItem() => new InventoryItem
            {
                item = null,
                quantity = 0,
                
            };


           
        }

       
    }
}
