using Assets.PixelHeroes.Scripts.CharacterScrips;
using Shop;
using Shop.Model;
using Shop.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Decoration.Model.DecorSO;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameManager instance;
    public CharacterBuilder charBuilder;
    public int ShopSize = 100;


    public ShopController SC;
    public Shop.Model.ShopSO so;
    public DecorationManager DecorMan;

    public Image Monitor;
    public Image Keyboard;
    public Image Mouse;
    public Image Desk;
    public Image Background;

    public Button removeButton;
    public Button MirrorButton;
    public Button rotateLeftButton;
    public Button rotateRightButton;
    public Button ResizeIncButton;
    public Button ResizeDecButton;

    public bool clicked = false;

    public int tempindex;

    public event Action<DecorationItem> OnDecorToTransferUpdated;
    public List<DecorationItem> DecorToTransfer = new List<DecorationItem>();

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
    public void AddItemToTransfer(DecorationItem item)
    {
       DecorToTransfer.Add(item);
       OnDecorToTransferUpdated?.Invoke(item);
    }
    public void SaveDecorInitialItems(List<DecorationItem> items)
    {

        string jsonData = JsonUtility.ToJson(new DecorationItemList { Items = items });
        PlayerPrefs.SetString("SavedInitialItems", jsonData);
        PlayerPrefs.Save();
        //Debug.LogError("Data has been Saved");
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
    public void DecorUseEnable(bool Editing, DecorationItem Item)
    {
        DecorMan.UseDecor(Editing, Item);
    }

    public void LoadCharacter()
    {
        charBuilder.LoadSavedData();
    }
    public void DecorClicked()
    {
        DecorMan.DecorClicked();
    }
    public void DecorUnClicked()
    {
        DecorMan.UnclickedDecor();
    }
    private const string PlayerPrefsKey = "SavedDecorPrefabs";

    // Save the prefabs of the child objects from the MainDecorPanel into PlayerPrefs
    public void SaveDecorPrefabs(Transform mainDecorPanel)//need fix
    {
        List<string> savedPrefabs = new List<string>();

        foreach (Transform child in mainDecorPanel)
        {
            // Assuming each child object has a unique identifier (e.g., prefab name)
            string prefabName = child.name; // Replace this with the actual unique identifier

            savedPrefabs.Add(prefabName);
        }

        // Convert the list of prefab names to a JSON string
        string json = JsonUtility.ToJson(savedPrefabs);

        // Save the JSON string to PlayerPrefs
        PlayerPrefs.SetString(PlayerPrefsKey, json);
        PlayerPrefs.Save();

    }

    // Load the saved prefabs from PlayerPrefs and instantiate them in the MainDecorPanel
    public void LoadDecorPrefabs(Transform mainDecorPanel)
    {
        // Retrieve the saved JSON string from PlayerPrefs
        string json = PlayerPrefs.GetString(PlayerPrefsKey);

        if (!string.IsNullOrEmpty(json))
        {
            // Convert the JSON string back to a list of prefab names
            List<string> savedPrefabs = JsonUtility.FromJson<List<string>>(json);

            // Instantiate each prefab in the MainDecorPanel
            foreach (string prefabName in savedPrefabs)
            {
                // Load the prefab using its unique identifier (e.g., prefab name)
                GameObject prefab = Resources.Load<GameObject>(prefabName); // Adjust the path as needed

                if (prefab != null)
                {
                    // Instantiate the prefab as a child of the MainDecorPanel
                    Instantiate(prefab, mainDecorPanel);
                }
            }
        }
    }
}
[System.Serializable]
public class DecorationItemList
{
    public List<DecorationItem> Items;
}
