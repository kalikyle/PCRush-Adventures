using Exchanger.Model.StorageWorld;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace Exchanger.Model.StorageWorld
{

    [CreateAssetMenu]
    public class StorageItemSO : ScriptableObject
    {
        [SerializeField]
        public List<Storages> storages;
        public int size = 3;
        public event Action<Dictionary<int, Storages>> OnStoragesUpdated;

        public Storages GetItemAt(int obj)
        {
            return storages[obj];
        }

        public Dictionary<int, Storages> GetCurrentInventoryState()
        {
            Dictionary<int, Storages> returnValue = new Dictionary<int, Storages>();
            for (int i = 0; i < storages.Count; i++)
            {
                if (storages[i].isEmpty)
                {
                    continue;
                }
                returnValue[i] = storages[i];
            }
            return returnValue;
        }

        public void ReplaceStorage(int oldIndex, Storages newMission)
        {
            if (oldIndex >= 0 && oldIndex < storages.Count)
            {
                storages[oldIndex] = newMission;
            }
            else
            {
                Debug.LogError("Invalid oldIndex provided for replacing mission.");
            }
        }

        private void AddStorage(StorageWorldExchangerSO item)
        {
            for (int i = 0; i < storages.Count; i++)
            {
                if (storages[i].isEmpty)
                {
                    storages[i] = new Storages
                    {
                        item = item
                    };
                }
            }
            InformAboutChange();
        }
        internal void RemoveStorage(int itemIndex)
        {
            try
            {
                // Remove the entire the pc
                storages.RemoveAt(itemIndex);
                InformAboutChange();

            }
            catch { }

        }

        public void ShuffleStorages()
        {
            System.Random rng = new System.Random();
            int playerLevel = GameManager.instance.PlayerLevel;

            // Separate the Storages into two lists: one for Storages matching the player's level and one for others
            List<Storages> matchingStorages = new List<Storages>();
            List<Storages> otherStorages = new List<Storages>();

            foreach (var cpu in storages)
            {
                if (cpu.item.Level <= playerLevel)
                {
                    matchingStorages.Add(cpu);
                }
                else
                {
                    otherStorages.Add(cpu);
                }
            }

            // Shuffle the matching Storages
            int n = matchingStorages.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                Storages value = matchingStorages[k];
                matchingStorages[k] = matchingStorages[n];
                matchingStorages[n] = value;
            }

            // Combine the lists, putting matching Storages at the beginning
            storages = matchingStorages.Concat(otherStorages).ToList();

            InformAboutChange();
        }

        public void InformAboutChange()
        {

            OnStoragesUpdated?.Invoke(GetCurrentInventoryState());
        }
    }
    [Serializable]
    public struct Storages
    {

        public StorageWorldExchangerSO item;
        public bool isEmpty => item == null;

        public static Storages GetEmptyItem() => new Storages
        {
            item = null,
        };
    }
}
