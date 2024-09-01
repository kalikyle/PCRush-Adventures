using Inventory.UI;
using Shop.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

namespace PC.UI
{
    public class PCPage2 : MonoBehaviour
    {
        [SerializeField]
        private PCItem2 itemPrefab;
        [SerializeField]
        private RectTransform contentPanel;

        [SerializeField]
        private PCDesc2 PCDesc2ription;

        [SerializeField]
        private Sprite UntestedImage;

        [SerializeField]
        private Sprite TestedImage;


        public List<PCItem2> ListOfPCs = new List<PCItem2>();

        //public Sprite PCimage, pcase, pmb, pcpu, pcpuf, pram, pgpu, pstrg, ppsu;
        //public string pcname, pcprice, casen, mbn, cpun, cpufn, ramn, gpun, strgn, psun;
        public event Action<int> OnDescriptionRequested, OnItemActionRequested;
        public void UpdateDescription(int index,Sprite pcsprite, Sprite casesprite, Sprite mbsprite, Sprite cpusprite, Sprite cpufsprite, Sprite ramsprite, Sprite gpusprite, Sprite strgsprite, Sprite psusprite,
        string PCname, string PCprice, string Casename, string mbname, string cpuname, string cpufname, string ramname, string gpuname, string strgname, string psuname, string status)
        {

            PCDesc2ription.SetDescription(pcsprite, casesprite,mbsprite,  cpusprite, cpufsprite, ramsprite,  gpusprite, strgsprite, psusprite,
            PCname,  PCprice, Casename, mbname,  cpuname,  cpufname,  ramname,  gpuname, strgname, psuname, status);
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

            PCDesc2ription.Hide();
            PCDesc2ription.ResetDescription();
            DeselectAllItems();
            
        }

        private void DeselectAllItems()
        {
            foreach (PCItem2 item in ListOfPCs)
            {
                item.DeSelect();
            }
        }
        public void AddAnotherPC()
        {
            PCItem2 uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
            uiItem.transform.SetParent(contentPanel);
            ListOfPCs.Add(uiItem);
            uiItem.OnItemClicked += HandleItemSelection;
            uiItem.OnRightMouseBtnClick += HandleRightClickAction;
        }
        public void Awake()
        {
           
            //PCDesc2ription.ResetDescription();
        }
        public void InitializedPCs(int inventorysize)
        {
            for (int i = 0; i < inventorysize; i++)
            {
                PCItem2 uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
                uiItem.transform.SetParent(contentPanel);
                ListOfPCs.Add(uiItem);
                uiItem.OnItemClicked += HandleItemSelection;
                uiItem.OnRightMouseBtnClick += HandleRightClickAction;

            }
        }
        public void UpdateData(int ItemIndex, Sprite PCimage, string PCname, string testStatus)
        {
            if(ListOfPCs.Count > ItemIndex)
            {
                if (testStatus == "Untested")
                {
                    ListOfPCs[ItemIndex].SetData(UntestedImage,PCimage, PCname);
                }
                if (testStatus == "Tested")
                {
                    ListOfPCs[ItemIndex].SetData(TestedImage, PCimage, PCname);
                }

            }
        }
        private void HandleItemSelection(PCItem2 item)
        {
            PCDesc2ription.Show();
            int index = ListOfPCs.IndexOf(item);
            if(index == -1)
            {
                return;
            }
            OnDescriptionRequested?.Invoke(index);

        }
        private void HandleRightClickAction(PCItem2 item)
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

