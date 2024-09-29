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
using Helmets.UI;
using Helmets.Model;
using Shield.UI;
using Shield.Model;
using static OtherWorld.Model.OWInvSO;
using System;

public class EquipmentsController : MonoBehaviour
{
    [SerializeField]
    private SwordsPage swordsPage;

    [SerializeField]
    private SwordItemsSO swordsData;
    public Dictionary<int, int> SwordtempToOriginalIndexMapping = new Dictionary<int, int>();

    [SerializeField]
    private ArmorsPage armorsPage;
  
    [SerializeField]
    private ArmorItemsSO armorsData;
    public Dictionary<int, int> ArmortempToOriginalIndexMapping = new Dictionary<int, int>();

    [SerializeField]
    private HelmetPage helmetPage;

    [SerializeField]
    private HelmetItemSO helmetData;
    public Dictionary<int, int> HelmettempToOriginalIndexMapping = new Dictionary<int, int>();

    [SerializeField]
    private ShieldPage shieldPage;

    [SerializeField]
    private ShieldItemSO shieldData;
    public Dictionary<int, int> ShieldtempToOriginalIndexMapping = new Dictionary<int, int>();




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

    private void HelmetPrepareUI()
    {
        helmetPage.InitializedShop(HelmetsGetUsedSlotsCount());
    }

    private void ShieldPrepareUI()
    {
        shieldPage.InitializedShop(ShieldsGetUsedSlotsCount());
    }


    //public void SwordsToggleALLButton()
    //{

    //    swordsPage.ResetSelection();
    //    swordsPage.ClearItems();
    //    swordsPage.InitializedShop(SwordsGetUsedSlotsCount() /* GameManager.Instance.shopSize*/);



    //    SwordsShowAllCategory();

    //}
    //public void SwordsOpenShop()
    //{


    //    // Call the methods after the delay

    //    SwordsPrepareUI();
    //    SwordsToggleALLButton();
    //    swordsPage.Show();
    //    swordsPage.ResetSelection();
    //}
    //public void SwordsShowAllCategory()
    //{

    //    var spriteArray = GameManager.instance.SpriteCollections.Layers;


    //    var nonEmptyItems = swordsData.GetCurrentInventoryState().Where(item => !item.Value.isEmpty);

    //    int displayedItemsCount = 0;
    //    foreach (var item in nonEmptyItems)
    //    {


    //        if (displayedItemsCount >= SwordsGetUsedSlotsCount())
    //            break;

    //        int spriteIndex = item.Value.item.SpriteIndex;


    //                if (spriteIndex >= 0 && spriteIndex < spriteArray.Count)
    //                {



    //                    Texture2D texture = spriteArray[8].Textures[spriteIndex];
    //                    Texture2D text2 = spriteArray[8].GetIcon(texture);
    //                    // Create a sprite from the texture
    //                    Sprite sprite = Sprite.Create(text2, new Rect(0, 0, text2.width, text2.height), Vector2.one * 0.5f);


    //                        string perks = "";
    //                        // Check each perk property and accumulate non-zero values
    //                        if (item.Value.item.AttackDamage != 0)
    //                        {
    //                            perks += "Attack Damage +" + item.Value.item.AttackDamage + "\n";
    //                        }
    //                        if (item.Value.item.AttackSpeed != 0)
    //                        {
    //                            perks += "Attack Speed +" + item.Value.item.AttackSpeed + "\n";
    //                        }



    //            swordsPage.UpdateData(item.Key, sprite, item.Value.item.Name, item.Value.item.Price.ToString(), perks);


    //                    displayedItemsCount++;
    //                }

    //    }
    //}
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
  
    public void SwordsToggleFiltered(int from, int to)
    {

        swordsPage.ResetSelection();
        swordsPage.ClearItems();
        swordsPage.InitializedShop(SwordsGetUsedSlotsCount() /* GameManager.Instance.shopSize*/);
        ShowItemsByIndexRange(from, to);
    }

    public void SwordsFilteredOpenShop(int from, int to)
    {


        // Call the methods after the delay
        SwordsPrepareUI();
        SwordsToggleFiltered(from, to);
        swordsPage.Show();
        swordsPage.ResetSelection();
    }

    private void ShowItemsByIndexRange(int from, int to)
    {

        swordsPage.ResetSelection();
        swordsPage.SwordBuy.filteredItems.Clear();
        SwordtempToOriginalIndexMapping.Clear();
        swordsPage.ClearItems(); // Clear the existing items in the UI
        
        // Validate the range
        if (from < 0 || to >= swordsData.Sword.Count || from > to)
        {
            Debug.LogError("Invalid index range.");
            return;
        }

        int originalIndex = 0;
        int tempIndex = 0;

        // Loop through the specified range of items
        for (int i = from; i <= to; i++)
        {
            var item = swordsData.Sword[i];

            if (!item.isEmpty)
            {
                // Add items to the filtered list and store the mapping
                swordsPage.SwordBuy.filteredItems.Add(item); // Then add to filteredItems

                SwordtempToOriginalIndexMapping[tempIndex] = originalIndex;

                var spriteArray = GameManager.instance.SpriteCollections.Layers;

                int spriteIndex = item.item.SpriteIndex;


                if (spriteIndex >= 0 && spriteIndex < spriteArray.Count)
                {



                    Texture2D texture = spriteArray[8].Textures[spriteIndex];
                    Texture2D text2 = spriteArray[8].GetIcon(texture);
                    // Create a sprite from the texture
                    Sprite sprite = Sprite.Create(text2, new Rect(0, 0, text2.width, text2.height), Vector2.one * 0.5f);


                    string perks = "";
                    // Check each perk property and accumulate non-zero values
                    if (item.item.AttackDamage != 0)
                    {
                        perks += "Attack Damage +" + item.item.AttackDamage + "\n";
                    }
                   

                    // Create a new filtered item
                    swordsPage.AddShopItem(sprite,item.item.Name, item.item.Price.ToString(), perks);
                    originalIndex++;
                    tempIndex++;
                }

               
            }
        }
    }

   
    //for Armor
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

    public void ArmorsToggleFiltered(int from, int to)
    {

        armorsPage.ResetSelection();
        armorsPage.ClearItems();
        armorsPage.InitializedShop(ArmorsGetUsedSlotsCount() /* GameManager.Instance.shopSize*/);
        ArmorShowItemsByIndexRange(from, to);
    }

    public void ArmorsFilteredOpenShop(int from, int to)
    {


        // Call the methods after the delay
        ArmorsPrepareUI();
        ArmorsToggleFiltered(from, to);
        armorsPage.Show();
        armorsPage.ResetSelection();
    }

    private void ArmorShowItemsByIndexRange(int from, int to)
    {

        armorsPage.ResetSelection();
        armorsPage.ArmorBuy.filteredItems.Clear();
        ArmortempToOriginalIndexMapping.Clear();
        armorsPage.ClearItems(); // Clear the existing items in the UI

        // Validate the range
        if (from < 0 || to >= armorsData.Armor.Count || from > to)
        {
            Debug.LogError("Invalid index range.");
            return;
        }

        int originalIndex = 0;
        int tempIndex = 0;

        // Loop through the specified range of items
        for (int i = from; i <= to; i++)
        {
            var item = armorsData.Armor[i];

            if (!item.isEmpty)
            {
                // Add items to the filtered list and store the mapping
                armorsPage.ArmorBuy.filteredItems.Add(item); // Then add to filteredItems

                ArmortempToOriginalIndexMapping[tempIndex] = originalIndex;

                var spriteArray = GameManager.instance.SpriteCollections.Layers;

                int spriteIndex = item.item.SpriteIndex;


                if (spriteIndex >= 0 && spriteIndex < spriteArray.Count)
                {

                    Texture2D texture = spriteArray[3].Textures[spriteIndex];
                    Texture2D text2 = spriteArray[3].GetIcon(texture);
                    // Create a sprite from the texture
                    Sprite sprite = Sprite.Create(text2, new Rect(0, 0, text2.width, text2.height), Vector2.one * 0.5f);


                    string perks = "";
                    // Check each perk property and accumulate non-zero values
                    if (item.item.Mana != 0)
                    {
                        perks += "Mana +" + item.item.Mana + "\n";
                    }
                    if (item.item.Armor != 0)
                    {
                        perks += "Armor +" + item.item.Armor + "\n";
                    }

                    // Create a new filtered item
                    armorsPage.AddShopItem(sprite, item.item.Name, item.item.Price.ToString(), perks);
                    originalIndex++;
                    tempIndex++;
                }


            }
        }
    }



    //for Helmet
    //public void HelmetsToggleALLButton()
    //{
    //    helmetPage.ResetSelection();
    //    helmetPage.ClearItems();
    //    helmetPage.InitializedShop(HelmetsGetUsedSlotsCount());
    //    HelmetsShowAllCategory();

    //}

    //public void HelmetsOpenShop()
    //{


    //    // Call the methods after the delay

    //    HelmetPrepareUI();
    //    HelmetsToggleALLButton();
    //    helmetPage.Show();
    //    helmetPage.ResetSelection();
    //}

    //public void HelmetsShowAllCategory()
    //{

    //    var spriteArray = GameManager.instance.SpriteCollections.Layers;


    //    var nonEmptyItems = helmetData.GetCurrentInventoryState().Where(item => !item.Value.isEmpty);

    //    int displayedItemsCount = 0;
    //    foreach (var item in nonEmptyItems)
    //    {



    //        if (displayedItemsCount >= HelmetsGetUsedSlotsCount())
    //            break;

    //        int spriteIndex = item.Value.item.SpriteIndex;






    //        if (spriteIndex >= 0 && spriteIndex < spriteArray.Count)
    //        {

    //            Texture2D texture = spriteArray[7].Textures[spriteIndex];
    //            Texture2D text2 = spriteArray[7].GetIcon(texture);
    //            // Create a sprite from the texture
    //            Sprite sprite = Sprite.Create(text2, new Rect(0, 0, text2.width, text2.height), Vector2.one * 0.5f);

    //            string perks = "";
    //            // Check each perk property and accumulate non-zero values
    //            if (item.Value.item.Health != 0)
    //            {
    //                perks += "Health +" + item.Value.item.Health + "\n";
    //            }
    //            if (item.Value.item.HealthRegen != 0)
    //            {
    //                perks += "Health Regen +" + item.Value.item.HealthRegen + "\n";
    //            }

    //            helmetPage.UpdateData(item.Key, sprite, item.Value.item.Name, item.Value.item.Price.ToString(), perks);

    //            displayedItemsCount++;
    //        }

    //    }
    //}







    //for Shield
    public int HelmetsGetUsedSlotsCount()//this will only used the slots with items
    {
        int usedSlots = 0;
        foreach (var item in helmetData.Helmet)
        {
            if (!item.isEmpty)
            {
                usedSlots++;
            }
        }
        return usedSlots;
    }

    public void HelmetsToggleFiltered(int from, int to)
    {

        helmetPage.ResetSelection();
        helmetPage.ClearItems();
        helmetPage.InitializedShop(HelmetsGetUsedSlotsCount() /* GameManager.Instance.shopSize*/);
        HelmetShowItemsByIndexRange(from, to);
    }

    public void HelmetsFilteredOpenShop(int from, int to)
    {


        // Call the methods after the delay
        HelmetPrepareUI();
        HelmetsToggleFiltered(from, to);
        helmetPage.Show();
        helmetPage.ResetSelection();
    }

    private void HelmetShowItemsByIndexRange(int from, int to)
    {

        helmetPage.ResetSelection();
        helmetPage.HelmetBuy.filteredItems.Clear();
        HelmettempToOriginalIndexMapping.Clear();
        helmetPage.ClearItems(); // Clear the existing items in the UI

        // Validate the range
        if (from < 0 || to >= helmetData.Helmet.Count || from > to)
        {
            Debug.LogError("Invalid index range.");
            return;
        }

        int originalIndex = 0;
        int tempIndex = 0;

        // Loop through the specified range of items
        for (int i = from; i <= to; i++)
        {
            var item = helmetData.Helmet[i];

            if (!item.isEmpty)
            {
                // Add items to the filtered list and store the mapping
                helmetPage.HelmetBuy.filteredItems.Add(item); // Then add to filteredItems

                HelmettempToOriginalIndexMapping[tempIndex] = originalIndex;

                var spriteArray = GameManager.instance.SpriteCollections.Layers;

                int spriteIndex = item.item.SpriteIndex;


                if (spriteIndex >= 0 && spriteIndex < spriteArray.Count)
                {

                    Texture2D texture = spriteArray[7].Textures[spriteIndex];
                    Texture2D text2 = spriteArray[7].GetIcon(texture);
                    // Create a sprite from the texture
                    Sprite sprite = Sprite.Create(text2, new Rect(0, 0, text2.width, text2.height), Vector2.one * 0.5f);


                    string perks = "";
                    // Check each perk property and accumulate non-zero values
                    if (item.item.Health != 0)
                    {
                        perks += "Health +" + item.item.Health + "\n";
                    }
                    if (item.item.HealthRegen != 0)
                    {
                        perks += "Helmet +" + item.item.HealthRegen + "\n";
                    }

                    // Create a new filtered item
                    helmetPage.AddShopItem(sprite, item.item.Name, item.item.Price.ToString(), perks);
                    originalIndex++;
                    tempIndex++;
                }


            }
        }
    }

    //for Shield
    //public void ShieldsToggleALLButton()
    //{
    //    shieldPage.ResetSelection();
    //    shieldPage.ClearItems();
    //    shieldPage.InitializedShop(ShieldsGetUsedSlotsCount());
    //    ShieldsShowAllCategory();

    //}

    //public void ShieldsOpenShop()
    //{


    //    // Call the methods after the delay

    //    ShieldPrepareUI();
    //    ShieldsToggleALLButton();
    //    shieldPage.Show();
    //    shieldPage.ResetSelection();
    //}

    //public void ShieldsShowAllCategory()
    //{

    //    var spriteArray = GameManager.instance.SpriteCollections.Layers;


    //    var nonEmptyItems = shieldData.GetCurrentInventoryState().Where(item => !item.Value.isEmpty);

    //    int displayedItemsCount = 0;
    //    foreach (var item in nonEmptyItems)
    //    {



    //        if (displayedItemsCount >= ShieldsGetUsedSlotsCount())
    //            break;

    //        int spriteIndex = item.Value.item.SpriteIndex;


    //        if (spriteIndex >= 0 && spriteIndex < spriteArray.Count)
    //        {

    //            Texture2D texture = spriteArray[1].Textures[spriteIndex];
    //            Texture2D text2 = spriteArray[1].GetIcon(texture);
    //            // Create a sprite from the texture
    //            Sprite sprite = Sprite.Create(text2, new Rect(0, 0, text2.width, text2.height), Vector2.one * 0.5f);

    //            string perks = "";
    //            // Check each perk property and accumulate non-zero values
    //            if (item.Value.item.CriticalHit != 0)

    //            {
    //                perks += "Critical Hit +" + item.Value.item.CriticalHit + "\n";
    //            }
    //            if (item.Value.item.CriticalChance != 0)
    //            {
    //                perks += "Critical Chance +" + item.Value.item.CriticalChance + "\n";
    //            }

    //            shieldPage.UpdateData(item.Key, sprite, item.Value.item.Name, item.Value.item.Price.ToString(), perks);

    //            displayedItemsCount++;
    //        }

    //    }
    //}
    public int ShieldsGetUsedSlotsCount()//this will only used the slots with items
    {
        int usedSlots = 0;
        foreach (var item in shieldData.Shield)
        {
            if (!item.isEmpty)
            {
                usedSlots++;
            }
        }
        return usedSlots;
    }
    public void ShieldsToggleFiltered(int from, int to)
    {

        shieldPage.ResetSelection();
        shieldPage.ClearItems();
        shieldPage.InitializedShop(ShieldsGetUsedSlotsCount() /* GameManager.Instance.shopSize*/);
        ShieldShowItemsByIndexRange(from, to);
    }

    public void ShieldsFilteredOpenShop(int from, int to)
    {


        // Call the methods after the delay
        ShieldPrepareUI();
        ShieldsToggleFiltered(from, to);
        shieldPage.Show();
        shieldPage.ResetSelection();
    }

    private void ShieldShowItemsByIndexRange(int from, int to)
    {

        shieldPage.ResetSelection();
        shieldPage.ShieldBuy.filteredItems.Clear();
        ShieldtempToOriginalIndexMapping.Clear();
        shieldPage.ClearItems(); // Clear the existing items in the UI

        // Validate the range
        if (from < 0 || to >= shieldData.Shield.Count || from > to)
        {
            Debug.LogError("Invalid index range.");
            return;
        }

        int originalIndex = 0;
        int tempIndex = 0;

        // Loop through the specified range of items
        for (int i = from; i <= to; i++)
        {
            var item = shieldData.Shield[i];

            if (!item.isEmpty)
            {
                // Add items to the filtered list and store the mapping
                shieldPage.ShieldBuy.filteredItems.Add(item); // Then add to filteredItems

                ShieldtempToOriginalIndexMapping[tempIndex] = originalIndex;

                var spriteArray = GameManager.instance.SpriteCollections.Layers;

                int spriteIndex = item.item.SpriteIndex;


                if (spriteIndex >= 0 && spriteIndex < spriteArray.Count)
                {

                    Texture2D texture = spriteArray[1].Textures[spriteIndex];
                    Texture2D text2 = spriteArray[1].GetIcon(texture);
                    // Create a sprite from the texture
                    Sprite sprite = Sprite.Create(text2, new Rect(0, 0, text2.width, text2.height), Vector2.one * 0.5f);


                    string perks = "";
                    // Check each perk property and accumulate non-zero values
                    if (item.item.ManaRegen != 0)
                    {
                        perks += "Mana Regen +" + item.item.ManaRegen + "\n";
                    }
                    if (item.item.CriticalChance != 0)
                    {
                        perks += "Critical Chance +" + item.item.CriticalChance + "\n";
                    }

                    // Create a new filtered item
                    shieldPage.AddShopItem(sprite, item.item.Name, item.item.Price.ToString(), perks);
                    originalIndex++;
                    tempIndex++;
                }


            }
        }
    }



}
