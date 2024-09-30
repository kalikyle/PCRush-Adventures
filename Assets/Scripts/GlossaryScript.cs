using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GlossaryScript : MonoBehaviour
{

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

    //forFaqs
    public Sprite ThermalQrImage;
    public Sprite WhyBuildImage;
    public Sprite CableManage;

    //fortrivia
    public Sprite BugImage;
    public Sprite hddImage;
    public Sprite eniacImage;
    public Sprite mouseImage;
    public Sprite ibmImage;
    public Sprite qwertyImage;
    public Sprite bitImage;
    public Sprite GUIImage;
    public Sprite BSODImage;
    public Sprite programmerImage;


    //for Parts Info
    public Image ImagePlaceHolder;
    public TMP_Text Header1;
    public TMP_Text AlsoKnowAs;
    public TMP_Text Caption1;
    public TMP_Text Caption2;


    //for FaQs
    public Image FaqImage;
    public GameObject FaqBorder;
    public TMP_Text FaqHeader;
    public TMP_Text FaqCaption1;
    public TMP_Text Faqtitle2;
    public TMP_Text FaqCaption2;
    public TMP_Text FaqCaption3;

    //for trvia
    public Image triviaImage;
    public GameObject triviaBorder;
    public TMP_Text triviaHeader;
    public TMP_Text triviaCaption1;
    public TMP_Text triviaRef;




    void Start()
    {
        ComputerGlossary();
    }

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
        Caption1.text = " Provides structural integrity and physical protection to the internal components of a computer, fostering an organized and secure hardware environment.";
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
        Caption2.text = " The CPU fan, also recognized as a cooling fan, maintains the optimal temperature of the CPU by dissipating heat generated during its operation. It prevents overheating, safeguarding the CPU's functionality and longevity, ensuring the stable performance of the entire system.";
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



    //for Faqs Method
    public void WhyThermal()
    {
        FaqImage.sprite = ThermalQrImage;
        FaqHeader.text = "WHY WE SHOULD APPLY THERMAL PASTE IN CPU?";
        FaqCaption1.text = "Thermal paste is crucial in PC building as it fills microscopic gaps between the CPU and its cooler, enhancing heat transfer by eliminating air pockets and irregularities on the contact surface. This compound improves thermal conductivity, ensuring efficient heat dissipation and preventing overheating, which can reduce a processor's lifespan and performance.";
        Faqtitle2.text = "Benefits of Thermal Paste Application:";
        FaqCaption2.text = "- Fills gaps for better contact between CPU and cooler.\r\n- Improves heat transfer, preventing overheating.\r\n- Acts as a protective barrier, preventing corrosion.\r\n- Enhances component longevity and performance.";
        FaqCaption3.text = "How to properly apply thermal paste in real life";
    }
    public void whyBuild()
    {
        FaqImage.sprite = WhyBuildImage;
        FaqHeader.text = "why people build their own PC?";
        FaqCaption1.text = "Building your own PC is like creating your custom LEGO set for computers. It's cool because you get to choose the exact parts you want, making it perfect for gaming, schoolwork, or whatever you love doing on a computer. Plus, you can make it look awesome with colors and lights!";
        Faqtitle2.text = "building your PC can save money in the long run!";
        FaqCaption2.text = "It's like buying the parts separately to make a super cool robot that you can upgrade whenever you want. And when you turn it on for the first time, knowing you made it all work feels pretty awesome!";
        FaqCaption3.text = "Building Your Own PC is FUN!";
    }
    public void whyExpensive()
    {
        FaqImage.sprite = WhyBuildImage;
        FaqHeader.text = "Why Computer Parts are Expensive?";
        FaqCaption1.text = "Computer parts often come with a higher price tag due to several reasons. These components are engineered to perform at high standards, requiring quality materials and precise manufacturing processes. The technology packed into each part, involves intricate designs and advanced manufacturing techniques, contributing significantly to their cost.";
        Faqtitle2.text = "the demand for computer parts, particularly those aimed at gaming.";
        FaqCaption2.text = "This consistent demand, combined with the costs associated with research, development, and marketing, influences the prices of these components. The competitive nature of the market, with several big-name manufacturers vying for market share, also impacts pricing, as companies invest in R&D to offer better-performing components, which inevitably affects the final cost to consumers.";
        FaqCaption3.text = "Building Your Own PC is FUN!";
    }
    public void whyWires()
    {
        FaqImage.sprite = WhyBuildImage;
        FaqHeader.text = "Why knowing the PSU wires is important?";
        FaqCaption1.text = "Understanding PSU (Power Supply Unit) wires is crucial for PC builders as these wires facilitate power distribution to various components. Knowing their roles and connections ensures correct installation, preventing potential issues like short circuits or component damage due to improper wiring.";
        Faqtitle2.text = "PSU wires enables builders to troubleshoot power-related issues effectively.";
        FaqCaption2.text = "Identifying wiring problems or detecting faulty cables can prevent system instability, unexpected shutdowns, or hardware failures. Knowledge of PSU wires aids in cable management, allowing builders to organize and route cables efficiently within the case, improving airflow and aesthetics while reducing potential obstructions or hazards within the system.";
        FaqCaption3.text = "Cable Management Guide";
    }
    public void whyGPU()
    {
        FaqImage.sprite = WhyBuildImage;
        FaqHeader.text = "How does the choice of a graphics card affect gaming performance?";
        FaqCaption1.text = "A powerful graphics card serves as the powerhouse behind visually demanding tasks in gaming. It handles rendering intricate textures, complex lighting effects, and high-resolution graphics, ensuring a visually immersive gaming experience.";
        Faqtitle2.text = "a robust GPU directly influences frame rates, determining how smoothly a game runs.";
        FaqCaption2.text = "Higher frame rates contribute to seamless, fluid gameplay, reducing lags or stuttering, and enhancing overall responsiveness. Additionally, a potent graphics card not only impacts gaming but also enhances performance in content creation tasks like video editing, 3D modeling, and rendering by accelerating graphical computations.";
        FaqCaption3.text = "Building Your Own PC is FUN!";
    }
    public void whyRAM()
    {
        FaqImage.sprite = WhyBuildImage;
        FaqHeader.text = "What purpose does RAM serve in a computer build?";
        FaqCaption1.text = "RAM (Random Access Memory) plays a crucial role in a computer build by acting as temporary storage for data that the CPU needs to access quickly. It allows for swift data retrieval and manipulation, facilitating faster processing speeds for applications and games.";
        Faqtitle2.text = "RAM enables efficient multitasking";
        FaqCaption2.text = "Allowing the system to handle multiple tasks simultaneously without a significant drop in performance. Its temporary storage capacity allows data to be swiftly accessed, modified, or retrieved, contributing to the overall responsiveness and efficiency of the system during various computing tasks.";
        FaqCaption3.text = "Building Your Own PC is FUN!";
    }
    public void whySSDnHDD()
    {
        FaqImage.sprite = WhyBuildImage;
        FaqHeader.text = "The difference between HDD and SSD";
        FaqCaption1.text = "HDDs, or Hard Disk Drives, rely on spinning magnetic platters and a read/write head to store and access data, offering larger storage capacities at a lower cost per GB. However, due to their mechanical nature, they operate slower than SSDs and are more susceptible to physical damage in environments with shock or vibration.";
        Faqtitle2.text = "SSD is faster than HDD";
        FaqCaption2.text = " SSDs, or Solid State Drives, utilize flash memory without any moving parts, resulting in faster read/write speeds and improved system responsiveness. While typically available in smaller capacities than HDDs, SSDs are more durable, being resistant to shock, vibration, and mechanical failure due to their solid-state construction. ";
        FaqCaption3.text = "Building Your Own PC is FUN!";
    }
    public void whyCareful()
    {
        FaqImage.sprite = WhyBuildImage;
        FaqHeader.text = "why we should be careful when we are building a PC?";
        FaqCaption1.text = "Building a PC requires caution due to the sensitive nature of its components. Firstly, the components, like the CPU, GPU, and motherboard, are delicate and can be easily damaged by static electricity. ";
        Faqtitle2.text = "Handling them without proper precautions, can result in irreparable harm.";
        FaqCaption2.text = "mishandling cables or components might result in connectivity issues or shorts, leading to system instability or failure. Also, without careful attention to detail, mistakes during installation, like improperly seating the CPU or RAM, can lead to poor performance or system boot failures.";
        FaqCaption3.text = "Building Your Own PC is FUN!";
    }
    public void whySockets()
    {
        FaqImage.sprite = WhyBuildImage;
        FaqHeader.text = "why knowing the sockets of motherboard is important?";
        FaqCaption1.text = "Knowing the sockets of a motherboard is crucial during a PC build because they dictate compatibility with various components. For instance, the CPU socket type determines which processors are compatible with the motherboard. ";
        Faqtitle2.text = " Choosing a CPU that fits the motherboard's socket ensures a proper fit and functionality.";
        FaqCaption2.text = "Similarly, the RAM sockets dictate the type and generation of RAM modules supported, affecting the system's memory capacity and speed. Additionally, expansion slots like PCIe sockets determine compatibility with GPUs, expansion cards, and other add-on components.";
        FaqCaption3.text = "Building Your Own PC is FUN!";
    }
    public void whyPCIE()
    {
        FaqImage.sprite = WhyBuildImage;
        FaqHeader.text = "What does PCIE means?";
        FaqCaption1.text = "PCIe stands for Peripheral Component Interconnect Express. It's a high-speed interface commonly used for connecting various components in computers, primarily expansion cards like GPUs (Graphics Processing Units), sound cards, network adapters, and storage devices such as SSDs (Solid State Drives).";
        Faqtitle2.text = "PCIe stands for Peripheral Component Interconnect Express.";
        FaqCaption2.text = "The PCIe interface comes in different sizes (e.g., PCIe x1, x4, x8, x16) denoting the number of data lanes they possess, determining the bandwidth and speed capabilities for connected devices.";
        FaqCaption3.text = "Building Your Own PC is FUN!";
    }

    //for trivia
    public void knowBUG()
    {
        triviaImage.sprite = BugImage;
        triviaHeader.text = "Origin of the Term \"Bug\"";
        triviaCaption1.text = "The term \"bug\" in computing originated in 1947 when Grace Hopper, a computer scientist, found a moth stuck in a relay of the Harvard Mark II computer, causing a malfunction. She then coined the term \"debugging\" to describe the process of fixing computer glitches.";
        triviaRef.text = "Reference: TheTable. (2023, September 11). Why do we call software bugs, bugs? DbVisualizer. https://www.dbvis.com/thetable/why-are-they-called-bugs/";    
    }
    public void knowHdd()
    {
        triviaImage.sprite = hddImage;
        triviaHeader.text = "Largest Hard Drive";
        triviaCaption1.text = "As of 2022, the largest commercially available hard drive is a mechanical HDD with a capacity of 20 terabytes. However, SSDs with higher capacities continue to be developed, aiming to surpass traditional hard drives.";
        triviaRef.text = "Reference: Gower, E. (2023, June 15). What’s the Largest Hard Drive You Can Buy? Alphr. https://www.alphr.com/largest-hard-drive-you-can-buy/";
    }
    public void knowENIac()
    {
        triviaImage.sprite = eniacImage;
        triviaHeader.text = "ENIAC - Early Computer";
        triviaCaption1.text = "The Electronic Numerical Integrator and Computer (ENIAC) built in the 1940s was one of the earliest general-purpose computers. It weighed over 27 tons and used around 17,000 vacuum tubes.";
        triviaRef.text = "Reference: Sheldon, R., & Wigmore, I. (2023, September 25). ENIAC (Electronic Numerical Integrator and Computer). WhatIs. https://www.techtarget.com/whatis/definition/ENIAC";
    }
    public void knowMouse()
    {
        triviaImage.sprite = mouseImage;
        triviaHeader.text = "Computer Mouse Origin";
        triviaCaption1.text = "The computer mouse was invented by Douglas Engelbart in 1964 and was made of wood. It was used to control a pointer on a computer screen during demonstrations of early graphical user interfaces.";
        triviaRef.text = "Reference: SRI International. (2023, November 13). The computer mouse and interactive computing. SRI. https://www.sri.com/hoi/computer-mouse-and-interactive-computing/";
    }
    public void knowIBM()
    {
        triviaImage.sprite = ibmImage;
        triviaHeader.text = "IBM PC Release";
        triviaCaption1.text = "IBM released its first personal computer, the IBM PC, in 1981. It became a standard for personal computing and significantly contributed to the rise of the PC industry.";
        triviaRef.text = "Reference: IBM Archives: The birth of the IBM PC. (n.d.). https://www.ibm.com/ibm/history/exhibits/pc25/pc25_birth.html";
    }
    public void knowQWERTY()
    {
        triviaImage.sprite = qwertyImage;
        triviaHeader.text = "QWERTY Keyboard Layout";
        triviaCaption1.text = "The QWERTY layout, commonly used on keyboards, was designed in the 1870s to prevent jamming on typewriters by spacing commonly used letters apart. Despite its origin, it's now used universally in computer keyboards.";
        triviaRef.text = "Reference: Starr, M. (2016, July 1). A brief history of the QWERTY keyboard. CNET. https://www.cnet.com/culture/a-brief-history-of-the-qwerty-keyboard/";
    }
    public void knowBit()
    {
        triviaImage.sprite = bitImage;
        triviaHeader.text = "Bit and Byte Origin";
        triviaCaption1.text = "The term \"bit\" is short for \"binary digit,\" representing the smallest unit of data in computing. A \"byte\" consists of 8 bits and is the basic unit for representing data in most computer systems.";
        triviaRef.text = "Reference: Bits and bytes. (n.d.). https://web.stanford.edu/class/cs101/bits-bytes.html";
    }
    public void knowGUI()
    {
        triviaImage.sprite = GUIImage;
        triviaHeader.text = "First GUI Computer";
        triviaCaption1.text = "The Xerox Alto, developed in the 1970s, was the first computer with a graphical user interface (GUI), featuring icons, windows, and a mouse, setting the stage for modern computing interfaces.";
        triviaRef.text = "Reference: Bales, R. (2023, July 31). Xerox Alto: Everything you need to know. History-Computer. https://history-computer.com/xerox-alto-guide/";
    }
    public void knowBSOD()
    {
        triviaImage.sprite = BSODImage;
        triviaHeader.text = "The 'Blue Screen of Death'";
        triviaCaption1.text = "Windows operating systems display the \"Blue Screen of Death\" (BSOD) when encountering critical system errors, signaling a severe problem requiring attention or troubleshooting.";
        triviaRef.text = "Reference: Malwarebytes. (2023, November 3). What is BSOD: Blue Screen of Death | Blue Screen Error. https://www.malwarebytes.com/cybersecurity/computer/blue-screen-of-death";
    }
    public void knowProgrammer()
    {
        triviaImage.sprite = programmerImage;
        triviaHeader.text = "World's First Computer Programmer'";
        triviaCaption1.text = "Ada Lovelace, an English mathematician, is often regarded as the world's first computer programmer for her work on Charles Babbage's Analytical Engine in the 1800s.";
        triviaRef.text = "Reference: Gregersen, E. (n.d.). Ada Lovelace: the first computer programmer. Encyclopedia Britannica. https://www.britannica.com/story/ada-lovelace-the-first-computer-programmer";
    }


}
