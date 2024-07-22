using Exchanger.Model.CPUFWorld;
using Exchanger.Model.CPUWorld;
using Exchanger.Model.GPUWorld;
using Exchanger.Model.RAMWorld;
using Exchanger.UI.CPUFWorld;
using Exchanger.UI.CPUWorld;
using Exchanger.UI.GPUWorld;
using Exchanger.UI.RAMWorld;
using PC.UI;
using Swords.Model;
using Swords.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

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

        public void Update()
        {
            currentlevel = GameManager.instance.PlayerLevel;
        }
        void Start()
        {
            currentlevel = GameManager.instance.PlayerLevel;
           
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
                    if (item.Value.item.Parts.AttackSpeed != 0)
                    {
                        perks += "Attack Speed +" + item.Value.item.Parts.AttackSpeed + "\n";
                    }
                    if (item.Value.item.Parts.CriticalHit != 0)
                    {
                        perks += "Critical Hit +" + item.Value.item.Parts.CriticalHit + "\n";
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
            CPUsData.ShuffleCPUs();
            InitializeCPUs();
            remainingTime = Time;
            CPUsPage.ResetSelection();
        }

        private void UpdateTimerDisplay()
        {
            int minutes = Mathf.FloorToInt(remainingTime / 60F);
            int seconds = Mathf.FloorToInt(remainingTime - minutes * 60);
            string timeText = string.Format("{0:0}:{1:00}", minutes, seconds);
            CPUsPage.UpdateTimer(timeText);
        }

        public void CPUsOpenShop()
        {
            CPUsPage.Show();
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
                    if (item.Value.item.Parts.AttackSpeed != 0)
                    {
                        perks += "Attack Speed +" + item.Value.item.Parts.AttackSpeed + "\n";
                    }
                    if (item.Value.item.Parts.CriticalHit != 0)
                    {
                        perks += "Critical Hit +" + item.Value.item.Parts.CriticalHit + "\n";
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
            RAMsData.ShuffleRAMs();
            InitializeRAMs();
            RAMremainingTime = RAMTime;
            RAMsPage.ResetSelection();
        }

        private void RAMUpdateTimerDisplay()
        {
            int minutes = Mathf.FloorToInt(RAMremainingTime / 60F);
            int seconds = Mathf.FloorToInt(RAMremainingTime - minutes * 60);
            string timeText = string.Format("{0:0}:{1:00}", minutes, seconds);
            RAMsPage.UpdateTimer(timeText);
        }

        public void RAMsOpenShop()
        {
            RAMsPage.Show();
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
                    if (item.Value.item.Parts.AttackSpeed != 0)
                    {
                        perks += "Attack Speed +" + item.Value.item.Parts.AttackSpeed + "\n";
                    }
                    if (item.Value.item.Parts.CriticalHit != 0)
                    {
                        perks += "Critical Hit +" + item.Value.item.Parts.CriticalHit + "\n";
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
            CPUFsData.ShuffleCPUFs();
            InitializeCPUFs();
            CPUFremainingTime = CPUFTime;
            CPUFsPage.ResetSelection();
        }

        private void CPUFUpdateTimerDisplay()
        {
            int minutes = Mathf.FloorToInt(CPUFremainingTime / 60F);
            int seconds = Mathf.FloorToInt(CPUFremainingTime - minutes * 60);
            string timeText = string.Format("{0:0}:{1:00}", minutes, seconds);
            CPUFsPage.UpdateTimer(timeText);
        }

        public void CPUFsOpenShop()
        {
            CPUFsPage.Show();
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
                    if (item.Value.item.Parts.AttackSpeed != 0)
                    {
                        perks += "Attack Speed +" + item.Value.item.Parts.AttackSpeed + "\n";
                    }
                    if (item.Value.item.Parts.CriticalHit != 0)
                    {
                        perks += "Critical Hit +" + item.Value.item.Parts.CriticalHit + "\n";
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
            GPUsData.ShuffleGPUs();
            InitializeGPUs();
            GPUremainingTime = GPUTime;
            GPUsPage.ResetSelection();
        }

        private void GPUUpdateTimerDisplay()
        {
            int minutes = Mathf.FloorToInt(GPUremainingTime / 60F);
            int seconds = Mathf.FloorToInt(GPUremainingTime - minutes * 60);
            string timeText = string.Format("{0:0}:{1:00}", minutes, seconds);
            GPUsPage.UpdateTimer(timeText);
        }

        public void GPUsOpenShop()
        {
            GPUsPage.Show();
            GPUsPage.ResetSelection();
        }

    }

}

