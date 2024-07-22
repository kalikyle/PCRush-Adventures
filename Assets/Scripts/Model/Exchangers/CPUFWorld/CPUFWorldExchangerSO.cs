using PartsInventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Exchanger.Model.CPUFWorld
{
    [CreateAssetMenu]
    public class CPUFWorldExchangerSO : ScriptableObject
    {
        public int ID => GetInstanceID();


        [field: SerializeField]
        public PartsSO Parts { get; set; }

        [field: SerializeField]
        public GameObject MaterialsNeed { get; set; }

        [field: SerializeField]
        public int MaterialsAmountNeed { get; set; }


        [field: SerializeField]
        public int Level { get; set; }
    }
}
