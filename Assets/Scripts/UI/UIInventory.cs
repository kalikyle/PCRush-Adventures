using Inventory.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Inventory.UI
{
    public class UIInventory : MonoBehaviour
    {
        [SerializeField]
        private UIInventoryItem itemPrefab;
        [SerializeField]
        private RectTransform contentPanel;
        [SerializeField]
        private UIInventoryDesc itemDesc;
        [SerializeField]
        private MouseFollower mousefollower;

        public List<UIInventoryItem> ListofItems = new List<UIInventoryItem>();
        public InventoryController IC;
        private int currentlyDraggedItemIndex = -1;

        public event Action<int> OnDescriptionRequested, OnItemActionRequested, OnStartDragging;
        public event Action<int, int> OnSwapItems;

        private void Awake()
        {
            Hide();//if inventory is disabled, make this code to be comment
            mousefollower.Toggle(false);
            itemDesc.ResetDescription();


        }
        public void InitializeInventoryUI(int inventSize)
        {

            for (int i = 0; i < inventSize; i++)
            {
                UIInventoryItem uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
                uiItem.transform.SetParent(contentPanel);
                uiItem.transform.localScale = new Vector3(100, 100, 100);
                ListofItems.Add(uiItem);
                uiItem.OnItemClicked += HandleItemSelection;
                uiItem.OnItemBeginDrag += HandleBeginDrag;
                uiItem.OnItemDroppedOn += HandleSwap;
                uiItem.OnItemEndDrag += HandleEndDrag;
                uiItem.OnRightMouseBtnClick += HandleShowItemActions;
            }

        }

        /*internal void ResetAllItems()
        {
            foreach (var item in ListofItems)
            {
                item.ResetData();
                item.DeSelect();
            }
        }*/

        public void UpdateDescription(int itemIndex, Sprite itemImage, string name, string Compat, string speed, string category)
        {
            itemDesc.Show();
            itemDesc.SetDescription(itemImage, name, Compat, speed, category);
            DeselectAllItems();
            ListofItems[itemIndex].select();
            
        }

        public void UpdateData(int itemIndex, Sprite ItemImage, int ItemQuantity)
        {
            if (ListofItems.Count > itemIndex)
            {
                ListofItems[itemIndex].SetData(ItemImage, ItemQuantity);
            }
        }

        private void HandleShowItemActions(UIInventoryItem InventoryItemUI)
        {
            int index = ListofItems.IndexOf(InventoryItemUI);
            if (index == -1)
            {
                return;
            }
            OnItemActionRequested?.Invoke(index);
        }

        private void HandleEndDrag(UIInventoryItem InventoryItemUI)
        {
            ResetDraggedItem();
        }

        private void HandleSwap(UIInventoryItem InventoryItemUI)
        {
            int index = ListofItems.IndexOf(InventoryItemUI);
            if (index == -1)
            {
                return;
            }
            OnSwapItems?.Invoke(currentlyDraggedItemIndex, index);
            HandleItemSelection(InventoryItemUI);
        }

        private void ResetDraggedItem()
        {
            mousefollower.Toggle(false);
            currentlyDraggedItemIndex = -1;
        }

        private void HandleBeginDrag(UIInventoryItem InventoryItemUI)
        {
            int index = ListofItems.IndexOf(InventoryItemUI);
            if (index == -1)
            {
                return;
            }
            currentlyDraggedItemIndex = index;
            HandleItemSelection(InventoryItemUI);
            OnStartDragging?.Invoke(index);

        }

        //public void CreateDraggedItem(Sprite sprite, int quantity)
        //{
        //    mousefollower.Toggle(true);
        //    mousefollower.SetData(sprite, quantity);
        //}

        private void HandleItemSelection(UIInventoryItem InventoryItemUI)
        {
            int index = ListofItems.IndexOf(InventoryItemUI);
            if (index == -1)
            {
                return;
            }
            InventoryItemUI.SetTemporaryIndex(index);
            OnDescriptionRequested?.Invoke(index);  
            
        }

        public void Show()
        {

            gameObject.SetActive(true);
            itemDesc.ResetDescription();
            IC.XButton.gameObject.SetActive(true);
            ResetSelection();
        }

        public void ResetSelection()
        {
            itemDesc.Hide();
            itemDesc.ResetDescription();
            DeselectAllItems();
        }
        public void ClearItems()
        {
            foreach (var item in ListofItems)
            {
                item.gameObject.SetActive(false);// Assuming ListOfShopItem2s contains the GameObjects of shop items
            }
            ListofItems.Clear();
        }

        private void DeselectAllItems()
        {
            foreach (UIInventoryItem item in ListofItems)
            {
                item.DeSelect();
            }
        }

        public void Hide()
        {
            itemDesc.Hide();
            gameObject.SetActive(false);
            IC.XButton.gameObject.SetActive(false);
            ResetDraggedItem();
        }
        public void SelectItemAtIndex(int selectedIndex)
        {
            if (selectedIndex >= 0 && selectedIndex < ListofItems.Count)
            {
                // Deselect all items first
                ResetSelection();
                DeselectAllItems();

                // Select the item at the specified index
                ListofItems[selectedIndex].select();
                //itemDesc.ResetDescription();
                //OnDescriptionRequested?.Invoke(selectedIndex);
                
               
            }
            else
            {
                Debug.LogError("Invalid index provided for selection.");
            }
           
        }
        public void AddShopItem2(Sprite ItemImage, int ItemQuantity)
        {

            UIInventoryItem uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
            uiItem.transform.SetParent(contentPanel);
            uiItem.transform.localScale = new Vector3(100, 100, 100);
            ListofItems.Add(uiItem);//add shop items in the list
            uiItem.SetData(ItemImage, ItemQuantity);

            int itemIndex = IC.InventoryfilteredItems.Count - 1;//this is for the filtered items
            Debug.Log(IC.InventoryfilteredItems.Count);
            uiItem.SetTemporaryIndex(itemIndex);


            uiItem.OnItemClickeds += (tempIndex) => {
                uiItem.SetTemporaryIndex(tempIndex);
                Debug.Log("Item Clicked. tempIndex: " + tempIndex);
                if (IC != null)
                {
                   
                    IC.HandleItemSelection(tempIndex);
                    


                }
                else
                {
                    Debug.Log("shopC is not assigned.");
                }
            };
            uiItem.OnItemRightClicked += (tempIndex) => {

                Debug.Log("Item Clicked. tempIndex: " + tempIndex);
                if (IC != null)
                {
                    IC.HandleItemRightActionRequest(tempIndex);
                    

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