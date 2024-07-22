using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
namespace Exchanger.UI.GPUWorld
{
    public class GPUExchangerPage : MonoBehaviour
    {
        [SerializeField]
        private GPUExchangerItem itemPrefab;

        [SerializeField]
        private RectTransform contentPanel;

        public TMP_Text Time;


        public List<GPUExchangerItem> ListOfGPUItems = new List<GPUExchangerItem>();

        void Start()
        {

        }
        public void UpdateTimer(string timeText)
        {
            Time.text = timeText;
        }

        public void InitializedGPU(int inventorysize)
        {
            for (int i = 0; i < inventorysize; i++)
            {
                GPUExchangerItem uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);

                uiItem.transform.SetParent(contentPanel);
                uiItem.transform.localScale = new Vector3(1, 1, 1);
                ListOfGPUItems.Add(uiItem);

                uiItem.SetTemporaryIndex(i);
                uiItem.OnItemClicked += HandleItemSelection;

            }
        }

        private IEnumerator SpawnMissions(int numberOfMissions)
        {
            for (int i = 0; i < numberOfMissions; i++)
            {
                GPUExchangerItem uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
                uiItem.transform.SetParent(contentPanel);
                uiItem.transform.localScale = new Vector3(1, 1, 1);
                ListOfGPUItems.Add(uiItem);
                uiItem.SetTemporaryIndex(i);
                uiItem.OnItemClicked += HandleItemSelection;
                //buyItem.OnItemClicked += HandleItemSelectionBuy;
                yield return new WaitForSeconds(3f); // Wait for 5 seconds before spawning the next mission
            }
        }

        public bool IsGPUVisible(int missionId)
        {
            if (missionId >= 0 && missionId < ListOfGPUItems.Count)
            {
                // Assuming the mission UI object is active or visible based on its game object's active status
                return ListOfGPUItems[missionId].gameObject.activeSelf;
            }
            return false;
        }


        private void HandleItemSelection(GPUExchangerItem item)
        {
            int index = ListOfGPUItems.IndexOf(item);
            if (index == -1)
            {
                return;
            }
            //OnDescriptionRequested?.Invoke(index);
            DeselectAllItems();

        }

        private void DeselectAllItems()
        {
            foreach (GPUExchangerItem item in ListOfGPUItems)
            {
                item.DeSelect();

            }
        }

        private void ResetQuantity()
        {
            foreach (GPUExchangerItem item in ListOfGPUItems)
            {
                item.ResetQuantity();


            }
        }


        public void ResetSelection()
        {

            DeselectAllItems();
            ResetQuantity();

        }

        public void UpdateData(int itemIndex, Sprite GPUImage, Sprite MaterialNeed, string GPUName, string rarity, int Material, string stats)
        {
            if (ListOfGPUItems.Count > itemIndex)
            {
                ListOfGPUItems[itemIndex].SetData(GPUImage, MaterialNeed, GPUName, rarity, Material.ToString(), stats);

            }

        }
        public void ClearItems()
        {
            foreach (var item in ListOfGPUItems)
            {
                item.gameObject.SetActive(false);// Assuming ListOfShopItems contains the GameObjects of shop items
            }
            ListOfGPUItems.Clear();
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
