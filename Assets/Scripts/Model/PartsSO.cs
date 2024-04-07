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


        [field: SerializeField]
        public double attack { get; set; }

        [field: SerializeField]
        public double health { get; set; }

        [field: SerializeField]
        public double healthregen { get; set; }


        //[field: SerializeField]
        //public double Price { get; set; }
    }
}
