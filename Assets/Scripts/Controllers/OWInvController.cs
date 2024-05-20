using Firebase.Firestore;
using Inventory.Model;
using OtherWorld.Model;
using OtherWorld.UI;
using PartsInventory.Model;
using PartsInventory.UI;
using PC.Model;
using PC.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Inventory.Model.PartsInventorySO;
using static OtherWorld.Model.OWInvSO;

namespace OtherWorld
{
    public class OWInvController : MonoBehaviour
    {
        [SerializeField]
        private OWInvPage inventoryUI;

        [SerializeField]
        public OWInvSO inventoryData;

        public List<OtherWorldItem> initialItems = new List<OtherWorldItem>();
        public event Action<int> OnDescriptionRequested;

        //public OWInvItem items;




        public void Start()
        {
            //inventoryData.Initialize();
            GameManager.instance.OnOWItemsToTransferUpdated += UpdateInventory;
            
            //ToggleALLButton();

            inventoryUI.OnDescriptionRequested += HandleDescriptionRequest;
            inventoryUI.OnItemActionRequested += HandleItemActionRequest;
            OnDescriptionRequested += HandleDescriptionRequests;
            
        }

        public void Awake()
        {
            StartCoroutine(DelayedItemsLoad());
        }

        IEnumerator DelayedItemsLoad()
        {
            // Wait for 1 second
            yield return new WaitForSeconds(2f);

            // Now load initial items
            //LoadComputerItems();
            LoadItemsList();
        }

        public async void LoadItemsList()
        {
            var spriteArray = GameManager.instance.SpriteCollections.Layers;
            int spriteIndex;
            if (GameManager.instance.UserID != "")
            {
                inventoryData.Initialize();
                //GameManager.instance.pcsoDocumentIds.Clear();
                // Get a reference to the Firestore collection containing the PCSO documents
                CollectionReference collectionRef = FirebaseFirestore.DefaultInstance
                    .Collection(GameManager.instance.UserCollection)
                    .Document(GameManager.instance.UserID)
                    .Collection("OtherWorldInventory");

                // Fetch all documents from the PCSO collection asynchronously
                QuerySnapshot querySnapshot = await collectionRef.GetSnapshotAsync();

                // Iterate through the retrieved documents
                foreach (DocumentSnapshot docSnapshot in querySnapshot.Documents)
                {

                    string documentId = docSnapshot.Id;
                    GameManager.instance.pcsoDocumentIds.Add(documentId);

                    // Deserialize the PCSO data from the Firestore document
                    string itemJson = docSnapshot.GetValue<string>("Items");
                    int intJson = docSnapshot.GetValue<int>("Quantity");

                    if (!string.IsNullOrEmpty(itemJson))
                    {
                        // Create a new PCSO instance
                        OtherWorldItemSO loadedItem = ScriptableObject.CreateInstance<OtherWorldItemSO>();

                        // Deserialize the JSON data into the PCSO object
                        JsonUtility.FromJsonOverwrite(itemJson, loadedItem);
                        OtherWorldItem inventoryItem = new OtherWorldItem();


                        inventoryItem.item = loadedItem;
                        inventoryItem.quantity = intJson;


                        spriteIndex = loadedItem.SpriteIndex;
                        if (spriteIndex >= 0 && spriteIndex < spriteArray.Count)
                        {

                            Texture2D texture = spriteArray[8].Textures[spriteIndex];
                            Texture2D text2 = spriteArray[8].GetIcon(texture);
                            // Create a sprite from the texture
                            Sprite sprite = Sprite.Create(text2, new Rect(0, 0, text2.width, text2.height), Vector2.one * 0.5f);

  

                            inventoryItem.item.ItemImage = sprite;

                        }




                        inventoryData.AddItem(inventoryItem);
                        // Optionally perform any other actions with the loaded PCSO
                    }
                }

                // Log a message indicating the successful loading of PCSO items
                Debug.Log("Items items loaded from Firestore.");
            }
        }

        public void UpdateInventory(OtherWorldItem updatedItems)
        {
            //inventoryData.inventoryItems.Clear();
            //initialItems.Clear();
            //initialItems.Add(updatedItems);
           
            inventoryData.OnInventoryUpdated += UpdateInventoryUI;
            inventoryData.AddItem(updatedItems);
            inventoryData.OWSaveItems(updatedItems.item, updatedItems.quantity);
            //initialItems.Clear();
        }

        public List<OtherWorldItem> InventoryfilteredItems;
        public Dictionary<int, int> tempToOriginalIndexMapping = new Dictionary<int, int>();


        private void UpdateInventoryUI(Dictionary<int, OtherWorldItem> inventoryState)
        {
            //inventoryUI.ResetAllItems();
            foreach (var item in inventoryState)
            {
                inventoryUI.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.quantity, item.Value.item.Name);
            }
        }

        private int GetUsedSlotsCount()//this will only used the slots with items
        {
            int usedSlots = 0;
            foreach (var item in inventoryData.OWinventoryItems)
            {
                if (!item.isEmpty)
                {
                    usedSlots++;
                }
            }
            return usedSlots;
        }
        public void ToggleALLButton()
        {
            //infoButton.gameObject.SetActive(false);
            //PartsButton.gameObject.SetActive(true);
            //ComputerButton.gameObject.SetActive(true);
            ToogleFiltered = false;
            Debug.Log("Toggle: false");
            inventoryUI.ResetSelection();
            inventoryUI.ClearItems();
            inventoryUI.InitializeInventoryUI(GetUsedSlotsCount());
            //inventoryData.PartsSaveItems();
            OpenInvBTN();


        }

        public void OpenFiltered(string category)
        {
            //infoButton.gameObject.SetActive(true);

            Debug.Log("Toggle: true");

            //PartsPanel.SetActive(true);
            //ComputerPanel.SetActive(false);
            //PartsButton.gameObject.SetActive(false);
            //ComputerButton.gameObject.SetActive(false);
            //usebuttonpanel.gameObject.SetActive(true);
            
            
            ShowCategory(category);
            //infocategory = category;
            //DisplayText.text = category;
            int i = 0;
            if (inventoryUI.isActiveAndEnabled == false)
            {
                inventoryUI.Show();

                foreach (var item in InventoryfilteredItems)
                {
                    inventoryUI.UpdateData(i, item.item.ItemImage, item.quantity, item.item.Name);
                    i++;
                }
            }
            else
            {
                inventoryUI.Hide();

            }
        }

        public void OpenInvBTN()
        {

            // if (inventoryUI.isActiveAndEnabled == false)
            // {
            inventoryUI.Show();

            //DisplayText.text = "Inventory".ToUpper();
            foreach (var item in inventoryData.GetCurrentInventoryState())
            {
                inventoryUI.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.quantity, item.Value.item.Name);
            }
           

        }
        private string currentCategory = "";
        public List<OtherWorldItem> itemsToShow;
        public bool ToogleFiltered = true;
        public Dictionary<string, OtherWorldItem> lastUsedItems = new Dictionary<string, OtherWorldItem>();
        private void ShowCategory(string category)
        {
            ToogleFiltered = true;
            currentCategory = category;
            inventoryUI.ResetSelection();
            InventoryfilteredItems.Clear();
            tempToOriginalIndexMapping.Clear();
            inventoryUI.ClearItems();// Clear the existing items in the UI

            itemsToShow = inventoryData.GetItemsByCategory(currentCategory);//all by category

            int originalIndex = 0;
            int tempIndex = 0;

            foreach (var item in itemsToShow)//loop each categorized item
            {
                // Add items to the filtered list and store the mapping

                InventoryfilteredItems.Add(item);//then add to filteredItems

                tempToOriginalIndexMapping[tempIndex] = originalIndex;

                //create a new filteredItems
                inventoryUI.AddShopItem(item.item.ItemImage, item.quantity, item.item.Name);
                originalIndex++;
                tempIndex++;
            }

        }

        private void HandleDescriptionRequest(int itemIndex)
        {
            OtherWorldItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.isEmpty)
            {

                inventoryUI.ResetSelection();
                return;

            }
            OtherWorldItemSO item = inventoryItem.item;
            inventoryUI.UpdateDescription(itemIndex, item.ItemImage, item.Name, item.Category, item.Attack.ToString());
        }
        public void HandleDescriptionRequests(int obj)
        {
            if (obj >= 0 && obj < InventoryfilteredItems.Count)
            {
                OtherWorldItem shopItem = InventoryfilteredItems[obj];
                if (!shopItem.isEmpty)
                {
                    OtherWorldItemSO item = shopItem.item;
                    inventoryUI.UpdateDescription(obj, item.ItemImage, item.Name, item.Category, item.Attack.ToString());//update description
                }
            }
        }

        public void HandleItemSelection(int tempIndex)
        {
            // Use the mapping to get the original index
            if (tempToOriginalIndexMapping.TryGetValue(tempIndex, out int originalIndex))
            {
                OnDescriptionRequested?.Invoke(originalIndex);
            }
        }

        public void HandleItemActionRequest(int itemIndex)//for all
        {

            OtherWorldItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            string category = inventoryItem.item.Category;

            Debug.Log("Item has been click");

            //if (HasItemBeenUsed(inventoryItem))
            //{
            //    inventoryUI.Hide();
            //    DialogBox.gameObject.SetActive(true);
            //    DialogText.text = "Item already In use...";
            //    CanYesButton.gameObject.SetActive(false);
            //    CanNoButton.gameObject.SetActive(false);
            //    DisYesButton.gameObject.SetActive(false);
            //    DisNoButton.gameObject.SetActive(false);
            //    DialogButton.gameObject.SetActive(true);
            //}
            //else
            //{
            //    //useSound.Play();
            //    MBImage.sprite = inventoryItem.item.ItemImage; // this is for the whole inventory
            //    UseItems(inventoryItem, category);
            //    inventoryData.RemoveItem(itemIndex, 1);
            //    BackItem(inventoryItem, category);

            //    //totalUsedItemsPrice += inventoryItem.item.Price;
            //    // DisplayPrices(totalUsedItemsPrice);



            //    if (category != "Case")
            //    {

            //        Unused(inventoryItem, category);
            //    }

            //    if (CaseImage.sprite == null && MBImage.sprite == null)
            //    {
            //        usedItems.Remove(inventoryItem);
            //    }
            //    else if (CaseImage.sprite == null || MBImage.sprite == null)
            //    {
            //        usedItems.Remove(inventoryItem);
            //    }
            //    else
            //    {
            //        usedItems.Add(inventoryItem);
            //    }

            //    if (category == "CPU")
            //    {
            //        // ApplyThermal.gameObject.SetActive(true);
            //        // AT.SetCPUImage(inventoryItem.item.ItemImage);
            //    }

            //    if (category == "PSU")
            //    {
            //        if (!(CaseImage.isActiveAndEnabled && MBImage.isActiveAndEnabled && CPUImage.isActiveAndEnabled && CPUFImage.isActiveAndEnabled && RAMImage.isActiveAndEnabled && GPUButton.isActiveAndEnabled && STRG1Image.isActiveAndEnabled))
            //        {
            //            DialogBox.gameObject.SetActive(true);
            //            DialogText.text = "Can't Use this " + category + " without the needed Parts";
            //            CanYesButton.gameObject.SetActive(false);
            //            CanNoButton.gameObject.SetActive(false);
            //            DisYesButton.gameObject.SetActive(false);
            //            DisNoButton.gameObject.SetActive(false);
            //            DialogButton.gameObject.SetActive(true);
            //            try
            //            {
            //                HandleBackItem(category);

            //            }
            //            catch (Exception) { }
            //        }
            //        else
            //        {
            //            // SceneManager.LoadScene("PSUWiring", LoadSceneMode.Additive);
            //        }

            //    }
            //}



        }



    }

   
}
