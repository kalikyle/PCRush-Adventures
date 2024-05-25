using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Armor.Model
{
    [CreateAssetMenu]
    public class ArmorItemsSO : ScriptableObject
    {
        [SerializeField]
        public List<Armors> Armor;



        public Armors GetItemAt(int obj)
        {
            return Armor[obj];
        }

        public Dictionary<int, Armors> GetCurrentInventoryState()
        {
            Dictionary<int, Armors> returnValue = new Dictionary<int, Armors>();
            for (int i = 0; i < Armor.Count; i++)
            {
                if (Armor[i].isEmpty)
                {
                    continue;
                }
                returnValue[i] = Armor[i];
            }
            return returnValue;
        }

    }
    [Serializable]
    public struct Armors
    {

        public ArmorSO item;
        public bool isEmpty => item == null;

        public static Armors GetEmptyItem() => new Armors
        {
            item = null,
        };
    }
}


