using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Shop.UI
{
    public class ShopPage2 : MonoBehaviour
    {
        [SerializeField]
        private ShopItem2 itemPrefab;
        [SerializeField]
        private RectTransform contentPanel;
        [SerializeField]
        private ShopDesc2 shopDesc;

        public ShopController2 shopC;

        public NumericUpDown NumericUpDown;

        public List<ShopItem2> ListOfShopItem2s = new List<ShopItem2>();

        public event Action<int> OnDescriptionRequested;

       

        /* public Sprite Image;
         public string title, description, price, category;*///served as temporary data

        public void Start()//from the start this statement whill be executed
        {
            //ListOfShopItem2s[0].SetData(Image,title, price, category);//gumagana//adding temporary to the list
            // Attach the click listener to the BuyButton
            
            // Set the initial state of the Buy button
          
        }
        private void Awake()
        {
            shopDesc.Hide();
            shopDesc.ResetDescription();
        }
        public void InitializedShop(int inventorysize)
        {
            for (int i = 0; i < inventorysize; i++)
            {
                ShopItem2 uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
                uiItem.transform.SetParent(contentPanel);
                uiItem.transform.localScale = new Vector2(85, 90);
                ListOfShopItem2s.Add(uiItem);
                uiItem.SetTemporaryIndex(i);
                uiItem.OnItemClicked += HandleItemSelection;
                uiItem.OnRightMouseBtnClick += HandleShowItemActions;
               
            }
        }
        private void DeselectAllItems()
        {
            foreach (ShopItem2 item in ListOfShopItem2s)
            {
                item.DeSelect();
            }
        }
        internal void ResetSelection()
        {
            shopDesc.Hide();
            shopDesc.ResetDescription();
            DeselectAllItems();
        }

        internal void UpdateDescription(int obj, Sprite itemImage, string name, string category, string speed, string compat)
        {
            shopDesc.Show();
            shopDesc.SetDescription(itemImage, name, category, speed, compat);
            DeselectAllItems();
            ListOfShopItem2s[obj].select();

        }

        public void UpdateData(int itemIndex, Sprite ItemImage, Sprite ImageBackground, string Itemtitle, string Itemprice, string Itemcategory, string speed, string compat)
        {
            if (ListOfShopItem2s.Count > itemIndex)
            {
                ListOfShopItem2s[itemIndex].SetData(ItemImage, ImageBackground, Itemtitle, "$" + Itemprice, Itemcategory, speed, compat);//this will add to the shop
               
            }
        }
        private void HandleShowItemActions(ShopItem2 obj)//if right clicked
        {
            
        }

        private void HandleItemSelection(ShopItem2 obj)//if clicked
        {
            //shopDesc.SetDescription(Image, title, description, category, price);
            //ListOfShopItem2s[0].select();
           
                int index = ListOfShopItem2s.IndexOf(obj);
                if (index == -1)
                {
                    return;
                }
            OnDescriptionRequested?.Invoke(index);
            
        }
        

        public void Show()//show are looping in the controller using update method
        {
            gameObject.SetActive(true);
            
        }
        public void Hide()
        {
            gameObject.SetActive(false);
        }
        public void ClearItems()
        {
            foreach (var item in ListOfShopItem2s)
            {
                item.gameObject.SetActive(false);// Assuming ListOfShopItem2s contains the GameObjects of shop items
            }
            ListOfShopItem2s.Clear();
        }


       //then this for adding a new filtered items
        public void AddShopItem2(Sprite itemImage, Sprite ImageBackground, string itemName, string price, string category, string speed, string compat)
        {
           
            ShopItem2 uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
            uiItem.transform.SetParent(contentPanel);
            uiItem.transform.localScale = new Vector2(85, 90);
            ListOfShopItem2s.Add(uiItem);//add shop items in the list
            uiItem.SetData(itemImage, ImageBackground, itemName, "$" + price, category, speed, compat);
            int itemIndex = NumericUpDown.filteredItems.Count - 1;//this is for the filtered items
            Debug.Log(NumericUpDown.filteredItems.Count);
            uiItem.SetTemporaryIndex(itemIndex);

            uiItem.OnItemClickeds += (tempIndex) => {

                Debug.Log("Item Clicked. tempIndex: " + tempIndex);
                if (shopC != null)
                {
                    shopC.HandleItemSelection(tempIndex);
                    
                }
                else
                {
                    Debug.Log("shopC is not assigned.");
                }
            };
           
            /*uiItem.OnItemClicked += (ShopItem2) =>
            {
                HandleItemSelection(ShopItem2);
                // Your code here, using the 'id' parameter
                // This lambda expression takes the 'id' as an int parameter
            };*/


            //need fix
        }
        
    }
}
