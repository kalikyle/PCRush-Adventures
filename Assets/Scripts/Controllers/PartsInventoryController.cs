using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;


using UnityEngine.SceneManagement;
using PartsInventory.UI;
using UnityEngine.UI;
using Inventory.Model;
using static Inventory.Model.PartsInventorySO;
using PartsInventory.Model;
using PC.UI;
using PC;
using PC.Model;
using Firebase.Firestore;
using System.Linq;

namespace PartsInventory
{
    public class PartsInventoryController : MonoBehaviour
    {
        [SerializeField]
        private UIPartsInventoryPage inventoryUI;

        [SerializeField]
        public PartsInventorySO inventoryData;

        [SerializeField]
        private PCInventSO PCData;


        // public GlossaryScript GS;



        [SerializeField]
        private Button CaseButton;
        [SerializeField]
        private Button MBButton;
        [SerializeField]
        private Button CPUButton;
        [SerializeField]
        private Button CPUFButton;
        [SerializeField]
        private Button RAMButton;
        [SerializeField]
        private Button GPUButton;
        [SerializeField]
        private Button STRG1Button;
        [SerializeField]
        private Button PSUButton;
        [SerializeField]
        private Button InventoryButton;

        [SerializeField]
        public Button XButton;

        [SerializeField]
        public Button MBXButton;
        [SerializeField]
        public Button CPUXButton;
        [SerializeField]
        public Button CPUFXButton;
        [SerializeField]
        public Button RAMXButton;
        [SerializeField]
        public Button GPUXButton;
        [SerializeField]
        public Button STRG1XButton;
        [SerializeField]
        public Button PSUXButton;

        [SerializeField]
        public Button useButton;
        [SerializeField]
        public Button sellButton;
        [SerializeField]
        public Button DoneButton;

        [SerializeField]
        public Button DisassmebleButton;

        [SerializeField]
        public Image DialogBox;
        [SerializeField]
        public TMP_Text DialogText;
        [SerializeField]
        public Button DialogButton;
        [SerializeField]
        public Button CanYesButton;
        [SerializeField]
        public Button CanNoButton;

        [SerializeField]
        public Button DisYesButton;
        [SerializeField]
        public Button DisNoButton;

        //[SerializeField]
        //public Button backButton;
        //[SerializeField]
        //public Button shopButton;

        [SerializeField]
        public Button CancelButton;


        [SerializeField]
        public Button PartsButton;
        [SerializeField]
        public Button ComputerButton;

        [SerializeField]
        public GameObject ComputerPanel;
        [SerializeField]
        public GameObject PartsPanel;

        //[SerializeField]
        //public Button TestPCButton;


        //[SerializeField]
        //public Button infoButton;



        [SerializeField]
        public TMP_Text DisplayText;

        [SerializeField]
        public GameObject ModifyingPC;

        [SerializeField]
        public GameObject newpc;

        [SerializeField]
        public GameObject pcName;

        [SerializeField]
        public GameObject Inuse;

        //[SerializeField]
        //public TMP_Text NameText;

        [SerializeField]
        private Image MBImage;
        [SerializeField]
        private Image CaseImage;
        [SerializeField]
        private Image CPUImage;
        [SerializeField]
        private Image CPUFImage;
        [SerializeField]
        private Image RAMImage;
        [SerializeField]
        private Image GPUImage;
        [SerializeField]
        private Image STRG1Image;
        [SerializeField]
        private Image PSUImage;

        //[SerializeField]
        //private Canvas Buildscene;
        //[SerializeField]
        //private Canvas ShopScene;

        //[SerializeField]
        //private Canvas HomeScene;

        //[SerializeField]
        //private Canvas TopUI;

        //[SerializeField]
        //public Canvas RenamePC;

        //[SerializeField]
        //public Canvas ApplyThermal;

        //[SerializeField]
        //public Canvas GameGlossary;

        [SerializeField]
        public TMP_InputField RenameTxt;
        [SerializeField]
        public TMP_Text PCNameTxt;


        [SerializeField]
        public GameObject[] objectsToCheck;

        public List<InventoryItem> initialItems = new List<InventoryItem>();
        public event Action<int> OnDescriptionRequested;

        public UIPartsInventoryItem items;

        //[SerializeField]
        //public GameObject SuccesfullyCreated;

        //[SerializeField]
        //public Image SuccesfullPCImage;

        //[SerializeField]
        //public TMP_Text SuccesfullPCName;


        [SerializeField]
        public AudioSource useSound;

        public GameObject _2exp;


        public GameObject Parts;
        public GameObject Faqs;
        public GameObject Trivia;
        public GameObject About;



        public TMP_Text Perks;
        public Dictionary<string, int> PerksDictionary = new Dictionary<string, int>();

        private bool AreAllObjectsActiveAndEnabled()
        {
            foreach (GameObject obj in objectsToCheck)
            {
                if (obj == null || !obj.activeInHierarchy || !obj.activeSelf)
                {
                    return false;
                }
            }
            return true;
        }
        private void CheckAndUpdateDoneButton()
        {
            if (DoneButton != null)
            {
                DoneButton.interactable = AreAllObjectsActiveAndEnabled();
            }
        }
        private void OnGameObjectStateChanged()
        {
            // Call this method to check and update the Done button
            CheckAndUpdateDoneButton();
        }
        public async void LoadPartsItems()
        {
            if (GameManager.instance.UserID != "")
            {
                try
                {
                    DocumentReference docRef = FirebaseFirestore.DefaultInstance.Collection(GameManager.instance.UserCollection).Document(GameManager.instance.UserID);
                    CollectionReference subDocRef = docRef.Collection("PartsInventory");
                    DocumentReference decorDocRef = subDocRef.Document("PartsInvent");

                    DocumentSnapshot snapshot = await decorDocRef.GetSnapshotAsync();

                    if (snapshot.Exists)
                    {
                        string jsonData = snapshot.GetValue<string>("Parts");
                        PartsItemList loadedData = JsonUtility.FromJson<PartsItemList>(jsonData);

                        if (loadedData != null)
                        {
                            initialItems.Clear();
                            GameManager.instance.itemsToTransfer.Clear();
                            inventoryData.Initialize();
                            inventoryData.OnInventoryUpdated += UpdateInventoryUI;

                            foreach (var item in loadedData.Items)
                            {
                                GameManager.instance.itemsToTransfer.Add(item);
                                if (!item.isEmpty)
                                {
                                    inventoryData.AddItem(item);
                                }
                            }

                            Debug.Log("Parts items loaded from Firestore.");
                        }
                    }
                    else
                    {
                        Debug.Log("No initial parts items found in Firestore for player.");
                    }
                }
                catch (System.Exception ex)
                {
                    Debug.LogError("Failed to load initial parts items from Firestore: " + ex.Message);
                }
            }
        }

        IEnumerator DelayedPartsLoad()
        {
            // Wait for 1 second
            yield return new WaitForSeconds(2f);

            // Now load initial items
            LoadPartsItems();
        }
        private void Start()
        {
            //LoadPCSO();
            
            // LoadInitialItems();
            GameManager.instance.OnItemsToTransferUpdated += UpdateInventory;
            //PrepareInventoryData();
            //ToggleALLButton();
            StartCoroutine(DelayedPartsLoad());
            //inventoryData.Initialize();
            //PrepareUI();
            CheckAndUpdateDoneButton();

            onStartClickables();

            CaseImage.gameObject.SetActive(false);
            MBImage.gameObject.SetActive(false);
            CPUImage.gameObject.SetActive(false);
            CPUFImage.gameObject.SetActive(false);
            RAMImage.gameObject.SetActive(false);
            GPUImage.gameObject.SetActive(false);
            STRG1Image.gameObject.SetActive(false);
            PSUImage.gameObject.SetActive(false);

            CaseImage.sprite = null;
            MBImage.sprite = null;

            MBButton.interactable = false;
            CPUButton.interactable = false;
            CPUFButton.interactable = false;
            RAMButton.interactable = false;
            GPUButton.interactable = false;
            STRG1Button.interactable = false;
            PSUButton.interactable = false;
            //backButton.onClick.AddListener(LoadMyHomeScene);
            //shopButton.onClick.AddListener(LoadMyShopScene);

            //DoneButton.onClick.AddListener(OnDoneButtonClick);

            //CancelButton.onClick.AddListener(() =>
            //{
            //    HandleCancel();

            //});

            //DisassmebleButton.onClick.AddListener(() =>
            //{
            //    DisassemblePC();

            //});




            //TestPCButton.onClick.AddListener(TestnewlyCreatePC);

            // infoButton.onClick.AddListener(() => {

            //     GlossaryInfo(infocategory);
            // }
            //);
        }
        //public void GlossaryInfo(string Category)
        //{

        //    //Debug.LogError(infocategory);
        //    if (Category == "Case")
        //    {
        //        //Debug.LogError("didntexe");
        //        GS.CaseGlossary();

        //    }
        //    else if (Category == "Motherboard")
        //    {
        //        GS.MotherboardGlossary();
        //    }
        //    else if (Category == "CPU")
        //    {
        //        GS.CPUGlossary();
        //    }
        //    else if (Category == "CPU Fan")
        //    {
        //        GS.CPUFGlossary();
        //    }
        //    else if (Category == "RAM")
        //    {
        //        GS.RAMGlossary();
        //    }
        //    else if (Category == "Video Card")
        //    {
        //        GS.GPUGlossary();
        //    }
        //    else if (Category == "Storage")
        //    {
        //        GS.StorageGlossary();
        //    }
        //    else if (Category == "PSU")
        //    {
        //        GS.PSUGlossary();
        //    }
        //    //inventoryUI.Hide();
        //    //GameGlossary.gameObject.SetActive(true);
        //    LTA.ShowGlossary();
        //    Parts.gameObject.SetActive(true);
        //    Faqs.gameObject.SetActive(false);
        //    Trivia.gameObject.SetActive(false);
        //    About.gameObject.SetActive(false);
        //}

        public void HandleCancel()
        {
            DialogBox.gameObject.SetActive(true);
            CanYesButton.gameObject.SetActive(true);
            CanNoButton.gameObject.SetActive(true);
            DisYesButton.gameObject.SetActive(false);
            DisNoButton.gameObject.SetActive(false);
            DialogButton.gameObject.SetActive(false);

            if (GameManager.instance.BeenModified)
            {
                DialogText.text = "You are currently Modifying a PC... \n Do you Want to Cancel your Build? \n If yes, Previous Parts will return to the PC...";
            }
            else
            {
                DialogText.text = "Do you Want to Cancel your Build?, \n All Items will be Return In your Inventory...";
            }


            CanYesButton.onClick.AddListener(() =>
            {
                YesCancelIt();
            });

            CanNoButton.onClick.AddListener(() =>
            {
                CloseDialog();
            });




        }

        private void YesCancelIt()
        {
            if (GameManager.instance.BeenModified)
            {

                if (recentlyBackedItems.Count > 0)
                {
                    foreach (var kvp in recentlyBackedItems)
                    {
                        lastUsedItems[kvp.Key] = kvp.Value;
                        inventoryData.RemoveItem(kvp.Value.item.name, 1);
                        UpdatePerksDictionary(kvp.Value, true);
                        //totalUsedItemsPrice += kvp.Value.item.Price;

                    }
                    recentlyBackedItems.Clear();
                }

                OnDoneButtonClick();

                ModifyingPC.gameObject.SetActive(false);
                pcName.gameObject.SetActive(false);
                Inuse.gameObject.SetActive(false);


                GameManager.instance.BeenModified = false;

                lastUsedItems.Clear();

            }
            else
            {


                newpc.gameObject.SetActive(false);
                GameManager.instance.BeenModified = false;
                BackAllCurrentlyItem();
                lastUsedItems.Clear();
            }


            CloseDialog();
            inventoryData.PartsSaveItems();
            DisassmebleButton.gameObject.SetActive(false);
            PerksDictionary.Clear();
            Perks.text = "";

        }

        //using an event
        public void UpdateInventory(InventoryItem updatedItems)
        {
            //inventoryData.inventoryItems.Clear();
            //initialItems.Clear();
            //initialItems.Add(updatedItems);
            //inventoryData.Initialize();
            inventoryData.OnInventoryUpdated += UpdateInventoryUI;
            inventoryData.AddItem(updatedItems);
            inventoryData.PartsSaveItems();
            //initialItems.Clear();
        }

        public List<InventoryItem> InventoryfilteredItems;
        public Dictionary<int, int> tempToOriginalIndexMapping = new Dictionary<int, int>();
       
        public void BackAllCurrentlyItem()
        {
            string[] categoriesToHandle = { "Case", "Motherboard", "CPU", "CPU Fan", "RAM", "Video Card", "Storage", "PSU" };
            foreach (string categories in categoriesToHandle)
            {
                if (lastUsedItems.ContainsKey(categories))
                {
                    HandleBackItem(categories);
                }
            }
            Perks.text = "";
            lastUsedItems.Clear();
            usedItems.Clear();
            CancelButton.interactable = false;
            Debug.LogError("Triggered");
        }

        private void PrepareInventoryData()
        {
            initialItems.AddRange(GameManager.instance.itemsToTransfer);
            inventoryData.Initialize();
            inventoryData.OnInventoryUpdated += UpdateInventoryUI;
            foreach (InventoryItem item in initialItems)
            {
                if (item.isEmpty) { continue; }
                inventoryData.AddItem(item);
            }
        }

        private void UpdateInventoryUI(Dictionary<int, InventoryItem> inventoryState)
        {
            //inventoryUI.ResetAllItems();
            foreach (var item in inventoryState)
            {
                inventoryUI.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.quantity);
            }
        }
        private int GetUsedSlotsCount()//this will only used the slots with items
        {
            int usedSlots = 0;
            foreach (var item in inventoryData.inventoryItems)
            {
                if (!item.isEmpty)
                {
                    usedSlots++;
                }
            }
            return usedSlots;
        }
        public void onStartClickables()
        {
            //inventoryUI.InitializeInventoryUI(GetUsedSlotsCount());
            //inventoryUI.InitializeInventoryUI(initialItems.Count);//inventoryData.Size
            inventoryUI.OnDescriptionRequested += HandleDescriptionRequest;
            inventoryUI.OnSwapItems += HandleSwapItems;
            //inventoryUI.OnStartDragging += HandleDragging;
            inventoryUI.OnItemActionRequested += HandleItemActionRequest;
            OnDescriptionRequested += HandleDescriptionRequests;//create new event handler

            CaseButton.onClick.AddListener(() => { OpenFiltered("Case"); });
            MBButton.onClick.AddListener(() => OpenFiltered("Motherboard"));
            CPUButton.onClick.AddListener(() => OpenFiltered("CPU"));
            CPUFButton.onClick.AddListener(() => OpenFiltered("CPU Fan"));
            RAMButton.onClick.AddListener(() => OpenFiltered("RAM"));
            GPUButton.onClick.AddListener(() => OpenFiltered("Video Card"));
            STRG1Button.onClick.AddListener(() => OpenFiltered("Storage"));
            PSUButton.onClick.AddListener(() => OpenFiltered("PSU"));
            InventoryButton.onClick.AddListener(() => ToggleALLButton());
            XButton.onClick.AddListener(() => OnInventExit());
            //XButton.gameObject.SetActive(false);

            useButton.onClick.AddListener(HandleUseButton);
            //sellButton.onClick.AddListener(HandleSellButton);

            MBXButton.onClick.AddListener(() => HandleBackItem("Motherboard"));
            CPUXButton.onClick.AddListener(() => HandleBackItem("CPU"));
            CPUFXButton.onClick.AddListener(() => HandleBackItem("CPU Fan"));
            RAMXButton.onClick.AddListener(() => HandleBackItem("RAM"));
            GPUXButton.onClick.AddListener(() => HandleBackItem("Video Card"));
            STRG1XButton.onClick.AddListener(() => HandleBackItem("Storage"));
            PSUXButton.onClick.AddListener(() => HandleBackItem("PSU"));

            //DialogButton.onClick.AddListener(CloseDialog);
        }
        public void OnInventExit()
        {
            PartsButton.gameObject.SetActive(true);
            ComputerButton.gameObject.SetActive(true);
            DisplayText.text = "HARDWARES".ToUpper();

            if (ComputerPanel.activeSelf)
            {
                ComputerPanel.SetActive(false);
                PartsPanel.SetActive(true);
            }

           

        }

        private string currentCategory = "";
        public List<InventoryItem> itemsToShow;
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
                inventoryUI.AddShopItem(item.item.ItemImage, item.quantity);
                originalIndex++;
                tempIndex++;
            }

        }

        public bool ToogleFiltered = true;
        public Dictionary<string, InventoryItem> lastUsedItems = new Dictionary<string, InventoryItem>();


        private void HandleSellButton()
        {

            int tempIndex = GameManager.instance.Partstempindex;

            if (ToogleFiltered)
            {

                if (tempToOriginalIndexMapping.TryGetValue(tempIndex, out int originalIndex))
                {

                    // Assuming InventoryfilteredItems contains filtered items from inventoryData
                    InventoryItem inventoryItem = InventoryfilteredItems[originalIndex];
                    Debug.Log("Index: " + originalIndex);
                    // Find the index of the item in the main inventory data
                    int indexInMainInventory = inventoryData.inventoryItems.IndexOf(inventoryItem);

                    Debug.Log("Index: " + indexInMainInventory);
                    if (indexInMainInventory != -1)
                    {
                        if (inventoryItem.item != null)
                        {
                            //GameManager.instance.PCMoney += inventoryItem.item.Price;
                        }

                        if (inventoryItem.quantity > 1)
                        {

                            // If the quantity is more than 1, decrease it by 1 in the main inventory
                            inventoryData.RemoveItem(indexInMainInventory, 1);
                            //inventoryUI.ResetSelection();
                            inventoryUI.Hide();

                            OpenFiltered(inventoryItem.item.Category);
                            inventoryUI.ResetSelection();
                            inventoryUI.SelectItemAtIndex(tempIndex);
                            HandleDescriptionRequests(tempIndex);
                            GameManager.instance.Partstempindex = tempIndex;

                            //HandleItemSelection(tempIndex);




                        }
                        else if (inventoryItem.quantity == 1)
                        {
                            // If the quantity is 1, remove the item from the main inventory
                            inventoryData.RemoveItem(indexInMainInventory, 1);

                            // Perform additional actions specific to the filtered inventory display, if needed
                            // For example, resetting selection or hiding the filtered inventory UI
                            inventoryUI.ResetSelection();
                            inventoryUI.Hide();
                            OpenFiltered(inventoryItem.item.Category);

                        }


                    }
                    else
                    {
                        Debug.LogError("Item not found in the main inventory.");
                        // Handle the scenario where the item is not found in the main inventory
                    }
                }
               // GameManager.instance.UpdatePCMoneyText();
               // GameManager.instance.SavePCMoney();//need fix


            }
            else
            {

                InventoryItem inventoryItem = inventoryData.GetItemAt(tempIndex);

                if (inventoryItem.item != null)
                {
                    //GameManager.Instance.PCMoney += inventoryItem.item.Price;
                }

                if (inventoryItem.quantity > 1)
                {
                    // If the quantity is more than 1, decrease it by 1
                    inventoryData.RemoveItem(tempIndex, 1);
                }
                else if (inventoryItem.quantity == 1)
                {


                    inventoryData.RemoveItem(tempIndex, 1);
                    inventoryUI.ResetSelection();
                    inventoryUI.Hide();
                    ToggleALLButton();// Remove the last item
                    // Additional logic or handling specific to quantity 1, if needed
                    // For instance, you might want to display a message or perform other actions
                    // You could also set the item's quantity back to its default or handle it differently
                }
                //GameManager.Instance.UpdatePCMoneyText();
                //GameManager.Instance.SavePCMoney();

            }


        }

        private void HandleUseButton()
        {

            int tempIndex = GameManager.instance.Partstempindex;
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
            inventoryData.PartsSaveItems();
            //inventoryData.SaveItems();



        }
        private void CloseDialog()
        {
            DialogBox.gameObject.SetActive(false);
        }

        private void Unused(InventoryItem inventoryItem, string category)
        {

            if (CaseImage.sprite == null)
            {
                DialogBox.gameObject.SetActive(true);
                DialogText.text = "Can't Use this " + category + " without the Case";
                CanYesButton.gameObject.SetActive(false);
                CanNoButton.gameObject.SetActive(false);
                DisYesButton.gameObject.SetActive(false);
                DisNoButton.gameObject.SetActive(false);
                DialogButton.gameObject.SetActive(true);
                try
                {

                    HandleBackItem(category);

                    if (category == "CPU")
                    {
                        //ApplyThermal.gameObject.SetActive(false);
                    }



                }
                catch (Exception) { }
            }
            else if (MBImage.sprite == null)
            {
                DialogBox.gameObject.SetActive(true);
                DialogText.text = "Can't Use this " + category + " without the Motherboard";
                CanYesButton.gameObject.SetActive(false);
                CanNoButton.gameObject.SetActive(false);
                DisYesButton.gameObject.SetActive(false);
                DisNoButton.gameObject.SetActive(false);
                DialogButton.gameObject.SetActive(true);
                try
                {
                    HandleBackItem(category);

                    if (category == "CPU")
                    {
                        //ApplyThermal.gameObject.SetActive(false);
                    }

                }
                catch (Exception) { }
            }


            Debug.Log(inventoryItem.quantity);
        }

        public void HandleBackItem(string category)
        {


            if (lastUsedItems.ContainsKey(category))
            {
                recentlyBackedItems[category] = lastUsedItems[category];
            }
            else if (recentlyBackedItems.ContainsKey(category))
            {
                recentlyBackedItems.Remove(category);
            }

            InventoryItem previousUsedItem = lastUsedItems[category];
            inventoryData.AddItem(previousUsedItem.ChangeQuantity(previousUsedItem.quantity - lastUsedItems[category].quantity + 1));
            UpdatePerksDictionary(previousUsedItem, false);
            switch (category)
            {
                case "Case":
                    CaseImage.gameObject.SetActive(false);
                    CaseImage.sprite = null;
                    break;
                case "Motherboard":
                    MBImage.gameObject.SetActive(false);
                    MBImage.sprite = null;

                    CPUButton.interactable = false;
                    CPUFButton.interactable = false;
                    RAMButton.interactable = false;
                    GPUButton.interactable = false;
                    STRG1Button.interactable = false;
                    PSUButton.interactable = false;
                    try
                    {
                        string[] categoriesToHandle = { "CPU", "CPU Fan", "RAM", "Video Card", "Storage", "PSU" };
                        foreach (string categories in categoriesToHandle)
                        {
                            if (lastUsedItems.ContainsKey(categories))
                            {
                                HandleBackItem(categories);
                            }
                        }
                    }
                    catch (Exception) { }
                    //GameManager.instance.PSUImagesNeeds.Remove(category);
                    break;
                case "CPU":
                    CPUImage.gameObject.SetActive(false);
                    CPUImage.sprite = null;

                    //GameManager.instance.PSUImagesNeeds.Remove(category);
                    break;
                case "CPU Fan":
                    CPUFImage.gameObject.SetActive(false);
                    CPUFImage.sprite = null;


                    break;
                case "RAM":
                    RAMImage.gameObject.SetActive(false);
                    RAMImage.sprite = null;


                    break;
                case "Video Card":
                    GPUImage.gameObject.SetActive(false);
                    GPUImage.sprite = null;

                    //GameManager.Instance.PSUImagesNeeds.Remove(category);
                    break;
                case "Storage":
                    STRG1Image.gameObject.SetActive(false);
                    STRG1Image.sprite = null;

                    //GameManager.Instance.PSUImagesNeeds.Remove(category);
                    break;
                case "PSU":
                    PSUImage.gameObject.SetActive(false);
                    PSUImage.sprite = null;
                    // GameManager.Instance.PSUImagesNeeds.Remove(category);
                    break;

            }
            if (!(CaseImage.isActiveAndEnabled && MBImage.isActiveAndEnabled && CPUImage.isActiveAndEnabled && CPUFImage.isActiveAndEnabled && RAMImage.isActiveAndEnabled && GPUImage.isActiveAndEnabled && STRG1Image.isActiveAndEnabled))
            {
                PSUButton.interactable = false;
                if (lastUsedItems.ContainsKey("PSU"))
                {
                    BackItem(lastUsedItems["PSU"], "PSU");
                    PSUImage.gameObject.SetActive(false);
                    PSUImage.sprite = null;
                    recentlyBackedItems["PSU"] = lastUsedItems["PSU"];
                    lastUsedItems.Remove("PSU");
                }
            }

            OnGameObjectStateChanged();
            usedItems.Remove(previousUsedItem);
            lastUsedItems.Remove(category);
            GameManager.instance.itemsToTransfer.Remove(previousUsedItem);
            inventoryData.PartsSaveItems();

        }





        public Dictionary<string, InventoryItem> recentlyBackedItems = new Dictionary<string, InventoryItem>();
        public List<InventoryItem> usedItems = new List<InventoryItem>();
        private bool HasItemBeenUsed(InventoryItem inventoryItem)
        {
            // Check if the item has been used in the same category
            return usedItems.Exists(item => item.item.Category == inventoryItem.item.Category && item.item.Name == inventoryItem.item.Name);
        }
        public void HandleItemActionRequest(int itemIndex)//for all
        {

            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            string category = inventoryItem.item.Category;
            if (HasItemBeenUsed(inventoryItem))
            {
                inventoryUI.Hide();
                DialogBox.gameObject.SetActive(true);
                DialogText.text = "Item already In use...";
                CanYesButton.gameObject.SetActive(false);
                CanNoButton.gameObject.SetActive(false);
                DisYesButton.gameObject.SetActive(false);
                DisNoButton.gameObject.SetActive(false);
                DialogButton.gameObject.SetActive(true);
            }
            else
            {
                //useSound.Play();
                MBImage.sprite = inventoryItem.item.ItemImage; // this is for the whole inventory
                UseItems(inventoryItem, category);
                inventoryData.RemoveItem(itemIndex, 1);
                BackItem(inventoryItem, category);
                
                //totalUsedItemsPrice += inventoryItem.item.Price;
                // DisplayPrices(totalUsedItemsPrice);



                if (category != "Case")
                {

                    Unused(inventoryItem, category);
                }

                if (CaseImage.sprite == null && MBImage.sprite == null)
                {
                    usedItems.Remove(inventoryItem);
                }
                else if (CaseImage.sprite == null || MBImage.sprite == null)
                {
                    usedItems.Remove(inventoryItem);
                }
                else
                {
                    usedItems.Add(inventoryItem);
                }

                if (category == "CPU")
                {
                   // ApplyThermal.gameObject.SetActive(true);
                   // AT.SetCPUImage(inventoryItem.item.ItemImage);
                }

                if (category == "PSU")
                {
                    if (!(CaseImage.isActiveAndEnabled && MBImage.isActiveAndEnabled && CPUImage.isActiveAndEnabled && CPUFImage.isActiveAndEnabled && RAMImage.isActiveAndEnabled && GPUButton.isActiveAndEnabled && STRG1Image.isActiveAndEnabled))
                    {
                        DialogBox.gameObject.SetActive(true);
                        DialogText.text = "Can't Use this " + category + " without the needed Parts";
                        CanYesButton.gameObject.SetActive(false);
                        CanNoButton.gameObject.SetActive(false);
                        DisYesButton.gameObject.SetActive(false);
                        DisNoButton.gameObject.SetActive(false);
                        DialogButton.gameObject.SetActive(true);
                        try
                        {
                            HandleBackItem(category);

                        }
                        catch (Exception) { }
                    }
                    else
                    {
                       // SceneManager.LoadScene("PSUWiring", LoadSceneMode.Additive);
                    }

                }

               
            }



        }
        //private void DisplayPrices(double totalPrice)
        //{
        //    //PriceText.text = "$" + totalPrice.ToString("F2");
        //}

        public double totalUsedItemsPrice = 0.0;
        public void HandleItemRightActionRequest(int tempIndex)//for filtered
        {

            if (tempToOriginalIndexMapping.TryGetValue(tempIndex, out int originalIndex))
            {

                InventoryItem inventoryItem = InventoryfilteredItems[originalIndex];
                string category = inventoryItem.item.Category;
                if (HasItemBeenUsed(inventoryItem))
                {
                    inventoryUI.Hide();
                    DialogBox.gameObject.SetActive(true);
                    DialogText.text = "Item already In use...";
                    CanYesButton.gameObject.SetActive(false);
                    CanNoButton.gameObject.SetActive(false);
                    DisYesButton.gameObject.SetActive(false);
                    DisNoButton.gameObject.SetActive(false);
                    DialogButton.gameObject.SetActive(true);
                }
                else
                {
                   // useSound.Play();
                    usedItems.Remove(inventoryItem);
                    UseItems(inventoryItem, category);
                    int index = inventoryData.inventoryItems.IndexOf(inventoryItem);

                    inventoryData.RemoveItem(index, 1);
                    BackItem(inventoryItem, category);
                   // UpdatePerksDictionary(inventoryItem, true);
                    //  totalUsedItemsPrice += inventoryItem.item.Price;
                    //DisplayPrices(totalUsedItemsPrice);

                    if (category != "Case")
                    {
                        Unused(inventoryItem, category);
                    }

                    if (CaseImage.sprite == null && MBImage.sprite == null)
                    {
                        usedItems.Remove(inventoryItem);
                    }
                    else if (CaseImage.sprite == null || MBImage.sprite == null)
                    {
                        usedItems.Remove(inventoryItem);
                    }
                    else
                    {
                        usedItems.Add(inventoryItem);
                    }

                    if (category == "CPU")
                    {
                       // ApplyThermal.gameObject.SetActive(true);
                       // AT.SetCPUImage(inventoryItem.item.ItemImage);
                    }

                    if (category == "PSU")
                    {
                        if (!(CaseImage.isActiveAndEnabled && MBImage.isActiveAndEnabled && CPUImage.isActiveAndEnabled && CPUFImage.isActiveAndEnabled && RAMImage.isActiveAndEnabled && GPUButton.isActiveAndEnabled && STRG1Image.isActiveAndEnabled))
                        {
                            DialogBox.gameObject.SetActive(true);
                            DialogText.text = "Can't Use this " + category + " without the needed Parts";
                            CanYesButton.gameObject.SetActive(false);
                            CanNoButton.gameObject.SetActive(false);
                            DisYesButton.gameObject.SetActive(false);
                            DisNoButton.gameObject.SetActive(false);
                            DialogButton.gameObject.SetActive(true);
                            try
                            {
                                HandleBackItem(category);

                            }
                            catch (Exception) { }
                        }
                        else
                        {
                            //SceneManager.LoadScene("PSUWiring", LoadSceneMode.Additive);
                        }


                    }
                    
                }

            }
        }

        private void UpdatePerksDictionary(InventoryItem item, bool add)
        {
            // Helper function to add or remove perk
            void AddOrRemovePerk(string perkName, double value)
            {
                if (value == 0) return;

                if (PerksDictionary.ContainsKey(perkName))
                {
                    PerksDictionary[perkName] += (int)value * (add ? 1 : -1);
                    if (PerksDictionary[perkName] == 0)
                    {
                        PerksDictionary.Remove(perkName);
                    }
                }
                else if (add)
                {
                    PerksDictionary[perkName] = (int)value;
                }
            }

            // Add or remove perks
            AddOrRemovePerk("Attack Damage", item.item.AttackDamage);
            AddOrRemovePerk("Health", item.item.Health);
            AddOrRemovePerk("Mana", item.item.Mana);
            AddOrRemovePerk("Health Regen", item.item.HealthRegen);
            AddOrRemovePerk("Walk Speed", item.item.WalkSpeed);
            AddOrRemovePerk("Armor", item.item.Armor);
            AddOrRemovePerk("Attack Speed", item.item.AttackSpeed);
            AddOrRemovePerk("Critical Hit", item.item.CriticalHit);

            // Update the Perks text
            Perks.text = string.Join("\n", PerksDictionary.Select(kv => $"{kv.Key} +{kv.Value}"));
        }

        private void BackItem(InventoryItem inventoryItem, string category)
        {
            if (!string.IsNullOrEmpty(category))
            {
                // Check if a previous item was used in the same category
                if (lastUsedItems.ContainsKey(category))
                {
                    InventoryItem previousUsedItem = lastUsedItems[category];
                    if (!inventoryItem.Equals(previousUsedItem))
                    {
                        // The item has changed, so remove it from the lastUsedItems and adjust the total price
                        //totalUsedItemsPrice -= previousUsedItem.item.Price;
                        inventoryData.AddItem(previousUsedItem.ChangeQuantity(previousUsedItem.quantity - lastUsedItems[category].quantity + 1));
                        Debug.Log(previousUsedItem.quantity);
                        usedItems.Remove(previousUsedItem);
                        GameManager.instance.itemsToTransfer.Remove(previousUsedItem);
                        lastUsedItems.Remove(category);

                    }
                    else
                    {
                        // The item hasn't changed, so add it back to the inventory
                        //totalUsedItemsPrice -= previousUsedItem.item.Price;
                        //totalUsedItemsPrice += inventoryItem.item.Price;
                        inventoryData.AddItem(previousUsedItem.ChangeQuantity(previousUsedItem.quantity - lastUsedItems[category].quantity + 1));
                        Debug.Log(previousUsedItem.quantity);
                        usedItems.Remove(previousUsedItem);
                        GameManager.instance.itemsToTransfer.Remove(previousUsedItem);
                    }
                }

            }
            // Update the last used item for the category
            //UpdatePerksDictionary(inventoryItem, false);
            lastUsedItems[category] = inventoryItem;
            //Debug.LogError("added to lastitem");
            //DisplayPrices(totalUsedItemsPrice);
        }

        //public ApplyThermalPaste AT;

        public void UseItems(InventoryItem inventoryItem, string category)
        {
            UpdatePerksDictionary(inventoryItem, true);
            switch (category)
            {
                case "Case":
                    CaseImage.gameObject.SetActive(true);
                    CaseImage.sprite = inventoryItem.item.ItemImage;
                    MBButton.interactable = true;
                    CancelButton.interactable = true;

                    if(GameManager.instance.BeenModified == false)
                    {
                        newpc.gameObject.SetActive(true);
                    }
                    else
                    {
                        newpc.gameObject.SetActive(false);
                    }

                    break;
                case "Motherboard":
                    MBImage.gameObject.SetActive(true);
                    MBImage.sprite = inventoryItem.item.ItemImage;
                    CPUButton.interactable = true;
                    CPUFButton.interactable = true;
                    RAMButton.interactable = true;
                    GPUButton.interactable = true;
                    STRG1Button.interactable = true;
                    //GameManager.Instance.PSUImagesNeeds[category] = inventoryItem;
                    break;
                case "CPU":
                    CPUImage.gameObject.SetActive(true);
                    CPUImage.sprite = inventoryItem.item.ItemImage;
                    //GameManager.Instance.PSUImagesNeeds[category] = inventoryItem;
                    //ApplyThermal.gameObject.SetActive(true);
                    //AT.SetCPUImage(inventoryItem.item.ItemImage);
                    break;
                case "CPU Fan":
                    CPUFImage.gameObject.SetActive(true);
                    CPUFImage.sprite = inventoryItem.item.ItemImage;
                    break;
                case "RAM":
                    RAMImage.gameObject.SetActive(true);
                    RAMImage.sprite = inventoryItem.item.ItemImage;
                    break;
                case "Video Card":
                    GPUImage.gameObject.SetActive(true);
                    GPUImage.sprite = inventoryItem.item.ItemImage;
                    //GameManager.Instance.PSUImagesNeeds[category] = inventoryItem;
                    break;
                case "Storage":
                    STRG1Image.gameObject.SetActive(true);
                    STRG1Image.sprite = inventoryItem.item.ItemImage;
                   // GameManager.Instance.PSUImagesNeeds[category] = inventoryItem;
                    break;

                case "PSU":
                    PSUImage.gameObject.SetActive(true);
                    PSUImage.sprite = inventoryItem.item.ItemImage;
                  //  GameManager.Instance.PSUImagesNeeds[category] = inventoryItem;
                    break;

            }
            if (CaseImage.isActiveAndEnabled && MBImage.isActiveAndEnabled && CPUImage.isActiveAndEnabled && CPUFImage.isActiveAndEnabled && RAMImage.isActiveAndEnabled && GPUImage.isActiveAndEnabled && STRG1Image.isActiveAndEnabled)
            {
                PSUButton.interactable = true;
            }
            OnGameObjectStateChanged();
            inventoryUI.Hide();
        }


        //private void HandleDragging(int itemIndex)
        //{
        //    InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
        //    if (inventoryItem.isEmpty) { return; }
        //    inventoryUI.CreateDraggedItem(inventoryItem.item.ItemImage, inventoryItem.quantity);


        //}


        private void HandleSwapItems(int ItemIndex1, int ItemIndex2)
        {
            inventoryData.SwapItems(ItemIndex1, ItemIndex2);
        }

        private void HandleDescriptionRequest(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.isEmpty)
            {

                inventoryUI.ResetSelection();
                return;

            }
            PartsSO item = inventoryItem.item;
            

            inventoryUI.UpdateDescription(itemIndex, item.ItemImage, item.Name, item.Category, item.rarity, ItemPerks(item));
        }

        

        public void HandleDescriptionRequests(int obj)
        {
            if (obj >= 0 && obj < InventoryfilteredItems.Count)
            {
                InventoryItem shopItem = InventoryfilteredItems[obj];
                if (!shopItem.isEmpty)
                {
                    PartsSO item = shopItem.item;
                    inventoryUI.UpdateDescription(obj, item.ItemImage, item.Name, item.Category, item.rarity, ItemPerks(item));//update description
                }
            }
        }

        private string ItemPerks(PartsSO item)
        {
            string perks = "";

            // Check each perk property and accumulate non-zero values
            if (item.AttackDamage != 0)
            {
                perks += "Attack Damage +" + item.AttackDamage + "\n";
            }
            if (item.Health != 0)
            {
                perks += "Health +" + item.Health + "\n";
            }
            if (item.Mana != 0)
            {
                perks += "Mana +" + item.Mana + "\n";
            }
            if (item.HealthRegen != 0)
            {
                perks += "Health Regen  +" + item.HealthRegen + "\n";
            }
            if (item.WalkSpeed != 0)
            {
                perks += "Walk Speed +" + item.WalkSpeed + "\n";
            }
            if (item.Armor != 0)
            {
                perks += "Armor +" + item.Armor + "\n";
            }
            if (item.AttackSpeed != 0)
            {
                perks += "Attack Speed +" + item.AttackSpeed + "\n";
            }
            if (item.CriticalHit != 0)
            {
                perks += "Critical Hit +" + item.CriticalHit + "\n";
            }

            return perks;
        }

        public void HandleItemSelection(int tempIndex)
        {
            // Use the mapping to get the original index
            if (tempToOriginalIndexMapping.TryGetValue(tempIndex, out int originalIndex))
            {
                OnDescriptionRequested?.Invoke(originalIndex);
            }
        }
        //public Build build;
        public Canvas MainMenu;
       // public LeanTweenAnimate LTA;
        //float lastKeyPressTime = 0f;
       // float cooldownDuration = 1f;
        public void Update()
        {
            OnGameObjectStateChanged();
            ////inventoryData.SaveItems();
            if (CaseImage.isActiveAndEnabled && MBImage.isActiveAndEnabled && CPUImage.isActiveAndEnabled && CPUFImage.isActiveAndEnabled && RAMImage.isActiveAndEnabled && GPUImage.isActiveAndEnabled && STRG1Image.isActiveAndEnabled)
            {
                PSUButton.interactable = true;
            }

            //if ((PlayerPrefs.GetInt("TutorialDone") == 1))
            //{
            //    if (!RenamePC.isActiveAndEnabled /*|| !ApplyThermal.isActiveAndEnabled || !DialogBox.isActiveAndEnabled*/)
            //    {


            //        if (!MainMenu.isActiveAndEnabled)
            //        {
            //            if (Input.GetKeyDown(KeyCode.LeftArrow))
            //            {
            //                if (Time.time - lastKeyPressTime > cooldownDuration)
            //                {

            //                    LTA.hideBuild();

            //                    lastKeyPressTime = Time.time;

            //                }

            //            }
            //            if (Input.GetKeyDown(KeyCode.I))
            //            {

            //                ToggleALLButton();

            //            }

            //            if (Input.GetKeyDown(KeyCode.Escape))
            //            {

            //                inventoryUI.Hide();

            //            }

            //            if (Input.GetKeyDown(KeyCode.S))
            //            {

            //                LoadMyShopScene();
            //                LTA.ShowShop();

            //            }
            //        }

            //    }
            //}



            //if (GameManager.Instance.BeenModified)
            //{
            //    ModifyingPC.SetActive(true);
            //}
            //else
            //{
            //    ModifyingPC.SetActive(false);
            //}

        }
        

        public void ToggleALLButton()
        {
            //infoButton.gameObject.SetActive(false);
            PartsButton.gameObject.SetActive(true);
            ComputerButton.gameObject.SetActive(true);
            ToogleFiltered = false;
            Debug.Log("Toggle: false");
            inventoryUI.ResetSelection();
            inventoryUI.ClearItems();
            inventoryUI.InitializeInventoryUI(GetUsedSlotsCount());
            //inventoryData.PartsSaveItems();
            OpenInvBTN();


        }
        public string infocategory = null;
        public GameObject usebuttonpanel;
        public void OpenFiltered(string category)
        {
            //infoButton.gameObject.SetActive(true);
            
            Debug.Log("Toggle: true");
            
            PartsPanel.SetActive(true);
            ComputerPanel.SetActive(false);
            PartsButton.gameObject.SetActive(false);
            ComputerButton.gameObject.SetActive(false);
            usebuttonpanel.gameObject.SetActive(true);
            ShowCategory(category);
            infocategory = category;
            DisplayText.text = category;
            int i = 0;
            if (inventoryUI.isActiveAndEnabled == false)
            {
                inventoryUI.Show();

                foreach (var item in InventoryfilteredItems)
                {
                    inventoryUI.UpdateData(i, item.item.ItemImage, item.quantity);
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

                DisplayText.text = "Hardwares".ToUpper();
                foreach (var item in inventoryData.GetCurrentInventoryState())
                {
                    inventoryUI.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.quantity);
                }
          //  }
            //else
            //{

            //    inventoryUI.Hide();

            //}

        }

        //public void SeeUsedItemList()
        //{
        //    foreach (var kvp in lastUsedItems)
        //    {
        //        string category = kvp.Key;
        //        InventoryItem inventoryItem = kvp.Value;

        //        Debug.Log($"Category: {category}, Item: {inventoryItem.item.Name}, Quantity: {inventoryItem.quantity}, Price: {inventoryItem.item.Price}");
        //    }
        //}

        //public List<PCSO> CreatedComputers;

        //public void TestnewlyCreatePC()
        //{

        //    PCC.TestComputer(PCData.ComputerItems.Count - 1);
        //    LTA.hideBuild();
        //    LTA.HideSuccess();
        //}
        public PCSO ConvertLastUsedItemsToPCSOList()
        {

            PCSO pcso = ScriptableObject.CreateInstance<PCSO>();

            pcso.name = pcname;//PCNameTxt.text;
            pcso.PCName = pcname;//PCNameTxt.text;
            pcso.PCImage = lastUsedItems.ContainsKey("Case") ? lastUsedItems["Case"].item.ItemImage : null;
            //pcso.PCPrice = totalUsedItemsPrice;
            pcso.Case = lastUsedItems.ContainsKey("Case") ? lastUsedItems["Case"].item : null;
            pcso.Motherboard = lastUsedItems.ContainsKey("Motherboard") ? lastUsedItems["Motherboard"].item : null;
            pcso.CPU = lastUsedItems.ContainsKey("CPU") ? lastUsedItems["CPU"].item : null;
            pcso.CPUFan = lastUsedItems.ContainsKey("CPU Fan") ? lastUsedItems["CPU Fan"].item : null;
            pcso.RAM = lastUsedItems.ContainsKey("RAM") ? lastUsedItems["RAM"].item : null;
            pcso.GPU = lastUsedItems.ContainsKey("Video Card") ? lastUsedItems["Video Card"].item : null;
            pcso.STORAGE = lastUsedItems.ContainsKey("Storage") ? lastUsedItems["Storage"].item : null;
            pcso.PSU = lastUsedItems.ContainsKey("PSU") ? lastUsedItems["PSU"].item : null;
            pcso.inUse = false;


            pcso.AttackDamage = PerksDictionary.ContainsKey("Attack Damage") ? PerksDictionary["Attack Damage"] : 0;
            pcso.Health = PerksDictionary.ContainsKey("Health") ? PerksDictionary["Health"] : 0;
            pcso.Mana = PerksDictionary.ContainsKey("Mana") ? PerksDictionary["Mana"] : 0;
            pcso.HealthRegen = PerksDictionary.ContainsKey("Health Regen") ? PerksDictionary["Health Regen"] : 0;
            pcso.WalkSpeed = PerksDictionary.ContainsKey("Walk Speed") ? PerksDictionary["Walk Speed"] : 0;
            pcso.Armor = PerksDictionary.ContainsKey("Armor") ? PerksDictionary["Armor"] : 0;
            pcso.AttackSpeed = PerksDictionary.ContainsKey("Attack Speed") ? PerksDictionary["Attack Speed"] : 0;
            pcso.CriticalHit = PerksDictionary.ContainsKey("Critical Hit") ? PerksDictionary["Critical Hit"] : 0;

            return pcso;
        }
        //public void SavePCSOData(PCSO pcso)
        //{
        //    // Convert PCSO data to JSON (you can use other serialization methods)
        //    string jsonData = JsonUtility.ToJson(pcso);

        //    // Save the JSON data to PlayerPrefs with a unique key based on the index
        //    int index = GetUniqueIndex();
        //    PlayerPrefs.SetString($"PCs_{index}", jsonData);

        //    // Add the key to the list of keys for later retrieval
        //    string keys = PlayerPrefs.GetString("PCs_Keys", "");
        //    keys += (keys.Length > 0 ? "," : "") + $"PCs_{index}";
        //    PlayerPrefs.SetString("PCs_Keys", keys);

        //}
        //public List<int> UniqueIndex = new List<int>();
        //private int GetUniqueIndex()
        //{
        //    // Generate a unique index based on the current time
        //    int newIndex = (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
        //    UniqueIndex.Add(newIndex);
        //    return newIndex;
        //}

        //public void ChangePCName()
        //{
        //    if (RenameTxt.text == null || RenameTxt.text == "")
        //    {
        //        return;

        //    }
        //    else
        //    {
        //        NameText.text = RenameTxt.text;


        //    }

        //}

        //public AudioSource Success;

        [SerializeField]
        private PCPage PCpage;
        [SerializeField]
        private PCPlayerController PCC;

        public LeanTweenAnimate LTA;


        public GameObject RenamePanel;

        //public TMP_Text Modified;
        public string pcname;
        public async void RenamePC()
        {
            pcname = RenameTxt.text;
            if(pcname == "")
            {
                pcname = "PC1";
            }

            if (!GameManager.instance.BeenModified) 
            { //new pc 

                await PCC.ModifyPCName(GameManager.instance.pcsoDocumentIds[0], pcname);

            }
            else
            {
                await PCC.ModifyPCName(GameManager.instance.pcsothatisModified, pcname);
            }

            PCNameTxt.text = pcname;

    }

        public void DisassemblePC()
        {


            if(GameManager.instance.pcsothatisModified == GameManager.instance.pcsothatinUse)
            {
                DialogBox.gameObject.SetActive(true);
                CanYesButton.gameObject.SetActive(false);
                CanNoButton.gameObject.SetActive(false);
                DisYesButton.gameObject.SetActive(false);
                DisNoButton.gameObject.SetActive(false);
                DialogButton.gameObject.SetActive(true);


                DialogText.text = "You Can't Disassemble This PC, because this PC is currently IN-USE \n Change your PC in Desk First Before Disassembling this PC";
            }
            else
            {
                DialogBox.gameObject.SetActive(true);
                CanYesButton.gameObject.SetActive(false);
                CanNoButton.gameObject.SetActive(false);
                DisYesButton.gameObject.SetActive(true);
                DisNoButton.gameObject.SetActive(true);
                DialogButton.gameObject.SetActive(false);


                DialogText.text = "Are you Sure you want to Dissamble this PC? \n This PC will be remove in your computer Inventory but the parts will be back into your Parts Inventory";




                DisYesButton.onClick.AddListener(() =>
                {
                    // Create a copy of the keys to iterate over
                    var keys = new List<string>(lastUsedItems.Keys);

                    if (keys.Count > 0)
                    {
                        foreach (var key in keys)
                        {
                            // Get the value corresponding to the key
                            var value = lastUsedItems[key];

                            // Remove the item
                            BackItem(value, key);
                            inventoryData.RemoveItem(value.item.name, 1);
                        }
                    }

                    ModifyingPC.gameObject.SetActive(false);
                    pcName.gameObject.SetActive(false);
                    Inuse.gameObject.SetActive(false);
                    PCC.DeletePC(GameManager.instance.pcsothatisModified);
                    GameManager.instance.pcsoDocumentIds.Remove(GameManager.instance.pcsothatisModified);
                    //OnDoneButtonClick();
                    BackAllCurrentlyItem();

                    GameManager.instance.BeenModified = false;

                    lastUsedItems.Clear();
                    CloseDialog();
                    DisassmebleButton.gameObject.SetActive(false);
                    inventoryData.PartsSaveItems();
                    PCpage.ClearItems();
                    PerksDictionary.Clear();
                    Perks.text = "";


                });

                DisNoButton.onClick.AddListener(() =>
                {
                    CloseDialog();

                });
            }
            
        }
        public async void OnDoneButtonClick()
        {

            PCSO PC = ConvertLastUsedItemsToPCSOList();

            if (!GameManager.instance.BeenModified) // not modified pc 
            {
                Debug.LogError("New Computer");
                PCData.AddPCSOList(PC);
                PCData.ComputerSave(PC);
                CaseImage.gameObject.SetActive(false);
                MBImage.gameObject.SetActive(false);
                CPUImage.gameObject.SetActive(false);
                CPUFImage.gameObject.SetActive(false);
                RAMImage.gameObject.SetActive(false);
                GPUImage.gameObject.SetActive(false);
                STRG1Image.gameObject.SetActive(false);
                PSUImage.gameObject.SetActive(false);

                CaseImage.sprite = null;
                MBImage.sprite = null;

                MBButton.interactable = false;
                CPUButton.interactable = false;
                CPUFButton.interactable = false;
                RAMButton.interactable = false;
                GPUButton.interactable = false;
                STRG1Button.interactable = false;
                PSUButton.interactable = false;

                usedItems.Clear();
                lastUsedItems.Clear();
                recentlyBackedItems.Clear();
               

                OnGameObjectStateChanged();
                PCpage.AddAnotherPC();
                

                CancelButton.interactable = false;
                GameManager.instance.BeenModified = false;

                Debug.Log("PCSO items added to ComputerItems list.");

                //RenamePanel.gameObject.SetActive(true);
                LTA.InstallOS();
                RenameTxt.text = "PC1";
                
                
            }
            else // modified pc 
            {
                Debug.LogError("Modified");
                await PCC.ModifyPCSOs(GameManager.instance.pcsothatisModified,PC);
                CaseImage.gameObject.SetActive(false);
                MBImage.gameObject.SetActive(false);
                CPUImage.gameObject.SetActive(false);
                CPUFImage.gameObject.SetActive(false);
                RAMImage.gameObject.SetActive(false);
                GPUImage.gameObject.SetActive(false);
                STRG1Image.gameObject.SetActive(false);
                PSUImage.gameObject.SetActive(false);

                CaseImage.sprite = null;
                MBImage.sprite = null;

                MBButton.interactable = false;
                CPUButton.interactable = false;
                CPUFButton.interactable = false;
                RAMButton.interactable = false;
                GPUButton.interactable = false;
                STRG1Button.interactable = false;
                PSUButton.interactable = false;

                usedItems.Clear();
                lastUsedItems.Clear();
                recentlyBackedItems.Clear();


                OnGameObjectStateChanged();


                CancelButton.interactable = false;
                GameManager.instance.BeenModified = false;



            }
            ModifyingPC.gameObject.SetActive(false);
            pcName.gameObject.SetActive(false);
            Inuse.gameObject.SetActive(false);
            newpc.gameObject.SetActive(false);
            DisassmebleButton.gameObject.SetActive(false);
            pcname = "PC1";
            PerksDictionary.Clear();
            Perks.text = "";
        }

        //}
        //public void CheckifBuildingbeforeQuit()
        //{
        //    if (lastUsedItems.Count > 0)
        //    {
        //        if (GameManager.Instance.BeenModified)
        //        {
        //            if (recentlyBackedItems.Count > 0)
        //            {
        //                foreach (var kvp in recentlyBackedItems)
        //                {
        //                    lastUsedItems[kvp.Key] = kvp.Value;
        //                    totalUsedItemsPrice += kvp.Value.item.Price;
        //                }
        //                recentlyBackedItems.Clear();
        //            }
        //            OnDoneButtonClick();

        //            GameManager.Instance.BeenModified = false;
        //        }
        //        else
        //        {
        //            BackAllCurrentlyItem();
        //            GameManager.Instance.BeenModified = false;
        //        }
        //        inventoryData.SaveItems();
        //    }

        //}

        private void OnApplicationQuit()
        {
            
            try
            {
                if (lastUsedItems.Count > 0) {

                    YesCancelIt();
                }
                
            }
            catch (Exception) { }


        }
    }
}
