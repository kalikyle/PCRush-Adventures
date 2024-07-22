using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GPUExchangerItem : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField]
    public Image GPUImage;


    [SerializeField]
    public Image MaterialNeed;

    [SerializeField]
    public TMP_Text GPUPrice;
    [SerializeField]
    public TMP_Text GPUName;
    [SerializeField]
    public TMP_Text stats;

    [SerializeField]
    public TMP_Text Rarity;

    [SerializeField]
    private Image borderImage;

    [SerializeField]
    public GPUExchangerBuy GPUBuy;

    public event Action<GPUExchangerItem> OnItemClicked;

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
            GPUBuy.SelectItem(this);

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
        GPUBuy.ResetQuantity();
    }
    public void ResetData()
    {

        GPUImage.gameObject.SetActive(false);
    }
    public void SetData(Sprite GPUSprite, Sprite MaterialNeeds, string name, string rarity, string Price, string stat)//quantity has been removed
    {
        GPUImage.gameObject.SetActive(true);

        GPUImage.sprite = GPUSprite;
        MaterialNeed.sprite = MaterialNeeds;

        GPUName.text = name;
        GPUPrice.text = Price;
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
