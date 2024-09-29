using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Shop.UI;
using System.Linq;
using PartsInventory.Model;

namespace Inventory.Model
{
    [CreateAssetMenu]
    public class ShopSO2 : ScriptableObject//how many of the items
    {
        
        [SerializeField]
        public List<ShopItem2> ShopItem2s;

        public ShopItem2 GetItemAt(int obj)
        {
            return ShopItem2s[obj];
        }
        
        public Dictionary<int, ShopItem2> GetCurrentInventoryState()
        {
            Dictionary<int, ShopItem2> returnValue = new Dictionary<int, ShopItem2>();
            for (int i = 0; i < ShopItem2s.Count; i++)
            {
                if (ShopItem2s[i].isEmpty)
                {
                    continue;
                }
                returnValue[i] = ShopItem2s[i];
            }
            return returnValue;
        }
        //this is for the filtering, getting the category of the item
        public List<ShopItem2> GetItemsByCategory(string category)
        {
            return ShopItem2s.Where(item => !item.isEmpty && item.item.Category.Equals(category)).ToList();
        }

    }


    [Serializable]
    public struct ShopItem2
    {

        public PartsSO item;
        public bool isEmpty => item == null;

        public static ShopItem2 GetEmptyItem() => new ShopItem2
        {
            item = null,
        };
    }
}
