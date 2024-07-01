using Swords.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Exchanger.Model.CPUWorld
{
    [CreateAssetMenu]
    public class CPUWorldItemSO : ScriptableObject
    {
        [SerializeField]
        public List<CPUs> Procies;
        public int size = 3;
        public event Action<Dictionary<int, CPUs>> OnCPUsUpdated;

        public CPUs GetItemAt(int obj)
        {
            return Procies[obj];
        }

        public Dictionary<int, CPUs> GetCurrentInventoryState()
        {
            Dictionary<int, CPUs> returnValue = new Dictionary<int, CPUs>();
            for (int i = 0; i < Procies.Count; i++)
            {
                if (Procies[i].isEmpty)
                {
                    continue;
                }
                returnValue[i] = Procies[i];
            }
            return returnValue;
        }

        public void ReplaceCPU(int oldIndex, CPUs newMission)
        {
            if (oldIndex >= 0 && oldIndex < Procies.Count)
            {
                Procies[oldIndex] = newMission;
            }
            else
            {
                Debug.LogError("Invalid oldIndex provided for replacing mission.");
            }
        }

        private void AddCPU(CPUWorldExchangerSO item)
        {
            for (int i = 0; i < Procies.Count; i++)
            {
                if (Procies[i].isEmpty)
                {
                    Procies[i] = new CPUs
                    {
                        item = item
                    };
                }
            }
            InformAboutChange();
        }
        internal void RemoveCPU(int itemIndex)
        {
            try
            {
                // Remove the entire the pc
                Procies.RemoveAt(itemIndex);
                InformAboutChange();

            }
            catch { }

        }

        public void ShuffleCPUs()
        {
            System.Random rng = new System.Random();
            int playerLevel = GameManager.instance.PlayerLevel;

            // Separate the CPUs into two lists: one for CPUs matching the player's level and one for others
            List<CPUs> matchingCPUs = new List<CPUs>();
            List<CPUs> otherCPUs = new List<CPUs>();

            foreach (var cpu in Procies)
            {
                if (cpu.item.Level == playerLevel)
                {
                    matchingCPUs.Add(cpu);
                }
                else
                {
                    otherCPUs.Add(cpu);
                }
            }

            // Shuffle the matching CPUs
            int n = matchingCPUs.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                CPUs value = matchingCPUs[k];
                matchingCPUs[k] = matchingCPUs[n];
                matchingCPUs[n] = value;
            }

            // Combine the lists, putting matching CPUs at the beginning
            Procies = matchingCPUs.Concat(otherCPUs).ToList();

            InformAboutChange();
        }

        public void InformAboutChange()
        {

            OnCPUsUpdated?.Invoke(GetCurrentInventoryState());
        }

    }
    [Serializable]
    public struct CPUs
    {

        public CPUWorldExchangerSO item;
        public bool isEmpty => item == null;

        public static CPUs GetEmptyItem() => new CPUs
        {
            item = null,
        };
    }
}
