using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PartsInventory.UI { 
public class UIPartsInventoryDesc : MonoBehaviour
{
    [SerializeField]
    private Image itemImage;
    [SerializeField]
    private TMP_Text itemName;
    
    [SerializeField]
    private TMP_Text Category;
    [SerializeField]
    private TMP_Text Rarity;
    [SerializeField]
    private TMP_Text Perks; 
        
    [SerializeField]
    private TMP_Text SellingPrice;//

        public void Awake()
    {
        ResetDescription();
    }
    public void ResetDescription()
    {

        itemImage.gameObject.SetActive(false);
        itemName.text = "";
        Rarity.text = "";
        Category.text = "";
        Perks.text = "";
        SellingPrice.text = "";


        }

    public void SetDescription(Sprite sprite, string itemName,  string category, string rarity, string perks, string Sellingprice)
    {
        itemImage.gameObject.SetActive(true);
        itemImage.sprite = sprite;
        this.itemName.text = itemName;
        
        Category.text = category;
        Rarity.text = rarity;
        Perks.text = perks;
        SellingPrice.text = Sellingprice;

            switch (rarity.ToLower())
            {
                case "common":
                    Rarity.color = Color.green;
                    break;
                case "rare":
                    Rarity.color = Color.blue;
                    break;
                case "epic":
                    Rarity.color = new Color(1f, 0.5f, 0f); // orange color
                    break;
                case "legend":
                    Rarity.color = Color.red;
                    break;
                default:
                    Rarity.color = Color.white; // default color if rarity is unknown
                    break;
            }

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

