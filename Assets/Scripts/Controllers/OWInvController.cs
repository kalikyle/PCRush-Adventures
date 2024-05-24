using Assets.PixelHeroes.Scripts.CharacterScrips;
using Assets.PixelHeroes.Scripts.CollectionScripts;
using Assets.PixelHeroes.Scripts.EditorScripts;
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
using System.Linq;
using System.Threading.Tasks;
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

        public List<LayerEditor> Layers;
        public CharacterBuilder CharacterBuilder;

        public Image SwordImage;

        public Button EquipBTN;

        //public OWInvItem items;




        public void Start()
        {
            //inventoryData.Initialize();
            GameManager.instance.OnOWItemsToTransferUpdated += UpdateInventory;
            
            //ToggleALLButton();

            inventoryUI.OnDescriptionRequested += HandleDescriptionRequest;
            inventoryUI.OnItemActionRequested += HandleItemActionRequest;
            OnDescriptionRequested += HandleDescriptionRequests;
            EquipBTN.onClick.AddListener(HandleUseButton);

        }


        private void HandleUseButton()
        {
           
            

            int tempIndex = GameManager.instance.OWstempindex;
            Debug.Log("Using item with temporary index: " + tempIndex);

          


            if (ToogleFiltered)
            {
                Debug.Log(ToogleFiltered);
                HandleItemRightActionRequest(tempIndex);

            }
            else
            {
                Debug.Log(ToogleFiltered);
                HandleItemActionRequest(tempIndex);

            }



            StartCoroutine(OpenOtherWorldInventory());









        }

        public IEnumerator OpenOtherWorldInventory()
        {

            inventoryData.OWinventoryItems.Clear();
            yield return new WaitForSeconds(.2f);
            LoadItemsList();
            yield return new WaitForSeconds(.2f);
            ToggleALLButton();
            

        }
        public void Awake()
        {
            //StartCoroutine(OtherWorldInventory());
        }

        public IEnumerator OtherWorldInventory()
        {
            // Wait for 1 second
            yield return new WaitForSeconds(.5f);

            // Now load initial items
            //LoadComputerItems();
            LoadItemsList();
        }

        public async void LoadItemsList()
        {
            var spriteArray = GameManager.instance.SpriteCollections.Layers;
            int spriteIndex;
            GameManager.instance.SwordsDocumentIds.Clear();
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

                        if (inventoryItem.item.Category == "Sword")
                        {
                           
                            GameManager.instance.SwordsDocumentIds.Add(documentId);
                            if (inventoryItem.item.inUse)
                            {
                                //UseloadComputer(loadedPCSO);
                                UseItem(inventoryItem);
                                GameManager.instance.SwordinUse = documentId;
                            }

                           
                        }
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

            inventoryData.OWinventoryItems.Clear();
            StartCoroutine(OtherWorldInventory());
        }

        

        public List<OtherWorldItem> InventoryfilteredItems;
        public Dictionary<int, int> tempToOriginalIndexMapping = new Dictionary<int, int>();


        private void UpdateInventoryUI(Dictionary<int, OtherWorldItem> inventoryState)
        {
            //inventoryUI.ResetAllItems();
            foreach (var item in inventoryState)
            {
                inventoryUI.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.quantity, item.Value.item.Name, item.Value.item.inUse);
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
                    inventoryUI.UpdateData(i, item.item.ItemImage, item.quantity, item.item.Name, item.item.inUse);
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
                inventoryUI.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.quantity, item.Value.item.Name, item.Value.item.inUse);
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
            if (item.Category == "Sword")
            {
                GameManager.instance.clickedInventoryItemID = GameManager.instance.SwordsDocumentIds[itemIndex];
            }


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

                    if(item.Category == "Sword")
                    {
                        GameManager.instance.clickedInventoryItemID = GameManager.instance.SwordsDocumentIds[obj];
                    }
                    
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

        public void HandleItemRightActionRequest(int tempIndex)//for filtered
        {

            //if (tempToOriginalIndexMapping.TryGetValue(tempIndex, out int originalIndex))
            //{

            //    InventoryItem inventoryItem = InventoryfilteredItems[originalIndex];
            //    string category = inventoryItem.item.Category;
            //    if (HasItemBeenUsed(inventoryItem))
            //    {
            //        inventoryUI.Hide();
            //        DialogBox.gameObject.SetActive(true);
            //        DialogText.text = "Item already In use...";
            //        CanYesButton.gameObject.SetActive(false);
            //        CanNoButton.gameObject.SetActive(false);
            //        DisYesButton.gameObject.SetActive(false);
            //        DisNoButton.gameObject.SetActive(false);
            //        DialogButton.gameObject.SetActive(true);
            //    }
            //    else
            //    {
            //        // useSound.Play();
            //        usedItems.Remove(inventoryItem);
            //        UseItems(inventoryItem, category);
            //        int index = inventoryData.inventoryItems.IndexOf(inventoryItem);

            //        inventoryData.RemoveItem(index, 1);
            //        BackItem(inventoryItem, category);

            //        //  totalUsedItemsPrice += inventoryItem.item.Price;
            //        //DisplayPrices(totalUsedItemsPrice);

            //        if (category != "Case")
            //        {
            //            Unused(inventoryItem, category);
            //        }

            //        if (CaseImage.sprite == null && MBImage.sprite == null)
            //        {
            //            usedItems.Remove(inventoryItem);
            //        }
            //        else if (CaseImage.sprite == null || MBImage.sprite == null)
            //        {
            //            usedItems.Remove(inventoryItem);
            //        }
            //        else
            //        {
            //            usedItems.Add(inventoryItem);
            //        }

            //        if (category == "CPU")
            //        {
            //            // ApplyThermal.gameObject.SetActive(true);
            //            // AT.SetCPUImage(inventoryItem.item.ItemImage);
            //        }

            //        if (category == "PSU")
            //        {
            //            if (!(CaseImage.isActiveAndEnabled && MBImage.isActiveAndEnabled && CPUImage.isActiveAndEnabled && CPUFImage.isActiveAndEnabled && RAMImage.isActiveAndEnabled && GPUButton.isActiveAndEnabled && STRG1Image.isActiveAndEnabled))
            //            {
            //                DialogBox.gameObject.SetActive(true);
            //                DialogText.text = "Can't Use this " + category + " without the needed Parts";
            //                CanYesButton.gameObject.SetActive(false);
            //                CanNoButton.gameObject.SetActive(false);
            //                DisYesButton.gameObject.SetActive(false);
            //                DisNoButton.gameObject.SetActive(false);
            //                DialogButton.gameObject.SetActive(true);
            //                try
            //                {
            //                    HandleBackItem(category);

            //                }
            //                catch (Exception) { }
            //            }
            //            else
            //            {
            //                //SceneManager.LoadScene("PSUWiring", LoadSceneMode.Additive);
            //            }


            //        }

            //    }

            //}
        }
        //for all 

        public void UseItem(OtherWorldItem inventoryItem)//for all
        {

            OtherWorldItemSO inventItem = inventoryItem.item;
            string category = inventoryItem.item.Category;


            if (category == "Sword")
            {

                SwordImage.gameObject.SetActive(true);
                SwordImage.sprite = inventoryItem.item.ItemImage;

                foreach (var layer in Layers)
                {

                    if (layer.Controls)
                    {
                        layer.Content = GameManager.instance.SpriteCollections.Layers.Single(i => i.Name == layer.Name);

                        if (layer.Name == "Weapon")
                        {
                            SetIndex(layer, inventoryItem.item.SpriteIndex + (layer.CanBeEmpty ? 1 : 0));
                            Rebuild(layer);
                        }

                    }

                }
               
            }

        }

        public async void HandleItemActionRequest(int itemIndex)//for all
        {

            OtherWorldItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            OtherWorldItemSO inventItem = inventoryItem.item;
            string category = inventoryItem.item.Category;


            if (category == "Sword")
            {

                SwordImage.gameObject.SetActive(true);
                SwordImage.sprite = inventoryItem.item.ItemImage;

                foreach (var layer in Layers)
                {

                    if (layer.Controls)
                    {
                        layer.Content = GameManager.instance.SpriteCollections.Layers.Single(i => i.Name == layer.Name);

                        if (layer.Name == "Weapon")
                        {
                            SetIndex(layer, inventoryItem.item.SpriteIndex + (layer.CanBeEmpty ? 1 : 0));
                            Rebuild(layer);
                        }

                    }

                }
                inventoryItem.item.inUse = true;
                await UpdateInventoryItem(GameManager.instance.clickedInventoryItemID, inventoryItem.item);
            }
            
         }

        private void SetIndex(LayerEditor layer, int index)
        {
            if (layer.CanBeEmpty) index--;

            layer.SetIndex(index);

            if (layer.Name == "Body")
            {
                Layers.Single(i => i.Name == "Head").SetIndex(index);
            }

            Rebuild(layer);
        }

        private void Rebuild(LayerEditor layer)
        {
            var layers = Layers.ToDictionary(i => i.Name, i => i.SpriteData);

            CharacterBuilder.Head = layers["Head"];
            CharacterBuilder.Body = layers["Body"];
            CharacterBuilder.Hair = layers["Hair"];
            CharacterBuilder.Armor = layers["Armor"];
            CharacterBuilder.Helmet = layers["Helmet"];
            CharacterBuilder.Weapon = layers["Weapon"];
            CharacterBuilder.Shield = layers["Shield"];
            CharacterBuilder.Cape = layers["Cape"];
            CharacterBuilder.Back = layers["Back"];
            CharacterBuilder.Rebuilds(layer?.Name);


        }
        public async Task UpdateInventoryItem(string documentId, OtherWorldItemSO UpdatedItem)
        {
            try
            {
                // Convert the updated PCSO object to JSON
                string updatedPCSOJson = JsonUtility.ToJson(UpdatedItem);
                //pcsothatinUse = documentId;

                if (!string.IsNullOrEmpty(GameManager.instance.SwordinUse) && GameManager.instance.SwordinUse != documentId)
                {
                    // Update the PCSO that was previously in use to set inUse = false
                    await UpdateInventoryInUse(GameManager.instance.SwordinUse, false);
                }


                // Get a reference to the Firestore document to be updated
                DocumentReference docRef = FirebaseFirestore.DefaultInstance
                    .Collection(GameManager.instance.UserCollection)
                    .Document(GameManager.instance.UserID)
                    .Collection("OtherWorldInventory")
                    .Document(documentId);

                // Create a dictionary to store the updated PCSO data
                Dictionary<string, object> updateData = new Dictionary<string, object>
        {
            { "Items", updatedPCSOJson }
        };

                // Update the Firestore document with the new data
                await docRef.UpdateAsync(updateData);
                GameManager.instance.SwordinUse = documentId;
                Debug.Log("PCSO document updated successfully.");
            }
            catch (Exception ex)
            {
                Debug.LogError("Error updating PCSO document: " + ex.Message);
            }
        }

        private async Task UpdateInventoryInUse(string ItemThatinUse, bool inUseStatus)
        {
            try
            {
                // Get a reference to the Firestore document to be updated
                DocumentReference docRef = FirebaseFirestore.DefaultInstance
                    .Collection(GameManager.instance.UserCollection)
                    .Document(GameManager.instance.UserID)
                    .Collection("OtherWorldInventory")
                    .Document(ItemThatinUse);

                // Fetch the document snapshot
                DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

                // Check if the document exists
                if (snapshot.Exists)
                {
                    // Deserialize the PCSO data from the Firestore document
                    string pcsoJson = snapshot.GetValue<string>("Items");

                    if (!string.IsNullOrEmpty(pcsoJson))
                    {
                        // Deserialize the JSON data into a PCSO object
                        //PCSO loadedPCSO = JsonUtility.FromJson<PCSO>(pcsoJson);

                        OtherWorldItemSO loadedPCSO = ScriptableObject.CreateInstance<OtherWorldItemSO>();

                        // Deserialize the JSON data into the PCSO object
                        JsonUtility.FromJsonOverwrite(pcsoJson, loadedPCSO);

                        // Update the inUse status
                        loadedPCSO.inUse = inUseStatus;

                        // Convert the updated PCSO object back to JSON
                        string updatedPCSOJson = JsonUtility.ToJson(loadedPCSO);

                        // Create a dictionary to update the inUse status
                        Dictionary<string, object> updateData = new Dictionary<string, object>
                {
                    { "Items", updatedPCSOJson }
                };

                        // Update the Firestore document with the new inUse status
                        await docRef.UpdateAsync(updateData);

                        Debug.Log("PCSO inUse status updated successfully.");


                    }
                    else
                    {
                        Debug.LogWarning("PCSO JSON data is empty or invalid.");
                    }
                }
                else
                {
                    Debug.LogWarning("PCSO document does not exist.");
                }
            }
            catch (Exception ex)
            {
                Debug.LogError("Error updating PCSO inUse status: " + ex.Message);
            }
        }



    }

   
}
