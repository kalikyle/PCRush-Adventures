using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DecorEdit : MonoBehaviour, IPointerClickHandler, IDragHandler, IPointerEnterHandler , IPointerExitHandler
{
    public RectTransform rectTransform;
    public GameObject Border;

    public void OnDrag(PointerEventData eventData)
    {
        // Implement logic for dragging the decoration here
        // For example, you can move the decoration based on drag delta
        rectTransform.anchoredPosition += eventData.delta;
        Select();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Check if a drag operation is ongoing
        if (eventData.dragging)
        {
            // Skip resizing if dragging
            return;
        }
        Select();
        Debug.Log("Clicked");
        rectTransform.localScale *= 1.2f; // Increase size by 20%
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Select();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        DeSelect();
    }

    public void Select()
    {
        Border.gameObject.SetActive(true);
    }
    public void DeSelect()
    {
        Border.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
