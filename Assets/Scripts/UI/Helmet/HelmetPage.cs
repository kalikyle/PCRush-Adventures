using Helmets.UI;
using Swords.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Helmets.UI
{
    public class HelmetPage : MonoBehaviour
    {
        [SerializeField]
        private HelmetItem itemPrefab;
        [SerializeField]
        private RectTransform contentPanel;


        public List<HelmetItem> ListOfHelmetItems = new List<HelmetItem>();
        public HelmetBuy HelmetBuy;
        void Start()
        {

        }
        public void InitializedShop(int inventorysize)
        {
            for (int i = 0; i < inventorysize; i++)
            {
                HelmetItem uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
                uiItem.transform.SetParent(contentPanel);
                uiItem.transform.localScale = new Vector3(1, 1, 1);
                ListOfHelmetItems.Add(uiItem);
                uiItem.SetTemporaryIndex(i);
                uiItem.OnItemClicked += HandleItemSelection;
                //uiItem.OnRightMouseBtnClick += HandleShowItemActions;

            }
        }

        private void HandleItemSelection(HelmetItem item)
        {
            int index = ListOfHelmetItems.IndexOf(item);
            if (index == -1)
            {
                return;
            }
            //OnDescriptionRequested?.Invoke(index);
            DeselectAllItems();
        }

        private void DeselectAllItems()
        {
            foreach (HelmetItem item in ListOfHelmetItems)
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

        public void UpdateData(int itemIndex, Sprite HelmetImage, string HelmetName, string Price, string attack)
        {
            if (ListOfHelmetItems.Count > itemIndex)
            {
                ListOfHelmetItems[itemIndex].SetData(HelmetImage, HelmetName, "$" + Price, attack);//this will add to the shop

            }

        }
        public void ClearItems()
        {
            foreach (var item in ListOfHelmetItems)
            {
                item.gameObject.SetActive(false);// Assuming ListOfShopItems contains the GameObjects of shop items
            }
            ListOfHelmetItems.Clear();
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

            HelmetItem uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
            uiItem.transform.SetParent(contentPanel);
            uiItem.transform.localScale = new Vector3(1, 1, 1);
            ListOfHelmetItems.Add(uiItem);//add shop items in the list
            uiItem.SetData(ItemImage, ItemName, price, perks);

            int itemIndex = HelmetBuy.filteredItems.Count - 1;//this is for the filtered items
            Debug.Log(HelmetBuy.filteredItems.Count);
            uiItem.SetTemporaryIndex(itemIndex);


            uiItem.OnItemClickeds += (tempIndex) =>
            {
                Debug.Log("Item Clicked. tempIndex: " + tempIndex);
                if (HelmetBuy != null)
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
