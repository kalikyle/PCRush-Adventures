using Decoration.Model;
using Decoration.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static Decoration.Model.DecorSO;

//

public class DecorEdit : MonoBehaviour, IPointerClickHandler, IDragHandler
{
    public RectTransform rectTransform;
    public GameObject Border;
    //public GameObject Clicked;
    public Image DecorImage;
    public float minSize = 50f;
    public float sizeStep = 10f;
    public float rotationStep = 15f;

    

    private static DecorEdit selectedDecor; // Static variable to keep track of the selected decoration
    private Vector3 initialMousePosition;
    private Vector3 initialOffset;

    public DecorSO decorSO;
    public void Start()
    {
        GameManager.instance.rotateLeftButton.onClick.AddListener(RotateLeft);
        GameManager.instance.rotateRightButton.onClick.AddListener(RotateRight);
        GameManager.instance.ResizeIncButton.onClick.AddListener(IncreaseSize);
        GameManager.instance.ResizeDecButton.onClick.AddListener(DecreaseSize);
        GameManager.instance.MirrorButton.onClick.AddListener(MirrorHorizontally);
        GameManager.instance.removeButton.onClick.AddListener(RemoveItem);
        GameManager.instance.DoneButton.onClick.AddListener(DeSelect);
    }
    public List<DecorationItem> associatedItems = new List<DecorationItem>();
    //public DecorationItem associatedItems = new DecorationItem();
    private DecorationItem clickedItem;

    // Method to add an associated DecorationItem
    public void AddAssociatedItem(DecorationItem item)
    {
        associatedItems.Add(item);
    }
  

    // Method to get the associated DecorationItems
    public List<DecorationItem> GetAssociatedItems()
    {
        return associatedItems;
    }
    //public void Update()
    //{
    //    if (selectedDecor == null)
    //    {
    //        // If no decoration is selected, select this one when dragging starts
    //        selectedDecor = this;
    //        initialMousePosition = Input.mousePosition;
    //        Vector3 decorationCenter = rectTransform.position;
    //        initialOffset = decorationCenter - Camera.main.ScreenToWorldPoint(initialMousePosition);
    //    }

    //    if (selectedDecor == this)
    //    {
    //        Select();

    //        // Get the current mouse position and convert it to world space
    //        Vector3 mousePosition = Input.mousePosition;
    //        Vector3 convertedMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
    //        convertedMousePosition.z = 0; // Keep the z position at 0 for 2D objects

    //        // Calculate the target position based on the initial offset
    //        Vector3 targetPosition = convertedMousePosition + initialOffset;

    //        // Update the position of the decoration
    //        rectTransform.position = targetPosition;

    //    }
    //}
    public void OnDrag(PointerEventData eventData)
    {
        if (selectedDecor == null)
        {
            // If no decoration is selected, select this one when dragging starts
            selectedDecor = this;
            //Select();
            initialMousePosition = Input.mousePosition;
            
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform)rectTransform.parent, // Parent RectTransform
            initialMousePosition,
            eventData.pressEventCamera,          // The camera being used for the UI (usually the event camera)
            out Vector2 localMousePosition
        );

            initialOffset = (Vector2)rectTransform.anchoredPosition - localMousePosition;


            //initialOffset = decorationCenter - initialMousePosition;

        }

        if (selectedDecor == this)
        {

            Select();
            // Calculate the target position based on the initial offset
            //Vector3 targetPosition = Input.mousePosition;

            // Calculate the delta movement based on the difference between the current and previous mouse positions
            //Vector2 deltaMovement = (targetPosition - rectTransform.position) / Screen.dpi;

            //// Update the anchored position with the delta movement
            //rectTransform.anchoredPosition += deltaMovement;


            // Update the position based on the initial offset so the object follows the mouse correctly
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform)rectTransform.parent, // Parent RectTransform
            Input.mousePosition,
            eventData.pressEventCamera,          // The camera being used for the UI
            out Vector2 localMousePosition
        );

            // Update the anchored position with the initial offset
            rectTransform.anchoredPosition = localMousePosition + (Vector2)initialOffset;


        }



    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (selectedDecor != null && selectedDecor != this)
        {
            selectedDecor.DeSelect(); // Deselect the previously selected decoration
        }
        
            Select();
            Debug.Log("Clicked");
            GameManager.instance.DecorClicked();
            selectedDecor = this; // Set the current decoration as the selected one
            List<DecorationItem> associatedItems = GetAssociatedItems();
            DecorationManager.selectedDecor = this;
            // Use the first associated item as the clicked item

            if (associatedItems.Count > 0)
            {
                clickedItem = associatedItems[0];
                Debug.Log("Clicked: " + clickedItem.item.Name);
            }



    }
    public void RemoveItem()
    {
        if (selectedDecor == this)
        {
            GameManager.instance.AddItemToTransfer(clickedItem.ChangeQuantity(clickedItem.quantity - clickedItem.quantity + 1));
            GameManager.instance.RemoveItem();

            if(GameManager.instance.isEditing == true)
            {
                GameManager.instance.removedItemsDuringEditing.Add(clickedItem);
            }

        } 
    }
    //public void RemoveItemDuringEditing(DecorationItem item)
    //{
    //    if (!removedItemsDuringEditing.Contains(item))
    //    {
    //        removedItemsDuringEditing.Add(item);
    //    }
    //}


    public void Select()
    {
        Border.SetActive(true);
        //GameManager.instance.DecorClickedUI.gameObject.SetActive(true);
    }

    public void DeSelect()
    {
        Border.SetActive(false);
        //GameManager.instance.DecorClickedUI.gameObject.SetActive(false);
    }

    public void RotateLeft()
    {
        if (selectedDecor == this)
        {
            rectTransform.Rotate(Vector3.forward, rotationStep);
        }
    }

    public void RotateRight()
    {
        if (selectedDecor == this)
        {
            rectTransform.Rotate(Vector3.forward, -rotationStep);
        }
    }

    public void IncreaseSize()
    {
        //if (selectedDecor == this)
        //{
        //    Vector2 newSize = rectTransform.sizeDelta + new Vector2(sizeStep, sizeStep);
        //    newSize = Vector2.Max(newSize, new Vector2(minSize, minSize));
        //    rectTransform.sizeDelta = newSize;
        //}

        if (selectedDecor == this)
        {
            Vector2 newSize = rectTransform.sizeDelta + new Vector2(sizeStep, sizeStep);
            newSize.x = Mathf.Max(newSize.x, minSize);
            newSize.y = Mathf.Max(newSize.y, minSize);
            rectTransform.sizeDelta = newSize;
        }
    }

    public void DecreaseSize()
    {
        //if (selectedDecor == this)
        //{
        //    Vector2 newSize = rectTransform.sizeDelta - new Vector2(sizeStep, sizeStep);
        //    newSize = Vector2.Max(newSize, new Vector2(minSize, minSize));
        //    rectTransform.sizeDelta = newSize;
        //}

        if (selectedDecor == this)
        {
            Vector2 newSize = rectTransform.sizeDelta - new Vector2(sizeStep, sizeStep);
            newSize.x = Mathf.Max(newSize.x, minSize);
            newSize.y = Mathf.Max(newSize.y, minSize);
            rectTransform.sizeDelta = newSize;
        }
    }

    public void MirrorHorizontally()
    {
        if (selectedDecor == this)
        {
            // Get the current scale
            Vector3 currentScale = rectTransform.localScale;

            // Calculate the new scale with X-axis mirrored
            Vector3 newScale = new Vector3(-currentScale.x, currentScale.y, currentScale.z);

            // Update the scale
            rectTransform.localScale = newScale;
        }
    }


}
