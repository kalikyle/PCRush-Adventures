using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Shop.UI
{
    public class ShopItem : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
    {
        [SerializeField]
        private Image itemImage;
        [SerializeField]
        public TMP_Text itemPrice;
        [SerializeField]
        public TMP_Text itemName;
        [SerializeField]
        public TMP_Text itemCategory;
        [SerializeField]
        public Image ImageCategory;
        [SerializeField]
        private Image borderImage;


        [SerializeField]
        private Button Buy;
        [SerializeField]
        private Button Equip;
        [SerializeField]
        private Button Equipped;



        public event Action<ShopItem> OnItemClicked;// OnRightMouseBtnClick;
        public event Action<int> OnItemClickeds; // create new event for filtering

        public ShopBuy shopBuy;


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
        public void SetData(Sprite sprite, Sprite ImageCat, string name, string Price, string category)//quantity has been removed
        {
            itemImage.gameObject.SetActive(true);
            ImageCategory.sprite = ImageCat;
            itemImage.sprite = sprite;
            itemName.text = name;
            itemPrice.text = Price;
            itemCategory.text = category;
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
                shopBuy.SelectItem(this);

                //OnItemPurchaseClicked?.Invoke(this);

            }
        }
    }
}
