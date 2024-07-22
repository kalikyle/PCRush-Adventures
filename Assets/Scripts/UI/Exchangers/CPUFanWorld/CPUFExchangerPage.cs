using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
namespace Exchanger.UI.CPUFWorld
{
    public class CPUFExchangerPage : MonoBehaviour
    {
        [SerializeField]
        private CPUFExchangerItem itemPrefab;

        [SerializeField]
        private RectTransform contentPanel;

        public TMP_Text Time;


        public List<CPUFExchangerItem> ListOfCPUFItems = new List<CPUFExchangerItem>();

        void Start()
        {

        }
        public void UpdateTimer(string timeText)
        {
            Time.text = timeText;
        }

        public void InitializedCPUF(int inventorysize)
        {
            for (int i = 0; i < inventorysize; i++)
            {
                CPUFExchangerItem uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);

                uiItem.transform.SetParent(contentPanel);
                uiItem.transform.localScale = new Vector3(1, 1, 1);
                ListOfCPUFItems.Add(uiItem);

                uiItem.SetTemporaryIndex(i);
                uiItem.OnItemClicked += HandleItemSelection;

            }
        }

        private IEnumerator SpawnMissions(int numberOfMissions)
        {
            for (int i = 0; i < numberOfMissions; i++)
            {
                CPUFExchangerItem uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
                uiItem.transform.SetParent(contentPanel);
                uiItem.transform.localScale = new Vector3(1, 1, 1);
                ListOfCPUFItems.Add(uiItem);
                uiItem.SetTemporaryIndex(i);
                uiItem.OnItemClicked += HandleItemSelection;
                //buyItem.OnItemClicked += HandleItemSelectionBuy;
                yield return new WaitForSeconds(3f); // Wait for 5 seconds before spawning the next mission
            }
        }

        public bool IsCPUFVisible(int missionId)
        {
            if (missionId >= 0 && missionId < ListOfCPUFItems.Count)
            {
                // Assuming the mission UI object is active or visible based on its game object's active status
                return ListOfCPUFItems[missionId].gameObject.activeSelf;
            }
            return false;
        }


        private void HandleItemSelection(CPUFExchangerItem item)
        {
            int index = ListOfCPUFItems.IndexOf(item);
            if (index == -1)
            {
                return;
            }
            //OnDescriptionRequested?.Invoke(index);
            DeselectAllItems();

        }

        private void DeselectAllItems()
        {
            foreach (CPUFExchangerItem item in ListOfCPUFItems)
            {
                item.DeSelect();

            }
        }

        private void ResetQuantity()
        {
            foreach (CPUFExchangerItem item in ListOfCPUFItems)
            {
                item.ResetQuantity();


            }
        }


        public void ResetSelection()
        {

            DeselectAllItems();
            ResetQuantity();

        }

        public void UpdateData(int itemIndex, Sprite CPUFImage, Sprite MaterialNeed, string CPUFName, string rarity, int Material, string stats)
        {
            if (ListOfCPUFItems.Count > itemIndex)
            {
                ListOfCPUFItems[itemIndex].SetData(CPUFImage, MaterialNeed, CPUFName, rarity, Material.ToString(), stats);

            }

        }
        public void ClearItems()
        {
            foreach (var item in ListOfCPUFItems)
            {
                item.gameObject.SetActive(false);// Assuming ListOfShopItems contains the GameObjects of shop items
            }
            ListOfCPUFItems.Clear();
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
