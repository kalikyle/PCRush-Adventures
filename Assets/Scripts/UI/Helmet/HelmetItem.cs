using Helmets.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Helmets.UI
{
    public class HelmetItem : MonoBehaviour, IPointerEnterHandler
    {
        [SerializeField]
        public Image HelmetImage;

        [SerializeField]
        public TMP_Text HelmetPrice;
        [SerializeField]
        public TMP_Text HelmetName;
        [SerializeField]
        public TMP_Text attack;

        [SerializeField]
        private Image borderImage;

        [SerializeField]
        public HelmetBuy helmetBuy;

        public event Action<HelmetItem> OnItemClicked;

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                //OnRightMouseBtnClick?.Invoke(this);
            }
            else
            {
                OnItemClicked?.Invoke(this);
                //OnItemClickeds?.Invoke(temporaryIndex);
                helmetBuy.SelectItem(this);

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

            HelmetImage.gameObject.SetActive(false);
        }
        public void SetData(Sprite HelmetSprite, string name, string Price, string attacks)//quantity has been removed
        {
            HelmetImage.gameObject.SetActive(true);

            HelmetImage.sprite = HelmetSprite;

            HelmetName.text = name;
            HelmetPrice.text = Price;
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
