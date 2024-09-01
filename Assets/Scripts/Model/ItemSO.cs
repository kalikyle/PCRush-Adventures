using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory.Model {
    


    [CreateAssetMenu]
    public class ItemSO : ScriptableObject
    {
        [field: SerializeField]
        public bool IsStackable { get; set; }

        public int ID => GetInstanceID();

        [field: SerializeField]
        public int MaxStackableSize { get; set; } = 1;

        [field: SerializeField]
        public string Name { get; set; }

        [field: SerializeField]
        [field: TextArea]
        public string Description { get; set; }

        [field: SerializeField]
        public Sprite ItemImage { get; set; } //sprite

        [field: SerializeField]
        public string Category { get; set; }
        [field: SerializeField]
        public double Price { get; set; }


        //FOR CASE

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
    }

}
