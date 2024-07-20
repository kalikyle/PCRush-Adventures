using Helmets.UI;
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
    }
}
