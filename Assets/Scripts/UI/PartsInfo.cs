using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static Inventory.Model.PartsInventorySO;

public class PartsInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public GameObject hoverPanel;
    public Image ItemImage;
    public TMP_Text ItemName;
    public TMP_Text category;
    public TMP_Text rarity;
    public TMP_Text perks;
    public InventoryItem inventoryItem;

    public Vector2 offset = new Vector2(-100f, 0f);  // Offset for the hover panel

    private RectTransform hoverPanelRectTransform;


    public void OnPointerEnter(PointerEventData eventData)
    {
        hoverPanel.SetActive(true);

        ItemImage.sprite = inventoryItem.item.ItemImage;
        ItemName.text = inventoryItem.item.Name;
        category.text = inventoryItem.item.Category;
        rarity.text = inventoryItem.item.rarity;
        
        //perks
        string cat = inventoryItem.item.Category;
        switch (cat)
        {
            case "Case":
                perks.text = "Critical Chance +" + inventoryItem.item.CriticalChance.ToString();
                break;
            case "Motherboard":
                perks.text = "Attack Damage +" + inventoryItem.item.AttackDamage.ToString();
                break;
            case "CPU":
                perks.text = "Health +" + inventoryItem.item.Health.ToString();
                break;
            case "RAM":
                perks.text = "Armor +" + inventoryItem.item.Armor.ToString();
                break;
            case "CPU Fan":
                perks.text = "Health Regen +" + inventoryItem.item.HealthRegen.ToString();
                break;
            case "Video Card":
                perks.text = "Mana +" + inventoryItem.item.Mana.ToString();
                break;
            case "Storage":
                perks.text = "Mana Regen +" + inventoryItem.item.ManaRegen.ToString();
                break;
            case "PSU":
                perks.text = "Walk Speed +" + inventoryItem.item.WalkSpeed.ToString();
                break;
        }

        // Position the hover panel to the right of the cursor
        UpdateHoverPanelPosition(eventData.position);
    }

    

    public void OnPointerExit(PointerEventData eventData)
    {
        hoverPanel.SetActive(false);
    }

    // Start is called before the first frame update
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
