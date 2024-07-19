using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RAMWorldExchangerItem : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField]
    public Image RAMImage;


    [SerializeField]
    public Image MaterialNeed;

    [SerializeField]
    public TMP_Text RAMPrice;
    [SerializeField]
    public TMP_Text RAMName;
    [SerializeField]
    public TMP_Text stats;

    [SerializeField]
    public TMP_Text Rarity;

    [SerializeField]
    private Image borderImage;

    [SerializeField]
    public RAMWorldExchangerBuy RAMBuy;

    public event Action<RAMWorldExchangerItem> OnItemClicked;

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
            RAMBuy.SelectItem(this);

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
        RAMBuy.ResetQuantity();
    }
    public void ResetData()
    {

        RAMImage.gameObject.SetActive(false);
    }
    public void SetData(Sprite RAMSprite, Sprite MaterialNeeds, string name, string rarity, string Price, string stat)//quantity has been removed
    {
        RAMImage.gameObject.SetActive(true);

        RAMImage.sprite = RAMSprite;
        MaterialNeed.sprite = MaterialNeeds;

        RAMName.text = name;
        RAMPrice.text = Price;
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
