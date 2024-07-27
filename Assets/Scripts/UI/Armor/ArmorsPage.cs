using Swords.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Armor.UI
{
    public class ArmorsPage : MonoBehaviour
    {
        [SerializeField]
        private ArmorsItem itemPrefab;
        [SerializeField]
        private RectTransform contentPanel;


        public List<ArmorsItem> ListOfArmorsItems = new List<ArmorsItem>();

        public ArmorBuy ArmorBuy;
        void Start()
        {

        }
        public void InitializedShop(int inventorysize)
        {
            for (int i = 0; i < inventorysize; i++)
            {
                ArmorsItem uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
                uiItem.transform.SetParent(contentPanel);
                uiItem.transform.localScale = new Vector3(1, 1, 1);
                ListOfArmorsItems.Add(uiItem);
                uiItem.SetTemporaryIndex(i);
                uiItem.OnItemClicked += HandleItemSelection;
                //uiItem.OnRightMouseBtnClick += HandleShowItemActions;

            }
        }

        private void HandleItemSelection(ArmorsItem item)
        {
            int index = ListOfArmorsItems.IndexOf(item);
            if (index == -1)
            {
                return;
            }
            //OnDescriptionRequested?.Invoke(index);
            DeselectAllItems();
        }

        private void DeselectAllItems()
        {
            foreach (ArmorsItem item in ListOfArmorsItems)
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

        public void UpdateData(int itemIndex, Sprite ArmorImage, string ArmorName, string Price, string armor)
        {
            if (ListOfArmorsItems.Count > itemIndex)
            {
                ListOfArmorsItems[itemIndex].SetData(ArmorImage, ArmorName, "$" + Price, armor);//this will add to the shop

            }

        }
        public void ClearItems()
        {
            foreach (var item in ListOfArmorsItems)
            {
                item.gameObject.SetActive(false);// Assuming ListOfShopItems contains the GameObjects of shop items
            }
            ListOfArmorsItems.Clear();
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

            ArmorsItem uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
            uiItem.transform.SetParent(contentPanel);
            uiItem.transform.localScale = new Vector3(1, 1, 1);
            ListOfArmorsItems.Add(uiItem);//add shop items in the list
            uiItem.SetData(ItemImage, ItemName, price, perks);

            int itemIndex = ArmorBuy.filteredItems.Count - 1;//this is for the filtered items
            Debug.Log(ArmorBuy.filteredItems.Count);
            uiItem.SetTemporaryIndex(itemIndex);


            uiItem.OnItemClickeds += (tempIndex) =>
            {
                Debug.Log("Item Clicked. tempIndex: " + tempIndex);
                if (ArmorBuy != null)
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

