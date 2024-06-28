using Assets.PixelHeroes.Scripts.CollectionScripts;
using Ink.Runtime;
using OtherWorld.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static Decoration.Model.DecorSO;
using static Inventory.Model.PartsInventorySO;
using static OtherWorld.Model.OWInvSO;
using Unity.Collections.LowLevel.Unsafe;


public class SwordBuy : MonoBehaviour
{
    private List<Swords.UI.SwordsItem> toBuy = new List<Swords.UI.SwordsItem>();
    public Swords.Model.SwordItemsSO so;
    public OWInvSO data;
    public Button buyButton;
    private int value = 1;
    
    public void Start()
    {
        buyButton.onClick.AddListener(HandleThePurchase);
    }
    public void SelectItem(Swords.UI.SwordsItem item)
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

    private void SelectNewOrDeselectPrevious(Swords.UI.SwordsItem item)
    {
        Debug.Log("Item Called to Buy.");


        item.select();
        //selectedItems.Add(item);
        toBuy.Add(item);



        int index = item.temporaryIndex;
        Swords.Model.Swords sp = GetItemAt(index);
        //ItemPrice = sp.item.Price;
        //total = sp.item.Price;






        // Assuming Price is a field in ShopItem
        Debug.Log("Item added to Buy.");

        //UpdateBuyButtonInteractability();

    }

    public Swords.Model.Swords GetItemAt(int obj)
    {
        return so.Sword[obj];
    }

    private void HandleThePurchase()
    {
        if (toBuy.Count > 0)
        {
            Swords.UI.SwordsItem swordItem = toBuy[0];
            if (swordItem != null)
            {
                //you can place the condition for currency here



                SwordBuys(swordItem);



                //ConvertShopItemToInventoryItem(shopItem);
                Debug.Log("The item has been purchase");

                //Debug.Log("The item is " + ConvertShopItemToInventoryItem(shopItem).item.Name);
                //Debug.Log("Quantity: " + ConvertShopItemToInventoryItem(shopItem).quantity);
                swordItem.DeSelect();
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
    private Dictionary<int, int> tempToOriginalIndexMapping = new Dictionary<int, int>();
    //public void HandlePurchase(Armors.UI.SwordsItem shopItem)//need fix
    //{
    //    List<Armors.Model.Armors> shopItems = so.Sword;
    //    int tempIndexs;
    //    int originalIndex;
    //    int tempIndex;

    //    tempToOriginalIndexMapping.Clear();
    //    tempIndexs = 0;
    //    originalIndex = 0;
    //    Debug.Log("Toggle: False");
    //    //Debug.Log(shopItems.Count);
    //    foreach (var item in shopItems)
    //    {

    //        tempToOriginalIndexMapping[tempIndexs] = originalIndex;
    //        tempIndexs++;
    //        originalIndex++;

    //    }

    //    tempIndex = shopItem.temporaryIndex;
    //    if (tempToOriginalIndexMapping.TryGetValue(tempIndex, out int originalIndexs))
    //    {

    //        Armors.Model.Armors shpItem = GetItemAt(originalIndexs);

    //        SwordBuys(shopItem);





    //    }



    //}

    public void SwordBuys(Swords.UI.SwordsItem swordItem)
    {
        if (swordItem != null)
        {
            Debug.Log("Buyedd");
            OtherWorldItem inventoryItem = ConvertShopItemToDecorationItem(swordItem);

            if (inventoryItem.isEmpty)
            {

                Debug.LogWarning("Null inventory item returned from conversion.");
            }
            else
            {
                
                //data.AddItem(inventoryItem);
                //data.AddItemList(inventoryItem.item);
                GameManager.instance.AddItemToTransfer(inventoryItem);
                Debug.Log("Item added to inventory ");
            }

        }
        else
        {
            Debug.LogWarning("Null shop item passed to HandlePurchase.");
        }

    }

    public OtherWorldItem ConvertShopItemToDecorationItem(Swords.UI.SwordsItem shopItem)
    {
       
        List<Swords.Model.Swords> shopItems = so.Sword;
        OtherWorldItem inventoryItem = new OtherWorldItem();
        
        int tempIndexs;
        int originalIndex;
        int tempIndex;

        
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
            Swords.Model.Swords shpItem = GetItemAt(originalIndexs);

                if (!shpItem.isEmpty)
                {

                inventoryItem.item = ConvertSword(shpItem);
               

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



        inventoryItem.quantity = 1;

        return inventoryItem;

    }

    public OtherWorldItemSO ConvertSword(Swords.Model.Swords shpItem)
    {
        OtherWorldItemSO inventoryItems = ScriptableObject.CreateInstance<OtherWorldItemSO>();
        inventoryItems.name = shpItem.item.Name;
        var spriteArray = GameManager.instance.SpriteCollections.Layers;
        int spriteIndex;

        inventoryItems.Name = shpItem.item.Name;
        inventoryItems.Attack = shpItem.item.attack;
        spriteIndex = shpItem.item.SpriteIndex;

        if (spriteIndex >= 0 && spriteIndex < spriteArray.Count)
        {

            Texture2D texture = spriteArray[8].Textures[spriteIndex];
            Texture2D text2 = spriteArray[8].GetIcon(texture);
            // Create a sprite from the texture
            Sprite sprite = Sprite.Create(text2, new Rect(0, 0, text2.width, text2.height), Vector2.one * 0.5f);

            inventoryItems.ItemImage = sprite;
        }

        inventoryItems.Category = "Sword";

        //if want not stackable
        inventoryItems.IsStackable = false;
        inventoryItems.MaxStackableSize = 1;
        inventoryItems.SpriteIndex = shpItem.item.SpriteIndex;
        // if stackable
        //inventoryItems.IsStackable = true;
        //inventoryItems.MaxStackableSize = 99;

        return inventoryItems;
    }
}