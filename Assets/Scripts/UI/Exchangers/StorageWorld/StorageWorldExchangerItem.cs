using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StorageWorldExchangerItem : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField]
    public Image StorageImage;


    [SerializeField]
    public Image MaterialNeed;

    [SerializeField]
    public TMP_Text StoragePrice;
    [SerializeField]
    public TMP_Text StorageName;
    [SerializeField]
    public TMP_Text stats;

    [SerializeField]
    public TMP_Text Rarity;

    [SerializeField]
    private Image borderImage;

    [SerializeField]
    public StorageWorldExchangerBuy StorageBuy;

    public event Action<StorageWorldExchangerItem> OnItemClicked;

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
            StorageBuy.SelectItem(this);

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
        StorageBuy.ResetQuantity();
    }
    public void ResetData()
    {

        StorageImage.gameObject.SetActive(false);
    }
    public void SetData(Sprite StorageSprite, Sprite MaterialNeeds, string name, string rarity, string Price, string stat)//quantity has been removed
    {
        StorageImage.gameObject.SetActive(true);

        StorageImage.sprite = StorageSprite;
        MaterialNeed.sprite = MaterialNeeds;

        StorageName.text = name;
        StoragePrice.text = Price;
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
