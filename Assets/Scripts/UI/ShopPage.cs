using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Shop.UI
{
    public class ShopPage : MonoBehaviour
    {
        [SerializeField]
        private ShopItem itemPrefab;
        [SerializeField]
        private RectTransform contentPanel;

        public ShopController shopC;

        public ShopBuy shopBuy;

        public List<ShopItem> ListOfShopItems = new List<ShopItem>();
        void Start()
        {

        }
        public void InitializedShop(int inventorysize)
        {
            for (int i = 0; i < inventorysize; i++)
            {
                ShopItem uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
                uiItem.transform.SetParent(contentPanel);
                uiItem.transform.localScale = new Vector3(1, 1, 1);
                ListOfShopItems.Add(uiItem);
                uiItem.SetTemporaryIndex(i);
                uiItem.OnItemClicked += HandleItemSelection;
                //uiItem.OnRightMouseBtnClick += HandleShowItemActions;

            }
        }

        // Update is called once per frame
        void Update()
        {

        }
        private void HandleItemSelection(ShopItem obj)//if clicked
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
            foreach (ShopItem item in ListOfShopItems)
            {
                item.DeSelect();
            }
        }
        public void ResetSelection()
        {
            //shopDesc.Hide();
            //shopDesc.ResetDescription();
            DeselectAllItems();
        }
        public void UpdateData(int itemIndex, Sprite ItemImage, Sprite CatImage, string Itemtitle, string Itemprice, string Itemcategory, bool inuse, bool sold)
        {
            if (ListOfShopItems.Count > itemIndex)
            {
                ListOfShopItems[itemIndex].SetData(ItemImage, CatImage, Itemtitle, "$" + Itemprice, Itemcategory, inuse, sold);//this will add to the shop

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


        //then this for adding a new filtered items
        public void AddShopItem(Sprite itemImage, Sprite ImageCat, string itemName, string price, string category, bool inuse, bool sold)
        {

            ShopItem uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
            uiItem.transform.SetParent(contentPanel);
            uiItem.transform.localScale = new Vector3(1, 1, 1);

            ListOfShopItems.Add(uiItem);//add shop items in the list
            uiItem.SetData(itemImage, ImageCat, itemName, "$" + price, category, inuse, sold);
            int itemIndex = shopBuy.filteredItems.Count - 1;//this is for the filtered items
            Debug.Log(shopBuy.filteredItems.Count);
            uiItem.SetTemporaryIndex(itemIndex);

            uiItem.OnItemClickeds += (tempIndex) => {

                Debug.Log("Item Clicked. tempIndex: " + tempIndex);
                if (shopC != null)
                {
                    ResetSelection();

                }
                else
                {
                    Debug.Log("shopC is not assigned.");
                }
            };
        }
    }
}
