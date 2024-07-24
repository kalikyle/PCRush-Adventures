
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace Exchanger.Model.PSUWorld
{

    [CreateAssetMenu]
    public class PSUWorldItemSO : ScriptableObject
    {
        [SerializeField]
        public List<PSUs> psus;
        public int size = 3;
        public event Action<Dictionary<int, PSUs>> OnPSUsUpdated;

        public PSUs GetItemAt(int obj)
        {
            return psus[obj];
        }

        public Dictionary<int, PSUs> GetCurrentInventoryState()
        {
            Dictionary<int, PSUs> returnValue = new Dictionary<int, PSUs>();
            for (int i = 0; i < psus.Count; i++)
            {
                if (psus[i].isEmpty)
                {
                    continue;
                }
                returnValue[i] = psus[i];
            }
            return returnValue;
        }

        public void ReplacePSU(int oldIndex, PSUs newMission)
        {
            if (oldIndex >= 0 && oldIndex < psus.Count)
            {
                psus[oldIndex] = newMission;
            }
            else
            {
                Debug.LogError("Invalid oldIndex provided for replacing mission.");
            }
        }

        private void AddPSU(PSUWorldExchangerSO item)
        {
            for (int i = 0; i < psus.Count; i++)
            {
                if (psus[i].isEmpty)
                {
                    psus[i] = new PSUs
                    {
                        item = item
                    };
                }
            }
            InformAboutChange();
        }
        internal void RemovePSU(int itemIndex)
        {
            try
            {
                // Remove the entire the pc
                psus.RemoveAt(itemIndex);
                InformAboutChange();

            }
            catch { }

        }

        public void ShufflePSUs()
        {
            System.Random rng = new System.Random();
            int playerLevel = GameManager.instance.PlayerLevel;

            // Separate the PSUs into two lists: one for PSUs matching the player's level and one for others
            List<PSUs> matchingPSUs = new List<PSUs>();
            List<PSUs> otherPSUs = new List<PSUs>();

            foreach (var cpu in psus)
            {
                if (cpu.item.Level <= playerLevel)
                {
                    matchingPSUs.Add(cpu);
                }
                else
                {
                    otherPSUs.Add(cpu);
                }
            }

            // Shuffle the matching PSUs
            int n = matchingPSUs.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                PSUs value = matchingPSUs[k];
                matchingPSUs[k] = matchingPSUs[n];
                matchingPSUs[n] = value;
            }

            // Combine the lists, putting matching PSUs at the beginning
            psus = matchingPSUs.Concat(otherPSUs).ToList();

            InformAboutChange();
        }

        public void InformAboutChange()
        {

            OnPSUsUpdated?.Invoke(GetCurrentInventoryState());
        }
    }
    [Serializable]
    public struct PSUs
    {

        public PSUWorldExchangerSO item;
        public bool isEmpty => item == null;

        public static PSUs GetEmptyItem() => new PSUs
        {
            item = null,
        };
    }
}
