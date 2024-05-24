using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
//

namespace Shop.Model
{
    [CreateAssetMenu]
    public class ShopSO : ScriptableObject
    {
        [SerializeField]
        public List<ShopItem> ShopItems;

       

        public ShopItem GetItemAt(int obj)
        {
            return ShopItems[obj];
        }

        public Dictionary<int, ShopItem> GetCurrentInventoryState()
        {
            Dictionary<int, ShopItem> returnValue = new Dictionary<int, ShopItem>();
            for (int i = 0; i < ShopItems.Count; i++)
            {
                if (ShopItems[i].isEmpty)
                {
                    continue;
                }
                returnValue[i] = ShopItems[i];
            }
            return returnValue;
        }

        //this is for the filtering, getting the category of the item
        public List<ShopItem> GetItemsByCategory(string category)
        {
            return ShopItems.Where(item => !item.isEmpty && item.item.Category.Equals(category)).ToList();
        }

        public List<ShopItem> GetItemsSoldAndInUse(string category)
        {
            return ShopItems.Where(item => !item.isEmpty && item.item.Category.Equals(category) && item.item.Sold && item.item.InUse).ToList();
        }
        public List<ShopItem> GetItemsSoldAndNotInUse(string category)
        {
            return ShopItems.Where(item => !item.isEmpty && item.item.Category.Equals(category) && item.item.Sold && !item.item.InUse).ToList();
        }
        public List<ShopItem> GetItemsNotSoldAndNotInUse(string category)
        {
            return ShopItems.Where(item => !item.isEmpty && item.item.Category.Equals(category) && !item.item.Sold && !item.item.InUse).ToList();
        }

        public List<ShopItem> GetItemsSoldAndInUse()
        {
            return ShopItems.Where(item => !item.isEmpty && item.item.Sold && item.item.InUse).ToList();
        }

        public List<ShopItem> GetItemsSoldAndNotInUse()
        {
            return ShopItems.Where(item => !item.isEmpty && item.item.Sold && !item.item.InUse).ToList();
        }

        public List<ShopItem> GetItemsNotSoldAndNotInUse()
        {
            return ShopItems.Where(item => !item.isEmpty && !item.item.Sold && !item.item.InUse).ToList();
        }
        public ShopItem FindLocalItem(string category, string name)
        {
            return ShopItems.FirstOrDefault(item => !item.isEmpty && item.item.Category.Equals(category) && item.item.Name.Equals(name));
        }
        public void ReplaceItem(ShopItem newItem)
        {
            // Find the index of the existing item with the same category and name
            int index = ShopItems.FindIndex(item =>
                !item.isEmpty && item.item.Category.Equals(newItem.item.Category) && item.item.Name.Equals(newItem.item.Name));

            // If the item is found, replace it with the new item
            if (index != -1)
            {
                // Create a new ShopItem object with the new ShopItemSO
                ShopItem updatedItem = new ShopItem { item = newItem.item };

                // Replace the existing item with the new item
                ShopItems[index] = updatedItem;
            }
            else
            {
                Debug.LogWarning("Item not found in ShopSO: " + newItem.item.Name);
            }
        }
    }
    [Serializable]
    public struct ShopItem
    {

        public ShopItemSO item;
        public bool isEmpty => item == null;

        public static ShopItem GetEmptyItem() => new ShopItem
        {
            item = null,
        };
    }
}
