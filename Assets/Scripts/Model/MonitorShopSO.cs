using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.Progress;

namespace Shop.Model
{
    [CreateAssetMenu]
    public class MonitorShopSO : ScriptableObject
    {
        [SerializeField]
        public List<MonitorShopItem> ShopItems;

        public MonitorShopItem GetItemAt(int obj)
        {
            return ShopItems[obj];
        }

        public Dictionary<int, MonitorShopItem> GetCurrentInventoryState()
        {
            Dictionary<int, MonitorShopItem> returnValue = new Dictionary<int, MonitorShopItem>();
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
        public List<MonitorShopItem> GetItemsByCategory(string category)
        {
            return ShopItems.Where(item => !item.isEmpty && item.item.Category.Equals(category)).ToList();
        }
    }
    [Serializable]
    public struct MonitorShopItem
    {

        public MonitorShopItemSO item;
        public bool isEmpty => item == null;

        public static MonitorShopItem GetEmptyItem() => new MonitorShopItem
        {
            item = null,
        };
    }
}
