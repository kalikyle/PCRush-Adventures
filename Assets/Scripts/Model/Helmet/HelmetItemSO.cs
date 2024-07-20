using Helmets.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Helmets.Model
{
    [CreateAssetMenu]
    public class HelmetItemSO : ScriptableObject
    {
        [SerializeField]
        public List<Helmets> Helmet;



        public Helmets GetItemAt(int obj)
        {
            return Helmet[obj];
        }

        public Dictionary<int, Helmets> GetCurrentInventoryState()
        {
            Dictionary<int, Helmets> returnValue = new Dictionary<int, Helmets>();
            for (int i = 0; i < Helmet.Count; i++)
            {
                if (Helmet[i].isEmpty)
                {
                    continue;
                }
                returnValue[i] = Helmet[i];
            }
            return returnValue;
        }

    }
    [Serializable]
    public struct Helmets
    {

        public HelmetSO item;
        public bool isEmpty => item == null;

        public static Helmets GetEmptyItem() => new Helmets
        {
            item = null,
        };
    }
}

