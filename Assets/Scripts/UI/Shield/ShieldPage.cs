using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Shield.UI
{
    public class ShieldPage : MonoBehaviour
    {
        [SerializeField]
        private ShieldItem itemPrefab;
        [SerializeField]
        private RectTransform contentPanel;


        public List<ShieldItem> ListOfShieldItems = new List<ShieldItem>();
        void Start()
        {

        }
        public void InitializedShop(int inventorysize)
        {
            for (int i = 0; i < inventorysize; i++)
            {
                ShieldItem uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
                uiItem.transform.SetParent(contentPanel);
                uiItem.transform.localScale = new Vector3(1, 1, 1);
                ListOfShieldItems.Add(uiItem);
                uiItem.SetTemporaryIndex(i);
                uiItem.OnItemClicked += HandleItemSelection;
                //uiItem.OnRightMouseBtnClick += HandleShowItemActions;

            }
        }

        private void HandleItemSelection(ShieldItem item)
        {
            int index = ListOfShieldItems.IndexOf(item);
            if (index == -1)
            {
                return;
            }
            //OnDescriptionRequested?.Invoke(index);
            DeselectAllItems();
        }

        private void DeselectAllItems()
        {
            foreach (ShieldItem item in ListOfShieldItems)
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

        public void UpdateData(int itemIndex, Sprite ShieldImage, string ShieldName, string Price, string attack)
        {
            if (ListOfShieldItems.Count > itemIndex)
            {
                ListOfShieldItems[itemIndex].SetData(ShieldImage, ShieldName, "$" + Price, attack);//this will add to the shop

            }

        }
        public void ClearItems()
        {
            foreach (var item in ListOfShieldItems)
            {
                item.gameObject.SetActive(false);// Assuming ListOfShopItems contains the GameObjects of shop items
            }
            ListOfShieldItems.Clear();
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

