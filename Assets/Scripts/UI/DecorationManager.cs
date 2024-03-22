using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DecorationManager : MonoBehaviour
{
    public GameObject panel;
    public GameObject desk;
    public GameObject decorationPrefab;
    public Button doneButton;
    public Button cancelButton;
    public Button editButton;
    public Button removeButton;

    private bool isEditing = false;

    void Start()
    {
        // Disable the panel and set up button click listeners
        panel.SetActive(false);
        doneButton.onClick.AddListener(OnDoneButtonClick);
        cancelButton.onClick.AddListener(OnCancelButtonClick);
        editButton.onClick.AddListener(OnEditButtonClick);
    }

    void Update()
    {
        // Check if the panel is active and the player is not clicking on a UI element
        if (panel.activeSelf && !EventSystem.current.IsPointerOverGameObject())
        {
            // Check for mouse input to drag and resize the decoration
            if (Input.GetMouseButton(0))
            {
                // Implement drag and resize logic here
            }
        }
    }

    void OnDoneButtonClick()
    {
        // Place the decoration in the panel
        Instantiate(decorationPrefab, panel.transform);
        ToggleDeskAndPanel(false);
    }

    void OnCancelButtonClick()
    {
        // Revert to the original state
        ToggleDeskAndPanel(true);
    }

    void OnEditButtonClick()
    {
        // Enable editing mode
        isEditing = true;
        removeButton.gameObject.SetActive(true);
    }

    void ToggleDeskAndPanel(bool showDesk)
    {
        // Toggle the visibility of the desk and panel
        desk.SetActive(showDesk);
        panel.SetActive(!showDesk);
    }
}
