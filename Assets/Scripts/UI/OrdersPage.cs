using PC.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
//using static UnityEditor.Progress;


namespace Orders.UI
{
    public class OrdersPage : MonoBehaviour
    {
        [SerializeField]
        private OrdersMission itemPrefab;
        [SerializeField]
        private RectTransform contentPanel;



        public List<OrdersMission> ListofMissions = new List<OrdersMission>();

        public event Action<int> OnOpenPCInventory;


        public void InitializedMissions(int inventorysize)
        {
            StartCoroutine(SpawnMissions(inventorysize));
        }

        private IEnumerator SpawnMissions(int numberOfMissions)
        {
            for (int i = 0; i < numberOfMissions; i++)
            {
                OrdersMission missions = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
                missions.transform.SetParent(contentPanel);
                ListofMissions.Add(missions);
                missions.OnMissionClicked += HandleItemSelection;

                yield return new WaitForSeconds(5f); // Wait for 5 seconds before spawning the next mission
            }
        }
        public void SetTimeTextColor(int missionIndex, Color color)
        {
            if (ListofMissions.Count > missionIndex)
            {
                ListofMissions[missionIndex].SetTimeTextColor(color);
            }
        }
        public bool IsMissionVisible(int missionId)
        {
            if (missionId >= 0 && missionId < ListofMissions.Count)
            {
                // Assuming the mission UI object is active or visible based on its game object's active status
                return ListofMissions[missionId].gameObject.activeSelf;
            }
            return false;
        }

        private void HandleItemSelection(OrdersMission mission)
        {
            int index = ListofMissions.IndexOf(mission);
            if (index == -1)
            {
                return;
            }
            OnOpenPCInventory?.Invoke(index);
        }

        public void UpdateData(int itemIndex, Sprite clientImage, string clientname, string descriptions, float time, float price, int exp)
        {
            if (ListofMissions.Count > itemIndex)
            {
                ListofMissions[itemIndex].SetMissionData(clientImage,clientname,descriptions, time,price,exp);

            }
        }

        public void Start()
        {

        }

       
        void Update()
        {

        }
    }
}

