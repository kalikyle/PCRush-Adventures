using Exchanger.Model.MBWorld;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace Exchanger.Model.MBWorld
{

    [CreateAssetMenu]
    public class MBWorldItemSO : ScriptableObject
    {
        [SerializeField]
        public List<MBs> mbs;
        public int size = 3;
        public event Action<Dictionary<int, MBs>> OnMBsUpdated;

        public MBs GetItemAt(int obj)
        {
            return mbs[obj];
        }

        public Dictionary<int, MBs> GetCurrentInventoryState()
        {
            Dictionary<int, MBs> returnValue = new Dictionary<int, MBs>();
            for (int i = 0; i < mbs.Count; i++)
            {
                if (mbs[i].isEmpty)
                {
                    continue;
                }
                returnValue[i] = mbs[i];
            }
            return returnValue;
        }

        public void ReplaceMB(int oldIndex, MBs newMission)
        {
            if (oldIndex >= 0 && oldIndex < mbs.Count)
            {
                mbs[oldIndex] = newMission;
            }
            else
            {
                Debug.LogError("Invalid oldIndex provided for replacing mission.");
            }
        }

        private void AddMB(MBWorldExchangerSO item)
        {
            for (int i = 0; i < mbs.Count; i++)
            {
                if (mbs[i].isEmpty)
                {
                    mbs[i] = new MBs
                    {
                        item = item
                    };
                }
            }
            InformAboutChange();
        }
        internal void RemoveMB(int itemIndex)
        {
            try
            {
                // Remove the entire the pc
                mbs.RemoveAt(itemIndex);
                InformAboutChange();

            }
            catch { }

        }

        public void ShuffleMBs()
        {
            System.Random rng = new System.Random();
            int playerLevel = GameManager.instance.PlayerLevel;

            // Separate the MBs into two lists: one for MBs matching the player's level and one for others
            List<MBs> matchingMBs = new List<MBs>();
            List<MBs> otherMBs = new List<MBs>();

            foreach (var cpu in mbs)
            {
                if (cpu.item.Level <= playerLevel)
                {
                    matchingMBs.Add(cpu);
                }
                else
                {
                    otherMBs.Add(cpu);
                }
            }

            // Shuffle the matching MBs
            int n = matchingMBs.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                MBs value = matchingMBs[k];
                matchingMBs[k] = matchingMBs[n];
                matchingMBs[n] = value;
            }

            // Combine the lists, putting matching MBs at the beginning
            mbs = matchingMBs.Concat(otherMBs).ToList();

            InformAboutChange();
        }

        public void InformAboutChange()
        {

            OnMBsUpdated?.Invoke(GetCurrentInventoryState());
        }
    }
    [Serializable]
    public struct MBs
    {

        public MBWorldExchangerSO item;
        public bool isEmpty => item == null;

        public static MBs GetEmptyItem() => new MBs
        {
            item = null,
        };
    }
}
