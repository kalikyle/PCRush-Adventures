using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Decoration.UI
{
    public class DecorItem : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler

    {
        [SerializeField]
        private Image itemImage;
        [SerializeField]
        private TMP_Text itemQuantity;
        [SerializeField]
        private TMP_Text itemName;
        [SerializeField]
        private TMP_Text itemCategory;
        [SerializeField]
        private Image borderImage;


        //this whole code is for the border only when its clicked and also when dragged
        public event Action<DecorItem> OnItemClicked, OnItemDroppedOn, OnItemBeginDrag, OnItemEndDrag
            , OnRightMouseBtnClick;
        public event Action<int> OnItemClickeds, OnItemRightClicked; // create new event for filtering

        private bool empty = true;

        public void Awake()
        {
            //DontDestroyOnLoad(itemImage);
            //ResetData();
            // DeSelect();
        }
        public void ResetData()
        {

            itemImage.gameObject.SetActive(false);
            empty = true;
        }
        public void DeSelect()
        {
            borderImage.enabled = false;
        }
        public void SetData(Sprite sprite, int quantity, string name, string category)
        {
            itemImage.gameObject.SetActive(true);
            itemImage.sprite = sprite;
            itemQuantity.text = quantity + "";
            itemName.text = name;
            itemCategory.text = category;
            empty = false;
        }
        public void select()
        {
            borderImage.enabled = true;
        }
        // Member variable to store the temporary index
        public int temporaryIndex = 0;
        public void SetTemporaryIndex(int index)
        {
            GameManager.instance.tempindex = index;
            temporaryIndex = index;
        }

        public void OnPointerClick(PointerEventData pointerdata)
        {

           
        }

        public void OnPointerEnter(PointerEventData pointerData)
        {
            if (pointerData.button == PointerEventData.InputButton.Right)
            {
                OnRightMouseBtnClick?.Invoke(this);
                OnItemRightClicked?.Invoke(temporaryIndex);
            }
            else
            {
                OnItemClicked?.Invoke(this);
                OnItemClickeds?.Invoke(temporaryIndex);
            }
        }
    }
}
