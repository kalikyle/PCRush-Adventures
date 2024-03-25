using Decoration.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static Decoration.Model.DecorSO;
using static UnityEditor.Progress;

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

    public GameObject DecorClickedUI;
    

    private GameObject currentDecoration;
    public DecorEdit decorationPrefab;
    private RectTransform currentDecorationRectTransform;
    private Vector3 offset;

   // private bool isEditing = false;

    public List<DecorEdit> ListofDecors = new List<DecorEdit>();
    public List<DecorationItem> ListofUseDecors = new List<DecorationItem>();
    public static DecorEdit selectedDecor;
    private Dictionary<DecorEdit, Vector2> initialPositions = new Dictionary<DecorEdit, Vector2>();
    private List<DecorEdit> newDecorations = new List<DecorEdit>();

    void Start()
    {
        // Disable the panel and set up button click listeners
        doneButton.onClick.AddListener(OnDoneButtonClick);
        cancelButton.onClick.AddListener(OnCancelButtonClick);
        editButton.onClick.AddListener(OnEditButtonClick);
        //GameManager.instance.LoadDecorPrefabs(MainDecorpanel.transform);
       

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
       // isEditing = false;
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
        newDecorations.Clear();

        // ToggleDeskAndPanel(false);
        GameManager.instance.SaveDecorProperties(ListofDecors);
    }

    public void OnCancelButtonClick()
    {
        foreach (var kvp in initialPositions)
        {
            if (newDecorations.Contains(kvp.Key))
            {
               
                Destroy(kvp.Key.gameObject);

                RemoveItem(kvp.Key);
                // Remove the decoration from the list of decorations
                //ListofDecors.Remove(kvp.Key);

                // Remove the decoration from the list of newly placed decorations
                newDecorations.Remove(kvp.Key);

               
                // Add the removed decoration back to the inventory
                
            }
        }
      //  isEditing = false;
        panel.SetActive(false);
        DecorUI.SetActive(false);
        desk.SetActive(true);
        TopUI.SetActive(true);
        ShopUI.SetActive(false);
        DeselectAllItems();
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
        // Enable editing mode
       // isEditing = true;
        panel.SetActive(true);
        DecorUI.SetActive(true);
        desk.SetActive(false);
        Inventory.SetActive(false);
        TopUI.SetActive(false);
        ShopUI.SetActive(false);
        //DecorClickedUI.SetActive(false);
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
    }
   
    public void UseDecor( DecorationItem Item)
    {

          //  isEditing = true;
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


    

    public void LoadDecorProperties()
    {
        
        string json = PlayerPrefs.GetString("DecorProperties");
        List<DecorationData> decorDataList = JsonUtility.FromJson<List<DecorationData>>(json);

        foreach (DecorationData data in decorDataList)
        {
            // Instantiate the decoration prefab under the MainDecorPanel
            DecorEdit newDecoration = Instantiate(decorationPrefab, MainDecorpanel.transform);
            newDecoration.name = data.name; // Set the decoration name
            
            // Set the sprite of the Image component using the sprite name
            Sprite sprite = Resources.Load<Sprite>("Decorations/"+data.spriteName);
            if (sprite != null)
            {
                newDecoration.GetComponent<Image>().sprite = sprite;
                Debug.LogWarning("Sprite found: " + data.spriteName);
            }
            else
            {
                Debug.LogWarning("Sprite not found: " + data.spriteName);
            }

            // Set the decoration's RectTransform properties
            RectTransform newDecorationRectTransform = newDecoration.GetComponent<RectTransform>();
            newDecorationRectTransform.anchoredPosition = data.position; // Set the saved position
            newDecorationRectTransform.sizeDelta = data.size;  // Set the saved size

            // Add the instantiated decoration to the list of decors
            ListofDecors.Add(newDecoration);
        }
    }
}


