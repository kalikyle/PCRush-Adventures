using PartsInventory.Model;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ComputerDescription : MonoBehaviour
{
    //for use pc desc
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

    [SerializeField]
    private TMP_Text Perks;
    [SerializeField]
    private TMP_Text Status;


    [SerializeField]
    private TMP_Text CaseRarity;
    [SerializeField]
    private TMP_Text MBRarity;
    [SerializeField]
    private TMP_Text CPURarity;
    [SerializeField]
    private TMP_Text CPUFRarity;
    [SerializeField]
    private TMP_Text RAMRarity;
    [SerializeField]
    private TMP_Text GPURarity;
    [SerializeField]
    private TMP_Text STRGRarity;
    [SerializeField]
    private TMP_Text PSURarity;

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
    private Button CaseInfoBTN;
    [SerializeField]
    private Button MBInfoBTN;
    [SerializeField]
    private Button CPUInfoBTN;
    [SerializeField]
    private Button CPUFInfoBTN;
    [SerializeField]
    private Button RAMInfoBTN;
    [SerializeField]
    private Button GPUInfoBTN;
    [SerializeField]
    private Button STRGInfoBTN;
    [SerializeField]
    private Button PSUInfoBTN;

    public GameObject PartsPanel;

    [SerializeField]
    private Image PartImage;

    [SerializeField]
    private TMP_Text PartsName;
    [SerializeField]
    private TMP_Text PartsCategory;
    [SerializeField]
    private TMP_Text PartsRarity;
    [SerializeField]
    private TMP_Text PartsPerks;
    [SerializeField]
    private TMP_Text PartsPrice;

    private PCSO ClickedPC;
    void Start()
    {
        CaseInfoBTN.onClick.AddListener(() => ShowParts("Case", ClickedPC));
        MBInfoBTN.onClick.AddListener(() => ShowParts("Motherboard", ClickedPC));
        CPUInfoBTN.onClick.AddListener(() => ShowParts("CPU", ClickedPC));
        RAMInfoBTN.onClick.AddListener(() => ShowParts("RAM", ClickedPC));
        CPUFInfoBTN.onClick.AddListener(() => ShowParts("CPU Fan", ClickedPC));
        GPUInfoBTN.onClick.AddListener(() => ShowParts("Video Card", ClickedPC));
        STRGInfoBTN.onClick.AddListener(() => ShowParts("Storage", ClickedPC));
        PSUInfoBTN.onClick.AddListener(() => ShowParts("PSU", ClickedPC));
    }

    public void ShowPCData(PCSO PC, string perks)
    {
        

        ClickedPC = PC;
        this.PCImage.sprite = PC.Case.ItemImage;
        this.PCName.text = PC.PCName;
        this.Perks.text = perks;
        this.CaseName.text = PC.Case.Name;
        this.MBName.text = PC.Motherboard.Name;
        this.CPUName.text = PC.CPU.Name;
        this.CPUFName.text = PC.CPUFan.Name;
        this.RAMName.text = PC.RAM.Name;
        this.GPUName.text = PC.GPU.Name;
        this.STRGName.text = PC.STORAGE.Name;
        this.PSUName.text = PC.PSU.Name;
        this.CaseImage.sprite = PC.Case.ItemImage;
        this.MBImage.sprite = PC.Motherboard.ItemImage;
        this.CPUImage.sprite = PC.CPU.ItemImage;
        this.RAMImage.sprite = PC.RAM.ItemImage;
        this.CPUFImage.sprite = PC.CPUFan.ItemImage;
        this.GPUImage.sprite = PC.GPU.ItemImage;
        this.STRGImage.sprite = PC.STORAGE.ItemImage;
        this.PSUImage.sprite = PC.PSU.ItemImage;
        this.CaseRarity.text = PC.Case.rarity;
        this.MBRarity.text = PC.Motherboard.rarity;
        this.CPURarity.text = PC.CPU.rarity;
        this.RAMRarity.text = PC.RAM.rarity;
        this.CPUFRarity.text = PC.CPUFan.rarity;
        this.GPURarity.text = PC.GPU.rarity;
        this.STRGRarity.text = PC.STORAGE.rarity;
        this.PSURarity.text = PC.PSU.rarity;


        if(PC.inUse == true)
        {
            Status.text = "<color=green>Currently Use PC</color>";
        }
        else
        {
            Status.text = "<color=orange>Not Used</color>";
        }
    }

    public void ShowParts(string Category, PCSO pc) 
    {
        PartsPanel.SetActive(true);
        switch (Category) {

            case "Case":
                PartImage.sprite = pc.Case.ItemImage;
                PartsName.text = pc.Case.Name;
                PartsCategory.text = pc.Case.Category;
                PartsRarity.text = pc.Case.rarity;
                PartsPerks.text = "Critical Chance +" + pc.Case.CriticalChance.ToString();
                PartsPrice.text = pc.Case.Price.ToString();
                break;

            case "Motherboard":
                PartImage.sprite = pc.Motherboard.ItemImage;
                PartsName.text = pc.Motherboard.Name;
                PartsCategory.text = pc.Motherboard.Category;
                PartsRarity.text = pc.Motherboard.rarity;
                PartsPerks.text = "Attack Damage +" + pc.Motherboard.AttackDamage.ToString();
                PartsPrice.text = pc.Motherboard.Price.ToString();
                break;

            case "CPU":
                PartImage.sprite = pc.CPU.ItemImage;
                PartsName.text = pc.CPU.Name;
                PartsCategory.text = pc.CPU.Category;
                PartsRarity.text = pc.CPU.rarity;
                PartsPerks.text = "Health +" + pc.CPU.Health.ToString();
                PartsPrice.text = pc.CPU.Price.ToString();
                break;

            case "RAM":
                PartImage.sprite = pc.RAM.ItemImage;
                PartsName.text = pc.RAM.Name;
                PartsCategory.text = pc.RAM.Category;
                PartsRarity.text = pc.RAM.rarity;
                PartsPerks.text = "Armor +" + pc.RAM.Armor.ToString();
                PartsPrice.text = pc.RAM.Price.ToString();
                break;
            
            case "CPU Fan":
                PartImage.sprite = pc.CPUFan.ItemImage;
                PartsName.text = pc.CPUFan.Name;
                PartsCategory.text = pc.CPUFan.Category;
                PartsRarity.text = pc.CPUFan.rarity;
                PartsPerks.text = "Health Regen +" + pc.CPUFan.HealthRegen.ToString();
                PartsPrice.text = pc.CPUFan.Price.ToString();
                break;

            case "Video Card":
                PartImage.sprite = pc.GPU.ItemImage;
                PartsName.text = pc.GPU.Name;
                PartsCategory.text = pc.GPU.Category;
                PartsRarity.text = pc.GPU.rarity;
                PartsPerks.text = "Mana +" + pc.GPU.Mana.ToString();
                PartsPrice.text = pc.GPU.Price.ToString();
                break;

            case "Storage":
                PartImage.sprite = pc.STORAGE.ItemImage;
                PartsName.text = pc.STORAGE.Name;
                PartsCategory.text = pc.STORAGE.Category;
                PartsRarity.text = pc.STORAGE.rarity;
                PartsPerks.text = "Mana Regen +" + pc.STORAGE.ManaRegen.ToString();
                PartsPrice.text = pc.STORAGE.Price.ToString();
                break;

            case "PSU":
                PartImage.sprite = pc.PSU.ItemImage;
                PartsName.text = pc.PSU.Name;
                PartsCategory.text = pc.PSU.Category;
                PartsRarity.text = pc.PSU.rarity;
                PartsPerks.text = "Walk Speed +" + pc.PSU.WalkSpeed.ToString();
                PartsPrice.text = pc.PSU.Price.ToString();
                break;
        }
    }

    public void OpenDesc()
    {
        gameObject.SetActive(true);
    }
}
