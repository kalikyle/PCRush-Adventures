using Assets.PixelHeroes.Scripts.CharacterScrips;
using Decoration;
using Firebase;
using Firebase.Auth;
using Firebase.Extensions;
using Firebase.Firestore;
using PartsInventory.Model;
using PC.Model;
//using Firebase.Analytics;
using Shop;
using Shop.Model;
using Shop.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Decoration.Model.DecorSO;
using static Inventory.Model.PartsInventorySO;
//using static UnityEditor.Progress;
//using static UnityEditorInternal.ReorderableList;

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

    public bool isEditing = false;
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


    public GameObject UIExplore;
    public GameObject ComputerInv;

    //public GameObject DecorClickedUI;

    public bool clicked = false;
    public bool OpenEditor = false;
    public bool BeenModified = false;

    public event Action<InventoryItem> OnItemsToTransferUpdated;
    public List<InventoryItem> itemsToTransfer = new List<InventoryItem>();
    public int Partstempindex;

    //player info
    public string PlayerName;


    public List<DecorationItem> removedItemsDuringEditing = new List<DecorationItem>();
    public event Action<DecorationItem> OnDecorToTransferUpdated;
    public List<DecorationItem> DecorToTransfer = new List<DecorationItem>();
    public int tempindex;



    public Dictionary<string, Shop.Model.ShopItem> equippedItemsByCategory = new Dictionary<string, Shop.Model.ShopItem>();
    //public DecorEdit de;
    public TMP_Text PlayerDesk;




    public void PlayerDeskName()
    {
        PlayerDesk.text = "- " + PlayerName + "'s Desk -";
    }

    public async void SaveCharInfo(string userID, string playerName)
    {
        // Check if the UserID and playerName are not null or empty
        if (string.IsNullOrEmpty(userID) || string.IsNullOrEmpty(playerName))
        {
            UnityEngine.Debug.LogError("UserID or playerName is null or empty.");
            return;
        }

        // Get a reference to the user's document under the 'users' collection
        DocumentReference userDocRef = FirebaseFirestore.DefaultInstance
            .Collection("users").Document(userID);

        // Create a dictionary to store the playerName
        Dictionary<string, object> playerNameData = new Dictionary<string, object>
        {
            { "playerName", playerName }
            //add other fields
        };

        try
        {
            // Save the playerName data to Firestore
            await userDocRef.SetAsync(playerNameData, SetOptions.MergeAll);
            UnityEngine.Debug.Log("PlayerName saved successfully.");
        }
        catch (System.Exception ex)
        {
            UnityEngine.Debug.LogError("Error saving playerName: " + ex.Message);
        }
    }

    public async void RetrievePlayerInfo(string userID)
    {
        // Check if the UserID is not null or empty
        if (string.IsNullOrEmpty(userID))
        {
            Debug.LogError("UserID is null or empty.");
            return;
        }

        // Get a reference to the user's document under the 'users' collection
        DocumentReference userDocRef = FirebaseFirestore.DefaultInstance
            .Collection("users").Document(userID);

        try
        {
            // Retrieve the document snapshot asynchronously
            DocumentSnapshot snapshot = await userDocRef.GetSnapshotAsync();

            // Check if the document exists
            if (snapshot.Exists)
            {
                // Extract the playerName from the document data
                PlayerName = snapshot.GetValue<string>("playerName");

            }
            else
            {
                Debug.LogWarning("Document does not exist for UserID: " + userID);
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Error retrieving playerName: " + ex.Message);
        }
    }


    //public async Task LoadInUseItems()
    //{
       
    //    string[] categories = new string[] { "Monitor", "Mouse", "Keyboard", "Desk", "Background" };

    //    foreach (string category in categories)
    //    {
    //        // Check if the item in this category is both sold and in use

    //        // Load the in-use items from Firestore for this category
    //        //await LoadInSoldItemsFromFirestore(category);
    //        await LoadInUseItemsFromFirestore(category);
              
        
    //    }
    //}

    //public async Task LoadSoldItems()
    //{

    //    string[] categories = new string[] { "Monitor", "Mouse", "Keyboard", "Desk", "Background" };

    //    foreach (string category in categories)
    //    {
    //        // Check if the item in this category is both sold and in use

    //        // Load the in-use items from Firestore for this category
    //        await LoadInSoldItemsFromFirestore(category);


    //    }
    //}
    public void SaveSoldItems()
    {
        
            foreach (var item in so.ShopItems)
            {
                if (item.item.Sold == true && item.item.InUse == true)
                {
                    SaveItemToFirestore(item);
                    SaveUsedItemToFirestore(item);


                    if (equippedItemsByCategory.ContainsKey(item.item.Category))
                    {
                        equippedItemsByCategory[item.item.Category] = item;

                    }
                    else
                    {

                        equippedItemsByCategory.Add(item.item.Category, item);

                    }
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
    public void AddItemToTransfer(InventoryItem item)
    {
        itemsToTransfer.Add(item);
        OnItemsToTransferUpdated?.Invoke(item);
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
       // CollectionReference collectionRef = FirebaseFirestore.DefaultInstance.Collection("users");

        // Create a new document with the generated document ID
        DocumentReference docRef = FirebaseFirestore.DefaultInstance.Collection(GameManager.instance.UserCollection).Document(GameManager.instance.UserID);

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

    public async void SaveComputerParts(List<InventoryItem> items)
    {
        // Convert the list of items to JSON
        string jsonData = JsonUtility.ToJson(new PartsItemList { Items = items });

        DocumentReference docRef = FirebaseFirestore.DefaultInstance.Collection(GameManager.instance.UserCollection).Document(GameManager.instance.UserID);

        CollectionReference SubDocRef = docRef.Collection("PartsInventory");

        DocumentReference DecordocRef = SubDocRef.Document("PartsInvent");
        // Create a dictionary to store the data
        Dictionary<string, object> dataDict = new Dictionary<string, object>
    {
        { "Parts", jsonData }
    };

        // Set the data of the document
        await DecordocRef.SetAsync(dataDict);

        Debug.Log("Parts items saved to Firestore.");
    }

    //public async void SaveComputer(List<Computer> items)
    //{
    //    // Convert the list of items to JSON
    //    string jsonData = JsonUtility.ToJson(new ComputerList { Items = items });

    //    DocumentReference docRef = FirebaseFirestore.DefaultInstance.Collection(GameManager.instance.UserCollection).Document(GameManager.instance.UserID);

    //    CollectionReference SubDocRef = docRef.Collection("ComputerInventory");

    //    DocumentReference DecordocRef = SubDocRef.Document("ComputerInvent");
    //    // Create a dictionary to store the data
    //    Dictionary<string, object> dataDict = new Dictionary<string, object>
    //{
    //    { "PC", jsonData }
    //};



    //    // Set the data of the document
    //    await DecordocRef.SetAsync(dataDict);

    //    Debug.Log("Computers saved to Firestore.");
    //}

    //public async void SavePCSO(PCSO pcso)
    //{
    //    // Convert the PCSO object to JSON
    //    string pcsoJson = JsonUtility.ToJson(pcso);

    //    // Get a reference to the Firestore document where you want to store the PCSO data
    //    DocumentReference docRef = FirebaseFirestore.DefaultInstance
    //        .Collection(GameManager.instance.UserCollection)
    //        .Document(GameManager.instance.UserID)
    //        .Collection("PCSOCollection")
    //        .Document("PCSOItem");

    //    // Create a dictionary to store the PCSO data
    //    Dictionary<string, object> dataDict = new Dictionary<string, object>
    //{
    //    { "pcsoData", pcsoJson }
    //};

    //    // Set the data in the Firestore document
    //    await docRef.SetAsync(dataDict);
    //}

    //tosave

    public List<string> pcsoDocumentIds = new List<string>();
    public string pcsothatinUse;
    public string pcsothatisModified;
    public async Task SavePCSO(PCSO pcso)
    {
        // Convert the PCSO object to JSON
        string pcsoJson = JsonUtility.ToJson(pcso);

        // Get a reference to the Firestore document where you want to store the PCSO data
        DocumentReference docRef = FirebaseFirestore.DefaultInstance
            .Collection(GameManager.instance.UserCollection)
            .Document(GameManager.instance.UserID)
            .Collection("ComputersCollection")
            .Document();

        // Set the data in the Firestore document using the generated document ID
        await docRef.SetAsync(new Dictionary<string, object> { { "PC", pcsoJson } });

        pcsoDocumentIds.Insert(0, docRef.Id);

    }
    //to update
    //public async Task UpdatePCSOItemList()
    //{
    //    // Get a reference to the Firestore document for the PCSOItem
    //    DocumentReference docRef = FirebaseFirestore.DefaultInstance
    //        .Collection(GameManager.instance.UserCollection)
    //        .Document(GameManager.instance.UserID)
    //        .Collection("PCSOCollection")
    //        .Document("PCSOItem");

    //    // Run a Firestore transaction to update the PCSOItem document
    //    await FirebaseFirestore.DefaultInstance.RunTransactionAsync(async transaction =>
    //    {
    //        // Retrieve the current PCSO list data from Firestore
    //        DocumentSnapshot snapshot = await transaction.GetSnapshotAsync(docRef);
    //        List<object> currentPCSOList = snapshot.Exists ? (List<object>)snapshot.GetValue("pcsoList") : new List<object>();

    //        // Generate a new entry for the updated PCSO list (e.g., with the latest PCSO ID)
    //        Dictionary<string, object> newPCSOEntry = new Dictionary<string, object>
    //    {
    //        { "pcsoID", currentPCSOList.Count }, // Use a unique ID or index for each PCSO
    //        { "pcsoData", JsonUtility.ToJson(currentPCSOList[currentPCSOList.Count - 1]) } // Save the latest PCSO data
    //    };

    //        // Append the new PCSO entry to the current PCSO list
    //        currentPCSOList.Add(newPCSOEntry);

    //        // Update the PCSOItem document with the updated PCSO list
    //        transaction.Set(docRef, new Dictionary<string, object> { { "pcsoList", currentPCSOList } });

    //        // Return a success result (you can handle this as needed)
    //        return true;
    //    });
    //}

    //public async void SavePCSOList(List<PCSO> pcsoList)
    //{
    //    // Convert the list of PCSO objects to JSON
    //    string pcsoListJson = JsonUtility.ToJson(new PCList { PCSOs = pcsoList });

    //    // Get a reference to the Firestore document where you want to store the PCSO list
    //    DocumentReference docRef = FirebaseFirestore.DefaultInstance
    //        .Collection(GameManager.instance.UserCollection)
    //        .Document(GameManager.instance.UserID)
    //        .Collection("PCSOCollection")
    //        .Document("PCSOItem");

    //    // Create a dictionary to store the PCSO list data
    //    Dictionary<string, object> dataDict = new Dictionary<string, object>
    //{
    //    { "pcsoListData", pcsoListJson }
    //};

    //    // Set the data in the Firestore document
    //    await docRef.SetAsync(dataDict);
    //}

    public async Task LoadInUseItemsFromFirestore(string category)
    {
        try
        {
            CollectionReference equippedItemsRef = FirebaseFirestore.DefaultInstance.Collection(UserCollection)
            .Document(UserID).Collection("EquippedItems").Document("InUseItems").Collection(category);

            // Get all documents in the "InUseItems" subcollection for the specified category
            QuerySnapshot inUseItemsSnapshot = await equippedItemsRef.GetSnapshotAsync();

            // Iterate through the documents and handle each item
            foreach (DocumentSnapshot docSnapshot in inUseItemsSnapshot.Documents)
            {
                // Deserialize the item data
                string jsonData = docSnapshot.GetValue<string>("itemData");
                string jsonName = docSnapshot.GetValue<string>("itemName");
                Debug.Log("JSON Data: " + jsonData); // Debugging statement to inspect JSON data

                // Shop.Model.ShopItem item = new Shop.Model.ShopItem();
                // Deserialize the item part
                /*Shop.Model.ShopItem*/ item = JsonUtility.FromJson<Shop.Model.ShopItem>(jsonData);

                // Create a new ShopItem instance and assign the deserialized item data
              
                
                item.item.InUse = true;
                       
                Debug.Log("Loaded ShopItem: " + item.item.Name); // Debugging statement to confirm deserialization
                                                                 //Debug.Log("Category: " + item.item.Category);
                UpdateSprite(item);




            }


        }
        catch (System.Exception ex)
        {
            Debug.LogError("Error loading in-use items from Firestore: " + ex.Message);
        }
    }
    //public async Task LoadInSoldItemsFromFirestore(string category)
    //{
    //    try
    //    {
    //        CollectionReference equippedItemsRef = FirebaseFirestore.DefaultInstance.Collection(UserCollection)
    //        .Document(UserID).Collection("EquippedItems").Document("SoldItems").Collection(category);

    //        // Get all documents in the "InUseItems" subcollection for the specified category
    //        QuerySnapshot inUseItemsSnapshot = await equippedItemsRef.GetSnapshotAsync();

    //        // Iterate through the documents and handle each item
    //        foreach (DocumentSnapshot docSnapshot in inUseItemsSnapshot.Documents)
    //        {
    //            // Deserialize the item data
    //            string jsonData = docSnapshot.GetValue<string>("itemData");
    //            Debug.Log("JSON Data: " + jsonData); // Debugging statement to inspect JSON data

    //            // Shop.Model.ShopItem item = new Shop.Model.ShopItem();
    //            // Deserialize the item part
    //            Shop.Model.ShopItem item = JsonUtility.FromJson<Shop.Model.ShopItem>(jsonData);

    //            // Create a new ShopItem instance and assign the deserialized item data
    //            item.item.Sold = true;


    //            Debug.Log("Loaded ShopItem: " + item.item.Name); // Debugging statement to confirm deserialization
    //                                                             //Debug.Log("Category: " + item.item.Category);

    //            //UpdateSprite(item);



    //        }
    //    }
    //    catch (System.Exception ex)
    //    {
    //        Debug.LogError("Error loading in-use items from Firestore: " + ex.Message);
    //    }
    //}
    public Shop.Model.ShopItem item;
    public async Task LoadInSoldItemsFromFirestore(string[] categories)
    {
        try
        {
            foreach (string category in categories)
            {
                CollectionReference equippedItemsRef = FirebaseFirestore.DefaultInstance.Collection(UserCollection)
                    .Document(UserID).Collection("EquippedItems").Document("SoldItems").Collection(category);

                // Get all documents in the "InUseItems" subcollection for the specified category
                QuerySnapshot inUseItemsSnapshot = await equippedItemsRef.GetSnapshotAsync();

                // Iterate through the documents and handle each item
                foreach (DocumentSnapshot docSnapshot in inUseItemsSnapshot.Documents)
                {
                    // Deserialize the item data
                    string jsonData = docSnapshot.GetValue<string>("itemData");
                    Debug.Log("JSON Data: " + jsonData); // Debugging statement to inspect JSON data

                    // Deserialize the item data into a ShopItem object
                    /*Shop.Model.ShopItem*/ item = JsonUtility.FromJson<Shop.Model.ShopItem>(jsonData);

                    // Update item properties (e.g., set Sold flag)
                    item.item.Sold = true;

                    Debug.Log("Loaded ShopItem: " + item.item.Name); // Debugging statement to confirm deserialization
                }
                
                await LoadInUseItemsFromFirestore(category);
            }

        }
        catch (System.Exception ex)
        {
            Debug.LogError("Error loading sold items from Firestore: " + ex.Message);
        }
    }




    private void UpdateSprite(Shop.Model.ShopItem item)
    {
        if (equippedItemsByCategory.ContainsKey(item.item.Category))
        {
            equippedItemsByCategory[item.item.Category] = item;

        }
        else
        {

            equippedItemsByCategory.Add(item.item.Category, item);

        }


        switch (item.item.Category)
        {
            case "Monitor":
                Monitor.sprite = item.item.ItemImage;
                break;
            case "Mouse":
                Mouse.sprite = item.item.ItemImage;
                break;
            case "Keyboard":
                Keyboard.sprite = item.item.ItemImage;
                break;
            case "Desk":
                Desk.sprite = item.item.ItemImage;
                break;
            case "Background":
                Background.sprite = item.item.ItemImage;
                break;
            default:
                Debug.LogWarning("Unsupported item category: " + item.item.Category);
                break;
        }
    }
     public async void SaveItemToFirestore(Shop.Model.ShopItem item)//needfix
    {
        try
        {
            // Get a reference to the Firestore document
            DocumentReference docRef = FirebaseFirestore.DefaultInstance.Collection(UserCollection).Document(UserID);

            // Create a reference to the subcollection for equipped items
            CollectionReference equippedItemsRef = docRef.Collection("EquippedItems");

            // Check if the item is sold
            if (item.item.Sold)
            {
                // Determine the subcollection name
                string subcollectionName = "SoldItems";

                // Create a reference to the subcollection
                CollectionReference itemSubcollectionRef = equippedItemsRef.Document(subcollectionName).Collection(item.item.Category);

                // Check if an item with the same category and name already exists
                QuerySnapshot querySnapshot = await itemSubcollectionRef.WhereEqualTo("itemName", item.item.Name).GetSnapshotAsync();
                foreach (DocumentSnapshot documentSnapshot in querySnapshot.Documents)
                {
                    // Delete the previous item with the same category and name
                    await documentSnapshot.Reference.DeleteAsync();
                    Debug.Log("Previous item deleted: " + documentSnapshot.Id);
                }

                // Create a new document for the item
                DocumentReference itemDocRef = itemSubcollectionRef.Document();

                // Serialize the item data
                string jsonData = JsonUtility.ToJson(item);

                // Save the item data to Firestore
                await itemDocRef.SetAsync(new Dictionary<string, object>
            {
                { "itemData", jsonData },
                { "itemName", item.item.Name } // Include item name for future deletion checks
            });

                Debug.Log("Item saved to Firestore: " + item.item.Name);
            }
            else
            {
                Debug.LogWarning("Item is not sold. Not saving to Firestore.");
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Error saving item to Firestore: " + ex.Message);
        }
    }
    public async void SaveUsedItemToFirestore(Shop.Model.ShopItem item)
    {
        try
        {
            // Get a reference to the Firestore document
            DocumentReference docRef = FirebaseFirestore.DefaultInstance.Collection(UserCollection).Document(UserID);

            // Create a reference to the subcollection for equipped items
            CollectionReference equippedItemsRef = docRef.Collection("EquippedItems");


        if (item.item.InUse)
        {
            // Determine the subcollection based on whether the item is sold or in use
            string subcollectionName = "InUseItems";

            // Create a reference to the subcollection
            CollectionReference itemSubcollectionRef = equippedItemsRef.Document(subcollectionName).Collection(item.item.Category);

            await DeletePreviousInUseItem(item.item.Category, equippedItemsRef);

            // Create a new document for the item
            DocumentReference itemDocRef = itemSubcollectionRef.Document();

            // Serialize the item data
            string jsonData = JsonUtility.ToJson(item);

            // Save the item data to Firestore
            await itemDocRef.SetAsync(new Dictionary<string, object>
            {
                { "itemData", jsonData },
                { "itemName", item.item.Name }
            });

            Debug.Log("Item used and save to Firestore: " + item.item.Name);
        }
        else
        {
            Debug.LogWarning("Item is neither sold nor in use. Not saving to Firestore.");
        }
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Error saving item to Firestore: " + ex.Message);
        }
    }
        private async Task DeletePreviousInUseItem(string category, CollectionReference equippedItemsRef)
    {
        try
        {
            // Create a reference to the "InUseItems" subcollection for the specific category
            CollectionReference inUseItemsRef = equippedItemsRef.Document("InUseItems").Collection(category);

            // Query to get the previous item in use
            QuerySnapshot snapshot = await inUseItemsRef.GetSnapshotAsync();
            foreach (DocumentSnapshot docSnap in snapshot.Documents)
            {
                // Delete the previous item in use
                await docSnap.Reference.DeleteAsync();
                Debug.Log("Previous item in use deleted: " + docSnap.Id);
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Error deleting previous item in use: " + ex.Message);
        }
    }
    private void Start()
    {



        UserID = PlayerPrefs.GetString("UserID", "");
        ClearPlayerPrefsIfUserIDNotFound(UserID);

       
        Debug.Log(UserID);
        // player page

        StartCoroutine(DropPartsInRandomPosition());



        //EnableDefault();


        //DC.LoadInitialItems();

    }
    //this is the start when click play button
    public async void AtTheStart()
    {
        if (UserID != "")
        {

            UIExplore.SetActive(true);

            string[] categories = new string[] { "Monitor", "Mouse", "Keyboard", "Desk", "Background" };
            DisableFirstall();
            charBuilder.LoadSavedData();

            // If initial items have already been saved, load in-use items
            //await Task.Delay(500);
            ////await LoadInUseItems();
            await Task.Delay(500);
            //await LoadSoldItems();
            await LoadInSoldItemsFromFirestore(categories);
            await Task.Delay(500);
            SaveSoldItems();
            await Task.Delay(500);
            await DecorMan.LoadAllDecorationsFromFirestore();

            RetrievePlayerInfo(UserID);
            
        }
    }
    public void ClearPlayerPrefsIfUserIDNotFound(string userID)
    {
        if (userID == "")
        {
            Debug.Log("UserID is null or empty.");
            SceneManager.LoadScene(1, LoadSceneMode.Additive);
            EnableDefault();
        }
        else
        {
            FirebaseFirestore db = FirebaseFirestore.DefaultInstance;

            // Get a reference to the user's document in Firestore
            DocumentReference userDocRef = db.Collection(UserCollection).Document(userID);

            // Check if the document exists in Firestore
            userDocRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
            {
                if (task.IsCompleted)
                {
                    DocumentSnapshot snapshot = task.Result;
                    if (!snapshot.Exists)
                    {
                        // User document not found, clear player prefs
                        PlayerPrefs.DeleteAll();
                        UserID = "";
                        Debug.Log("PlayerPrefs cleared because userID was not found in Firestore.");
                        SceneManager.LoadScene(1, LoadSceneMode.Additive);
                        EnableDefault();
                    }
                    else
                    {
                        Debug.Log("UserID found in Firestore. PlayerPrefs not cleared.");
                        SceneManager.LoadScene(1, LoadSceneMode.Additive);
                        EnableDefault();
                    }
                }
                else if (task.IsFaulted)
                {
                    Debug.LogError("Error checking if userID exists in Firestore: " + task.Exception);
                }
            });
        }

       
    }
    public void DisableFirstall()
    {
        if (UserID != null && UserID != "" && !UserID.IsUnityNull())
        {
            foreach (var item in so.ShopItems)
            {

                item.item.InUse = false;
                item.item.Sold = false;
                //Debug.LogError("disabled");

            }
        }
    }
    public void EnableDefault()
    {
        foreach (var item in so.ShopItems)
        {
            if (UserID == null || UserID == "") { 
                if (item.item.Price == 0)
                {
                    item.item.Sold = true;
                    item.item.InUse = true;
                }
                else
                {
                    item.item.Sold = false;
                    item.item.InUse = false;
                }
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        UserIDTxt.text = UserID;

        if (UserID != "")
        {
            RetrievePlayerInfo(UserID);
        }
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

    public PartsCollect partsCollectPrefab;
    public float dropDuration = 5f;
    public Vector2 spawnAreaSize = new Vector2(5f, 5f);
    public Transform partsToCollect;
    public List<PartsSO> partsList = new List<PartsSO>();

    private IEnumerator DropPartsInRandomPosition()
    {
        while (true)
        {
            yield return new WaitForSeconds(dropDuration);

            // Get the bounds of the parent object
            Bounds parentBounds = GetParentBounds();

            // Calculate a random position within the bounds of the parent object
            Vector3 randomPosition = new Vector3(
                UnityEngine.Random.Range(parentBounds.min.x, parentBounds.max.x),
                UnityEngine.Random.Range(parentBounds.min.y, parentBounds.max.y),
                0f);

            // Instantiate PartsCollect prefab at the calculated random position
            PartsCollect newPartsCollect = Instantiate(partsCollectPrefab, randomPosition, Quaternion.identity);

            // Set the parent of the instantiated prefab
            if (partsToCollect != null)
            {
                newPartsCollect.transform.SetParent(partsToCollect);
            }
        }
    }
    private Bounds GetParentBounds()
    {
        // Get the renderer component of the parent object
        Collider2D parentCollider = partsToCollect.GetComponent<Collider2D>();


        // If the parent object has a renderer component, return its bounds
        if (parentCollider != null)
        {
            return parentCollider.bounds;
        }
        else
        {
            // If the parent object does not have a renderer, return default bounds
            Debug.LogWarning("Parent object does not have a renderer component. Using default bounds.");
            return new Bounds(Vector3.zero, Vector3.one);
        }
    }
}
[System.Serializable]
public class DecorationItemList
{
    public List<DecorationItem> Items;
}

[System.Serializable]
public class PartsItemList
{
    public List<InventoryItem> Items;
}

//[System.Serializable]
//public class ComputerList
//{
//    public List<Computer> Items;
//}

//[System.Serializable]
//public class PCList
//{
//    public List<PCSO> PCSOs;
//}



