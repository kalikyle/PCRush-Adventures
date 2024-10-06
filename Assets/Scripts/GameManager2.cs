using Inventory;
using Inventory.Model;
using Inventory.UI;
using Orders.Model;
using PC;
using PC.Model;
using Shop;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Inventory.Model.InventorySO;
public class GameManager2 : MonoBehaviour
{
    public static GameManager2 Instance { get; set; }

    
    [SerializeField]
    private InventoryController IC;
    [SerializeField]
    private ShopController2 IS;

    [SerializeField]
    private LeanTweenAnimate2 LTA;

    [SerializeField]
    public Camera MainCamera;

    [SerializeField]
    public Canvas ShopScene;

    [SerializeField]
    public Canvas BuildScene;

    public event Action <InventoryItem> OnItemsToTransferUpdated;
    public List<InventoryItem> itemsToTransfer = new List<InventoryItem>();
    public int tempindex;

    //public bool WiresSceneEnabled = false;

    public Dictionary<string, InventoryItem> UsedImagesNeeds = new Dictionary<string, InventoryItem>();


    [SerializeField]
    public Image DialogBox;
    [SerializeField]
    public TMP_Text DialogText;

    [SerializeField]
    public TMP_Text playerlabel;
    [SerializeField]
    public TMP_Text BottomplayerName;

    [SerializeField]
    private InventorySO inventoryData;

    [SerializeField]
    private PCInventSO2 PCData;
    [SerializeField]
    private ShopSO2 shopData;
    public GameObject DialogUI, Dialog;


    public GameObject RequirementsPanel;
    public GameObject HardText;
    public GameObject NormalText;
    public GameObject EasyText;

    public Image ClientImage;
    public TMP_Text ClientText;
    public TMP_Text Description;
    public TMP_Text Requirements;
    public GameObject TryAgainPanel;


    public GameObject image1Ready;
    public GameObject image2Ready;

    public GameObject ReadyPanel;
    public GameObject ReadyUI;
    public GameObject CountdownUI;
    public GameObject ReadyBTN;
    public GameObject three;
    public GameObject two;
    public GameObject one;
    public GameObject Build;



    [SerializeField] private GameObject shoppopupPrefab;// Reference to the parent transform where the popups will be placed
    [SerializeField] private Transform shopPopUpParent;


    public TMP_Text timerText; // Reference to the TMP_Text component
    private float timer; // Timer variable

    private bool isTimerRunning; // To check if the timer is running

    public void StartTimer()
    {
        isTimerRunning = true;
        timer = 0f; // Reset the timer
    }

    public void TheTimer()
    {
        if (isTimerRunning)
        {
            timer += Time.deltaTime; // Increase timer by the time passed since the last frame

            // Convert the timer to minutes, seconds, and milliseconds
            int minutes = Mathf.FloorToInt(timer / 60f);
            int seconds = Mathf.FloorToInt(timer % 60);
            int milliseconds = Mathf.FloorToInt((timer * 1000f) % 1000);

            // Update the TMP_Text component with the formatted time
            timerText.text = $"{minutes:00}:{seconds:00}:{milliseconds:00}";

            TheGame.instance.Time = timer;
        }
    }

    public void StopTimer()
    {
        isTimerRunning = false;
        timer = 0f; // Reset the timer
    }

    public void Update()
    {
        TheTimer();
    }


    public void ShowPopUp(InventoryItem inventoryItem)
    {
        GameObject newShopPopup = Instantiate(shoppopupPrefab, shopPopUpParent); // Instantiate the popup as a child of the designated parent

        LeanTween.moveLocal(newShopPopup, new Vector3(0f, -143f, 0f), 1f)
            .setEase(LeanTweenType.easeOutExpo)
            .setOnComplete(() => HidePopUp(newShopPopup));

        UpdateShopPopup(newShopPopup, inventoryItem);
    }

    private void HidePopUp(GameObject shopPopup)
    {
        LeanTween.moveLocal(shopPopup, new Vector3(0f, -283f, 0f), 1f)
            .setDelay(1f)
            .setEase(LeanTweenType.easeOutExpo)
            .setOnComplete(() => Destroy(shopPopup));
    }

    private void UpdateShopPopup(GameObject shopPopup, InventoryItem inventoryItem)
    {
        Image[] images = shopPopup.GetComponentsInChildren<Image>();
        TMP_Text[] texts = shopPopup.GetComponentsInChildren<TMP_Text>();

        foreach (var image in images)
        {
            // Check conditions or naming conventions to identify the image elements you need to update
            if (image.gameObject.name == "ItemImage") // Assuming the GameObject name is set in the editor
            {
                image.sprite = inventoryItem.item.ItemImage;
                break; // Assuming there's only one image to update
            }
        }

        foreach (var text in texts)
        {
            if (text.gameObject.name == "ItemName") // Assuming the GameObject name is set in the editor
            {
                text.text = inventoryItem.item.Name;
            }
        }
    }
    private void Start()
    {
        TheGame.instance.image1Ready = image1Ready;
        TheGame.instance.image2Ready = image2Ready;

       

    CheckMission();

    }

    private void Awake()
    {
        TheGame.instance.ReadyPanel = ReadyPanel;
        TheGame.instance.ReadyUI = ReadyUI;
        TheGame.instance.CountdownUI = CountdownUI;
        TheGame.instance.ReadyBTN = ReadyBTN;
        TheGame.instance.three = three;
        TheGame.instance.two = two;
        TheGame.instance.one = one;
        TheGame.instance.Build = Build;

        if (Instance == null)
        {
            
            Instance = this;
            DontDestroyOnLoad(gameObject);

            //IC.LoadInitialItems();
            // Keep this object alive between scenes.
            SceneManager.sceneUnloaded += OnSceneUnloaded;
        }
        else
        {
            Destroy(gameObject);


        }


       
    }

    public void ReadyClicked()
    {
        TheGame.instance.OnReadyButtonClick();
    }

    private void OnSceneUnloaded(Scene scene)
    {
        if (scene.name == "PCRush")
        {
            // Destroy the GameManager2 instance when PCRush scene is unloaded
            Destroy(gameObject);
        }
    }

    public void CheckMission()
    {

        if (TheGame.instance != null)
        {
            if (!TheGame.instance.currentMission.isEmpty && TheGame.instance.IsHardMode.Value == true)
            {
                //The Game is in hardmode
                RequirementsPanel.SetActive(true);
                HardText.SetActive(true);
                NormalText.SetActive(false);
                EasyText.SetActive(false);
                Missions mission = TheGame.instance.currentMission;

                ClientImage.sprite = mission.orders.ClientImage;
                ClientText.text = mission.orders.ClientName;
                Description.text = mission.orders.Description;
                Requirements.text = RequirementsList(mission);



            }

            else if (TheGame.instance.IsNormalMode.Value == true)
            {
                HardText.SetActive(false);
                NormalText.SetActive(true);
                EasyText.SetActive(false);
                RequirementsPanel.SetActive(false);

            }

            else if (TheGame.instance.IsEasyMode.Value == true)
            {
                HardText.SetActive(false);
                NormalText.SetActive(false);
                EasyText.SetActive(true);
                RequirementsPanel.SetActive(false);
            }
            else
            {
                return;
            }
        }
    }


    public string RequirementsList(Missions mission)
    {
        string missions = "";
        if (mission.orders.CaseStrength != 0)
        {
            missions += "Case Strength: +" + mission.orders.CaseStrength + "\n";
        }
        if (mission.orders.MotherboardStrength != 0)
        {
            missions += "Motherboard Strength: +" + mission.orders.MotherboardStrength + "\n";
        }
        if (mission.orders.CPUBaseSpeed != 0)
        {
            missions += "CPU Base Speed: +" + mission.orders.CPUBaseSpeed + "Ghz\n";
        }
        if (mission.orders.RAMMemory != 0)
        {
            missions += "RAM Memory:  +" + mission.orders.RAMMemory + "GB\n";
        }
        if (mission.orders.CPUFanCoolingPower != 0)
        {
            missions += "CPU Fan Cooling Power: +" + mission.orders.CPUFanCoolingPower + "\n";
        }
        if (mission.orders.GPUClockSpeed != 0)
        {
            missions += "GPU Clock Speed: +" + mission.orders.GPUClockSpeed + "Mhz\n";
        }
        if (mission.orders.Storage != 0)
        {
            missions += "Storage: +" + mission.orders.Storage + "GB\n";
        }
        if (mission.orders.PSUWattagePower != 0)
        {
            missions += "PSU Power +" + mission.orders.PSUWattagePower + "W\n";
        }
        return missions;
    }

    private void OnDestroy()
    {
        // Unsubscribe from scene unloaded event to avoid memory leaks
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }

    public void AddItemToTransfer(InventoryItem item)
    {
        itemsToTransfer.Add(item);
        OnItemsToTransferUpdated?.Invoke(item);
    }


    public void SaveInitialItems(List<InventoryItem> items)
    {
        
         string jsonData = JsonUtility.ToJson(new InventoryItemList { Items = items });
         PlayerPrefs.SetString("SavedInitialItems", jsonData);
         PlayerPrefs.Save();
        //Debug.LogError("Data has been Saved");
    }
    public void SaveComputerItems(List<Computer2> PCitems)
    {

        string jsonData = JsonUtility.ToJson(new ComputerItemList { PCItem2s = PCitems });
        PlayerPrefs.SetString("SavedComputerItems", jsonData);
        PlayerPrefs.Save();
       // Debug.LogError("Data has been Saved");
    }

    public void SaveUniqueIndex(List<int> unique)
    {

        string jsonData = JsonUtility.ToJson(new UniqueIndexes { unique = unique });
        PlayerPrefs.SetString("SavedUniqueIndex", jsonData);
        PlayerPrefs.Save();
        
    }

    public void BackSingleItem(string category)
    {
        IC.HandleBackItem(category);
    }
    private void OnApplicationQuit()
    {
        
    }
}
[System.Serializable]
public class InventoryItemList
{
    public List<InventoryItem> Items;
}
[System.Serializable]
public class ComputerItemList
{
    public List<Computer2> PCItem2s;
}

[System.Serializable]
public class UniqueIndexes
{
    public List<int> unique;
}




