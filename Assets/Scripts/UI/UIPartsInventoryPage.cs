using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PartsInventory.UI
{
    public class UIPartsInventoryPage : MonoBehaviour
    {

        [SerializeField]
        private UIPartsInventoryItem itemPrefab;
        [SerializeField]
        private RectTransform contentPanel;
        [SerializeField]
        private UIPartsInventoryDesc itemDesc;
        //[SerializeField]
        //private MouseFollower mousefollower;

        public List<UIPartsInventoryItem> ListofItems = new List<UIPartsInventoryItem>();
        public PartsInventoryController IC;
        private int currentlyDraggedItemIndex = -1;

        public event Action<int> OnDescriptionRequested, OnItemActionRequested, OnStartDragging;
        public event Action<int, int> OnSwapItems;
        // Start is called before the first frame update
        private void Awake()
        {
            Hide();//if inventory is disabled, make this code to be comment
            //mousefollower.Toggle(false);
            itemDesc.ResetDescription();


        }
        public void InitializeInventoryUI(int inventSize)
        {

            for (int i = 0; i < inventSize; i++)
            {
                UIPartsInventoryItem uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
                uiItem.transform.SetParent(contentPanel);
                uiItem.transform.localScale = new Vector3(1, 1, 1);
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

        public void UpdateDescription(int itemIndex, Sprite itemImage, string name, string category, string rarity, string perks)
        {
            itemDesc.Show();
            itemDesc.SetDescription(itemImage, name, category, rarity, perks);
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

        private void HandleShowItemActions(UIPartsInventoryItem InventoryItemUI)
        {
            int index = ListofItems.IndexOf(InventoryItemUI);
            if (index == -1)
            {
                return;
            }
            OnItemActionRequested?.Invoke(index);
        }

        private void HandleEndDrag(UIPartsInventoryItem InventoryItemUI)
        {
            ResetDraggedItem();
        }

        private void HandleSwap(UIPartsInventoryItem InventoryItemUI)
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
            //mousefollower.Toggle(false);
            currentlyDraggedItemIndex = -1;
        }

        private void HandleBeginDrag(UIPartsInventoryItem InventoryItemUI)
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

        private void HandleItemSelection(UIPartsInventoryItem InventoryItemUI)
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
            //IC.XButton.gameObject.SetActive(true);
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
                item.gameObject.SetActive(false);// Assuming ListOfShopItems contains the GameObjects of shop items
            }
            ListofItems.Clear();
        }

        private void DeselectAllItems()
        {
            foreach (UIPartsInventoryItem item in ListofItems)
            {
                item.DeSelect();
            }
        }

        public void Hide()
        {
            itemDesc.Hide();
            gameObject.SetActive(false);
            //IC.XButton.gameObject.SetActive(false);
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
        public void AddShopItem(Sprite ItemImage, int ItemQuantity)
        {

            UIPartsInventoryItem uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
            uiItem.transform.SetParent(contentPanel);
            uiItem.transform.localScale = new Vector3(1, 1, 1);
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

        }
    }
}
