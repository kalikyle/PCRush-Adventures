using Shop.Model;
using Shop.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Shop
{
    public class MonitorShopController : MonoBehaviour
    {
       [SerializeField]
       private MonitorShopPage Mshop;

       [SerializeField]
       private MonitorShopSO MshopData;

        [SerializeField]
        private TMP_Dropdown CategoryDropdown;

        public Dictionary<int, int> tempToOriginalIndexMapping = new Dictionary<int, int>();
        private List<Shop.Model.MonitorShopItem> itemsShownInAllCategory = new List<Shop.Model.MonitorShopItem>();


        // Start is called before the first frame update
        void Start()
        {
            PrepareUI();//initial size of the shop\
                        //shopData.Initialize();
            //NumUpDown.ToggleTF = false;
            ShowAllCategory();
            Mshop.Show();
            
        }

        // Update is called once per frame
        void Update()
        {

        }
        private void ShowAllCategory()
        {
            itemsShownInAllCategory.Clear();

            var nonEmptyItems = MshopData.GetCurrentInventoryState().Where(item => !item.Value.isEmpty);

            int displayedItemsCount = 0;
            foreach (var item in nonEmptyItems)
            {
                if (displayedItemsCount >= GameManager.instance.MonitorShopSize)
                    break;

                Mshop.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.item.Name, item.Value.item.Price.ToString(), item.Value.item.Category);
                itemsShownInAllCategory.Add(item.Value); // Add to items shown in "All" category
                displayedItemsCount++;
            }
        }
        private void PrepareUI()
        {
            Mshop.InitializedShop(GameManager.instance.MonitorShopSize);
                
                
        }
    }
}
