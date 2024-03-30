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
//using static UnityEditor.Progress;

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

    private bool isEditing = false;

    public List<DecorEdit> ListofDecors = new List<DecorEdit>();
    public List<DecorationItem> ListofUseDecors = new List<DecorationItem>();
    public static DecorEdit selectedDecor;
    private Dictionary<DecorEdit, Vector2> initialPositions = new Dictionary<DecorEdit, Vector2>();
    private Dictionary<DecorEdit, Vector2> SavedPositions = new Dictionary<DecorEdit, Vector2>();
    private List<DecorEdit> newDecorations = new List<DecorEdit>();
    //private List<DecorEdit> EditedDecorations = new List<DecorEdit>();
    private List<DecorEdit> PlacedDecorations = new List<DecorEdit>();


    private async void Start()
    {
       // ExecuteAfterDelay(2f);
        // Disable the panel and set up button click listeners
        doneButton.onClick.AddListener(OnDoneButtonClick);
        cancelButton.onClick.AddListener(OnCancelButtonClick);
        editButton.onClick.AddListener(OnEditButtonClick);
        //GameManager.instance.LoadDecorPrefabs(MainDecorpanel.transform);

        await Task.Delay(1000);
        await Loadall();
    }

    private IEnumerator ExecuteAfterDelay(float delayInSeconds)
    {
        yield return new WaitForSeconds(delayInSeconds);

        // Call the methods after the delay
       
    }

    public async Task Loadall()
    {
       await LoadAllDecorationsFromFirestore();
    }
    public void Awake()
    {
        //LoadDecorProperties();
    }

    public void DecorClicked()
    {
        
       // isEditing = true;
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
     //   isEditing = false;
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


        isEditing = false;
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
    }

    public void OnCancelButtonClick()
    {
        isEditing = false;
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


        //foreach (var decor in SavedPositions)
        //{
           

        //        decor.Key.rectTransform.anchoredPosition = decor.Value;
            
        //}

        foreach (DecorEdit decor in newDecorations)
        {
            
            SaveDecor(decor);
            PlacedDecorations.Add(decor);
            
        }



        //EditedDecorations.Clear();
        newDecorations.Clear();

        //  isEditing = false;
        panel.SetActive(false);
        DecorUI.SetActive(false);
        desk.SetActive(true);
        TopUI.SetActive(true);
        ShopUI.SetActive(false);

        
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
        isEditing = true;
        ClearPlacedDecorationsInFirestore();
        foreach (DecorEdit decor in PlacedDecorations)
        {
            //EditedDecorations.Add(decor);
            newDecorations.Add(decor);
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

            isEditing = true;
            DeselectAllItems();
            panel.SetActive(true);
            DecorUI.SetActive(true);
            desk.SetActive(false);
            Inventory.SetActive(false);
            TopUI.SetActive(false);
            ShopUI.SetActive(false);
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
        if(isEditing == true)
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

    private List<string> SerializeAssociatedItems(DecorEdit decor)
    {
        List<string> serializedItems = new List<string>();
        foreach (DecorationItem item in decor.GetAssociatedItems())
        {
            
            serializedItems.Add(item.item.Name); 
        }
        return serializedItems;
    }
    private async Task LoadAllDecorationsFromFirestore()
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

            }

            Debug.Log("Decorations loaded from Firestore!");

        }
       
    }

    private DecorationItem FetchDecorationItem(string itemName)
    {
       
        foreach (DecorationItem item in decordata.DecorationItems)
        {
            if (item.item.Name == itemName)
            {
                // Return the found DecorationItem
                return item;
            }
        }

        // If no matching item is found, return null
        return new DecorationItem();
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
        decorationImage = decor.GetComponent<Image>().sprite;
        //scaledSize = new Vector2(decor.GetComponent<RectTransform>().sizeDelta.x * scale.x, decor.GetComponent<RectTransform>().sizeDelta.y * scale.y);
        scaledSize = new Vector2(
        Mathf.Abs(decor.GetComponent<RectTransform>().sizeDelta.x * positiveScale.x),
        Mathf.Abs(decor.GetComponent<RectTransform>().sizeDelta.y * positiveScale.y)
    );
    }
}


