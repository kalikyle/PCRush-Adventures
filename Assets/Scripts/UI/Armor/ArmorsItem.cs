using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Armor.UI
{
    public class ArmorsItem : MonoBehaviour, IPointerEnterHandler
    {
        [SerializeField]
        public Image ArmorImage;

        [SerializeField]
        public TMP_Text ArmorPrice;
        [SerializeField]
        public TMP_Text ArmorName;
        [SerializeField]
        public TMP_Text armor;

        [SerializeField]
        private Image borderImage;

        [SerializeField]
        public ArmorBuy armorBuy;

        public event Action<ArmorsItem> OnItemClicked;
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
                armorBuy.SelectItem(this);

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

            ArmorImage.gameObject.SetActive(false);
        }
        public void SetData(Sprite ArmorSprite, string name, string Price, string attacks)//quantity has been removed
        {
            ArmorImage.gameObject.SetActive(true);

            ArmorImage.sprite = ArmorSprite;

            ArmorName.text = name;
            ArmorPrice.text = Price;
            armor.text = attacks;



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

