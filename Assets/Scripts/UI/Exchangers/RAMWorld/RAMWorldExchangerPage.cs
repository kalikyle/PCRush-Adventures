using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Exchanger.UI.RAMWorld
{
    public class RAMWorldExchangerPage : MonoBehaviour
    {
        [SerializeField]
        private RAMWorldExchangerItem itemPrefab;

        [SerializeField]
        private RectTransform contentPanel;

        public TMP_Text Time;


        public List<RAMWorldExchangerItem> ListOfRAMItems = new List<RAMWorldExchangerItem>();

        void Start()
        {

        }
        public void UpdateTimer(string timeText)
        {
            Time.text = timeText;
        }

        public void InitializedRAM(int inventorysize)
        {
            for (int i = 0; i < inventorysize; i++)
            {
                RAMWorldExchangerItem uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);

                uiItem.transform.SetParent(contentPanel);
                uiItem.transform.localScale = new Vector3(1, 1, 1);
                ListOfRAMItems.Add(uiItem);

                uiItem.SetTemporaryIndex(i);
                uiItem.OnItemClicked += HandleItemSelection;

            }
        }

        private IEnumerator SpawnMissions(int numberOfMissions)
        {
            for (int i = 0; i < numberOfMissions; i++)
            {
                RAMWorldExchangerItem uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
                uiItem.transform.SetParent(contentPanel);
                uiItem.transform.localScale = new Vector3(1, 1, 1);
                ListOfRAMItems.Add(uiItem);
                uiItem.SetTemporaryIndex(i);
                uiItem.OnItemClicked += HandleItemSelection;
                //buyItem.OnItemClicked += HandleItemSelectionBuy;
                yield return new WaitForSeconds(3f); // Wait for 5 seconds before spawning the next mission
            }
        }

        public bool IsRAMVisible(int missionId)
        {
            if (missionId >= 0 && missionId < ListOfRAMItems.Count)
            {
                // Assuming the mission UI object is active or visible based on its game object's active status
                return ListOfRAMItems[missionId].gameObject.activeSelf;
            }
            return false;
        }


        private void HandleItemSelection(RAMWorldExchangerItem item)
        {
            int index = ListOfRAMItems.IndexOf(item);
            if (index == -1)
            {
                return;
            }
            //OnDescriptionRequested?.Invoke(index);
            DeselectAllItems();

        }

        private void DeselectAllItems()
        {
            foreach (RAMWorldExchangerItem item in ListOfRAMItems)
            {
                item.DeSelect();

            }
        }

        private void ResetQuantity()
        {
            foreach (RAMWorldExchangerItem item in ListOfRAMItems)
            {
                item.ResetQuantity();


            }
        }
        

        public void ResetSelection()
        {
           
            DeselectAllItems();
            ResetQuantity();
           
        }

        public void UpdateData(int itemIndex, Sprite RAMImage, Sprite MaterialNeed, string RAMName, string rarity, int Material, string stats)
        {
            if (ListOfRAMItems.Count > itemIndex)
            {
                ListOfRAMItems[itemIndex].SetData(RAMImage, MaterialNeed, RAMName, rarity, Material.ToString(), stats);

            }

        }
        public void ClearItems()
        {
            foreach (var item in ListOfRAMItems)
            {
                item.gameObject.SetActive(false);// Assuming ListOfShopItems contains the GameObjects of shop items
            }
            ListOfRAMItems.Clear();
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

