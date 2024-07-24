using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Exchanger.UI.StorageWorld
{
    public class StorageWorldExchangerPage : MonoBehaviour
    {
        [SerializeField]
        private StorageWorldExchangerItem itemPrefab;

        [SerializeField]
        private RectTransform contentPanel;

        public TMP_Text Time;


        public List<StorageWorldExchangerItem> ListOfStorageItems = new List<StorageWorldExchangerItem>();

        void Start()
        {

        }
        public void UpdateTimer(string timeText)
        {
            Time.text = timeText;
        }

        public void InitializedStorage(int inventorysize)
        {
            for (int i = 0; i < inventorysize; i++)
            {
                StorageWorldExchangerItem uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);

                uiItem.transform.SetParent(contentPanel);
                uiItem.transform.localScale = new Vector3(1, 1, 1);
                ListOfStorageItems.Add(uiItem);

                uiItem.SetTemporaryIndex(i);
                uiItem.OnItemClicked += HandleItemSelection;

            }
        }

        private IEnumerator SpawnMissions(int numberOfMissions)
        {
            for (int i = 0; i < numberOfMissions; i++)
            {
                StorageWorldExchangerItem uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
                uiItem.transform.SetParent(contentPanel);
                uiItem.transform.localScale = new Vector3(1, 1, 1);
                ListOfStorageItems.Add(uiItem);
                uiItem.SetTemporaryIndex(i);
                uiItem.OnItemClicked += HandleItemSelection;
                //buyItem.OnItemClicked += HandleItemSelectionBuy;
                yield return new WaitForSeconds(3f); // Wait for 5 seconds before spawning the next mission
            }
        }

        public bool IsStorageVisible(int missionId)
        {
            if (missionId >= 0 && missionId < ListOfStorageItems.Count)
            {
                // Assuming the mission UI object is active or visible based on its game object's active status
                return ListOfStorageItems[missionId].gameObject.activeSelf;
            }
            return false;
        }


        private void HandleItemSelection(StorageWorldExchangerItem item)
        {
            int index = ListOfStorageItems.IndexOf(item);
            if (index == -1)
            {
                return;
            }
            //OnDescriptionRequested?.Invoke(index);
            DeselectAllItems();

        }

        private void DeselectAllItems()
        {
            foreach (StorageWorldExchangerItem item in ListOfStorageItems)
            {
                item.DeSelect();

            }
        }

        private void ResetQuantity()
        {
            foreach (StorageWorldExchangerItem item in ListOfStorageItems)
            {
                item.ResetQuantity();


            }
        }


        public void ResetSelection()
        {

            DeselectAllItems();
            ResetQuantity();

        }

        public void UpdateData(int itemIndex, Sprite StorageImage, Sprite MaterialNeed, string StorageName, string rarity, int Material, string stats)
        {
            if (ListOfStorageItems.Count > itemIndex)
            {
                ListOfStorageItems[itemIndex].SetData(StorageImage, MaterialNeed, StorageName, rarity, Material.ToString(), stats);

            }

        }
        public void ClearItems()
        {
            foreach (var item in ListOfStorageItems)
            {
                item.gameObject.SetActive(false);// Assuming ListOfShopItems contains the GameObjects of shop items
            }
            ListOfStorageItems.Clear();
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
