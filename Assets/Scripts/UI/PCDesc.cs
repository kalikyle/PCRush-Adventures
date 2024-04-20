using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

namespace PC.UI
{
    public class PCDesc : MonoBehaviour
    {
        [SerializeField]
        private Image PCImage;
        [SerializeField]
        private TMP_Text PCName;
        

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

        //[SerializeField]
        //private TMP_Text Status;

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
        private Button UseButton;

        [SerializeField]
        private Button UsedButton;
        public void Start()
        {
            
        }
        public void Awake()
        {
            ResetDescription();
        }
        public void ResetDescription()
        {

            PCImage.gameObject.SetActive(false);
            PCName.text = "";
            //PCPrice.text = "";

            CaseName.text = "";
            MBName.text = "";
            CPUName.text = "";
            CPUFName.text = "";
            RAMName.text = "";
            GPUName.text = "";
            STRGName.text = "";
            PSUName.text = "";
            //Status.text = "";

           CaseImage.gameObject.SetActive(false);
            MBImage.gameObject.SetActive(false);
            CPUImage.gameObject.SetActive(false);
            CPUFImage.gameObject.SetActive(false);
            RAMImage.gameObject.SetActive(false);
            GPUImage.gameObject.SetActive(false);
            STRGImage.gameObject.SetActive(false);
            PSUImage.gameObject.SetActive(false);
        }
        public void SetDescription(Sprite pcsprite, Sprite casesprite, Sprite mbsprite, Sprite cpusprite, Sprite cpufsprite, Sprite ramsprite, Sprite gpusprite, Sprite strgsprite, Sprite psusprite,
        string PCname, string Casename, string mbname, string cpuname, string cpufname, string ramname, string gpuname, string strgname, string psuname, bool inUse)
        {
            PCImage.gameObject.SetActive(true);
            PCImage.sprite = pcsprite;
            PCName.text = PCname;
            

            CaseName.text = Casename;
            MBName.text = mbname;
            CPUName.text = cpuname;
            CPUFName.text = cpufname;
            RAMName.text = ramname;
            GPUName.text = gpuname;
            STRGName.text = strgname;
            PSUName.text = psuname;

           // Status.text = status;

            CaseImage.gameObject.SetActive(true);
            CaseImage.sprite = casesprite;

            MBImage.gameObject.SetActive(true);
            MBImage.sprite = mbsprite;

            CPUImage.gameObject.SetActive(true);
            CPUImage.sprite = cpusprite;

            CPUFImage.gameObject.SetActive(true);
            CPUFImage.sprite = cpufsprite;

            RAMImage.gameObject.SetActive(true);
            RAMImage.sprite = ramsprite;

            GPUImage.gameObject.SetActive(true);
            GPUImage.sprite = gpusprite;

            STRGImage.gameObject.SetActive(true);
            STRGImage.sprite = strgsprite;

            PSUImage.gameObject.SetActive(true);
            PSUImage.sprite = psusprite;



            if(inUse == true){
                UsedButton.gameObject.SetActive (true);
                UseButton.gameObject.SetActive(false);
            }
            else
            {
                UseButton.gameObject.SetActive (true);
                UsedButton.gameObject.SetActive(false);
            }


        }
        public void Show()
        {

            gameObject.SetActive(true);

        }
        public void Hide()
        {
            gameObject.SetActive(false);

        }

    }
}

