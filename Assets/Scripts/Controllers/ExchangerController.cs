using Exchanger.Model.CPUWorld;
using Exchanger.UI.CPUWorld;
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
        [SerializeField]
        private CPUWorldExchangerPage CPUsPage;

        [SerializeField]
        private CPUWorldItemSO CPUsData;

        private Dictionary<int, float> remainingTimes = new Dictionary<int, float>();
        private Dictionary<int, bool> cpuDisplayed = new Dictionary<int, bool>();
        public int currentlevel;
        private Queue<int> cpusToReplace = new Queue<int>();

        public float Time = 120f;

        private float remainingTime = 0f; // 3 minutes in seconds

        public Button RefreshNow;

        //private void CPUsPrepareUI()
        //{
        //    CPUsPage.InitializedShop(CPUsGetUsedSlotsCount());


        //}

        //public int CPUsGetUsedSlotsCount()//this will only used the slots with items
        //{
        //    int usedSlots = 0;
        //    foreach (var item in CPUsData.Procies)
        //    {
        //        if (!item.isEmpty)
        //        {
        //            usedSlots++;
        //        }
        //    }
        //    return usedSlots;
        //}

        //public void CPUsToggleALLButton()
        //{
        //    //shopBuy.ToggleTF = false;
        //    //shopBuy.ToggleBSE = false;
        //    // Toggle the state
        //    CPUsPage.ResetSelection();
        //    CPUsPage.ClearItems();
        //    CPUsPage.InitializedShop(CPUsGetUsedSlotsCount() /* GameManager.Instance.shopSize*/);



        //    SwordsShowAllCategory();

        //}

        //public void CPUsOpenShop()
        //{


        //    // Call the methods after the delay

        //    CPUsPrepareUI();
        //    CPUsToggleALLButton();
        //    CPUsPage.Show();
        //    CPUsPage.ResetSelection();
        //}

        //public void SwordsShowAllCategory()
        //{

        //    var nonEmptyItems = CPUsData.GetCurrentInventoryState().Where(item => !item.Value.isEmpty);

        //    int displayedItemsCount = 0;
        //    foreach (var item in nonEmptyItems)
        //    {
        //        //ShopUpdate(item.Value.item.InUse, item.Value.item.Sold);
        //        if (displayedItemsCount >= /*GameManager.instance.ShopSize*/CPUsGetUsedSlotsCount())
        //            break;

        //        GameObject materialsNeedObject = item.Value.item.MaterialsNeed;

        //        // Try to get the SpriteRenderer component from the GameObject
        //        SpriteRenderer spriteRenderer;
        //        if (materialsNeedObject.TryGetComponent<SpriteRenderer>(out spriteRenderer))
        //        {
        //            // Access the sprite from the SpriteRenderer
        //            Sprite itemSprite = spriteRenderer.sprite;

        //            // Now update your UI element using the retrieved sprite
        //            // Example function call - you need to implement the actual UpdateData function
        //            CPUsPage.UpdateData(item.Key, item.Value.item.Parts.ItemImage, itemSprite, item.Value.item.Parts.Name, item.Value.item.MaterialsAmountNeed, item.Value.item.Parts.Attack);
        //        }
        //        else
        //        {
        //            Debug.LogError("SpriteRenderer component not found on the MaterialsNeed GameObject.");
        //        }



        //       // CPUsPage.UpdateData(item.Key, item.Value.item.Parts.ItemImage, item.Value.item.MaterialsNeed.TryGetComponent<SpriteRenderer>.sprite, item.Value.item.Parts.Name, item.Value.item.MaterialsAmountNeed, item.Value.item.Parts.Attack);

        //        displayedItemsCount++;
        //    }
        //}

        void Start()
        {
            remainingTime = Time;
            currentlevel = GameManager.instance.PlayerLevel;
            CPUsData.ShuffleCPUs();
            CPUsPage.InitializedCPU(CPUsData.size);
            InitializeCPUs();

            StartCoroutine(UpdateTimers());

            RefreshNow.onClick.AddListener(RefreshCPUs);
        }

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

        public void Update()
        {
            currentlevel = GameManager.instance.PlayerLevel;
        }
    }

}

