using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

namespace Inventory.UI
{
    public class UIInventoryDesc : MonoBehaviour
    {
        [SerializeField]
        private Image itemImage;
        [SerializeField]
        private TMP_Text Title;
        [SerializeField]
        private TMP_Text Description;
        [SerializeField]
        private TMP_Text Category;

        public void Awake()
        {
            ResetDescription();
        }
        public void ResetDescription()
        {

            itemImage.gameObject.SetActive(false);
            Title.text = "";
            Description.text = "";
            Category.text = "";

        }

        public void SetDescription(Sprite sprite, string itemName, string itemDescription, string category)
        {
            itemImage.gameObject.SetActive(true);
            itemImage.sprite = sprite;
            Title.text = itemName;
            Description.text = itemDescription;
            Category.text = category;

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