using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace Swords.UI
{
    public class SwordsItem : MonoBehaviour, IPointerEnterHandler
    {
        [SerializeField]
        public Image SwordImage;

        [SerializeField]
        public TMP_Text SwordPrice;
        [SerializeField]
        public TMP_Text SwordName;
        [SerializeField]
        public TMP_Text attack;
        
        [SerializeField]
        private Image borderImage;

        [SerializeField]
        public SwordBuy swordBuy;

        public event Action<SwordsItem> OnItemClicked;
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
                //OnItemClickeds?.Invoke(temporaryIndex);
                swordBuy.SelectItem(this);

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

            SwordImage.gameObject.SetActive(false);
        }
        public void SetData(Sprite SwordSprite, string name, string Price, string attacks)//quantity has been removed
        {
            SwordImage.gameObject.SetActive(true);
           
            SwordImage.sprite = SwordSprite;
            
            SwordName.text = name;
            SwordPrice.text = Price;
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
