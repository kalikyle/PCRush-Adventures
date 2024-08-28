using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shield.Model
{
    [CreateAssetMenu]
    public class ShieldSO : ScriptableObject
    {
        public int ID => GetInstanceID();


        [field: SerializeField]
        public string Name { get; set; }

        [field: SerializeField]
        public int SpriteIndex { get; set; } //sprite

        [field: SerializeField]
        public int Price { get; set; }


        //perks
        [field: SerializeField]
        public double ManaRegen { get; set; }
        [field: SerializeField]
        public double CriticalChance { get; set; }
    }
}
