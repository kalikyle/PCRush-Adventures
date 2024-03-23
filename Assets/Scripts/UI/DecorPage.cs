using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

namespace Decoration.UI
{


    public class DecorPage : MonoBehaviour
    {
        [SerializeField]
        private DecorItem itemPrefab;
        [SerializeField]
        private RectTransform contentPanel;

        //[SerializeField]
        //private MouseFollower mousefollower;

        public List<DecorItem> ListofItems = new List<DecorItem>();
        public DecorController DC;
        //private int currentlyDraggedItemIndex = -1;

        public event Action<int> OnDescriptionRequested, OnItemActionRequested;
        public event Action<int, int> OnSwapItems;


        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        public void InitializeInventoryUI(int inventSize)
        {

            for (int i = 0; i < inventSize; i++)
            {
                DecorItem uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
                uiItem.transform.SetParent(contentPanel);
                uiItem.transform.localScale = new Vector3(1, 1, 1);
                ListofItems.Add(uiItem);
                uiItem.OnItemClicked += HandleItemSelection;
                uiItem.OnRightMouseBtnClick += HandleShowItemActions;
                //uiItem.OnItemBeginDrag += HandleBeginDrag;
                //uiItem.OnItemDroppedOn += HandleSwap;
                //uiItem.OnItemEndDrag += HandleEndDrag;
                //uiItem.OnRightMouseBtnClick += HandleShowItemActions;
            }

        }
        public void UpdateData(int itemIndex, Sprite ItemImage, int ItemQuantity, string name, string category)
        {
            if (ListofItems.Count > itemIndex)
            {
                ListofItems[itemIndex].SetData(ItemImage, ItemQuantity, name, category);
            }
        }


        private void HandleShowItemActions(DecorItem Item)
        {
            int index = ListofItems.IndexOf(Item);
            if (index == -1)
            {
                return;
            }
            Item.SetTemporaryIndex(index);
            OnItemActionRequested?.Invoke(index);
        }
        private void HandleItemSelection(DecorItem item)
        {
            int index = ListofItems.IndexOf(item);
            if (index == -1)
            {
                return;
            }
            item.SetTemporaryIndex(index);
            OnItemActionRequested?.Invoke(index);
        }
        public void Show()
        {

            gameObject.SetActive(true);
            ResetSelection();
        }

        public void ResetSelection()
        {
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
            foreach (DecorItem item in ListofItems)
            {
                item.DeSelect();
            }
        }

        public void Hide()
        {

            gameObject.SetActive(false);
            //ResetDraggedItem();
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
    }

}