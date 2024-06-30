using Exchanger.Model.CPUWorld;
using Exchanger.UI.CPUWorld;
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



        private void CPUsPrepareUI()
        {
            CPUsPage.InitializedShop(CPUsGetUsedSlotsCount());


        }

        public int CPUsGetUsedSlotsCount()//this will only used the slots with items
        {
            int usedSlots = 0;
            foreach (var item in CPUsData.Procies)
            {
                if (!item.isEmpty)
                {
                    usedSlots++;
                }
            }
            return usedSlots;
        }

        public void CPUsToggleALLButton()
        {
            //shopBuy.ToggleTF = false;
            //shopBuy.ToggleBSE = false;
            // Toggle the state
            CPUsPage.ResetSelection();
            CPUsPage.ClearItems();
            CPUsPage.InitializedShop(CPUsGetUsedSlotsCount() /* GameManager.Instance.shopSize*/);



            SwordsShowAllCategory();

        }

        public void CPUsOpenShop()
        {


            // Call the methods after the delay

            CPUsPrepareUI();
            CPUsToggleALLButton();
            CPUsPage.Show();
            CPUsPage.ResetSelection();
        }

        public void SwordsShowAllCategory()
        {

            var nonEmptyItems = CPUsData.GetCurrentInventoryState().Where(item => !item.Value.isEmpty);

            int displayedItemsCount = 0;
            foreach (var item in nonEmptyItems)
            {
                //ShopUpdate(item.Value.item.InUse, item.Value.item.Sold);
                if (displayedItemsCount >= /*GameManager.instance.ShopSize*/CPUsGetUsedSlotsCount())
                    break;

                GameObject materialsNeedObject = item.Value.item.MaterialsNeed;

                // Try to get the SpriteRenderer component from the GameObject
                SpriteRenderer spriteRenderer;
                if (materialsNeedObject.TryGetComponent<SpriteRenderer>(out spriteRenderer))
                {
                    // Access the sprite from the SpriteRenderer
                    Sprite itemSprite = spriteRenderer.sprite;

                    // Now update your UI element using the retrieved sprite
                    // Example function call - you need to implement the actual UpdateData function
                    CPUsPage.UpdateData(item.Key, item.Value.item.Parts.ItemImage, itemSprite, item.Value.item.Parts.Name, item.Value.item.MaterialsAmountNeed, item.Value.item.Parts.Attack);
                }
                else
                {
                    Debug.LogError("SpriteRenderer component not found on the MaterialsNeed GameObject.");
                }



               // CPUsPage.UpdateData(item.Key, item.Value.item.Parts.ItemImage, item.Value.item.MaterialsNeed.TryGetComponent<SpriteRenderer>.sprite, item.Value.item.Parts.Name, item.Value.item.MaterialsAmountNeed, item.Value.item.Parts.Attack);

                displayedItemsCount++;
            }
        }

    }
}
