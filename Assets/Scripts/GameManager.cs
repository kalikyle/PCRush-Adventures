using Assets.PixelHeroes.Scripts.CharacterScrips;
using Shop;
using Shop.UI;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameManager instance;
    public CharacterBuilder charBuilder;
    public int ShopSize = 100;


    public ShopController SC;
    public Shop.Model.ShopSO so;

    public Image Monitor;
    public Image Keyboard;
    public Image Mouse;
    public Image Desk;
    public Image Background;



    public Dictionary<string, Shop.Model.ShopItem> equippedItemsByCategory = new Dictionary<string, Shop.Model.ShopItem>();
   
    public void AddInitiallyEquippedItems()
    {
        foreach (var item in so.ShopItems)
        {
            if (item.item.Sold == true && item.item.InUse == true)
            {
                AddEquippedItemToDictionary(item);
                Debug.Log(item.item.name);
            }
        }
    }

    private void AddEquippedItemToDictionary(Shop.Model.ShopItem item)
    {
        if (equippedItemsByCategory.ContainsKey(item.item.Category))
        {
            equippedItemsByCategory[item.item.Category] = item;

        }
        else
        {
            
            equippedItemsByCategory.Add(item.item.Category, item);

          
        }

        // Check if the item category is "Monitor" and if it's sold and in use
        if (item.item.Category == "Monitor" && item.item.Sold && item.item.InUse)
        {
            Monitor.sprite = item.item.ItemImage;
        }
        if (item.item.Category == "Mouse" && item.item.Sold && item.item.InUse)
        {
            Mouse.sprite = item.item.ItemImage;
        }
        if (item.item.Category == "Keyboard" && item.item.Sold && item.item.InUse)
        {
            Keyboard.sprite = item.item.ItemImage;
        }
        if (item.item.Category == "Desk" && item.item.Sold && item.item.InUse)
        {
            Desk.sprite = item.item.ItemImage;
        }
        if (item.item.Category == "Background" && item.item.Sold && item.item.InUse)
        {
            Background.sprite = item.item.ItemImage;
        }
    }
    void Start()
    {
        if(PlayerPrefs.GetInt("CharChanged") == 0)
        {
            SceneManager.LoadScene(1, LoadSceneMode.Additive);
        }
        charBuilder.LoadSavedData();
        AddInitiallyEquippedItems();
       
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
    public void UpdateShop(string category)
    {
        if(SC.shopBuy.ToggleTF == true && SC.shopBuy.ToggleBSE == false)
        {
            SC.ShowCategory(category);

        }
        else if (SC.shopBuy.ToggleTF == true && SC.shopBuy.ToggleBSE == true)
        {
            if(SC.shopBuy.filteredBSE == "All")
            {
                SC.ShowAllCategory();

            }else if (SC.shopBuy.filteredBSE == "Unsold")
            {
                SC.ShowBuy();
            }
            else if (SC.shopBuy.filteredBSE == "Sold")
            {
                SC.ShowSold();
            }
            else if (SC.shopBuy.filteredBSE == "Equipped")
            {
                SC.ShowEquipped();
            }
        }
        else
        {
            SC.ShowAllCategory();
        }
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // If another instance already exists, destroy this one
            Destroy(gameObject);
        }
    }
    public void LoadCharacter()
    {
        charBuilder.LoadSavedData();
    }
    
}
