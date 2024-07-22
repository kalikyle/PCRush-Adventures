using Exchanger.Model.GPUWorld;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace Exchanger.Model.GPUWorld
{
    [CreateAssetMenu]
    public class GPUWorldItemSO : ScriptableObject
    {
        [SerializeField]
        public List<GPUs> gpus;
        public int size = 3;
        public event Action<Dictionary<int, GPUs>> OnGPUsUpdated;

        public GPUs GetItemAt(int obj)
        {
            return gpus[obj];
        }

        public Dictionary<int, GPUs> GetCurrentInventoryState()
        {
            Dictionary<int, GPUs> returnValue = new Dictionary<int, GPUs>();
            for (int i = 0; i < gpus.Count; i++)
            {
                if (gpus[i].isEmpty)
                {
                    continue;
                }
                returnValue[i] = gpus[i];
            }
            return returnValue;
        }

        public void ReplaceGPU(int oldIndex, GPUs newMission)
        {
            if (oldIndex >= 0 && oldIndex < gpus.Count)
            {
                gpus[oldIndex] = newMission;
            }
            else
            {
                Debug.LogError("Invalid oldIndex provided for replacing mission.");
            }
        }

        private void AddGPU(GPUWorldExchangerSO item)
        {
            for (int i = 0; i < gpus.Count; i++)
            {
                if (gpus[i].isEmpty)
                {
                    gpus[i] = new GPUs
                    {
                        item = item
                    };
                }
            }
            InformAboutChange();
        }
        internal void RemoveGPU(int itemIndex)
        {
            try
            {
                // Remove the entire the pc
                gpus.RemoveAt(itemIndex);
                InformAboutChange();

            }
            catch { }

        }

        public void ShuffleGPUs()
        {
            System.Random rng = new System.Random();
            int playerLevel = GameManager.instance.PlayerLevel;

            // Separate the GPUs into two lists: one for GPUs matching the player's level and one for others
            List<GPUs> matchingGPUs = new List<GPUs>();
            List<GPUs> otherGPUs = new List<GPUs>();

            foreach (var cpu in gpus)
            {
                if (cpu.item.Level <= playerLevel)
                {
                    matchingGPUs.Add(cpu);
                }
                else
                {
                    otherGPUs.Add(cpu);
                }
            }

            // Shuffle the matching GPUs
            int n = matchingGPUs.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                GPUs value = matchingGPUs[k];
                matchingGPUs[k] = matchingGPUs[n];
                matchingGPUs[n] = value;
            }

            // Combine the lists, putting matching GPUs at the beginning
            gpus = matchingGPUs.Concat(otherGPUs).ToList();

            InformAboutChange();
        }

        public void InformAboutChange()
        {

            OnGPUsUpdated?.Invoke(GetCurrentInventoryState());
        }
    }
    [Serializable]
    public struct GPUs
    {

        public GPUWorldExchangerSO item;
        public bool isEmpty => item == null;

        public static GPUs GetEmptyItem() => new GPUs
        {
            item = null,
        };
    }
}
