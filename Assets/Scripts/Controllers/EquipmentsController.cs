using Assets.PixelHeroes.Scripts.CharacterScrips;
using Assets.PixelHeroes.Scripts.CollectionScripts;
using Assets.PixelHeroes.Scripts.EditorScripts;
using Shop.Model;
using Shop.UI;
using Swords.Model;
using Swords.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EquipmentsController : MonoBehaviour
{
    [SerializeField]
    private SwordsPage swordsPage;

    [SerializeField]
    private SwordItemsSO swordsData;
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

    private void PrepareUI()
    {
        swordsPage.InitializedShop(GetUsedSlotsCount());


    }

    public int GetUsedSlotsCount()//this will only used the slots with items
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

    public void ToggleALLButton()
    {
        //shopBuy.ToggleTF = false;
        //shopBuy.ToggleBSE = false;
        // Toggle the state
        swordsPage.ResetSelection();
        swordsPage.ClearItems();
        swordsPage.InitializedShop(GetUsedSlotsCount() /* GameManager.Instance.shopSize*/);

        

        ShowAllCategory();

    }

    public void OpenShop()
    {


        // Call the methods after the delay

        PrepareUI();
        ToggleALLButton();
        swordsPage.Show();
        swordsPage.ResetSelection();
    }

    public void ShowAllCategory()
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


            if (displayedItemsCount >= GetUsedSlotsCount())
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


                        swordsPage.UpdateData(item.Key, sprite, item.Value.item.Name, item.Value.item.Price.ToString(), item.Value.item.attack.ToString());
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
