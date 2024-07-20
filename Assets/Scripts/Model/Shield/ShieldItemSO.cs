
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Shield.Model
{
    [CreateAssetMenu]
    public class ShieldItemSO : ScriptableObject
    {
        [SerializeField]
        public List<Shields> Shield;



        public Shields GetItemAt(int obj)
        {
            return Shield[obj];
        }

        public Dictionary<int, Shields> GetCurrentInventoryState()
        {
            Dictionary<int, Shields> returnValue = new Dictionary<int, Shields>();
            for (int i = 0; i < Shield.Count; i++)
            {
                if (Shield[i].isEmpty)
                {
                    continue;
                }
                returnValue[i] = Shield[i];
            }
            return returnValue;
        }
    }
    [Serializable]
    public struct Shields
    {

        public ShieldSO item;
        public bool isEmpty => item == null;

        public static Shields GetEmptyItem() => new Shields
        {
            item = null,
        };
    }
}
