using Exchanger.Model.CaseWorld;
using Firebase.Firestore;
using Inventory;
using Inventory.Model;
//using Mono.Cecil.Cil;
using Orders.Model;
using PartsInventory;
using PartsInventory.Model;
using PC.Model;
using PC.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Xml.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Inventory.Model.PartsInventorySO;
//using static UnityEditor.Progress;

//using static Inventory.Model.InventorySO;
//
using Image = UnityEngine.UI.Image;

namespace PC
{
    public class PCPlayerController : MonoBehaviour
    {
        [SerializeField]
        private PCInventSO PCData;

        [SerializeField]
        public UnityEngine.UI.Button BuildButton;

        [SerializeField]
        public UnityEngine.UI.Button ShopButton;

        [SerializeField]
        public UnityEngine.UI.Button PCsButton;

        [SerializeField]
        public UnityEngine.UI.Button XButton;

        [SerializeField]
        public UnityEngine.UI.Button ModifyButton;

        [SerializeField]
        public UnityEngine.UI.Button UseButton;

        [SerializeField]
        public UnityEngine.UI.Button InUseButton;

        [SerializeField]
        public UnityEngine.UI.Button sellButton;

        [SerializeField]
        public Canvas ShopScene;

        [SerializeField]
        public Canvas BuildScene;

        [SerializeField]
        public Canvas TopUI;

        [SerializeField]
        private PCPage PCpage;

        [SerializeField]
        private PCMenu PCMenu;

        [SerializeField]
        private GameObject TestingComputerPanel;

        [SerializeField]
        private GameObject EmptyComputerPanel;

        [SerializeField]
        private PartsInventoryController InventCon;

        [SerializeField]
        public UnityEngine.UI.Image DialogBox;
        [SerializeField]
        public TMP_Text DialogText;
        [SerializeField]
        public UnityEngine.UI.Button NoButton;
        [SerializeField]
        public UnityEngine.UI.Button YesButton;

        [SerializeField]
        public UnityEngine.UI.Button exitButton;


        [SerializeField]
        public UnityEngine.UI.Button TurnOnButton;

        [SerializeField]
        public UnityEngine.UI.Button TurnOffButton;

        [SerializeField]
        public UnityEngine.UI.Button ReturnButton;

        [SerializeField]
        public UnityEngine.UI.Button OrderButton;

        [SerializeField]
        public UnityEngine.UI.Button InfoButton;

        [SerializeField]
        private Sprite WindowsOSImage;

        [SerializeField]
        private Image MonitorScreen;


        //for use pc desc
        //[SerializeField]
        //private Image PCImage;
        //[SerializeField]
        //private TMP_Text PCName;
        //[SerializeField]
        //private TMP_Text PCPrice;

        //[SerializeField]
        //private TMP_Text Perks;

        public ComputerDescription PCDesccription;
        private PCSO UsedPC;

        //[SerializeField]
        //private TMP_Text CaseName;
        //[SerializeField]
        //private TMP_Text MBName;
        //[SerializeField]
        //private TMP_Text CPUName;
        //[SerializeField]
        //private TMP_Text CPUFName;
        //[SerializeField]
        //private TMP_Text RAMName;
        //[SerializeField]
        //private TMP_Text GPUName;
        //[SerializeField]
        //private TMP_Text STRGName;
        //[SerializeField]
        //private TMP_Text PSUName;



        //[SerializeField]
        //private Image CaseImage;
        //[SerializeField]
        //private Image MBImage;
        //[SerializeField]
        //private Image CPUImage;
        //[SerializeField]
        //private Image CPUFImage;
        //[SerializeField]
        //private Image RAMImage;
        //[SerializeField]
        //private Image GPUImage;
        //[SerializeField]
        //private Image STRGImage;
        //[SerializeField]
        //private Image PSUImage;

        //[SerializeField]
        //private Button CaseInfoBTN;
        //[SerializeField]
        //private Button MBInfoBTN;
        //[SerializeField]
        //private Button CPUInfoBTN;
        //[SerializeField]
        //private Button CPUFInfoBTN;
        //[SerializeField]
        //private Button RAMInfoBTN;
        //[SerializeField]
        //private Button GPUInfoBTN;
        //[SerializeField]
        //private Button STRGInfoBTN;
        //[SerializeField]
        //private Button PSUInfoBTN;

        //[SerializeField]
        //private Image PartImage;

        //[SerializeField]
        //private TMP_Text PartsName;
        //[SerializeField]
        //private TMP_Text PartsCategory;
        //[SerializeField]
        //private TMP_Text PartsRarity; 
        //[SerializeField]
        //private TMP_Text PartsPerks;
        //[SerializeField]
        //private TMP_Text PartsPrice;



        [SerializeField]
        private Image PCImagePlaceholder;

        //[SerializeField]
        //private AnimateHandler Anim;

        [SerializeField]
        private GameObject Blocker;

        [SerializeField]
        private TMP_Text BlockerText;
        
        public EventTrigger PCeventTrigger1;
        public EventTrigger PCeventTrigger2;

       
        
        public GameObject SubmitPanel;

        
        public GameObject TnMPanel;

        [SerializeField]
        private GameObject CongratsPanel;

        [SerializeField]
        private Image SubmittedPCImage;
        public TMP_Text SubmittedPCName;
        public TMP_Text SubmittedPCPrice;

        public TMP_Text displayText;
        public TMP_Text displayText2;
        public TMP_Text displayText3;

        public TMP_Text Price;
        public TMP_Text ExP;

        public TMP_Text ClientName;
        public Image ClientImage;

        public GameObject theClickable;

        public GameObject Desktop;
        public GameObject UIPanel;

        //[SerializeField]
        //private MissionConSO OrderData;

        //[SerializeField]
        //private AudioSource SubmitSound;


        //[SerializeField]
        //public UnityEngine.UI.Image DialogBoxForQuit;
        //[SerializeField]
        //public TMP_Text DialogTextForQuit;
        //[SerializeField]
        //public UnityEngine.UI.Button NoButtonForQuit;
        //[SerializeField]
        //public UnityEngine.UI.Button YesButtonForQuit;


        //[SerializeField]
        //public Canvas Canvas;
        //[SerializeField]
        //private InventorySO inventoryData;

        public int inventorySize = 10;
        public void Awake()
        {
            PCMenu.Hide();
            StartCoroutine(DelayedComputerLoad());

            //GameObject image = GameObject.Find("PCItemImage");
            //if (image != null)
            //{
            //    PCImage = image.GetComponent<Image>();
            //}
        }
        IEnumerator DelayedComputerLoad()
        {
            // Wait for 1 second
            yield return new WaitForSeconds(1f);

            // Now load initial items
            //LoadComputerItems();
            LoadPCSOList();
        }
        //private async void LoadPCSOLists()
        //{
        //    PCData.ComputerItems.Clear();
        //    // Get a reference to the Firestore document containing the PCSO data
        //    DocumentReference docRef = FirebaseFirestore.DefaultInstance
        //        .Collection(GameManager.instance.UserCollection)
        //        .Document(GameManager.instance.UserID)
        //        .Collection("PCSOCollection")
        //        .Document("PCSOItem");

        //    // Fetch the document snapshot asynchronously
        //    DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();


        //    if (snapshot.Exists)
        //    {
        //        // Retrieve the serialized JSON array of PCSO data from Firestore
        //        string pcsoJson = snapshot.GetValue<string>("pcsoData");

        //        if (!string.IsNullOrEmpty(pcsoJson))
        //        {
        //            // Deserialize the JSON array into an array of PCSO objects


        //            // Clear existing items in PCData.ComputerItems


        //            // Add each loaded PCSO item to PCData.ComputerItems

        //            PCSO newPCSO = ScriptableObject.CreateInstance<PCSO>();

        //            // Copy the data from the loaded PCSO to the new instance
        //            JsonUtility.FromJsonOverwrite(pcsoJson, newPCSO);

        //            PCData.AddPCSOList(newPCSO);
        //            PCpage.AddAnotherPC();


        //            Debug.Log("PCSO items loaded from Firestore.");
        //        }
        //    }
        //    else
        //    {
        //        Debug.LogWarning("PCSO document does not exist in Firestore.");
        //    }
        //}
        
        public void OpenDesktop()
        {

            Desktop.SetActive(true);
            UIPanel.SetActive(false);
        }
        public void CloseDesktop()
        {
            Desktop.SetActive(false);
            UIPanel.SetActive(true);
        }


        public async void LoadPCSOList()
        {
            if (GameManager.instance.UserID != "")
            {
                PCData.ComputerItems.Clear();
                GameManager.instance.pcsoDocumentIds.Clear();
                // Get a reference to the Firestore collection containing the PCSO documents
                CollectionReference collectionRef = FirebaseFirestore.DefaultInstance
                    .Collection(GameManager.instance.UserCollection)
                    .Document(GameManager.instance.UserID)
                    .Collection("ComputersCollection");

                // Fetch all documents from the PCSO collection asynchronously
                QuerySnapshot querySnapshot = await collectionRef.GetSnapshotAsync();

                // Iterate through the retrieved documents
                foreach (DocumentSnapshot docSnapshot in querySnapshot.Documents)
                {

                    string documentId = docSnapshot.Id;
                    GameManager.instance.pcsoDocumentIds.Add(documentId);

                    // Deserialize the PCSO data from the Firestore document
                    string PCName = docSnapshot.GetValue<string>("PCName");
                    int Armor = docSnapshot.GetValue<int>("Armor");
                    int AttackDamage = docSnapshot.GetValue<int>("AttackDamage");
                    int Health = docSnapshot.GetValue<int>("Health");
                    int HealthRegen = docSnapshot.GetValue<int>("HealthRegen");
                    int Mana = docSnapshot.GetValue<int>("Mana");
                    int ManaRegen = docSnapshot.GetValue<int>("ManaRegen");
                    int WalkSpeed = docSnapshot.GetValue<int>("WalkSpeed");
                    int CriticalChance = docSnapshot.GetValue<int>("CriticalChance");
                    bool InUse = docSnapshot.GetValue<bool>("InUse");
                    string itemImageBase64 = docSnapshot.GetValue<string>("ItemImage");

                    //if (!string.IsNullOrEmpty(pcsoJson))
                    //{
                        // Create a new PCSO instance
                        PCSO loadedPCSO = ScriptableObject.CreateInstance<PCSO>();

                    // Deserialize the JSON data into the PCSO object
                    //JsonUtility.FromJsonOverwrite(pcsoJson, loadedPCSO);
                            loadedPCSO.PCName = PCName;
                            loadedPCSO.Armor = Armor;
                            loadedPCSO.AttackDamage = AttackDamage;
                            loadedPCSO.Health = Health;
                            loadedPCSO.HealthRegen = HealthRegen;
                            loadedPCSO.Mana = Mana;
                            loadedPCSO.ManaRegen = ManaRegen;
                            loadedPCSO.WalkSpeed = WalkSpeed;
                            loadedPCSO.CriticalChance = CriticalChance;
                            loadedPCSO.inUse = InUse;

                        // Find and assign parts by their UniqueID
                        if (docSnapshot.ContainsField("Case"))
                        {
                            var caseData = docSnapshot.GetValue<Dictionary<string, object>>("Case");
                            string caseUniqueID = caseData["UniqueID"].ToString();
                            loadedPCSO.Case = GameManager.instance.FindPartByUniqueID(caseUniqueID);
                        }

                        if (docSnapshot.ContainsField("Motherboard"))
                        {
                            var motherboardData = docSnapshot.GetValue<Dictionary<string, object>>("Motherboard");
                            string motherboardUniqueID = motherboardData["UniqueID"].ToString();
                            loadedPCSO.Motherboard = GameManager.instance.FindPartByUniqueID(motherboardUniqueID);
                        }

                        if (docSnapshot.ContainsField("CPU"))
                        {
                            var cpuData = docSnapshot.GetValue<Dictionary<string, object>>("CPU");
                            string cpuUniqueID = cpuData["UniqueID"].ToString();
                            loadedPCSO.CPU = GameManager.instance.FindPartByUniqueID(cpuUniqueID);
                        }

                        if (docSnapshot.ContainsField("CPUFan"))
                        {
                            var cpuFanData = docSnapshot.GetValue<Dictionary<string, object>>("CPUFan");
                            string cpuFanUniqueID = cpuFanData["UniqueID"].ToString();
                            loadedPCSO.CPUFan = GameManager.instance.FindPartByUniqueID(cpuFanUniqueID);
                        }

                        if (docSnapshot.ContainsField("RAM"))
                        {
                            var ramData = docSnapshot.GetValue<Dictionary<string, object>>("RAM");
                            string ramUniqueID = ramData["UniqueID"].ToString();
                            loadedPCSO.RAM = GameManager.instance.FindPartByUniqueID(ramUniqueID);
                        }

                        if (docSnapshot.ContainsField("GPU"))
                        {
                            var gpuData = docSnapshot.GetValue<Dictionary<string, object>>("GPU");
                            string gpuUniqueID = gpuData["UniqueID"].ToString();
                            loadedPCSO.GPU = GameManager.instance.FindPartByUniqueID(gpuUniqueID);
                        }

                        if (docSnapshot.ContainsField("STORAGE"))
                        {
                            var storageData = docSnapshot.GetValue<Dictionary<string, object>>("STORAGE");
                            string storageUniqueID = storageData["UniqueID"].ToString();
                            loadedPCSO.STORAGE = GameManager.instance.FindPartByUniqueID(storageUniqueID);
                        }

                        if (docSnapshot.ContainsField("PSU"))
                        {
                            var psuData = docSnapshot.GetValue<Dictionary<string, object>>("PSU");
                            string psuUniqueID = psuData["UniqueID"].ToString();
                            loadedPCSO.PSU = GameManager.instance.FindPartByUniqueID(psuUniqueID);
                        }

                        byte[] imageData = Convert.FromBase64String(itemImageBase64);

                        // Load the byte array into a Texture2D
                        Texture2D texture = new Texture2D(1, 1);
                        texture.LoadImage(imageData);

                        // Create a new Sprite from the Texture2D
                        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

                        // Assign the Sprite to the PCSO's Case.ItemImage
                        loadedPCSO.PCImage = sprite;
                        
                        //loadedPCSO.PCImage = loadedPCSO.Case.ItemImage;
                        // Add the loaded PCSO to the PCData.ComputerItems list
                        PCData.AddPCSOList(loadedPCSO);
                        PCpage.AddAnotherPC();


                        if (loadedPCSO.inUse == true)
                        {
                            UseloadComputer(loadedPCSO);
                            GameManager.instance.pcsothatinUse = documentId;
                        }
                        // Optionally perform any other actions with the loaded PCSO
                    //}
                }

                // Log a message indicating the successful loading of PCSO items
                Debug.Log("PCSO items loaded from Firestore.");
            }
        }


        public async void LoadUpdatePCSOList()
        {
            PCData.ComputerItems.Clear();
            GameManager.instance.pcsoDocumentIds.Clear();
            // Get a reference to the Firestore collection containing the PCSO documents
            CollectionReference collectionRef = FirebaseFirestore.DefaultInstance
                .Collection(GameManager.instance.UserCollection)
                .Document(GameManager.instance.UserID)
                .Collection("ComputersCollection");

            // Fetch all documents from the PCSO collection asynchronously
            QuerySnapshot querySnapshot = await collectionRef.GetSnapshotAsync();

            


            // Iterate through the retrieved documents
            foreach (DocumentSnapshot docSnapshot in querySnapshot.Documents)
            {

                string documentId = docSnapshot.Id;
                GameManager.instance.pcsoDocumentIds.Add(documentId);
                string PCName = docSnapshot.GetValue<string>("PCName");
                int Armor = docSnapshot.GetValue<int>("Armor");
                int AttackDamage = docSnapshot.GetValue<int>("AttackDamage");
                int Health = docSnapshot.GetValue<int>("Health");
                int HealthRegen = docSnapshot.GetValue<int>("HealthRegen");
                int Mana = docSnapshot.GetValue<int>("Mana");
                int ManaRegen = docSnapshot.GetValue<int>("ManaRegen");
                int WalkSpeed = docSnapshot.GetValue<int>("WalkSpeed");
                int CriticalChance = docSnapshot.GetValue<int>("CriticalChance");
                bool InUse = docSnapshot.GetValue<bool>("InUse");
                string itemImageBase64 = docSnapshot.GetValue<string>("ItemImage");

                //if (!string.IsNullOrEmpty(pcsoJson))
                //{
                // Create a new PCSO instance
                PCSO loadedPCSO = ScriptableObject.CreateInstance<PCSO>();

                // Deserialize the JSON data into the PCSO object
                //JsonUtility.FromJsonOverwrite(pcsoJson, loadedPCSO);
                loadedPCSO.PCName = PCName;
                loadedPCSO.Armor = Armor;
                loadedPCSO.AttackDamage = AttackDamage;
                loadedPCSO.Health = Health;
                loadedPCSO.HealthRegen = HealthRegen;
                loadedPCSO.Mana = Mana;
                loadedPCSO.ManaRegen = ManaRegen;
                loadedPCSO.WalkSpeed = WalkSpeed;
                loadedPCSO.CriticalChance = CriticalChance;
                loadedPCSO.inUse = InUse;

                // Find and assign parts by their UniqueID
                if (docSnapshot.ContainsField("Case"))
                {
                    var caseData = docSnapshot.GetValue<Dictionary<string, object>>("Case");
                    string caseUniqueID = caseData["UniqueID"].ToString();
                    loadedPCSO.Case = GameManager.instance.FindPartByUniqueID(caseUniqueID);
                }

                if (docSnapshot.ContainsField("Motherboard"))
                {
                    var motherboardData = docSnapshot.GetValue<Dictionary<string, object>>("Motherboard");
                    string motherboardUniqueID = motherboardData["UniqueID"].ToString();
                    loadedPCSO.Motherboard = GameManager.instance.FindPartByUniqueID(motherboardUniqueID);
                }

                if (docSnapshot.ContainsField("CPU"))
                {
                    var cpuData = docSnapshot.GetValue<Dictionary<string, object>>("CPU");
                    string cpuUniqueID = cpuData["UniqueID"].ToString();
                    loadedPCSO.CPU = GameManager.instance.FindPartByUniqueID(cpuUniqueID);
                }

                if (docSnapshot.ContainsField("CPUFan"))
                {
                    var cpuFanData = docSnapshot.GetValue<Dictionary<string, object>>("CPUFan");
                    string cpuFanUniqueID = cpuFanData["UniqueID"].ToString();
                    loadedPCSO.CPUFan = GameManager.instance.FindPartByUniqueID(cpuFanUniqueID);
                }

                if (docSnapshot.ContainsField("RAM"))
                {
                    var ramData = docSnapshot.GetValue<Dictionary<string, object>>("RAM");
                    string ramUniqueID = ramData["UniqueID"].ToString();
                    loadedPCSO.RAM = GameManager.instance.FindPartByUniqueID(ramUniqueID);
                }

                if (docSnapshot.ContainsField("GPU"))
                {
                    var gpuData = docSnapshot.GetValue<Dictionary<string, object>>("GPU");
                    string gpuUniqueID = gpuData["UniqueID"].ToString();
                    loadedPCSO.GPU = GameManager.instance.FindPartByUniqueID(gpuUniqueID);
                }

                if (docSnapshot.ContainsField("STORAGE"))
                {
                    var storageData = docSnapshot.GetValue<Dictionary<string, object>>("STORAGE");
                    string storageUniqueID = storageData["UniqueID"].ToString();
                    loadedPCSO.STORAGE = GameManager.instance.FindPartByUniqueID(storageUniqueID);
                }

                if (docSnapshot.ContainsField("PSU"))
                {
                    var psuData = docSnapshot.GetValue<Dictionary<string, object>>("PSU");
                    string psuUniqueID = psuData["UniqueID"].ToString();
                    loadedPCSO.PSU = GameManager.instance.FindPartByUniqueID(psuUniqueID);
                }

                byte[] imageData = Convert.FromBase64String(itemImageBase64);

                // Load the byte array into a Texture2D
                Texture2D texture = new Texture2D(1, 1);
                texture.LoadImage(imageData);

                // Create a new Sprite from the Texture2D
                Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

                // Assign the Sprite to the PCSO's Case.ItemImage
                loadedPCSO.PCImage = sprite;
                // Deserialize the PCSO data from the Firestore document
                //string pcsoJson = docSnapshot.GetValue<string>("PC");
                //string itemImageBase64 = docSnapshot.GetValue<string>("ItemImage");

                //if (!string.IsNullOrEmpty(pcsoJson))
                //{
                //    // Create a new PCSO instance
                //    PCSO loadedPCSO = ScriptableObject.CreateInstance<PCSO>();


                //    // Deserialize the JSON data into the PCSO object
                //    JsonUtility.FromJsonOverwrite(pcsoJson, loadedPCSO);

                //    byte[] imageData = Convert.FromBase64String(itemImageBase64);

                //    // Load the byte array into a Texture2D
                //    Texture2D texture = new Texture2D(1, 1);
                //    texture.LoadImage(imageData);

                //    // Create a new Sprite from the Texture2D
                //    Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

                //    // Assign the Sprite to the PCSO's Case.ItemImage
                //    loadedPCSO.PCImage = sprite;

                //    // Add the loaded PCSO to the PCData.ComputerItems list
                PCData.AddPCSOList(loadedPCSO);
                PCpage.AddAnotherPC();

                OnInventOpen();


                if (loadedPCSO.inUse == true)
                {
                    UseloadComputer(loadedPCSO);
                    GameManager.instance.pcsothatinUse = documentId;

                }
                // Optionally perform any other actions with the loaded PCSO
            
        }

            // Log a message indicating the successful loading of PCSO items
            Debug.Log("PCSO items loaded from Firestore.");
        }


        //public async void LoadComputerItems()
        //{
        //    if (GameManager.instance.UserID != "")
        //    {
        //        try
        //        {
        //            DocumentReference docRef = FirebaseFirestore.DefaultInstance.Collection(GameManager.instance.UserCollection).Document(GameManager.instance.UserID);
        //            CollectionReference subDocRef = docRef.Collection("ComputerInventory");
        //            DocumentReference decorDocRef = subDocRef.Document("ComputerInvent");

        //            DocumentSnapshot snapshot = await decorDocRef.GetSnapshotAsync();

        //            if (snapshot.Exists)
        //            {
        //                string jsonData = snapshot.GetValue<string>("PC");
        //                ComputerList loadedData = JsonUtility.FromJson<ComputerList>(jsonData);

        //                if (loadedData != null)
        //                {
        //                    PCData.ComputerItems.Clear();
        //                         foreach (var item in loadedData.Items)
        //                            {

        //                            PCData.ComputerItems.Add(item);
        //                            PCData.AddItem(item);
        //                          }

        //                    Debug.Log("Parts items loaded from Firestore.");
        //                }
        //            }
        //            else
        //            {
        //                Debug.Log("No initial parts items found in Firestore for player.");
        //            }
        //        }
        //        catch (System.Exception ex)
        //        {
        //            Debug.LogError("Failed to load initial parts items from Firestore: " + ex.Message);
        //        }
        //    }
        //}


        public void OnInventOpen()
        {
            
               
                PCpage.ResetSelection();
                PCpage.ClearItems();
                PCpage.InitializedPCs(GetUsedSlotsCount());
            //inventoryData.PartsSaveItems();
                OpenPCInv();


            
        }
        public void Start()
        {
            //LoadPCSOList();
            //StartCoroutine(DelayedComputerLoad());
            PrepareUI();
            //PCData.Initialize();

            //PCeventTrigger1.enabled = false;
            //PCeventTrigger2.enabled = false;


            YesButton.onClick.AddListener(() =>
            {
                //LTA.showBuild();
                InventCon.lastUsedItems.Clear();
                if (GameManager.instance.BeenModified)
                {
                    if (InventCon.recentlyBackedItems.Count > 0)
                    {
                        foreach (var kvp in InventCon.recentlyBackedItems)
                        {
                            InventCon.lastUsedItems[kvp.Key] = kvp.Value;
                            InventCon.inventoryData.RemoveItem(kvp.Value.item.name, 1);
                            //InventCon.inventoryData.RemoveItem(InventCon.lastUsedItems[kvp.Key], 1);
                            //InventCon.totalUsedItemsPrice += kvp.Value.item.Price;
                        }
                        InventCon.recentlyBackedItems.Clear();
                    }
                    //InventCon.OnDoneButtonClick();
                    //InventCon.Success.Stop();
                    //LTA.HideSuccess();

                   
                    
                }
                else
                {
                    InventCon.BackAllCurrentlyItem();
                }


                ModifyPC(PCIndexOutside);
                DialogBox.gameObject.SetActive(false);


            });

            NoButton.onClick.AddListener(() =>
            {
                //LTA.showBuild();
                //LoadMyDeskScene();
                DialogBox.gameObject.SetActive(false);
            });


            //TurnOnButton.onClick.AddListener(() =>
            //{

            //    TurnOnFuntions();
            //});

            //TurnOffButton.onClick.AddListener(() =>
            //{

            //    TurnOffFuntions();
            //});

            //ReturnButton.onClick.AddListener(() =>
            //{
            //    PCeventTrigger1.enabled = false;
            //    PCeventTrigger2.enabled = false;
            //    ReturnPC();
            //});

            //submitButton.onClick.AddListener(() =>
            //{
            //    SubmitPC(PCIndexOutside);
            //});


            //YesButtonForQuit.onClick.AddListener(() =>
            //{
            //    BackAllCurrentlyItem();
            //    DialogBoxForQuit.gameObject.SetActive(false);
            //    Application.Quit();
            //});

            //NoButtonForQuit.onClick.AddListener(() =>
            //{
            //    DialogBoxForQuit.gameObject.SetActive(false);
            //});

        }
        //public void LoadPCItems()
        //{

        //    string savedData = PlayerPrefs.GetString("SavedComputerItems");
        //    ComputerItemList loadedData = JsonUtility.FromJson<ComputerItemList>(savedData);
        //    if (loadedData != null)
        //    {

        //        PCData.ComputerItems.Clear();
        //        foreach (var item in loadedData.PCItems)
        //        {

        //            PCData.ComputerItems.Add(item);
        //            PCData.AddItem(item);
        //        }

        //        Debug.LogWarning("Computers has been Loaded");

        //    }

        //}
        //private void LoadPCSOLists() //need fix
        //{

        //    // Retrieve all PlayerPrefs keys that start with "PCs_"
        //    //string[] allKeys = PlayerPrefs.GetString("PCs", "").Split(',');

        //    //foreach (string key in allKeys)
        //    //{
        //        // Load JSON data for each key
        //        string jsonData = PlayerPrefs.GetString("PCs");

        //        if (!string.IsNullOrEmpty(jsonData))
        //        {
        //        PCData.ComputerItems.Clear();
        //        // Deserialize the JSON data into a PCSO
        //        PCSOs loadedPCSO = ScriptableObject.CreateInstance<PCSOs>();
        //        JsonUtility.FromJsonOverwrite(jsonData, loadedPCSO);
        //        foreach (var item in loadedPCSO.PCs)
        //        {
        //            // Check if the correct PC is being added


        //            PCData.AddPCSOList(item);
        //        }

        //        Debug.LogError("PC has been added");
        //        }
        //    //}
        //}
        public void OpenUsedPCDesc()
        {
            PCDesccription.ShowPCData(UsedPC, ItemPerks(UsedPC));
            PCDesccription.OpenDesc();
        }

        public void JustUpdateClickedPC() {

            Computer PCs = PCData.GetItemAt(PCIndexOutside);
            PCSO pc = PCs.PC;

            PCDesccription.ShowPCData(pc, ItemPerks(pc));
            PCDesccription.OpenDesc();
        }

        public void TurnOnFuntions()
        {
            PCeventTrigger1.enabled = false;
            PCeventTrigger2.enabled = true;
            MonitorScreen.gameObject.SetActive(true);
            TurnONPC(TestedPCint);
            MonitorScreen.sprite = WindowsOSImage;
            //Anim.OpenFirstAnimate();
            Blocker.SetActive(true);
            BlockerText.text = "Computer Turning ON...";
        }
        public void TurnOffFuntions()
        {
            PCeventTrigger1.enabled = true;
            PCeventTrigger2.enabled = false;
            //Anim.TurnOFF();
            MonitorScreen.gameObject.SetActive(false);
            TurnOffButton.gameObject.SetActive(false);
            TurnOnButton.gameObject.SetActive(true);
            Blocker.SetActive(true);
            BlockerText.text = "Computer Turning Off...";
        }
        //public void LoadInitialItems()
        //{

        //    string savedData = PlayerPrefs.GetString("SavedUniqueIndex");
        //    UniqueIndexes loadedData = JsonUtility.FromJson<UniqueIndexes>(savedData);
        //    if (loadedData != null)
        //    {

        //        foreach (var index in loadedData.unique)
        //        {
        //           InventCon.UniqueIndex.Add(index);
        //        }
        //    }

        //}
        //private void LoadPCSOList()
        //{
        //    // Retrieve all PlayerPrefs keys that start with "PCs_"
        //    //LoadInitialItems();
        //    PCData.ComputerItems.Clear();
        //    string[] allKeys = PlayerPrefs.GetString("PCs_Keys", "").Split(',');
        //    Debug.Log(allKeys.Length);
        //    foreach (string key in allKeys)
        //    {
        //        // Load JSON data for each key
        //        string jsonData = PlayerPrefs.GetString(key, "");

        //        if (!string.IsNullOrEmpty(jsonData))
        //        {
        //            // Deserialize the JSON data into a PCSO
        //            PCSO loadedPCSO = ScriptableObject.CreateInstance<PCSO>();
        //            JsonUtility.FromJsonOverwrite(jsonData, loadedPCSO);
        //            PCData.AddPCSOList(loadedPCSO);
        //        }
        //    }
        //}
        //private void RemovePCSOFromSave(int PCIndex)
        //{
        //    // Retrieve all PlayerPrefs keys that start with "PCs_"

        //    int uniquekeys = InventCon.UniqueIndex[PCIndex];


        //    string[] allKeys = PlayerPrefs.GetString("PCs_Keys", "").Split(',');

        //    // Get the key associated with the provided PCIndex
        //    string keyToRemove = $"PCs_{uniquekeys}";

        //    // Remove the corresponding key from PlayerPrefs
        //    PlayerPrefs.DeleteKey(keyToRemove);

        //    // Remove the key from the list of saved keys
        //    List<string> updatedKeys = new List<string>(allKeys);
        //    updatedKeys.Remove(keyToRemove);

        //    // Reconstruct the keys string without the removed key
        //    string updatedKeysString = string.Join(",", updatedKeys.ToArray());

        //    // Save the updated keys string back to PlayerPrefs
        //    PlayerPrefs.SetString("PCs_Keys", updatedKeysString);
        //}

        public int GetUsedSlotsCount()//this will only used the slots with items
        {
            int usedSlots = 0;
            foreach (var item in PCData.ComputerItems)
            {
                if (!item.isEmpty)
                {
                    usedSlots++;
                }
            }
            return usedSlots;
        }
        //public OrdersController OC;
        public PCDesc PCdesc;
        //public void SubmitPC(int PCindex)
        //{
        //    //PCIndexOutside
        //    Computer PCs = PCData.GetItemAt(PCindex);
        //    var mission = OrderData.GetItemAt(OC.selectedMissionIndex);

        //    if (PCs.PC.TestStatus == "Untested")
        //    {
        //        DialogBox.gameObject.SetActive(true);
        //        DialogText.text = "Can't Submit Untested Computers \n Test the Selected Computer First...";
        //        YesButton.gameObject.SetActive(false);
        //        NoButton.gameObject.SetActive(false);
        //        exitButton.gameObject.SetActive(true);


        //    }
        //    else
        //    {
        //        SubmitSound.Play();
        //        CongratsPanel.gameObject.SetActive(true);
        //        LTA.ShowSubmitPC();

        //        SubmittedPCImage.sprite = PCs.PC.PCImage;
        //        SubmittedPCName.text = PCs.PC.PCName;

        //        Price.text = "$" + mission.orders.OrderPrice.ToString();
        //        ClientImage.sprite = mission.orders.ClientImage;
        //        ClientName.text = mission.orders.ClientName;

        //        ClearPCs(PCindex);
        //        PCMenu.Hide();
        //        OC.MissionPrice(OC.selectedMissionIndex);
        //        OC.RespawnMission(OC.selectedMissionIndex, OC.selectedMissionIndex + 1);
        //       // Debug.LogError(GameManager.Instance.PCMoney);
        //        ReturnPC();
        //    }
        //    OC.selectedMissionIndex = -1;
        //}
        //public void SubmitPC(int PCindex)
        //{
        //    Computer PCs = PCData.GetItemAt(PCindex);
        //    var mission = OrderData.GetItemAt(OC.selectedMissionIndex);

        //    if (PCs.PC.TestStatus == "Untested")
        //    {
        //        // Untested PC, show error message
        //        DialogBox.gameObject.SetActive(true);
        //        DialogText.text = "Can't Submit Untested Computers \n Test the Selected Computer First...";
        //        YesButton.gameObject.SetActive(false);
        //        NoButton.gameObject.SetActive(false);
        //        exitButton.gameObject.SetActive(true);
        //    }
        //    else
        //    {
        //        SubmitSound.Play();
        //        CongratsPanel.gameObject.SetActive(true);
        //        LTA.ShowSubmitPC();

        //        SubmittedPCImage.sprite = PCs.PC.PCImage;
        //        SubmittedPCName.text = PCs.PC.PCName;
        //        SubmittedPCPrice.text = "$"+PCs.PC.PCPrice.ToString();



        //        ClientImage.sprite = mission.orders.ClientImage;
        //        ClientName.text = mission.orders.ClientName;
        //        ExP.text = "+" + mission.orders.EXP.ToString();

        //        ClearPCs(PCindex);
        //        PCMenu.Hide();

        //        // Check the submitted PC parts against mission requirements
        //        List<ItemSO> pcParts = new List<ItemSO>
        //        {
        //            PCs.PC.Case,
        //            PCs.PC.Motherboard,
        //            PCs.PC.CPU,
        //            PCs.PC.CPUFan,
        //            PCs.PC.RAM,
        //            PCs.PC.GPU,
        //            PCs.PC.STORAGE,
        //            PCs.PC.PSU
        //        };


        //        // Calculate the deduction percentage per mismatched part
        //        const float deductionPercentage = 0.1f; // 10% deduction per mismatched part

        //        // Count the number of mismatched parts
        //        int mismatchCount = 0;
        //        float deduction = 0;

        //        // Iterate through the mission requirements and the submitted PC parts
        //        foreach (ItemSO requiredItem in mission.orders.Requirements)
        //        {
        //            //bool isPartFound = pcParts.Any(part => part.Name == requiredItem.Name);

        //            //if (!isPartFound)
        //            //{
        //            //    mismatchCount++;
        //            //    var mismatchedPart = pcParts.FirstOrDefault(part => part.Category == requiredItem.Category);
        //            //    if (mismatchedPart != null)
        //            //    {
        //            //        Debug.LogError($"Required Item: {requiredItem.Name}, Part in PC: {mismatchedPart.Name}");
        //            //    }
        //            //}
        //            //else
        //            //{

        //            //}

        //            bool isPartFound = pcParts.Any(part => part.Name == requiredItem.Name);

        //            if (!isPartFound)
        //            {
        //                var mismatchedPart = pcParts.FirstOrDefault(part => part.Category == requiredItem.Category);
        //                if (mismatchedPart != null)
        //                {
        //                    //float deduction = mission.orders.OrderPrice * deductionPercentage * mismatchCount;
        //                    float partDeduction = deductionPercentage * mission.orders.OrderPrice;
        //                    displayText.text += $"{requiredItem.Name} |==|  {mismatchedPart.Name} \n<color=red>-${partDeduction}</color>\n";
        //                    mismatchCount++;
        //                    deduction += partDeduction;
        //                }
        //            }
        //            else
        //            {
        //                displayText.text += $"{requiredItem.Name} |==| {requiredItem.Name}\n";
        //            }
        //        }

        //        // Calculate the final amount to be received by the player
        //        float finalAmount = mission.orders.OrderPrice - deduction;


        //        if (mismatchCount == 0)
        //        {
        //            displayText2.text = "- Congrats, Client Requirements are all met -";
        //            displayText3.text = "$" + mission.orders.OrderPrice + " = " + "$" +finalAmount;
        //        }
        //        else if (mismatchCount < mission.orders.Requirements.Count)
        //        {
        //            displayText2.text = "- You didn't meet some of the Client Requirements -";
        //            displayText3.text = "$" + mission.orders.OrderPrice + " - " + "$" + deduction + " = " + "$" + finalAmount;
        //        }
        //        else
        //        {
        //            displayText2.text = "- You didn't meet any of The Client Requirements -";
        //            displayText3.text = "$" + mission.orders.OrderPrice + " - " + "$" + deduction + " = " + "$" + finalAmount;
        //        }

        //        // If there were mismatches, adjust the price accordingly
        //        if (mismatchCount > 0)
        //        {
        //            GameManager.Instance.PCMoney += Convert.ToInt32(finalAmount);
        //            GameManager.Instance.SavePCMoney();
        //        }
        //        else
        //        {
        //            // No mismatches, claim the full price
        //            GameManager.Instance.PCMoney += Convert.ToInt32(mission.orders.OrderPrice);
        //            GameManager.Instance.SavePCMoney();
        //        }

        //        // Update the PC money UI
        //        GameManager.Instance.UpdatePCMoneyText();
        //        GameManager.Instance.experience += mission.orders.EXP;
        //        Price.text = "$" + finalAmount.ToString();
        //        // Respawn a new mission
        //        //OC.RespawnMission(OC.selectedMissionIndex, OC.selectedMissionIndex + 1);
        //        OC.remainingTimes[OC.selectedMissionIndex] = 0;

        //        ReturnPC();
        //        GameManager.Instance.SaveData();
        //    }
        //    OC.selectedMissionIndex = -1;
        //}

        //public PCSO LoadPCSO()
        //{
        //    string pcDataJson = PlayerPrefs.GetString("SavedPCSO");
        //    PCSO loadedPCSO = JsonUtility.FromJson<PCSO>(pcDataJson);
        //    return loadedPCSO;
        //}

        private void PrepareUI()
        {
            PCpage.InitializedPCs(GetUsedSlotsCount());
            this.PCpage.OnDescriptionRequested += HandleDescriptionRequest;
            //this.PCpage.OnItemActionRequested += HandleRightActionRequest;

            //BuildButton.onClick.AddListener(LoadMyDeskScene);
            //ShopButton.onClick.AddListener(LoadMyShopScene);

            //PCsButton.onClick.AddListener(() =>
            //{
            //    //OC.SubmitToName.text = "Computers";
            //    SubmitPanel.SetActive(false);
            //    TnMPanel.SetActive(true);
            //    OpenPCInv();
            //    InfoButton.gameObject.SetActive(true);
            //});


            //XButton.onClick.AddListener(() => {
            //    PCMenu.Hide();
            //    //OC.selectedMissionIndex = -1;
            //    //Anim.ShowAllAnimation();
            //});
            //ModifyButton.onClick.AddListener(() => {

            //    HandleRightActionRequest(PCIndexOutside);


            //    });
            UseButton.onClick.AddListener(() => UseComputer(PCIndexOutside));
            ModifyButton.onClick.AddListener(() => ModifyComputer(PCIndexOutside));
            sellButton.onClick.AddListener(() => SellPC(PCIndexOutside));



            //OrderButton.onClick.AddListener(() => Anim.HideAllAnimation());
        }
        private int TestedPCint;

        public void UseloadComputer(PCSO PCitem)
        {
            
            theClickable.gameObject.SetActive(true);

            UsedPC = PCitem;
            GameManager.instance.GetPCStats(PCitem.AttackDamage, PCitem.Health, PCitem.Mana, PCitem.HealthRegen, PCitem.WalkSpeed, PCitem.Armor, PCitem.ManaRegen, PCitem.CriticalChance);
            GameManager.instance.StatsUsedPCPanel.SetActive(true);
            GameManager.instance.StatsPCImageUsed.sprite = PCitem.PCImage;
            GameManager.instance.StatsPCName.text = PCitem.PCName;
            
            PCImagePlaceholder.gameObject.SetActive(true);
            PCImagePlaceholder.sprite = PCitem.PCImage;
        }

        

        public async void UseComputer(int index)
        {
            if (GameManager.instance.PlayerDeskUI.activeSelf) {
                PCMenu.Hide();

                if(GameManager.instance.OnGoToDeskQuest == true)
                {
                    GameManager.instance.ComputerPlaced = true;
                }
                //TestingComputerPanel.SetActive(true);
                //EmptyComputerPanel.SetActive(false);

                Computer PCs = PCData.GetItemAt(index);
                PCSO PCitem = PCs.PC;
                string documentId = GameManager.instance.pcsoDocumentIds[index];

                theClickable.gameObject.SetActive(true);
                UsedPC = PCitem;
                GameManager.instance.GetPCStats(PCitem.AttackDamage, PCitem.Health, PCitem.Mana, PCitem.HealthRegen, PCitem.WalkSpeed, PCitem.Armor, PCitem.ManaRegen, PCitem.CriticalChance);
                GameManager.instance.StatsUsedPCPanel.SetActive(true);
                GameManager.instance.StatsPCImageUsed.sprite = PCitem.PCImage;
                GameManager.instance.StatsPCName.text = PCitem.PCName;
               
                //PCPrice.text = "$" + PCitem.PCPrice.ToString() + ".00";


                //PCImage.gameObject.SetActive(true);
                //PCImage.sprite = PCitem.Case.ItemImage;
                //PCName.text = PCitem.PCName;
                //Perks.text = ItemPerks(PCitem);
                //CaseName.text = PCitem.Case.Name;
                //MBName.text = PCitem.Motherboard.Name;
                //CPUName.text = PCitem.CPU.Name;
                //CPUFName.text = PCitem.CPUFan.Name;
                //RAMName.text = PCitem.RAM.Name;
                //GPUName.text = PCitem.GPU.Name;
                //STRGName.text = PCitem.STORAGE.Name;
                //PSUName.text = PCitem.PSU.Name;

                ////Status.text = PCitem.TestStatus;


                //CaseImage.sprite = PCitem.Case.ItemImage;


                //MBImage.sprite = PCitem.Motherboard.ItemImage;


                //CPUImage.sprite = PCitem.CPU.ItemImage;


                //CPUFImage.sprite = PCitem.CPUFan.ItemImage;


                //RAMImage.sprite = PCitem.RAM.ItemImage;


                //GPUImage.sprite = PCitem.GPU.ItemImage;


                //STRGImage.sprite = PCitem.STORAGE.ItemImage;


                //PSUImage.sprite = PCitem.PSU.ItemImage;

                PCImagePlaceholder.gameObject.SetActive(true);
                PCImagePlaceholder.sprite = PCitem.PCImage;

                PCitem.inUse = true;

                TestedPCint = index;

                

                await UpdatePCSO(documentId, PCitem);
                GameManager.instance.PlayerArmorScript.currentArmor = (int)GameManager.instance.PlayerTotalArmor;
                GameManager.instance.PlayerHealthScript.currentHealth = (int)GameManager.instance.PlayerTotalHealth;

            }
            else
            {
                DialogBox.gameObject.SetActive(true);
                DialogText.text = "Can't Use this Computer, \n Go to Your Desk First...";
                YesButton.gameObject.SetActive(false);
                NoButton.gameObject.SetActive(false);
                exitButton.gameObject.SetActive(true);
            }
            
            //PCeventTrigger1.enabled = true;
            //PCeventTrigger2.enabled = false;
            //MonitorScreen.gameObject.SetActive(false);
            //TurnOffButton.gameObject.SetActive(false);
            //TurnOnButton.gameObject.SetActive(true);
            //PCeventTrigger1.enabled = true;

            

            



        }

        public async Task UpdatePCSO(string documentId, PCSO updatedPCSO)
        {
            try
            {
                // Convert the updated PCSO object to JSON
                //string updatedPCSOJson = JsonUtility.ToJson(updatedPCSO);
                //pcsothatinUse = documentId;

                if (!string.IsNullOrEmpty(GameManager.instance.pcsothatinUse) && GameManager.instance.pcsothatinUse != documentId)
                {
                    // Update the PCSO that was previously in use to set inUse = false
                    await UpdatePCSOInUseStatus(GameManager.instance.pcsothatinUse, false);
                }
                // Prepare a dictionary to store the updated PCSO data
                Dictionary<string, object> updateData = new Dictionary<string, object>
        {
            { "PCName", updatedPCSO.PCName },
            { "Armor", updatedPCSO.Armor },
            { "AttackDamage", updatedPCSO.AttackDamage },
            { "Health", updatedPCSO.Health },
            { "HealthRegen", updatedPCSO.HealthRegen },
            { "Mana", updatedPCSO.Mana },
            { "ManaRegen", updatedPCSO.ManaRegen },
            { "WalkSpeed", updatedPCSO.WalkSpeed },
            { "CriticalChance", updatedPCSO.CriticalChance },
            { "InUse", updatedPCSO.inUse },
            { "ItemImage", Convert.ToBase64String(updatedPCSO.PCImage.texture.EncodeToPNG()) },  // Convert image to base64
            { "Case", GameManager.instance.ConvertPartToDictionary(updatedPCSO.Case) },
            { "Motherboard", GameManager.instance.ConvertPartToDictionary(updatedPCSO.Motherboard) },
            { "CPU", GameManager.instance.ConvertPartToDictionary(updatedPCSO.CPU) },
            { "CPUFan", GameManager.instance.ConvertPartToDictionary(updatedPCSO.CPUFan) },
            { "RAM", GameManager.instance.ConvertPartToDictionary(updatedPCSO.RAM) },
            { "GPU", GameManager.instance.ConvertPartToDictionary(updatedPCSO.GPU) },
            { "STORAGE", GameManager.instance.ConvertPartToDictionary(updatedPCSO.STORAGE) },
            { "PSU", GameManager.instance.ConvertPartToDictionary(updatedPCSO.PSU) },
        };


                // Get a reference to the Firestore document to be updated
                DocumentReference docRef = FirebaseFirestore.DefaultInstance
                    .Collection(GameManager.instance.UserCollection)
                    .Document(GameManager.instance.UserID)
                    .Collection("ComputersCollection")
                    .Document(documentId);

        //        // Create a dictionary to store the updated PCSO data
        //        Dictionary<string, object> updateData = new Dictionary<string, object>
        //{
        //    { "PC", updatedPCSOJson }
        //};

                // Update the Firestore document with the new data
                await docRef.UpdateAsync(updateData);
                GameManager.instance.pcsothatinUse = documentId;
                Debug.Log("PCSO document updated successfully.");
            }
            catch (Exception ex)
            {
                Debug.LogError("Error updating PCSO document: " + ex.Message);
            }
        }

        //private async Task UpdatePCSOInUseStatus(string pcsothatinUse, bool inUseStatus)
        //{
        //    try
        //    {
        //        // Get a reference to the Firestore document to be updated
        //        DocumentReference docRef = FirebaseFirestore.DefaultInstance
        //            .Collection(GameManager.instance.UserCollection)
        //            .Document(GameManager.instance.UserID)
        //            .Collection("ComputersCollection")
        //            .Document(pcsothatinUse);

        //        // Fetch the document snapshot
        //        DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

        //        // Check if the document exists
        //        if (snapshot.Exists)
        //        {
        //            // Deserialize the PCSO data from the Firestore document
        //            string pcsoJson = snapshot.GetValue<string>("PC");

        //            if (!string.IsNullOrEmpty(pcsoJson))
        //            {
        //                // Deserialize the JSON data into a PCSO object
        //                //PCSO loadedPCSO = JsonUtility.FromJson<PCSO>(pcsoJson);

        //                PCSO loadedPCSO = ScriptableObject.CreateInstance<PCSO>();

        //                // Deserialize the JSON data into the PCSO object
        //                JsonUtility.FromJsonOverwrite(pcsoJson, loadedPCSO);

        //                // Update the inUse status
        //                loadedPCSO.inUse = inUseStatus;

        //                // Convert the updated PCSO object back to JSON
        //                string updatedPCSOJson = JsonUtility.ToJson(loadedPCSO);

        //                // Create a dictionary to update the inUse status
        //                Dictionary<string, object> updateData = new Dictionary<string, object>
        //        {
        //            { "PC", updatedPCSOJson }
        //        };

        //                // Update the Firestore document with the new inUse status
        //                await docRef.UpdateAsync(updateData);

        //                Debug.Log("PCSO inUse status updated successfully.");


        //            }
        //            else
        //            {
        //                Debug.LogWarning("PCSO JSON data is empty or invalid.");
        //            }
        //        }
        //        else
        //        {
        //            Debug.LogWarning("PCSO document does not exist.");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.LogError("Error updating PCSO inUse status: " + ex.Message);
        //    }
        //}

        private async Task UpdatePCSOInUseStatus(string pcsothatinUse, bool inUseStatus)
        {
            try
            {
                // Get a reference to the Firestore document to be updated
                DocumentReference docRef = FirebaseFirestore.DefaultInstance
                    .Collection(GameManager.instance.UserCollection)
                    .Document(GameManager.instance.UserID)
                    .Collection("ComputersCollection")
                    .Document(pcsothatinUse);

                // Fetch the document snapshot
                DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

                // Check if the document exists
                if (snapshot.Exists)
                {
                    // Instead of deserializing the entire object, update the inUse status directly
                    Dictionary<string, object> updateData = new Dictionary<string, object>
            {
                { "InUse", inUseStatus }
            };

                    // Update the Firestore document with the new inUse status
                    await docRef.UpdateAsync(updateData);

                    Debug.Log("PCSO inUse status updated successfully.");
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




        public async Task ModifyPCSOs(string pcsothatisModified, PCSO updatedPCSO)
        {
            try
            {
                // Convert the updated PCSO object to JSON
                //string updatedPCSOJson = JsonUtility.ToJson(pc);
                //pcsothatinUse = documentId;
                // Prepare a dictionary to store the updated PCSO data
                Dictionary<string, object> updateData = new Dictionary<string, object>
        {
            { "PCName", updatedPCSO.PCName },
            { "Armor", updatedPCSO.Armor },
            { "AttackDamage", updatedPCSO.AttackDamage },
            { "Health", updatedPCSO.Health },
            { "HealthRegen", updatedPCSO.HealthRegen },
            { "Mana", updatedPCSO.Mana },
            { "ManaRegen", updatedPCSO.ManaRegen },
            { "WalkSpeed", updatedPCSO.WalkSpeed },
            { "CriticalChance", updatedPCSO.CriticalChance },
            //{ "InUse", updatedPCSO.inUse },
            { "ItemImage", Convert.ToBase64String(updatedPCSO.PCImage.texture.EncodeToPNG()) },  // Convert image to base64
            { "Case", GameManager.instance.ConvertPartToDictionary(updatedPCSO.Case) },
            { "Motherboard", GameManager.instance.ConvertPartToDictionary(updatedPCSO.Motherboard) },
            { "CPU", GameManager.instance.ConvertPartToDictionary(updatedPCSO.CPU) },
            { "CPUFan", GameManager.instance.ConvertPartToDictionary(updatedPCSO.CPUFan) },
            { "RAM", GameManager.instance.ConvertPartToDictionary(updatedPCSO.RAM) },
            { "GPU", GameManager.instance.ConvertPartToDictionary(updatedPCSO.GPU) },
            { "STORAGE", GameManager.instance.ConvertPartToDictionary(updatedPCSO.STORAGE) },
            { "PSU", GameManager.instance.ConvertPartToDictionary(updatedPCSO.PSU) },
        };


                // Get a reference to the Firestore document to be updated
                DocumentReference docRef = FirebaseFirestore.DefaultInstance
                    .Collection(GameManager.instance.UserCollection)
                    .Document(GameManager.instance.UserID)
                    .Collection("ComputersCollection")
                    .Document(pcsothatisModified);

                //        // Create a dictionary to store the updated PCSO data
                //        Dictionary<string, object> updateData = new Dictionary<string, object>
                //{
                //    { "PC", updatedPCSOJson }
                //};

                if(updatedPCSO.inUse == true)
                {
                    UseloadComputer(updatedPCSO);
                    updatedPCSO.inUse = true;
                    GameManager.instance.pcsothatinUse = pcsothatisModified;
                }


                // Update the Firestore document with the new data
                await docRef.UpdateAsync(updateData);
                Debug.Log("PCSO document updated successfully.");
            }
            catch (Exception ex)
            {
                Debug.LogError("Error updating PCSO document: " + ex.Message);
            }
        }


        public async Task ModifyPCName(string pcsothatisModified, string name)
        {
            try
            {
                
                //pcsothatinUse = documentId;
                // Prepare a dictionary to store the updated PCSO data
                Dictionary<string, object> updateData = new Dictionary<string, object>
        {
              { "PCName", name },
            
        };


                // Get a reference to the Firestore document to be updated
                DocumentReference docRef = FirebaseFirestore.DefaultInstance
                    .Collection(GameManager.instance.UserCollection)
                    .Document(GameManager.instance.UserID)
                    .Collection("ComputersCollection")
                    .Document(pcsothatisModified);

                //        // Create a dictionary to store the updated PCSO data
                //        Dictionary<string, object> updateData = new Dictionary<string, object>
                //{
                //    { "PC", updatedPCSOJson }
                //};

                // Update the Firestore document with the new data
                await docRef.UpdateAsync(updateData);
                Debug.Log("PCSO document updated successfully.");
            }
            catch (Exception ex)
            {
                Debug.LogError("Error updating PCSO inUse status: " + ex.Message);
            }
        }

        public async void DeletePC(string documentID)
        {
            await DeletePCDocument(documentID);
        }
        public async Task DeletePCDocument(string documentId)
        {
            try
            {
                // Get a reference to the Firestore document to be deleted
                DocumentReference docRef = FirebaseFirestore.DefaultInstance
                    .Collection(GameManager.instance.UserCollection)
                    .Document(GameManager.instance.UserID)
                    .Collection("ComputersCollection")
                    .Document(documentId);

                // Delete the Firestore document
                await docRef.DeleteAsync();

                Debug.Log("PCSO document deleted successfully.");
            }
            catch (Exception ex)
            {
                Debug.LogError("Error deleting PCSO document: " + ex.Message);
            }
        }

        public void ReturnPC()
        {
           
            TestingComputerPanel.SetActive(false);
            EmptyComputerPanel.SetActive(true);
            PCImagePlaceholder.gameObject.SetActive(false);
            //Anim.HideAllAnimation();
            MonitorScreen.gameObject.SetActive(false);
       
            TurnOffButton.gameObject.SetActive(false);
            TurnOnButton.gameObject.SetActive(true);
        }

        public void TurnONPC(int index)
        {

           
            Computer PCs = PCData.GetItemAt(index);
            PCSO PCitem = PCs.PC;
            ClearPCs(index);

            PCSO pcso = ScriptableObject.CreateInstance<PCSO>();

            pcso.name = PCitem.PCName;
            pcso.PCName = PCitem.PCName;
            pcso.PCImage = PCitem.Case.ItemImage;
            //pcso.PCPrice = PCitem.PCPrice;
            pcso.Case = PCitem.Case;
            pcso.Motherboard = PCitem.Motherboard;
            pcso.CPU = PCitem.CPU;
            pcso.CPUFan = PCitem.CPUFan;
            pcso.RAM = PCitem.RAM;
            pcso.GPU = PCitem.GPU;
            pcso.STORAGE = PCitem.STORAGE;
            pcso.PSU = PCitem.PSU;
            //pcso.TestStatus = "Tested";

            

           // InventCon.SavePCSOData(pcso);
           // GameManager.Instance.SaveUniqueIndex(InventCon.UniqueIndex);
            PCData.AddPCSOList(pcso);
            PCpage.AddAnotherPC();

            //TestingComputerPanel.SetActive(true);
            //EmptyComputerPanel.SetActive(false);
            //Status.text = "Tested";
            //TestComputer(index);

            TurnOnButton.gameObject.SetActive(false);
            TurnOffButton.gameObject.SetActive(true);

        }
        public void ClearPCs(int index)
        {
            // Destroy all instantiated items in the list
            foreach (PCItem item in PCpage.ListOfPCs)
            {
                Destroy(item.gameObject);
            }

            PCpage.ListOfPCs.Clear();

            PCData.RemoveComputer(index);
            //Debug.LogError(PCData.ComputerItems.Count);
            //Debug.LogError("Removed");

            PCpage.InitializedPCs(GetUsedSlotsCount());
            foreach (var item in PCData.GetCurrentInventoryState())
            {
                PCpage.UpdateData(item.Key, item.Value.PC.Case.ItemImage, item.Value.PC.PCName, item.Value.PC.inUse);
                //Debug.LogError("Added");
            }
           
             //RemovePCSOFromSave(index);
            // InventCon.UniqueIndex.RemoveAt(index);
             //PlayerPrefs.DeleteKey("SavedUniqueIndex");
            // GameManager.Instance.SaveUniqueIndex(InventCon.UniqueIndex);

          


        }
        public void ModifyPC(int PCIndex)
        {
            //Debug.LogError(PCIndex);
            //LoadMyDeskScene();

            GameManager.instance.BeenModified = true;
            Computer PCs = PCData.GetItemAt(PCIndex);
            GameManager.instance.pcsothatisModified = GameManager.instance.pcsoDocumentIds[PCIndex];
           

            InventCon.usedItems.Clear();//clear first all the lastusedItems
            InventCon.lastUsedItems.Clear();
            //ClearPCs(PCIndex);
            //InventCon.NameText.text = PCs.PC.PCName;
            //InventCon.RenameTxt.text = PCs.PC.PCName;
           // InventCon.PriceText.text = "$" + PCs.PC.PCPrice.ToString("F2");
             InventCon.ModifyingPC.gameObject.SetActive(true);
             InventCon.pcName.gameObject.SetActive(true);
             InventCon.newpc.gameObject.SetActive(true);


            string[] categories = new string[]
            {
                "Case",
                "Motherboard",
                "CPU",
                "CPU Fan",
                "RAM",
                "Video Card",
                "Storage",
                "PSU"

            };

            foreach (string category in categories)
            {
                InventoryItem PCparts = new InventoryItem();
                switch (category)
                {
                    case "Case":
                        PCparts.item = PCs.PC.Case;
                        break;
                    case "Motherboard":
                        PCparts.item = PCs.PC.Motherboard;
                        break;
                    case "CPU":
                        PCparts.item = PCs.PC.CPU;
                        break;
                    case "CPU Fan":
                        PCparts.item = PCs.PC.CPUFan;
                        break;
                    case "RAM":
                        PCparts.item = PCs.PC.RAM;
                        break;
                    case "Video Card":
                        PCparts.item = PCs.PC.GPU;
                        break;
                    case "Storage":
                        PCparts.item = PCs.PC.STORAGE;
                        break;

                    case "PSU":
                        PCparts.item = PCs.PC.PSU;
                        break;
                }

               

                InventCon.UseItems(PCparts, category);
                InventCon.usedItems.Add(PCparts);
                InventCon.lastUsedItems[category] = PCparts;

            }
            InventCon.RenameTxt.text = PCs.PC.PCName;
            InventCon.PCNameTxt.text = PCs.PC.PCName;
            InventCon.pcname = PCs.PC.PCName;
            InventCon.DisassmebleButton.gameObject.SetActive(true);
            if (PCs.PC.inUse == true)
            {
                InventCon.Inuse.gameObject.SetActive(true);
            }
            else
            {
                InventCon.Inuse.gameObject.SetActive(false);
            }

        }

        

        public void ModifyComputer(int PCIndex)
        {

            if (InventCon.lastUsedItems.Count > 0)
            {
                DialogBox.gameObject.SetActive(true);
                YesButton.gameObject.SetActive(true);
                NoButton.gameObject.SetActive(true);
                exitButton.gameObject.SetActive(false);
                // List has values
               

                if (GameManager.instance.BeenModified) {
                    DialogText.text = "You're currently Modifying a computer... \n Would you like to alter this computer? \n If Yes, The currently in modifying computer will be back to the Computer's Inventory.";
                }
                else
                {
                    DialogText.text = "You're currently building a computer... \n Would you like to alter this computer? \n If Yes, You will find your ongoing build parts back in your inventory.";
                }
               
                //DialogBox.gameObject.SetActive(false);
            }
            else
            {
                //LTA.showBuild();
                ModifyPC(PCIndex);
                
                
            }
            PCIndexOutside = PCIndex;
            //ReturnPC();
        }
        private int PCIndexOutside = 0;
        private void HandleDescriptionRequest(int PCindex)
        {
            Computer PCs = PCData.GetItemAt(PCindex);
            if (PCs.isEmpty) { return; }
            PCSO PCitems = PCs.PC;

            PCpage.UpdateDescription(PCindex, PCitems.Case.ItemImage, PCitems.Case.ItemImage, PCitems.Motherboard.ItemImage, PCitems.CPU.ItemImage, PCitems.CPUFan.ItemImage, PCitems.RAM.ItemImage, PCitems.GPU.ItemImage, PCitems.STORAGE.ItemImage, PCitems.PSU.ItemImage,
            PCitems.PCName, PCitems.Case.Name, PCitems.Motherboard.Name, PCitems.CPU.Name, PCitems.CPUFan.Name, PCitems.RAM.Name, PCitems.GPU.Name, PCitems.STORAGE.Name, PCitems.PSU.Name, PCitems.inUse, ItemPerks(PCitems),
            PCitems.Case.rarity, PCitems.Motherboard.rarity, PCitems.CPU.rarity, PCitems.CPUFan.rarity, PCitems.RAM.rarity, PCitems.GPU.rarity, PCitems.STORAGE.rarity, PCitems.PSU.rarity);
            PCIndexOutside = PCindex;
        }


        private string ItemPerks(PCSO item)
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
            if (item.ManaRegen != 0)
            {
                perks += "Mana Regen +" + item.ManaRegen + "\n";
            }
            if (item.CriticalChance != 0)
            {
                perks += "Critical Chance +" + item.CriticalChance + "\n";
            }

            return perks;
        }

        public void SellPC(int index)
        {
            Computer PCs = PCData.GetItemAt(index);
            PCSO PCitem = PCs.PC;
            string documentId = GameManager.instance.pcsoDocumentIds[index];


            if(PCitem.inUse == true)
            {
                GameManager.instance.ShowFloatingText("You Can't Sell Used PC");
                Debug.LogError("can't sell use pc");
            }
            else
            {
                // Get the current mission's requirements
                Missions currentMission = GameManager.instance.Buyers.SelectedMission;
                int currentMissionIndex = GameManager.instance.Buyers.selectedMissionIndex;
                bool isMissionMet = true;
                float deductionPercentage = 0.1f; // 10% deduction per mismatched part
                float totalDeduction = 0;
                float totalEXPDeduction = 0;

                string comparisonText = "";

                // Check the PC against the mission requirements
                //if (PCitem.Case.CaseStrength < currentMission.orders.CaseStrength)
                //{
                //    isMissionMet = false;
                //    totalDeduction += deductionPercentage * currentMission.orders.OrderPrice;
                //    totalEXPDeduction += deductionPercentage * currentMission.orders.EXP;
                //}

                //if (PCitem.Motherboard.MotherboardStrength < currentMission.orders.MotherboardStrength)
                //{
                //    isMissionMet = false;
                //    totalDeduction += deductionPercentage * currentMission.orders.OrderPrice;
                //    totalEXPDeduction += deductionPercentage * currentMission.orders.EXP;
                //}

                //if (PCitem.CPU.BaseSpeed < currentMission.orders.CPUBaseSpeed)
                //{
                //    isMissionMet = false;
                //    totalDeduction += deductionPercentage * currentMission.orders.OrderPrice;
                //    totalEXPDeduction += deductionPercentage * currentMission.orders.EXP;
                //}

                //if (PCitem.RAM.Memory < currentMission.orders.RAMMemory)
                //{
                //    isMissionMet = false;
                //    totalDeduction += deductionPercentage * currentMission.orders.OrderPrice;
                //    totalEXPDeduction += deductionPercentage * currentMission.orders.EXP;
                //}

                //if (PCitem.CPUFan.CoolingPower < currentMission.orders.CPUFanCoolingPower)
                //{
                //    isMissionMet = false;
                //    totalDeduction += deductionPercentage * currentMission.orders.OrderPrice;
                //    totalEXPDeduction += deductionPercentage * currentMission.orders.EXP;
                //}

                //if (PCitem.GPU.ClockSpeed < currentMission.orders.GPUClockSpeed)
                //{
                //    isMissionMet = false;
                //    totalDeduction += deductionPercentage * currentMission.orders.OrderPrice;
                //    totalEXPDeduction += deductionPercentage * currentMission.orders.EXP;
                //}

                //if (PCitem.STORAGE.Storage < currentMission.orders.Storage)
                //{
                //    isMissionMet = false;
                //    totalDeduction += deductionPercentage * currentMission.orders.OrderPrice;
                //    totalEXPDeduction += deductionPercentage * currentMission.orders.EXP;
                //}

                //if (PCitem.PSU.WattagePower < currentMission.orders.PSUWattagePower)
                //{
                //    isMissionMet = false;
                //    totalDeduction += deductionPercentage * currentMission.orders.OrderPrice;
                //    totalEXPDeduction += deductionPercentage * currentMission.orders.EXP;
                //}

                if (currentMission.orders.CaseStrength > 0) // Only show if requirement > 0
                {
                    if (PCitem.Case.CaseStrength < currentMission.orders.CaseStrength)
                    {
                        isMissionMet = false;
                        totalDeduction += deductionPercentage * currentMission.orders.OrderPrice;
                        totalEXPDeduction += deductionPercentage * currentMission.orders.EXP;

                        // Append the comparison to the string with red color if not met
                        comparisonText += $"<color=red>CaseStrength + {currentMission.orders.CaseStrength} >= PC CaseStrength {PCitem.Case.CaseStrength}</color>\n";
                    }
                    else
                    {
                        // Append the comparison with white color if met
                        comparisonText += $"<color=white>CaseStrength + {currentMission.orders.CaseStrength} >= PC CaseStrength {PCitem.Case.CaseStrength}</color>\n";
                    }
                }

                // Motherboard Strength comparison
                if (currentMission.orders.MotherboardStrength > 0)
                {
                    if (PCitem.Motherboard.MotherboardStrength < currentMission.orders.MotherboardStrength)
                    {
                        isMissionMet = false;
                        totalDeduction += deductionPercentage * currentMission.orders.OrderPrice;
                        totalEXPDeduction += deductionPercentage * currentMission.orders.EXP;

                        comparisonText += $"<color=red>MotherboardStrength + {currentMission.orders.MotherboardStrength} >= PC MotherboardStrength {PCitem.Motherboard.MotherboardStrength}</color>\n";
                    }
                    else
                    {
                        comparisonText += $"<color=white>MotherboardStrength + {currentMission.orders.MotherboardStrength} >= PC MotherboardStrength {PCitem.Motherboard.MotherboardStrength}</color>\n";
                    }
                }

                // Add similar logic for other components
                if (currentMission.orders.CPUBaseSpeed > 0)
                {
                    if (PCitem.CPU.BaseSpeed < currentMission.orders.CPUBaseSpeed)
                    {
                        isMissionMet = false;
                        totalDeduction += deductionPercentage * currentMission.orders.OrderPrice;
                        totalEXPDeduction += deductionPercentage * currentMission.orders.EXP;

                        comparisonText += $"<color=red>CPU BaseSpeed + {currentMission.orders.CPUBaseSpeed} >= PC CPU BaseSpeed {PCitem.CPU.BaseSpeed}</color>\n";
                    }
                    else
                    {
                        comparisonText += $"<color=white>CPU BaseSpeed + {currentMission.orders.CPUBaseSpeed} >= PC CPU BaseSpeed {PCitem.CPU.BaseSpeed}</color>\n";
                    }
                }

                if (currentMission.orders.RAMMemory > 0)
                {
                    if (PCitem.RAM.Memory < currentMission.orders.RAMMemory)
                    {
                        isMissionMet = false;
                        totalDeduction += deductionPercentage * currentMission.orders.OrderPrice;
                        totalEXPDeduction += deductionPercentage * currentMission.orders.EXP;

                        comparisonText += $"<color=red>RAM Memory + {currentMission.orders.RAMMemory} >= PC RAM Memory {PCitem.RAM.Memory}</color>\n";
                    }
                    else
                    {
                        comparisonText += $"<color=white>RAM Memory + {currentMission.orders.RAMMemory} >= PC RAM Memory {PCitem.RAM.Memory}</color>\n";
                    }
                }

                if (currentMission.orders.CPUFanCoolingPower > 0)
                {
                    if (PCitem.CPUFan.CoolingPower < currentMission.orders.CPUFanCoolingPower)
                    {
                        isMissionMet = false;
                        totalDeduction += deductionPercentage * currentMission.orders.OrderPrice;
                        totalEXPDeduction += deductionPercentage * currentMission.orders.EXP;

                        comparisonText += $"<color=red>CPU Fan CoolingPower + {currentMission.orders.CPUFanCoolingPower} >= PC CPU Fan CoolingPower {PCitem.CPUFan.CoolingPower}</color>\n";
                    }
                    else
                    {
                        comparisonText += $"<color=white>CPU Fan CoolingPower + {currentMission.orders.CPUFanCoolingPower} >= PC CPU Fan CoolingPower {PCitem.CPUFan.CoolingPower}</color>\n";
                    }
                }

                // GPU ClockSpeed comparison
                if (currentMission.orders.GPUClockSpeed > 0)
                {
                    if (PCitem.GPU.ClockSpeed < currentMission.orders.GPUClockSpeed)
                    {
                        isMissionMet = false;
                        totalDeduction += deductionPercentage * currentMission.orders.OrderPrice;
                        totalEXPDeduction += deductionPercentage * currentMission.orders.EXP;

                        comparisonText += $"<color=red>GPU ClockSpeed + {currentMission.orders.GPUClockSpeed} >= PC GPU ClockSpeed {PCitem.GPU.ClockSpeed}</color>\n";
                    }
                    else
                    {
                        comparisonText += $"<color=white>GPU ClockSpeed + {currentMission.orders.GPUClockSpeed} >= PC GPU ClockSpeed {PCitem.GPU.ClockSpeed}</color>\n";
                    }
                }

                // Storage comparison
                if (currentMission.orders.Storage > 0)
                {
                    if (PCitem.STORAGE.Storage < currentMission.orders.Storage)
                    {
                        isMissionMet = false;
                        totalDeduction += deductionPercentage * currentMission.orders.OrderPrice;
                        totalEXPDeduction += deductionPercentage * currentMission.orders.EXP;

                        comparisonText += $"<color=red>Storage + {currentMission.orders.Storage} >= PC Storage {PCitem.STORAGE.Storage}</color>\n";
                    }
                    else
                    {
                        comparisonText += $"<color=white>Storage + {currentMission.orders.Storage} >= PC Storage {PCitem.STORAGE.Storage}</color>\n";
                    }
                }

                // PSU WattagePower comparison
                if (currentMission.orders.PSUWattagePower > 0)
                {
                    if (PCitem.PSU.WattagePower < currentMission.orders.PSUWattagePower)
                    {
                        isMissionMet = false;
                        totalDeduction += deductionPercentage * currentMission.orders.OrderPrice;
                        totalEXPDeduction += deductionPercentage * currentMission.orders.EXP;

                        comparisonText += $"<color=red>PSU WattagePower + {currentMission.orders.PSUWattagePower} >= PC PSU WattagePower {PCitem.PSU.WattagePower}</color>\n";
                    }
                    else
                    {
                        comparisonText += $"<color=white>PSU WattagePower + {currentMission.orders.PSUWattagePower} >= PC PSU WattagePower {PCitem.PSU.WattagePower}</color>\n";
                    }
                }



                // Calculate final price after deduction
                float finalPrice = currentMission.orders.OrderPrice - totalDeduction;
                float finalEXP = currentMission.orders.EXP - totalEXPDeduction;

                // Update player's money
                GameManager.instance.PlayerMoney += (int)finalPrice;
                GameManager.instance.PlayerTotalMoney += (int)finalPrice;
                GameManager.instance.AddPlayerExp((int)finalEXP);
                GameManager.instance.SaveCharInfo(GameManager.instance.UserID, GameManager.instance.PlayerName);

                //delete pc
                DeletePC(documentId);
                LoadUpdatePCSOList();
                GameManager.instance.Buyers.Xbutton();
                GameManager.instance.Buyers.BuyersHideShop();

                //replace Mission
                GameManager.instance.Buyers.RespawnMission(currentMissionIndex, GameManager.instance.Buyers.MissionData.size);


               //congrats panel logic
               ComparisonText.text = comparisonText;
                if (isMissionMet)
                {
                    //GameManager.instance.ShowFloatingText("All requirements met! Full price received.");
                    FeedbackText.text = "Congrats!, All requirements met! Full price received.";
                    TotalText.text = "Money: " + currentMission.orders.OrderPrice + " EXP: " + currentMission.orders.EXP;
                }
                else
                {
                    //GameManager.instance.ShowFloatingText($"Requirements not fully met. Deducted: ${totalDeduction}. Final Price: ${finalPrice}");
                    FeedbackText.text = "Some Requirements are not fully met";
                    TotalText.text = "Money: " + currentMission.orders.OrderPrice + " - " + totalDeduction + " = " + finalPrice + " EXP: " + currentMission.orders.EXP + " - " + totalEXPDeduction + " = " + finalEXP;
                }
                BuyerName.text = currentMission.orders.ClientName;
                CoinReward.text = finalPrice.ToString();
                ExpReward.text = finalEXP.ToString();
                SoldPCName.text = PCitem.PCName;
                yourName.text = "by: " + GameManager.instance.PlayerName;

                BuyerImage1.sprite = currentMission.orders.ClientImage;
                BuyerImage2.sprite = currentMission.orders.ClientImage;
                PCImage1.sprite = PCitem.PCImage;
                PCImage2.sprite = PCitem.PCImage;

                //animation
                GameManager.instance.LTA.PlaySoldPCAnimation();
            }

        }

        public TMP_Text ComparisonText, FeedbackText, TotalText, BuyerName, CoinReward, ExpReward, SoldPCName, yourName;
        public Image BuyerImage1, BuyerImage2, PCImage1, PCImage2;

        //public LeanTweenAnimate LTA;
        public Canvas MainMenu;
        //float lastKeyPressTime = 0f;
        //float cooldownDuration = 1f;
        public void Update()
        {
            //PCData.SavePCItems();

            //if ((PlayerPrefs.GetInt("TutorialDone") == 1))
            //{
            //    if (!InventCon.RenamePC.isActiveAndEnabled /*|| !ApplyThermal.isActiveAndEnabled || !DialogBox.isActiveAndEnabled*/)
            //    {
            //        if (!MainMenu.isActiveAndEnabled)
            //        {
            //            if (Input.GetKeyDown(KeyCode.RightArrow))
            //            {
            //                if (Time.time - lastKeyPressTime > cooldownDuration)
            //                {
            //                    LoadMyDeskScene();
            //                    LTA.showBuild();
            //                    LTA.HideOrders();

            //                    lastKeyPressTime = Time.time;


            //                }


            //            }
            //            if (Input.GetKeyDown(KeyCode.UpArrow))
            //            {

            //                LTA.showOrders();

            //            }
            //            if (Input.GetKeyDown(KeyCode.DownArrow))
            //            {

            //                LTA.HideOrders();

            //            }
            //            if (Input.GetKeyDown(KeyCode.P))
            //            {
            //                OpenPCInv();

            //            }

            //            if (Input.GetKeyDown(KeyCode.S))
            //            {

            //                LoadMyShopScene();
            //                LTA.ShowShop();

            //            }


            //        }
            //    }

            //}
           


            

        }

        public void OpenPCInv()
        {
          
                
                //PCMenu.Show();
                //Anim.HideAllAnimation();
                foreach (var item in PCData.GetCurrentInventoryState())
                {
                    PCpage.UpdateData(item.Key, item.Value.PC.Case.ItemImage, item.Value.PC.PCName, item.Value.PC.inUse);
                    //Debug.LogError("Added");
                }


                
     
            
        }
       
        public void LoadMyDeskScene()
        {
            // Load the "MyDesk" scene when the button is clicked.
            // Canvas.gameObject.SetActive(false);
            PCMenu.Hide();
            BuildScene.gameObject.SetActive(true);

        }
        public void LoadMyShopScene()
        {
            // Load the "MyDesk" scene when the button is clicked.
            // Canvas.gameObject.SetActive(false);
            //SceneManager.LoadScene("Shop", LoadSceneMode.Additive);
            PCMenu.Hide();
            //TopUI.gameObject.SetActive(false);
            ShopScene.gameObject.SetActive(true);
            

        }
        private void OnApplicationQuit()
        {
            //InventCon.CheckifBuildingbeforeQuit();
          
        }

        //if (InventCon.lastUsedItems.Count > 0)
        //{
        //    // List has values
        //    DialogBoxForQuit.gameObject.SetActive(true);
        //    DialogTextForQuit.text = "You're currently building a computer... \n Are You Sure you Want to Quit? \n If Yes, You will find your ongoing build parts back in your inventory.";
        //}
        //else
        //{
        //    return;

        //}
        // Save the player prefs data when the game is quitting.
        // GameManager.Instance.SaveInitialItems(initialItems)

        //try
        //{

        //    PCData.SavePCItems();
        //    //if this is use the items will be removed
        //    //GameManager.Instance.SaveInitialItems(initialItems);
        //    //SaveInitialItems();
        //}
        //catch (Exception) { }

        /*public void LoadInitialItems()
        {
            string savedData = PlayerPrefs.GetString("SavedInitialItems");
            InventoryItemList loadedData = JsonUtility.FromJson<InventoryItemList>(savedData);
            //Debug.LogError(loadedData);
            if (loadedData != null) {
               // GameManager.Instance.itemsToTransfer.AddRange(loadedData.Items);

                foreach (InventoryItem item in loadedData.Items)
                {
                    GameManager.Instance.itemsToTransfer.Add(item);
                    //inventoryData.AddItem(item);

                }
                Debug.LogWarning("Data has been Loaded");

            }*/

        /*
            foreach (InventoryItem item in loadedData.Items)
            {
                //GameManager.Instance.itemsToTransfer.Add(item);
               // inventoryData.AddItem(item);

            }*/
        //initialItems.AddRange(loadedData.Items);
        //Debug.LogWarning("Data has been Loaded");


    }

}
