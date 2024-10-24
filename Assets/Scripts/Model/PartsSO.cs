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
        public string UniqueID { get; private set; }

        [field: SerializeField]
        public bool IsStackable { get; set; }

        public int ID => GetInstanceID();

        [field: SerializeField]
        public int MaxStackableSize { get; set; } = 1;

        [field: SerializeField]
        public string Name { get; set; }

        [field: SerializeField]
        public string rarity { get; set; }

        [field: SerializeField]
        public Sprite ItemImage { get; set; } //sprite

        [field: SerializeField]
        public string Category { get; set; }

        [field: SerializeField]
        public double Price { get; set; }




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


        //for stats

        [field: SerializeField]
        public double CaseStrength { get; set; }

        // FOR MOTHERBOARD
        [field: SerializeField]
        public double MotherboardStrength { get; set; }
        [field: SerializeField]
        public string CPUSocket { get; set; }
        [field: SerializeField]
        public string RAMSlot { get; set; }

        //for CPU
        [field: SerializeField]
        public double BaseSpeed { get; set; }

        [field: SerializeField]
        public string CPUSupportedSocket { get; set; }

        //for RAM
        [field: SerializeField]
        public double Memory { get; set; }

        [field: SerializeField]
        public string RAMSupportedSlot { get; set; }

        //for CPU Fan
        [field: SerializeField]
        public double CoolingPower { get; set; }

        //for GPU 
        [field: SerializeField]
        public double ClockSpeed { get; set; }

        //for Storage
        [field: SerializeField]
        public double Storage { get; set; }

        // for PSU
        [field: SerializeField]
        public double WattagePower { get; set; }



        private void OnValidate()
        {
            if (string.IsNullOrEmpty(UniqueID))
            {
                UniqueID = Guid.NewGuid().ToString();

                #if UNITY_EDITOR
                UnityEditor.EditorUtility.SetDirty(this);
                #endif
            }
        }
    }
}
