using Shop.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.Progress;

namespace Decoration.Model
{
    [CreateAssetMenu]
    public class DecorSO : ScriptableObject
    {
        [SerializeField]
        public List<DecorationItem> DecorationItems;
        public event Action<Dictionary<int, DecorationItem>> OnInventoryUpdated;
        [field: SerializeField]
        public int Size { get; private set; }//this will add a size field in unity//sets to 999


        public void Initialize()
        {

            DecorationItems = new List<DecorationItem>();
            for (int i = 0; i < Size; i++)
            {
                DecorationItems.Add(DecorationItem.GetEmptyItem());
            }
        }

        public void SaveItems()
        {
            GameManager.instance.SaveDecorInitialItems(DecorationItems);
            //SaveInitialItems(DecorationItems);
        }

        public void AddItem(DecorationItem item)
        {
            AddItem(item.item, item.quantity);
        }

        public void AddItem(ShopItemSO item, int quantity)
        {
            if (item.IsStackable == false)
            {
                for (int i = 0; i < DecorationItems.Count;)//i++
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


        private int AddItemToFirstFreeSlot(ShopItemSO item, int quantity)
        {
            DecorationItem newItem = new DecorationItem
            {
                item = item,
                quantity = quantity
            };

            for (int i = 0; i < DecorationItems.Count; i++)
            {
                if (DecorationItems[i].isEmpty)
                {
                    DecorationItems[i] = newItem;
                    return quantity;
                }
            }
            return 0;
        }


        private bool IsInventoryFull() => DecorationItems.Where(item => item.isEmpty).Any() == false;

        public int AddStackebleItem(ShopItemSO item, int quantity)
        {

            for (int i = 0; i < DecorationItems.Count; i++)
            {
                if (DecorationItems[i].isEmpty) { continue; }
                if (DecorationItems[i].item.ID == item.ID)
                {

                    int amountPossibleToTake = DecorationItems[i].item.MaxStackableSize - DecorationItems[i].quantity;

                    if (quantity > amountPossibleToTake)
                    {
                        DecorationItems[i] = DecorationItems[i].ChangeQuantity(DecorationItems[i].item.MaxStackableSize);
                        quantity -= amountPossibleToTake;
                    }
                    else
                    {
                        DecorationItems[i] = DecorationItems[i].ChangeQuantity(DecorationItems[i].quantity + quantity);
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
                if (DecorationItems.Count > itemIndex)
                {
                    if (DecorationItems[itemIndex].isEmpty)
                    {
                        return;
                    }

                    int currentQuantity = DecorationItems[itemIndex].quantity;

                    if (currentQuantity <= amount)
                    {
                        // Remove the entire item.
                        DecorationItems.RemoveAt(itemIndex);
                       GameManager.instance.DecorToTransfer.RemoveAt(itemIndex);
                        InformAboutChange();

                    }
                    else
                    {
                        // Decrease the item quantity.
                        DecorationItems[itemIndex] = DecorationItems[itemIndex].ChangeQuantity(currentQuantity - amount);
                       GameManager.instance.DecorToTransfer[itemIndex] = GameManager.instance.DecorToTransfer[itemIndex].ChangeQuantity(currentQuantity - amount);
                        InformAboutChange();
                    }

                }
            }
            catch (Exception) { }

        }

        public Dictionary<int, DecorationItem> GetCurrentInventoryState()
        {
            Dictionary<int, DecorationItem> returnValue = new Dictionary<int, DecorationItem>();
            for (int i = 0; i < DecorationItems.Count; i++)
            {
                if (DecorationItems[i].isEmpty)
                {
                    continue;
                }
                returnValue[i] = DecorationItems[i];
            }
            return returnValue;
        }
        public List<DecorationItem> GetItemsByCategory(string category)
        {
            return DecorationItems.Where(item => !item.isEmpty && item.item.Category.Equals(category)).ToList();
        }

        public DecorationItem GetItemAt(int itemIndex)
        {
            return DecorationItems[itemIndex];
        }

        public void SwapItems(int itemIndex1, int itemIndex2)
        {
            try
            {
                DecorationItem item1 = DecorationItems[itemIndex1];
                DecorationItems[itemIndex1] = DecorationItems[itemIndex2];
                DecorationItems[itemIndex2] = item1;
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
        public struct DecorationItem
        {
            public int quantity;
            public ShopItemSO item;
            public bool isEmpty => item == null;

            public DecorationItem ChangeQuantity(int newQuantity)
            {
                return new DecorationItem
                {
                    item = this.item,
                    quantity = newQuantity,
                };
            }
            public static DecorationItem GetEmptyItem() => new DecorationItem
            {
                item = null,
                quantity = 0,

            };



        }


    }
}

