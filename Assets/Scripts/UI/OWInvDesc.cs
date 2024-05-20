using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace OtherWorld.UI
{
    public class OWInvDesc : MonoBehaviour
    {
        [SerializeField]
        private Image itemImage;
        [SerializeField]
        private TMP_Text itemName;

        [SerializeField]
        private TMP_Text Category;
       
        [SerializeField]
        private TMP_Text Perks;

        public void Awake()
        {
            ResetDescription();
        }
        public void ResetDescription()
        {

            itemImage.gameObject.SetActive(false);
            itemName.text = "";
            //Rarity.text = "";
            Category.text = "";
            Perks.text = "";

        }

        public void SetDescription(Sprite sprite, string itemName, string category, string perks)
        {
            itemImage.gameObject.SetActive(true);
            itemImage.sprite = sprite;
            this.itemName.text = itemName;

            Category.text = category;
           
            Perks.text = perks;

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

