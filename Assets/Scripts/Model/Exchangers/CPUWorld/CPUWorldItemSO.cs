using Swords.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Exchanger.Model.CPUWorld
{
    [CreateAssetMenu]
    public class CPUWorldItemSO : ScriptableObject
    {
        [SerializeField]
        public List<CPUs> Procies;

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
