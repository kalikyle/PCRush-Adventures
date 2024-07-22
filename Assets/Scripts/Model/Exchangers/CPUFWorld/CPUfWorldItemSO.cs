using Exchanger.Model.CPUFWorld;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace Exchanger.Model.CPUFWorld
{
    [CreateAssetMenu]
    public class CPUfWorldItemSO : ScriptableObject
    {
        [SerializeField]
        public List<CPUFs> cpufs;
        public int size = 3;
        public event Action<Dictionary<int, CPUFs>> OnCPUFsUpdated;

        public CPUFs GetItemAt(int obj)
        {
            return cpufs[obj];
        }

        public Dictionary<int, CPUFs> GetCurrentInventoryState()
        {
            Dictionary<int, CPUFs> returnValue = new Dictionary<int, CPUFs>();
            for (int i = 0; i < cpufs.Count; i++)
            {
                if (cpufs[i].isEmpty)
                {
                    continue;
                }
                returnValue[i] = cpufs[i];
            }
            return returnValue;
        }

        public void ReplaceCPUF(int oldIndex, CPUFs newMission)
        {
            if (oldIndex >= 0 && oldIndex < cpufs.Count)
            {
                cpufs[oldIndex] = newMission;
            }
            else
            {
                Debug.LogError("Invalid oldIndex provided for replacing mission.");
            }
        }

        private void AddCPUF(CPUFWorldExchangerSO item)
        {
            for (int i = 0; i < cpufs.Count; i++)
            {
                if (cpufs[i].isEmpty)
                {
                    cpufs[i] = new CPUFs
                    {
                        item = item
                    };
                }
            }
            InformAboutChange();
        }
        internal void RemoveCPUF(int itemIndex)
        {
            try
            {
                // Remove the entire the pc
                cpufs.RemoveAt(itemIndex);
                InformAboutChange();

            }
            catch { }

        }

        public void ShuffleCPUFs()
        {
            System.Random rng = new System.Random();
            int playerLevel = GameManager.instance.PlayerLevel;

            // Separate the CPUFs into two lists: one for CPUFs matching the player's level and one for others
            List<CPUFs> matchingCPUFs = new List<CPUFs>();
            List<CPUFs> otherCPUFs = new List<CPUFs>();

            foreach (var cpu in cpufs)
            {
                if (cpu.item.Level <= playerLevel)
                {
                    matchingCPUFs.Add(cpu);
                }
                else
                {
                    otherCPUFs.Add(cpu);
                }
            }

            // Shuffle the matching CPUFs
            int n = matchingCPUFs.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                CPUFs value = matchingCPUFs[k];
                matchingCPUFs[k] = matchingCPUFs[n];
                matchingCPUFs[n] = value;
            }

            // Combine the lists, putting matching CPUFs at the beginning
            cpufs = matchingCPUFs.Concat(otherCPUFs).ToList();

            InformAboutChange();
        }

        public void InformAboutChange()
        {

            OnCPUFsUpdated?.Invoke(GetCurrentInventoryState());
        }
    }
    [Serializable]
    public struct CPUFs
    {

        public CPUFWorldExchangerSO item;
        public bool isEmpty => item == null;

        public static CPUFs GetEmptyItem() => new CPUFs
        {
            item = null,
        };
    }
}
