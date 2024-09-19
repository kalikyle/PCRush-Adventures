using Armor.Model;
using OtherWorld.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static OtherWorld.Model.OWInvSO;

public class ArmorBuy : MonoBehaviour
{
    private List<Armor.UI.ArmorsItem> toBuy = new List<Armor.UI.ArmorsItem>();
    public Armor.Model.ArmorItemsSO so;
    public OWInvSO data;
    public Button buyButton;
    //private int value = 1;
    public List<Armor.Model.Armors> filteredItems;

    public void Start()
    {
        buyButton.onClick.AddListener(HandleThePurchase);
    }
    public void SelectItem(Armor.UI.ArmorsItem item)
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

    private void SelectNewOrDeselectPrevious(Armor.UI.ArmorsItem item)
    {
        Debug.Log("Item Called to Buy.");


        item.select();
        //selectedItems.Add(item);
        toBuy.Add(item);


        // Assuming Price is a field in ShopItem
        Debug.Log("Item added to Buy.");

        //UpdateBuyButtonInteractability();

    }

    public Armor.Model.Armors GetItemAt(int obj)
    {
        return so.Armor[obj];
    }

    private void HandleThePurchase()
    {
        if (toBuy.Count > 0)
        {
            Armor.UI.ArmorsItem swordItem = toBuy[0];
            if (swordItem != null)
            {
                //you can place the condition for currency here

               
                    ArmorBuys(swordItem);
                    Debug.Log("The item has been purchase");

                

              



                
                swordItem.DeSelect();
                


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

    public void ArmorBuys(Armor.UI.ArmorsItem swordItem)
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

                int ArmorPrice = Armor.item.Price;
                if (GameManager.instance.PlayerMoney >= ArmorPrice)
                {
                    GameManager.instance.AddItemToTransfer(inventoryItem);   
                    Debug.Log("The item has been purchase");
                    GameManager.instance.ShowPopUpEquipments(inventoryItem);
                    GameManager.instance.PlayerMoney -= ArmorPrice;
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
    private Armor.Model.Armors Armor;
    public OtherWorldItem ConvertShopItemToDecorationItem(Armor.UI.ArmorsItem shopItem)
    {

        List<Armor.Model.Armors> shopItems = so.Armor;
        OtherWorldItem inventoryItem = new OtherWorldItem();

        int tempIndex;

        tempIndex = shopItem.temporaryIndex;
        Armor.Model.Armors shpItem = filteredItems[tempIndex];

        if (!shpItem.isEmpty)
        {

            inventoryItem.item = ConvertSword(shpItem);
            Armor = shpItem;

        }
        else
        {
            Debug.Log("ShopItem is empty");
        }


        inventoryItem.quantity = 1;

        return inventoryItem;

    }

    public OtherWorldItemSO ConvertSword(Armor.Model.Armors shpItem)
    {
        OtherWorldItemSO inventoryItems = ScriptableObject.CreateInstance<OtherWorldItemSO>();
        inventoryItems.name = shpItem.item.Name;
        var spriteArray = GameManager.instance.SpriteCollections.Layers;
        int spriteIndex;

        inventoryItems.Name = shpItem.item.Name;
        inventoryItems.Armor = shpItem.item.Armor;
        inventoryItems.Mana = shpItem.item.Mana;
        spriteIndex = shpItem.item.SpriteIndex;

        if (spriteIndex >= 0 && spriteIndex < spriteArray.Count)
        {

            Texture2D texture = spriteArray[3].Textures[spriteIndex];
            Texture2D text2 = spriteArray[3].GetIcon(texture);
            // Create a sprite from the texture
            Sprite sprite = Sprite.Create(text2, new Rect(0, 0, text2.width, text2.height), Vector2.one * 0.5f);

            inventoryItems.ItemImage = sprite;
        }

        inventoryItems.Category = "Armor";

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
