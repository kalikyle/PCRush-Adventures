using Decoration.Model;
using Decoration.UI;
using Firebase.Firestore;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Decoration.Model.DecorSO;
using static UnityEditor.Progress;

namespace Decoration
{


    public class DecorController : MonoBehaviour
    {
        [SerializeField]
        private DecorPage DecorUI;

        [SerializeField]
        private DecorSO inventoryData;

        public List<DecorationItem> initialItems = new List<DecorationItem>();

        private void Start()
        {
            
            GameManager.instance.OnDecorToTransferUpdated += UpdateInventory;
            PrepareInventoryData();
            PrepareUI();
            DecorUI.ResetSelection();

            //StartCoroutine(DelayedLoad());
            LoadInitialItems();

        }
        IEnumerator DelayedLoad()
        {
            // Wait for 1 second
            yield return new WaitForSeconds(1f);

            // Now load initial items
            //LoadInitialItems();
        }
        private void Update()
        {
            //inventoryData.SaveItems();
            
        }

        private void PrepareUI()
        {
            DecorUI.InitializeInventoryUI(GetUsedSlotsCount());

            foreach (var item in inventoryData.GetCurrentInventoryState())
            {
                DecorUI.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.quantity, item.Value.item.Name, item.Value.item.Category);
            }

            DecorUI.OnItemActionRequested += HandleItemActionRequest;

        }
        public void OpenInv()
        {

            DecorUI.ClearItems();
            DecorUI.InitializeInventoryUI(GetUsedSlotsCount());

            foreach (var item in inventoryData.GetCurrentInventoryState())
            {
                DecorUI.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.quantity, item.Value.item.Name, item.Value.item.Category);
            }

            DecorUI.ResetSelection();


        }

        private void HandleItemActionRequest(int itemIndex)
        {
            DecorationItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.isEmpty)
            {

                DecorUI.ResetSelection();
                return;

            }
            DecorUI.SelectItemAtIndex(itemIndex);
            //DecorSO item = DecorationItem.item;
            //DecorUI.UpdateDescription(itemIndex, item.ItemImage, item.Name, item.Description, item.Category);
        }

        public void UpdateInventory(DecorationItem updatedItems)
        {
            //inventoryData.inventoryItems.Clear();
            //initialItems.Clear();
            //initialItems.Add(updatedItems);
            //inventoryData.Initialize();
            inventoryData.OnInventoryUpdated += UpdateInventoryUI;
            inventoryData.AddItem(updatedItems);
            inventoryData.SaveItems();
            //initialItems.Clear();
        }
        public async void LoadInitialItems()
        {
            try
            {
                DocumentReference docRef = FirebaseFirestore.DefaultInstance.Collection("users").Document("Data");
                CollectionReference subDocRef = docRef.Collection("DecorationInventory");
                DocumentReference decorDocRef = subDocRef.Document("DecorInvent");

                DocumentSnapshot snapshot = await decorDocRef.GetSnapshotAsync();

                if (snapshot.Exists)
                {
                    string jsonData = snapshot.GetValue<string>("items");
                    DecorationItemList loadedData = JsonUtility.FromJson<DecorationItemList>(jsonData);

                    if (loadedData != null)
                    {
                        initialItems.Clear();
                        GameManager.instance.DecorToTransfer.Clear();
                        inventoryData.Initialize();
                        inventoryData.OnInventoryUpdated += UpdateInventoryUI;

                        foreach (var item in loadedData.Items)
                        {
                            GameManager.instance.DecorToTransfer.Add(item);
                            if (!item.isEmpty)
                            {
                                inventoryData.AddItem(item);
                            }
                        }

                        Debug.Log("Initial decoration items loaded from Firestore.");
                    }
                }
                else
                {
                    Debug.Log("No initial decoration items found in Firestore for player.");
                }
            }
            catch (System.Exception ex)
            {
                Debug.LogError("Failed to load initial decoration items from Firestore: " + ex.Message);
            }
        }
        public void LoadInitialItemss()
        {

            string savedData = PlayerPrefs.GetString("SavedInitialItems");
            DecorationItemList loadedData = JsonUtility.FromJson<DecorationItemList>(savedData);
            if (loadedData != null)
            {
                initialItems.Clear();
                GameManager.instance.DecorToTransfer.Clear();
                inventoryData.Initialize();
                inventoryData.OnInventoryUpdated += UpdateInventoryUI;
                //inventoryData.inventoryItems.Clear();

                foreach (var item in loadedData.Items)
                {
                    GameManager.instance.DecorToTransfer.Add(item);
                    //initialItems.Add(item);
                    if (item.isEmpty) { continue; }
                    inventoryData.AddItem(item);
                }
                Debug.LogWarning("Data has been Loaded");
            }

        }
        private void PrepareInventoryData()
        {
            initialItems.AddRange(GameManager.instance.DecorToTransfer);
            inventoryData.Initialize();
            inventoryData.OnInventoryUpdated += UpdateInventoryUI;
            foreach (DecorationItem item in initialItems)
            {
                if (item.isEmpty) { continue; }
                inventoryData.AddItem(item);
            }
        }
        private int GetUsedSlotsCount()//this will only used the slots with items
        {
            int usedSlots = 0;
            foreach (var item in inventoryData.DecorationItems)
            {
                if (!item.isEmpty)
                {
                    usedSlots++;
                }
            }
            return usedSlots;
        }

        private void UpdateInventoryUI(Dictionary<int, DecorationItem> inventoryState)
        {
            //DecorUI.ResetAllItems();
            foreach (var item in inventoryState)
            {
                DecorUI.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.quantity, item.Value.item.name, item.Value.item.Category);
            }
        }

        private void HandleSellButton()
        {

            int tempIndex = GameManager.instance.tempindex;

            

                DecorationItem inventoryItem = inventoryData.GetItemAt(tempIndex);

                if (inventoryItem.item != null)
                {
                    //GameManager.instance.PCMoney += inventoryItem.item.Price;
                }

                if (inventoryItem.quantity > 1)
                {
                    // If the quantity is more than 1, decrease it by 1
                    inventoryData.RemoveItem(tempIndex, 1);
                }
                else if (inventoryItem.quantity == 1)
                {


                    inventoryData.RemoveItem(tempIndex, 1);
                    DecorUI.ResetSelection();
                    DecorUI.Hide();
                   
                }
                //GameManager.instance.UpdatePCMoneyText();
                //GameManager.instance.SavePCMoney();






            
        }
    }
}