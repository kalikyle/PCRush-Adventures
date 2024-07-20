using OtherWorld.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static OtherWorld.Model.OWInvSO;

public class HelmetBuy : MonoBehaviour
{
    private List<Helmets.UI.HelmetItem> toBuy = new List<Helmets.UI.HelmetItem>();
    public Helmets.Model.HelmetItemSO so;
    public OWInvSO data;
    public Button buyButton;
    private int value = 1;

    public void Start()
    {
        buyButton.onClick.AddListener(HandleThePurchase);
    }
    public void SelectItem(Helmets.UI.HelmetItem item)
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

    private void SelectNewOrDeselectPrevious(Helmets.UI.HelmetItem item)
    {
        Debug.Log("Item Called to Buy.");


        item.select();
        //selectedItems.Add(item);
        toBuy.Add(item);



        int index = item.temporaryIndex;
        Helmets.Model.Helmets sp = GetItemAt(index);
        //ItemPrice = sp.item.Price;
        //total = sp.item.Price;






        // Assuming Price is a field in ShopItem
        Debug.Log("Item added to Buy.");

        //UpdateBuyButtonInteractability();

    }

    public Helmets.Model.Helmets GetItemAt(int obj)
    {
        return so.Helmet[obj];
    }

    private void HandleThePurchase()
    {
        if (toBuy.Count > 0)
        {
            Helmets.UI.HelmetItem helmetItem = toBuy[0];
            if (helmetItem != null)
            {
                //you can place the condition for currency here



                HelmetBuys(helmetItem);




                Debug.Log("The item has been purchase");

                helmetItem.DeSelect();
                


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
    
    public void HelmetBuys(Helmets.UI.HelmetItem helmetItem)
    {
        if (helmetItem != null)
        {
            Debug.Log("Buyedd");
            OtherWorldItem inventoryItem = ConvertShopItemToDecorationItem(helmetItem);

            if (inventoryItem.isEmpty)
            {

                Debug.LogWarning("Null inventory item returned from conversion.");
            }
            else
            {
                int helmetPrice = Helmet.item.Price;
                if (GameManager.instance.PlayerMoney >= helmetPrice)
                {
                    GameManager.instance.ShowPopUpEquipments(inventoryItem);
                    GameManager.instance.AddItemToTransfer(inventoryItem);
                    Debug.Log("Item added to inventory ");

                    GameManager.instance.PlayerMoney -= helmetPrice;
                    GameManager.instance.SaveCharInfo(GameManager.instance.UserID, GameManager.instance.PlayerName);
                }
                else
                {
                    Debug.LogError("You dont have enough money");
                }


            }

        }
        else
        {
            Debug.LogWarning("Null shop item passed to HandlePurchase.");
        }

    }
    public Helmets.Model.Helmets Helmet;
    public OtherWorldItem ConvertShopItemToDecorationItem(Helmets.UI.HelmetItem shopItem)
    {

        List<Helmets.Model.Helmets> shopItems = so.Helmet;
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
            Helmets.Model.Helmets shpItem = GetItemAt(originalIndexs);

            if (!shpItem.isEmpty)
            {

                inventoryItem.item = ConvertHelmet(shpItem);
                Helmet = shpItem;

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

    public OtherWorldItemSO ConvertHelmet(Helmets.Model.Helmets shpItem)
    {
        OtherWorldItemSO inventoryItems = ScriptableObject.CreateInstance<OtherWorldItemSO>();
        inventoryItems.name = shpItem.item.Name;
        var spriteArray = GameManager.instance.SpriteCollections.Layers;
        int spriteIndex;

        inventoryItems.Name = shpItem.item.Name;
        inventoryItems.Health = shpItem.item.Health;
        inventoryItems.HealthRegen = shpItem.item.HealthRegen;
        spriteIndex = shpItem.item.SpriteIndex;

        if (spriteIndex >= 0 && spriteIndex < spriteArray.Count)
        {

            Texture2D texture = spriteArray[7].Textures[spriteIndex];
            Texture2D text2 = spriteArray[7].GetIcon(texture);
            // Create a sprite from the texture
            Sprite sprite = Sprite.Create(text2, new Rect(0, 0, text2.width, text2.height), Vector2.one * 0.5f);

            inventoryItems.ItemImage = sprite;
        }

        inventoryItems.Category = "Helmet";

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
