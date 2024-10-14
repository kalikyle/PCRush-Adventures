using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CaseWorldExchangerItem : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField]
    public Image CaseImage;


    [SerializeField]
    public Image MaterialNeed;

    [SerializeField]
    public TMP_Text CasePrice;
    [SerializeField]
    public TMP_Text CaseName;
    [SerializeField]
    public TMP_Text stats;

    [SerializeField]
    public TMP_Text Rarity;

    [SerializeField]
    private Image borderImage;

    [SerializeField]
    public CaseWorldExchangerBuy CaseBuy;

    public event Action<CaseWorldExchangerItem> OnItemClicked;

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
            CaseBuy.SelectItem(this);

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
        CaseBuy.ResetQuantity();
    }
    public void ResetData()
    {

        CaseImage.gameObject.SetActive(false);
    }
    public void SetData(Sprite CaseSprite, Sprite MaterialNeeds, string name, string rarity, string Price, string stat)//quantity has been removed
    {
        CaseImage.gameObject.SetActive(true);

        CaseImage.sprite = CaseSprite;
        MaterialNeed.sprite = MaterialNeeds;

        CaseName.text = name;
        CasePrice.text = Price;
        stats.text = stat;
        Rarity.text = rarity;

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
