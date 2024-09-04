using Decoration.Model;
using Decoration.UI;
using Firebase.Extensions;
using Firebase.Firestore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static Decoration.Model.DecorSO;
//using static UnityEditor.PlayerSettings.WSA;
//

public class DecorationManager : MonoBehaviour
{
    public GameObject panel;
    public GameObject MainDecorpanel;
    public GameObject desk;
    public GameObject Inventory;
    public GameObject DecorUI;
    public GameObject TopUI;
    public GameObject ShopUI;

    public Button doneButton;
    public Button cancelButton;
    public Button editButton;

    //public GameObject DecorClickedUI;
    public DecorSO decordata;

    private GameObject currentDecoration;
    public DecorEdit decorationPrefab;
    private RectTransform currentDecorationRectTransform;
    private Vector3 offset;

   

    public List<DecorEdit> ListofDecors = new List<DecorEdit>();
    public List<DecorationItem> ListofUseDecors = new List<DecorationItem>();
    public static DecorEdit selectedDecor;
    private Dictionary<DecorEdit, Vector2> initialPositions = new Dictionary<DecorEdit, Vector2>();
    private Dictionary<DecorEdit, Vector2> SavedPositions = new Dictionary<DecorEdit, Vector2>();
    private List<DecorEdit> newDecorations = new List<DecorEdit>();
    //private List<DecorEdit> EditedDecorations = new List<DecorEdit>();
    private List<DecorEdit> PlacedDecorations = new List<DecorEdit>();
    private Dictionary<DecorEdit, List<DecorationItem>> removedDecorations = new Dictionary<DecorEdit, List<DecorationItem>>();


    private void Start()
    {
       // ExecuteAfterDelay(2f);
        // Disable the panel and set up button click listeners
        doneButton.onClick.AddListener(OnDoneButtonClick);
        cancelButton.onClick.AddListener(OnCancelButtonClick);
        editButton.onClick.AddListener(OnEditButtonClick);
        //GameManager.instance.LoadDecorPrefabs(MainDecorpanel.transform);

       
    }

    public void Awake()
    {
        //await Task.Delay(1000);
        //await LoadAllDecorationsFromFirestore();
    }

    public void DecorClicked()
    {
        
       // GameManager.instance.isEditing = true;
        panel.SetActive(true);
        DecorUI.SetActive(true);
        desk.SetActive(false);
        Inventory.SetActive(false);
        TopUI.SetActive(false);
        ShopUI.SetActive(false);
        //DecorClickedUI.SetActive(true);
    }
    public void UnclickedDecor()
    {
     //   GameManager.instance.isEditing = false;
        GameManager.instance.clicked = false;
        //DecorClickedUI.SetActive(false); 
        DeselectAllItems();

    }

    void Update()
    {
       
    }

    public void OnDoneButtonClick()
    {
        
        foreach (DecorEdit decor in newDecorations)
        {
           //RectTransform newDecorationRectTransform = decor.GetComponent<RectTransform>();
           SaveDecor(decor);
           PlacedDecorations.Add(decor);
          // SavedPositions.Add(decor, newDecorationRectTransform.anchoredPosition);
       }

       

       
        ClearEditDecorationsInFirestore();



        GameManager.instance.isEditing = false;
        panel.SetActive(false);
        DecorUI.SetActive(false);
        desk.SetActive(true);
        TopUI.SetActive(true);
        ShopUI.SetActive(false);
        DeselectAllItems();
       // GameManager.instance.SaveDecorPrefabs(MainDecorpanel.transform);
        //SaveDecorProperties();

        //foreach (var decor in newDecorations)
        //{
        //    ListofDecors.Remove(decor);
        //}
        // Clear the list of newly placed decorations
        //EditedDecorations.Clear();
        newDecorations.Clear();
        //initialPositions.Clear();

        //SaveAllDecorations();
        // ToggleDeskAndPanel(false);
        // GameManager.instance.SaveDecorProperties(ListofDecors);
        //await LoadAllDecorationsFromFirestore();
    }

   
    public async void OnCancelButtonClick()
    {
        GameManager.instance.isEditing = false;
        DeselectAllItems();
       
       
        foreach (var kvp in initialPositions)
        {
            if (newDecorations.Contains(kvp.Key))
            {
                Destroy(kvp.Key.gameObject);
                RemoveItem(kvp.Key);
                newDecorations.Remove(kvp.Key);
            }
        }

        foreach (var kvp in SavedPositions)
        {
            if (newDecorations.Contains(kvp.Key))
            {
                Destroy(kvp.Key.gameObject);
                //RemoveItem(kvp.Key);
                newDecorations.Remove(kvp.Key);
                
            }
        }


       



        newDecorations.Clear();
        await LoadInEditDecorationsFromFirestore();

        //foreach (var decor in SavedPositions)
        //{


        //        decor.Key.rectTransform.anchoredPosition = decor.Value;

        //}
        //foreach (DecorEdit decor in newDecorations)
        //{

        //    SaveDecor(decor);
        //    PlacedDecorations.Add(decor);

        //}



        //EditedDecorations.Clear();


        //  GameManager.instance.isEditing = false;
        panel.SetActive(false);
        DecorUI.SetActive(false);
        desk.SetActive(true);
        TopUI.SetActive(true);
        ShopUI.SetActive(false);

        ClearEditDecorationsInFirestore();

        // GameManager.instance.SaveDecorPrefabs(MainDecorpanel.transform);
        // SaveDecorProperties();
    }
    
    public void RemoveItem(DecorEdit decor)
    {

        //if (decor != null)
        //{
        //    // Get the associated decoration item
        //    DecorationItem item = decor.GetAssociatedItems()[0]; // Assuming there's only one associated item

        //    // Change the quantity of the item to 1 before adding it to the transfer list
        //    GameManager.instance.AddItemToTransfer(item.ChangeQuantity(1));
        //    GameManager.instance.RemoveItem();
        //}

        if (decor != null)
        {
            // Get the associated decoration items
            List<DecorationItem> associatedItems = decor.GetAssociatedItems();

            if (associatedItems != null)
            {
                // Iterate through each associated item
                foreach (DecorationItem item in associatedItems)
                {
                    // Change the quantity of each item to 1 before adding it to the transfer list
                    GameManager.instance.AddItemToTransfer(item.ChangeQuantity(1));
                }
            }

            // Remove the decoration from the scene
            Destroy(decor.gameObject);

            // Optionally, you can remove the decoration from the list of decorations if needed
            // ListofDecors.Remove(decor);
        }

    }
    public void OnEditButtonClick()
    {
        SavedPositions.Clear();
        GameManager.instance.isEditing = true;
        ClearPlacedDecorationsInFirestore();
        foreach (DecorEdit decor in PlacedDecorations)
        {
            //EditedDecorations.Add(decor);
            RectTransform newDecorationRectTransform = decor.GetComponent<RectTransform>();
            newDecorations.Add(decor);
            SavedPositions.Add(decor, newDecorationRectTransform.anchoredPosition);
            InEditSaveDecor(decor);
            
        }
        
        panel.SetActive(true);
        DecorUI.SetActive(true);
        desk.SetActive(false);
        Inventory.SetActive(false);
        TopUI.SetActive(false);
        ShopUI.SetActive(false);

        PlacedDecorations.Clear();
        
    }
    private void DeselectAllItems()
    {
        if (newDecorations.Count > 0)
        {
            foreach (DecorEdit item in newDecorations)
            {
                item.DeSelect();
            }

        }
        if (PlacedDecorations.Count > 0)
        {
            foreach (DecorEdit item in PlacedDecorations)
            {
                item.DeSelect();
            }

        }
        //if (EditedDecorations.Count > 0)
        //{
        //    foreach (DecorEdit item in EditedDecorations)
        //    {
        //        item.DeSelect();
        //    }

        //}
    }
   
    public void UseDecor( DecorationItem Item)
    {

            GameManager.instance.isEditing = true;
            DeselectAllItems();
            //ClearPlacedDecorationsInFirestore();
            //panel.SetActive(true);
            //DecorUI.SetActive(true);
            //desk.SetActive(false);
            //Inventory.SetActive(false);
            //TopUI.SetActive(false);
            //ShopUI.SetActive(false);
            //DecorClickedUI.SetActive(true);

            // Instantiate the decoration prefab under the MainDecorPanel
            DecorEdit newDecoration = Instantiate(decorationPrefab, MainDecorpanel.transform);
            newDecoration.Select();
            // Set the decoration image
            newDecoration.GetComponent<Image>().sprite = Item.item.ItemImage;
           
            // Set the decoration's RectTransform properties
            RectTransform newDecorationRectTransform = newDecoration.GetComponent<RectTransform>();
            newDecorationRectTransform.anchorMin = Vector2.one * 0.5f; // Center the decoration
            newDecorationRectTransform.anchorMax = Vector2.one * 0.5f; // Center the decoration
            newDecorationRectTransform.pivot = Vector2.one * 0.5f; // Center the decoration
            newDecorationRectTransform.anchoredPosition = Vector2.zero; // Center the decoration
            newDecorationRectTransform.sizeDelta = new Vector2(100, 100); // Set the initial size


            ListofDecors.Add(decorationPrefab);
            ListofUseDecors.Add(Item);
            newDecoration.AddAssociatedItem(Item);
            initialPositions.Add(newDecoration, newDecorationRectTransform.anchoredPosition);
            newDecorations.Add(newDecoration);
           
            newDecoration.OnPointerClick(null);
        //InEditSaveDecor(newDecoration);

           OnEditButtonClick();


    }
    public void DecorRemove()
    {
        if (DecorationManager.selectedDecor != null)
        {
            DecorEdit selectedDecors = DecorationManager.selectedDecor;

            // Remove the selected decoration from the scene
            Destroy(selectedDecors.gameObject);

            // Remove the selected decoration from the list of decorations
            ListofDecors.Remove(selectedDecors);
            newDecorations.Remove(selectedDecors);

            if (selectedDecors.associatedItems != null)
            {
                foreach (DecorationItem item in selectedDecors.associatedItems)
                {
                    ListofUseDecors.Remove(item);
                }
            }
            // Deselect the decoration
            DecorationManager.selectedDecor = null;
        }
    }


    public void SaveAllDecorations()
    {
        if(GameManager.instance.isEditing == true)
        {
            foreach (DecorEdit decor in newDecorations)
            {
                SaveDecor(decor);
            }
        }
    }
    private void OnApplicationQuit()
    {
        //if the application quits while in editing mode
        SaveAllDecorations();
        ClearEditDecorationsInFirestore();
    }
    private async void ClearPlacedDecorationsInFirestore()
    {
        if (!string.IsNullOrEmpty(GameManager.instance.UserID))
        {
            // Create a document reference for the "PlacedDecorations" collection
            CollectionReference placedDecorationsRef = FirebaseFirestore.DefaultInstance
                .Collection("users").Document(GameManager.instance.UserID)
                .Collection("PlacedDecorations");

            // Get all documents in the "PlacedDecorations" collection
            QuerySnapshot snapshot = await placedDecorationsRef.GetSnapshotAsync();

           // Delete each document in the collection
            foreach (DocumentSnapshot docSnap in snapshot.Documents)
           {
                await docSnap.Reference.DeleteAsync();
            }

          Debug.Log("PlacedDecorations collection cleared in Firestore!");
        }
        else
        {
            Debug.LogWarning("User ID not found!");
        }
    }
    private async void ClearEditDecorationsInFirestore()
    {
        if (!string.IsNullOrEmpty(GameManager.instance.UserID))
        {
            // Create a document reference for the "PlacedDecorations" collection
           CollectionReference placedDecorationsRef = FirebaseFirestore.DefaultInstance
                .Collection("users").Document(GameManager.instance.UserID)
                .Collection("InEditDecors");

            // Get all documents in the "PlacedDecorations" collection
          QuerySnapshot snapshot = await placedDecorationsRef.GetSnapshotAsync();

           // Delete each document in the collection
           foreach (DocumentSnapshot docSnap in snapshot.Documents)
            {
               await docSnap.Reference.DeleteAsync();
           }

            Debug.Log("PlacedDecorations collection cleared in Firestore!");
        }

     
    }
    public void SaveDecor(DecorEdit decor)
    {
        if (decor != null && !string.IsNullOrEmpty(GameManager.instance.UserID))
        {
            // Create a document reference for the user's document
            DocumentReference userDocRef = FirebaseFirestore.DefaultInstance.Collection(GameManager.instance.UserCollection).Document(GameManager.instance.UserID);

            // Serialize decoration data
            DecorationData decorationData = new DecorationData(decor);
            string jsonData = JsonUtility.ToJson(decorationData);

            // Serialize associated items
            //List<string> associatedItemNames = SerializeAssociatedItems(decor);
            string associatedItemNames = JsonUtility.ToJson(decor.associatedItems[0]);
            // Save decoration data and associated item names to Firestore
            userDocRef.Collection("PlacedDecorations").AddAsync(new Dictionary<string, object>
        {
            { "data", jsonData },
            { "associatedItems", associatedItemNames } 
                // Include associated item names
        }).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("Decoration data saved to Firestore!");
            }
            else if (task.IsFaulted)
            {
                Debug.LogError("Failed to save decoration data: " + task.Exception);
            }
        });
        }
        else
        {
            Debug.LogWarning("Invalid decor or user ID!");
        }
    }
    public void InEditSaveDecor(DecorEdit decor)
    {
        if (decor != null && !string.IsNullOrEmpty(GameManager.instance.UserID))
        {
            // Create a document reference for the user's document
            DocumentReference userDocRef = FirebaseFirestore.DefaultInstance.Collection(GameManager.instance.UserCollection).Document(GameManager.instance.UserID);

            // Serialize decoration data
            DecorationData decorationData = new DecorationData(decor);
            string jsonData = JsonUtility.ToJson(decorationData);

            // Serialize associated items
            //List<string> associatedItemNames = SerializeAssociatedItems(decor);
            string associatedItemNames = JsonUtility.ToJson(decor.associatedItems[0]);
            // Save decoration data and associated item names to Firestore
            userDocRef.Collection("InEditDecors").AddAsync(new Dictionary<string, object>
        {
            { "data", jsonData },
            { "associatedItems", associatedItemNames } 
                // Include associated item names
        }).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("Decoration data saved to Firestore!");
            }
            else if (task.IsFaulted)
            {
                Debug.LogError("Failed to save decoration data: " + task.Exception);
            }
        });
        }
        else
        {
            Debug.LogWarning("Invalid decor or user ID!");
        }
    }
    private async Task LoadInEditDecorationsFromFirestore()
    {
        if (GameManager.instance.UserID != "")
        {
            // Get a reference to the user's document
            DocumentReference userDocRef = FirebaseFirestore.DefaultInstance
                .Collection(GameManager.instance.UserCollection).Document(GameManager.instance.UserID);

            // Get all documents in the "PlacedDecorations" collection
            QuerySnapshot snapshot = await userDocRef.Collection("InEditDecors").GetSnapshotAsync();

            foreach (DocumentSnapshot docSnap in snapshot.Documents)
            {
                Debug.Log("Loaded");
                // Deserialize the JSON data into a DecorationData object
                string jsonData = docSnap.GetValue<string>("data");
                DecorationData decorationData = JsonUtility.FromJson<DecorationData>(jsonData);

                // Instantiate the decoration prefab under the MainDecorPanel
                DecorEdit newDecoration = Instantiate(decorationPrefab, MainDecorpanel.transform);

                // Set the decoration image
                newDecoration.GetComponent<Image>().sprite = decorationData.decorationImage;
                // Set the decoration position, rotation, and scale
                newDecoration.transform.position = decorationData.position;
                newDecoration.transform.rotation = decorationData.rotation;
                newDecoration.transform.localScale = decorationData.scale;

                // Set the decoration's RectTransform properties
                RectTransform newDecorationRectTransform = newDecoration.GetComponent<RectTransform>();
                newDecorationRectTransform.sizeDelta = decorationData.scaledSize;




                // Load associated items
                string associatedItemNames = docSnap.GetValue<string>("associatedItems");

                DecorationItem decorationItems = JsonUtility.FromJson<DecorationItem>(associatedItemNames);


                //DecorationItem item = decordata.FindLocalItem(itemName);
                Debug.Log(decorationItems.item.Name);
                ListofUseDecors.Add(decorationItems);
                newDecoration.AddAssociatedItem(decorationItems);


                // Add the loaded decoration to the list of placed decorations
                PlacedDecorations.Add(newDecoration);

                foreach(var items in GameManager.instance.removedItemsDuringEditing)
                {
                    decordata.RemoveItem(items, 1);
                    Debug.Log("Removed item during editing: " + items.item.Name);
                }
                // If it matches, remove the item from the decoration data
                // Adjust this method based on your implementation

                // Optionally, you can log the removal or perform additional actions


                GameManager.instance.removedItemsDuringEditing.Clear();
                SaveDecor(newDecoration);

            }

            Debug.Log("Decorations loaded from Firestore!");

        }

    }

    public async Task LoadAllDecorationsFromFirestore()
    {
        if (GameManager.instance.UserID != "")
        {
            // Get a reference to the user's document
            DocumentReference userDocRef = FirebaseFirestore.DefaultInstance
                .Collection(GameManager.instance.UserCollection).Document(GameManager.instance.UserID);

            // Get all documents in the "PlacedDecorations" collection
            QuerySnapshot snapshot = await userDocRef.Collection("PlacedDecorations").GetSnapshotAsync();

            foreach (DocumentSnapshot docSnap in snapshot.Documents)
            {
                Debug.Log("Loaded");
                // Deserialize the JSON data into a DecorationData object
                string jsonData = docSnap.GetValue<string>("data");
                DecorationData decorationData = JsonUtility.FromJson<DecorationData>(jsonData);

                // Instantiate the decoration prefab under the MainDecorPanel
                DecorEdit newDecoration = Instantiate(decorationPrefab, MainDecorpanel.transform);

                // Set the decoration image
                
                // Set the decoration position, rotation, and scale
                newDecoration.transform.position = decorationData.position;
                newDecoration.transform.rotation = decorationData.rotation;
                newDecoration.transform.localScale = decorationData.scale;

                // Set the decoration's RectTransform properties
                RectTransform newDecorationRectTransform = newDecoration.GetComponent<RectTransform>();
                newDecorationRectTransform.sizeDelta = decorationData.scaledSize;

                // newDecoration.GetComponent<Image>().sprite = decorationData.decorationImage;

                if (decorationData != null && decorationData.decorationImage != null)
                {
                    newDecoration.DecorImage.sprite = decorationData.decorationImage;
                }
                // Load associated items
                string associatedItemNames = docSnap.GetValue<string>("associatedItems");



                DecorationItem decorationItems = JsonUtility.FromJson<DecorationItem>(associatedItemNames);

                
                    Debug.Log(decorationItems.item.Name);
                    ListofUseDecors.Add(decorationItems);
                    newDecoration.AddAssociatedItem(decorationItems);
                

                //DecorationItem item = decordata.FindLocalItem(itemName);
               


                // Add the loaded decoration to the list of placed decorations
                PlacedDecorations.Add(newDecoration);

            }

            Debug.Log("Decorations loaded from Firestore!");

        }
       
    }

}
[System.Serializable]
public class DecorationData
{
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 scale;
    public Sprite decorationImage;
    public Vector2 scaledSize;
    // Constructor to initialize data from a DecorEdit object
    public DecorationData(DecorEdit decor)
    {
        Vector3 positiveScale = new Vector3(
        Mathf.Abs(decor.transform.localScale.x),
        Mathf.Abs(decor.transform.localScale.y),
        Mathf.Abs(decor.transform.localScale.z)
    );


        position = decor.transform.position;
        rotation = decor.transform.rotation;
        scale = decor.transform.localScale;
        decorationImage = decor.DecorImage.sprite;
        //scaledSize = new Vector2(decor.GetComponent<RectTransform>().sizeDelta.x * scale.x, decor.GetComponent<RectTransform>().sizeDelta.y * scale.y);
        scaledSize = new Vector2(
        Mathf.Abs(decor.GetComponent<RectTransform>().sizeDelta.x * positiveScale.x),
        Mathf.Abs(decor.GetComponent<RectTransform>().sizeDelta.y * positiveScale.y)
    );
    }
}


