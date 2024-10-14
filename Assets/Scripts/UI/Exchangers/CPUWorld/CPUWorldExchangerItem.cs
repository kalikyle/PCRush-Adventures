using Swords.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CPUWorldExchangerItem : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField]
    public Image CPUImage;


    [SerializeField]
    public Image MaterialNeed;

    [SerializeField]
    public TMP_Text CPUPrice;
    [SerializeField]
    public TMP_Text CPUName;
    [SerializeField]
    public TMP_Text stats;

    [SerializeField]
    public TMP_Text Rarity;

    [SerializeField]
    private Image borderImage;

    [SerializeField]
    public CPUWorldExchangerBuy CPUBuy;

    public event Action<CPUWorldExchangerItem> OnItemClicked;

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
            CPUBuy.SelectItem(this);

            //OnItemPurchaseClicked?.Invoke(this);

        }
    }

    public void DeSelect()
    {
        borderImage.enabled = false;
        
        //monitorBuy.buyButton.interactable = false;
    }

    public void ResetQuantity()
    {
        CPUBuy.ResetQuantity();
    }
    public void ResetData()
    {

        CPUImage.gameObject.SetActive(false);
    }
    public void SetData(Sprite CPUSprite, Sprite MaterialNeeds, string name, string rarity, string Price, string stat)//quantity has been removed
    {
        CPUImage.gameObject.SetActive(true);

        CPUImage.sprite = CPUSprite;
        MaterialNeed.sprite = MaterialNeeds;

        CPUName.text = name;
        CPUPrice.text = Price;
        stats.text = stat;
        Rarity.text = rarity;

        // Set the color of the Rarity text based on the rarity value
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
