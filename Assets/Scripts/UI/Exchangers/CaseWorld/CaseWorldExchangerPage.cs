using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
namespace Exchanger.UI.CaseWorld
{
    public class CaseWorldExchangerPage : MonoBehaviour
    {
        [SerializeField]
        private CaseWorldExchangerItem itemPrefab;

        [SerializeField]
        private RectTransform contentPanel;

        public TMP_Text Time;


        public List<CaseWorldExchangerItem> ListOfCaseItems = new List<CaseWorldExchangerItem>();

        void Start()
        {

        }
        public void UpdateTimer(string timeText)
        {
            Time.text = timeText;
        }

        public void InitializedCase(int inventorysize)
        {
            for (int i = 0; i < inventorysize; i++)
            {
                CaseWorldExchangerItem uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);

                uiItem.transform.SetParent(contentPanel);
                uiItem.transform.localScale = new Vector3(1, 1, 1);
                ListOfCaseItems.Add(uiItem);

                uiItem.SetTemporaryIndex(i);
                uiItem.OnItemClicked += HandleItemSelection;

            }
        }

        private IEnumerator SpawnMissions(int numberOfMissions)
        {
            for (int i = 0; i < numberOfMissions; i++)
            {
                CaseWorldExchangerItem uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
                uiItem.transform.SetParent(contentPanel);
                uiItem.transform.localScale = new Vector3(1, 1, 1);
                ListOfCaseItems.Add(uiItem);
                uiItem.SetTemporaryIndex(i);
                uiItem.OnItemClicked += HandleItemSelection;
                //buyItem.OnItemClicked += HandleItemSelectionBuy;
                yield return new WaitForSeconds(3f); // Wait for 5 seconds before spawning the next mission
            }
        }

        public bool IsCaseVisible(int missionId)
        {
            if (missionId >= 0 && missionId < ListOfCaseItems.Count)
            {
                // Assuming the mission UI object is active or visible based on its game object's active status
                return ListOfCaseItems[missionId].gameObject.activeSelf;
            }
            return false;
        }


        private void HandleItemSelection(CaseWorldExchangerItem item)
        {
            int index = ListOfCaseItems.IndexOf(item);
            if (index == -1)
            {
                return;
            }
            //OnDescriptionRequested?.Invoke(index);
            DeselectAllItems();

        }

        private void DeselectAllItems()
        {
            foreach (CaseWorldExchangerItem item in ListOfCaseItems)
            {
                item.DeSelect();

            }
        }

        private void ResetQuantity()
        {
            foreach (CaseWorldExchangerItem item in ListOfCaseItems)
            {
                item.ResetQuantity();


            }
        }


        public void ResetSelection()
        {

            DeselectAllItems();
            ResetQuantity();

        }

        public void UpdateData(int itemIndex, Sprite CaseImage, Sprite MaterialNeed, string CaseName, string rarity, int Material, string stats)
        {
            if (ListOfCaseItems.Count > itemIndex)
            {
                ListOfCaseItems[itemIndex].SetData(CaseImage, MaterialNeed, CaseName, rarity, Material.ToString(), stats);

            }

        }
        public void ClearItems()
        {
            foreach (var item in ListOfCaseItems)
            {
                item.gameObject.SetActive(false);// Assuming ListOfShopItems contains the GameObjects of shop items
            }
            ListOfCaseItems.Clear();
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
