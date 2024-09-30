using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PRA_Glossary_Script : MonoBehaviour
{
    // for Game Info Objects
    public Sprite HomeWorldImage;
    public Sprite HardwareRegionsImage;
    public Sprite EnemiesImage;
    public Sprite ComputerPartsImage;
    public Sprite YourPCImage;
    public Sprite EquipmentsImage;
    public Sprite MaterialsImage;
    public Sprite InventoriesImage;
    public Sprite BuildingDeskImage;
    public Sprite YourDesktopImage;
    public Sprite NPCsImage;
    public Sprite TheExchangersImage;
    public Sprite PCRushShopImage;
    
    // for Game Info Content
    public Image GI_ImagePlaceholder;
    public TMP_Text GI_Title;
    public TMP_Text GI_Description;
    // LAST OF GAME INFO OBJECTS


    //for Hardware IRL Objects
    public Sprite ComputerImage;
    public Sprite CaseImage;
    public Sprite MotherboardImage;
    public Sprite CPUImage;
    public Sprite CPUFImage;
    public Sprite RAMImage;
    public Sprite GPUImage;
    public Sprite STORAGEImage;
    public Sprite PSUImage;
    public Sprite _24PINImage;
    public Sprite _8PINImage;
    public Sprite PCIEImage;
    public Sprite SATAImage;
    public Sprite monitorImage;
    public Sprite knbImage;

    //for Hardware IRL Content
    public Image ImagePlaceHolder;
    public TMP_Text Header1;
    public TMP_Text AlsoKnowAs;
    public TMP_Text Caption1;
    public TMP_Text Caption2;
    // LAST OF HARDWARE IRL OBJECTS

    void Start()
    {
        HomeWorldInfo();        
    }

    // GAME INFO METHODS
    public void HomeWorldInfo()
    {
        GI_ImagePlaceholder.sprite = HomeWorldImage;
        GI_Title.text = "Home World";
        GI_Description.text = "It serves as the central hub of the game, where players can manage their resources, access their Desk Room, and utilize the Build Room for computer assembly. It acts as a headquarters for monitoring components gathered during exploration. From here, players can plan their next moves, organize materials, and prepare for further adventures in the game.";
    }
    public void HardwareRegionsInfo()
    {
        GI_ImagePlaceholder.sprite = HardwareRegionsImage;
        GI_Title.text = "Hardware Regions";
        GI_Description.text = "They are diverse, combat-driven landscapes where players engage in strategic battles against a variety of enemies. Each region presents unique challenges, requiring players to use a combination of tactical combat skills to progress. As they defeat enemies, players collect valuable materials essential for crafting computer parts and upgrading their system units.";
    }
    public void EnemiesInfo()
    {   
        GI_ImagePlaceholder.sprite = EnemiesImage;
        GI_Title.text = "Enemies";
        GI_Description.text = "The harmful entities that the players will encounter and need to defeat in different worlds to gather the materials they carry.";
    }
    public void ComputerPartsInfo()
    {   
        GI_ImagePlaceholder.sprite = ComputerPartsImage;
        GI_Title.text = "Computer Parts";
        GI_Description.text = "It tracks parts like CPUs and RAM, helping players manage resources for building or repairing systems.";
    }
    public void YourPCInfo()
    {
        GI_ImagePlaceholder.sprite = YourPCImage;
        GI_Title.text = "Your PC";
        GI_Description.text = "It displays your computer information, including stats and perks, along with access to PC shops and settings. Players can manage their hardware and explore features from other worlds within the game.";
    }
    public void EquipmentsInfo()
    {
        GI_ImagePlaceholder.sprite = EquipmentsImage;
        GI_Title.text = "Equipments";
        GI_Description.text = "They are essential for combat, with each item offering different attack, defense, or armor stats. Players can upgrade their gear using in-game currency, improving their performance against enemies. The equipment system also ties into resource collection and trading, enhancing the gameplay loop.";
    }
    public void MaterialsInfo()
    {   
        GI_ImagePlaceholder.sprite = MaterialsImage;
        GI_Title.text = "Materials";
        GI_Description.text = "These are the materials collected after defeating an enemy in The Hardwares. Exchange these materials to The Exchangers to acquire more powerful hardware.";
    }
    public void InventoriesInfo()
    {
        GI_ImagePlaceholder.sprite = InventoriesImage;
        GI_Title.text = "Inventory";
        GI_Description.text = "It stores the helmets, swords, armor, and shields bought from the sword dealer, which players can equip or sell. It also tracks hardware materials like silicon wafers, used to buy computer components in the game.";
    }
    public void BuildingDeskInfo()
    {
        GI_ImagePlaceholder.sprite = BuildingDeskImage;
        GI_Title.text = "Building Desk";
        GI_Description.text = "It's an interactive workspace where players can assemble custom computers by selecting and arranging components, simulating real-world PC building. Players can explore various regions to collect computer parts, which they can use to upgrade and enhance their systems. This dynamic environment fosters creativity and deepens technical knowledge as players experiment with configurations and improve their builds through practical application.";
    }
    public void YourDesktopInfo()
    {
        GI_ImagePlaceholder.sprite = YourDesktopImage;
        GI_Title.text = "Your Desktop";
        GI_Description.text = "allows players to personalize their virtual gaming space by customizing the monitor, keyboard, mouse, and many more. Players can add unique decorations and accessories to reflect their personal style and preferences. This feature creates an immersive experience, letting players craft their ideal gaming environment.";
    }
    public void NPCsInfo()
    {
        GI_ImagePlaceholder.sprite = NPCsImage;
        GI_Title.text = "NPCs";
        GI_Description.text = "NPCs in The Hardwares are there to help you with your equipment and can be involved in different quests. Interact with them to provide assistance to you.";
    }
    public void TheExchangersInfo()
    {
        GI_ImagePlaceholder.sprite = TheExchangersImage;
        GI_Title.text = "The Exchangers";
        GI_Description.text = "They're the NPC you need in order to exchange the materials you collect to the hardware you desired for. After exchanging those materials, your hardware will be dropped off in the player's living room in the Home World.";
    }
    public void PCRushShopInfo()
    {
        GI_ImagePlaceholder.sprite = PCRushShopImage;
        GI_Title.text = "PCRush Shop";
        GI_Description.text = "It lets players purchase and customize computer additional and decorations for their virtual workspace. Using in-game currency, players can buy new items like monitors, keyboards, desks, and more. Items bought from the shop are reflected in the player's Desk Room, allowing for continuous personalization.";
    }
    // END OF GAME INFO METHODS

    // HARDWARE IRL
    public void ComputerGlossary()
    {
        ImagePlaceHolder.sprite = ComputerImage;
        Header1.text = "Computer";
        AlsoKnowAs.text = "Also Known As: PC, Personal Computer and System Unit";
        Caption1.text = "Often abbreviated as \"PC,\" this device forms the cornerstone of modern computing, offering a versatile platform for various digital tasks and entertainment endeavors.";
        Caption2.text = "The computer, encompasses a versatile electronic device designed to perform various tasks, from calculations to multimedia processing. It integrates essential components like the central processing unit (CPU), memory (RAM), storage, and input/output devices, orchestrating a harmonious blend of hardware and software to execute myriad functions.";
    }

    public void CaseGlossary()
    {
        ImagePlaceHolder.sprite = CaseImage;
        Header1.text = "Case";
        AlsoKnowAs.text = "Also Known As: Tower, Chassis";
        Caption1.text = "Provides structural integrity and physical protection to the internal components of a computer, fostering an organized and secure hardware environment.";
        Caption2.text = "The case, commonly known as a tower or chassis, encapsulates and protects the internal components of a computer. It provides a structural framework, housing the motherboard, power supply unit (PSU), storage drives, and other hardware components, ensuring physical safety while allowing connectivity and ventilation for efficient cooling.";
    }

    public void MotherboardGlossary()
    {
        ImagePlaceHolder.sprite = MotherboardImage;
        Header1.text = "Motherboard";
        AlsoKnowAs.text = "Also Known As: Mainboard, Logic Board";
        Caption1.text = "Known interchangeably as a mainboard or logic board, the motherboard serves as the essential nexus, enabling seamless communication among diverse hardware components, fostering a cohesive computing ecosystem.";
        Caption2.text = "Serving as the backbone of a computer, the motherboard, or mainboard, acts as a hub connecting various hardware components. It houses critical elements such as the CPU, RAM slots, expansion slots, and interfaces for peripheral devices, facilitating data transfer and enabling seamless communication among system parts.";
    }

    public void CPUGlossary()
    {
        ImagePlaceHolder.sprite = CPUImage;
        Header1.text = "CPU (Central Processing Unit)";
        AlsoKnowAs.text = "Also Known As: Processor";
        Caption1.text = "Orchestrates the core computational operations of a computer, serving as the primary engine driving the system's functionality.";
        Caption2.text = "The CPU, often referred to as the processor, functions as the brain of a computer, executing instructions and performing calculations essential for system operation. It carries out tasks by processing data, manipulating information, and coordinating the functioning of hardware components, significantly impacting overall system performance.";
    }
    public void CPUFGlossary()
    {
        ImagePlaceHolder.sprite = CPUFImage;
        Header1.text = "CPU Fan";
        AlsoKnowAs.text = "Also Known As: Cooling Fan";
        Caption1.text = "The CPU fan diligently regulates the processor's temperature, ensuring optimal thermal conditions for sustained performance.";
        Caption2.text = "The CPU fan, also recognized as a cooling fan, maintains the optimal temperature of the CPU by dissipating heat generated during its operation. It prevents overheating, safeguarding the CPU's functionality and longevity, ensuring the stable performance of the entire system.";
    }

    public void RAMGlossary()
    {
        ImagePlaceHolder.sprite = RAMImage;
        Header1.text = "RAM (Random Access Memory)";
        AlsoKnowAs.text = "Also Known As: Memory";
        Caption1.text = "RAM acts as a swift data repository, facilitating quick access to information vital for ongoing computer operations.";
        Caption2.text = "RAM, or memory, serves as the temporary workspace for active data and program instructions while the computer is operational. It allows quick access to information, enhancing system responsiveness and multitasking capabilities by temporarily storing data required for ongoing processes.";
    }
    public void GPUGlossary()
    {
        ImagePlaceHolder.sprite = GPUImage;
        Header1.text = "GPU (Graphics Processing Unit)";
        AlsoKnowAs.text = "Also Known As: Video Card";
        Caption1.text = "the GPU excels in handling graphical computations, enhancing visual outputs and supporting graphic-intensive tasks.";
        Caption2.text = "The GPU, often referred to as a video card, specializes in rendering graphics, handling image processing, and accelerating visual computations. It significantly enhances a computer's ability to display high-quality images, videos, and support complex graphical applications.";
    }
    public void StorageGlossary()
    {
        ImagePlaceHolder.sprite = STORAGEImage;
        Header1.text = "Storage";
        AlsoKnowAs.text = "Also Known As: Hard Disk Drive (HDD) or Solid-State Drive (SSD)";
        Caption1.text = "Renowned as hard disk drives (HDDs) or solid-state drives (SSDs), these storage devices ensure efficient data management, offering persistent storage for crucial files and applications.";
        Caption2.text = "Storage devices, including hard drives and solid-state drives (SSDs), store data permanently or temporarily. They enable the retrieval, saving, and long-term storage of files, software, and the operating system, playing a crucial role in data management and accessibility.";
    }
    public void PSUGlossary()
    {
        ImagePlaceHolder.sprite = PSUImage;
        Header1.text = "PSU (Power Supply Unit)";
        AlsoKnowAs.text = "Also Known As: Power Supply";
        Caption1.text = "The PSU delivers essential electrical power to all components within the computer system, sustaining smooth and reliable system operation.";
        Caption2.text = "The PSU, or power supply unit, furnishes electrical power to all components within a computer system. It converts incoming AC power into DC power, supplying the required voltages and currents to ensure the stable and efficient operation of the computer.";
    }
    public void ATX24PINGlossary()
    {
        ImagePlaceHolder.sprite = _24PINImage;
        Header1.text = "ATX 24-PIN";
        AlsoKnowAs.text = "Also Known As: Main Power Connector";
        Caption1.text = "The ATX 24-pin connector is the primary power supply connection on a motherboard, supplying power to the main components. It carries various voltages, including +3.3V, +5V, and +12V, providing power to the CPU, RAM, PCIe slots, and other motherboard components.";
        Caption2.text = "This connector ensures stable power delivery to the system, enabling it to function properly. The 24-pin design comprises a 20-pin block with an additional 4-pin section that allows compatibility with older 20-pin motherboards.";
    }
    public void ATX8PINGlossary()
    {
        ImagePlaceHolder.sprite = _8PINImage;
        Header1.text = "ATX12V 8-4 PIN";
        AlsoKnowAs.text = "Also Known As: CPU Power Connector";
        Caption1.text = "The ATX12V 8-4 pin connector is specifically designed to provide power to the CPU. It delivers +12V power to the processor, ensuring stable and sufficient power for its operation. ";
        Caption2.text = "The connector is often split into an 8-pin block, which can be divided into two 4-pin sections, providing flexibility for compatibility with motherboards that might only have a 4-pin CPU power socket. ";
    }
    public void PcieGlossary()
    {
        ImagePlaceHolder.sprite = PCIEImage;
        Header1.text = "6+2 PIN PCIE";
        AlsoKnowAs.text = "Also Known As: PCIe Power Connector";
        Caption1.text = "The PCIe power connector, often seen as a 6+2 pin configuration, is used to supply power to graphics cards (GPUs).";
        Caption2.text = "It provides additional power beyond what the motherboard PCIe slot delivers, especially for high-end GPUs that require more power for optimal performance. The 6+2 design allows flexibility, enabling it to function as either a 6-pin or 8-pin connector as required by the graphics card.";
    }
    public void SATAGlossary()
    {
        ImagePlaceHolder.sprite = SATAImage;
        Header1.text = "SATA Power Connector";
        AlsoKnowAs.text = "Also Known As: Serial ATA Power Connector";
        Caption1.text = "The SATA power connector is used to supply power to SATA drives such as SSDs, HDDs, and optical drives. It delivers +3.3V, +5V, and +12V power to these drives, supporting their operation.";
        Caption2.text = "SATA power connectors come in a flattened L-shaped design and are commonly found in modern power supplies due to their prevalence in current storage devices.";
    }
    public void MonitorGlossary()
    {
        ImagePlaceHolder.sprite = monitorImage;
        Header1.text = "Monitor";
        AlsoKnowAs.text = "Also Known As: Display or Screen.";
        Caption1.text = "A monitor, also referred to as a display screen or visual display unit (VDU), is the primary output device for a computer. It visually presents the data processed by the computer's graphics card. ";
        Caption2.text = "Monitors come in various types, including LCD (Liquid Crystal Display), LED (Light-Emitting Diode), OLED (Organic Light-Emitting Diode), and more. They vary in size, resolution, refresh rate, and panel technology, offering different visual experiences suited for diverse tasks such as gaming, graphic design, video editing, and general office work.";
    }
    public void KNBGlossary()
    {
        ImagePlaceHolder.sprite = knbImage;
        Header1.text = "keyboard and mouse";
        AlsoKnowAs.text = "Also Known As: Keypad, Pointing Device and Peripheral Set";
        Caption1.text = "The keyboard and mouse are essential input devices for a computer. The keyboard allows users to input text, commands, and shortcuts, featuring alphanumeric keys, function keys, and additional keys for specific functions like volume control or media playback.";
        Caption2.text = "These combos are popular among users seeking a unified look for their desktop setup or those who prefer purchasing both input devices simultaneously to ensure compatibility and a cohesive user experience.";
    }
}
