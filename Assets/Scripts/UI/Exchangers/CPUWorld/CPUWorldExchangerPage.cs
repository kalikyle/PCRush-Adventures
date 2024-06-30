using Swords.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Exchanger.UI.CPUWorld
{


    public class CPUWorldExchangerPage : MonoBehaviour
    {
        [SerializeField]
        private CPUWorldExchangerItem itemPrefab;
        [SerializeField]
        private RectTransform contentPanel;


        public List<CPUWorldExchangerItem> ListOfSwordsItems = new List<CPUWorldExchangerItem>();
        void Start()
        {

        }
        public void InitializedShop(int inventorysize)
        {
            for (int i = 0; i < inventorysize; i++)
            {
                CPUWorldExchangerItem uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
                uiItem.transform.SetParent(contentPanel);
                uiItem.transform.localScale = new Vector3(1, 1, 1);
                ListOfSwordsItems.Add(uiItem);
                uiItem.SetTemporaryIndex(i);
                uiItem.OnItemClicked += HandleItemSelection;
                //uiItem.OnRightMouseBtnClick += HandleShowItemActions;

            }
        }

        private void HandleItemSelection(CPUWorldExchangerItem item)
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
            foreach (CPUWorldExchangerItem item in ListOfSwordsItems)
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

        public void UpdateData(int itemIndex, Sprite CPUImage, Sprite MaterialNeed, string CPUName, int Material, double stats)
        {
            if (ListOfSwordsItems.Count > itemIndex)
            {
                ListOfSwordsItems[itemIndex].SetData(CPUImage, MaterialNeed, CPUName, Material.ToString(), stats.ToString());//this will add to the shop

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
