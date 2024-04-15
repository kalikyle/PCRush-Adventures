using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace PC.UI
{
    public class PCItem : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField]
        private Image PCImage;

        [SerializeField]
        private Image BackgroundImage;

        [SerializeField]
        private TMP_Text PCName;

        [SerializeField]
        private Image borderImage;

        public event Action<PCItem> OnItemClicked, OnRightMouseBtnClick;

        //private bool empty = true;

        public void Awake()
        {
            DeSelect();
            //ResetData();
        }


        public void DeSelect()
        {
            borderImage.enabled = false;

        }
        public void ResetData()
        {

            //PCImage.gameObject.SetActive(false);
            //empty = true;
        }
        public void SetData(Sprite backimage, Sprite sprite, string PCName)
        {
           // this.PCImage.gameObject.SetActive(true);
            this.PCImage.sprite = sprite;
            this.PCName.text = PCName;
            this.BackgroundImage.sprite = backimage;
            //empty = false;
        }
        public void Select()
        {
            borderImage.enabled = true;
        }
        

        public void OnPointerClick(PointerEventData pointerData)
        {
            //if(empty) { return; }
            if (pointerData.button == PointerEventData.InputButton.Right)
            {

                OnRightMouseBtnClick.Invoke(this);
            }
            else
            {
                OnItemClicked.Invoke(this);
            }
        }
        //public void UpdateUIWithLoadedPCSO(PCSO loadedPCSO)
        //{
        //    // Update UI elements with loaded PCSO data
        //    // Example: Set text on a UI element
        //    PCName.text = loadedPCSO.PCName;

        //    // Example: Display the loaded PCImage
        //    PCImage.sprite = loadedPCSO.PCImage;

        //    // Update other UI elements as needed...
        ////}
    }



}