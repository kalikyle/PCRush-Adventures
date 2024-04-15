using PC.Model;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
namespace PC.UI
{
    public class PCMenu : MonoBehaviour
    {
        [SerializeField]
        private PCPage PCpage;
        public void Start()
        {
           // PCData.Initialize();
        }
        public void Show()
        {
            gameObject.SetActive(true);

        }
        public void Hide()
        {
            gameObject.SetActive(false);
            PCpage.ResetSelection();
        }
    }
}

