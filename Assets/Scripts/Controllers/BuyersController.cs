using Exchanger.Model.CPUWorld;
using Exchanger.UI.CPUWorld;
using Orders.Model;
using Orders.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyersController : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private BuyersPage BuyerPage;
    [SerializeField]
    private MissionConSO MissionData;
    private Dictionary<int, float> remainingTimes = new Dictionary<int, float>();
    private Dictionary<int, bool> missionDisplayed = new Dictionary<int, bool>();
    private Queue<int> missionsToReplace = new Queue<int>();
    public float Time = 600f;
    private float remainingTime = 0f; // 3 minutes in seconds
    public Button RefreshNow;
    public int MissionRefreshPrice = 5000;


    public GameObject PCInventoryPanel;
    public GameObject ParstInvent;
    public GameObject PCInvent;
    public GameObject HardwaresButton;
    public GameObject ComputersButton;
    public GameObject UseButton;
    public GameObject InusedButton;
    public Button SellButton;
    public TMP_Text hardwares;


  
    void Start()
    {
        remainingTime = Time;
        MissionData.ShuffleMissions();
        BuyerPage.InitializedCPU(MissionData.size);
        InitializeMissions();

        StartCoroutine(UpdateTimers());
        RefreshNow.onClick.AddListener(RefreshMissions);
        BuyerPage.OnOpenPCInventory += OpenComputerInventory;
    }

    private void InitializeMissions()
    {

        var currentInventoryState = MissionData.GetCurrentInventoryState();
        foreach (var item in currentInventoryState)
        {
            string requirements = RequirementsList(item.Value);
            BuyerPage.UpdateData(item.Key, item.Value.orders.ClientImage, item.Value.orders.ClientName, item.Value.orders.Description, requirements, (int)item.Value.orders.OrderPrice, item.Value.orders.EXP);

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


    private IEnumerator UpdateTimers()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);

            remainingTime -= 1f;

            if (remainingTime <= 0f)
            {
                RespawnMissions();
                remainingTime = Time; // Reset time to 3 minutes
            }

            UpdateTimerDisplay();
            MissionData.InformAboutChange();
        }
    }

    private void RespawnMissions()
    {
        MissionData.ShuffleMissions();
        InitializeMissions();
        //CPUsPage.ResetSelection();
    }

    private void RefreshMissions()
    {
        if (GameManager.instance.PlayerMoney >= MissionRefreshPrice)
        {

            MissionData.ShuffleMissions();
            InitializeMissions();
            remainingTime = Time;
            //CPUsPage.ResetSelection();
            GameManager.instance.PlayerMoney -= MissionRefreshPrice;
            GameManager.instance.SaveCharInfo(GameManager.instance.UserID, GameManager.instance.PlayerName);
        }
        else
        {
            //Debug.LogError("You dont have enough money");
            GameManager.instance.ShowFloatingText("You don't have enough coins");
        }
    }

    private void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(remainingTime / 60F);
        int seconds = Mathf.FloorToInt(remainingTime - minutes * 60);
        string timeText = string.Format("{0:0}:{1:00}", minutes, seconds);
        BuyerPage.UpdateTimer(timeText);
    }

    public void BuyersOpenShop()
    {
        BuyerPage.Show();
        //CPUsPage.ResetSelection();
    }

    public int selectedMissionIndex = -1;
    public Missions SelectedMission;
    private void OpenComputerInventory(int missionId)
    {
        //open pc invent
        HardwaresButton.SetActive(false);
        ParstInvent.SetActive(false);
        UseButton.SetActive(false);
        InusedButton.SetActive(false);
        ComputersButton.SetActive(false);

        PCInventoryPanel.SetActive(true);
        PCInvent.SetActive(true);
        SellButton.gameObject.SetActive(true);
        
        var missionToMove = MissionData.GetItemAt(missionId);
        hardwares.text = "Sell Computer to " + missionToMove.orders.ClientName + "?";
        //SubmitToName.text = "Submit Computer to " + missionToMove.orders.ClientName + "?";
        selectedMissionIndex = missionId;
        SelectedMission = missionToMove;
    }

    public void Xbutton()
    {
        HardwaresButton.SetActive(true);
        ParstInvent.SetActive(true);
        UseButton.SetActive(true);
        InusedButton.SetActive(false);
        ComputersButton.SetActive(true);
        hardwares.text = "hardwares";
        PCInventoryPanel.SetActive(false);
        PCInvent.SetActive(false);
        SellButton.gameObject.SetActive(false);
    }

















    // Update is called once per frame
    void Update()
    {
        
    }
}
