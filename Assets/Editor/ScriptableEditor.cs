using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Shop.Model.Editor
{
    [CustomEditor(typeof(ShopItemSO))]
    public class MonitorShopItemSOEditor : UnityEditor.Editor
    {
        private string[] categoryOptions = new string[] { "Monitor", "Mouse", "MousePad" ,"Keyboard", "Desk", "Background", "Decorations" };

        public override void OnInspectorGUI()
        {
            ShopItemSO item = (ShopItemSO)target;

            EditorGUI.BeginChangeCheck();
            int selectedIndex = EditorGUILayout.Popup("Category", ArrayUtility.IndexOf(categoryOptions, item.Category), categoryOptions);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(item, "Category Change");
                item.Category = categoryOptions[selectedIndex];
                EditorUtility.SetDirty(item);
            }

            // Draw other serialized fields
            DrawDefaultInspector();
        }
    }
}
