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
    public Button removeButton;
    public Button MirrorButton;
    public Button rotateLeftButton;
    public Button rotateRightButton;
    public Button ResizeIncButton;
    public Button ResizeDecButton;

    private GameObject currentDecoration;
    public DecorEdit decorationPrefab;
    private RectTransform currentDecorationRectTransform;
    private Vector3 offset;

    private bool isEditing = false;

    public List<DecorEdit> ListofDecors = new List<DecorEdit>();





    void Start()
    {
        // Disable the panel and set up button click listeners
        doneButton.onClick.AddListener(OnDoneButtonClick);
        cancelButton.onClick.AddListener(OnCancelButtonClick);
        editButton.onClick.AddListener(OnEditButtonClick);
        

    }

    public void DecorClicked()
    {
        
        isEditing = true;
        panel.SetActive(true);
        DecorUI.SetActive(true);
        desk.SetActive(false);
        Inventory.SetActive(false);
        TopUI.SetActive(false);
        ShopUI.SetActive(false);
        DecorClickedUI.SetActive(true);
    }
    public void UnclickedDecor()
    {
        isEditing = false;
        GameManager.instance.clicked = false;
        DecorClickedUI.SetActive(false);

    }

    void Update()
    {
    }

    public void OnDoneButtonClick()
    {
        isEditing = false;
        panel.SetActive(false);
        DecorUI.SetActive(false);
        desk.SetActive(true);
        TopUI.SetActive(true);
        ShopUI.SetActive(false);
        // ToggleDeskAndPanel(false);
    }

    public void OnCancelButtonClick()
    {
        // Revert to the original state
       // ToggleDeskAndPanel(true);
    }

    public void OnEditButtonClick()
    {
        // Enable editing mode
        isEditing = true;
        panel.SetActive(true);
        DecorUI.SetActive(true);
        desk.SetActive(false);
        Inventory.SetActive(false);
        TopUI.SetActive(false);
        ShopUI.SetActive(false);
        DecorClickedUI.SetActive(false);
    }

    public void UseDecor(bool Editing, DecorationItem Item)
    {
        
        isEditing = Editing;

        if (Editing == false)
        {
            // Reset editing mode
            isEditing = false;
            panel.SetActive(false);
            DecorUI.SetActive(false);
            desk.SetActive(true);
            TopUI.SetActive(true);
            ShopUI.SetActive(false);
            DecorClickedUI.SetActive(false);

        }
        else
        {
            panel.SetActive(true);
            DecorUI.SetActive(true);
            desk.SetActive(false);
            Inventory.SetActive(false);
            TopUI.SetActive(false);
            ShopUI.SetActive(false);
            DecorClickedUI.SetActive(true);

            // Instantiate the decoration prefab under the MainDecorPanel
            DecorEdit newDecoration = Instantiate(decorationPrefab, MainDecorpanel.transform);

            // Set the decoration image
            newDecoration.GetComponent<Image>().sprite = Item.item.ItemImage;
            ListofDecors.Add(decorationPrefab);
            // Set the decoration's RectTransform properties
            RectTransform newDecorationRectTransform = newDecoration.GetComponent<RectTransform>();
            newDecorationRectTransform.anchorMin = Vector2.one * 0.5f; // Center the decoration
            newDecorationRectTransform.anchorMax = Vector2.one * 0.5f; // Center the decoration
            newDecorationRectTransform.pivot = Vector2.one * 0.5f; // Center the decoration
            newDecorationRectTransform.anchoredPosition = Vector2.zero; // Center the decoration
            newDecorationRectTransform.sizeDelta = new Vector2(100, 100); // Set the initial size

            
        }
    }
}
