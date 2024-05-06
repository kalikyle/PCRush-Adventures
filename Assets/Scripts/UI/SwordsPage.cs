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
    }
}
