using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PartsInventory.Model
{
    [CreateAssetMenu]
    public class PartsSO : ScriptableObject
    {
        [field: SerializeField]
        public bool IsStackable { get; set; }

        public int ID => GetInstanceID();

        [field: SerializeField]
        public int MaxStackableSize { get; set; } = 1;

        [field: SerializeField]
        public string Name { get; set; }

        [field: SerializeField]
        //[field: TextArea]
        public string rarity { get; set; }

        [field: SerializeField]
        public Sprite ItemImage { get; set; } //sprite

        [field: SerializeField]
        public string Category { get; set; }





        //Perks

        [field: SerializeField]
        public double AttackDamage { get; set; }

        [field: SerializeField]
        public double Health { get; set; }

        [field: SerializeField]
        public double Mana { get; set; }

        [field: SerializeField]
        public double HealthRegen { get; set; }

        [field: SerializeField]
        public double ManaRegen { get; set; }

        [field: SerializeField]
        public double WalkSpeed { get; set; }

        [field: SerializeField]
        public double Armor { get; set; }

        [field: SerializeField]
        public double CriticalChance { get; set; }




    }
}
