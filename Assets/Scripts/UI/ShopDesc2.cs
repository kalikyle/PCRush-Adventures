using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System;


namespace Shop.UI
{
    public class ShopDesc2 : MonoBehaviour
    {
        [SerializeField]
        private Image itemImage;
        [SerializeField]
        private TMP_Text Title;
        [SerializeField]
        private TMP_Text Compatibility; 
        [SerializeField]
        private TMP_Text Speed;
        [SerializeField]
        private TMP_Text category;

        public void Awake()
        {
            ResetDescription();
            //ss
        }
        public void ResetDescription()
        {

            itemImage.gameObject.SetActive(false);
            Title.text = "";
            Speed.text = "";
            Compatibility.text = "";
            Speed.text = "";
            category.text = "";

        }
        public void SetDescription(Sprite sprite, string itemName, string itemCategory, string itemSpeed, string itemCompat)
        {
            itemImage.gameObject.SetActive(true);
            itemImage.sprite = sprite;
            Title.text = itemName;
            Speed.text = itemSpeed;
            Compatibility.text = itemCompat;
            category.text = itemCategory;

        }
        public void Show()
        {

            gameObject.SetActive(true);

        }
        public void Hide()
        {
            gameObject.SetActive(false);

        }

    }
}

