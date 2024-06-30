using Exchanger.Model.CPUWorld;
using OtherWorld.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static OtherWorld.Model.OWInvSO;

public class CPUWorldExchangerBuy : MonoBehaviour
{
    private List<CPUWorldExchangerItem> toBuy = new List<CPUWorldExchangerItem>();
    public CPUWorldItemSO so;
    public OWInvSO data;
    public Button buyButton;
    private int value = 1;

    public void Start()
    {
        buyButton.onClick.AddListener(HandleThePurchase);
    }
    public void SelectItem(CPUWorldExchangerItem item)
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

    private void SelectNewOrDeselectPrevious(CPUWorldExchangerItem item)
    {
        Debug.Log("Item Called to Buy.");


        item.select();
        //selectedItems.Add(item);
        toBuy.Add(item);



        int index = item.temporaryIndex;
        CPUs sp = GetItemAt(index);
        //ItemPrice = sp.item.Price;
        //total = sp.item.Price;






        // Assuming Price is a field in ShopItem
        Debug.Log("Item added to Buy.");

        //UpdateBuyButtonInteractability();

    }

    public CPUs GetItemAt(int obj)
    {
        return so.Procies[obj];
    }

    private void HandleThePurchase()
    {
        if (toBuy.Count > 0)
        {
            CPUWorldExchangerItem swordItem = toBuy[0];
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
    //public void HandlePurchase(Armors.UI.CPUWorldExchangerItem shopItem)//need fix
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

    public void SwordBuys(CPUWorldExchangerItem swordItem)
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

    public OtherWorldItem ConvertShopItemToDecorationItem(CPUWorldExchangerItem shopItem)
    {

        List<CPUs> shopItems = so.Procies;
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
            CPUs shpItem = GetItemAt(originalIndexs);

            if (!shpItem.isEmpty)
            {

                //inventoryItem.item = ConvertSword(shpItem);


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

    //public OtherWorldItemSO ConvertSword(CPUs shpItem)
    //{
    //    //OtherWorldItemSO inventoryItems = ScriptableObject.CreateInstance<OtherWorldItemSO>();
    //    //inventoryItems.name = shpItem.item.Name;
    //    //var spriteArray = GameManager.instance.SpriteCollections.Layers;
    //    //int spriteIndex;

    //    //inventoryItems.Name = shpItem.item.Name;
    //    //inventoryItems.Attack = shpItem.item.attack;
    //    //spriteIndex = shpItem.item.SpriteIndex;

    //    //if (spriteIndex >= 0 && spriteIndex < spriteArray.Count)
    //    //{

    //    //    Texture2D texture = spriteArray[8].Textures[spriteIndex];
    //    //    Texture2D text2 = spriteArray[8].GetIcon(texture);
    //    //    // Create a sprite from the texture
    //    //    Sprite sprite = Sprite.Create(text2, new Rect(0, 0, text2.width, text2.height), Vector2.one * 0.5f);

    //    //    inventoryItems.ItemImage = sprite;
    //    //}

    //    //inventoryItems.Category = "Sword";

    //    ////if want not stackable
    //    //inventoryItems.IsStackable = false;
    //    //inventoryItems.MaxStackableSize = 1;
    //    //inventoryItems.SpriteIndex = shpItem.item.SpriteIndex;
    //    //// if stackable
    //    ////inventoryItems.IsStackable = true;
    //    ////inventoryItems.MaxStackableSize = 99;

    //    //return inventoryItems;
    //}
}
