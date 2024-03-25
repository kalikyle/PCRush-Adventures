using Decoration.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static Decoration.Model.DecorSO;

namespace Decoration.UI
{
    public class DecorUse : MonoBehaviour
    {
        [SerializeField]
        private DecorSO decorSO;

        public UnityEngine.UI.Button Use;

        public void Start()
        {
            Use.onClick.AddListener(HandleUseButton);
        }
        private void HandleUseButton()
        {

            int tempIndex = GameManager.instance.tempindex;
            Debug.Log("Using item with temporary index: " + tempIndex);
            
            UseDecor(tempIndex);
        }
        public void UseDecor(int itemIndex)//for all
        {
            
               DecorationItem inventoryItem = decorSO.GetItemAt(itemIndex);
           
               
                UseItems(inventoryItem);
                decorSO.RemoveItem(itemIndex, 1);
                decorSO.SaveItems();


        }
        public void UseItems(DecorationItem inventoryItem)
        {

            GameManager.instance.DecorUseEnable(inventoryItem);
        }

    }
}
