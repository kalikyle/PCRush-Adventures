using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Exchanger.Model.RAMWorld
{
    [CreateAssetMenu]
    public class RAMWorldItemSO : ScriptableObject
    {
        [SerializeField]
        public List<RAMs> rams;
        public int size = 3;
        public event Action<Dictionary<int, RAMs>> OnRAMsUpdated;

        public RAMs GetItemAt(int obj)
        {
            return rams[obj];
        }

        public Dictionary<int, RAMs> GetCurrentInventoryState()
        {
            Dictionary<int, RAMs> returnValue = new Dictionary<int, RAMs>();
            for (int i = 0; i < rams.Count; i++)
            {
                if (rams[i].isEmpty)
                {
                    continue;
                }
                returnValue[i] = rams[i];
            }
            return returnValue;
        }

        public void ReplaceRAM(int oldIndex, RAMs newMission)
        {
            if (oldIndex >= 0 && oldIndex < rams.Count)
            {
                rams[oldIndex] = newMission;
            }
            else
            {
                Debug.LogError("Invalid oldIndex provided for replacing mission.");
            }
        }

        private void AddRAM(RAMWorldExchangerSO item)
        {
            for (int i = 0; i < rams.Count; i++)
            {
                if (rams[i].isEmpty)
                {
                    rams[i] = new RAMs
                    {
                        item = item
                    };
                }
            }
            InformAboutChange();
        }
        internal void RemoveRAM(int itemIndex)
        {
            try
            {
                // Remove the entire the pc
                rams.RemoveAt(itemIndex);
                InformAboutChange();

            }
            catch { }

        }

        public void ShuffleRAMs()
        {
            System.Random rng = new System.Random();
            int playerLevel = GameManager.instance.PlayerLevel;

            // Separate the RAMs into two lists: one for RAMs matching the player's level and one for others
            List<RAMs> matchingRAMs = new List<RAMs>();
            List<RAMs> otherRAMs = new List<RAMs>();

            foreach (var cpu in rams)
            {
                if (cpu.item.Level <= playerLevel)
                {
                    matchingRAMs.Add(cpu);
                }
                else
                {
                    otherRAMs.Add(cpu);
                }
            }

            // Shuffle the matching RAMs
            int n = matchingRAMs.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                RAMs value = matchingRAMs[k];
                matchingRAMs[k] = matchingRAMs[n];
                matchingRAMs[n] = value;
            }

            // Combine the lists, putting matching RAMs at the beginning
            rams = matchingRAMs.Concat(otherRAMs).ToList();

            InformAboutChange();
        }

        public void InformAboutChange()
        {

            OnRAMsUpdated?.Invoke(GetCurrentInventoryState());
        }
    }
    [Serializable]
    public struct RAMs
    {

        public RAMWorldExchangerSO item;
        public bool isEmpty => item == null;

        public static RAMs GetEmptyItem() => new RAMs
        {
            item = null,
        };
    }
}
