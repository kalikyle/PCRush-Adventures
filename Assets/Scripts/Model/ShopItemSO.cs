using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shop.Model
{
    [CreateAssetMenu]
    public class ShopItemSO : ScriptableObject
    {
        [field: SerializeField]
        public bool IsStackable { get; set; }

        public int ID => GetInstanceID();

        [field: SerializeField]
        public int MaxStackableSize { get; set; } = 1;

        [field: SerializeField]
        public string Name { get; set; }

        [field: SerializeField]
        public Sprite ItemImage { get; set; } //sprite

        public string Category { get; set; }

        [field: SerializeField]
        public double Price { get; set; }

        [field: SerializeField]
        public bool Sold { get; set; }
    }
}