using Orders.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuyersPage : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private BuyersItem itemPrefab;
    [SerializeField]
    private RectTransform contentPanel;

    public TMP_Text Time;

    public List<BuyersItem> ListofMissions = new List<BuyersItem>();

    public event Action<int> OnOpenPCInventory;


   

    void Start()
    {
        
    }

    public void UpdateTimer(string timeText)
    {
        Time.text = timeText;
    }

    public void InitializedCPU(int inventorysize)
    {
        for (int i = 0; i < inventorysize; i++)
        {
            BuyersItem uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);

            uiItem.transform.SetParent(contentPanel);
            uiItem.transform.localScale = new Vector3(1, 1, 1);
            ListofMissions.Add(uiItem);

            uiItem.SetTemporaryIndex(i);
            uiItem.OnMissionClicked += HandleItemSelection;
        }
    }

    private void HandleItemSelection(BuyersItem item)
    {
        int index = ListofMissions.IndexOf(item);
        if (index == -1)
        {
            return;
        }
        //Debug.LogError("clicked:" + index);
        OnOpenPCInventory?.Invoke(index);

    }

    public void UpdateData(int itemIndex, Sprite clientImage, string clientname, string descriptions, string requirements, int price, int exp)
    {
        if (ListofMissions.Count > itemIndex)
        {
            ListofMissions[itemIndex].SetMissionData(clientImage, clientname, descriptions, requirements, price, exp);

        }

    }

    // Update is called once per frame
    void Update()
    {
        
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
