using Inventory;
using Inventory.Model;
using PartsInventory;
using PC.Model;
using PC.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static Inventory.Model.PartsInventorySO;
//using static Inventory.Model.InventorySO;
//using static UnityEditor.Progress;
using Image = UnityEngine.UI.Image;

namespace PC
{
    public class PCPlayerController : MonoBehaviour
    {
        [SerializeField]
        private PCInventSO PCData;

        [SerializeField]
        private PCItem PCitem;

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
        public UnityEngine.UI.Button TestButton;

        [SerializeField]
        public UnityEngine.UI.Button submitButton;

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

        [SerializeField]
        private Image PCImage;
        [SerializeField]
        private TMP_Text PCName;
        [SerializeField]
        private TMP_Text PCPrice;

        [SerializeField]
        private TMP_Text CaseName;
        [SerializeField]
        private TMP_Text MBName;
        [SerializeField]
        private TMP_Text CPUName;
        [SerializeField]
        private TMP_Text CPUFName;
        [SerializeField]
        private TMP_Text RAMName;
        [SerializeField]
        private TMP_Text GPUName;
        [SerializeField]
        private TMP_Text STRGName;
        [SerializeField]
        private TMP_Text PSUName;

        [SerializeField]
        private TMP_Text Status;

        [SerializeField]
        private Image CaseImage;
        [SerializeField]
        private Image MBImage;
        [SerializeField]
        private Image CPUImage;
        [SerializeField]
        private Image CPUFImage;
        [SerializeField]
        private Image RAMImage;
        [SerializeField]
        private Image GPUImage;
        [SerializeField]
        private Image STRGImage;
        [SerializeField]
        private Image PSUImage;



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
        }
        public void Start()
        {
           //LoadPCSOList();
           PrepareUI();
            //PCData.Initialize();


            //PCeventTrigger1.enabled = false;
            //PCeventTrigger2.enabled = false;


            //YesButton.onClick.AddListener(() =>
            //{
            //    //LTA.showBuild();

            //    if (GameManager.instance.BeenModified)
            //    {
            //        if (InventCon.recentlyBackedItems.Count > 0)
            //        {
            //            foreach (var kvp in InventCon.recentlyBackedItems)
            //            {
            //                InventCon.lastUsedItems[kvp.Key] = kvp.Value;
            //                //InventCon.totalUsedItemsPrice += kvp.Value.item.Price;
            //            }
            //            InventCon.recentlyBackedItems.Clear();
            //        }
            //        //InventCon.OnDoneButtonClick();
            //       // InventCon.Success.Stop();
            //        //LTA.HideSuccess();
            //    }
            //    else
            //    {
            //        InventCon.BackAllCurrentlyItem();
            //    }

               
            //    ModifyPC(PCIndexOutside);
            //    DialogBox.gameObject.SetActive(false);
                
            //});

            //NoButton.onClick.AddListener(() =>
            //{
            //    //LTA.showBuild();
            //    LoadMyDeskScene();
            //    DialogBox.gameObject.SetActive(false);
            //});


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
            this.PCpage.OnItemActionRequested += HandleRightActionRequest;

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
            //TestButton.onClick.AddListener(() => TestComputer(PCIndexOutside));

            //OrderButton.onClick.AddListener(() => Anim.HideAllAnimation());
        }
        private int TestedPCint;
        public void TestComputer(int index)
        {
            PCeventTrigger1.enabled = true;
            PCeventTrigger2.enabled = false;
            MonitorScreen.gameObject.SetActive(false);
            TurnOffButton.gameObject.SetActive(false);
            TurnOnButton.gameObject.SetActive(true);
            PCeventTrigger1.enabled = true;
            PCMenu.Hide();
            TestingComputerPanel.SetActive(true);
            EmptyComputerPanel.SetActive(false);
            Computer PCs = PCData.GetItemAt(index);
            PCSO PCitem = PCs.PC;

            PCImage.sprite = PCitem.PCImage;
            PCName.text = PCitem.PCName;
            //PCPrice.text = "$" + PCitem.PCPrice.ToString() + ".00";

            CaseName.text = PCitem.Case.Name;
            MBName.text = PCitem.Motherboard.Name;
            CPUName.text = PCitem.CPU.Name;
            CPUFName.text = PCitem.CPUFan.Name;
            RAMName.text = PCitem.RAM.Name;
            GPUName.text = PCitem.GPU.Name;
            STRGName.text = PCitem.STORAGE.Name;
            PSUName.text = PCitem.PSU.Name;

            //Status.text = PCitem.TestStatus;

            
            CaseImage.sprite = PCitem.Case.ItemImage;

           
            MBImage.sprite = PCitem.Motherboard.ItemImage;

          
            CPUImage.sprite = PCitem.CPU.ItemImage;

           
            CPUFImage.sprite = PCitem.CPUFan.ItemImage;

          
            RAMImage.sprite = PCitem.RAM.ItemImage;

           
            GPUImage.sprite = PCitem.GPU.ItemImage;

        
            STRGImage.sprite = PCitem.STORAGE.ItemImage;

          
            PSUImage.sprite =  PCitem.PSU.ItemImage;

            PCImagePlaceholder.gameObject.SetActive(true);
            PCImagePlaceholder.sprite = PCitem.PCImage;


            TestedPCint = index;

            

           
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
            pcso.PCImage = PCitem.PCImage;
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
                PCpage.UpdateData(item.Key, item.Value.PC.PCImage, item.Value.PC.PCName, item.Value.PC.inUse);
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
            LoadMyDeskScene();
            Computer PCs = PCData.GetItemAt(PCIndex);
            GameManager.instance.BeenModified = true;

            InventCon.usedItems.Clear();//clear first all the lastusedItems
            InventCon.lastUsedItems.Clear();
            ClearPCs(PCIndex);


            //InventCon.NameText.text = PCs.PC.PCName;
            //InventCon.RenameTxt.text = PCs.PC.PCName;
           // InventCon.PriceText.text = "$" + PCs.PC.PCPrice.ToString("F2");
           // InventCon.totalUsedItemsPrice = PCs.PC.PCPrice;


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
           
        }

        

        private void HandleRightActionRequest(int PCIndex)
        {

            if (InventCon.lastUsedItems.Count > 0)
            {
                YesButton.gameObject.SetActive(true);
                NoButton.gameObject.SetActive(true);
                exitButton.gameObject.SetActive(false);
                // List has values
                DialogBox.gameObject.SetActive(true);

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
            ReturnPC();
        }
        private int PCIndexOutside = 0;
        private void HandleDescriptionRequest(int PCindex)
        {
            Computer PCs = PCData.GetItemAt(PCindex);
            if(PCs.isEmpty) { return; }
            PCSO PCitems = PCs.PC;
            PCpage.UpdateDescription(PCindex, PCitems.PCImage, PCitems.Case.ItemImage, PCitems.Motherboard.ItemImage, PCitems.CPU.ItemImage, PCitems.CPUFan.ItemImage, PCitems.RAM.ItemImage, PCitems.GPU.ItemImage, PCitems.STORAGE.ItemImage, PCitems.PSU.ItemImage,
            PCitems.PCName, PCitems.Case.Name, PCitems.Motherboard.Name, PCitems.CPU.Name, PCitems.CPUFan.Name, PCitems.RAM.Name, PCitems.GPU.Name, PCitems.STORAGE.Name, PCitems.PSU.Name, PCitems.inUse);
            PCIndexOutside = PCindex;
        }
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
          
                if (PCMenu.isActiveAndEnabled == false)
                {
                    PCMenu.Show();
                //Anim.HideAllAnimation();
                foreach (var item in PCData.GetCurrentInventoryState())
                {
                    PCpage.UpdateData(item.Key, item.Value.PC.PCImage, item.Value.PC.PCName, item.Value.PC.inUse);
                    //Debug.LogError("Added");
                }


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
