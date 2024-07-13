using Assets.PixelHeroes.Scripts.CharacterScrips;
using Assets.PixelHeroes.Scripts.CollectionScripts;
using Assets.PixelHeroes.Scripts.EditorScripts;
using Shop.Model;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Swords.UI;
using Swords.Model;
using Armor.UI;
using Armor.Model;

public class EquipmentsController : MonoBehaviour
{
    [SerializeField]
    private SwordsPage swordsPage;

    [SerializeField]
    private SwordItemsSO swordsData;

    [SerializeField]
    private ArmorsPage armorsPage;

    [SerializeField]
    private ArmorItemsSO armorsData;

    //public List<LayerEditor> Layers;
    //public CharacterBuilder CharacterBuilder;



    //[SerializeField]
    //public ShopBuy shopBuy;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ArmorsPrepareUI()
    {

        armorsPage.InitializedShop(ArmorsGetUsedSlotsCount());

    }

    private void SwordsPrepareUI()
    {
        swordsPage.InitializedShop(SwordsGetUsedSlotsCount());
        

    }

    public int SwordsGetUsedSlotsCount()//this will only used the slots with items
    {
        int usedSlots = 0;
        foreach (var item in swordsData.Sword)
        {
            if (!item.isEmpty)
            {
                usedSlots++;
            }
        }
        return usedSlots;
    }

    public void SwordsToggleALLButton()
    {
        //shopBuy.ToggleTF = false;
        //shopBuy.ToggleBSE = false;
        // Toggle the state
        swordsPage.ResetSelection();
        swordsPage.ClearItems();
        swordsPage.InitializedShop(SwordsGetUsedSlotsCount() /* GameManager.Instance.shopSize*/);

        

        SwordsShowAllCategory();

    }

    public void SwordsOpenShop()
    {


        // Call the methods after the delay

        SwordsPrepareUI();
        SwordsToggleALLButton();
        swordsPage.Show();
        swordsPage.ResetSelection();
    }

    public void SwordsShowAllCategory()
    {
        //shopBuy.ToggleTF = false;
        //shopBuy.ToggleBSE = false;
        //itemsShownInAllCategory.Clear();
        var spriteArray = GameManager.instance.SpriteCollections.Layers;


        var nonEmptyItems = swordsData.GetCurrentInventoryState().Where(item => !item.Value.isEmpty);

        int displayedItemsCount = 0;
        foreach (var item in nonEmptyItems)
        {
            ////ShopUpdate(item.Value.item.InUse, item.Value.item.Sold);
            //if (displayedItemsCount >= /*GameManager.instance.ShopSize*/GetUsedSlotsCount())
            //    break;

            ////swordsPage.UpdateData(item.Key, item.Value.item.SpriteIndex, item.Value.item.Name, item.Value.item.Price.ToString(), item.Value.item.Category, item.Value.item.InUse, item.Value.item.Sold);
            ////itemsShownInAllCategory.Add(item.Value); // Add to items shown in "All" category

            //displayedItemsCount++;


            if (displayedItemsCount >= SwordsGetUsedSlotsCount())
                break;

            int spriteIndex = item.Value.item.SpriteIndex;
            


            //foreach (var layers in spriteArray)
            //{
            //    if (spriteArray[5].Name == "Weapon")
            //    {


                    if (spriteIndex >= 0 && spriteIndex < spriteArray.Count)
                    {


                        //int specificX = 0; // The x-coordinate of the top-left corner of the area
                        //int specificY = 0;
                        //float specificWidth = 64; // The width of the area
                        //float specificHeight = 64;
                        //Rect zoomedRect = new Rect(specificX, specificY, specificWidth, specificHeight);
                        //Texture2D texture = spriteArray[spriteIndex].Textures[spriteIndex];

                        //// Create a sprite from the texture
                        //Sprite sprite = Sprite.Create(texture, zoomedRect, new Vector2(0.5f, 0.5f));


                        Texture2D texture = spriteArray[8].Textures[spriteIndex];
                        Texture2D text2 = spriteArray[8].GetIcon(texture);
                        // Create a sprite from the texture
                        Sprite sprite = Sprite.Create(text2, new Rect(0, 0, text2.width, text2.height), Vector2.one * 0.5f);


                string perks = "";
                // Check each perk property and accumulate non-zero values
                if (item.Value.item.AttackDamage != 0)
                {
                    perks += "Attack Damage +" + item.Value.item.AttackDamage + "\n";
                }
                if (item.Value.item.AttackSpeed != 0)
                {
                    perks += "Attack Speed +" + item.Value.item.AttackSpeed + "\n";
                }
                


                swordsPage.UpdateData(item.Key, sprite, item.Value.item.Name, item.Value.item.Price.ToString(), perks);
                        // Now you can use the sprite as needed
                        // For example, you can assign it to an image component
                        // imageComponent.sprite = sprite;

                        displayedItemsCount++;
                    }
            //    }
            //}
        }
    }




    public int ArmorsGetUsedSlotsCount()//this will only used the slots with items
    {
        int usedSlots = 0;
        foreach (var item in armorsData.Armor)
        {
            if (!item.isEmpty)
            {
                usedSlots++;
            }
        }
        return usedSlots;
    }

    public void ArmorsToggleALLButton()
    {
        //shopBuy.ToggleTF = false;
        //shopBuy.ToggleBSE = false;
        // Toggle the state
        armorsPage.ResetSelection();
        armorsPage.ClearItems();
       armorsPage.InitializedShop(ArmorsGetUsedSlotsCount() /* GameManager.Instance.shopSize*/);



        ArmorsShowAllCategory();

    }

    public void ArmorsOpenShop()
    {


        // Call the methods after the delay

        ArmorsPrepareUI();
        ArmorsToggleALLButton();
        armorsPage.Show();
        armorsPage.ResetSelection();
    }

    public void ArmorsShowAllCategory()
    {
        //shopBuy.ToggleTF = false;
        //shopBuy.ToggleBSE = false;
        //itemsShownInAllCategory.Clear();
        var spriteArray = GameManager.instance.SpriteCollections.Layers;


        var nonEmptyItems = armorsData.GetCurrentInventoryState().Where(item => !item.Value.isEmpty);

        int displayedItemsCount = 0;
        foreach (var item in nonEmptyItems)
        {
            ////ShopUpdate(item.Value.item.InUse, item.Value.item.Sold);
            //if (displayedItemsCount >= /*GameManager.instance.ShopSize*/GetUsedSlotsCount())
            //    break;

            ////swordsPage.UpdateData(item.Key, item.Value.item.SpriteIndex, item.Value.item.Name, item.Value.item.Price.ToString(), item.Value.item.Category, item.Value.item.InUse, item.Value.item.Sold);
            ////itemsShownInAllCategory.Add(item.Value); // Add to items shown in "All" category

            //displayedItemsCount++;


            if (displayedItemsCount >= ArmorsGetUsedSlotsCount())
                break;

            int spriteIndex = item.Value.item.SpriteIndex;



            //foreach (var layers in spriteArray)
            //{
            //    if (spriteArray[5].Name == "Weapon")
            //    {


            if (spriteIndex >= 0 && spriteIndex < spriteArray.Count)
            {


                //int specificX = 0; // The x-coordinate of the top-left corner of the area
                //int specificY = 0;
                //float specificWidth = 64; // The width of the area
                //float specificHeight = 64;
                //Rect zoomedRect = new Rect(specificX, specificY, specificWidth, specificHeight);
                //Texture2D texture = spriteArray[spriteIndex].Textures[spriteIndex];

                //// Create a sprite from the texture
                //Sprite sprite = Sprite.Create(texture, zoomedRect, new Vector2(0.5f, 0.5f));


                Texture2D texture = spriteArray[3].Textures[spriteIndex];
                Texture2D text2 = spriteArray[3].GetIcon(texture);
                // Create a sprite from the texture
                Sprite sprite = Sprite.Create(text2, new Rect(0, 0, text2.width, text2.height), Vector2.one * 0.5f);

                string perks = "";
                // Check each perk property and accumulate non-zero values
                if (item.Value.item.Armor != 0)
                {
                    perks += "Armor +" + item.Value.item.Armor + "\n";
                }
                if (item.Value.item.Mana != 0)
                {
                    perks += "Mana +" + item.Value.item.Mana + "\n";
                }



                armorsPage.UpdateData(item.Key, sprite, item.Value.item.Name, item.Value.item.Price.ToString(), perks);
                // Now you can use the sprite as needed
                // For example, you can assign it to an image component
                // imageComponent.sprite = sprite;

                displayedItemsCount++;
            }
            //    }
            //}
        }
    }


}
