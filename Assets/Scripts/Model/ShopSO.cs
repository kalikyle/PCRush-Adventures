using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.Progress;

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
