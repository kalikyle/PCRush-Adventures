using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
namespace Exchanger.UI.PSUWorld
{
    public class PSUWorldExchangerPage : MonoBehaviour
    {
        [SerializeField]
        private PSUWorldExchangerItem itemPrefab;

        [SerializeField]
        private RectTransform contentPanel;

        public TMP_Text Time;


        public List<PSUWorldExchangerItem> ListOfPSUItems = new List<PSUWorldExchangerItem>();

        void Start()
        {

        }
        public void UpdateTimer(string timeText)
        {
            Time.text = timeText;
        }

        public void InitializedPSU(int inventorysize)
        {
            for (int i = 0; i < inventorysize; i++)
            {
                PSUWorldExchangerItem uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);

                uiItem.transform.SetParent(contentPanel);
                uiItem.transform.localScale = new Vector3(1, 1, 1);
                ListOfPSUItems.Add(uiItem);

                uiItem.SetTemporaryIndex(i);
                uiItem.OnItemClicked += HandleItemSelection;

            }
        }

        private IEnumerator SpawnMissions(int numberOfMissions)
        {
            for (int i = 0; i < numberOfMissions; i++)
            {
                PSUWorldExchangerItem uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
                uiItem.transform.SetParent(contentPanel);
                uiItem.transform.localScale = new Vector3(1, 1, 1);
                ListOfPSUItems.Add(uiItem);
                uiItem.SetTemporaryIndex(i);
                uiItem.OnItemClicked += HandleItemSelection;
                //buyItem.OnItemClicked += HandleItemSelectionBuy;
                yield return new WaitForSeconds(3f); // Wait for 5 seconds before spawning the next mission
            }
        }

        public bool IsPSUVisible(int missionId)
        {
            if (missionId >= 0 && missionId < ListOfPSUItems.Count)
            {
                // Assuming the mission UI object is active or visible based on its game object's active status
                return ListOfPSUItems[missionId].gameObject.activeSelf;
            }
            return false;
        }


        private void HandleItemSelection(PSUWorldExchangerItem item)
        {
            int index = ListOfPSUItems.IndexOf(item);
            if (index == -1)
            {
                return;
            }
            //OnDescriptionRequested?.Invoke(index);
            DeselectAllItems();

        }

        private void DeselectAllItems()
        {
            foreach (PSUWorldExchangerItem item in ListOfPSUItems)
            {
                item.DeSelect();

            }
        }

        private void ResetQuantity()
        {
            foreach (PSUWorldExchangerItem item in ListOfPSUItems)
            {
                item.ResetQuantity();


            }
        }


        public void ResetSelection()
        {

            DeselectAllItems();
            ResetQuantity();

        }

        public void UpdateData(int itemIndex, Sprite PSUImage, Sprite MaterialNeed, string PSUName, string rarity, int Material, string stats)
        {
            if (ListOfPSUItems.Count > itemIndex)
            {
                ListOfPSUItems[itemIndex].SetData(PSUImage, MaterialNeed, PSUName, rarity, Material.ToString(), stats);

            }

        }
        public void ClearItems()
        {
            foreach (var item in ListOfPSUItems)
            {
                item.gameObject.SetActive(false);// Assuming ListOfShopItems contains the GameObjects of shop items
            }
            ListOfPSUItems.Clear();
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

