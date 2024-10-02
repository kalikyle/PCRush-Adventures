using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuyersItem : MonoBehaviour, IPointerClickHandler
{
    // Start is called before the first frame update

    [SerializeField]
    private Image ClientImage;

    [SerializeField]
    private TMP_Text ClientName;

    [SerializeField]
    private TMP_Text Description;

    [SerializeField]
    private TMP_Text Requirements;

    [SerializeField]
    private TMP_Text Price;

    [SerializeField]
    private TMP_Text Experience;

    public event Action<BuyersItem> OnMissionClicked;

    public void OnPointerClick(PointerEventData eventData)
    {
        OnMissionClicked.Invoke(this);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMissionData(Sprite clientImage, string clientname, string descriptions, string requirements, int price, int exp)
    {
        this.ClientImage.sprite = clientImage;
        this.ClientName.text = clientname;
        this.Description.text = descriptions;
        this.Requirements.text = requirements;
        this.Price.text = "$" + price + "";
        this.Experience.text = exp + "";

    }
    public int temporaryIndex = 0;
    public void SetTemporaryIndex(int index)
    {
        temporaryIndex = index;
    }


}
