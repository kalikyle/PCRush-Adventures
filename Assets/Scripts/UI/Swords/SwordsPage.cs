using OtherWorld.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Swords.UI
{
    public class SwordsPage : MonoBehaviour
    {
        [SerializeField]
        private SwordsItem itemPrefab;
        [SerializeField]
        private RectTransform contentPanel;
        public List<SwordsItem> ListOfSwordsItems = new List<SwordsItem>();
        public SwordBuy SwordBuy;
        void Start()
        {

        }
        public void InitializedShop(int inventorysize)
        {
            for (int i = 0; i < inventorysize; i++)
            {
                SwordsItem uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
                uiItem.transform.SetParent(contentPanel);
                uiItem.transform.localScale = new Vector3(1, 1, 1);
                ListOfSwordsItems.Add(uiItem);
                uiItem.SetTemporaryIndex(i);
                uiItem.OnItemClicked += HandleItemSelection;
                //uiItem.OnRightMouseBtnClick += HandleShowItemActions;

            }
        }

        private void HandleItemSelection(SwordsItem item)
        {
            int index = ListOfSwordsItems.IndexOf(item);
            if (index == -1)
            {
                return;
            }
            //OnDescriptionRequested?.Invoke(index);
            DeselectAllItems();
        }

        private void DeselectAllItems()
        {
            foreach (SwordsItem item in ListOfSwordsItems)
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

        public void UpdateData(int itemIndex, Sprite SwordImage, string SwordName, string Price, string attack)
        {
            if (ListOfSwordsItems.Count > itemIndex)
            {
                ListOfSwordsItems[itemIndex].SetData(SwordImage, SwordName, "$" + Price, attack);//this will add to the shop
            }

        }
        public void ClearItems()
        {
            foreach (var item in ListOfSwordsItems)
            {
                item.gameObject.SetActive(false);// Assuming ListOfShopItems contains the GameObjects of shop items
            }
            ListOfSwordsItems.Clear();
        }
        public void Show()//show are looping in the controller using update method
        {
            gameObject.SetActive(true);

        }
        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void AddShopItem(Sprite ItemImage, string ItemName, string price, string perks)
        {

            SwordsItem uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
            uiItem.transform.SetParent(contentPanel);
            uiItem.transform.localScale = new Vector3(1, 1, 1);
            ListOfSwordsItems.Add(uiItem);//add shop items in the list
            uiItem.SetData(ItemImage, ItemName, price, perks);

            int itemIndex = SwordBuy.filteredItems.Count - 1;//this is for the filtered items
            Debug.Log(SwordBuy.filteredItems.Count);
            uiItem.SetTemporaryIndex(itemIndex);


            uiItem.OnItemClickeds += (tempIndex) =>
            {
                Debug.Log("Item Clicked. tempIndex: " + tempIndex);
                if (SwordBuy != null)
                {
                    DeselectAllItems();
                }
                else
                {
                    Debug.Log("shopC is not assigned.");
                }
            };
            

        }
    }
}
