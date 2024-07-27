
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace Shield.UI
{
    public class ShieldItem : MonoBehaviour, IPointerEnterHandler
    {
        [SerializeField]
        public Image ShieldImage;

        [SerializeField]
        public TMP_Text ShieldPrice;
        [SerializeField]
        public TMP_Text ShieldName;
        [SerializeField]
        public TMP_Text attack;

        [SerializeField]
        private Image borderImage;

        [SerializeField]
        public ShieldBuy shieldBuy;

        public event Action<ShieldItem> OnItemClicked;
        public event Action<int> OnItemClickeds;

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
                shieldBuy.SelectItem(this);

                //OnItemPurchaseClicked?.Invoke(this);

            }
        }

        public void DeSelect()
        {
            borderImage.enabled = false;
            //monitorBuy.buyButton.interactable = false;
        }
        public void ResetData()
        {

            ShieldImage.gameObject.SetActive(false);
        }
        public void SetData(Sprite ShieldSprite, string name, string Price, string attacks)//quantity has been removed
        {
            ShieldImage.gameObject.SetActive(true);

            ShieldImage.sprite = ShieldSprite;

            ShieldName.text = name;
            ShieldPrice.text = Price;
            attack.text = attacks;



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
    }
}
