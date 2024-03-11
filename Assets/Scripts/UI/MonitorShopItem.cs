using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Shop.UI
{
    public class MonitorShopItem : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
    {
        [SerializeField]
        private Image itemImage;
        [SerializeField]
        public TMP_Text itemPrice;
        [SerializeField]
        public TMP_Text itemName;
        [SerializeField]
        private Image borderImage;

        public event Action<MonitorShopItem> OnItemClicked, OnRightMouseBtnClick;
        public event Action<int> OnItemClickeds; // create new event for filtering

        public MonitorBuy monitorBuy;


        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        public void Awake()
        {
            DeSelect();
            ResetData();
        }


        public void DeSelect()
        {
            borderImage.enabled = false;
            //monitorBuy.buyButton.interactable = false;
        }
        public void ResetData()
        {

            itemImage.gameObject.SetActive(false);
        }
        public void SetData(Sprite sprite, string name, string Price, string category)//quantity has been removed
        {
            itemImage.gameObject.SetActive(true);
            itemImage.sprite = sprite;
            itemName.text = name;
            itemPrice.text = Price;
            //empty = false;
        }
        public void select()
        {
            borderImage.enabled = true;
            //monitorBuy.buyButton.interactable = true;

        }
        public int temporaryIndex = 0; // Member variable to store the temporary index

        public void SetTemporaryIndex(int index)
        {
            temporaryIndex = index;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            //throw new NotImplementedException();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                //OnRightMouseBtnClick?.Invoke(this);
            }
            else
            {
                OnItemClicked?.Invoke(this);
                OnItemClickeds?.Invoke(temporaryIndex);
                monitorBuy.SelectItem(this);

                //OnItemPurchaseClicked?.Invoke(this);

            }
        }
    }
}
