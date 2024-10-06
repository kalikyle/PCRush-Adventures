using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static Inventory.Model.InventorySO;


public class MultiPartsInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // Start is called before the first frame update

    public GameObject hoverPanel;
    public Image ItemImage;
    public TMP_Text ItemName;
    public TMP_Text category;
    public TMP_Text Speed;
    public TMP_Text Compatibility;
    public InventoryItem inventoryItem;

    public Vector2 offset = new Vector2(-100f, 0f);  // Offset for the hover panel

    private RectTransform hoverPanelRectTransform;

    public void OnPointerEnter(PointerEventData eventData)
    {
        hoverPanel.SetActive(true);

        ItemImage.sprite = inventoryItem.item.ItemImage;
        ItemName.text = inventoryItem.item.Name;
        category.text = inventoryItem.item.Category;

        //compats
        string cat = inventoryItem.item.Category;
        switch (cat)
        {
            case "Case":
                Speed.text = "Case Strenght: " + inventoryItem.item.CaseStrength;
                Compatibility.text = "";
                break;
            case "Motherboard":
                Speed.text = "Motherboad Strenght: " + inventoryItem.item.MotherboardStrength;
                Compatibility.text = inventoryItem.item.CPUSocket + " - " + inventoryItem.item.RAMSlot;
                break;
            case "CPU":
                Speed.text = "Base Speed: " + inventoryItem.item.BaseSpeed + "Ghz";
                Compatibility.text = inventoryItem.item.CPUSupportedSocket;
                break;
            case "RAM":
                Speed.text = "Memory: " + inventoryItem.item.Memory + "GB";
                Compatibility.text = inventoryItem.item.RAMSupportedSlot;
                break;
            case "CPU Fan":
                Speed.text = "Cooling Power: " + inventoryItem.item.CoolingPower;
                Compatibility.text = "";
                break;
            case "Video Card":
                Speed.text = "Clock Speed: " + inventoryItem.item.ClockSpeed + "Mhz";
                Compatibility.text = "";
                break;
            case "Storage":
                Speed.text = "Storage: " + inventoryItem.item.Storage + "GB";
                Compatibility.text = "";
                break;
            case "PSU":
                Speed.text = "Wattage Power: " + inventoryItem.item.WattagePower + "W";
                Compatibility.text = "";
                break;
        }

        // Position the hover panel to the right of the cursor
        UpdateHoverPanelPosition(eventData.position);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hoverPanel.SetActive(false);
    }

    void Start()
    {
        hoverPanelRectTransform = hoverPanel.GetComponent<RectTransform>();
    }

    private void UpdateHoverPanelPosition(Vector2 cursorPosition)
    {
        // Convert the cursor position from screen space to world space
        Vector2 hoverPanelPosition = cursorPosition + offset;

        // Apply the new position to the hover panel's RectTransform
        hoverPanelRectTransform.position = hoverPanelPosition;

        // Optionally, clamp the hover panel position to keep it within screen bounds
        Vector2 clampedPosition = ClampToScreen(hoverPanelRectTransform, hoverPanelPosition);
        hoverPanelRectTransform.position = clampedPosition;
    }

    // Optional: Ensure the hover panel stays within screen bounds
    private Vector2 ClampToScreen(RectTransform panelRectTransform, Vector2 desiredPosition)
    {
        Vector2 minPosition = panelRectTransform.rect.size / 2;
        Vector2 maxPosition = new Vector2(Screen.width, Screen.height) - minPosition;

        float clampedX = Mathf.Clamp(desiredPosition.x, minPosition.x, maxPosition.x);
        float clampedY = Mathf.Clamp(desiredPosition.y, minPosition.y, maxPosition.y);

        return new Vector2(clampedX, clampedY);
    }

    public void Update()
    {
        if (gameObject == null)
        {
            hoverPanel.SetActive(false);
        }
    }
}
