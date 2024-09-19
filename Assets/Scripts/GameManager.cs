using Assets.PixelHeroes.Scripts.CharacterScrips;
using Assets.PixelHeroes.Scripts.CollectionScripts;
using Decoration;
using Exchanger;
using Firebase;
using Firebase.Auth;
using Firebase.Extensions;
using Firebase.Firestore;
using OtherWorld;
using OtherWorld.Model;
using PartsInventory;
using PartsInventory.Model;
using PC;
using PC.Model;
//using Firebase.Analytics;
using Shop;
using Shop.Model;
using Shop.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Decoration.Model.DecorSO;
using static Inventory.Model.PartsInventorySO;
using static OtherWorld.Model.OWInvSO;
//
//using static UnityEditorInternal.ReorderableList;

public class GameManager : MonoBehaviour
{
   
    public string UserID;
    public string UserCollection = "users";
    public static GameManager instance;
    public CharacterBuilder charBuilder;
    public SpriteCollection SpriteCollections;
    public ShopController SC;
    public DecorController DC;
    public Shop.Model.ShopSO so;
    public DecorationManager DecorMan;
    public EquipmentsController Equipments;
    public LeanTweenAnimate LTA;
    public QuestPoint qp;
    public OWInvController OWC;
    public ExchangerController Exchanger;
    public HorderManager Hordemanager;
    public Health PlayerHealthScript;
    public PlayerArmor PlayerArmorScript;
    public PartsInventoryController PartsController;
    public PlayerTeleport playerTeleport;
    public PCPlayerController PPC;
    public PartsInventoryController PIC;
    public bool HasOpened = false;
    public Dictionary<string, InventoryItem> UsedImagesNeeds = new Dictionary<string, InventoryItem>();
    [SerializeField]
    public Camera MainCamera;


    [Header("For Shop and Decorations")]
    public bool isEditing = false;
    public TMP_Text UserIDTxt;
    public int ShopSize = 100;

    public Image Monitor;
    public Image Keyboard;
    public Image Mouse;
    public Image MousePad;
    public Image Desk;
    public Image Background;

    public Button removeButton;
    public Button MirrorButton;
    public Button rotateLeftButton;
    public Button rotateRightButton;
    public Button ResizeIncButton;
    public Button ResizeDecButton;
    public Button DoneButton;

    public Button Enter;


    //for Lan
    [Header("LAN")]
    public GameObject PlayOnLanCanvas;


    //for MiniMap
    [Header("MINIMAP")]
    public Button MiniMapButton;
    public GameObject HomeWorldMap;
    public GameObject CPUWorldMap;
    public GameObject RAMWorldMap;
    public GameObject CPUFWorldMap;
    public GameObject GPUWorldMap;
    public GameObject STORAGEWorldMap;
    public GameObject PSUWorldMap;
    public GameObject MBWorldMap;
    public GameObject CASEWorldMap;

    public Button MinimapButton;
    public bool HomeWorld = true;
    public bool CPUWorld = false;
    public bool RAMWorld = false;
    public bool CPUFWorld = false;
    public bool GPUWorld = false;
    public bool StorageWorld = false;
    public bool PSUWorld = false;
    public bool MBWorld = false;
    public bool CaseWorld = false;
    public bool MinimapOpened = false;

    //for CutScenes
    [Header("CUT SCENES")]
    public GameObject CutScene1;
    public GameObject CutScene2;
    public GameObject CutScene3;
    public GameObject CutScene4;
    public GameObject CutScene5;
    public GameObject CutScene6;
    public GameObject CutScene7;
    public GameObject CutScene8;
    public GameObject CutScene9;
    public GameObject CutScene10;
    public GameObject SquareBars;

    //for UI
    [Header("UI")]
    public GameObject UIExplore;
    public GameObject ComputerInv;
    public GameObject InGamePanel;
    public GameObject QuestUI;
    public GameObject PlayerDeskUI;
    public GameObject UIPanel;
    public GameObject ModifyBTN;
    public GameObject BuildingRoom;
    

    public GameObject SwordDealerPanel;
    public GameObject ArmorDealerPanel;
    public GameObject HelmetDealerPanel;
    public GameObject ShieldDealerPanel;


    public GameObject CPUExhangerPanel;
    public TMP_Text CPUMaterialText;
    public GameObject RAMExhangerPanel;
    public TMP_Text RAMMaterialText;
    public GameObject CPUFExhangerPanel;
    public TMP_Text CPUFMaterialText;
    public GameObject GPUExhangerPanel;
    public TMP_Text GPUMaterialText;
    public GameObject StorageExhangerPanel;
    public TMP_Text StorageMaterialText;
    public GameObject PSUExhangerPanel;
    public TMP_Text PSUMaterialText;
    public GameObject MBExhangerPanel;
    public TMP_Text MBMaterialText;
    public GameObject CaseExhangerPanel;
    public TMP_Text CaseMaterialText;


    public GameObject ArmorFixer;

    //public GameObject DecorClickedUI;

    public bool clicked = false;
    public bool OpenEditor = false;
    public bool BeenModified = false;

    public event Action<InventoryItem> OnItemsToTransferUpdated;
    public List<InventoryItem> itemsToTransfer = new List<InventoryItem>();
    public int Partstempindex;

    public event Action<OtherWorldItem> OnOWItemsToTransferUpdated;
    public List<OtherWorldItem> OWitemsToTransfer = new List<OtherWorldItem>();
    public int OWstempindex;

    public List<DecorationItem> removedItemsDuringEditing = new List<DecorationItem>();
    public event Action<DecorationItem> OnDecorToTransferUpdated;
    public List<DecorationItem> DecorToTransfer = new List<DecorationItem>();
    public int tempindex;

    //player info
    [Header("PLAYER INFO")]
    public string PlayerName;
    public int PlayerMoney = 0;
    //public int PlayerGems = 0;
    public int PlayerLevel = 1;
    public int PlayerExpToLevelUp = 20;
    public int PlayerEXP = 0;
    public int PlayerEasyModeWins = 0;
    public int PlayerNormalModeWins = 0;
    public int PlayerHardModeWins = 0;
    public float PlayerBestTimeEasyMode = 0f;
    public float PlayerBestTimeNormalMode = 0f;
    public float PlayerBestTimeHardMode = 0f;

    public int PlayerTotalMoney = 0;

    //player base stats
    public int PlayerAttackDamage = 1;
    public int PlayerHealth = 100;
    public int PlayerMana = 100;
    public int PlayerHealthRegen = 1;
    public int PlayerWalkSpeed = 1;
    public int PlayerArmor = 50;
    public int PlayerManaRegen = 1;
    public double PlayerCriticalChance = 1;

    //player PC stats
    public double PlayerPCAttackDamage = 0;
    public double PlayerPCHealth = 0;
    public double PlayerPCMana = 0;
    public double PlayerPCHealthRegen = 0;
    public double PlayerPCWalkSpeed = 0;
    public double PlayerPCArmor = 0;
    public double PlayerPCManaRegen = 0;
    public double PlayerPCCriticalChance = 0;

    //player Total Stats
    public double PlayerTotalAttackDamage = 0;
    public double PlayerTotalHealth = 0;
    public double PlayerTotalMana = 0;
    public double PlayerTotalHealthRegen = 0;
    public double PlayerTotalWalkSpeed = 0;
    public double PlayerTotalArmor = 0;
    public double PlayerTotalManaRegen = 0;
    public double PlayerTotalCriticalChance = 0;


    public int EquipmentAttackDamage = 0;
    public int EquipmentMana = 0;
    public int EquipmentArmor = 0;
    public int EquipmentHealth = 0;
    public int EquipmentHealthRegen = 0;
    public int EquipmentManaRegen = 0;
    public int EquipmentCriticalChance = 0;


    public bool InHomeWorld = true;
    public int TempEnemyKilled = 0;

    public Dictionary<string, Shop.Model.ShopItem> equippedItemsByCategory = new Dictionary<string, Shop.Model.ShopItem>();


    //in Explore UI
    [Header("EXPLORE UI")]
    public TMP_Text PlayerDesk;
    public TMP_Text Playerui;
    public Image PlayerImage;
    public Image PlayerOWImage;
    public TMP_Text PlayerMoneyText;
    public TMP_Text PlayerGemsText;
    public TMP_Text PlayerLevelText;
    public Slider PlayerLevelSlide;
    public Slider healthSlider;
    public TMP_Text HealthhitPoints;
    public Slider manaSlider;
    public TMP_Text manaText;
    public Slider ArmorSlider;
    public TMP_Text armorText;

    //Player Stats UI
    public TMP_Text BaseAttackDamage;
    public TMP_Text BaseHealth;
    public TMP_Text BaseMana;
    public TMP_Text BaseHealthRegen;
    public TMP_Text BaseWalkSpeed;
    public TMP_Text BaseArmor;
    public TMP_Text BaseManaRegen;
    public TMP_Text BaseCriticalChance;

    public TMP_Text PCAttackDamage;
    public TMP_Text PCHealth;
    public TMP_Text PCMana;
    public TMP_Text PCHealthRegen;
    public TMP_Text PCWalkSpeed;
    public TMP_Text PCArmor;
    public TMP_Text PCManaRegen;
    public TMP_Text PCCriticalChance;

    public TMP_Text TotalAttackDamage;
    public TMP_Text TotalHealth;
    public TMP_Text TotalMana;
    public TMP_Text TotalHealthRegen;
    public TMP_Text TotalWalkSpeed;
    public TMP_Text TotalArmor;
    public TMP_Text TotalManaRegen;
    public TMP_Text TotalCriticalChance;
    public TMP_Text TotalCriticalHit;

    public TMP_Text StatsPlayerName;
    public Image PlayerStatsImage;

    public Image StatsSwordImage;
    public Image StatsArmorImage;
    public Image StatsHelmetImage;
    public Image StatsShieldImage;

    public GameObject StatsUsedPCPanel;
    public Image StatsPCImageUsed;
    public TMP_Text StatsPCName;

    public Slider StatsPlayerLevelSlider;
    public TMP_Text StatsPlayerLevel;
    public TMP_Text StatsCurrentExp;

    public Slider StatsPlayerHealthSlider;
    public TMP_Text StatsCurrentHealthText;

    public Slider StatsmanaSlider;
    public TMP_Text StatsmanaText;

    public Slider StatsArmorSlider;
    public TMP_Text StatsarmorText;
    public Button TalkBTN;

    //for Quest
    [Header("QUESTS")]
    public string CurrentNPC;
    public int packagescollected = 0;
    public int regionsunlocked = 1;
    public QuestEvent questEvents;
    public bool HasInitialize = false;
    public bool OnQuest = false;
    public bool OpenBuild = false;

    public GameObject HouseDoor;
    public GameObject BuildingDesk;
    public GameObject PlayerDeskRoom;
    public GameObject pumpkinSoup;
    public GameObject ArenaWall;
    public GameObject CpuTwirl;
    public GameObject ExitRoom;
    public GameObject TheLostAdventurer;

    public bool CutScene2Open = false;
    public bool OnBuildingQuest = false;
    public bool DoneRename = false;
    public bool ComputerPlaced = false;
    public bool OnGoToDeskQuest = false;
    public bool GoDownStairsQuest = false;
    public bool DoneDownStairsQuets = false;
    public bool OnGoToDeskQuestAgain = false;
    public bool OntheDesk = false;
    public bool OnTurnOnQuest = false;
    public bool PCTurnOn = false;
    public bool OnExploreDesktopQuest = false;
    public bool OnExploreDeskDone = false;
    public bool OnCutScene7Open = false;
    public bool OnBuySwordQuest = false;
    public bool OnBuyDone = false;
    public bool OnStartFightQuest = false;
    public bool OnTheArea = false;
    public bool HordeFinished = false;
    public bool OnHeadBackQuest = false;
    public bool GoBackHomeQuest = false;
    public bool OnCutScene9Finish = false;
    public bool OnSleepQuest = false;
    public bool OnSleepFinish = false;
    public bool OnCollectCPUQuest = false;
    public bool CPUCollected = false;
    public bool OnModifyQuest = false;
    public bool DoneModify = false;
    public bool OnRegionQuest = false;
    public bool OnStartCaseFightQuest = false;
    public bool CaseHordeFinished = false;
    public bool ExchangeToCaseQuest = false;
    public bool OnCollectCaseQuest = false;
    public bool CaseCollected = false;
    public bool OnModifyCaseQuest = false;
    public bool DoneModifyCase = false;
    public bool OpenYourDesktopFinish = false;
    public bool DesktopOpenedFinish = false;

    public Button BackButton;
    public Button OnDeskBackButton;
    public Button PerksButon;
    public Button CanCelButtonBuild;
    public Button CPUExchangeXButton;
    public Button CaseExchangeXButton;



    public GameObject CPUSpawn;

    

    [Header("Ink JSON")]
    [SerializeField] public TextAsset MainStory;


    //for shop ui
    [Header("SHOP UI")]
    public TMP_Text ShopGold;
    public TMP_Text ShopGem;

    //FOR NOTIFS
    [Header("NOTIF UI")]
    [SerializeField] private GameObject notifpopup;
    [SerializeField] private GameObject Equipnotifpopup;
    [SerializeField] private GameObject Itemsnotifpopup;
    [SerializeField] public Transform notifpPopUpParent;

    public Dictionary<string, string> DefaultCharacter = new Dictionary<string, string>();

    public void ResetPlayer()
    {
        UserID = string.Empty;
        PlayerName = string.Empty;
        CurrentNPC = string.Empty;

        PlayerName = "";
    PlayerMoney = 0;
    //PlayerGems = 0;
   PlayerLevel = 1;
   PlayerExpToLevelUp = 20;
   PlayerEXP = 0;

    //player base stats
    PlayerAttackDamage = 1;
    PlayerHealth = 100;
    PlayerMana = 100;
    PlayerHealthRegen = 1;
    PlayerWalkSpeed = 1;
   PlayerArmor = 50;
   PlayerManaRegen = 1;
    PlayerCriticalChance = 1;

    //player PC stats
     PlayerPCAttackDamage = 0;
  PlayerPCHealth = 0;
  PlayerPCMana = 0;
   PlayerPCHealthRegen = 0;
   PlayerPCWalkSpeed = 0;
   PlayerPCArmor = 0;
  PlayerPCManaRegen = 0;
    PlayerPCCriticalChance = 0;

    //player Total Stats
   PlayerTotalAttackDamage = 1;
   PlayerTotalHealth = 100;
   PlayerTotalMana = 100;
    PlayerTotalHealthRegen = 1;
PlayerTotalWalkSpeed = 1;
    PlayerTotalArmor = 50;
    PlayerTotalManaRegen = 1;
    PlayerTotalCriticalChance = 1;


   EquipmentAttackDamage = 0;
   EquipmentMana = 0;
  EquipmentArmor = 0;
   EquipmentHealth = 0;
   EquipmentHealthRegen = 0;
    EquipmentManaRegen = 0;
    EquipmentCriticalChance = 0;

       UIExplore.SetActive(false);

    // Reset booleans
        HasOpened = true;
        isEditing = false;
        clicked = false;
        OpenEditor = false;
        BeenModified = false;

        InHomeWorld = true;
        CutScene2Open = false;
        OnBuildingQuest = false;
        DoneRename = false;
        ComputerPlaced = false;
        OnGoToDeskQuest = false;
        GoDownStairsQuest = false;
        DoneDownStairsQuets = false;
        OnGoToDeskQuestAgain = false;
        OntheDesk = false;
        OnTurnOnQuest = false;
        PCTurnOn = false;
        OnExploreDesktopQuest = false;
        OnExploreDeskDone = false;
        OnCutScene7Open = false;
        OnBuySwordQuest = false;
        OnBuyDone = false;
        OnStartFightQuest = false;
        OnTheArea = false;
        HordeFinished = false;
        OnHeadBackQuest = false;
        GoBackHomeQuest = false;
        OnCutScene9Finish = false;
        OnSleepQuest = false;
        OnSleepFinish = false;
        OnCollectCPUQuest = false;
        CPUCollected = false;
        OnModifyQuest = false;
        DoneModify = false;
        OnRegionQuest = false;


        MinimapOpened = false;
        HomeWorld = true;
        CPUWorld = false;
        RAMWorld = false;
        CPUFWorld = false;
        GPUWorld = false;
        StorageWorld = false;
        PSUWorld = false;
        MBWorld = false;
        CaseWorld = false;

        // Clear lists
        itemsToTransfer.Clear();
        OWitemsToTransfer.Clear();
        removedItemsDuringEditing.Clear();
        DecorToTransfer.Clear();

        // Clear dictionaries
        equippedItemsByCategory.Clear();
        DefaultCharacter.Clear();
    }


    //public int lastPlayerMoney = 0;

    public void ThePlayerStats()
    {
        
        
        BaseAttackDamage.text = PlayerAttackDamage.ToString();
        BaseHealth.text = PlayerHealth.ToString();
        BaseMana.text = PlayerMana.ToString();
        BaseHealthRegen.text = PlayerHealthRegen.ToString();
        BaseWalkSpeed.text = PlayerWalkSpeed.ToString();
        BaseArmor.text = PlayerArmor.ToString();
        BaseManaRegen.text = PlayerManaRegen.ToString();
        BaseCriticalChance.text = PlayerCriticalChance.ToString() + "%";

        PCAttackDamage.text = PlayerPCAttackDamage.ToString();
        PCHealth.text = PlayerPCHealth.ToString();
        PCMana.text = PlayerPCMana.ToString();
        PCHealthRegen.text= PlayerPCHealthRegen.ToString();
        PCWalkSpeed.text = "0."+PlayerPCWalkSpeed.ToString();
        PCArmor.text = PlayerPCArmor.ToString();
        PCManaRegen.text = PlayerPCManaRegen.ToString();
        PCCriticalChance.text = PlayerPCCriticalChance.ToString() +"%";

        if(EquipmentAttackDamage != 0)
        {
            PlayerTotalAttackDamage = PlayerAttackDamage + EquipmentAttackDamage + PlayerPCAttackDamage;
            BaseAttackDamage.text = PlayerAttackDamage.ToString()  + " (+ " + EquipmentAttackDamage + ")";

        }
        else
        {
            PlayerTotalAttackDamage = PlayerAttackDamage + PlayerPCAttackDamage;
        }

        if (EquipmentHealth != 0)
        {
            PlayerTotalHealth = PlayerHealth + EquipmentHealth + PlayerPCHealth;
            BaseHealth.text = PlayerHealth.ToString() + " (+ " + EquipmentHealth + ")";
            if (PlayerHealthScript.currentHealth == PlayerHealthScript.maxHealth)
            {
                PlayerHealthScript.currentHealth = (int)PlayerTotalHealth;
            }
            
        }
        else
        {
            PlayerTotalHealth = PlayerHealth + PlayerPCHealth;
        }

        if(EquipmentMana != 0)
        {
            PlayerTotalMana = PlayerMana + EquipmentMana + PlayerPCMana;
            BaseMana.text = PlayerMana.ToString() + " (+ " + EquipmentMana + ")";
        }
        else
        {
            PlayerTotalMana = PlayerMana + PlayerPCMana;
        }

        if (EquipmentHealthRegen != 0)
        {
            PlayerTotalHealthRegen = PlayerHealthRegen + EquipmentHealthRegen + PlayerPCHealthRegen;
            BaseHealthRegen.text = PlayerHealthRegen.ToString() + " (+ " + EquipmentHealthRegen + ")";
        }
        else
        {
            PlayerTotalHealthRegen = PlayerHealthRegen + PlayerPCHealthRegen;
        }

        
        PlayerTotalWalkSpeed = PlayerWalkSpeed + PlayerPCWalkSpeed * 0.1;

        if(EquipmentArmor != 0)
        {
            PlayerTotalArmor = PlayerArmor + EquipmentArmor + PlayerPCArmor;
            BaseArmor.text = PlayerArmor.ToString() + " (+ " + EquipmentArmor + ")";

            if (PlayerArmorScript.currentArmor == PlayerArmorScript.maxArmor)
            {
                PlayerArmorScript.currentArmor = (int)PlayerTotalArmor;
            }
            
        }
        else
        {
            PlayerTotalArmor = PlayerArmor + PlayerPCArmor;
        }
        

        if(EquipmentManaRegen != 0)
        {
            PlayerTotalManaRegen = PlayerManaRegen  + EquipmentManaRegen + PlayerPCManaRegen;
            BaseManaRegen.text = PlayerManaRegen.ToString() + " (+ " + EquipmentManaRegen + ")";
        }
        else
        {
            PlayerTotalManaRegen = PlayerManaRegen + PlayerPCManaRegen;
        }

        if (EquipmentCriticalChance != 0)
        {
            PlayerTotalCriticalChance = PlayerCriticalChance + EquipmentCriticalChance + PlayerPCCriticalChance;
            BaseCriticalChance.text = PlayerCriticalChance.ToString()+"%"+ " (+ " + EquipmentCriticalChance + "%)";
        }
        else
        {
            PlayerTotalCriticalChance = PlayerCriticalChance + PlayerPCCriticalChance;
        }

       
        TotalAttackDamage.text = PlayerTotalAttackDamage.ToString();
        TotalHealth.text = PlayerTotalHealth.ToString();
        TotalMana.text = PlayerTotalMana.ToString();
        TotalHealthRegen.text = PlayerTotalHealthRegen.ToString();
        TotalWalkSpeed.text = PlayerTotalWalkSpeed.ToString();
        TotalArmor.text = PlayerTotalArmor.ToString();
        TotalManaRegen.text = PlayerTotalManaRegen.ToString();
        TotalCriticalChance.text = PlayerTotalCriticalChance.ToString() + "%";
        TotalCriticalHit.text = (PlayerTotalAttackDamage * 2).ToString();

        
    }

   // private bool isFirstCheck = true;

    public void checkMoney()
    {

        //if (isFirstCheck)
        //{
        //    lastPlayerMoney = PlayerMoney; // Initialize lastPlayerMoney with the loaded PlayerMoney value
        //    isFirstCheck = false; // Mark that the first check has passed
        //    return;
        //}

        //if (PlayerMoney > lastPlayerMoney)
        //    {

        //        // Calculate the difference (newly earned money)
        //        int moneyEarned = PlayerMoney - lastPlayerMoney;

        //        // Add to the total money earned
        //        PlayerTotalMoney += moneyEarned;
        //        SaveCharInfo(UserID, PlayerName);
        //        AchievementManager.instance.CheckAchievements();
        //        AchievementManager.instance.DisplayAchievements();
        //    }

        //await Task.Delay(500);
        //lastPlayerMoney = PlayerMoney;
        AchievementManager.instance.CheckAchievements();

    }

    public void UnequipEquipment()
    {
        EquipmentAttackDamage = 0;
        EquipmentMana = 0;
        EquipmentArmor = 0;
        EquipmentHealth = 0;
        EquipmentHealthRegen = 0;
        EquipmentManaRegen = 0;
        EquipmentCriticalChance = 0;
    }


    

    public void CurrencyText()
    {
        PlayerMoneyText.text = PlayerMoney.ToString();
        //PlayerGemsText.text = PlayerGems.ToString();
        ShopGold.text = PlayerMoney.ToString();
        //ShopGem.text = PlayerGems.ToString();
    }

    public void PlayerLevelUpdate()
    {
        PlayerLevelText.text = PlayerLevel.ToString();
        PlayerLevelSlide.value = PlayerEXP;
        PlayerLevelSlide.maxValue = PlayerExpToLevelUp;


        StatsPlayerLevelSlider.value = PlayerEXP;
        StatsPlayerLevelSlider.maxValue = PlayerExpToLevelUp;
        StatsPlayerLevel.text = PlayerLevel.ToString();
        StatsCurrentExp.text = PlayerEXP.ToString() + " / " + PlayerExpToLevelUp.ToString();

    }
    public void PartsOpenSell()
    {
        PIC.OpenSell();
    }
    public void OpenSwordShop(int from, int to)
    {
        //Equipments.SwordsOpenShop();
        Equipments.SwordsFilteredOpenShop(from, to);
    }
    public void OpenArmorShop(int from, int to)
    {
        //Equipments.ArmorsOpenShop();
        Equipments.ArmorsFilteredOpenShop(from, to);
    }
    public void OpenHelmetShop(int from, int to)
    {
        //Equipments.HelmetsOpenShop();
        Equipments.HelmetsFilteredOpenShop(from, to);
    }
    public void OpenShieldShop(int from, int to)
    {
        //Equipments.ShieldsOpenShop();
        Equipments.ShieldsFilteredOpenShop(from, to);
    }

    public void AddPlayerExp(int EXP)
    {
        PlayerEXP += EXP;
        SaveCharInfo(UserID, PlayerName);
    }

    public void OpenCPUWorldExhanger()
    {
        Exchanger.CPUsOpenShop();
    }
    public void OpenRAMWorldExhanger()
    {
        Exchanger.RAMsOpenShop();
    }
    public void OpenCPUFWorldExhanger()
    {
        Exchanger.CPUFsOpenShop();
    }
    public void OpenGPUWorldExhanger()
    {
        Exchanger.GPUsOpenShop();
    }
    public void OpenStorageWorldExhanger()
    {
        Exchanger.StoragesOpenShop();
    }
    public void OpenPSUWorldExhanger()
    {
        Exchanger.PSUsOpenShop();
    }
    public void OpenMBWorldExhanger()
    {
        Exchanger.MBsOpenShop();
    }
    public void OpenCaseWorldExhanger()
    {
        Exchanger.CasesOpenShop();
    }
    public void PlayerDeskName()
    {
        PlayerDesk.text = PlayerName + "'s Desk ";
        Playerui.text = PlayerName;
        StatsPlayerName.text = PlayerName;
    }

    public void StartQuest()
    {
        qp.startQuest();
    }

    public async void SaveCharInfo(string userID, string playerName)
    {
        // Check if the UserID and playerName are not null or empty
        if (string.IsNullOrEmpty(userID) || string.IsNullOrEmpty(playerName))
        {
            //UnityEngine.Debug.Log("UserID or playerName is null or empty.");
            return;
        }

        // Get a reference to the user's document under the 'users' collection
        DocumentReference userDocRef = FirebaseFirestore.DefaultInstance
            .Collection("users").Document(userID);

        // Create a dictionary to store the playerName
        Dictionary<string, object> playerNameData = new Dictionary<string, object>
        {
            //player info
            { "playerName", playerName },
            { "playerMoney", PlayerMoney },
            //{ "playerGems", PlayerGems },
            { "playerLevel", PlayerLevel },
            {"playerEXP", PlayerEXP},
            {"playerEXPNeedtoLevelUp", PlayerExpToLevelUp },
            {"playerTotalMoneyEarned", PlayerTotalMoney },
            //player stats

            {"playerAttack", PlayerAttackDamage },
            { "playerHealth", PlayerHealth },
            { "playerMana", PlayerMana },
            { "playerHealthRegen", PlayerHealthRegen },
            { "playerWalkSpeed", PlayerWalkSpeed },
            { "playerArmor", PlayerArmor },
            { "playerManaRegen", PlayerManaRegen },
            { "playerCriticalChance", PlayerCriticalChance },

            //for lan
            { "playerEasyModeWin", PlayerEasyModeWins },
            { "playerNormalModeWin", PlayerNormalModeWins },
            { "playerHardModeWin", PlayerHardModeWins },
            { "playerBestTimeEasy", PlayerBestTimeEasyMode },
            { "playerBestTimeNormal", PlayerBestTimeNormalMode },
            { "playerBestTimeHard", PlayerBestTimeHardMode },
        };  



        try
        {
            // Save the playerName data to Firestore
            await userDocRef.SetAsync(playerNameData, SetOptions.MergeAll);
            //UnityEngine.Debug.Log("PlayerName saved successfully.");
        }
        catch (System.Exception ex)
        {
            UnityEngine.Debug.LogError("Error saving playerName: " + ex.Message);
        }
    }

    

    public async void RetrievePlayerInfo(string userID)
    {

        // Check if the UserID is not null or empty
        if (string.IsNullOrEmpty(userID))
        {
            Debug.LogError("UserID is null or empty.");
            return;
        }

        // Get a reference to the user's document under the 'users' collection
        DocumentReference userDocRef = FirebaseFirestore.DefaultInstance
            .Collection("users").Document(userID);

        try
        {
            // Retrieve the document snapshot asynchronously
            DocumentSnapshot snapshot = await userDocRef.GetSnapshotAsync();

            // Check if the document exists
            if (snapshot.Exists)
            {
                // load player info
                PlayerName = snapshot.GetValue<string>("playerName");
                PlayerMoney = snapshot.GetValue<int>("playerMoney");
                //PlayerGems = snapshot.GetValue<int>("playerGems");
                PlayerLevel = snapshot.GetValue<int>("playerLevel");
                PlayerEXP = Math.Abs(snapshot.GetValue<int>("playerEXP"));
                PlayerExpToLevelUp = snapshot.GetValue<int>("playerEXPNeedtoLevelUp");
                PlayerTotalMoney = snapshot.GetValue<int>("playerTotalMoneyEarned");
                

                // load player stats
                PlayerAttackDamage = snapshot.GetValue<int>("playerAttack");
                PlayerHealth = snapshot.GetValue<int>("playerHealth");
                PlayerMana = snapshot.GetValue<int>("playerMana");
                PlayerHealthRegen = snapshot.GetValue<int>("playerHealthRegen");
                PlayerWalkSpeed = snapshot.GetValue<int>("playerWalkSpeed");
                PlayerArmor = snapshot.GetValue<int>("playerArmor");
                PlayerManaRegen = snapshot.GetValue<int>("playerManaRegen");
                PlayerCriticalChance = snapshot.GetValue<double>("playerCriticalChance");

                //load player lan
                PlayerEasyModeWins = snapshot.GetValue<int>("playerEasyModeWin");
                PlayerNormalModeWins = snapshot.GetValue<int>("playerNormalModeWin");
                PlayerHardModeWins = snapshot.GetValue<int>("playerHardModeWin");
                PlayerBestTimeEasyMode = snapshot.GetValue<float>("playerBestTimeEasy");
                PlayerBestTimeNormalMode = snapshot.GetValue<float>("playerBestTimeNormal");
                PlayerBestTimeHardMode = snapshot.GetValue<float>("playerBestTimeHard");

            }
            //else
            //{
            //    //Debug.LogWarning("Document does not exist for UserID: " + userID);
            //}

            //if(lastPlayerMoney == 0)
            //{
            //    lastPlayerMoney = PlayerMoney;
            //}

            
            ThePlayerStats();
            checkMoney();


        }
        catch(Exception ex)
        {
            Debug.LogError("Error retrieving playerName: " + ex.Message);
            //return;
        }
    }
    public void OnApplicationQuit()
    {
        SaveCharInfo(UserID, PlayerName);
    }

    public void GetPCStats(double AttackDamage, double Health, double Mana, double HealthRegen, double WalkSpeed, double Armor, double ManaRegen, double CriticalChance)
    {
        PlayerPCAttackDamage = AttackDamage;
        PlayerPCHealth = Health;
        PlayerPCMana = Mana;
        PlayerPCHealthRegen = HealthRegen;
        PlayerPCWalkSpeed = WalkSpeed;
        PlayerPCArmor = Armor;
        PlayerPCManaRegen = ManaRegen;
        PlayerPCCriticalChance = CriticalChance;
    }

    //public async Task LoadInUseItems()
    //{

    //    string[] categories = new string[] { "Monitor", "Mouse", "Keyboard", "Desk", "Background" };

    //    foreach (string category in categories)
    //    {
    //        // Check if the item in this category is both sold and in use

    //        // Load the in-use items from Firestore for this category
    //        //await LoadInSoldItemsFromFirestore(category);
    //        await LoadInUseItemsFromFirestore(category);


    //    }
    //}

    //public async Task LoadSoldItems()
    //{

    //    string[] categories = new string[] { "Monitor", "Mouse", "Keyboard", "Desk", "Background" };

    //    foreach (string category in categories)
    //    {
    //        // Check if the item in this category is both sold and in use

    //        // Load the in-use items from Firestore for this category
    //        await LoadInSoldItemsFromFirestore(category);


    //    }
    //}
    public void SaveSoldItems()
    {
        
            foreach (var item in so.ShopItems)
            {
                if (item.item.Sold == true && item.item.InUse == true)
                {
                    SaveItemToFirestore(item);
                    SaveUsedItemToFirestore(item);


                    if (equippedItemsByCategory.ContainsKey(item.item.Category))
                    {
                        equippedItemsByCategory[item.item.Category] = item;

                    }
                    else
                    {

                        equippedItemsByCategory.Add(item.item.Category, item);

                    }
                }
            }
        
        
    }
    //public void SetUserID(string userID)
    //{
    //    UserID = userID;
    //    PlayerPrefs.SetString("UserID", UserID);
    //    PlayerPrefs.Save();
    //}
    public void AddItemToTransfer(DecorationItem item)
    {
       DecorToTransfer.Add(item);
       OnDecorToTransferUpdated?.Invoke(item);
    }

    public void AddItemToTransfer(OtherWorldItem item)
    {
        OWitemsToTransfer.Add(item);
        OnOWItemsToTransferUpdated.Invoke(item);
    }
    public void AddItemToTransfer(InventoryItem item)
    {
        itemsToTransfer.Add(item);
        OnItemsToTransferUpdated?.Invoke(item);
    }
    //public void SaveDecorInitialItemss(List<DecorationItem> items)
    //{

    //    string jsonData = JsonUtility.ToJson(new DecorationItemList { Items = items });
    //    PlayerPrefs.SetString("SavedInitialItems", jsonData);
    //    PlayerPrefs.Save();
    //    //Debug.LogError("Data has been Saved");
    //}
    public async void SaveDecorInitialItems(List<DecorationItem> items)
    {
        // Convert the list of items to JSON
        string jsonData = JsonUtility.ToJson(new DecorationItemList { Items = items });

        // Generate a unique document ID (e.g., based on player ID or timestamp)
        //string documentID = "Player1DecInventory"; // Example document ID

        // Get a reference to the Firestore collection
       // CollectionReference collectionRef = FirebaseFirestore.DefaultInstance.Collection("users");

        // Create a new document with the generated document ID
        DocumentReference docRef = FirebaseFirestore.DefaultInstance.Collection(GameManager.instance.UserCollection).Document(GameManager.instance.UserID);

        CollectionReference SubDocRef = docRef.Collection("DecorationInventory");

        DocumentReference DecordocRef = SubDocRef.Document("DecorInvent");
        // Create a dictionary to store the data
        Dictionary<string, object> dataDict = new Dictionary<string, object>
    {
        { "items", jsonData }
    };

        // Set the data of the document
        await DecordocRef.SetAsync(dataDict);

        Debug.Log("decoration items saved to Firestore.");
    }

    public async void SaveComputerParts(List<InventoryItem> items)
    {
        // Convert the list of items to JSON
        string jsonData = JsonUtility.ToJson(new PartsItemList { Items = items });

        DocumentReference docRef = FirebaseFirestore.DefaultInstance.Collection(GameManager.instance.UserCollection).Document(GameManager.instance.UserID);

        CollectionReference SubDocRef = docRef.Collection("PartsInventory");

        DocumentReference DecordocRef = SubDocRef.Document("PartsInvent");
        // Create a dictionary to store the data
        Dictionary<string, object> dataDict = new Dictionary<string, object>
    {
        { "Parts", jsonData }
    };

        // Set the data of the document
        await DecordocRef.SetAsync(dataDict);

        Debug.Log("Parts items saved to Firestore.");
    }


    //public async void SaveComputer(List<Computer> items)
    //{
    //    // Convert the list of items to JSON
    //    string jsonData = JsonUtility.ToJson(new ComputerList { Items = items });

    //    DocumentReference docRef = FirebaseFirestore.DefaultInstance.Collection(GameManager.instance.UserCollection).Document(GameManager.instance.UserID);

    //    CollectionReference SubDocRef = docRef.Collection("ComputerInventory");

    //    DocumentReference DecordocRef = SubDocRef.Document("ComputerInvent");
    //    // Create a dictionary to store the data
    //    Dictionary<string, object> dataDict = new Dictionary<string, object>
    //{
    //    { "PC", jsonData }
    //};



    //    // Set the data of the document
    //    await DecordocRef.SetAsync(dataDict);

    //    Debug.Log("Computers saved to Firestore.");
    //}

    //public async void SavePCSO(PCSO pcso)
    //{
    //    // Convert the PCSO object to JSON
    //    string pcsoJson = JsonUtility.ToJson(pcso);

    //    // Get a reference to the Firestore document where you want to store the PCSO data
    //    DocumentReference docRef = FirebaseFirestore.DefaultInstance
    //        .Collection(GameManager.instance.UserCollection)
    //        .Document(GameManager.instance.UserID)
    //        .Collection("PCSOCollection")
    //        .Document("PCSOItem");

    //    // Create a dictionary to store the PCSO data
    //    Dictionary<string, object> dataDict = new Dictionary<string, object>
    //{
    //    { "pcsoData", pcsoJson }
    //};

    //    // Set the data in the Firestore document
    //    await docRef.SetAsync(dataDict);
    //}

    //tosave

    public List<string> pcsoDocumentIds = new List<string>();
    public string pcsothatinUse;
    public string pcsothatisModified;



    public List<string> AllDocumentIds = new List<string>();
    public List<string> SwordDocumentIds = new List<string>();
    public string SwordinUse;
    public List<string> ArmorDocumentIds = new List<string>();
    public string ArmorinUse;
    public List<string> HelmetDocumentIds = new List<string>();
    public string HelmetinUse;
    public List<string> ShieldDocumentIds = new List<string>();
    public string ShieldinUse;


    public List<string> MaterialsDocumentIds = new List<string>();

    public void LoadOtherWorldInventory()
    {
        StartCoroutine(OWC.OtherWorldInventory());

       
    }



    public void StopLoadWorldInventory()
    {
        StopCoroutine(OWC.OtherWorldInventory());
    }
    public void BackSingleItem(string category)
    {
        PartsController.HandleBackItem(category);
    }

    public string clickedInventoryItemID;
    public async Task SavePCSO(PCSO pcso)
    {
        // Convert the PCSO object to JSON
        string pcsoJson = JsonUtility.ToJson(pcso);

        // Get a reference to the Firestore document where you want to store the PCSO data
        DocumentReference docRef = FirebaseFirestore.DefaultInstance
            .Collection(GameManager.instance.UserCollection)
            .Document(GameManager.instance.UserID)
            .Collection("ComputersCollection")
            .Document();

        // Set the data in the Firestore document using the generated document ID
        await docRef.SetAsync(new Dictionary<string, object> { { "PC", pcsoJson } });

        pcsoDocumentIds.Insert(0, docRef.Id);

    }

    //public async Task SaveOWItemss(OtherWorldItemSO invItem, int quantity) // previous versions of save ow Items
    //{
    //    // Convert the PCSO object to JSON
    //    string itemJson = JsonUtility.ToJson(invItem);

    //    // Get a reference to the Firestore document where you want to store the PCSO data
    //    DocumentReference docRef = FirebaseFirestore.DefaultInstance
    //        .Collection(GameManager.instance.UserCollection)
    //        .Document(GameManager.instance.UserID)
    //        .Collection("OtherWorldInventory")
    //        .Document();

    //    // Set the data in the Firestore document using the generated document ID
    //    await docRef.SetAsync(new Dictionary<string, object> { { "Items", itemJson }, { "Quantity", quantity }});

    //    if(invItem.Category == "Sword")
    //    {
    //        SwordDocumentIds.Insert(0, docRef.Id);
    //    }
    //    if (invItem.Category == "Armor")
    //    {
    //        ArmorDocumentIds.Insert(0, docRef.Id);
    //    }
    //    if(invItem.Category == "Materials")
    //    {
    //        MaterialsDocumentIds.Insert(0, docRef.Id);
    //    }

    //    AllDocumentIds.Insert(0,docRef.Id);

    //    //pcsoDocumentIds.Insert(0, docRef.Id);

    //}

    //public async Task SaveOWItemsss(OtherWorldItemSO invItem, int quantity)
    //{
    //    // Convert the item object to JSON
    //    string itemJson = JsonUtility.ToJson(invItem);

    //    // Get a reference to the Firestore collection where you want to store the item data
    //    CollectionReference collectionRef = FirebaseFirestore.DefaultInstance
    //        .Collection(GameManager.instance.UserCollection)
    //        .Document(GameManager.instance.UserID)
    //        .Collection("OtherWorldInventory");


    //    QuerySnapshot querySnapshot = await collectionRef.GetSnapshotAsync();

    //    bool itemExists = false;

    //    foreach (var docSnapshot in querySnapshot.Documents)
    //    {
    //        // Deserialize the JSON data from the Firestore document
    //        string existingItemJson = docSnapshot.GetValue<string>("Items");
    //        //OtherWorldItemSO existingItem = JsonUtility.FromJson<OtherWorldItemSO>(existingItemJson);

    //        OtherWorldItemSO loadedItem = ScriptableObject.CreateInstance<OtherWorldItemSO>();

    //        // Deserialize the JSON data into the PCSO object
    //        JsonUtility.FromJsonOverwrite(existingItemJson, loadedItem);
    //        OtherWorldItem existingItem = new OtherWorldItem();

    //        existingItem.item = loadedItem;


    //        // Check if the category is "Materials" and the name matches
    //        if (existingItem.item.Category == "Materials" && existingItem.item.Name == invItem.Name)
    //        {
    //            itemExists = true;

    //            int existingQuantity = docSnapshot.GetValue<int>("Quantity");
    //            int newQuantity = existingQuantity + quantity;

    //            await docSnapshot.Reference.UpdateAsync(new Dictionary<string, object> { { "Quantity", newQuantity } });

               

    //        }
            
    //    }

    //    if(itemExists == false) 
    //    {
    //        // If no item with the same name exists, create a new document
    //        DocumentReference docRef = collectionRef.Document();

    //        await docRef.SetAsync(new Dictionary<string, object> { { "Items", itemJson }, { "Quantity", quantity } });

    //        // Update the document ID lists
    //        if (invItem.Category == "Sword")
    //        {
    //            SwordDocumentIds.Insert(0, docRef.Id);
    //        }
    //        else if (invItem.Category == "Armor")
    //        {
    //            ArmorDocumentIds.Insert(0, docRef.Id);
    //        }
    //        else if (invItem.Category == "Materials")
    //        {
    //            MaterialsDocumentIds.Insert(0, docRef.Id);
    //        }

    //        AllDocumentIds.Insert(0, docRef.Id);
    //    }

    //}

    public async Task SaveOWItems(OtherWorldItemSO invItem, int quantity)
    {
        // Convert the item object to JSON
        string itemJson = JsonUtility.ToJson(invItem);

        // Get a reference to the Firestore collection where you want to store the item data
        CollectionReference collectionRef = FirebaseFirestore.DefaultInstance
            .Collection(GameManager.instance.UserCollection)
            .Document(GameManager.instance.UserID)
            .Collection("OtherWorldInventory");

        QuerySnapshot querySnapshot = await collectionRef.GetSnapshotAsync();

        bool itemExists = false;
        string documentIdToDelete = null;

        foreach (var docSnapshot in querySnapshot.Documents)
        {
            // Deserialize the JSON data from the Firestore document
            string existingItemJson = docSnapshot.GetValue<string>("Items");
            OtherWorldItemSO loadedItem = ScriptableObject.CreateInstance<OtherWorldItemSO>();
            JsonUtility.FromJsonOverwrite(existingItemJson, loadedItem);
            OtherWorldItem existingItem = new OtherWorldItem
            {
                item = loadedItem
            };

            // Check if the category is "Materials" and the name matches
            if (existingItem.item.Category == "Materials" && existingItem.item.Name == invItem.Name)
            {
                itemExists = true;

                int existingQuantity = docSnapshot.GetValue<int>("Quantity");
                int newQuantity = existingQuantity + quantity;

                if (newQuantity > 0)
                {
                    await docSnapshot.Reference.UpdateAsync(new Dictionary<string, object> { { "Quantity", newQuantity } });
                }
                else
                {
                    // Store the document ID to delete after the loop
                    documentIdToDelete = docSnapshot.Id;
                    await docSnapshot.Reference.DeleteAsync();
                }

                break;
            }
        }

        if (!itemExists && quantity > 0)
        {
            // If no item with the same name exists, create a new document
            DocumentReference docRef = collectionRef.Document();

            await docRef.SetAsync(new Dictionary<string, object> { { "Items", itemJson }, { "Quantity", quantity } });

            // Update the document ID lists
            if (invItem.Category == "Sword")
            {
                SwordDocumentIds.Insert(0, docRef.Id);
            }
            else if (invItem.Category == "Armor")
            {
                ArmorDocumentIds.Insert(0, docRef.Id);
            }
            else if (invItem.Category == "Helmet")
            {
                HelmetDocumentIds.Insert(0, docRef.Id);
            }
            else if (invItem.Category == "Shield")
            {
                ShieldDocumentIds.Insert(0, docRef.Id);
            }
            else if (invItem.Category == "Materials")
            {
                MaterialsDocumentIds.Insert(0, docRef.Id);
            }

            AllDocumentIds.Insert(0, docRef.Id);
        }

        if (documentIdToDelete != null)
        {
            // Remove the document ID from the lists if it was deleted
            SwordDocumentIds.Remove(documentIdToDelete);
            ArmorDocumentIds.Remove(documentIdToDelete);
            HelmetDocumentIds.Remove(documentIdToDelete);
            ShieldDocumentIds.Remove(documentIdToDelete);
            MaterialsDocumentIds.Remove(documentIdToDelete);
            AllDocumentIds.Remove(documentIdToDelete);
        }
    }

    //to update
    //public async Task UpdatePCSOItemList()
    //{
    //    // Get a reference to the Firestore document for the PCSOItem
    //    DocumentReference docRef = FirebaseFirestore.DefaultInstance
    //        .Collection(GameManager.instance.UserCollection)
    //        .Document(GameManager.instance.UserID)
    //        .Collection("PCSOCollection")
    //        .Document("PCSOItem");

    //    // Run a Firestore transaction to update the PCSOItem document
    //    await FirebaseFirestore.DefaultInstance.RunTransactionAsync(async transaction =>
    //    {
    //        // Retrieve the current PCSO list data from Firestore
    //        DocumentSnapshot snapshot = await transaction.GetSnapshotAsync(docRef);
    //        List<object> currentPCSOList = snapshot.Exists ? (List<object>)snapshot.GetValue("pcsoList") : new List<object>();

    //        // Generate a new entry for the updated PCSO list (e.g., with the latest PCSO ID)
    //        Dictionary<string, object> newPCSOEntry = new Dictionary<string, object>
    //    {
    //        { "pcsoID", currentPCSOList.Count }, // Use a unique ID or index for each PCSO
    //        { "pcsoData", JsonUtility.ToJson(currentPCSOList[currentPCSOList.Count - 1]) } // Save the latest PCSO data
    //    };

    //        // Append the new PCSO entry to the current PCSO list
    //        currentPCSOList.Add(newPCSOEntry);

    //        // Update the PCSOItem document with the updated PCSO list
    //        transaction.Set(docRef, new Dictionary<string, object> { { "pcsoList", currentPCSOList } });

    //        // Return a success result (you can handle this as needed)
    //        return true;
    //    });
    //}

    //public async void SavePCSOList(List<PCSO> pcsoList)
    //{
    //    // Convert the list of PCSO objects to JSON
    //    string pcsoListJson = JsonUtility.ToJson(new PCList { PCSOs = pcsoList });

    //    // Get a reference to the Firestore document where you want to store the PCSO list
    //    DocumentReference docRef = FirebaseFirestore.DefaultInstance
    //        .Collection(GameManager.instance.UserCollection)
    //        .Document(GameManager.instance.UserID)
    //        .Collection("PCSOCollection")
    //        .Document("PCSOItem");

    //    // Create a dictionary to store the PCSO list data
    //    Dictionary<string, object> dataDict = new Dictionary<string, object>
    //{
    //    { "pcsoListData", pcsoListJson }
    //};

    //    // Set the data in the Firestore document
    //    await docRef.SetAsync(dataDict);
    //}

    public async Task LoadInUseItemsFromFirestore(string category)
    {
        try
        {
            CollectionReference equippedItemsRef = FirebaseFirestore.DefaultInstance.Collection(UserCollection)
            .Document(UserID).Collection("EquippedItems").Document("InUseItems").Collection(category);

            // Get all documents in the "InUseItems" subcollection for the specified category
            QuerySnapshot inUseItemsSnapshot = await equippedItemsRef.GetSnapshotAsync();

            // Iterate through the documents and handle each item
            foreach (DocumentSnapshot docSnapshot in inUseItemsSnapshot.Documents)
            {
                // Deserialize the item data
                //string jsonData = docSnapshot.GetValue<string>("itemData");
                string jsonName = docSnapshot.GetValue<string>("itemName");
                //Debug.Log("JSON Data: " + jsonData); // Debugging statement to inspect JSON data

                // Shop.Model.ShopItem item = new Shop.Model.ShopItem();
                Shop.Model.ShopItem item = SC.shopData.ShopItems.Find(a => a.item.Name == jsonName);
                // Deserialize the item part
                /*Shop.Model.ShopItem*/ //item = JsonUtility.FromJson<Shop.Model.ShopItem>(jsonData);

                // Create a new ShopItem instance and assign the deserialized item data


                item.item.InUse = true;
                       
                //Debug.Log("Loaded ShopItem: " + item.item.Name); // Debugging statement to confirm deserialization
                                                                 //Debug.Log("Category: " + item.item.Category);
                UpdateSprite(item);




            }


        }
        catch (System.Exception ex)
        {
            Debug.LogError("Error loading in-use items from Firestore: " + ex.Message);
        }
    }
    //public async Task LoadInSoldItemsFromFirestore(string category)
    //{
    //    try
    //    {
    //        CollectionReference equippedItemsRef = FirebaseFirestore.DefaultInstance.Collection(UserCollection)
    //        .Document(UserID).Collection("EquippedItems").Document("SoldItems").Collection(category);

    //        // Get all documents in the "InUseItems" subcollection for the specified category
    //        QuerySnapshot inUseItemsSnapshot = await equippedItemsRef.GetSnapshotAsync();

    //        // Iterate through the documents and handle each item
    //        foreach (DocumentSnapshot docSnapshot in inUseItemsSnapshot.Documents)
    //        {
    //            // Deserialize the item data
    //            string jsonData = docSnapshot.GetValue<string>("itemData");
    //            Debug.Log("JSON Data: " + jsonData); // Debugging statement to inspect JSON data

    //            // Shop.Model.ShopItem item = new Shop.Model.ShopItem();
    //            // Deserialize the item part
    //            Shop.Model.ShopItem item = JsonUtility.FromJson<Shop.Model.ShopItem>(jsonData);

    //            // Create a new ShopItem instance and assign the deserialized item data
    //            item.item.Sold = true;


    //            Debug.Log("Loaded ShopItem: " + item.item.Name); // Debugging statement to confirm deserialization
    //                                                             //Debug.Log("Category: " + item.item.Category);

    //            //UpdateSprite(item);



    //        }
    //    }
    //    catch (System.Exception ex)
    //    {
    //        Debug.LogError("Error loading in-use items from Firestore: " + ex.Message);
    //    }
    //}
    public Shop.Model.ShopItem item;
    //public async Task LoadInSoldItemsFromFirestore(string[] categories)
    //{
    //    try
    //    {
    //        foreach (string category in categories)
    //        {
    //            CollectionReference equippedItemsRef = FirebaseFirestore.DefaultInstance.Collection(UserCollection)
    //                .Document(UserID).Collection("EquippedItems").Document("SoldItems").Collection(category);

    //            // Get all documents in the "InUseItems" subcollection for the specified category
    //            QuerySnapshot inUseItemsSnapshot = await equippedItemsRef.GetSnapshotAsync();

    //            // Iterate through the documents and handle each item
    //            foreach (DocumentSnapshot docSnapshot in inUseItemsSnapshot.Documents)
    //            {
    //                // Deserialize the item data
    //                string jsonData = docSnapshot.GetValue<string>("itemData");
    //                //Debug.Log("JSON Data: " + jsonData); // Debugging statement to inspect JSON data

    //                // Deserialize the item data into a ShopItem object
    //                /*Shop.Model.ShopItem*/ item = JsonUtility.FromJson<Shop.Model.ShopItem>(jsonData);

    //                // Update item properties (e.g., set Sold flag)
    //                item.item.Sold = true;

    //                //Debug.Log("Loaded ShopItem: " + item.item.Name); // Debugging statement to confirm deserialization
    //            }

    //            await LoadInUseItemsFromFirestore(category);
    //        }

    //    }
    //    catch (System.Exception ex)
    //    {
    //        Debug.LogError("Error loading sold items from Firestore: " + ex.Message);
    //    }
    //}

    public async Task LoadInSoldItemsFromFirestore(string[] categories)
    {
        try
        {
            foreach (string category in categories)
            {
                CollectionReference equippedItemsRef = FirebaseFirestore.DefaultInstance.Collection(UserCollection)
                    .Document(UserID).Collection("EquippedItems").Document("SoldItems").Collection(category);

                // Get all documents in the "InUseItems" subcollection for the specified category
                QuerySnapshot inUseItemsSnapshot = await equippedItemsRef.GetSnapshotAsync();

                // Iterate through the documents and handle each item
                foreach (DocumentSnapshot docSnapshot in inUseItemsSnapshot.Documents)
                {
                    // Capture the item data on a background thread
                    //string jsonData = docSnapshot.GetValue<string>("itemData");

                    //// Switch to the main thread for Unity-specific operations
                    //await Task.Factory.StartNew(() =>
                    //{
                    //    // Deserialize the item data into a ShopItem object on the main thread
                    //    Shop.Model.ShopItem item = JsonUtility.FromJson<Shop.Model.ShopItem>(jsonData);

                    //    // Update item properties (e.g., set Sold flag)
                    //    item.item.Sold = true;

                    //    // Any other Unity-related operations go here

                    //}, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.FromCurrentSynchronizationContext());


                    string itemName = docSnapshot.GetValue<string>("itemName");
                    // Update the local achievement list based on the data loaded from Firebase
                    Shop.Model.ShopItem item = SC.shopData.ShopItems.Find(a => a.item.Name == itemName);
                    
                    item.item.Sold = true;



                }

                // Load in-use items from Firestore
                await LoadInUseItemsFromFirestore(category);
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Error loading sold items from Firestore: " + ex.Message);
        }
    }




    private void UpdateSprite(Shop.Model.ShopItem item)
    {
        if (equippedItemsByCategory.ContainsKey(item.item.Category))
        {
            equippedItemsByCategory[item.item.Category] = item;

        }
        else
        {

            equippedItemsByCategory.Add(item.item.Category, item);

        }


        switch (item.item.Category)
        {
            case "Monitor":
                Monitor.sprite = item.item.ItemImage;
                break;
            case "Mouse":
                Mouse.sprite = item.item.ItemImage;
                break;
            case "MousePad":
                MousePad.sprite = item.item.ItemImage;
                break;
            case "Keyboard":
                Keyboard.sprite = item.item.ItemImage;
                break;
            case "Desk":
                Desk.sprite = item.item.ItemImage;
                break;
            case "Background":
                Background.sprite = item.item.ItemImage;
                break;
            default:
                Debug.LogWarning("Unsupported item category: " + item.item.Category);
                break;
        }
    }
     public async void SaveItemToFirestore(Shop.Model.ShopItem item)//needfix
    {
        try
        {
            // Get a reference to the Firestore document
            DocumentReference docRef = FirebaseFirestore.DefaultInstance.Collection(UserCollection).Document(UserID);

            // Create a reference to the subcollection for equipped items
            CollectionReference equippedItemsRef = docRef.Collection("EquippedItems");

            // Check if the item is sold
            if (item.item.Sold)
            {
                // Determine the subcollection name
                string subcollectionName = "SoldItems";

                // Create a reference to the subcollection
                CollectionReference itemSubcollectionRef = equippedItemsRef.Document(subcollectionName).Collection(item.item.Category);

                // Check if an item with the same category and name already exists
                QuerySnapshot querySnapshot = await itemSubcollectionRef.WhereEqualTo("itemName", item.item.Name).GetSnapshotAsync();
                foreach (DocumentSnapshot documentSnapshot in querySnapshot.Documents)
                {
                    // Delete the previous item with the same category and name
                    await documentSnapshot.Reference.DeleteAsync();
                    //Debug.Log("Previous item deleted: " + documentSnapshot.Id);
                }

                // Serialize the item data on the main thread
                string jsonData = null;
                MainThreadDispatcher.Enqueue(() =>
                {
                    jsonData = JsonUtility.ToJson(item);
                });

                // Wait until the JSON data is ready
                while (jsonData == null)
                {
                    await Task.Yield();
                }

                // Create a new document for the item
                DocumentReference itemDocRef = itemSubcollectionRef.Document();

                // Serialize the item data
                //string jsonData = JsonUtility.ToJson(item);

                // Save the item data to Firestore
                await itemDocRef.SetAsync(new Dictionary<string, object>
            {
                { "itemData", jsonData },
                { "itemName", item.item.Name } // Include item name for future deletion checks
            });

                //Debug.Log("Item saved to Firestore: " + item.item.Name);
            }
            else
            {
                Debug.LogWarning("Item is not sold. Not saving to Firestore.");
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Error saving item to Firestore: " + ex.Message);
        }
    }
    public async void SaveUsedItemToFirestore(Shop.Model.ShopItem item)
    {
        try
        {
            // Get a reference to the Firestore document
            DocumentReference docRef = FirebaseFirestore.DefaultInstance.Collection(UserCollection).Document(UserID);

            // Create a reference to the subcollection for equipped items
            CollectionReference equippedItemsRef = docRef.Collection("EquippedItems");


        if (item.item.InUse)
        {
            // Determine the subcollection based on whether the item is sold or in use
            string subcollectionName = "InUseItems";

            // Create a reference to the subcollection
            CollectionReference itemSubcollectionRef = equippedItemsRef.Document(subcollectionName).Collection(item.item.Category);

            await DeletePreviousInUseItem(item.item.Category, equippedItemsRef);

                // Serialize the item data on the main thread
                string jsonData = null;
                MainThreadDispatcher.Enqueue(() =>
                {
                    jsonData = JsonUtility.ToJson(item);
                });

                // Wait until the JSON data is ready
                while (jsonData == null)
                {
                    await Task.Yield();
                }

                // Create a new document for the item
                DocumentReference itemDocRef = itemSubcollectionRef.Document();

            // Serialize the item data
           // string jsonData = JsonUtility.ToJson(item);

            // Save the item data to Firestore
            await itemDocRef.SetAsync(new Dictionary<string, object>
            {
                { "itemData", jsonData },
                { "itemName", item.item.Name }
            });

            //Debug.Log("Item used and save to Firestore: " + item.item.Name);
        }
        else
        {
            Debug.LogWarning("Item is neither sold nor in use. Not saving to Firestore.");
        }
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Error saving item to Firestore: " + ex.Message);
        }
    }
        private async Task DeletePreviousInUseItem(string category, CollectionReference equippedItemsRef)
    {
        try
        {
            // Create a reference to the "InUseItems" subcollection for the specific category
            CollectionReference inUseItemsRef = equippedItemsRef.Document("InUseItems").Collection(category);

            // Query to get the previous item in use
            QuerySnapshot snapshot = await inUseItemsRef.GetSnapshotAsync();
            foreach (DocumentSnapshot docSnap in snapshot.Documents)
            {
                // Delete the previous item in use
                await docSnap.Reference.DeleteAsync();
                //Debug.Log("Previous item in use deleted: " + docSnap.Id);
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Error deleting previous item in use: " + ex.Message);
        }
    }
    private void Start()
    {
        //UserID = PlayerPrefs.GetString("UserID", "");
        //ClearPlayerPrefsIfUserIDNotFound(UserID);
        
        scene.LoadScene();
        //SceneManager.LoadScene(1, LoadSceneMode.Additive);
        EnableDefault();

        //Debug.Log(UserID);

        if (partsToCollect != null)
        {
            // Iterate over all children of the partsToCollect GameObject
            foreach (Transform child in partsToCollect)
            {
                // Add each child GameObject to the PartsToCollect list
                PartsToCollect.Add(child.gameObject);
            }
        }
        // player page

        //StartCoroutine(DropPartsInRandomPosition());



        //EnableDefault();


        //DC.LoadInitialItems();
        //FirstDrop();
        //await Task.Delay(3000);
        //DropAllPartsInRandomPositions();

        //StartCoroutine(FirstDropAndPosition());
        ThePlayerStats();



    }

    public async Task LoadGameObjectsFromFirestore()
    {
        try
        {
           
            // Get a reference to the Firestore document
            DocumentReference docRef = FirebaseFirestore.DefaultInstance.Collection(UserCollection).Document(UserID);

            // Create a reference to the subcollection for parts
            CollectionReference partsCollectionRef = docRef.Collection("ToCollectPackages");

            // Clear existing objects in partsToCollect
            ClearPartsToCollect();

            // Query to get all documents in the parts collection
            QuerySnapshot snapshot = await partsCollectionRef.GetSnapshotAsync();

            foreach (DocumentSnapshot documentSnapshot in snapshot.Documents)
            {
                // Deserialize the data from Firestore
                string jsonData = documentSnapshot.GetValue<string>("data");
                GameObjectData data = JsonUtility.FromJson<GameObjectData>(jsonData);

               
                // Instantiate or update the GameObject in partsToCollect based on the retrieved data
                InstantiateOrUpdateGameObject(data);
               
                
            }

            Debug.Log("GameObjects loaded from Firestore.");
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Error loading GameObjects from Firestore: " + ex.Message);
        }
    }

    private void ClearPartsToCollect()
    {
        // Destroy all existing children of partsToCollect
        
        foreach (Transform child in partsToCollect)
        {
            Destroy(child.gameObject);
        }
        PartsToCollect.Clear();
    }

    private void InstantiateOrUpdateGameObject(GameObjectData data)
    {
        partsCollectPrefab.parts = data.parts;
        partsCollectPrefab.Quantity = data.Quantity;
        // Instantiate or update the GameObject based on the retrieved data
        var obj = Instantiate(partsCollectPrefab, data.position, Quaternion.identity, partsToCollect);
        obj.name = data.name;

        obj.transform.localScale = data.scale;

        PartsToCollect.Add(obj.gameObject);
      

        Debug.Log("GameObject instantiated/updated: " + obj.name);
    }


    public async void CollectObject(GameObject collectedObject)
    {
        if (collectedObject != null && PartsToCollect.Contains(collectedObject))
        {
            // Remove the collected object from PartsToCollect
            PartsToCollect.Remove(collectedObject);

            // Save updated PartsToCollect list to Firestore
            await Task.Delay(5000);
            SaveGameObjectsToFirestore(PartsToCollect);

            // Optionally, destroy or deactivate the collected object
            //Destroy(collectedObject);
        }
        else
        {
            Debug.LogWarning("The collected object is null or not found in PartsToCollect list.");
        }
    }
    //this is the start when click play button
    public async void AtTheStart()
    {
       
        if (UserID != "")
        {

            UIExplore.SetActive(true);
            //QuestUI.SetActive(true);

            //string[] categories = new string[] { "Monitor", "Mouse","MousePad", "Keyboard", "Desk", "Background" };
            string[] categories = new string[] { "Background", "Desk" , "Keyboard", "Monitor", "Mouse", "MousePad" };
            DisableFirstall();
            //LoadCharacter();
            charBuilder.LoadSavedData();
           
            // If initial items have already been saved, load in-use items
            //await Task.Delay(500);
            ////await LoadInUseItems();
            await Task.Delay(500);
            //await LoadSoldItems();
            await LoadInSoldItemsFromFirestore(categories);
            await Task.Delay(500);
            SaveSoldItems();
            await Task.Delay(500);
            await DecorMan.LoadAllDecorationsFromFirestore();

            RetrievePlayerInfo(UserID);

                if (packagescollected != 0)
                {
                    //SaveGameObjectsToFirestore(PartsToCollect);
                    await LoadGameObjectsFromFirestore();
                }

        }
    }
    public SceneLoader scene;
    public void ClearPlayerPrefsIfUserIDNotFound(string userID)
    {
        if (userID == "")
        {
            Debug.Log("UserID is null or empty.");
            scene.LoadScene();
            //SceneManager.LoadScene(1, LoadSceneMode.Additive);
            EnableDefault();
        }
        else
        {
            FirebaseFirestore db = FirebaseFirestore.DefaultInstance;

            // Get a reference to the user's document in Firestore
            DocumentReference userDocRef = db.Collection(UserCollection).Document(userID);

            // Check if the document exists in Firestore
            userDocRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
            {
                if (task.IsCompleted)
                {
                    DocumentSnapshot snapshot = task.Result;
                    if (!snapshot.Exists)
                    {
                        // User document not found, clear player prefs
                        FirebaseController.Instance.deleteLogin();
                        PlayerPrefs.DeleteAll();
                        UserID = "";
                        
                        Debug.Log("PlayerPrefs cleared because userID was not found in Firestore.");

                        scene.LoadScene();
                        //SceneManager.LoadScene(1, LoadSceneMode.Additive);
                        EnableDefault();
                    }
                    else
                    {
                        Debug.Log("UserID found in Firestore. PlayerPrefs not cleared.");
                        scene.LoadScene();
                        //SceneManager.LoadScene(1, LoadSceneMode.Additive);
                        EnableDefault();
                    }
                }
                else if (task.IsFaulted)
                {
                    Debug.LogError("Error checking if userID exists in Firestore: " + task.Exception);
                }
            });
        }

       
    }

    

    public void DisableFirstall()
    {
        if (UserID != null && UserID != "" && !UserID.IsUnityNull())
        {
            foreach (var item in so.ShopItems)
            {

                item.item.InUse = false;
                item.item.Sold = false;
                //Debug.LogError("disabled");

            }
        }
    }
    public void EnableDefault()
    {
        foreach (var item in so.ShopItems)
        {
            if (UserID == null || UserID == "") { 
                if (item.item.Price == 0)
                {
                    item.item.Sold = true;
                    item.item.InUse = true;
                }
                else
                {
                    item.item.Sold = false;
                    item.item.InUse = false;
                }
            }
        }
       
    }

    // Update is called once per frame
    //bool done = false;
    void Update()
    {
        UserIDTxt.text = UserID;
        Playerui.text = PlayerName;
        StatsPlayerName.text = PlayerName;
        CurrencyText();
        PlayerLevelUpdate();
        

        if(UserID != "")
        {
            OpenLogouMenu(true);
        }
        else
        {
            OpenLogouMenu(false);
        }

        

        if (UserID != "" && FirebaseController.Instance.isSigned == true)
        {
            RetrievePlayerInfo(UserID);


            //if (done == false)
            //{
            //    await Task.Delay(1000);
            //    AchievementManager.instance.CheckAchievements();
            //    //AchievementManager.instance.DisplayAchievements();
            //    done = true;
            //}
            //InGamePanel.gameObject.SetActive(true);

        }
        else
        {
            //InGamePanel.gameObject.SetActive(false);
        }



    }
    public async void OpenLogouMenu(bool tralse)
    {


        //Debug.LogError("OPEEENED");
        //Debug.LogError(tralse);
        await Task.Delay(1000);
        InGamePanel.SetActive(tralse);

        if (UserSetup.instance != null)
        {
            UserSetup.instance.logoutButton.gameObject.SetActive(tralse);
        }
       



    }
    public void UpdateShop(string category)
    {
        if(SC.shopBuy.ToggleTF == true && SC.shopBuy.ToggleBSE == false)
        {
            SC.ShowCategory(category);

        }
        else if (SC.shopBuy.ToggleTF == true && SC.shopBuy.ToggleBSE == true)
        {
            if(SC.shopBuy.filteredBSE == "All")
            {
                SC.ShowAllCategory();

            }else if (SC.shopBuy.filteredBSE == "Unsold")
            {
                SC.ShowBuy();
            }
            else if (SC.shopBuy.filteredBSE == "Sold")
            {
                SC.ShowSold();
            }
            else if (SC.shopBuy.filteredBSE == "Equipped")
            {
                SC.ShowEquipped();
            }
        }
        else
        {
            SC.ShowAllCategory();
        }
    }
    private void Awake()
    {
        //questEvents = new QuestEvent();
        if (instance == null)
        {
            
            instance = this;
            DontDestroyOnLoad(gameObject);
            
        }
        else
        {
            // If another instance already exists, destroy this one
            Destroy(gameObject);
        }
        //questEvents = new QuestEvent();

    }
    public void DecorUseEnable( DecorationItem Item)
    {
        DecorMan.UseDecor( Item);
    }

    public void LoadCharacter()
    {
        charBuilder.LoadSavedData();
    }
    public void DecorClicked()
    {
        DecorMan.DecorClicked();
    }
    public void DecorUnClicked()
    {
        DecorMan.UnclickedDecor();
    }
    public void RemoveItem()
    {
        DecorMan.DecorRemove();
    }

    public PartsCollect partsCollectPrefab;
    public float dropDuration = 5f;
    public Vector2 spawnAreaSize = new Vector2(5f, 5f);
    public Transform partsToCollect;
    public List<PartsSO> partsList = new List<PartsSO>();
    public List<GameObject> PartsToCollect = new List<GameObject>();



    //public List<PartsCollect> parts = new List<PartsCollect>();


    //private IEnumerator FirstDropAndPosition()
    //{
    //    if (partsList != null && partsList.Count > 0)
    //    {
    //        // Create and store all parts
    //        for (int i = 0; i < partsList.Count; i++)
    //        {
    //            // Instantiate a new PartsCollect object for each part in the list
    //            PartsCollect newPartsCollect = Instantiate(partsCollectPrefab);

    //            // Assign the current part from the list to the new instance
    //            newPartsCollect.currentParts = partsList[i];

    //            // Set the quantity to 1
    //            newPartsCollect.currentQuantity = 1;

    //            // Assign the new PartsSO and Quantity
    //            newPartsCollect.parts = newPartsCollect.currentParts;
    //            newPartsCollect.Quantity = newPartsCollect.currentQuantity;

    //            // Set the sprite of the object based on the new PartsSO
    //            // newPartsCollect.GetComponent<SpriteRenderer>().sprite = newPartsCollect.packageimage;

    //            // Add the new instance to the parts list
    //            parts.Add(newPartsCollect);
    //        }

    //        // Wait for the specified delay
    //        yield return new WaitForSeconds(3.0f);

    //        // Drop all parts in random positions
    //        Bounds parentBounds = GetParentBounds();

    //        foreach (var part in parts)
    //        {
    //            // Calculate a random position within the bounds of the parent object
    //            Vector3 randomPosition = new Vector3(
    //                UnityEngine.Random.Range(parentBounds.min.x, parentBounds.max.x),
    //                UnityEngine.Random.Range(parentBounds.min.y, parentBounds.max.y),
    //                0f);

    //            // Set the position of the part to the calculated random position
    //            part.transform.position = randomPosition;

    //            // Set the parent of the instantiated prefab
    //            if (partsToCollect != null)
    //            {
    //                part.transform.SetParent(partsToCollect);
    //            }
    //        }
    //    }
    //    else
    //    {
    //        Debug.LogWarning("PartsManager or partsList is not assigned or empty.");
    //    }
    //}
    //public async void SaveGameObjectsToFirestore(List<GameObject> gameObjects)
    //{
    //    try
    //    {
    //        // Get a reference to the Firestore document
    //        DocumentReference docRef = FirebaseFirestore.DefaultInstance.Collection(UserCollection).Document(UserID);

    //        // Create a reference to the subcollection for parts
    //        CollectionReference partsCollectionRef = docRef.Collection("ToCollectPackages");

    //        // Clear existing data in Firestore
    //        await DeleteExistingParts(partsCollectionRef);

    //        // Iterate over the gameObjects and save their data to Firestore
    //        foreach (GameObject obj in gameObjects)
    //        {
    //            // Create a new GameObjectData instance
    //            GameObjectData data = new GameObjectData
    //            {
    //                name = obj.name,
    //                position = obj.transform.position,
    //                parts = obj.gameObject.GetComponent<PartsCollect>().parts,
    //                Quantity = obj.gameObject.GetComponent<PartsCollect>().Quantity,
    //                scale = new Vector3(0.71716f, 0.71716f, 0.71716f)

    //            };

    //            string jsonData = null;
    //            MainThreadDispatcher.Enqueue(() =>
    //            {
    //                jsonData = JsonUtility.ToJson(item);
    //            });

    //            // Wait until the JSON data is ready
    //            while (jsonData == null)
    //            {
    //                await Task.Yield();
    //            }
    //            // Serialize the GameObjectData
    //            //string jsonData = JsonUtility.ToJson(data);

    //            // Create a new document for the GameObject
    //            DocumentReference partDocRef = partsCollectionRef.Document();

    //            // Save the serialized data to Firestore
    //            await partDocRef.SetAsync(new Dictionary<string, object>
    //            {
    //                { "data", jsonData }
    //            });

    //            Debug.Log("GameObject saved to Firestore: " + obj.name);
    //        }
    //    }
    //    catch (System.Exception ex)
    //    {
    //        Debug.LogError("Error saving GameObject to Firestore: " + ex.Message);
    //    }
    //}

    public async void SaveGameObjectsToFirestore(List<GameObject> gameObjects)
    {
        try
        {
            // Get a reference to the Firestore document
            DocumentReference docRef = FirebaseFirestore.DefaultInstance.Collection(UserCollection).Document(UserID);

            // Create a reference to the subcollection for parts
            CollectionReference partsCollectionRef = docRef.Collection("ToCollectPackages");

            // Clear existing data in Firestore
            await DeleteExistingParts(partsCollectionRef);

            // Iterate over the gameObjects and save their data to Firestore
            foreach (GameObject obj in gameObjects)
            {
                // Create a local variable to store the JSON data
                string jsonData = null;

                // Use the dispatcher to ensure operations are on the main thread
                MainThreadDispatcher.Enqueue(() =>
                {
                    // Create a new GameObjectData instance
                    GameObjectData data = new GameObjectData
                    {
                        name = obj.name,
                        position = obj.transform.position,
                        parts = obj.GetComponent<PartsCollect>().parts,
                        Quantity = obj.GetComponent<PartsCollect>().Quantity,
                        scale = new Vector3(0.71716f, 0.71716f, 0.71716f)
                    };

                    // Serialize the GameObjectData
                    jsonData = JsonUtility.ToJson(data);
                });

                // Wait until the JSON data is ready
                while (jsonData == null)
                {
                    await Task.Yield();
                }

                // Create a new document for the GameObject
                DocumentReference partDocRef = partsCollectionRef.Document();

                // Save the serialized data to Firestore
                await partDocRef.SetAsync(new Dictionary<string, object>
            {
                { "data", jsonData }
            });

               // Debug.Log("GameObject saved to Firestore: " + obj.name);
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Error saving GameObject to Firestore: " + ex.Message);
        }
    }

    private async Task DeleteExistingParts(CollectionReference partsCollectionRef)
    {
        try
        {
            // Query to get the existing parts
            QuerySnapshot snapshot = await partsCollectionRef.GetSnapshotAsync();
            foreach (DocumentSnapshot docSnap in snapshot.Documents)
            {
                // Delete the existing part
                await docSnap.Reference.DeleteAsync();
                Debug.Log("Existing part deleted: " + docSnap.Id);
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Error deleting existing parts: " + ex.Message);
        }
    }


    private IEnumerator DropPartsInRandomPosition() // orig
    {
        while (true)
        {
            yield return new WaitForSeconds(dropDuration);

            // Get the bounds of the parent object
            Bounds parentBounds = GetParentBounds();

            // Calculate a random position within the bounds of the parent object
            Vector3 randomPosition = new Vector3(
                UnityEngine.Random.Range(parentBounds.min.x, parentBounds.max.x),
                UnityEngine.Random.Range(parentBounds.min.y, parentBounds.max.y),
                0f);

            // Instantiate PartsCollect prefab at the calculated random position
            PartsCollect newPartsCollect = Instantiate(partsCollectPrefab, randomPosition, Quaternion.identity);

            // Set the parent of the instantiated prefab
            if (partsToCollect != null)
            {
                newPartsCollect.transform.SetParent(partsToCollect);
            }
        }
    }

    public Bounds GetParentBounds()
    {
        // Get the renderer component of the parent object
        Collider2D parentCollider = partsToCollect.GetComponent<Collider2D>();


        // If the parent object has a renderer component, return its bounds
        if (parentCollider != null)
        {
            return parentCollider.bounds;
        }
        else
        {
            // If the parent object does not have a renderer, return default bounds
            Debug.LogWarning("Parent object does not have a renderer component. Using default bounds.");
            return new Bounds(Vector3.zero, Vector3.one);
        }
    }

    public GameObject floatingTextPrefab;
    public void ShowFloatingText(string text)
    {
        if (floatingTextPrefab != null && notifpPopUpParent != null)
        {
            GameObject floatingText = Instantiate(floatingTextPrefab, transform.position, Quaternion.identity, notifpPopUpParent);
            NotifText floatingTextComponent = floatingText.GetComponent<NotifText>();

            floatingTextComponent.SetText(text, Color.white);

        }
    }
    public void ShowPopUp(PartsCollect inventoryItem, bool cpuBuy)
    {
        GameObject newShopPopup = Instantiate(notifpopup, notifpPopUpParent); // Instantiate the popup as a child of the designated parent

        LeanTween.moveLocal(newShopPopup, new Vector3(264.7f, 133.7f, 0f), 2f)
            .setEase(LeanTweenType.easeOutExpo)
            .setOnComplete(() => HidePopUp(newShopPopup));

        UpdateShopPopup(newShopPopup, inventoryItem, cpuBuy);
    }

    private void HidePopUp(GameObject shopPopup)
    {
        LeanTween.moveLocal(shopPopup, new Vector3(508.4f, 133.7f, 0f), 1f)
            .setDelay(1f)
            .setEase(LeanTweenType.easeOutExpo)
            .setOnComplete(() => Destroy(shopPopup));
    }

    private void UpdateShopPopup(GameObject shopPopup, PartsCollect inventoryItem, bool cpuBuy)
    {
        Image[] images = shopPopup.GetComponentsInChildren<Image>();
        TMP_Text[] texts = shopPopup.GetComponentsInChildren<TMP_Text>();

        foreach (var image in images)
        {
            // Check conditions or naming conventions to identify the image elements you need to update
            if (image.gameObject.name == "ItemImage") // Assuming the GameObject name is set in the editor
            {
                image.sprite = inventoryItem.parts.ItemImage;
                break; // Assuming there's only one image to update
            }
        }

        foreach (var text in texts)
        {
            // Check conditions or naming conventions to identify the text elements you need to update
            if (text.gameObject.name == "Quantity") // Assuming the GameObject name is set in the editor
            {
                text.text = inventoryItem.Quantity.ToString() + "X";
            }
            else if (text.gameObject.name == "ItemName") // Assuming the GameObject name is set in the editor
            {
                text.text = inventoryItem.parts.Name;
            }
            else if (text.gameObject.name == "Category")
            {

                text.text = inventoryItem.parts.Category;
            }


            if(cpuBuy == true)
            {
                if (text.gameObject.name == "Info")
                {

                    text.text = "Item has been Delivered To Your Home";
                }
            }
            else
            {
                if (text.gameObject.name == "Info")
                {

                    text.text = "Item Has been added to your hardware inventory";
                }
            }

        }
    }


    //for equipements
    public void ShowPopUpEquipments(OtherWorldItem inventoryItem)
    {
        GameObject newShopPopup = Instantiate(Equipnotifpopup, notifpPopUpParent); // Instantiate the popup as a child of the designated parent

        LeanTween.moveLocal(newShopPopup, new Vector3(264.7f, 133.7f, 0f), 2f)
            .setEase(LeanTweenType.easeOutExpo)
            .setOnComplete(() => HidePopUpEquipments(newShopPopup));

        UpdateShopPopupEquipments(newShopPopup, inventoryItem);
    }

    private void HidePopUpEquipments(GameObject shopPopup)
    {
        LeanTween.moveLocal(shopPopup, new Vector3(508.4f, 133.7f, 0f), 1f)
            .setDelay(1f)
            .setEase(LeanTweenType.easeOutExpo)
            .setOnComplete(() => Destroy(shopPopup));
    }

    private void UpdateShopPopupEquipments(GameObject shopPopup, OtherWorldItem inventoryItem)
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
            // Check conditions or naming conventions to identify the text elements you need to update
            if (text.gameObject.name == "Quantity") // Assuming the GameObject name is set in the editor
            {
                text.text = inventoryItem.quantity.ToString() + "X";
            }
            else if (text.gameObject.name == "ItemName") // Assuming the GameObject name is set in the editor
            {
                text.text = inventoryItem.item.Name;
            }
            else if (text.gameObject.name == "Category")
            {

                text.text = inventoryItem.item.Category;
            }

           

        }
    }
   

    //for Items
    public void ShowPopUpItems(Shop.Model.ShopItem inventoryItem)
    {
        GameObject newShopPopup = Instantiate(Itemsnotifpopup, notifpPopUpParent); // Instantiate the popup as a child of the designated parent

        LeanTween.moveLocal(newShopPopup, new Vector3(264.7f, 133.7f, 0f), 2f)
            .setEase(LeanTweenType.easeOutExpo)
            .setOnComplete(() => HidePopUpItems(newShopPopup));

        UpdateShopPopupItems(newShopPopup, inventoryItem);
    }
    public void ShowPopUpItems(DecorationItem inventoryItem)
    {
        GameObject newShopPopup = Instantiate(Itemsnotifpopup, notifpPopUpParent); // Instantiate the popup as a child of the designated parent

        LeanTween.moveLocal(newShopPopup, new Vector3(264.7f, 133.7f, 0f), 2f)
            .setEase(LeanTweenType.easeOutExpo)
            .setOnComplete(() => HidePopUpItems(newShopPopup));

        UpdateShopPopupItems(newShopPopup, inventoryItem);
    }

    private void HidePopUpItems(GameObject shopPopup)
    {
        LeanTween.moveLocal(shopPopup, new Vector3(508.4f, 133.7f, 0f), 1f)
            .setDelay(1f)
            .setEase(LeanTweenType.easeOutExpo)
            .setOnComplete(() => Destroy(shopPopup));
    }

    private void UpdateShopPopupItems(GameObject shopPopup, Shop.Model.ShopItem inventoryItem)
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
            if (text.gameObject.name == "Quantity") // Assuming the GameObject name is set in the editor
            {
                text.text = "";
            }
           else if (text.gameObject.name == "ItemName") // Assuming the GameObject name is set in the editor
            {
                text.text = inventoryItem.item.Name;
            }
            else if (text.gameObject.name == "Category")
            {

                text.text = inventoryItem.item.Category;
            }
             else if (text.gameObject.name == "Info")
             {

                    text.text = "This " + inventoryItem.item.Category + " Has been sold and Equipped";
             }
            

        }
    }

    private void UpdateShopPopupItems(GameObject shopPopup, DecorationItem inventoryItem )
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
            if (text.gameObject.name == "Quantity") // Assuming the GameObject name is set in the editor
            {
                text.text = inventoryItem.quantity.ToString() + "X";
            }

            else if (text.gameObject.name == "ItemName") // Assuming the GameObject name is set in the editor
            {
                text.text = inventoryItem.item.Name;
            }
            else if (text.gameObject.name == "Category")
            {

                text.text = inventoryItem.item.Category;
            }


            if (text.gameObject.name == "Info")
            {

                text.text = "This Decoration has been sent to Decoration Inventory";
            }

        }

    }

    //for Quest Scripts

    public void OpenDesktopMyPC()
    {
        if(OnExploreDesktopQuest == true)
        {
            DialogueManager.GetInstance().EnterDialogueMode(GameManager.instance.MainStory);
            DialogueManager.GetInstance().TriggerSection("TwelveMYPC");
        }
    }
    public void OpenDesktopShop()
    {
        if (OnExploreDesktopQuest == true)
        {
            DialogueManager.GetInstance().EnterDialogueMode(GameManager.instance.MainStory);
            DialogueManager.GetInstance().TriggerSection("TwelveShop");
        }
    }
    public void OpenDesktopSettings()
    {
        if (OnExploreDesktopQuest == true)
        {
            DialogueManager.GetInstance().EnterDialogueMode(GameManager.instance.MainStory);
            DialogueManager.GetInstance().TriggerSection("TwelveSettings");
        }
    }
    public void OpenDesktopStats()
    {
        if (OnExploreDesktopQuest == true)
        {
            DialogueManager.GetInstance().EnterDialogueMode(GameManager.instance.MainStory);
            DialogueManager.GetInstance().TriggerSection("TwelveStats");
        }
    }
    public void OpenDesktopHardware()
    {
        if (OnExploreDesktopQuest == true)
        {
            DialogueManager.GetInstance().EnterDialogueMode(GameManager.instance.MainStory);
            DialogueManager.GetInstance().TriggerSection("TwelveHardwares");
        }
    }

    public void OpenDesktopPlay()
    {
        if (OnExploreDesktopQuest == true)
        {
            DialogueManager.GetInstance().EnterDialogueMode(GameManager.instance.MainStory);
            DialogueManager.GetInstance().TriggerSection("TwelvePlayToMap");
        }
    }

    public void OnModifyQuests(string inputs)
    {
        if(OnModifyQuest == true)
        {
            if(inputs == "Entered")
            {
                DialogueManager.GetInstance().EnterDialogueMode(MainStory);
                DialogueManager.GetInstance().TriggerSection("TwentyThree");
            }

            if(inputs == "Hardwares")
            {
                DialogueManager.GetInstance().EnterDialogueMode(MainStory);
                DialogueManager.GetInstance().TriggerSection("TwentyFourDoneHardwares");
            }

            if(inputs == "Computers" && !UIExplore.activeSelf)
            {
                DialogueManager.GetInstance().EnterDialogueMode(MainStory);
                DialogueManager.GetInstance().TriggerSection("TwentyFourDoneComputers");
            }

            if(inputs == "Modify")
            {
                DialogueManager.GetInstance().EnterDialogueMode(MainStory);
                DialogueManager.GetInstance().TriggerSection("TwentyFourDoneModify");
            }

            if(inputs == "Done")
            {
                DoneModify = true;
            }
        }
    }

    //for playOnLan scripts
    public void LanCancelButton()
    {
        scene.LoadScene();
    }

  }
[System.Serializable]
public class DecorationItemList
{
    public List<DecorationItem> Items;
}

[System.Serializable]
public class PartsItemList
{
    public List<InventoryItem> Items;
}

[System.Serializable]
public class OtherWorldInventory
{
    public List<OtherWorldItem> Items;
}

[System.Serializable]
public class GameObjectData
{
    public string name;
    public PartsSO parts;
    public int Quantity;
    public Vector3 position;
    public Vector3 scale;
}

//[System.Serializable]
//public class ComputerList
//{
//    public List<Computer> Items;
//}

//[System.Serializable]
//public class PCList
//{
//    public List<PCSO> PCSOs;
//}



