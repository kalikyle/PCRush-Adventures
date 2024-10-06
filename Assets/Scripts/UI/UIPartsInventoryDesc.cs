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

