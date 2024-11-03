using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System;
using Inventory.Model;

namespace Shop.UI
{
    public class ShopItem2 : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
    {

        [SerializeField]
        private Image ImageBackground;


        [SerializeField]
        private Image itemImage;
        [SerializeField]
        private TMP_Text itemQuantity;//might be useful
        [SerializeField]
        public TMP_Text itemPrice;
        [SerializeField]
        private TMP_Text itemCategory;
        [SerializeField]
        private TMP_Text itemSpeed;
        [SerializeField]
        private TMP_Text itemCompatibility;
        [SerializeField]
        public TMP_Text itemName;
        [SerializeField]
        private Image borderImage;

       

        public event Action<ShopItem2> OnItemClicked, OnRightMouseBtnClick;
        public event Action<int> OnItemClickeds; // create new event for filtering

        public NumericUpDown numericUpDown;

     

        //private bool empty = true;

        public void Awake()
        {
            DeSelect();
            ResetData();
        }
        

        public void DeSelect()
        {
            borderImage.enabled = false;
            numericUpDown.buyButton.interactable = false;
        }
        public void ResetData()
        {

            itemImage.gameObject.SetActive(false);
            //empty = true;
        }
        public void SetData(Sprite sprite, Sprite imageBackground, string name, string Price, string category, string speed, string compatibility)//quantity has been removed
        {
            itemImage.gameObject.SetActive(true);
            itemImage.sprite = sprite;
            ImageBackground.sprite = imageBackground;
            itemName.text = name;
            itemPrice.text = Price;
            itemCategory.text = category;
            itemSpeed.text = speed;
            itemCompatibility.text = compatibility;
            //empty = false;
        }
        public void select()
        {
            borderImage.enabled = true;
            numericUpDown.buyButton.interactable = true;

        }

        public int temporaryIndex = 0; // Member variable to store the temporary index

        public void SetTemporaryIndex(int index)
        {
            temporaryIndex = index;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            
           
        
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                OnRightMouseBtnClick?.Invoke(this);
            }
            else
            {
                OnItemClicked?.Invoke(this);
                OnItemClickeds?.Invoke(temporaryIndex);
                numericUpDown.SelectItem(this);

                //OnItemPurchaseClicked?.Invoke(this);

            }
        }
    }
}
