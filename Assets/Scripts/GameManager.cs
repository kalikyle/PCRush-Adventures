using Assets.PixelHeroes.Scripts.CharacterScrips;
using Decoration;
using Firebase;
using Firebase.Firestore;
//using Firebase.Analytics;
using Shop;
using Shop.Model;
using Shop.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Decoration.Model.DecorSO;

public class GameManager : MonoBehaviour
{

    public string UserID;
    public string UserCollection = "users";
    // Start is called before the first frame update
    public static GameManager instance;
    public CharacterBuilder charBuilder;
    public int ShopSize = 100;


    public ShopController SC;
    public DecorController DC;
    public Shop.Model.ShopSO so;
    public DecorationManager DecorMan;


    public TMP_Text UserIDTxt;

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
    public Button DoneButton;

    //public GameObject DecorClickedUI;

    public bool clicked = false;

    public int tempindex;

    public event Action<DecorationItem> OnDecorToTransferUpdated;
    public List<DecorationItem> DecorToTransfer = new List<DecorationItem>();

    public Dictionary<string, Shop.Model.ShopItem> equippedItemsByCategory = new Dictionary<string, Shop.Model.ShopItem>();
    //public DecorEdit de;
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
    public void SetUserID(string userID)
    {
        UserID = userID;
        PlayerPrefs.SetString("UserID", UserID);
        PlayerPrefs.Save();
    }
    public void AddItemToTransfer(DecorationItem item)
    {
       DecorToTransfer.Add(item);
       OnDecorToTransferUpdated?.Invoke(item);
    }
    public void SaveDecorInitialItemss(List<DecorationItem> items)
    {

        string jsonData = JsonUtility.ToJson(new DecorationItemList { Items = items });
        PlayerPrefs.SetString("SavedInitialItems", jsonData);
        PlayerPrefs.Save();
        //Debug.LogError("Data has been Saved");
    }
    public async void SaveDecorInitialItems(List<DecorationItem> items)
    {
        // Convert the list of items to JSON
        string jsonData = JsonUtility.ToJson(new DecorationItemList { Items = items });

        // Generate a unique document ID (e.g., based on player ID or timestamp)
        //string documentID = "Player1DecInventory"; // Example document ID

        // Get a reference to the Firestore collection
        CollectionReference collectionRef = FirebaseFirestore.DefaultInstance.Collection("users");

        // Create a new document with the generated document ID
        DocumentReference docRef = collectionRef.Document("Data");

        CollectionReference SubDocRef = docRef.Collection("DecorationInventory");

        DocumentReference DecordocRef = SubDocRef.Document("DecorInvent");
        // Create a dictionary to store the data
        Dictionary<string, object> dataDict = new Dictionary<string, object>
    {
        { "items", jsonData }
    };

        // Set the data of the document
        await DecordocRef.SetAsync(dataDict);

        Debug.Log("decoration items saved to Firestore.");
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

        UserID = PlayerPrefs.GetString("UserID", "");
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            FirebaseApp app = FirebaseApp.DefaultInstance;
            if (app != null)
            {
                // Firebase is initialized successfully
                Debug.Log("Firebase initialized successfully.");
            }
            else
            {
                // Firebase initialization failed
                Debug.LogError("Failed to initialize Firebase.");
            }
        });

        if (UserID == null || UserID == "")
        {
            SceneManager.LoadScene(1, LoadSceneMode.Additive);
        }

        charBuilder.LoadSavedData();
        AddInitiallyEquippedItems();
        //DC.LoadInitialItems();
       
    }

    // Update is called once per frame
    void Update()
    {
        UserIDTxt.text = UserID;

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
    public void DecorUseEnable( DecorationItem Item)
    {
        DecorMan.UseDecor( Item);
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
    public void RemoveItem()
    {
        DecorMan.DecorRemove();
    }
    public void SaveDecorProperties(List<DecorEdit> ListofDecors)
    {
        List<DecorationData> decorDataList = new List<DecorationData>();

        foreach (DecorEdit decor in ListofDecors)
        {
            // Get the sprite name from the Image component
            string spriteName = decor.GetComponent<Image>().sprite.name;

            // Create a new DecorationData object and add it to the list
            DecorationData data = new DecorationData(decor.name, decor.rectTransform.anchoredPosition, decor.rectTransform.sizeDelta, spriteName);
            decorDataList.Add(data);

            Debug.LogError("saved");
        }
        foreach (DecorationData data in decorDataList)
        {
            Debug.LogError(data.spriteName);
        }
       
        // Serialize the list to JSON and save it to PlayerPrefs
        string json = JsonUtility.ToJson(decorDataList);
        PlayerPrefs.SetString("DecorProperties", json);
        PlayerPrefs.Save();
        Debug.Log("Serialized JSON: " + json);
    }

}
[System.Serializable]
public class DecorationItemList
{
    public List<DecorationItem> Items;
}

[System.Serializable]
public class DecorationData //needfix
{
    public string name;
    public Vector2 position;
    public Vector2 size;
    public string spriteName;

    // Constructor to initialize the DecorationData object with values
    public DecorationData(string name, Vector2 position, Vector2 size, string spriteName)
    {
        this.name = name;
        this.position = position;
        this.size = size;
        this.spriteName = spriteName;
    }
    // Add more properties as needed
}


