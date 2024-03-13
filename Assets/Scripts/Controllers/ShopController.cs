using Shop.Model;
using Shop.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Shop
{
    public class ShopController : MonoBehaviour
    {
       [SerializeField]
       private ShopPage shop;

       [SerializeField]
       private ShopSO shopData;

        [SerializeField]
        private ShopBuy shopBuy;

        [SerializeField]
        private TMP_Dropdown CategoryDropdown;

        public Dictionary<int, int> tempToOriginalIndexMapping = new Dictionary<int, int>();
        private List<Shop.Model.ShopItem> itemsShownInAllCategory = new List<Shop.Model.ShopItem>();


        // Start is called before the first frame update
        void Start()
        {
            PrepareUI();//initial size of the shop\
                        //shopData.Initialize();
            shopBuy.ToggleTF = false;
            ShowAllCategory();
            shop.Show();
            
        }

        // Update is called once per frame
        void Update()
        {

        }
        public void ToggleALLButton()
        {
            shopBuy.ToggleTF = false;
            // Toggle the state
            shop.ResetSelection();
            shop.ClearItems();
            shop.InitializedShop(GetUsedSlotsCount() /* GameManager.Instance.shopSize*/);

            CategoryDropdown.value = 0;

            ShowAllCategory();

        }
        public int GetUsedSlotsCount()//this will only used the slots with items
        {
            int usedSlots = 0;
            foreach (var item in shopData.ShopItems)
            {
                if (!item.isEmpty)
                {
                    usedSlots++;
                }
            }
            return usedSlots;
        }
        public void ShowAllCategory()
        {
            itemsShownInAllCategory.Clear();

            var nonEmptyItems = shopData.GetCurrentInventoryState().Where(item => !item.Value.isEmpty);

            int displayedItemsCount = 0;
            foreach (var item in nonEmptyItems)
            {
                if (displayedItemsCount >= GameManager.instance.ShopSize)
                    break;

                shop.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.item.Name, item.Value.item.Price.ToString(), item.Value.item.Category);
                itemsShownInAllCategory.Add(item.Value); // Add to items shown in "All" category
                displayedItemsCount++;
            }
        }
        private void PrepareUI()
        {
            shop.InitializedShop(GetUsedSlotsCount());
                
                
        }
        public List<Shop.Model.ShopItem> itemsToShow;
        private string currentCategory = "";
        public void ShowCategory(string category)
        {
            shopBuy.ToggleTF = true;
            currentCategory = category;
            shop.ResetSelection();
            shopBuy.filteredItems.Clear();
            tempToOriginalIndexMapping.Clear();
            shop.ClearItems();// Clear the existing items in the UI

            itemsToShow = shopData.GetItemsByCategory(currentCategory);//all by category

            int displayedItemsCount = 0;
            foreach (var item in itemsToShow)
            {
                if (displayedItemsCount >= GameManager.instance.ShopSize)
                    break;

                if (itemsShownInAllCategory.Contains(item)) // Check if the item is in the "All" category
                {
                    shopBuy.filteredItems.Add(item);//then add to filteredItems
                    tempToOriginalIndexMapping[displayedItemsCount] = displayedItemsCount;

                    shop.AddShopItem(item.item.ItemImage, item.item.Name, item.item.Price.ToString(), item.item.Category);
                    displayedItemsCount++;
                }
            }
        }
    }
}
