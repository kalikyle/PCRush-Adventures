using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
namespace Exchanger.UI.MBWorld
{
    public class MBWorldExchangerPage : MonoBehaviour
    {
        [SerializeField]
        private MBWorldExchangerItem itemPrefab;

        [SerializeField]
        private RectTransform contentPanel;

        public TMP_Text Time;


        public List<MBWorldExchangerItem> ListOfMBItems = new List<MBWorldExchangerItem>();

        void Start()
        {

        }
        public void UpdateTimer(string timeText)
        {
            Time.text = timeText;
        }

        public void InitializedMB(int inventorysize)
        {
            for (int i = 0; i < inventorysize; i++)
            {
                MBWorldExchangerItem uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);

                uiItem.transform.SetParent(contentPanel);
                uiItem.transform.localScale = new Vector3(1, 1, 1);
                ListOfMBItems.Add(uiItem);

                uiItem.SetTemporaryIndex(i);
                uiItem.OnItemClicked += HandleItemSelection;

            }
        }

        private IEnumerator SpawnMissions(int numberOfMissions)
        {
            for (int i = 0; i < numberOfMissions; i++)
            {
                MBWorldExchangerItem uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
                uiItem.transform.SetParent(contentPanel);
                uiItem.transform.localScale = new Vector3(1, 1, 1);
                ListOfMBItems.Add(uiItem);
                uiItem.SetTemporaryIndex(i);
                uiItem.OnItemClicked += HandleItemSelection;
                //buyItem.OnItemClicked += HandleItemSelectionBuy;
                yield return new WaitForSeconds(3f); // Wait for 5 seconds before spawning the next mission
            }
        }

        public bool IsMBVisible(int missionId)
        {
            if (missionId >= 0 && missionId < ListOfMBItems.Count)
            {
                // Assuming the mission UI object is active or visible based on its game object's active status
                return ListOfMBItems[missionId].gameObject.activeSelf;
            }
            return false;
        }


        private void HandleItemSelection(MBWorldExchangerItem item)
        {
            int index = ListOfMBItems.IndexOf(item);
            if (index == -1)
            {
                return;
            }
            //OnDescriptionRequested?.Invoke(index);
            DeselectAllItems();

        }

        private void DeselectAllItems()
        {
            foreach (MBWorldExchangerItem item in ListOfMBItems)
            {
                item.DeSelect();

            }
        }

        private void ResetQuantity()
        {
            foreach (MBWorldExchangerItem item in ListOfMBItems)
            {
                item.ResetQuantity();


            }
        }


        public void ResetSelection()
        {

            DeselectAllItems();
            ResetQuantity();

        }

        public void UpdateData(int itemIndex, Sprite MBImage, Sprite MaterialNeed, string MBName, string rarity, int Material, string stats)
        {
            if (ListOfMBItems.Count > itemIndex)
            {
                ListOfMBItems[itemIndex].SetData(MBImage, MaterialNeed, MBName, rarity, Material.ToString(), stats);

            }

        }
        public void ClearItems()
        {
            foreach (var item in ListOfMBItems)
            {
                item.gameObject.SetActive(false);// Assuming ListOfShopItems contains the GameObjects of shop items
            }
            ListOfMBItems.Clear();
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