using PartsInventory.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Decoration.Model.DecorSO;
using static UnityEditor.Progress;

namespace Inventory.Model
{
    [CreateAssetMenu]
    public class PartsInventorySO : ScriptableObject
    {
        [SerializeField]
        public List<InventoryItem> inventoryItems;
        public event Action<Dictionary<int, InventoryItem>> OnInventoryUpdated;
        [field: SerializeField]
        public int Size { get; private set; }//this will add a size field in unity//sets to 999


        public void Initialize()
        {

            inventoryItems = new List<InventoryItem>();
            for (int i = 0; i < Size; i++)
            {
                inventoryItems.Add(InventoryItem.GetEmptyItem());
            }
        }

        

        public void PartsSaveItems()
        {
            GameManager.instance.SaveComputerParts(inventoryItems);
            //SaveInitialItems(DecorationItems);
        }

        public void AddItem(InventoryItem item)
        {
            AddItem(item.item, item.quantity);
        }

        public int AddItem(PartsSO item, int quantity)
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
                    return quantity;
                }
            }
            InformAboutChange();
            return quantity = AddStackebleItem(item, quantity);
        }


        private int AddItemToFirstFreeSlot(PartsSO item, int quantity)
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

        public int AddStackebleItem(PartsSO item, int quantity)
        {

            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i].isEmpty) { continue; }
                if (inventoryItems[i].item.ID == item.ID)
                {

                    int amountPossibleToTake = inventoryItems[i].item.MaxStackableSize - inventoryItems[i].quantity;

                    if (quantity > amountPossibleToTake)
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
            try
            {
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
                        GameManager.instance.itemsToTransfer.RemoveAt(itemIndex);
                        InformAboutChange();

                    }
                    else
                    {
                        // Decrease the item quantity.
                        inventoryItems[itemIndex] = inventoryItems[itemIndex].ChangeQuantity(currentQuantity - amount);
                        GameManager.instance.itemsToTransfer[itemIndex] = GameManager.instance.itemsToTransfer[itemIndex].ChangeQuantity(currentQuantity - amount);
                        InformAboutChange();
                    }

                }
            }
            catch (Exception) { }

        }
        internal void RemoveItem(string name, int amount)
        {
            try
            {
                // Find the index of the itemToRemove in the inventoryItems list
                int indexToRemove = inventoryItems.FindIndex(item => item.item.name.Equals(name));
            // Debug.LogError(indexToRemove);
               
                    if (inventoryItems.Count > indexToRemove)
                    {
                        if (inventoryItems[indexToRemove].isEmpty)
                        {
                            return;
                        }

                        int currentQuantity = inventoryItems[indexToRemove].quantity;

                        if (currentQuantity <= amount)
                        {
                            // Remove the entire item.
                            inventoryItems.RemoveAt(indexToRemove);
                            GameManager.instance.itemsToTransfer.RemoveAt(indexToRemove);
                            InformAboutChange();

                        }
                        else
                        {
                            // Decrease the item quantity.
                            inventoryItems[indexToRemove] = inventoryItems[indexToRemove].ChangeQuantity(currentQuantity - amount);
                            GameManager.instance.itemsToTransfer[indexToRemove] = GameManager.instance.itemsToTransfer[indexToRemove].ChangeQuantity(currentQuantity - amount);
                            InformAboutChange();
                           // Debug.LogError("Item has been removed");
                    }

                    }
                
            }
            catch (Exception)
            {
                // Handle any exceptions here
            }
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
            }
            catch (Exception ex)
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
            public PartsSO item;
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
