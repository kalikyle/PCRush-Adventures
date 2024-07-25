using Exchanger.Model.CaseWorld;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace Exchanger.Model.CaseWorld
{

    [CreateAssetMenu]
    public class CaseWorldItemSO : ScriptableObject
    {
        [SerializeField]
        public List<Cases> cases;
        public int size = 3;
        public event Action<Dictionary<int, Cases>> OnCasesUpdated;

        public Cases GetItemAt(int obj)
        {
            return cases[obj];
        }

        public Dictionary<int, Cases> GetCurrentInventoryState()
        {
            Dictionary<int, Cases> returnValue = new Dictionary<int, Cases>();
            for (int i = 0; i < cases.Count; i++)
            {
                if (cases[i].isEmpty)
                {
                    continue;
                }
                returnValue[i] = cases[i];
            }
            return returnValue;
        }

        public void ReplaceCase(int oldIndex, Cases newMission)
        {
            if (oldIndex >= 0 && oldIndex < cases.Count)
            {
                cases[oldIndex] = newMission;
            }
            else
            {
                Debug.LogError("Invalid oldIndex provided for replacing mission.");
            }
        }

        private void AddCase(CaseWorldExchangerSO item)
        {
            for (int i = 0; i < cases.Count; i++)
            {
                if (cases[i].isEmpty)
                {
                    cases[i] = new Cases
                    {
                        item = item
                    };
                }
            }
            InformAboutChange();
        }
        internal void RemoveCase(int itemIndex)
        {
            try
            {
                // Remove the entire the pc
                cases.RemoveAt(itemIndex);
                InformAboutChange();

            }
            catch { }

        }

        public void ShuffleCases()
        {
            System.Random rng = new System.Random();
            int playerLevel = GameManager.instance.PlayerLevel;

            // Separate the Cases into two lists: one for Cases matching the player's level and one for others
            List<Cases> matchingCases = new List<Cases>();
            List<Cases> otherCases = new List<Cases>();

            foreach (var cpu in cases)
            {
                if (cpu.item.Level <= playerLevel)
                {
                    matchingCases.Add(cpu);
                }
                else
                {
                    otherCases.Add(cpu);
                }
            }

            // Shuffle the matching Cases
            int n = matchingCases.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                Cases value = matchingCases[k];
                matchingCases[k] = matchingCases[n];
                matchingCases[n] = value;
            }

            // Combine the lists, putting matching Cases at the beginning
            cases = matchingCases.Concat(otherCases).ToList();

            InformAboutChange();
        }

        public void InformAboutChange()
        {

            OnCasesUpdated?.Invoke(GetCurrentInventoryState());
        }
    }
    [Serializable]
    public struct Cases
    {

        public CaseWorldExchangerSO item;
        public bool isEmpty => item == null;

        public static Cases GetEmptyItem() => new Cases
        {
            item = null,
        };
    }
}
