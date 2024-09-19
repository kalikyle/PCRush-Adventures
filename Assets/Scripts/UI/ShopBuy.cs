using Inventory.Model;
using Shop.Model;
using Shop.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static Decoration.Model.DecorSO;
using static Inventory.Model.PartsInventorySO;
//

public class ShopBuy : MonoBehaviour
{

    public TMP_Text displayText;
    public TMP_Text priceText;
    public Button incrementButton;
    public Button decrementButton;

    public Button buyButton;
    public Button EquipButton;
    public Button EquippedButton;

    //private List<Shop.UI.ShopItem> selectedItems = new List<Shop.UI.ShopItem>();
    private List<Shop.UI.ShopItem> toBuy = new List<Shop.UI.ShopItem>();
    public List<Shop.Model.ShopItem> filteredItems;


    public Shop.Model.ShopSO so;

    private int value = 1;
    public double ItemPrice = 0;
    private int minValue = 1; // Minimum value (decrement limit)
    private int maxValue = 99;// Maximum value (increment limit)
    double total;
    public bool ToggleTF = false;
    public bool ToggleBSE = false;
    public string filteredBSE;
    void Start()
    {
        incrementButton.onClick.AddListener(IncrementValue);
        decrementButton.onClick.AddListener(DecrementValue);
        buyButton.onClick.AddListener(HandleThePurchase);
        EquipButton.onClick.AddListener(HandleEquip);

        UpdateDisplay();
    }
    private void IncrementValue()
    {
        if (value < maxValue)
        {
            value++;
            UpdateDisplay();
            UpdatePriceDisplay();


        }
    }

    private void DecrementValue()
    {
        if (value > minValue)
        {
            value--;
            UpdateDisplay();
            UpdatePriceDisplay();


        }
    }

    private void UpdateDisplay()
    {

        if (displayText != null)
        {
            displayText.text = value.ToString();

        }

    }
    private void UpdatePriceDisplay()
    {
        total = ItemPrice * value;
        if (priceText != null)
        {
            priceText.text = "$" + total.ToString();

        }

    }


    void UpdateButton(string category)//need fix
    {
        // int temp = shopItem.temporaryIndex;
        GameManager.instance.UpdateShop(category);
       // buyButton.gameObject.SetActive(false);
       // EquippedButton.gameObject.SetActive(true);

    }
    


    // Update is called once per frame
    void Update()
    {
       
    }
    public void SelectItem(Shop.UI.ShopItem item)
    {

        if (!toBuy.Contains(item))// selectedItem = 0 item + 1
        {
            
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
        //selectedItems.Add(item);
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

                

              
               
                    HandlePurchase(shopItem);
                
                
                
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
    private void HandleEquip()
    {
        if (toBuy.Count > 0)
        {
            Shop.UI.ShopItem shopItem = toBuy[0];
            if (shopItem != null)
            {
                //you can place the condition for currency here
                HandleEquipButton(shopItem);
                
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
    public void previousItem(Shop.Model.ShopItem newShopItem)
    {
        string category = newShopItem.item.Category;

        if (GameManager.instance.equippedItemsByCategory.ContainsKey(category))
        {
            // Replace the previously equipped item with the purchased item
            Shop.Model.ShopItem previousItem = GameManager.instance.equippedItemsByCategory[category];

            GameManager.instance.equippedItemsByCategory[category] = newShopItem;
            previousItem.item.InUse = false; // Set the previous item's InUse to false
            
           
        }
        else
        {
            GameManager.instance.equippedItemsByCategory.Add(category, newShopItem);
        }

        //if((ToggleTF == true || ToggleTF == false) && ToggleBSE == false)
        //{
            UpdateButton(category);
        //}
        
    }


    public void HandleEquipButton(Shop.UI.ShopItem shopItem)
    {
        ChangeItemImages(shopItem);

        List<Shop.Model.ShopItem> shopItems = so.ShopItems;
        int tempIndexs;
        int originalIndex;
        int tempIndex;
        if (ToggleTF == true && ToggleBSE == false)//for filtered
        {
            Debug.Log("Toggle: True");
            Debug.Log(filteredItems.Count);
            tempIndex = shopItem.temporaryIndex;
            Shop.Model.ShopItem shpItem = filteredItems[tempIndex];
            shpItem.item.InUse = true;
            GameManager.instance.SaveUsedItemToFirestore(shpItem);
            previousItem(shpItem);

            

        }
        else if (ToggleTF == true && ToggleBSE == true) // for filtere buy, sold or equip
        {
            Debug.Log("Toggle: True");
            Debug.Log(filteredItems.Count);
            tempIndex = shopItem.temporaryIndex;
            Shop.Model.ShopItem shpItem = filteredItems[tempIndex];

            shpItem.item.InUse = true;
            GameManager.instance.SaveUsedItemToFirestore(shpItem);
            previousItem(shpItem);
        }
        else if (ToggleTF == false && ToggleBSE == false)// for all
        {
            tempToOriginalIndexMapping.Clear();
            tempIndexs = 0;
            originalIndex = 0;
            Debug.Log("Toggle: False");
            //Debug.Log(shopItems.Count);
            foreach (var item in shopItems)
            {

                tempToOriginalIndexMapping[tempIndexs] = originalIndex;
                tempIndexs++;
                originalIndex++;

            }

            tempIndex = shopItem.temporaryIndex;
            if (tempToOriginalIndexMapping.TryGetValue(tempIndex, out int originalIndexs))
            {
                // Use the original index to retrieve the ShopItem
                Shop.Model.ShopItem shpItem = GetItemAt(originalIndexs);
                shpItem.item.InUse = true;
                GameManager.instance.SaveUsedItemToFirestore(shpItem);
                previousItem(shpItem);

                
            }
            else
            {
                Debug.LogError("Mapping not found for temporary index: " + tempIndex);
            }
        }

        EquipButton.gameObject.SetActive(false);
        buyButton.gameObject.SetActive(false);
        EquippedButton.gameObject.SetActive(true);
    }
    public void HandlePurchase(Shop.UI.ShopItem shopItem)//need fix
    {
        
        
        List<Shop.Model.ShopItem> shopItems = so.ShopItems;
        int tempIndexs;
        int originalIndex;
        int tempIndex;
        
        if (ToggleTF == true && ToggleBSE == false)//for filtered
        {
            Debug.Log("Toggle: True");
            Debug.Log(filteredItems.Count);
            tempIndex = shopItem.temporaryIndex;
            Shop.Model.ShopItem shpItem = filteredItems[tempIndex];

            if (shpItem.item.Category == "Decorations")
            {
                DecorBuy(shopItem);
            }
            else
            {
                BuyItemAndCheck(shpItem, shopItem);
            }


        } else if (ToggleTF == true && ToggleBSE == true) // for filtered buy, sold or equip
        {
            Debug.Log("Toggle: True");
            Debug.Log(filteredItems.Count);
            tempIndex = shopItem.temporaryIndex;
            Shop.Model.ShopItem shpItem = filteredItems[tempIndex];

            if (shpItem.item.Category == "Decorations")
            {
                DecorBuy(shopItem);
            }
            else
            {
                BuyItemAndCheck(shpItem, shopItem);
            }
        }
        else if (ToggleTF == false && ToggleBSE == false)// for all
        {
            tempToOriginalIndexMapping.Clear();
            tempIndexs = 0;
            originalIndex = 0;
            Debug.Log("Toggle: False");
            //Debug.Log(shopItems.Count);
            foreach (var item in shopItems)
            {

                tempToOriginalIndexMapping[tempIndexs] = originalIndex;
                tempIndexs++;
                originalIndex++;

            }

            tempIndex = shopItem.temporaryIndex;
            if (tempToOriginalIndexMapping.TryGetValue(tempIndex, out int originalIndexs))
            {
                // Use the original index to retrieve the ShopItem
                Shop.Model.ShopItem shpItem = GetItemAt(originalIndexs);

                if (shpItem.item.Category == "Decorations")
                {
                    DecorBuy(shopItem);
                }
                else
                {
                    BuyItemAndCheck(shpItem,shopItem);
                }
            }
            else
            {
                Debug.LogError("Mapping not found for temporary index: " + tempIndex);
            }
        }
       

    }

    private void BuyItemAndCheck(Shop.Model.ShopItem shpItem, Shop.UI.ShopItem shopItem)
    {
        if (GameManager.instance.PlayerMoney >= shpItem.item.Price)
        {
            ChangeItemImages(shopItem);
            ItemsBuy(shpItem);
            GameManager.instance.PlayerMoney -= shpItem.item.Price;
            GameManager.instance.SaveCharInfo(GameManager.instance.UserID, GameManager.instance.PlayerName);
            GameManager.instance.ShowPopUpItems(shpItem);
        }
        else
        {
            //Debug.LogError("You dont have enough money");
            GameManager.instance.ShowFloatingText("You don't have enough coins");
        }
    }

    private void ItemsBuy(Shop.Model.ShopItem shpItem)
    {
        shpItem.item.Sold = true;
        shpItem.item.InUse = true;
        GameManager.instance.SaveItemToFirestore(shpItem);
        GameManager.instance.SaveUsedItemToFirestore(shpItem);

        previousItem(shpItem);

        EquipButton.gameObject.SetActive(false);
        buyButton.gameObject.SetActive(false);
        EquippedButton.gameObject.SetActive(true);
    }

    public void DecorBuy(Shop.UI.ShopItem shopItem)
    {
        if (shopItem != null)
        {

            DecorationItem inventoryItem = ConvertShopItemToDecorationItem(shopItem);
            //GameManager.Instance.SaveInventoryItem(inventoryItem);

            if (inventoryItem.isEmpty)
            {
                Debug.LogWarning("Null inventory item returned from conversion.");
            }
            else
            {

                if (GameManager.instance.PlayerMoney >= total)
                {

                    GameManager.instance.AddItemToTransfer(inventoryItem);

                    GameManager.instance.PlayerMoney -= (int)total;
                    GameManager.instance.SaveCharInfo(GameManager.instance.UserID, GameManager.instance.PlayerName);
                    
                    Debug.Log("Item added to inventory ");
                    GameManager.instance.ShowPopUpItems(inventoryItem);
                    value = 1;
                    displayText.text = value.ToString();
                    UpdatePriceDisplay();


                }
                else
                {
                    //Debug.LogError("You dont have enough money");
                    GameManager.instance.ShowFloatingText("You don't have enough coins");
                }
            }

        }
        else
        {
            Debug.LogWarning("Null shop item passed to HandlePurchase.");
        }

    }

    public DecorationItem ConvertShopItemToDecorationItem(Shop.UI.ShopItem shopItem)
    {

        List<Shop.Model.ShopItem> shopItems = so.ShopItems;
        DecorationItem inventoryItem = new DecorationItem();

        int tempIndexs;
        int originalIndex;
        int tempIndex;

        if (ToggleTF == true)
        {
            Debug.Log("Toggle: True");
            Debug.Log(filteredItems.Count);
            tempIndex = shopItem.temporaryIndex;
            Shop.Model.ShopItem shpItem = filteredItems[tempIndex];

            if (!shpItem.isEmpty)
            {
                inventoryItem.item = shpItem.item;
            }
            else
            {
                Debug.Log("ShopItem is empty");
            }
        }
        else
        {
            tempToOriginalIndexMapping.Clear();
            tempIndexs = 0;
            originalIndex = 0;
            Debug.Log("Toggle: False");
            //Debug.Log(shopItems.Count);
            foreach (var item in shopItems)
            {

                tempToOriginalIndexMapping[tempIndexs] = originalIndex;
                tempIndexs++;
                originalIndex++;

            }

            tempIndex = shopItem.temporaryIndex;
            if (tempToOriginalIndexMapping.TryGetValue(tempIndex, out int originalIndexs))
            {
                // Use the original index to retrieve the ShopItem
                Shop.Model.ShopItem shpItem = GetItemAt(originalIndexs);

                if (!shpItem.isEmpty)
                {
                    inventoryItem.item = shpItem.item;
                }
                else
                {
                    Debug.Log("ShopItem is empty");
                }
            }
            else
            {
                Debug.LogError("Mapping not found for temporary index: " + tempIndex);
            }
        }


        // Use the methods in the ShopItem class to retrieve the ItemSO and quantity.
        inventoryItem.quantity = Convert.ToInt32(value.ToString());


        // You can set other properties as needed
        return inventoryItem;

    }
    private Dictionary<int, int> tempToOriginalIndexMapping = new Dictionary<int, int>();
   

    public void ChangeItemImages(Shop.UI.ShopItem shopItem)
    {
        List < Shop.Model.ShopItem> shopItems = so.ShopItems;
        int tempIndexs;
        int originalIndex;
        int tempIndex;
             if (ToggleTF == true)//for filtered
             {
                 Debug.Log("Toggle: True");
                 Debug.Log(filteredItems.Count);
                 tempIndex = shopItem.temporaryIndex;
                 Shop.Model.ShopItem shpItem = filteredItems[tempIndex];

                    if(shpItem.item.Category == "Monitor")
                    {
                    GameManager.instance.Monitor.sprite = shpItem.item.ItemImage;
                    }

                    if (shpItem.item.Category == "Keyboard")
                    {
                        GameManager.instance.Keyboard.sprite = shpItem.item.ItemImage;
                    }

                    if(shpItem.item.Category == "Mouse")
                    {
                    GameManager.instance.Mouse.sprite = shpItem.item.ItemImage;
                    }
                    if (shpItem.item.Category == "MousePad")
                    {
                        GameManager.instance.MousePad.sprite = shpItem.item.ItemImage;
                    }

            if (shpItem.item.Category == "Desk")
                    {
                    GameManager.instance.Desk.sprite = shpItem.item.ItemImage;
                    }

                    if(shpItem.item.Category == "Background")
                    {
                    GameManager.instance.Background.sprite = shpItem.item.ItemImage;
                    }

             }
             else // for all
             {
                 tempToOriginalIndexMapping.Clear();
                 tempIndexs = 0;
                 originalIndex = 0;
                 Debug.Log("Toggle: False");
                 //Debug.Log(shopItems.Count);
                 foreach (var item in shopItems)
                 {

                     tempToOriginalIndexMapping[tempIndexs] = originalIndex;
                     tempIndexs++;
                     originalIndex++;

                 }

                 tempIndex = shopItem.temporaryIndex;
                 if (tempToOriginalIndexMapping.TryGetValue(tempIndex, out int originalIndexs))
                 {
                     // Use the original index to retrieve the ShopItem
                     Shop.Model.ShopItem shpItem = GetItemAt(originalIndexs);


                        if (shpItem.item.Category == "Monitor")
                        {
                            GameManager.instance.Monitor.sprite = shpItem.item.ItemImage;
                        }

                        if (shpItem.item.Category == "Keyboard")
                        {
                            GameManager.instance.Keyboard.sprite = shpItem.item.ItemImage;
                        }

                        if (shpItem.item.Category == "Mouse")
                        {
                            GameManager.instance.Mouse.sprite = shpItem.item.ItemImage;
                        }

                        if (shpItem.item.Category == "MousePad")
                        {
                            GameManager.instance.MousePad.sprite = shpItem.item.ItemImage;
                        }

                        if (shpItem.item.Category == "Desk")
                        {
                            GameManager.instance.Desk.sprite = shpItem.item.ItemImage;
                        }

                        if (shpItem.item.Category == "Background")
                        {
                            GameManager.instance.Background.sprite = shpItem.item.ItemImage;
                        }

                 }
                 else
                 {
                     Debug.LogError("Mapping not found for temporary index: " + tempIndex);
                 }
             }
        
    }

    //this is for conversion
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
