using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Shop.UI
{
    public class MonitorShopPage : MonoBehaviour
    {
        [SerializeField]
        private MonitorShopItem itemPrefab;
        [SerializeField]
        private RectTransform contentPanel;

        public MonitorShopController shopC;

        public MonitorBuy monitorBuy;

        public List<MonitorShopItem> ListOfShopItems = new List<MonitorShopItem>();
        void Start()
        {

        }
        public void InitializedShop(int inventorysize)
        {
            for (int i = 0; i < inventorysize; i++)
            {
                MonitorShopItem uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
                uiItem.transform.SetParent(contentPanel);
                uiItem.transform.localScale = new Vector3(1, 1, 1);
                ListOfShopItems.Add(uiItem);
                //uiItem.SetTemporaryIndex(i);
                uiItem.OnItemClicked += HandleItemSelection;
                //uiItem.OnRightMouseBtnClick += HandleShowItemActions;

            }
        }

        // Update is called once per frame
        void Update()
        {

        }
        private void HandleItemSelection(MonitorShopItem obj)//if clicked
        {
            //shopDesc.SetDescription(Image, title, description, category, price);
            //ListOfShopItems[0].select();

            int index = ListOfShopItems.IndexOf(obj);
            if (index == -1)
            {
                return;
            }
            //OnDescriptionRequested?.Invoke(index);
            DeselectAllItems();

        }
        private void DeselectAllItems()
        {
            foreach (MonitorShopItem item in ListOfShopItems)
            {
                item.DeSelect();
            }
        }
        public void UpdateData(int itemIndex, Sprite ItemImage, string Itemtitle, string Itemprice, string Itemcategory)
        {
            if (ListOfShopItems.Count > itemIndex)
            {
                ListOfShopItems[itemIndex].SetData(ItemImage, Itemtitle, "$" + Itemprice, Itemcategory);//this will add to the shop

            }
            
        }
        public void ClearItems()
        {
            foreach (var item in ListOfShopItems)
            {
                item.gameObject.SetActive(false);// Assuming ListOfShopItems contains the GameObjects of shop items
            }
            ListOfShopItems.Clear();
        }
        public void Show()//show are looping in the controller using update method
        {
            gameObject.SetActive(true);

        }
        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
