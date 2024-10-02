using Swords.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Exchanger.UI.CPUWorld
{


    public class CPUWorldExchangerPage : MonoBehaviour
    {
        [SerializeField]
        private CPUWorldExchangerItem itemPrefab;
        
        [SerializeField]
        private RectTransform contentPanel;

        public TMP_Text Time;

        
        public List<CPUWorldExchangerItem> ListOfCPUItems = new List<CPUWorldExchangerItem>();
       
        void Start()
        {

        }
        public void UpdateTimer(string timeText)
        {
            Time.text = timeText;
        }

        public void InitializedCPU(int inventorysize)
        {
            for (int i = 0; i < inventorysize; i++)
            {
                CPUWorldExchangerItem uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
               
                uiItem.transform.SetParent(contentPanel);
                uiItem.transform.localScale = new Vector3(1, 1, 1);
                ListOfCPUItems.Add(uiItem);
                
                uiItem.SetTemporaryIndex(i);
                uiItem.OnItemClicked += HandleItemSelection;
                
              

            }
        }

        private IEnumerator SpawnMissions(int numberOfMissions)
        {
            for (int i = 0; i < numberOfMissions; i++)
            {
                CPUWorldExchangerItem uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
                uiItem.transform.SetParent(contentPanel);
                uiItem.transform.localScale = new Vector3(1, 1, 1);
                ListOfCPUItems.Add(uiItem);
                uiItem.SetTemporaryIndex(i);
                uiItem.OnItemClicked += HandleItemSelection;
                yield return new WaitForSeconds(3f); // Wait for 5 seconds before spawning the next mission
            }
        }

        public bool IsCPUVisible(int missionId)
        {
            if (missionId >= 0 && missionId < ListOfCPUItems.Count)
            {
                // Assuming the mission UI object is active or visible based on its game object's active status
                return ListOfCPUItems[missionId].gameObject.activeSelf;
            }
            return false;
        }


        private void HandleItemSelection(CPUWorldExchangerItem item)
        {
            int index = ListOfCPUItems.IndexOf(item);
            if (index == -1)
            {
                return;
            }
            DeselectAllItems();
            
        }

        private void DeselectAllItems()
        {
            foreach (CPUWorldExchangerItem item in ListOfCPUItems)
            {
                item.DeSelect();
                
            }
        }

        private void ResetQuantity()
        {
            foreach (CPUWorldExchangerItem item in ListOfCPUItems)
            {
                item.ResetQuantity();


            }
        }

        public void ResetSelection()
        {
           
            DeselectAllItems();
            ResetQuantity();
           
        }

        public void UpdateData(int itemIndex, Sprite CPUImage, Sprite MaterialNeed, string CPUName, string rarity, int Material, string stats)
        {
            if (ListOfCPUItems.Count > itemIndex)
            {
                ListOfCPUItems[itemIndex].SetData(CPUImage, MaterialNeed, CPUName, rarity, Material.ToString(), stats);//this will add to the shop

            }

        }
        public void ClearItems()
        {
            foreach (var item in ListOfCPUItems)
            {
                item.gameObject.SetActive(false);// Assuming ListOfShopItems contains the GameObjects of shop items
            }
            ListOfCPUItems.Clear();
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
