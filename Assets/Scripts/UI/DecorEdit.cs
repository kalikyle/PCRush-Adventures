using Decoration.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DecorEdit : MonoBehaviour, IPointerClickHandler, IDragHandler, IPointerEnterHandler , IPointerExitHandler
{
    public RectTransform rectTransform;
    public GameObject Border;
    public GameObject Clicked;
    public float minSize = 50f;
    public float sizeStep = 10f;
    public float rotationStep = 15f;
    private bool isDragging = false;
    public void OnDrag(PointerEventData eventData)
    {
        if (!isDragging)
        {
            isDragging = true;
            Select();
        }

        // Implement logic for dragging the decoration here
        // For example, you can move the decoration based on drag delta
        rectTransform.anchoredPosition += eventData.delta;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isDragging)
        {
            Clicked.SetActive(true);
            Debug.Log("Clicked");
            GameManager.instance.DecorClicked();
        }
        else
        {
            isDragging = false;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isDragging)
        {
            Select();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isDragging)
        {
            DeSelect();
        }
    }

    public void Select()
    {
        Border.SetActive(true);
    }

    public void DeSelect()
    {
        Border.SetActive(false);
    }

    public void RotateLeft()
    {
        rectTransform.Rotate(Vector3.forward, rotationStep);
    }

    public void RotateRight()
    {
        rectTransform.Rotate(Vector3.forward, -rotationStep);
    }

    public void IncreaseSize()
    {
        Vector2 newSize = rectTransform.sizeDelta + new Vector2(sizeStep, sizeStep);
        newSize = Vector2.Max(newSize, new Vector2(minSize, minSize));
        rectTransform.sizeDelta = newSize;
    }

    public void DecreaseSize()
    {
        Vector2 newSize = rectTransform.sizeDelta - new Vector2(sizeStep, sizeStep);
        newSize = Vector2.Max(newSize, new Vector2(minSize, minSize));
        rectTransform.sizeDelta = newSize;
    }
}
