using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Armor.Model {

    [CreateAssetMenu]
    public class ArmorSO : ScriptableObject
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
        public double Armor { get; set; }

        [field: SerializeField]
        public double Mana { get; set; }
    }

}
