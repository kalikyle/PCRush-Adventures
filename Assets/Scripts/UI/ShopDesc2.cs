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
        private TMP_Text Description;
        [SerializeField]
        private TMP_Text Price;
        [SerializeField]
        private TMP_Text category;

        public void Awake()
        {
            ResetDescription();
        }
        public void ResetDescription()
        {

            itemImage.gameObject.SetActive(false);
            Title.text = "";
            Price.text = "";
            Description.text = "";
            category.text = "";

        }
        public void SetDescription(Sprite sprite, string itemName, string itemDescription, string itemCategory, string itemPrice)
        {
            itemImage.gameObject.SetActive(true);
            itemImage.sprite = sprite;
            Title.text = itemName;
            Description.text = itemDescription;
            Price.text = "$" + itemPrice;
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

