using Inventory.Model;
using PartsInventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(PartsSO))]
public class PartsSOEditor : Editor
{
    private string[] categories = new string[] { "Case", "Motherboard", "CPU", "CPU Fan", "RAM", "Video Card", "Storage", "PSU" };
    private string[] rarities = new string[] { "Common", "Rare", "Epic", "Legend"};

    public override void OnInspectorGUI()
    {
        PartsSO item = (PartsSO)target;

        item.IsStackable = EditorGUILayout.Toggle("Is Stackable", item.IsStackable);
        EditorGUILayout.LabelField("ID", item.ID.ToString());
        item.MaxStackableSize = EditorGUILayout.IntField("Max Stackable Size", item.MaxStackableSize);
        item.Name = EditorGUILayout.TextField("Name", item.Name);
        //item.Description = EditorGUILayout.TextField("Description", item.Description);
        item.ItemImage = (Sprite)EditorGUILayout.ObjectField("Item Image", item.ItemImage, typeof(Sprite), false);
        item.Price = EditorGUILayout.DoubleField("Price", item.Price);
        //item.rarity = EditorGUILayout.TextField("Rarities", item.Name);

        // Dropdown for string-based category
        int selectedIndex = System.Array.IndexOf(categories, item.Category);
        if (selectedIndex < 0) selectedIndex = 0; // Default to first category if not found
        selectedIndex = EditorGUILayout.Popup("Category", selectedIndex, categories);
        item.Category = categories[selectedIndex];

        // Dropdown for rarities
        int selectedRarityIndex = System.Array.IndexOf(rarities, item.rarity);
        if (selectedRarityIndex < 0) selectedRarityIndex = 0; // Default to first category if not found
        selectedRarityIndex = EditorGUILayout.Popup("Rarity", selectedRarityIndex, rarities);
        item.rarity = rarities[selectedRarityIndex];




        // Show fields based on Category
        switch (item.Category)
        {
            case "Case":
                item.CriticalChance = EditorGUILayout.DoubleField("Critical Chance", item.CriticalChance);
                item.CaseStrength = EditorGUILayout.DoubleField("Case Strength", item.CaseStrength);
                break;
            case "Motherboard":
                item.AttackDamage = EditorGUILayout.DoubleField("AttackDamage", item.AttackDamage);
                item.MotherboardStrength = EditorGUILayout.DoubleField("Motherboard Strength", item.MotherboardStrength);
                item.CPUSocket = EditorGUILayout.TextField("CPU Socket", item.CPUSocket);
                item.RAMSlot = EditorGUILayout.TextField("RAM Slot", item.RAMSlot);
                break;
            case "CPU":

                item.Health = EditorGUILayout.DoubleField("Health", item.Health);
                item.BaseSpeed = EditorGUILayout.DoubleField("Base Speed", item.BaseSpeed);
                item.CPUSupportedSocket = EditorGUILayout.TextField("CPU Supported Socket", item.CPUSupportedSocket);
                break;
            case "RAM":
                item.Armor = EditorGUILayout.DoubleField("Armor", item.Armor);
                item.Memory = EditorGUILayout.DoubleField("Memory", item.Memory);
                item.RAMSupportedSlot = EditorGUILayout.TextField("RAM Supported Slot", item.RAMSupportedSlot);
                break;
            case "CPU Fan":
                item.HealthRegen = EditorGUILayout.DoubleField("Health Regen", item.HealthRegen);
                item.CoolingPower = EditorGUILayout.DoubleField("Cooling Power", item.CoolingPower);
                break;
            case "Video Card":
                item.Mana = EditorGUILayout.DoubleField("Mana", item.Mana);
                item.ClockSpeed = EditorGUILayout.DoubleField("Clock Speed", item.ClockSpeed);
                break;
            case "Storage":
                item.ManaRegen = EditorGUILayout.DoubleField("Mana Regen", item.ManaRegen);
                item.Storage = EditorGUILayout.DoubleField("Storage", item.Storage);
                break;
            case "PSU":
                item.WalkSpeed = EditorGUILayout.DoubleField("Walk Speed", item.WalkSpeed);
                item.WattagePower = EditorGUILayout.DoubleField("Wattage Power", item.WattagePower);
                break;
        }

        // Save changes
        if (GUI.changed)
        {
            EditorUtility.SetDirty(item);
        }
    }
}
