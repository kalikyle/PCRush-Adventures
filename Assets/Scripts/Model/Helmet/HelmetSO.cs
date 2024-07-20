using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Helmets.Model
{
    [CreateAssetMenu]
    public class HelmetSO : ScriptableObject
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
        public double Health { get; set; }
        [field: SerializeField]
        public double HealthRegen { get; set; }
    }
}
