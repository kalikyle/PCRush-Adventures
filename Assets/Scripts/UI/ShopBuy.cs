using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShopBuy : MonoBehaviour
{
    public Button buyButton;

    private List<Shop.UI.ShopItem> selectedItems = new List<Shop.UI.ShopItem>();
    private List<Shop.UI.ShopItem> toBuy = new List<Shop.UI.ShopItem>();
    public List<Shop.Model.ShopItem> filteredItems;

    public Shop.Model.ShopSO so;
    public double ItemPrice = 0;
    double total;
    public bool ToggleTF = false;
    void Start()
    {
        buyButton.onClick.AddListener(HandleThePurchase);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SelectItem(Shop.UI.ShopItem item)
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
    private void SelectNewOrDeselectPrevious(Shop.UI.ShopItem item)
    {
        Debug.Log("Item Called to Buy.");
        item.select();

        selectedItems.Add(item);
        toBuy.Add(item);


        if (ToggleTF == true)
        {
            int tempIndex;
            tempIndex = item.temporaryIndex;
            Shop.Model.ShopItem sp = filteredItems[tempIndex];
            ItemPrice = sp.item.Price;
            total = sp.item.Price;
        }
        else
        {
            int index = item.temporaryIndex;
            Shop.Model.ShopItem sp = GetItemAt(index);
            ItemPrice = sp.item.Price;
            total = sp.item.Price;
        }





        // Assuming Price is a field in ShopItem
        Debug.Log("Item added to Buy.");

        //UpdateBuyButtonInteractability();

    }
    public Shop.Model.ShopItem GetItemAt(int obj)// this will return the ShopItem SO
    {
        return so.ShopItems[obj];
    }
    private void HandleThePurchase()
    {
        if (toBuy.Count > 0)
        {
            Shop.UI.ShopItem shopItem = toBuy[0];
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

    //this is for 
    //public InventoryItem ConvertShopItemToInventoryItem(Shop.UI.ShopItem shopItem)
    //{

    //    List<Inventory.Model.ShopItem> shopItems = so.ShopItems;
    //    InventoryItem inventoryItem = new InventoryItem();

    //    int tempIndexs;
    //    int originalIndex;
    //    int tempIndex;
    //    if (ToggleTF == true)
    //    {
    //        Debug.Log("Toggle: True");
    //        Debug.Log(filteredItems.Count);
    //        tempIndex = shopItem.temporaryIndex;
    //        Inventory.Model.ShopItem shpItem = filteredItems[tempIndex];

    //        if (!shpItem.isEmpty)
    //        {
    //            inventoryItem.item = shpItem.item;
    //        }
    //        else
    //        {
    //            Debug.Log("ShopItem is empty");
    //        }
    //    }
    //    else
    //    {
    //        tempToOriginalIndexMapping.Clear();
    //        tempIndexs = 0;
    //        originalIndex = 0;
    //        Debug.Log("Toggle: False");
    //        //Debug.Log(shopItems.Count);
    //        foreach (var item in shopItems)
    //        {

    //            tempToOriginalIndexMapping[tempIndexs] = originalIndex;
    //            tempIndexs++;
    //            originalIndex++;

    //        }

    //        tempIndex = shopItem.temporaryIndex;
    //        if (tempToOriginalIndexMapping.TryGetValue(tempIndex, out int originalIndexs))
    //        {
    //            // Use the original index to retrieve the ShopItem
    //            Inventory.Model.ShopItem shpItem = GetItemAt(originalIndexs);

    //            if (!shpItem.isEmpty)
    //            {
    //                inventoryItem.item = shpItem.item;
    //            }
    //            else
    //            {
    //                Debug.Log("ShopItem is empty");
    //            }
    //        }
    //        else
    //        {
    //            Debug.LogError("Mapping not found for temporary index: " + tempIndex);
    //        }
    //    }


    //    // Use the methods in the ShopItem class to retrieve the ItemSO and quantity.
    //    inventoryItem.quantity = Convert.ToInt32(value.ToString());


    //    // You can set other properties as needed
    //    return inventoryItem;

    //}
}
