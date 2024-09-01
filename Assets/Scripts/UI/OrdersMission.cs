using PC.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Orders.UI
{
    public class OrdersMission : MonoBehaviour, IPointerClickHandler
    {

        [SerializeField]
        private Image ClientImage;

        [SerializeField]
        private TMP_Text ClientName;

        [SerializeField]
        private TMP_Text Description;

        [SerializeField]
        private TMP_Text Requirements;

        [SerializeField]
        private TMP_Text Time;

        [SerializeField]
        private TMP_Text Price;

        [SerializeField]
        private TMP_Text Experience;

        [SerializeField]
        private TMP_Text Level;

        public event Action<OrdersMission> OnMissionClicked;


        public void OnPointerClick(PointerEventData eventData)
        {
            OnMissionClicked.Invoke(this);
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        public void SetTimeTextColor(Color color)
        {
            // Assuming you have a TextMeshPro component for the time text
            if (Time != null)
            {
                Time.color = color;
            }
        }
        public void SetMissionData(Sprite clientImage, string clientname, string descriptions,  float time, float price, int exp)
        {
            this.ClientImage.sprite = clientImage;
            this.ClientName.text = clientname;
            this.Description.text = descriptions;
            //this.Requirements.text = requirements;
            this.Time.text = time + "";
            this.Price.text = "$" + price + "";
            this.Experience.text = exp + "";
            //this.Level.text = level + "";

        }
    }
}

