using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonitorBuy : MonoBehaviour
{
    public Button buyButton;

    private List<Shop.UI.MonitorShopItem> selectedItems = new List<Shop.UI.MonitorShopItem>();
    private List<Shop.UI.MonitorShopItem> toBuy = new List<Shop.UI.MonitorShopItem>();
    public List<Shop.Model.MonitorShopItem> filteredItems;

    public Shop.Model.MonitorShopSO so;
    public double ItemPrice = 0;
    double total;
    void Start()
    {
        buyButton.onClick.AddListener(HandleThePurchase);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SelectItem(Shop.UI.MonitorShopItem item)
    {

        if (!selectedItems.Contains(item))// selectedItem = 0 item + 1
        {
            // DeselectItem(item);
            SelectNewOrDeselectPrevious(item);

        }
        else
        {
            toBuy.Clear();
            SelectNewOrDeselectPrevious(item);
        }

    }
    private void SelectNewOrDeselectPrevious(Shop.UI.MonitorShopItem item)
    {
        Debug.Log("Item Called to Buy.");
        item.select();

        selectedItems.Add(item);
        toBuy.Add(item);


        //if (ToggleTF == true)
        //{
        //    int tempIndex;
        //    tempIndex = item.temporaryIndex;
        //    Inventory.Model.ShopItem sp = filteredItems[tempIndex];
        //    ItemPrice = sp.item.Price;
        //    total = sp.item.Price;
        //}
        //else
        //{
            int index = item.temporaryIndex;
            Shop.Model.MonitorShopItem sp = GetItemAt(index);
            ItemPrice = sp.item.Price;
            total = sp.item.Price;
        //}





        // Assuming Price is a field in ShopItem
        Debug.Log("Item added to Buy.");

        //UpdateBuyButtonInteractability();

    }
    public Shop.Model.MonitorShopItem GetItemAt(int obj)// this will return the ShopItem SO
    {
        return so.ShopItems[obj];
    }
    private void HandleThePurchase()
    {
        if (toBuy.Count > 0)
        {
            Shop.UI.MonitorShopItem shopItem = toBuy[0];
            if (shopItem != null)
            {
                //you can place the condition for currency here
               // HandlePurchase(shopItem);
                //ConvertShopItemToInventoryItem(shopItem);
                Debug.Log("The item has been purchase");
                //Debug.Log("The item is " + ConvertShopItemToInventoryItem(shopItem).item.Name);
                //Debug.Log("Quantity: " + ConvertShopItemToInventoryItem(shopItem).quantity);
                shopItem.DeSelect();
                //value = 1;
                //displayText.text = value.ToString();
                //priceText.text = "$" + ConvertShopItemToInventoryItem(shopItem).item.Price.ToString();


            }
            else
            {
                Debug.LogWarning("Null shop item in toBuy list.");
            }
        }
        else
        {
            Debug.Log("No item to purchase. The toBuy list is empty.");
        }
        toBuy.Clear();

    }
}
