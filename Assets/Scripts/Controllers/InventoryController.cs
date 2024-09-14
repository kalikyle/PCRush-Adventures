using Inventory.Model;
using Inventory.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Inventory.Model.InventorySO;
using UnityEditor;
using PC.Model;
using PC.UI;
using PC;
using Orders.Model;
//using static UnityEditor.Progress;

namespace Inventory
{
    public class InventoryController : MonoBehaviour
    {

        [SerializeField]
        private UIInventory inventoryUI;

        [SerializeField]
        private InventorySO inventoryData;

        [SerializeField]
        private PCInventSO2 PCData;

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
        public Image DialogBox;
        [SerializeField]
        public TMP_Text DialogText;
        [SerializeField]
        public Button DialogButton;
        [SerializeField]
        public Button YesButton;
        [SerializeField]
        public Button NoButton;

        [SerializeField]
        public Button backButton;
        [SerializeField]
        public Button shopButton;

        [SerializeField]
        public Button CancelButton;

        [SerializeField]
        public Button TestPCButton;


        //[SerializeField]
        //public Button infoButton;

        [SerializeField]
        public Button surrenderButton;



        [SerializeField]
        public TMP_Text DisplayText;
        [SerializeField]
        public TMP_Text PriceText;
        [SerializeField]
        public TMP_Text NameText;

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

        [SerializeField]
        private Canvas Buildscene;
        [SerializeField]
        private Canvas ShopScene;

        [SerializeField]
        private Canvas HomeScene;

        [SerializeField]
        public GameObject[] objectsToCheck;

        public List<InventoryItem> initialItems = new List<InventoryItem>();
        public event Action<int> OnDescriptionRequested;

        public UIInventoryItem items;

        [SerializeField]
        public GameObject SuccesfullyCreated;

        [SerializeField]
        public Image SuccesfullPCImage;

        [SerializeField]
        public TMP_Text SuccesfullPCName;


        //[SerializeField]
        //public AudioSource useSound;

        public GameObject _2exp;


        public GameObject Parts;
        public GameObject Faqs;
        public GameObject Trivia;
        public GameObject About;





        public void LoadMyHomeScene()
        {
            // Load the "MyDesk" scene when the button is clicked.
            inventoryUI.Hide();
            Buildscene.gameObject.SetActive(false);
            HomeScene.gameObject.SetActive(true);
            //SceneManager.LoadScene("Home",LoadSceneMode.Additive);
        }
        public void LoadMyShopScene()
        {
            // Load the "MyDesk" scene when the button is clicked.
            inventoryUI.Hide();
            // Buildscene.gameObject.SetActive(false);
            // HomeScene.gameObject.SetActive(false);
            //Buildscene.gameObject.SetActive(false);
            //TopUI.gameObject.SetActive(false);
            //ShopScene.gameObject.SetActive(true);
            //SceneManager.LoadScene("Shop", LoadSceneMode.Additive);
            //SceneManager.LoadScene("Shop", LoadSceneMode.Additive);

        }
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
       
        private void Start()
        {
            //LoadPCSO2();
            
            //LoadInitialItems();
            GameManager2.Instance.OnItemsToTransferUpdated += UpdateInventory;
            PrepareInventoryData();
            PrepareUI();
            CheckAndUpdateDoneButton();


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
            shopButton.onClick.AddListener(LoadMyShopScene);

            DoneButton.onClick.AddListener(OnDoneButtonClick);


            TestPCButton.onClick.AddListener(FinishPC);

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
            lastUsedItems.Clear();
            usedItems.Clear();
            CancelButton.interactable = false;
            Debug.LogError("Triggered");
        }

        private void PrepareInventoryData()
        {
            initialItems.AddRange(GameManager2.Instance.itemsToTransfer);
            inventoryData.Initialize();
            inventoryData.OnInventoryUpdated += UpdateInventoryUI;
                foreach (InventoryItem item in initialItems)
                {
                    if (item.isEmpty) { continue;}
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
        
        private void PrepareUI()
        {
            inventoryUI.InitializeInventoryUI(GetUsedSlotsCount());
            //inventoryUI.InitializeInventoryUI(initialItems.Count);//inventoryData.Size
            inventoryUI.OnDescriptionRequested += HandleDescriptionRequest;
            inventoryUI.OnSwapItems += HandleSwapItems;
            //inventoryUI.OnStartDragging += HandleDragging;
            inventoryUI.OnItemActionRequested += HandleItemActionRequest;
            OnDescriptionRequested += HandleDescriptionRequests;//create new event handler

            CaseButton.onClick.AddListener(() => { OpenFiltered("Case");});
            MBButton.onClick.AddListener(() => OpenFiltered("Motherboard"));
            CPUButton.onClick.AddListener(() => OpenFiltered("CPU"));
            CPUFButton.onClick.AddListener(() => OpenFiltered("CPU Fan"));
            RAMButton.onClick.AddListener(() => OpenFiltered("RAM"));
            GPUButton.onClick.AddListener(() => OpenFiltered("Video Card"));
            STRG1Button.onClick.AddListener(() => OpenFiltered("Storage"));
            PSUButton.onClick.AddListener(() => OpenFiltered("PSU"));
            InventoryButton.onClick.AddListener(() => ToggleALLButton());
            XButton.onClick.AddListener(() => inventoryUI.Hide());
            XButton.gameObject.SetActive(false);

            useButton.onClick.AddListener(HandleUseButton);
            sellButton.onClick.AddListener(HandleSellButton);

            if (TheGame.instance != null)
            {
                surrenderButton.onClick.AddListener(TheGame.instance.OnSurrenderButtonClick);
            }

            MBXButton.onClick.AddListener(() => HandleBackItem("Motherboard"));
            CPUXButton.onClick.AddListener(() => HandleBackItem("CPU"));
            CPUFXButton.onClick.AddListener(() => HandleBackItem("CPU Fan"));
            RAMXButton.onClick.AddListener(() => HandleBackItem("RAM"));
            GPUXButton.onClick.AddListener(() => HandleBackItem("Video Card"));
            STRG1XButton.onClick.AddListener(() => HandleBackItem("Storage"));
            PSUXButton.onClick.AddListener(() => HandleBackItem("PSU"));

            DialogButton.onClick.AddListener(CloseDialog);

           

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
                inventoryUI.AddShopItem2(item.item.ItemImage, item.quantity);
                originalIndex++;
                tempIndex++;
            }

        }

        public bool ToogleFiltered = true;
        public Dictionary<string, InventoryItem> lastUsedItems = new Dictionary<string, InventoryItem>();
        

        private void HandleSellButton()
        {
           
            int tempIndex = GameManager2.Instance.tempindex;
           
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
                            GameManager2.Instance.tempindex = tempIndex;

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
                
               

            }
            else
            {
                
                InventoryItem inventoryItem = inventoryData.GetItemAt(tempIndex);
               

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
               
                





            }

           
        }

        private void HandleUseButton()
        {
            
            int tempIndex = GameManager2.Instance.tempindex;
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
           


        }
        private void CloseDialog()
        {
            DialogBox.gameObject.SetActive(false);
        }

        private void Unused(InventoryItem inventoryItem,string category)
        {
           
            if (CaseImage.sprite == null)
            {
                DialogBox.gameObject.SetActive(true);
                DialogText.text = "Can't Use this " + category + " without the Case";
                YesButton.gameObject.SetActive(false);
                NoButton.gameObject.SetActive(false);
                DialogButton.gameObject.SetActive(true);
                try
                {
                   
                    HandleBackItem(category);

                    


                }
                catch (Exception) { }
            }
            else if (MBImage.sprite == null)
            {
                DialogBox.gameObject.SetActive(true);
                DialogText.text = "Can't Use this " + category + " without the Motherboard";
                YesButton.gameObject.SetActive(false);
                NoButton.gameObject.SetActive(false);
                DialogButton.gameObject.SetActive(true);
                try
                {
                    HandleBackItem(category);

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
           
            switch (category)
            {
                case "Case":
                    CaseImage.gameObject.SetActive(false);
                    CaseImage.sprite = null;
                    GameManager2.Instance.UsedImagesNeeds.Remove(category);
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
                        //int index = inventoryData.inventoryItems.IndexOf(previousUsedItem);
                        //inventoryData.RemoveItem(index, 1);
                        foreach (string categories in categoriesToHandle)
                        {
                            if (lastUsedItems.ContainsKey(categories))
                            {
                                HandleBackItem(categories);
                            }
                        }
                    }
                    catch (Exception) { }
                    GameManager2.Instance.UsedImagesNeeds.Remove(category);
                    break;
                case "CPU":
                    CPUImage.gameObject.SetActive(false);
                    CPUImage.sprite = null;
                    GameManager2.Instance.UsedImagesNeeds.Remove(category);
                    break;
                case "CPU Fan":
                    CPUFImage.gameObject.SetActive(false);
                    CPUFImage.sprite = null;
                    GameManager2.Instance.UsedImagesNeeds.Remove(category);
                    break;
                case "RAM":
                    RAMImage.gameObject.SetActive(false);
                    RAMImage.sprite = null;
                    GameManager2.Instance.UsedImagesNeeds.Remove(category);
                    break;
                case "Video Card":
                    GPUImage.gameObject.SetActive(false);
                    GPUImage.sprite = null;
                    GameManager2.Instance.UsedImagesNeeds.Remove(category);
                    break;
                case "Storage":
                    STRG1Image.gameObject.SetActive(false);
                    STRG1Image.sprite = null;
                    GameManager2.Instance.UsedImagesNeeds.Remove(category);
                    break;
                case "PSU":
                    PSUImage.gameObject.SetActive(false);
                    PSUImage.sprite = null;
                    GameManager2.Instance.UsedImagesNeeds.Remove(category);
                    break;

            }
            OnGameObjectStateChanged();
            usedItems.Remove(previousUsedItem);
            lastUsedItems.Remove(category);
            GameManager2.Instance.itemsToTransfer.Remove(previousUsedItem);
            totalUsedItemsPrice -= previousUsedItem.item.Price;

            // Display the updated total price.
            DisplayPrices(totalUsedItemsPrice);
        }
        public Dictionary<string, InventoryItem> recentlyBackedItems = new Dictionary<string, InventoryItem>();
        public List<InventoryItem> usedItems = new List<InventoryItem>();
        private bool HasItemBeenUsed(InventoryItem inventoryItem)
        {
            // Check if the item has been used in the same category
            return usedItems.Exists(item => item.item.Category == inventoryItem.item.Category && item.item.Name == inventoryItem.item.Name);
        }

        public bool CheckCompatibility (InventoryItem inventoryItem)
        {
            if (TheGame.instance != null)
            {
                if (TheGame.instance.IsNormalMode.Value == true || TheGame.instance.IsHardMode.Value == true)
                {

                    Debug.LogError("CHECKING");
                    if (inventoryItem.item == null)
                    {
                        return false; // No item to check compatibility for
                    }

                    // Search for a motherboard in the usedItems list
                    InventoryItem motherboardItem = usedItems.Find(item => item.item.Category == "Motherboard");

                    // If a motherboard is found, check compatibility based on the category of the current item
                    if (!motherboardItem.isEmpty)
                    {
                        if (inventoryItem.item.Category == "CPU")
                        {
                            Debug.LogError("CPU");
                            // Check CPU compatibility with the motherboard's supported socket
                            if (inventoryItem.item.CPUSupportedSocket == motherboardItem.item.CPUSocket)
                            {
                                Debug.LogError("compatible");
                                return true; // Compatible

                            }
                            else
                            {
                                Debug.LogError("Not");
                                return false; // Not compatible
                            }
                        }

                        if (inventoryItem.item.Category == "RAM")
                        {
                            // Check RAM compatibility with the motherboard's supported RAM slot
                            if (inventoryItem.item.RAMSupportedSlot == motherboardItem.item.RAMSlot)
                            {
                                return true; // Compatible
                            }
                            else
                            {
                                return false; // Not compatible
                            }
                        }
                    }

                }
            }
            
                // If the item category doesn’t require a compatibility check, or if no motherboard is found, return true
                return true;
            
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
                YesButton.gameObject.SetActive(false);
                NoButton.gameObject.SetActive(false);
                DialogButton.gameObject.SetActive(true);
            }
            else if (CheckCompatibility(inventoryItem) == false)
            {
                inventoryUI.Hide();
                DialogBox.gameObject.SetActive(true);
                DialogText.text = "This Part is not compatible to the Motherboard...";
                YesButton.gameObject.SetActive(false);
                NoButton.gameObject.SetActive(false);
                DialogButton.gameObject.SetActive(true);
            }
            else
            {
                //useSound.Play();
                //MBImage.sprite = inventoryItem.item.ItemImage; // this is for the whole inventory
                UseItems(inventoryItem, category);
                inventoryData.RemoveItem(itemIndex, 1);
                BackItem(inventoryItem, category);

                totalUsedItemsPrice += inventoryItem.item.Price;
                DisplayPrices(totalUsedItemsPrice);

                

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

                OpenMiniGames(category, inventoryItem);



            }

           

        }
        private void DisplayPrices(double totalPrice)
        {
            //PriceText.text = "$" + totalPrice.ToString("F2");
        }

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
                    YesButton.gameObject.SetActive(false);
                    NoButton.gameObject.SetActive(false);
                    DialogButton.gameObject.SetActive(true);
                }

                else if (CheckCompatibility(inventoryItem) == false)
                {
                    inventoryUI.Hide();
                    DialogBox.gameObject.SetActive(true);
                    DialogText.text = "This Part is not compatible to the Motherboard...";
                    YesButton.gameObject.SetActive(false);
                    NoButton.gameObject.SetActive(false);
                    DialogButton.gameObject.SetActive(true);
                }
                else
                {
                    //useSound.Play();
                    usedItems.Remove(inventoryItem);
                    UseItems(inventoryItem, category);
                    int index = inventoryData.inventoryItems.IndexOf(inventoryItem);

                    inventoryData.RemoveItem(index, 1);
                    BackItem(inventoryItem, category);

                    totalUsedItemsPrice += inventoryItem.item.Price;
                    DisplayPrices(totalUsedItemsPrice);

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

                    OpenMiniGames(category, inventoryItem);
                    

                }
               
            }
        }

        public void OpenMiniGames(string category, InventoryItem inventoryItem)
        {

            if(category == "Case")
            {
                
                GameManager2.Instance.BuildScene.gameObject.SetActive(false);
                SceneManager.LoadScene("CaseMiniGame", LoadSceneMode.Additive);
                GameManager2.Instance.MainCamera.gameObject.SetActive(false);
            }


            if(category == "Motherboard")
            {
                GameManager2.Instance.BuildScene.gameObject.SetActive(false);
                SceneManager.LoadScene("MotherboardMiniGame", LoadSceneMode.Additive);
                GameManager2.Instance.MainCamera.gameObject.SetActive(false);

            }

            if (category == "CPU")
            {
                //ApplyThermal.gameObject.SetActive(true);
                //AT.SetCPUImage(inventoryItem.item.ItemImage);
                GameManager2.Instance.BuildScene.gameObject.SetActive(false);
                SceneManager.LoadScene("CPUMiniGame", LoadSceneMode.Additive);
                GameManager2.Instance.MainCamera.gameObject.SetActive(false);

            }

            if (category == "RAM")
            {
                GameManager2.Instance.BuildScene.gameObject.SetActive(false);
                SceneManager.LoadScene("RAMMiniGame", LoadSceneMode.Additive);
                GameManager2.Instance.MainCamera.gameObject.SetActive(false);

            }


            if (category == "CPU Fan")
            {
                GameManager2.Instance.BuildScene.gameObject.SetActive(false);
                SceneManager.LoadScene("CPUFanMiniGame", LoadSceneMode.Additive);
                GameManager2.Instance.MainCamera.gameObject.SetActive(false);

            }

            if (category == "Video Card")
            {

                GameManager2.Instance.BuildScene.gameObject.SetActive(false);
                SceneManager.LoadScene("GPUMiniGame", LoadSceneMode.Additive);
                GameManager2.Instance.MainCamera.gameObject.SetActive(false);

            }

            if (category == "Storage")
            {

                GameManager2.Instance.BuildScene.gameObject.SetActive(false);
                SceneManager.LoadScene("StorageMiniGame", LoadSceneMode.Additive);
                GameManager2.Instance.MainCamera.gameObject.SetActive(false);

            }

            if (category == "PSU")
            {
                if (!(CaseImage.isActiveAndEnabled && MBImage.isActiveAndEnabled && CPUImage.isActiveAndEnabled && CPUFImage.isActiveAndEnabled && RAMImage.isActiveAndEnabled && GPUButton.isActiveAndEnabled && STRG1Image.isActiveAndEnabled))
                {
                    DialogBox.gameObject.SetActive(true);
                    DialogText.text = "Can't Use this " + category + " without the needed Parts";
                    YesButton.gameObject.SetActive(false);
                    NoButton.gameObject.SetActive(false);
                    DialogButton.gameObject.SetActive(true);
                    try
                    {
                        HandleBackItem(category);

                    }
                    catch (Exception) { }
                }
                else
                {
                    GameManager2.Instance.BuildScene.gameObject.SetActive(false);
                    SceneManager.LoadScene("PSUMiniGame", LoadSceneMode.Additive);
                    GameManager2.Instance.MainCamera.gameObject.SetActive(false);

                }


            }
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
                        totalUsedItemsPrice -= previousUsedItem.item.Price;
                        inventoryData.AddItem(previousUsedItem.ChangeQuantity(previousUsedItem.quantity - lastUsedItems[category].quantity + 1));
                        Debug.Log(previousUsedItem.quantity);
                        usedItems.Remove(previousUsedItem);
                        GameManager2.Instance.itemsToTransfer.Remove(previousUsedItem);
                        lastUsedItems.Remove(category);
                        
                    }
                    else
                    {
                        // The item hasn't changed, so add it back to the inventory
                        totalUsedItemsPrice -= previousUsedItem.item.Price;
                        totalUsedItemsPrice += inventoryItem.item.Price;
                        inventoryData.AddItem(previousUsedItem.ChangeQuantity(previousUsedItem.quantity - lastUsedItems[category].quantity + 1));
                        Debug.Log(previousUsedItem.quantity);
                        usedItems.Remove(previousUsedItem);
                        GameManager2.Instance.itemsToTransfer.Remove(previousUsedItem);
                    }
                }

            }
            // Update the last used item for the category
            lastUsedItems[category] = inventoryItem;
            //Debug.LogError("added to lastitem");
            DisplayPrices(totalUsedItemsPrice);
        }

        public ApplyThermalPaste AT;

        public void UseItems(InventoryItem inventoryItem, string category)
        {
            switch (category)
            {
                case "Case":
                    CaseImage.gameObject.SetActive(true);
                    CaseImage.sprite = inventoryItem.item.ItemImage;
                    MBButton.interactable = true;
                    CancelButton.interactable = true;
                    GameManager2.Instance.UsedImagesNeeds[category] = inventoryItem;
                    break;
                case "Motherboard":
                    MBImage.gameObject.SetActive(true);
                    MBImage.sprite = inventoryItem.item.ItemImage;
                    CPUButton.interactable = true;
                    CPUFButton.interactable = true;
                    RAMButton.interactable = true;
                    GPUButton.interactable = true;
                    STRG1Button.interactable = true;
                    GameManager2.Instance.UsedImagesNeeds[category] = inventoryItem;
                    break;
                case "CPU":
                    CPUImage.gameObject.SetActive(true);
                    CPUImage.sprite = inventoryItem.item.ItemImage;
                    GameManager2.Instance.UsedImagesNeeds[category] = inventoryItem;
                    //ApplyThermal.gameObject.SetActive(true);
                    //AT.SetCPUImage(inventoryItem.item.ItemImage);
                    break;
                case "CPU Fan":
                    CPUFImage.gameObject.SetActive(true);
                    CPUFImage.sprite = inventoryItem.item.ItemImage;
                    GameManager2.Instance.UsedImagesNeeds[category] = inventoryItem;
                    break;
                case "RAM":
                    RAMImage.gameObject.SetActive(true);
                    RAMImage.sprite = inventoryItem.item.ItemImage;
                    GameManager2.Instance.UsedImagesNeeds[category] = inventoryItem;
                    break;
                case "Video Card":
                    GPUImage.gameObject.SetActive(true);
                    GPUImage.sprite = inventoryItem.item.ItemImage;
                    GameManager2.Instance.UsedImagesNeeds[category] = inventoryItem;
                    break;
                case "Storage":
                     STRG1Image.gameObject.SetActive(true);
                     STRG1Image.sprite = inventoryItem.item.ItemImage;
                    GameManager2.Instance.UsedImagesNeeds[category] = inventoryItem;
                    break;
                    
                case "PSU":
                    PSUImage.gameObject.SetActive(true);
                    PSUImage.sprite = inventoryItem.item.ItemImage;
                    GameManager2.Instance.UsedImagesNeeds[category] = inventoryItem;
                    break;

            }
            if (CaseImage.isActiveAndEnabled && MBImage.isActiveAndEnabled && CPUImage.isActiveAndEnabled && CPUFImage.isActiveAndEnabled && RAMImage.isActiveAndEnabled && GPUImage.isActiveAndEnabled && STRG1Image.isActiveAndEnabled)
            {
                PSUButton.interactable = true;
            }
            OnGameObjectStateChanged();
            inventoryUI.Hide();
        }

      
       

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
            ItemSO item = inventoryItem.item;
            inventoryUI.UpdateDescription(itemIndex, item.ItemImage, item.Name, Compat(inventoryItem), Speed(inventoryItem), item.Category);
        }



        public string Speed(InventoryItem item)
        {
            string speed = "";

            if (item.item.Category == "Case")
            {
                speed = "Case Strength: " + item.item.CaseStrength.ToString();
            }

            if (item.item.Category == "Motherboard")
            {
                speed = "Motherboard Strength: " + item.item.MotherboardStrength.ToString();
            }

            if (item.item.Category == "CPU")
            {
                speed = "CPU Base Speed: " + item.item.BaseSpeed.ToString() + "Ghz";

            }

            if (item.item.Category == "RAM")
            {
                speed = "Memory: " + item.item.Memory.ToString() + "GB";
            }

            if (item.item.Category == "CPU Fan")
            {
                speed = "Cooling Power: " + item.item.CoolingPower.ToString();
            }

            if (item.item.Category == "Video Card")
            {
                speed = "GPU Clock Speed: " + item.item.ClockSpeed.ToString() + "Mhz";
            }

            if (item.item.Category == "Storage")
            {
                speed = "Storage: " + item.item.Storage.ToString() + "GB";
            }

            if (item.item.Category == "PSU")
            {
                speed = "Power: " + item.item.WattagePower.ToString() + "W";
            }

            return speed;
        }

        public string Compat(InventoryItem item)
        {
            string compat = "";

            if (item.item.Category == "Case")
            {
                return compat;
            }

            if (item.item.Category == "Motherboard")
            {
                compat = item.item.CPUSocket.ToString() + "\n" + item.item.RAMSlot.ToString();
            }

            if (item.item.Category == "CPU")
            {
                compat = item.item.CPUSupportedSocket.ToString();

            }

            if (item.item.Category == "RAM")
            {
                compat = item.item.RAMSupportedSlot.ToString();
            }

            if (item.item.Category == "CPU Fan")
            {
                return compat;
            }

            if (item.item.Category == "Video Card")
            {
                return compat;
            }

            if (item.item.Category == "Storage")
            {
                return compat;
            }

            if (item.item.Category == "PSU")
            {
                return compat;
            }

            return compat;
        }

        public void HandleDescriptionRequests(int obj)
        {
            if (obj >= 0 && obj < InventoryfilteredItems.Count)
            {
                InventoryItem shopItem = InventoryfilteredItems[obj];
                if (!shopItem.isEmpty)
                {
                    ItemSO item = shopItem.item;
                    inventoryUI.UpdateDescription(obj, item.ItemImage, item.Name, Compat(shopItem), Speed(shopItem), item.Category);//update description 
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
        //public Build build;
        public Canvas MainMenu;
        public LeanTweenAnimate2 LTA;
        public void Update()
        {
            OnGameObjectStateChanged();
            //inventoryData.SaveItems();
            if (CaseImage.isActiveAndEnabled && MBImage.isActiveAndEnabled && CPUImage.isActiveAndEnabled && CPUFImage.isActiveAndEnabled && RAMImage.isActiveAndEnabled && GPUButton.isActiveAndEnabled && STRG1Image.isActiveAndEnabled)
            {
                PSUButton.interactable = true;
            }
        }
        public GameObject ModifyingPC;
      
        private void ToggleALLButton()
        {
           // infoButton.gameObject.SetActive(false);
            ToogleFiltered = false;
            Debug.Log("Toggle: false");
            inventoryUI.ResetSelection();
                inventoryUI.ClearItems();
                inventoryUI.InitializeInventoryUI(GetUsedSlotsCount());
                OpenInvBTN();


        }
        public string infocategory = null;
        public void OpenFiltered(string category)
        {
            //infoButton.gameObject.SetActive(true);
            Debug.Log("Toggle: true");
            ShowCategory(category);
            infocategory = category;
            DisplayText.text = "   "+category;
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
               
                if (inventoryUI.isActiveAndEnabled == false)
                {
                inventoryUI.Show();
               
                DisplayText.text = "Inventory".ToUpper();
                foreach (var item in inventoryData.GetCurrentInventoryState())
                    {
                        inventoryUI.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.quantity);
                    }
                }
                else
                {
                
                inventoryUI.Hide();
                
            }

        }

        public void SeeUsedItemList()
        {
            foreach (var kvp in lastUsedItems)
            {
                string category = kvp.Key;
                InventoryItem inventoryItem = kvp.Value;

                Debug.Log($"Category: {category}, Item: {inventoryItem.item.Name}, Quantity: {inventoryItem.quantity}, Price: {inventoryItem.item.Price}");
            }
        }

        //public List<PCSO2> CreatedComputers;

        public void FinishPC()
        {

            //PCC.TestComputer(PCData.ComputerItems.Count - 1);
            //LTA.hideBuild();
            //LTA.HideSuccess();

           

                TheGame.instance.OnFinishButtonClick();
                TheGame.instance.pcImage.sprite = PC.Case.ItemImage;
                GameManager2.Instance.OnItemsToTransferUpdated -= UpdateInventory;


            

        }
        PCSO2 PC;
        public PCSO2 ConvertLastUsedItemsToPCSO2List()
        {
            
            PCSO2 pcso = ScriptableObject.CreateInstance<PCSO2>();

            pcso.name = "Your PC has Been Finished";
            pcso.PCName = "Your PC has Been Finished";
            pcso.PCImage = lastUsedItems.ContainsKey("Case") ? lastUsedItems["Case"].item.ItemImage : null;
            pcso.PCPrice = 0;
            pcso.Case = lastUsedItems.ContainsKey("Case") ? lastUsedItems["Case"].item : null;
            pcso.Motherboard = lastUsedItems.ContainsKey("Motherboard") ? lastUsedItems["Motherboard"].item : null;
            pcso.CPU = lastUsedItems.ContainsKey("CPU") ? lastUsedItems["CPU"].item : null;
            pcso.CPUFan = lastUsedItems.ContainsKey("CPU Fan") ? lastUsedItems["CPU Fan"].item : null;
            pcso.RAM = lastUsedItems.ContainsKey("RAM") ? lastUsedItems["RAM"].item : null;
            pcso.GPU = lastUsedItems.ContainsKey("Video Card") ? lastUsedItems["Video Card"].item : null;
            pcso.STORAGE = lastUsedItems.ContainsKey("Storage") ? lastUsedItems["Storage"].item : null;
            pcso.PSU = lastUsedItems.ContainsKey("PSU") ? lastUsedItems["PSU"].item : null;
            pcso.TestStatus = "Untested";
            // During UnityEditor, save the ScriptableObject asset
            //string savePath = "Assets/Data/COMPUTERS/";
            //string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(savePath + pcso.name + ".asset");
            //AssetDatabase.CreateAsset(pcso, assetPathAndName);
            //AssetDatabase.SaveAssets();
            //AssetDatabase.Refresh();
            //CreatedComputers.Add(pcso);
            //SavePCSO2Data(pcso);
            //GameManager2.Instance.SaveUniqueIndex(UniqueIndex);
            // Add the created PCSO2 to the list
            return pcso;
        }
        public void SavePCSO2Data(PCSO2 pcso)
        {
            // Convert PCSO2 data to JSON (you can use other serialization methods)
            string jsonData = JsonUtility.ToJson(pcso);

            // Save the JSON data to PlayerPrefs with a unique key based on the index
            int index = GetUniqueIndex();
            PlayerPrefs.SetString($"PCs_{index}", jsonData);

            // Add the key to the list of keys for later retrieval
            string keys = PlayerPrefs.GetString("PCs_Keys", "");
            keys += (keys.Length > 0 ? "," : "") + $"PCs_{index}";
            PlayerPrefs.SetString("PCs_Keys", keys);
           
        }
        public List<int> UniqueIndex = new List<int>();
        private int GetUniqueIndex()
        {
            // Generate a unique index based on the current time
            int newIndex = (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
            UniqueIndex.Add(newIndex);
            return newIndex;
        }

        [SerializeField]
        private PCPage2 PCpage;
        

        public TMP_Text Modified;
        public GameObject ParentCheckPanel,checkpanel, didnotpanel, congratspanel, Checkcircle, Checking, didnotmet1, didnotmet2, backbutton, congrats, congrats1;


        public void OnDoneButtonClick()
        {
            // Convert the last used items to PCSO2 and assign it to this.PC
            PCSO2 PC = ConvertLastUsedItemsToPCSO2List();
            this.PC = PC;

            if (TheGame.instance.IsHardMode.Value == true)
            {
                 
                // Set the checkpanel active
                LeanTween.scale(ParentCheckPanel, new Vector2(2.25f, 2.25f), 0.5f).setEase(LeanTweenType.easeOutCubic);
                checkpanel.SetActive(true);

                // Rotate the Checkcircle and fade in the Checking text
                LeanTween.rotateAround(Checkcircle, Vector3.forward, -360f, 3f).setLoopClamp();
                LeanTween.scale(Checking, new Vector2(1f, 1f), 1f);

                // After 3 seconds, check the mission requirements
                LeanTween.delayedCall(3f, () =>
                {
                    
                    // Get the current mission's requirements
                    Missions currentMission = TheGame.instance.currentMission;
                    bool isMissionMet = true;

                    // Check if the PC meets the mission requirements
                    if (PC.Case.CaseStrength < currentMission.orders.CaseStrength) { isMissionMet = false; }
                    if (PC.Motherboard.MotherboardStrength < currentMission.orders.MotherboardStrength) { isMissionMet = false; }
                    if (PC.CPU.BaseSpeed < currentMission.orders.CPUBaseSpeed) { isMissionMet = false; }
                    if (PC.RAM.Memory < currentMission.orders.RAMMemory) { isMissionMet = false; }
                    if (PC.CPUFan.CoolingPower < currentMission.orders.CPUFanCoolingPower) { isMissionMet = false; }
                    if (PC.GPU.ClockSpeed < currentMission.orders.GPUClockSpeed) { isMissionMet = false; }
                    if (PC.STORAGE.Storage < currentMission.orders.Storage) { isMissionMet = false; }
                    if (PC.PSU.WattagePower < currentMission.orders.PSUWattagePower) { isMissionMet = false; }

                    if (!isMissionMet)
                    {
                        // Set the didnotpanel active and perform animations
                        checkpanel.SetActive(false);
                        didnotpanel.SetActive(true);
                        LeanTween.scale(didnotmet1, Vector3.one, 1f).setEase(LeanTweenType.easeOutElastic);
                        LeanTween.scale(didnotmet2, new Vector2(1f, 1f), 1f);
                        LeanTween.scale(backbutton, Vector3.one, 1f).setDelay(1f).setEase(LeanTweenType.easeOutElastic);
                    }
                    else
                    {
                        // Set the checkpanel to inactive and perform congrats animations
                        checkpanel.SetActive(false);
                        congratspanel.SetActive(true);
                        LeanTween.scale(congrats, Vector3.one, 0.5f).setEase(LeanTweenType.easeOutElastic);
                        LeanTween.scale(congrats1, Vector3.one, 0.5f);

                        // Call DonePC after the congrats animation
                        LeanTween.delayedCall(1.5f, () =>
                        {
                            DonePC(PC);
                            ResetUIElements();
                        });
                    }

                });

            }
            else {

                //for normal and easy mode
                DonePC(PC);

            }
        }

        public void ResetUIElements()
        {
            // Reset panels
            checkpanel.SetActive(false);
            didnotpanel.SetActive(false);
            congratspanel.SetActive(false);

            LeanTween.scale(ParentCheckPanel, new Vector2(0f, 2.25f), 0.5f).setEase(LeanTweenType.easeOutCubic);
            // Reset scaling and alpha of elements
            Checkcircle.transform.rotation = Quaternion.identity; // Reset rotation
            LeanTween.cancel(Checkcircle); // Stop any ongoing rotation
            LeanTween.scale(Checking, Vector2.zero, 0f); // Reset Checking text alpha

            didnotmet1.transform.localScale = Vector3.zero; // Reset scale
            didnotmet2.transform.localScale = Vector3.zero; ; // Reset alpha
            backbutton.transform.localScale = Vector3.zero; // Reset scale

            congrats.transform.localScale = Vector3.zero; // Reset scale
            congrats1.transform.localScale = Vector3.zero; // Reset alpha

        }

        //public void OnDoneButtonClick()
        //{

        //        // Call the AddPCSO2List method when the button is clicked
        //        //PCSO2 pcso = ConvertLastUsedItemsToPCSO2List();

        //        PCSO2 PC = ConvertLastUsedItemsToPCSO2List();
        //        this.PC = PC;

        //    if (TheGame.instance.IsHardMode.Value == true)
        //    {
        //        // Get the current mission's requirements
        //        Missions currentMission = TheGame.instance.currentMission;
        //        bool isMissionMet = true;

        //        // Check if the PC meets the mission requirements
        //        if (PC.Case.CaseStrength < currentMission.orders.CaseStrength)
        //        {
        //            isMissionMet = false;
        //        }

        //        if (PC.Motherboard.MotherboardStrength < currentMission.orders.MotherboardStrength)
        //        {
        //            isMissionMet = false;
        //        }

        //        if (PC.CPU.BaseSpeed < currentMission.orders.CPUBaseSpeed)
        //        {
        //            isMissionMet = false;
        //        }

        //        if (PC.RAM.Memory < currentMission.orders.RAMMemory)
        //        {
        //            isMissionMet = false;
        //        }

        //        if (PC.CPUFan.CoolingPower < currentMission.orders.CPUFanCoolingPower)
        //        {
        //            isMissionMet = false;
        //        }

        //        if (PC.GPU.ClockSpeed < currentMission.orders.GPUClockSpeed)
        //        {
        //            isMissionMet = false;
        //        }

        //        if (PC.STORAGE.Storage < currentMission.orders.Storage)
        //        {
        //            isMissionMet = false;
        //        }

        //        if (PC.PSU.WattagePower < currentMission.orders.PSUWattagePower)
        //        {
        //            isMissionMet = false;
        //        }

        //        // If all requirements are met, execute DonePC(PC)
        //        if (isMissionMet)
        //        {
        //            DonePC(PC);
        //        }
        //        else
        //        {
        //            // Handle the case where requirements are not met
        //            GameManager2.Instance.TryAgainPanel.SetActive(true);

        //            Debug.Log("PC does not meet the mission requirements.");
        //        }



        //    }
        //    else
        //    {

        //        //for normal and easy mode
        //        DonePC(PC);

        //    }


        //}

        public void DonePC(PCSO2 PC)
        {
            
            //PCData.AddPCSO2List(PC);
            //_2exp.gameObject.SetActive(true);
            //Success.Play();
            SuccesfullyCreated.gameObject.SetActive(true);
            SuccesfullPCName.text = PC.PCName;
            SuccesfullPCImage.sprite = PC.PCImage;
            LTA.ShowSuccessPC();



            //Debug.LogError("Added");
            //Debug.LogError(PCData.ComputerItems.Count);
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
            //totalUsedItemsPrice = 0;
            //GameManager2.Instance.itemsToTransfer.Clear();

            OnGameObjectStateChanged();
            //PCpage.AddAnotherPC();
            //GameManager2.Instance.itemsToTransfer.Clear();

            // Optional: You can perform additional actions after adding the PC items
            GameManager2.Instance.UsedImagesNeeds.Clear();
            GameManager2.Instance.itemsToTransfer.Clear();

            CancelButton.interactable = false;

            Debug.Log("PCSO2 items added to ComputerItems list.");
        }
        
    }

   
   
}



