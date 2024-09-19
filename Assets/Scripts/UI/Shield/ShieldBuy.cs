using OtherWorld.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static OtherWorld.Model.OWInvSO;

public class ShieldBuy : MonoBehaviour
{
    private List<Shield.UI.ShieldItem> toBuy = new List<Shield.UI.ShieldItem>();
    public Shield.Model.ShieldItemSO so;
    public OWInvSO data;
    public Button buyButton;
    //private int value = 1;
    public List<Shield.Model.Shields> filteredItems;

    public void Start()
    {
        buyButton.onClick.AddListener(HandleThePurchase);
    }
    public void SelectItem(Shield.UI.ShieldItem item)
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

    private void SelectNewOrDeselectPrevious(Shield.UI.ShieldItem item)
    {
        Debug.Log("Item Called to Buy.");


        item.select();
        //selectedItems.Add(item);
        toBuy.Add(item);



        int index = item.temporaryIndex;
        Shield.Model.Shields sp = GetItemAt(index);
        //ItemPrice = sp.item.Price;
        //total = sp.item.Price;






        // Assuming Price is a field in ShopItem
        Debug.Log("Item added to Buy.");

        //UpdateBuyButtonInteractability();

    }

    public Shield.Model.Shields GetItemAt(int obj)
    {
        return so.Shield[obj];
    }

    private void HandleThePurchase()
    {
        if (toBuy.Count > 0)
        {
            Shield.UI.ShieldItem helmetItem = toBuy[0];
            if (helmetItem != null)
            {
                //you can place the condition for currency here



                ShieldBuys(helmetItem);




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

    public void ShieldBuys(Shield.UI.ShieldItem helmetItem)
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
                int helmetPrice = Shield.item.Price;
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
    public Shield.Model.Shields Shield;
    public OtherWorldItem ConvertShopItemToDecorationItem(Shield.UI.ShieldItem shopItem)
    {

        List<Shield.Model.Shields> shopItems = so.Shield;
        OtherWorldItem inventoryItem = new OtherWorldItem();

        int tempIndex;

        tempIndex = shopItem.temporaryIndex;
        Shield.Model.Shields shpItem = filteredItems[tempIndex];

        if (!shpItem.isEmpty)
        {

            inventoryItem.item = ConvertShield(shpItem);
            Shield = shpItem;

        }
        else
        {
            Debug.Log("ShopItem is empty");
        }



        inventoryItem.quantity = 1;

        return inventoryItem;

    }

    public OtherWorldItemSO ConvertShield(Shield.Model.Shields shpItem)
    {
        OtherWorldItemSO inventoryItems = ScriptableObject.CreateInstance<OtherWorldItemSO>();
        inventoryItems.name = shpItem.item.Name;
        var spriteArray = GameManager.instance.SpriteCollections.Layers;
        int spriteIndex;

        inventoryItems.Name = shpItem.item.Name;
        inventoryItems.ManaRegen = shpItem.item.ManaRegen;
        inventoryItems.CriticalChance = shpItem.item.CriticalChance;
        spriteIndex = shpItem.item.SpriteIndex;

        if (spriteIndex >= 0 && spriteIndex < spriteArray.Count)
        {

            Texture2D texture = spriteArray[1].Textures[spriteIndex];
            Texture2D text2 = spriteArray[1].GetIcon(texture);
            // Create a sprite from the texture
            Sprite sprite = Sprite.Create(text2, new Rect(0, 0, text2.width, text2.height), Vector2.one * 0.5f);

            inventoryItems.ItemImage = sprite;
        }

        inventoryItems.Category = "Shield";

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
