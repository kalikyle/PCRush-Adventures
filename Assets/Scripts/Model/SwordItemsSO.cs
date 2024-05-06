using Shop.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Swords.Model
{
    [CreateAssetMenu]
    public class SwordItemsSO : ScriptableObject
    {

        [SerializeField]
        public List<Swords> Sword;

        

        public Swords GetItemAt(int obj)
        {
            return Sword[obj];
        }

        public Dictionary<int, Swords> GetCurrentInventoryState()
        {
            Dictionary<int, Swords> returnValue = new Dictionary<int, Swords>();
            for (int i = 0; i < Sword.Count; i++)
            {
                if (Sword[i].isEmpty)
                {
                    continue;
                }
                returnValue[i] = Sword[i];
            }
            return returnValue;
        }

    }
    [Serializable]
    public struct Swords
    {

        public SwordSO item;
        public bool isEmpty => item == null;

        public static Swords GetEmptyItem() => new Swords
        {
            item = null,
        };
    }

}
