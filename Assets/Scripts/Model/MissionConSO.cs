using PC.Model;
using PC.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;
using static Inventory.Model.InventorySO;

namespace Orders.Model {
    [CreateAssetMenu]
    public class MissionConSO : ScriptableObject
    {

        [SerializeField]
        public List<Missions> missionOrders;
        public int size = 3;
        
        public event Action<Dictionary<int, Missions>> OnMissionUpdated;


        //[SerializeField]
        //public List<Missions> allAvailableMissions;
        
        


        public Missions GetItemAt(int obj)
        {
           
                return missionOrders[obj];
           
        }
        //public void AddMissionByLevel(int level)
        //{
        //    List<Missions> missionsAtLevel = allAvailableMissions.Where(m => m.orders.Level == level).ToList();

        //    foreach (Missions mission in missionsAtLevel)
        //    {
        //        AddMission(mission);

        //        //allAvailableMissions.Remove(mission);
        //    }

        //    InformAboutChange();
        //}

        public int OrdersCount()
        {
            int missionCounts = missionOrders.Count();
            return missionCounts;
        }
        public void AddMission(Missions missions)
        {
            AddMission(missions.orders);
        }
        public void ReplaceMission(int oldIndex, Missions newMission)
        {
            if (oldIndex >= 0 && oldIndex < missionOrders.Count)
            {
                missionOrders[oldIndex] = newMission;
            }
            else
            {
                Debug.LogError("Invalid oldIndex provided for replacing mission.");
            }
        }

        private void AddMission(OrderSO orders)
        {
            for (int i = 0; i < missionOrders.Count; i++)
            {
                if (missionOrders[i].isEmpty)
                {
                    missionOrders[i] = new Missions
                    {
                        orders = orders
                    };
                }
            }
            InformAboutChange();
        }
        internal void RemoveMission(int itemIndex)
        {
            try
            {
                // Remove the entire the pc
                missionOrders.RemoveAt(itemIndex);
                InformAboutChange();

            }
            catch { }

        }

        public void InformAboutChange()
        {

            OnMissionUpdated?.Invoke(GetCurrentInventoryState());
        }

        //public void ShuffleMissions()
        //{
        //    System.Random rng = new System.Random();
        //    int n = missionOrders.Count;
        //    while (n > 1)
        //    {
        //        n--;
        //        int k = rng.Next(n + 1);
        //        Missions value = missionOrders[k];
        //        missionOrders[k] = missionOrders[n];
        //        missionOrders[n] = value;
        //    }

        //    InformAboutChange();
        //}
        //public void ShuffleMissions()
        //{
        //    System.Random rng = new System.Random();
        //    int n = missionOrders.Count;
        //    int startIndex = Mathf.Min(n, size); // Define the start index based on the size variable

        //    for (int i = 0; i < startIndex; i++)
        //    {
        //        if (missionOrders[i].orders.Level > GameManager2.Instance.currentLevel)
        //        {
        //            for (int j = startIndex; j < n; j++)
        //            {
        //                if (missionOrders[j].orders.Level <= GameManager2.Instance.currentLevel)
        //                {
        //                    Missions temp = missionOrders[i];
        //                    missionOrders[i] = missionOrders[j];
        //                    missionOrders[j] = temp;
        //                    break;
        //                }
        //            }
        //        }
        //    }

        //    while (n > startIndex)
        //    {
        //        n--;
        //        int k = rng.Next(startIndex, n + 1);
        //        Missions value = missionOrders[k];
        //        missionOrders[k] = missionOrders[n];
        //        missionOrders[n] = value;
        //    }

        //    InformAboutChange();
        //}
        public void ShuffleMissions()
        {
            System.Random rng = new System.Random();
            int n = missionOrders.Count;

            while (n > 1)
            {
                n--;
                int k = rng.Next(0, n + 1);
                Missions value = missionOrders[k];
                missionOrders[k] = missionOrders[n];
                missionOrders[n] = value;
            }

            InformAboutChange();
        }

        public Dictionary<int, Missions> GetCurrentInventoryState()
        {
            Dictionary<int, Missions> returnValue = new Dictionary<int, Missions>();
            for (int i = 0; i < missionOrders.Count; i++)
            {
                if (missionOrders[i].isEmpty)
                {
                    continue;
                }
                returnValue[i] = missionOrders[i];
            }
            return returnValue;

        }
    }
    [Serializable]
    public struct Missions
    {
        public OrderSO orders;
        public static readonly Missions Empty = new Missions();
        public bool isEmpty => orders == null;

        public static Missions GetEmptyItem() => new Missions
        {
            orders = null
        };
    }

}

