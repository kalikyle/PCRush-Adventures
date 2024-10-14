using Shop.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace PC.UI
{
    public class PCPage : MonoBehaviour
    {
        [SerializeField]
        private PCItem itemPrefab;
        [SerializeField]
        private RectTransform contentPanel;

        [SerializeField]
        private PCDesc PCDescription;

        [SerializeField]
        private Sprite InUseImage;

        [SerializeField]
        private Sprite NotInUseImage;


       


        public List<PCItem> ListOfPCs = new List<PCItem>();

        //public Sprite PCimage, pcase, pmb, pcpu, pcpuf, pram, pgpu, pstrg, ppsu;
        //public string pcname, pcprice, casen, mbn, cpun, cpufn, ramn, gpun, strgn, psun;
        public event Action<int> OnDescriptionRequested, OnItemActionRequested;
        public void UpdateDescription(int index, Sprite pcsprite, Sprite casesprite, Sprite mbsprite, Sprite cpusprite, Sprite cpufsprite, Sprite ramsprite, Sprite gpusprite, Sprite strgsprite, Sprite psusprite,
string PCname, string Casename, string mbname, string cpuname, string cpufname, string ramname, string gpuname, string strgname, string psuname, bool InUse, string perks,
string CaseRarityText, string MBRarityText, string CPURarityText, string CPUFRarityText, string RAMRarityText, string GPURarityText, string STRGRarityText, string PSURarityText)
        {
            PCDescription.SetDescription(pcsprite, casesprite, mbsprite, cpusprite, cpufsprite, ramsprite, gpusprite, strgsprite, psusprite,
            PCname, Casename, mbname, cpuname, cpufname, ramname, gpuname, strgname, psuname, InUse, perks,
            CaseRarityText, MBRarityText, CPURarityText, CPUFRarityText, RAMRarityText, GPURarityText, STRGRarityText, PSURarityText);
            DeselectAllItems();
            ListOfPCs[index].Select();
        }

        public void Start()
        {
            ResetSelection();
            //ListOfPCs[0].SetData(PCimage, pcname);
        }

        public void ResetSelection()
        {

            PCDescription.Hide();
            PCDescription.ResetDescription();
            DeselectAllItems();
            
        }

        private void DeselectAllItems()
        {
            foreach (PCItem item in ListOfPCs)
            {
                item.DeSelect();
            }
        }
        public void AddAnotherPC()
        {
            PCItem uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
            uiItem.transform.SetParent(contentPanel);
            uiItem.transform.localScale = new Vector3(1, 1, 1);
            ListOfPCs.Add(uiItem);
            uiItem.OnItemClicked += HandleItemSelection;
            uiItem.OnRightMouseBtnClick += HandleRightClickAction;
        }

        public void ClearItems()
        {
            foreach (var item in ListOfPCs)
            {
                item.gameObject.SetActive(false);// Assuming ListOfShopItems contains the GameObjects of shop items
            }
            ListOfPCs.Clear();
        }

        public void Awake()
        {
           
            //PCDescription.ResetDescription();
        }
        public void InitializedPCs(int inventorysize)
        {
            for (int i = 0; i < inventorysize; i++)
            {
                PCItem uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
                uiItem.transform.SetParent(contentPanel);
                uiItem.transform.localScale = new Vector3(1, 1, 1);
                ListOfPCs.Add(uiItem);
                uiItem.OnItemClicked += HandleItemSelection;
                uiItem.OnRightMouseBtnClick += HandleRightClickAction;

            }
        }
        public void UpdateData(int ItemIndex, Sprite PCimage, string PCname, bool inUse)
        {
            if(ListOfPCs.Count > ItemIndex)
            {
                if (inUse == true)
                {
                    
                    ListOfPCs[ItemIndex].SetData(InUseImage,PCimage, PCname);
                    ListOfPCs[ItemIndex].InUse.gameObject.SetActive(true);
                }
                else
                {
                    //disable inuse sprite
                    ListOfPCs[ItemIndex].SetData(NotInUseImage, PCimage, PCname);
                    ListOfPCs[ItemIndex].InUse.gameObject.SetActive(false);
                }

            }
        }
        private void HandleItemSelection(PCItem item)
        {
            PCDescription.Show();
            int index = ListOfPCs.IndexOf(item);
            if(index == -1)
            {
                return;
            }
            OnDescriptionRequested?.Invoke(index);
            


        }
        private void HandleRightClickAction(PCItem item)
        {
            int index = ListOfPCs.IndexOf(item);
            if (index == -1)
            {
                return;
            }
            OnItemActionRequested?.Invoke(index);
        }

        /* public void Show()
         {
             gameObject.SetActive(true);

         }
         public void Hide()
         {
             gameObject.SetActive(false);
         }*/


    }


}

