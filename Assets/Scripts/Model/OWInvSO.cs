using PartsInventory.Model;
using PC.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Decoration.Model.DecorSO;


namespace OtherWorld.Model
{
    [CreateAssetMenu]
    public class OWInvSO : ScriptableObject
    {
        [SerializeField]
        public List<OtherWorldItem> OWinventoryItems;
        public event Action<Dictionary<int, OtherWorldItem>> OnInventoryUpdated;
        [field: SerializeField]
        public int Size { get; private set; }//this will add a size field in unity//sets to 999


        public void Initialize()
        {

            OWinventoryItems = new List<OtherWorldItem>();
            for (int i = 0; i < Size; i++)
            {
                OWinventoryItems.Add(OtherWorldItem.GetEmptyItem());
            }
        }



        public async void OWSaveItems(OtherWorldItemSO items, int quantity)
        {
           await GameManager.instance.SaveOWItems(items, quantity);
            //SaveInitialItems(DecorationItems);
        }

        public void AddItem(OtherWorldItem item)
        {
            AddItem(item.item, item.quantity);
        }

        public void AddItem(OtherWorldItemSO item, int quantity)
        {
            if (item.IsStackable == false)
            {
                for (int i = 0; i < OWinventoryItems.Count;)//i++
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


        private int AddItemToFirstFreeSlot(OtherWorldItemSO item, int quantity)
        {
            OtherWorldItem newItem = new OtherWorldItem
            {
                item = item,
                quantity = quantity
            };

            for (int i = 0; i < OWinventoryItems.Count; i++)
            {
                if (OWinventoryItems[i].isEmpty)
                {
                    OWinventoryItems[i] = newItem;
                    return quantity;
                }
            }
            return 0;
        }


        private bool IsInventoryFull() => OWinventoryItems.Where(item => item.isEmpty).Any() == false;

        public int AddStackebleItem(OtherWorldItemSO item, int quantity)
        {

            for (int i = 0; i < OWinventoryItems.Count; i++)
            {
                if (OWinventoryItems[i].isEmpty) { continue; }
                if (OWinventoryItems[i].item.ID == item.ID)
                {

                    int amountPossibleToTake = OWinventoryItems[i].item.MaxStackableSize - OWinventoryItems[i].quantity;

                    if (quantity > amountPossibleToTake)
                    {
                        OWinventoryItems[i] = OWinventoryItems[i].ChangeQuantity(OWinventoryItems[i].item.MaxStackableSize);
                        quantity -= amountPossibleToTake;
                    }
                    else
                    {
                        OWinventoryItems[i] = OWinventoryItems[i].ChangeQuantity(OWinventoryItems[i].quantity + quantity);
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
                if (OWinventoryItems.Count > itemIndex)
                {
                    if (OWinventoryItems[itemIndex].isEmpty)
                    {
                        return;
                    }

                    int currentQuantity = OWinventoryItems[itemIndex].quantity;

                    if (currentQuantity <= amount)
                    {
                        // Remove the entire item.
                        OWinventoryItems.RemoveAt(itemIndex);
                        GameManager.instance.OWitemsToTransfer .RemoveAt(itemIndex);
                        InformAboutChange();

                    }
                    else
                    {
                        // Decrease the item quantity.
                        OWinventoryItems[itemIndex] = OWinventoryItems[itemIndex].ChangeQuantity(currentQuantity - amount);
                        GameManager.instance.OWitemsToTransfer [itemIndex] = GameManager.instance.OWitemsToTransfer [itemIndex].ChangeQuantity(currentQuantity - amount);
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
                // Find the index of the itemToRemove in the OWinventoryItems list
                int indexToRemove = OWinventoryItems.FindIndex(item => item.item.name.Equals(name));
                // Debug.LogError(indexToRemove);

                if (OWinventoryItems.Count > indexToRemove)
                {
                    if (OWinventoryItems[indexToRemove].isEmpty)
                    {
                        return;
                    }

                    int currentQuantity = OWinventoryItems[indexToRemove].quantity;

                    if (currentQuantity <= amount)
                    {
                        // Remove the entire item.
                        OWinventoryItems.RemoveAt(indexToRemove);
                        GameManager.instance.OWitemsToTransfer .RemoveAt(indexToRemove);
                        InformAboutChange();

                    }
                    else
                    {
                        // Decrease the item quantity.
                        OWinventoryItems[indexToRemove] = OWinventoryItems[indexToRemove].ChangeQuantity(currentQuantity - amount);
                        GameManager.instance.OWitemsToTransfer [indexToRemove] = GameManager.instance.OWitemsToTransfer [indexToRemove].ChangeQuantity(currentQuantity - amount);
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

        public Dictionary<int, OtherWorldItem> GetCurrentInventoryState()
        {
            Dictionary<int, OtherWorldItem> returnValue = new Dictionary<int, OtherWorldItem>();
            for (int i = 0; i < OWinventoryItems.Count; i++)
            {
                if (OWinventoryItems[i].isEmpty)
                {
                    continue;
                }
                returnValue[i] = OWinventoryItems[i];
            }
            return returnValue;
        }
        public List<OtherWorldItem> GetItemsByCategory(string category)
        {
            return OWinventoryItems.Where(item => !item.isEmpty && item.item.Category.Equals(category)).ToList();
        }

        public OtherWorldItem GetItemAt(int itemIndex)
        {
            return OWinventoryItems[itemIndex];
        }

        public void SwapItems(int itemIndex1, int itemIndex2)
        {
            try
            {
                OtherWorldItem item1 = OWinventoryItems[itemIndex1];
                OWinventoryItems[itemIndex1] = OWinventoryItems[itemIndex2];
                OWinventoryItems[itemIndex2] = item1;
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
        public void AddItemList(OtherWorldItemSO Item)
        {
            
            OtherWorldItem Items = new OtherWorldItem
            {
                item = Item
            };
            
            OWinventoryItems.Add(Items);


            InformAboutChange();
        }

        [Serializable]
        public struct OtherWorldItem
        {
            public int quantity;
            public OtherWorldItemSO item;
            public bool isEmpty => item == null;

            public OtherWorldItem ChangeQuantity(int newQuantity)
            {
                return new OtherWorldItem
                {
                    item = this.item,
                    quantity = newQuantity,
                };
            }
            public static OtherWorldItem GetEmptyItem() => new OtherWorldItem
            {
                item = null,
                quantity = 0,

            };



        }
    }
}
