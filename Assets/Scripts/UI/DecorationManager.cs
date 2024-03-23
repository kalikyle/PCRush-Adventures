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

    private bool isEditing = false;

    public List<DecorEdit> ListofDecors = new List<DecorEdit>();

   





    void Start()
    {
        // Disable the panel and set up button click listeners
        doneButton.onClick.AddListener(OnDoneButtonClick);
        cancelButton.onClick.AddListener(OnCancelButtonClick);
        editButton.onClick.AddListener(OnEditButtonClick);
        GameManager.instance.LoadDecorPrefabs(MainDecorpanel.transform);
        LoadDecorProperties();

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
        //DecorClickedUI.SetActive(true);
    }
    public void UnclickedDecor()
    {
        isEditing = false;
        GameManager.instance.clicked = false;
        //DecorClickedUI.SetActive(false); 
        DeselectAllItems();

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

        GameManager.instance.SaveDecorPrefabs(MainDecorpanel.transform);
        SaveDecorProperties();
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
        //DecorClickedUI.SetActive(false);
    }
    private void DeselectAllItems()
    {
        foreach (DecorEdit item in ListofDecors)
        {
            item.DeSelect();
        }
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
            //DecorClickedUI.SetActive(false);

        }
        else
        {
            panel.SetActive(true);
            DecorUI.SetActive(true);
            desk.SetActive(false);
            Inventory.SetActive(false);
            TopUI.SetActive(false);
            ShopUI.SetActive(false);
            //DecorClickedUI.SetActive(true);

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
    [System.Serializable]
    public class DecorationData //needfix
    {
        public string name;
        public Vector2 position;
        public Vector2 size;
        // Add more properties as needed
    }

    public void SaveDecorProperties()
    {
        List<DecorationData> decorDataList = new List<DecorationData>();
        foreach (DecorEdit decor in ListofDecors)
        {
            DecorationData data = new DecorationData();
            data.name = decor.name; // Assuming each decoration has a unique name
            data.position = decor.rectTransform.anchoredPosition;
            data.size = decor.rectTransform.sizeDelta;
            decorDataList.Add(data);
        }

        string json = JsonUtility.ToJson(decorDataList);
        PlayerPrefs.SetString("DecorProperties", json);
        PlayerPrefs.Save();
    }

    public void LoadDecorProperties()
    {
        string json = PlayerPrefs.GetString("DecorProperties");
        List<DecorationData> decorDataList = JsonUtility.FromJson<List<DecorationData>>(json);
        foreach (DecorationData data in decorDataList)
        {
            // Find the corresponding decoration by name
            DecorEdit decor = ListofDecors.Find(d => d.name == data.name);
            if (decor != null)
            {
                // Apply saved properties
                decor.rectTransform.anchoredPosition = data.position;
                decor.rectTransform.sizeDelta = data.size;
            }
        }
    }


}
