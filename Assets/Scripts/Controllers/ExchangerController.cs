using Exchanger.Model.CaseWorld;
using Exchanger.Model.CPUFWorld;
using Exchanger.Model.CPUWorld;
using Exchanger.Model.GPUWorld;
using Exchanger.Model.MBWorld;
using Exchanger.Model.PSUWorld;
using Exchanger.Model.RAMWorld;
using Exchanger.Model.StorageWorld;
using Exchanger.UI.CaseWorld;
using Exchanger.UI.CPUFWorld;
using Exchanger.UI.CPUWorld;
using Exchanger.UI.GPUWorld;
using Exchanger.UI.MBWorld;
using Exchanger.UI.PSUWorld;
using Exchanger.UI.RAMWorld;
using Exchanger.UI.StorageWorld;
using Firebase.Firestore;
using OtherWorld.Model;
using PC.UI;
using Swords.Model;
using Swords.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static OtherWorld.Model.OWInvSO;

namespace Exchanger
{
    public class ExchangerController : MonoBehaviour
    {

        public int currentlevel;

        //for CPU
        [SerializeField]
        private CPUWorldExchangerPage CPUsPage;
        [SerializeField]
        private CPUWorldItemSO CPUsData;
        private Dictionary<int, float> remainingTimes = new Dictionary<int, float>();
        private Dictionary<int, bool> cpuDisplayed = new Dictionary<int, bool>();
        private Queue<int> cpusToReplace = new Queue<int>();
        public float Time = 120f;
        private float remainingTime = 0f; // 3 minutes in seconds
        public Button RefreshNow;
        public int CPURefreshPrice = 300;
        public TMP_Text CPUTextRefreshPrice;


        //for RAM
        [SerializeField]
        private RAMWorldExchangerPage RAMsPage;
        [SerializeField]
        private RAMWorldItemSO RAMsData;
        private Dictionary<int, float> RAMremainingTimes = new Dictionary<int, float>();
        private Dictionary<int, bool> ramDisplayed = new Dictionary<int, bool>();
        private Queue<int> RAMsToReplace = new Queue<int>();
        public float RAMTime = 120f;
        private float RAMremainingTime = 0f;
        public Button RAMRefreshNow;
        public int RAMRefreshPrice = 600;
        public TMP_Text RAMTextRefreshPrice;

        //for CPU Fan
        [SerializeField]
        private CPUFExchangerPage CPUFsPage;
        [SerializeField]
        private CPUfWorldItemSO CPUFsData;
        private Dictionary<int, float> CPUFremainingTimes = new Dictionary<int, float>();
        private Dictionary<int, bool> cpufdisplayed = new Dictionary<int, bool>();
        private Queue<int> CPUFsToReplace = new Queue<int>();
        public float CPUFTime = 120f;
        private float CPUFremainingTime = 0f;
        public Button CPUFRefreshNow;
        public int CPUFRefreshPrice = 900;
        public TMP_Text CPUFTextRefreshPrice;

        //for GPU
        [SerializeField]
        private GPUExchangerPage GPUsPage;
        [SerializeField]
        private GPUWorldItemSO GPUsData;
        private Dictionary<int, float> GPUremainingTimes = new Dictionary<int, float>();
        private Dictionary<int, bool> gpudisplayed = new Dictionary<int, bool>();
        private Queue<int> GPUsToReplace = new Queue<int>();
        public float GPUTime = 120f;
        private float GPUremainingTime = 0f;
        public Button GPURefreshNow;
        public int GPURefreshPrice = 1200;
        public TMP_Text GPUTextRefreshPrice;

        //for Storage
        [SerializeField]
        private StorageWorldExchangerPage StoragesPage;
        [SerializeField]
        private StorageItemSO StoragesData;
        private Dictionary<int, float> StorageremainingTimes = new Dictionary<int, float>();
        private Dictionary<int, bool> storagedisplayed = new Dictionary<int, bool>();
        private Queue<int> StoragesToReplace = new Queue<int>();
        public float StorageTime = 120f;
        private float StorageremainingTime = 0f;
        public Button StorageRefreshNow;
        public int StorageRefreshPrice = 1500;
        public TMP_Text StorageTextRefreshPrice;

        //for PSU
        [SerializeField]
        private PSUWorldExchangerPage PSUsPage;
        [SerializeField]
        private PSUWorldItemSO PSUsData;
        private Dictionary<int, float> PSUremainingTimes = new Dictionary<int, float>();
        private Dictionary<int, bool> psudisplayed = new Dictionary<int, bool>();
        private Queue<int> PSUsToReplace = new Queue<int>();
        public float PSUTime = 120f;
        private float PSUremainingTime = 0f;
        public Button PSURefreshNow;
        public int PSURefreshPrice = 1800;
        public TMP_Text PSUTextRefreshPrice;

        //for Motherboard
        [SerializeField]
        private MBWorldExchangerPage MBsPage;
        [SerializeField]
        private MBWorldItemSO MBsData;
        private Dictionary<int, float> MBremainingTimes = new Dictionary<int, float>();
        private Dictionary<int, bool> mbdisplayed = new Dictionary<int, bool>();
        private Queue<int> MBsToReplace = new Queue<int>();
        public float MBTime = 120f;
        private float MBremainingTime = 0f;
        public Button MBRefreshNow;
        public int MBRefreshPrice = 2100;
        public TMP_Text MBTextRefreshPrice;


        //for Case
        [SerializeField]
        private CaseWorldExchangerPage CasesPage;
        [SerializeField]
        private CaseWorldItemSO CasesData;
        private Dictionary<int, float> CaseremainingTimes = new Dictionary<int, float>();
        private Dictionary<int, bool> casedisplayed = new Dictionary<int, bool>();
        private Queue<int> CasesToReplace = new Queue<int>();
        public float CaseTime = 120f;
        private float CaseremainingTime = 0f;
        public Button CaseRefreshNow;
        public int CaseRefreshPrice = 2400;
        public TMP_Text CaseTextRefreshPrice;

        public void Update()
        {
            currentlevel = GameManager.instance.PlayerLevel;
        }
        void Start()
        {
           currentlevel = GameManager.instance.PlayerLevel;
           //initialize RefreshPrice
           CPUTextRefreshPrice.text = CPURefreshPrice.ToString();
           RAMTextRefreshPrice.text = RAMRefreshPrice.ToString();
           CPUFTextRefreshPrice.text = CPUFRefreshPrice.ToString();
           GPUTextRefreshPrice.text = GPURefreshPrice.ToString();
           StorageTextRefreshPrice.text = StorageRefreshPrice.ToString();
           PSUTextRefreshPrice.text = PSURefreshPrice.ToString();
           MBTextRefreshPrice.text = MBRefreshPrice.ToString();
           CaseTextRefreshPrice.text = CaseRefreshPrice.ToString();


            //for CPU
            remainingTime = Time;
            CPUsData.ShuffleCPUs();
            CPUsPage.InitializedCPU(CPUsData.size);
            InitializeCPUs();

            StartCoroutine(UpdateTimers());
            RefreshNow.onClick.AddListener(RefreshCPUs);

            //for RAM
            RAMremainingTime = RAMTime;
            RAMsData.ShuffleRAMs();
            RAMsPage.InitializedRAM(RAMsData.size);
            InitializeRAMs();

            StartCoroutine(RAMUpdateTimers());
            RAMRefreshNow.onClick.AddListener(RefreshRAMs);

            //for CPUFan
            CPUFremainingTime = CPUFTime;
            CPUFsData.ShuffleCPUFs();
            CPUFsPage.InitializedCPUF(CPUFsData.size);
            InitializeCPUFs();

            StartCoroutine(CPUFUpdateTimers());
            CPUFRefreshNow.onClick.AddListener(RefreshCPUFs);

            //for GPU
            GPUremainingTime = GPUTime;
            GPUsData.ShuffleGPUs();
            GPUsPage.InitializedGPU(GPUsData.size);
            InitializeGPUs();

            StartCoroutine(GPUUpdateTimers());
            GPURefreshNow.onClick.AddListener(RefreshGPUs);

            // for Storage
            StorageremainingTime = StorageTime;
            StoragesData.ShuffleStorages();
            StoragesPage.InitializedStorage(StoragesData.size);
            InitializeStorages();

            StartCoroutine(StorageUpdateTimers());
            StorageRefreshNow.onClick.AddListener(RefreshStorages);

            //for PSU
            PSUremainingTime = PSUTime;
            PSUsData.ShufflePSUs();
            PSUsPage.InitializedPSU(PSUsData.size);
            InitializePSUs();

            StartCoroutine(PSUUpdateTimers());
            PSURefreshNow.onClick.AddListener(RefreshPSUs);

            //for Motherboard
            MBremainingTime = MBTime;
            MBsData.ShuffleMBs();
            MBsPage.InitializedMB(MBsData.size);
            InitializeMBs();

            StartCoroutine(MBUpdateTimers());
            MBRefreshNow.onClick.AddListener(RefreshMBs);

            //for Case
            CaseremainingTime = CaseTime;
            CasesData.ShuffleCases();
            CasesPage.InitializedCase(CasesData.size);
            InitializeCases();

            StartCoroutine(CaseUpdateTimers());
            CaseRefreshNow.onClick.AddListener(RefreshCases);

        }

        //for CPU
        private void InitializeCPUs()
        {

            var currentInventoryState = CPUsData.GetCurrentInventoryState();
            foreach (var item in currentInventoryState)
            {
                GameObject materialsNeedObject = item.Value.item.MaterialsNeed;

                // Try to get the SpriteRenderer component from the GameObject
                if (materialsNeedObject.TryGetComponent<SpriteRenderer>(out SpriteRenderer spriteRenderer))
                {
                    // Access the sprite from the SpriteRenderer
                    Sprite itemSprite = spriteRenderer.sprite;

                    string perks = "";

                    // Check each perk property and accumulate non-zero values
                    if (item.Value.item.Parts.AttackDamage != 0)
                    {
                        perks += "Attack Damage +" + item.Value.item.Parts.AttackDamage + "\n";
                    }
                    if (item.Value.item.Parts.Health != 0)
                    {
                        perks += "Health +" + item.Value.item.Parts.Health + "\n";
                    }
                    if (item.Value.item.Parts.Mana != 0)
                    {
                        perks += "Mana +" + item.Value.item.Parts.Mana + "\n";
                    }
                    if (item.Value.item.Parts.HealthRegen != 0)
                    {
                        perks += "Health Regen  +" + item.Value.item.Parts.HealthRegen + "\n";
                    }
                    if (item.Value.item.Parts.WalkSpeed != 0)
                    {
                        perks += "Walk Speed +" + item.Value.item.Parts.WalkSpeed + "\n";
                    }
                    if (item.Value.item.Parts.Armor != 0)
                    {
                        perks += "Armor +" + item.Value.item.Parts.Armor + "\n";
                    }
                    if (item.Value.item.Parts.ManaRegen != 0)
                    {
                        perks += "Mana Regen +" + item.Value.item.Parts.ManaRegen + "\n";
                    }
                    if (item.Value.item.Parts.CriticalChance != 0)
                    {
                        perks += "Critical Chance+" + item.Value.item.Parts.CriticalChance + "\n";
                    }

                    // Now update your UI element using the retrieved sprite
                    CPUsPage.UpdateData(item.Key, item.Value.item.Parts.ItemImage, itemSprite, item.Value.item.Parts.Name, item.Value.item.Parts.rarity, item.Value.item.MaterialsAmountNeed, perks);
                }
                else
                {
                    Debug.LogError("SpriteRenderer component not found on the MaterialsNeed GameObject.");
                }
            }
        }

        private IEnumerator UpdateTimers()
        {
            while (true)
            {
                yield return new WaitForSeconds(1f);

                remainingTime -= 1f;

                if (remainingTime <= 0f)
                {
                    RespawnCPUs();
                    remainingTime = Time; // Reset time to 3 minutes
                }

                UpdateTimerDisplay();
                CPUsData.InformAboutChange();
            }
        }

        private void RespawnCPUs()
        {
            CPUsData.ShuffleCPUs();
            InitializeCPUs();
            CPUsPage.ResetSelection();
        }

        private void RefreshCPUs()
        {
            if (GameManager.instance.PlayerMoney >= CPURefreshPrice)
            {

                CPUsData.ShuffleCPUs();
                InitializeCPUs();
                remainingTime = Time;
                CPUsPage.ResetSelection();
                GameManager.instance.PlayerMoney -= CPURefreshPrice;
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
            CPUsPage.UpdateTimer(timeText);
        }

        public async void CPUsOpenShop()
        {
            CPUsPage.Show();
            int quantity = await GetMaterialQuantity("Silicon Wafer");
            GameManager.instance.CPUMaterialText.text = quantity.ToString();
            CPUsPage.ResetSelection();
        }

       

        //for Ram
        private void InitializeRAMs()
        {

            var currentInventoryState = RAMsData.GetCurrentInventoryState();
            foreach (var item in currentInventoryState)
            {
                GameObject materialsNeedObject = item.Value.item.MaterialsNeed;

                // Try to get the SpriteRenderer component from the GameObject
                if (materialsNeedObject.TryGetComponent<SpriteRenderer>(out SpriteRenderer spriteRenderer))
                {
                    // Access the sprite from the SpriteRenderer
                    Sprite itemSprite = spriteRenderer.sprite;

                    string perks = "";

                    // Check each perk property and accumulate non-zero values
                    if (item.Value.item.Parts.AttackDamage != 0)
                    {
                        perks += "Attack Damage +" + item.Value.item.Parts.AttackDamage + "\n";
                    }
                    if (item.Value.item.Parts.Health != 0)
                    {
                        perks += "Health +" + item.Value.item.Parts.Health + "\n";
                    }
                    if (item.Value.item.Parts.Mana != 0)
                    {
                        perks += "Mana +" + item.Value.item.Parts.Mana + "\n";
                    }
                    if (item.Value.item.Parts.HealthRegen != 0)
                    {
                        perks += "Health Regen  +" + item.Value.item.Parts.HealthRegen + "\n";
                    }
                    if (item.Value.item.Parts.WalkSpeed != 0)
                    {
                        perks += "Walk Speed +" + item.Value.item.Parts.WalkSpeed + "\n";
                    }
                    if (item.Value.item.Parts.Armor != 0)
                    {
                        perks += "Armor +" + item.Value.item.Parts.Armor + "\n";
                    }
                    if (item.Value.item.Parts.ManaRegen != 0)
                    {
                        perks += "Mana Regen +" + item.Value.item.Parts.ManaRegen + "\n";
                    }
                    if (item.Value.item.Parts.CriticalChance != 0)
                    {
                        perks += "Critical Chance+" + item.Value.item.Parts.CriticalChance + "\n";
                    }


                    // Now update your UI element using the retrieved sprite
                    RAMsPage.UpdateData(item.Key, item.Value.item.Parts.ItemImage, itemSprite, item.Value.item.Parts.Name, item.Value.item.Parts.rarity, item.Value.item.MaterialsAmountNeed, perks);
                }
                else
                {
                    Debug.LogError("SpriteRenderer component not found on the MaterialsNeed GameObject.");
                }
            }
        }

        private IEnumerator RAMUpdateTimers()
        {
            while (true)
            {
                yield return new WaitForSeconds(1f);

                RAMremainingTime -= 1f;

                if (RAMremainingTime <= 0f)
                {
                    RespawnRAMs();
                    RAMremainingTime = RAMTime; // Reset time to 3 minutes
                }

                RAMUpdateTimerDisplay();
                RAMsData.InformAboutChange();
            }
        }

        private void RespawnRAMs()
        {
            RAMsData.ShuffleRAMs();
            InitializeRAMs();
            RAMsPage.ResetSelection();
        }

        private void RefreshRAMs()
        {
            if (GameManager.instance.PlayerMoney >= RAMRefreshPrice)
            {

                RAMsData.ShuffleRAMs();
                InitializeRAMs();
                RAMremainingTime = RAMTime;
                RAMsPage.ResetSelection();
                GameManager.instance.PlayerMoney -= RAMRefreshPrice;
                GameManager.instance.SaveCharInfo(GameManager.instance.UserID, GameManager.instance.PlayerName);
            }
            else
            {
                //Debug.LogError("You dont have enough money");
                GameManager.instance.ShowFloatingText("You don't have enough coins");
            }
        }

        private void RAMUpdateTimerDisplay()
        {
            int minutes = Mathf.FloorToInt(RAMremainingTime / 60F);
            int seconds = Mathf.FloorToInt(RAMremainingTime - minutes * 60);
            string timeText = string.Format("{0:0}:{1:00}", minutes, seconds);
            RAMsPage.UpdateTimer(timeText);
        }

        public async void RAMsOpenShop()
        {
            RAMsPage.Show();
            int quantity = await GetMaterialQuantity("Memory Chip");
            GameManager.instance.RAMMaterialText.text = quantity.ToString();
            RAMsPage.ResetSelection();
        }

        //for CPU Fan
        private void InitializeCPUFs()
        {

            var currentInventoryState = CPUFsData.GetCurrentInventoryState();
            foreach (var item in currentInventoryState)
            {
                GameObject materialsNeedObject = item.Value.item.MaterialsNeed;

                // Try to get the SpriteRenderer component from the GameObject
                if (materialsNeedObject.TryGetComponent<SpriteRenderer>(out SpriteRenderer spriteRenderer))
                {
                    // Access the sprite from the SpriteRenderer
                    Sprite itemSprite = spriteRenderer.sprite;

                    string perks = "";

                    // Check each perk property and accumulate non-zero values
                    if (item.Value.item.Parts.AttackDamage != 0)
                    {
                        perks += "Attack Damage +" + item.Value.item.Parts.AttackDamage + "\n";
                    }
                    if (item.Value.item.Parts.Health != 0)
                    {
                        perks += "Health +" + item.Value.item.Parts.Health + "\n";
                    }
                    if (item.Value.item.Parts.Mana != 0)
                    {
                        perks += "Mana +" + item.Value.item.Parts.Mana + "\n";
                    }
                    if (item.Value.item.Parts.HealthRegen != 0)
                    {
                        perks += "Health Regen  +" + item.Value.item.Parts.HealthRegen + "\n";
                    }
                    if (item.Value.item.Parts.WalkSpeed != 0)
                    {
                        perks += "Walk Speed +" + item.Value.item.Parts.WalkSpeed + "\n";
                    }
                    if (item.Value.item.Parts.Armor != 0)
                    {
                        perks += "Armor +" + item.Value.item.Parts.Armor + "\n";
                    }
                    if (item.Value.item.Parts.ManaRegen != 0)
                    {
                        perks += "Mana Regen +" + item.Value.item.Parts.ManaRegen + "\n";
                    }
                    if (item.Value.item.Parts.CriticalChance != 0)
                    {
                        perks += "Critical Chance+" + item.Value.item.Parts.CriticalChance + "\n";
                    }

                    // Now update your UI element using the retrieved sprite
                    CPUFsPage.UpdateData(item.Key, item.Value.item.Parts.ItemImage, itemSprite, item.Value.item.Parts.Name, item.Value.item.Parts.rarity, item.Value.item.MaterialsAmountNeed, perks);
                }
                else
                {
                    Debug.LogError("SpriteRenderer component not found on the MaterialsNeed GameObject.");
                }
            }
        }

        private IEnumerator CPUFUpdateTimers()
        {
            while (true)
            {
                yield return new WaitForSeconds(1f);

                CPUFremainingTime -= 1f;

                if (CPUFremainingTime <= 0f)
                {
                    RespawnCPUFs();
                    CPUFremainingTime = CPUFTime; // Reset time to 3 minutes
                }

                CPUFUpdateTimerDisplay();
                CPUFsData.InformAboutChange();
            }
        }

        private void RespawnCPUFs()
        {
            CPUFsData.ShuffleCPUFs();
            InitializeCPUFs();
            CPUFsPage.ResetSelection();
        }

        private void RefreshCPUFs()
        {
            if (GameManager.instance.PlayerMoney >= CPUFRefreshPrice)
            {

                CPUFsData.ShuffleCPUFs();
                InitializeCPUFs();
                CPUFremainingTime = CPUFTime;
                CPUFsPage.ResetSelection();
                GameManager.instance.PlayerMoney -= CPUFRefreshPrice;
                GameManager.instance.SaveCharInfo(GameManager.instance.UserID, GameManager.instance.PlayerName);
            }
            else
            {
                //Debug.LogError("You dont have enough money");
                GameManager.instance.ShowFloatingText("You don't have enough coins");
            }
        }

        private void CPUFUpdateTimerDisplay()
        {
            int minutes = Mathf.FloorToInt(CPUFremainingTime / 60F);
            int seconds = Mathf.FloorToInt(CPUFremainingTime - minutes * 60);
            string timeText = string.Format("{0:0}:{1:00}", minutes, seconds);
            CPUFsPage.UpdateTimer(timeText);
        }

        public async void CPUFsOpenShop()
        {
            CPUFsPage.Show();
            int quantity = await GetMaterialQuantity("Heat Sink");
            GameManager.instance.CPUFMaterialText.text = quantity.ToString();
            CPUFsPage.ResetSelection();
        }


        //for GPU
        private void InitializeGPUs()
        {

            var currentInventoryState = GPUsData.GetCurrentInventoryState();
            foreach (var item in currentInventoryState)
            {
                GameObject materialsNeedObject = item.Value.item.MaterialsNeed;

                // Try to get the SpriteRenderer component from the GameObject
                if (materialsNeedObject.TryGetComponent<SpriteRenderer>(out SpriteRenderer spriteRenderer))
                {
                    // Access the sprite from the SpriteRenderer
                    Sprite itemSprite = spriteRenderer.sprite;

                    string perks = "";

                    // Check each perk property and accumulate non-zero values
                    if (item.Value.item.Parts.AttackDamage != 0)
                    {
                        perks += "Attack Damage +" + item.Value.item.Parts.AttackDamage + "\n";
                    }
                    if (item.Value.item.Parts.Health != 0)
                    {
                        perks += "Health +" + item.Value.item.Parts.Health + "\n";
                    }
                    if (item.Value.item.Parts.Mana != 0)
                    {
                        perks += "Mana +" + item.Value.item.Parts.Mana + "\n";
                    }
                    if (item.Value.item.Parts.HealthRegen != 0)
                    {
                        perks += "Health Regen  +" + item.Value.item.Parts.HealthRegen + "\n";
                    }
                    if (item.Value.item.Parts.WalkSpeed != 0)
                    {
                        perks += "Walk Speed +" + item.Value.item.Parts.WalkSpeed + "\n";
                    }
                    if (item.Value.item.Parts.Armor != 0)
                    {
                        perks += "Armor +" + item.Value.item.Parts.Armor + "\n";
                    }
                    if (item.Value.item.Parts.ManaRegen != 0)
                    {
                        perks += "Mana Regen +" + item.Value.item.Parts.ManaRegen + "\n";
                    }
                    if (item.Value.item.Parts.CriticalChance != 0)
                    {
                        perks += "Critical Chance+" + item.Value.item.Parts.CriticalChance + "\n";
                    }

                    // Now update your UI element using the retrieved sprite
                    GPUsPage.UpdateData(item.Key, item.Value.item.Parts.ItemImage, itemSprite, item.Value.item.Parts.Name, item.Value.item.Parts.rarity, item.Value.item.MaterialsAmountNeed, perks);
                }
                else
                {
                    Debug.LogError("SpriteRenderer component not found on the MaterialsNeed GameObject.");
                }
            }
        }

        private IEnumerator GPUUpdateTimers()
        {
            while (true)
            {
                yield return new WaitForSeconds(1f);

                GPUremainingTime -= 1f;

                if (GPUremainingTime <= 0f)
                {
                    RespawnGPUs();
                    GPUremainingTime = GPUTime; // Reset time to 3 minutes
                }

                GPUUpdateTimerDisplay();
                GPUsData.InformAboutChange();
            }
        }

        private void RespawnGPUs()
        {
            GPUsData.ShuffleGPUs();
            InitializeGPUs();
            GPUsPage.ResetSelection();
        }

        private void RefreshGPUs()
        {

            if (GameManager.instance.PlayerMoney >= GPURefreshPrice)
            {
                GPUsData.ShuffleGPUs();
                InitializeGPUs();
                GPUremainingTime = GPUTime;
                GPUsPage.ResetSelection();
                GameManager.instance.PlayerMoney -= GPURefreshPrice;
                GameManager.instance.SaveCharInfo(GameManager.instance.UserID, GameManager.instance.PlayerName);
            }
            else
            {
                //Debug.LogError("You dont have enough money");
                GameManager.instance.ShowFloatingText("You don't have enough coins");
            }

        }

        private void GPUUpdateTimerDisplay()
        {
            int minutes = Mathf.FloorToInt(GPUremainingTime / 60F);
            int seconds = Mathf.FloorToInt(GPUremainingTime - minutes * 60);
            string timeText = string.Format("{0:0}:{1:00}", minutes, seconds);
            GPUsPage.UpdateTimer(timeText);
        }

        public async void GPUsOpenShop()
        {
            GPUsPage.Show();
            int quantity = await GetMaterialQuantity("Video Memory");
            GameManager.instance.GPUMaterialText.text = quantity.ToString();
            GPUsPage.ResetSelection();
        }


        //for Storage

        private void InitializeStorages()
        {

            var currentInventoryState = StoragesData.GetCurrentInventoryState();
            foreach (var item in currentInventoryState)
            {
                GameObject materialsNeedObject = item.Value.item.MaterialsNeed;

                // Try to get the SpriteRenderer component from the GameObject
                if (materialsNeedObject.TryGetComponent<SpriteRenderer>(out SpriteRenderer spriteRenderer))
                {
                    // Access the sprite from the SpriteRenderer
                    Sprite itemSprite = spriteRenderer.sprite;

                    string perks = "";

                    // Check each perk property and accumulate non-zero values
                    if (item.Value.item.Parts.AttackDamage != 0)
                    {
                        perks += "Attack Damage +" + item.Value.item.Parts.AttackDamage + "\n";
                    }
                    if (item.Value.item.Parts.Health != 0)
                    {
                        perks += "Health +" + item.Value.item.Parts.Health + "\n";
                    }
                    if (item.Value.item.Parts.Mana != 0)
                    {
                        perks += "Mana +" + item.Value.item.Parts.Mana + "\n";
                    }
                    if (item.Value.item.Parts.HealthRegen != 0)
                    {
                        perks += "Health Regen  +" + item.Value.item.Parts.HealthRegen + "\n";
                    }
                    if (item.Value.item.Parts.WalkSpeed != 0)
                    {
                        perks += "Walk Speed +" + item.Value.item.Parts.WalkSpeed + "\n";
                    }
                    if (item.Value.item.Parts.Armor != 0)
                    {
                        perks += "Armor +" + item.Value.item.Parts.Armor + "\n";
                    }
                    if (item.Value.item.Parts.ManaRegen != 0)
                    {
                        perks += "Mana Regen +" + item.Value.item.Parts.ManaRegen + "\n";
                    }
                    if (item.Value.item.Parts.CriticalChance != 0)
                    {
                        perks += "Critical Chance+" + item.Value.item.Parts.CriticalChance + "\n";
                    }

                    // Now update your UI element using the retrieved sprite
                    StoragesPage.UpdateData(item.Key, item.Value.item.Parts.ItemImage, itemSprite, item.Value.item.Parts.Name, item.Value.item.Parts.rarity, item.Value.item.MaterialsAmountNeed, perks);
                }
                else
                {
                    Debug.LogError("SpriteRenderer component not found on the MaterialsNeed GameObject.");
                }
            }
        }

        private IEnumerator StorageUpdateTimers()
        {
            while (true)
            {
                yield return new WaitForSeconds(1f);

                StorageremainingTime -= 1f;

                if (StorageremainingTime <= 0f)
                {
                    RespawnStorages();
                    StorageremainingTime = StorageTime; // Reset time to 3 minutes
                }

                StorageUpdateTimerDisplay();
                StoragesData.InformAboutChange();
            }
        }

        private void RespawnStorages()
        {
            StoragesData.ShuffleStorages();
            InitializeStorages();
            StoragesPage.ResetSelection();
        }

        private void RefreshStorages()
        {

            if (GameManager.instance.PlayerMoney >= StorageRefreshPrice)
            {

                StoragesData.ShuffleStorages();
                InitializeStorages();
                StorageremainingTime = StorageTime;
                StoragesPage.ResetSelection();
                GameManager.instance.PlayerMoney -= StorageRefreshPrice;
                GameManager.instance.SaveCharInfo(GameManager.instance.UserID, GameManager.instance.PlayerName);
            }
            else
            {
                //Debug.LogError("You dont have enough money");
                GameManager.instance.ShowFloatingText("You don't have enough coins");
            }
        }

        private void StorageUpdateTimerDisplay()
        {
            int minutes = Mathf.FloorToInt(StorageremainingTime / 60F);
            int seconds = Mathf.FloorToInt(StorageremainingTime - minutes * 60);
            string timeText = string.Format("{0:0}:{1:00}", minutes, seconds);
            StoragesPage.UpdateTimer(timeText);
        }

        public async void StoragesOpenShop()
        {
            StoragesPage.Show();
            int quantity = await GetMaterialQuantity("Disk Platter");
            GameManager.instance.StorageMaterialText.text = quantity.ToString();
            StoragesPage.ResetSelection();
        }

        //for PSU

        private void InitializePSUs()
        {

            var currentInventoryState = PSUsData.GetCurrentInventoryState();
            foreach (var item in currentInventoryState)
            {
                GameObject materialsNeedObject = item.Value.item.MaterialsNeed;

                // Try to get the SpriteRenderer component from the GameObject
                if (materialsNeedObject.TryGetComponent<SpriteRenderer>(out SpriteRenderer spriteRenderer))
                {
                    // Access the sprite from the SpriteRenderer
                    Sprite itemSprite = spriteRenderer.sprite;

                    string perks = "";

                    // Check each perk property and accumulate non-zero values
                    if (item.Value.item.Parts.AttackDamage != 0)
                    {
                        perks += "Attack Damage +" + item.Value.item.Parts.AttackDamage + "\n";
                    }
                    if (item.Value.item.Parts.Health != 0)
                    {
                        perks += "Health +" + item.Value.item.Parts.Health + "\n";
                    }
                    if (item.Value.item.Parts.Mana != 0)
                    {
                        perks += "Mana +" + item.Value.item.Parts.Mana + "\n";
                    }
                    if (item.Value.item.Parts.HealthRegen != 0)
                    {
                        perks += "Health Regen  +" + item.Value.item.Parts.HealthRegen + "\n";
                    }
                    if (item.Value.item.Parts.WalkSpeed != 0)
                    {
                        perks += "Walk Speed +" + item.Value.item.Parts.WalkSpeed + "\n";
                    }
                    if (item.Value.item.Parts.Armor != 0)
                    {
                        perks += "Armor +" + item.Value.item.Parts.Armor + "\n";
                    }
                    if (item.Value.item.Parts.ManaRegen != 0)
                    {
                        perks += "Mana Regen +" + item.Value.item.Parts.ManaRegen + "\n";
                    }
                    if (item.Value.item.Parts.CriticalChance != 0)
                    {
                        perks += "Critical Chance+" + item.Value.item.Parts.CriticalChance + "\n";
                    }

                    // Now update your UI element using the retrieved sprite
                    PSUsPage.UpdateData(item.Key, item.Value.item.Parts.ItemImage, itemSprite, item.Value.item.Parts.Name, item.Value.item.Parts.rarity, item.Value.item.MaterialsAmountNeed, perks);
                }
                else
                {
                    Debug.LogError("SpriteRenderer component not found on the MaterialsNeed GameObject.");
                }
            }
        }

        private IEnumerator PSUUpdateTimers()
        {
            while (true)
            {
                yield return new WaitForSeconds(1f);

                PSUremainingTime -= 1f;

                if (PSUremainingTime <= 0f)
                {
                    RespawnPSUs();
                    PSUremainingTime = PSUTime; // Reset time to 3 minutes
                }

                PSUUpdateTimerDisplay();
                PSUsData.InformAboutChange();
            }
        }

        private void RespawnPSUs()
        {
            PSUsData.ShufflePSUs();
            InitializePSUs();
            PSUsPage.ResetSelection();
        }

        private void RefreshPSUs()
        {

            if (GameManager.instance.PlayerMoney >= PSURefreshPrice)
            {

                PSUsData.ShufflePSUs();
                InitializePSUs();
                PSUremainingTime = PSUTime;
                PSUsPage.ResetSelection();
                GameManager.instance.PlayerMoney -= PSURefreshPrice;
                GameManager.instance.SaveCharInfo(GameManager.instance.UserID, GameManager.instance.PlayerName);
            }
            else
            {
                //Debug.LogError("You dont have enough money");
                GameManager.instance.ShowFloatingText("You don't have enough coins");
            }

            
        }

        private void PSUUpdateTimerDisplay()
        {
            int minutes = Mathf.FloorToInt(PSUremainingTime / 60F);
            int seconds = Mathf.FloorToInt(PSUremainingTime - minutes * 60);
            string timeText = string.Format("{0:0}:{1:00}", minutes, seconds);
            PSUsPage.UpdateTimer(timeText);
        }

        public async void PSUsOpenShop()
        {
            PSUsPage.Show();
            int quantity = await GetMaterialQuantity("PSU Transformer");
            GameManager.instance.PSUMaterialText.text = quantity.ToString();
            PSUsPage.ResetSelection();
        }


        //for Motherboard
        private void InitializeMBs()
        {

            var currentInventoryState = MBsData.GetCurrentInventoryState();
            foreach (var item in currentInventoryState)
            {
                GameObject materialsNeedObject = item.Value.item.MaterialsNeed;

                // Try to get the SpriteRenderer component from the GameObject
                if (materialsNeedObject.TryGetComponent<SpriteRenderer>(out SpriteRenderer spriteRenderer))
                {
                    // Access the sprite from the SpriteRenderer
                    Sprite itemSprite = spriteRenderer.sprite;

                    string perks = "";

                    // Check each perk property and accumulate non-zero values
                    if (item.Value.item.Parts.AttackDamage != 0)
                    {
                        perks += "Attack Damage +" + item.Value.item.Parts.AttackDamage + "\n";
                    }
                    if (item.Value.item.Parts.Health != 0)
                    {
                        perks += "Health +" + item.Value.item.Parts.Health + "\n";
                    }
                    if (item.Value.item.Parts.Mana != 0)
                    {
                        perks += "Mana +" + item.Value.item.Parts.Mana + "\n";
                    }
                    if (item.Value.item.Parts.HealthRegen != 0)
                    {
                        perks += "Health Regen  +" + item.Value.item.Parts.HealthRegen + "\n";
                    }
                    if (item.Value.item.Parts.WalkSpeed != 0)
                    {
                        perks += "Walk Speed +" + item.Value.item.Parts.WalkSpeed + "\n";
                    }
                    if (item.Value.item.Parts.Armor != 0)
                    {
                        perks += "Armor +" + item.Value.item.Parts.Armor + "\n";
                    }
                    if (item.Value.item.Parts.ManaRegen != 0)
                    {
                        perks += "Mana Regen +" + item.Value.item.Parts.ManaRegen + "\n";
                    }
                    if (item.Value.item.Parts.CriticalChance != 0)
                    {
                        perks += "Critical Chance+" + item.Value.item.Parts.CriticalChance + "\n";
                    }

                    // Now update your UI element using the retrieved sprite
                    MBsPage.UpdateData(item.Key, item.Value.item.Parts.ItemImage, itemSprite, item.Value.item.Parts.Name, item.Value.item.Parts.rarity, item.Value.item.MaterialsAmountNeed, perks);
                }
                else
                {
                    Debug.LogError("SpriteRenderer component not found on the MaterialsNeed GameObject.");
                }
            }
        }

        private IEnumerator MBUpdateTimers()
        {
            while (true)
            {
                yield return new WaitForSeconds(1f);

                MBremainingTime -= 1f;

                if (MBremainingTime <= 0f)
                {
                    RespawnMBs();
                    MBremainingTime = MBTime; // Reset time to 3 minutes
                }

                MBUpdateTimerDisplay();
                MBsData.InformAboutChange();
            }
        }

        private void RespawnMBs()
        {
            MBsData.ShuffleMBs();
            InitializeMBs();
            MBsPage.ResetSelection();
        }

        private void RefreshMBs()
        {

            if (GameManager.instance.PlayerMoney >= MBRefreshPrice)
            {
                MBsData.ShuffleMBs();
                InitializeMBs();
                MBremainingTime = MBTime;
                MBsPage.ResetSelection();
                GameManager.instance.PlayerMoney -= MBRefreshPrice;
                GameManager.instance.SaveCharInfo(GameManager.instance.UserID, GameManager.instance.PlayerName);
            }
            else
            {
                //Debug.LogError("You dont have enough money");
                GameManager.instance.ShowFloatingText("You don't have enough coins");
            }

           
        }

        private void MBUpdateTimerDisplay()
        {
            int minutes = Mathf.FloorToInt(MBremainingTime / 60F);
            int seconds = Mathf.FloorToInt(MBremainingTime - minutes * 60);
            string timeText = string.Format("{0:0}:{1:00}", minutes, seconds);
            MBsPage.UpdateTimer(timeText);
        }

        public async void MBsOpenShop()
        {
            MBsPage.Show();
            int quantity = await GetMaterialQuantity("Circuit Board");
            GameManager.instance.MBMaterialText.text = quantity.ToString();
            MBsPage.ResetSelection();
        }

        //for Case
        private void InitializeCases()
        {

            var currentInventoryState = CasesData.GetCurrentInventoryState();
            foreach (var item in currentInventoryState)
            {
                GameObject materialsNeedObject = item.Value.item.MaterialsNeed;

                // Try to get the SpriteRenderer component from the GameObject
                if (materialsNeedObject.TryGetComponent<SpriteRenderer>(out SpriteRenderer spriteRenderer))
                {
                    // Access the sprite from the SpriteRenderer
                    Sprite itemSprite = spriteRenderer.sprite;

                    string perks = "";

                    // Check each perk property and accumulate non-zero values
                    if (item.Value.item.Parts.AttackDamage != 0)
                    {
                        perks += "Attack Damage +" + item.Value.item.Parts.AttackDamage + "\n";
                    }
                    if (item.Value.item.Parts.Health != 0)
                    {
                        perks += "Health +" + item.Value.item.Parts.Health + "\n";
                    }
                    if (item.Value.item.Parts.Mana != 0)
                    {
                        perks += "Mana +" + item.Value.item.Parts.Mana + "\n";
                    }
                    if (item.Value.item.Parts.HealthRegen != 0)
                    {
                        perks += "Health Regen  +" + item.Value.item.Parts.HealthRegen + "\n";
                    }
                    if (item.Value.item.Parts.WalkSpeed != 0)
                    {
                        perks += "Walk Speed +" + item.Value.item.Parts.WalkSpeed + "\n";
                    }
                    if (item.Value.item.Parts.Armor != 0)
                    {
                        perks += "Armor +" + item.Value.item.Parts.Armor + "\n";
                    }
                    if (item.Value.item.Parts.ManaRegen != 0)
                    {
                        perks += "Mana Regen +" + item.Value.item.Parts.ManaRegen + "\n";
                    }
                    if (item.Value.item.Parts.CriticalChance != 0)
                    {
                        perks += "Critical Chance+" + item.Value.item.Parts.CriticalChance + "\n";
                    }

                    // Now update your UI element using the retrieved sprite
                    CasesPage.UpdateData(item.Key, item.Value.item.Parts.ItemImage, itemSprite, item.Value.item.Parts.Name, item.Value.item.Parts.rarity, item.Value.item.MaterialsAmountNeed, perks);
                }
                else
                {
                    Debug.LogError("SpriteRenderer component not found on the MaterialsNeed GameObject.");
                }
            }
        }

        private IEnumerator CaseUpdateTimers()
        {
            while (true)
            {
                yield return new WaitForSeconds(1f);

                CaseremainingTime -= 1f;

                if (CaseremainingTime <= 0f)
                {
                    RespawnCases();
                    CaseremainingTime = CaseTime; // Reset time to 3 minutes
                }

                CaseUpdateTimerDisplay();
                CasesData.InformAboutChange();
            }
        }

        private void RespawnCases()
        {
            CasesData.ShuffleCases();
            InitializeCases();
            CasesPage.ResetSelection();
        }

        private void RefreshCases()
        {

            if (GameManager.instance.PlayerMoney >= CaseRefreshPrice)
            {
                CasesData.ShuffleCases();
                InitializeCases();
                CaseremainingTime = CaseTime;
                CasesPage.ResetSelection();
                GameManager.instance.PlayerMoney -= CaseRefreshPrice;
                GameManager.instance.SaveCharInfo(GameManager.instance.UserID, GameManager.instance.PlayerName);
            }
            else
            {
                //Debug.LogError("You dont have enough money");
                GameManager.instance.ShowFloatingText("You don't have enough coins");
            }

        }

        private void CaseUpdateTimerDisplay()
        {
            int minutes = Mathf.FloorToInt(CaseremainingTime / 60F);
            int seconds = Mathf.FloorToInt(CaseremainingTime - minutes * 60);
            string timeText = string.Format("{0:0}:{1:00}", minutes, seconds);
            CasesPage.UpdateTimer(timeText);
        }

        public async void CasesOpenShop()
        {
            CasesPage.Show();
            int quantity = await GetMaterialQuantity("Steel Pane");
            GameManager.instance.CaseMaterialText.text = quantity.ToString();
            CasesPage.ResetSelection();
        }

        private async Task<int> GetMaterialQuantity(string materialName)
        {
            CollectionReference collectionRef = FirebaseFirestore.DefaultInstance
                .Collection(GameManager.instance.UserCollection)
                .Document(GameManager.instance.UserID)
                .Collection("OtherWorldInventory");

            QuerySnapshot querySnapshot = await collectionRef.GetSnapshotAsync();

            foreach (var docSnapshot in querySnapshot.Documents)
            {
                string existingItemJson = docSnapshot.GetValue<string>("Items");
                OtherWorldItemSO loadedItem = ScriptableObject.CreateInstance<OtherWorldItemSO>();
                JsonUtility.FromJsonOverwrite(existingItemJson, loadedItem);
                OtherWorldItem existingItem = new OtherWorldItem
                {
                    item = loadedItem
                };

                if (existingItem.item.Category == "Materials" && existingItem.item.Name == materialName)
                {
                    return docSnapshot.GetValue<int>("Quantity");
                }
            }

            return 0; // If material is not found, return 0
        }

    }

}

