using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Swords.Model
{
    [CreateAssetMenu]
    public class SwordSO : ScriptableObject
    {
        //[field: SerializeField]
        //public bool IsStackable { get; set; }

        public int ID => GetInstanceID();


        [field: SerializeField]
        public string Name { get; set; }

        [field: SerializeField]
        public int SpriteIndex { get; set; } //sprite

        [field: SerializeField]
        public int Price { get; set; }


        //perks
        [field: SerializeField]
        public double AttackDamage { get; set; }


    }
}