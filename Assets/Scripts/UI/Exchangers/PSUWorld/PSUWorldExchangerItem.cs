using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PSUWorldExchangerItem : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField]
    public Image PSUImage;


    [SerializeField]
    public Image MaterialNeed;

    [SerializeField]
    public TMP_Text PSUPrice;
    [SerializeField]
    public TMP_Text PSUName;
    [SerializeField]
    public TMP_Text stats;

    [SerializeField]
    public TMP_Text Rarity;

    [SerializeField]
    private Image borderImage;

    [SerializeField]
    public PSUWorldExchangerBuy PSUBuy;

    public event Action<PSUWorldExchangerItem> OnItemClicked;

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
            PSUBuy.SelectItem(this);

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
        PSUBuy.ResetQuantity();
    }
    public void ResetData()
    {

        PSUImage.gameObject.SetActive(false);
    }
    public void SetData(Sprite PSUSprite, Sprite MaterialNeeds, string name, string rarity, string Price, string stat)//quantity has been removed
    {
        PSUImage.gameObject.SetActive(true);

        PSUImage.sprite = PSUSprite;
        MaterialNeed.sprite = MaterialNeeds;

        PSUName.text = name;
        PSUPrice.text = Price;
        stats.text = stat;
        Rarity.text = rarity;



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
