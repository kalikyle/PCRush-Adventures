using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerTeleport : MonoBehaviour
    {
        private GameObject homeTeleport;
        public GameObject DeskPanel;
        public GameObject UIPanel;
        public GameObject inExplorePanel;
        public GameObject BuildRoom;
        public GameObject Desktop;
        public GameObject GameMap;

        public GameObject InvBTN;
        public GameObject EquippedStats;
       
        public TMP_Text Name;
        public TMP_Text WorldName;

        public LeanTweenAnimate LTA;

        public bool OpenDesk = false;
        public bool OpenBuild = false;
        public bool EditorOpen = false;
        public bool BackToHomeWorld = false;



    public void Awake()
    {
       

    }

    public void Start()
    {
        GameManager.instance.HomeWorld = true;
        GameManager.instance.Enter.onClick.AddListener(() =>
        {
            TheTeleporter();
        });

        if (GameManager.instance.HomeWorld == true)
        {
        }
        else if (GameManager.instance.CPUWorld == true)
        {
        }
        else if (GameManager.instance.RAMWorld == true) { }
        else if (GameManager.instance.CPUFWorld == true) { }
        else if (GameManager.instance.GPUWorld == true) { }
        else if (GameManager.instance.StorageWorld == true) { }
        else if (GameManager.instance.PSUWorld == true) { }
        else if (GameManager.instance.MBWorld == true) { }
        else if (GameManager.instance.CaseWorld == true) { }
    }

    void Update()
        {
            //for doors
            if (Input.GetKeyDown(KeyCode.E))
            {
               TheTeleporter();
            }

        
    }


    public void ToCPUWorld()
    {
        GameManager.instance.HomeWorld = false;
        GameManager.instance.CPUWorld = true;
        GameManager.instance.RAMWorld = false;
        GameManager.instance.CPUFWorld = false;
        GameManager.instance.GPUWorld = false;
        GameManager.instance.StorageWorld = false;
        GameManager.instance.PSUWorld = false;
        GameManager.instance.MBWorld = false;
        GameManager.instance.CaseWorld = false;

        GameManager.instance.HomeWorldMap.SetActive(false);
        GameManager.instance.CPUWorldMap.SetActive(true);
        GameManager.instance.RAMWorldMap.SetActive(false);
        GameManager.instance.CPUFWorldMap.SetActive(false);
        GameManager.instance.GPUWorldMap.SetActive(false);
        GameManager.instance.STORAGEWorldMap.SetActive(false);
        GameManager.instance.PSUWorldMap.SetActive(false);
        GameManager.instance.MBWorldMap.SetActive(false);
        GameManager.instance.CASEWorldMap.SetActive(false);

        GameManager.instance.InHomeWorld = false;
        Vector3 destination = new Vector3(216.45f, -46.33f, 0);
        transform.position = destination;
        WorldName.text = "CPU World";
        LTA.OpenTeleAnim();
        GameManager.instance.LoadOtherWorldInventory();
        InvBTN.gameObject.SetActive(true);
        EquippedStats.gameObject.SetActive(true);
        GameManager.instance.MiniMapButton.gameObject.SetActive(true);
    }
    public void ToRAMWorld()
    {
        GameManager.instance.HomeWorld = false;
        GameManager.instance.CPUWorld = false;
        GameManager.instance.RAMWorld = true;
        GameManager.instance.CPUFWorld = false;
        GameManager.instance.GPUWorld = false;
        GameManager.instance.StorageWorld = false;
        GameManager.instance.PSUWorld = false;
        GameManager.instance.MBWorld = false;
        GameManager.instance.CaseWorld = false;

        GameManager.instance.HomeWorldMap.SetActive(false);
        GameManager.instance.CPUWorldMap.SetActive(false);
        GameManager.instance.RAMWorldMap.SetActive(true);
        GameManager.instance.CPUFWorldMap.SetActive(false);
        GameManager.instance.GPUWorldMap.SetActive(false);
        GameManager.instance.STORAGEWorldMap.SetActive(false);
        GameManager.instance.PSUWorldMap.SetActive(false);
        GameManager.instance.MBWorldMap.SetActive(false);
        GameManager.instance.CASEWorldMap.SetActive(false);


        GameManager.instance.InHomeWorld = false;
        Vector3 destination = new Vector3(391.47f, -53.27f, 0);
        transform.position = destination;
        WorldName.text = "RAM World";
        LTA.OpenTeleAnim();
        GameManager.instance.LoadOtherWorldInventory();
        InvBTN.gameObject.SetActive(true);
        EquippedStats.gameObject.SetActive(true);
        GameManager.instance.MiniMapButton.gameObject.SetActive(true);
    }
    public void ToCPUFWorld()
    {
        GameManager.instance.HomeWorld = false;
        GameManager.instance.CPUWorld = false;
        GameManager.instance.RAMWorld = false;
        GameManager.instance.CPUFWorld = true;
        GameManager.instance.GPUWorld = false;
        GameManager.instance.StorageWorld = false;
        GameManager.instance.PSUWorld = false;
        GameManager.instance.MBWorld = false;
        GameManager.instance.CaseWorld = false;

        GameManager.instance.HomeWorldMap.SetActive(false);
        GameManager.instance.CPUWorldMap.SetActive(false);
        GameManager.instance.RAMWorldMap.SetActive(false);
        GameManager.instance.CPUFWorldMap.SetActive(true);
        GameManager.instance.GPUWorldMap.SetActive(false);
        GameManager.instance.STORAGEWorldMap.SetActive(false);
        GameManager.instance.PSUWorldMap.SetActive(false);
        GameManager.instance.MBWorldMap.SetActive(false);
        GameManager.instance.CASEWorldMap.SetActive(false);

        GameManager.instance.InHomeWorld = false;
        Vector3 destination = new Vector3(607.93f, -53.63f, 0);
        transform.position = destination;
        WorldName.text = "CPU Fan World";
        LTA.OpenTeleAnim();
        GameManager.instance.LoadOtherWorldInventory();
        InvBTN.gameObject.SetActive(true);
        EquippedStats.gameObject.SetActive(true);
        GameManager.instance.MiniMapButton.gameObject.SetActive(true);
    }
    public void ToGPUWorld()
    {
        GameManager.instance.HomeWorld = false;
        GameManager.instance.CPUWorld = false;
        GameManager.instance.RAMWorld = false;
        GameManager.instance.CPUFWorld = false;
        GameManager.instance.GPUWorld = true;
        GameManager.instance.StorageWorld = false;
        GameManager.instance.PSUWorld = false;
        GameManager.instance.MBWorld = false;
        GameManager.instance.CaseWorld = false;


        GameManager.instance.HomeWorldMap.SetActive(false);
        GameManager.instance.CPUWorldMap.SetActive(false);
        GameManager.instance.RAMWorldMap.SetActive(false);
        GameManager.instance.CPUFWorldMap.SetActive(false);
        GameManager.instance.GPUWorldMap.SetActive(true);
        GameManager.instance.STORAGEWorldMap.SetActive(false);
        GameManager.instance.PSUWorldMap.SetActive(false);
        GameManager.instance.MBWorldMap.SetActive(false);
        GameManager.instance.CASEWorldMap.SetActive(false);

        GameManager.instance.InHomeWorld = false;
        Vector3 destination = new Vector3(818.99f, 7.89f, 0);
        transform.position = destination;
        WorldName.text = "GPU World";
        LTA.OpenTeleAnim();
        GameManager.instance.LoadOtherWorldInventory();
        InvBTN.gameObject.SetActive(true);
        EquippedStats.gameObject.SetActive(true);
        GameManager.instance.MiniMapButton.gameObject.SetActive(true);
    }
    public void ToStorageWorld()
    {
        GameManager.instance.HomeWorld = false;
        GameManager.instance.CPUWorld = false;
        GameManager.instance.RAMWorld = false;
        GameManager.instance.CPUFWorld = false;
        GameManager.instance.GPUWorld = false;
        GameManager.instance.StorageWorld = true;
        GameManager.instance.PSUWorld = false;
        GameManager.instance.MBWorld = false;
        GameManager.instance.CaseWorld = false;

        GameManager.instance.HomeWorldMap.SetActive(false);
        GameManager.instance.CPUWorldMap.SetActive(false);
        GameManager.instance.RAMWorldMap.SetActive(false);
        GameManager.instance.CPUFWorldMap.SetActive(false);
        GameManager.instance.GPUWorldMap.SetActive(false);
        GameManager.instance.STORAGEWorldMap.SetActive(true);
        GameManager.instance.PSUWorldMap.SetActive(false);
        GameManager.instance.MBWorldMap.SetActive(false);
        GameManager.instance.CASEWorldMap.SetActive(false);

        GameManager.instance.InHomeWorld = false;
        Vector3 destination = new Vector3(-4.47f, -171.63f, 0);
        transform.position = destination;
        WorldName.text = "Storage World";
        LTA.OpenTeleAnim();
        GameManager.instance.LoadOtherWorldInventory();
        InvBTN.gameObject.SetActive(true);
        EquippedStats.gameObject.SetActive(true);
        GameManager.instance.MiniMapButton.gameObject.SetActive(true);
    }
    public void ToPSUWorld()
    {
        GameManager.instance.HomeWorld = false;
        GameManager.instance.CPUWorld = false;
        GameManager.instance.RAMWorld = false;
        GameManager.instance.CPUFWorld = false;
        GameManager.instance.GPUWorld = false;
        GameManager.instance.StorageWorld = false;
        GameManager.instance.PSUWorld = true;
        GameManager.instance.MBWorld = false;
        GameManager.instance.CaseWorld = false;

        GameManager.instance.HomeWorldMap.SetActive(false);
        GameManager.instance.CPUWorldMap.SetActive(false);
        GameManager.instance.RAMWorldMap.SetActive(false);
        GameManager.instance.CPUFWorldMap.SetActive(false);
        GameManager.instance.GPUWorldMap.SetActive(false);
        GameManager.instance.STORAGEWorldMap.SetActive(false);
        GameManager.instance.PSUWorldMap.SetActive(true);
        GameManager.instance.MBWorldMap.SetActive(false);
        GameManager.instance.CASEWorldMap.SetActive(false);

        GameManager.instance.InHomeWorld = false;
        Vector3 destination = new Vector3(222.51f, -249.61f, 0);
        transform.position = destination;
        WorldName.text = "PSU World";
        LTA.OpenTeleAnim();
        GameManager.instance.LoadOtherWorldInventory();
        InvBTN.gameObject.SetActive(true);
        EquippedStats.gameObject.SetActive(true);
        GameManager.instance.MiniMapButton.gameObject.SetActive(true);
    }

    public void ToMBWorld()
    {
        GameManager.instance.HomeWorld = false;
        GameManager.instance.CPUWorld = false;
        GameManager.instance.RAMWorld = false;
        GameManager.instance.CPUFWorld = false;
        GameManager.instance.GPUWorld = false;
        GameManager.instance.StorageWorld = false;
        GameManager.instance.PSUWorld = false;
        GameManager.instance.MBWorld = true;
        GameManager.instance.CaseWorld = false;

        GameManager.instance.HomeWorldMap.SetActive(false);
        GameManager.instance.CPUWorldMap.SetActive(false);
        GameManager.instance.RAMWorldMap.SetActive(false);
        GameManager.instance.CPUFWorldMap.SetActive(false);
        GameManager.instance.GPUWorldMap.SetActive(false);
        GameManager.instance.STORAGEWorldMap.SetActive(false);
        GameManager.instance.PSUWorldMap.SetActive(false);
        GameManager.instance.MBWorldMap.SetActive(true);
        GameManager.instance.CASEWorldMap.SetActive(false);

        GameManager.instance.InHomeWorld = false;
        Vector3 destination = new Vector3(591.51f, -214.54f, 0);
        transform.position = destination;
        WorldName.text = "Motherboard World";
        LTA.OpenTeleAnim();
        GameManager.instance.LoadOtherWorldInventory();
        InvBTN.gameObject.SetActive(true);
        EquippedStats.gameObject.SetActive(true);
        GameManager.instance.MiniMapButton.gameObject.SetActive(true);
    }
    public void ToCaseWorld()
    {
        GameManager.instance.HomeWorld = false;
        GameManager.instance.CPUWorld = false;
        GameManager.instance.RAMWorld = false;
        GameManager.instance.CPUFWorld = false;
        GameManager.instance.GPUWorld = false;
        GameManager.instance.StorageWorld = false;
        GameManager.instance.PSUWorld = false;
        GameManager.instance.MBWorld = false;
        GameManager.instance.CaseWorld = true;

        GameManager.instance.HomeWorldMap.SetActive(false);
        GameManager.instance.CPUWorldMap.SetActive(false);
        GameManager.instance.RAMWorldMap.SetActive(false);
        GameManager.instance.CPUFWorldMap.SetActive(false);
        GameManager.instance.GPUWorldMap.SetActive(false);
        GameManager.instance.STORAGEWorldMap.SetActive(false);
        GameManager.instance.PSUWorldMap.SetActive(false);
        GameManager.instance.MBWorldMap.SetActive(false);
        GameManager.instance.CASEWorldMap.SetActive(true);

        GameManager.instance.InHomeWorld = false;
        Vector3 destination = new Vector3(901.29f, -241.55f, 0);
        transform.position = destination;
        WorldName.text = "Case World";
        LTA.OpenTeleAnim();
        GameManager.instance.LoadOtherWorldInventory();
        InvBTN.gameObject.SetActive(true);
        EquippedStats.gameObject.SetActive(true);
        GameManager.instance.MiniMapButton.gameObject.SetActive(true);
    }

    //default player xny: 17.48, -26.46
    public void BackToTheHomeWorld()
    {
        GameManager.instance.HomeWorld = true;
        GameManager.instance.CPUWorld = false;
        GameManager.instance.RAMWorld = false;
        GameManager.instance.CPUFWorld = false;
        GameManager.instance.GPUWorld = false;
        GameManager.instance.StorageWorld = false;
        GameManager.instance.PSUWorld = false;
        GameManager.instance.MBWorld = false;
        GameManager.instance.CaseWorld = false;

        GameManager.instance.HomeWorldMap.SetActive(true);
        GameManager.instance.CPUWorldMap.SetActive(false);
        GameManager.instance.RAMWorldMap.SetActive(false);
        GameManager.instance.CPUFWorldMap.SetActive(false);
        GameManager.instance.GPUWorldMap.SetActive(false);
        GameManager.instance.STORAGEWorldMap.SetActive(false);
        GameManager.instance.PSUWorldMap.SetActive(false);
        GameManager.instance.MBWorldMap.SetActive(false);
        GameManager.instance.CASEWorldMap.SetActive(true);

        DeskPanel.SetActive(true);
        Desktop.SetActive(true);
        GameMap.SetActive(true);
        inExplorePanel.SetActive(false);
        WorldName.text = "Home World";
        BackToHomeWorld = false;
        GameManager.instance.LoadCharacter();
        InvBTN.gameObject.SetActive(false);
        EquippedStats.gameObject.SetActive(false);
        GameManager.instance.InHomeWorld = true;
        GameManager.instance.UnequipEquipment();
        GameManager.instance.MiniMapButton.gameObject.SetActive(true);
        LTA.OpenGameMap();
    }
    
    public void TheTeleporter()
    {

        GameManager.instance.Enter.gameObject.SetActive(false);
        if (homeTeleport != null)
        {
            transform.position = homeTeleport.GetComponent<Teleporter>().HomeDestination().position;

            if(BackToHomeWorld == true)
            {
                BackToTheHomeWorld();
            }
           
            LTA.OpenTeleAnim();
        }
        else if (OpenDesk == true && homeTeleport == null)
        {
            DeskPanel.gameObject.SetActive(true);
            GameManager.instance.PlayerDeskName();
            UIPanel.gameObject.SetActive(true);
            GameManager.instance.UIExplore.SetActive(false);
            OpenDesk = false;
        }
        else if (GameManager.instance.OpenEditor == true && homeTeleport == null && EditorOpen == false)
        {

            GameManager.instance.scene.LoadScene();
            //SceneManager.LoadScene(1, LoadSceneMode.Additive);
            EditorOpen = true;
            GameManager.instance.UIExplore.SetActive(false);
            //GameManager.instance.OpenEditor = false;
        }
        else if (OpenBuild == true && homeTeleport == null)
        {
            BuildRoom.gameObject.SetActive(true);
            OpenBuild = false;
            GameManager.instance.UIExplore.SetActive(false);

        }
        
    }

        //IMPORTANT: for other teleports, you just need to place game objects, create another tag and then just copy the code here
        private void OnTriggerEnter2D(Collider2D collision)
        {
            
            //for home
            
            if (collision.CompareTag("Home"))
            {
            GameManager.instance.Enter.gameObject.SetActive(true);
                homeTeleport = collision.gameObject;
                

                if (collision.gameObject.name == "HomeDoor")
                {
                    Name.text = "Leave Home";
                   
                }
                else if (collision.gameObject.name == "HomeDoorEnter")
                {
                    Name.text = "Enter Home";
               
                }


            }

            if (collision.CompareTag("Room"))
            {
            GameManager.instance.Enter.gameObject.SetActive(true);
                    homeTeleport = collision.gameObject;
                

                   if(collision.gameObject.name == "RoomDoorEnter")
                   {
                    Name.text = "Enter Room";
                   }

                   else if (collision.gameObject.name == "RoomDoorExit")
                   {
                    Name.text = "Exit Room";
                   }
            }

        //otherworlds
        if (collision.CompareTag("Worlds"))
        {
            homeTeleport = collision.gameObject;
            BackToHomeWorld = true;
            GameManager.instance.Enter.gameObject.SetActive(true);

            Name.text = "Back To Home";
        }

        //otherworldhouses
        if (collision.CompareTag("CPUExchanger"))
        {
            homeTeleport = collision.gameObject;
             GameManager.instance.Enter.gameObject.SetActive(true);

            if (collision.gameObject.name == "EnterDoor")
            {
                Name.text = "Enter Exchanger House";
                
            }

            else if (collision.gameObject.name == "ExitDoor")
            {
                Name.text = "Exit House";
            }


        }

        if (collision.CompareTag("RAMExchanger"))
        {
            homeTeleport = collision.gameObject;
            GameManager.instance.Enter.gameObject.SetActive(true);

            if (collision.gameObject.name == "EnterDoor")
            {
                Name.text = "Enter Exchanger House";
            }

            else if (collision.gameObject.name == "ExitDoor")
            {
                Name.text = "Exit House";
            }


        }

        if (collision.CompareTag("CPUFExchanger"))
        {
            homeTeleport = collision.gameObject;
            GameManager.instance.Enter.gameObject.SetActive(true);

            if (collision.gameObject.name == "EnterDoor")
            {
                Name.text = "Enter Exchanger House";
            }

            else if (collision.gameObject.name == "ExitDoor")
            {
                Name.text = "Exit House";
            }

        }
        if (collision.CompareTag("GPUExchanger"))
        {
            homeTeleport = collision.gameObject;
            GameManager.instance.Enter.gameObject.SetActive(true);

            if (collision.gameObject.name == "EnterDoor")
            {
                Name.text = "Enter Exchanger House";
            }

            else if (collision.gameObject.name == "ExitDoor")
            {
                Name.text = "Exit House";
            }

        }
        if (collision.CompareTag("StorageExchanger"))
        {
            homeTeleport = collision.gameObject;
            GameManager.instance.Enter.gameObject.SetActive(true);

            if (collision.gameObject.name == "EnterDoor")
            {
                Name.text = "Enter Exchanger House";
            }

            else if (collision.gameObject.name == "ExitDoor")
            {
                Name.text = "Exit House";
            }

        }
        if (collision.CompareTag("PSUExchanger"))
        {
            homeTeleport = collision.gameObject;
            GameManager.instance.Enter.gameObject.SetActive(true);

            if (collision.gameObject.name == "EnterDoor")
            {
                Name.text = "Enter Exchanger House";
            }

            else if (collision.gameObject.name == "ExitDoor")
            {
                Name.text = "Exit House";
            }

        }
        if (collision.CompareTag("MBExchanger"))
        {
            homeTeleport = collision.gameObject;
            GameManager.instance.Enter.gameObject.SetActive(true);

            if (collision.gameObject.name == "EnterDoor")
            {
                Name.text = "Enter Exchanger House";
            }

            else if (collision.gameObject.name == "ExitDoor")
            {
                Name.text = "Exit House";
            }

        }
        if (collision.CompareTag("CaseExchanger"))
        {
            homeTeleport = collision.gameObject;
            GameManager.instance.Enter.gameObject.SetActive(true);

            if (collision.gameObject.name == "EnterDoor")
            {
                Name.text = "Enter Exchanger House";
            }

            else if (collision.gameObject.name == "ExitDoor")
            {
                Name.text = "Exit House";
            }

        }

        ///////////////////////////////////
        if (collision.CompareTag("Desk"))
            {
            GameManager.instance.Enter.gameObject.SetActive(true);
            OpenDesk = true;
            Name.text = "Enter To Your Setup";
           
        }
            if (collision.CompareTag("Editor"))
            {
            GameManager.instance.Enter.gameObject.SetActive(true);
            GameManager.instance.OpenEditor = true;
                Name.text = "Open Player Editor";
            }
            if (collision.CompareTag("Build"))
            {
            GameManager.instance.Enter.gameObject.SetActive(true);
            GameManager.instance.OpenBuild = true;
            OpenBuild = true;
               Name.text = "Open Building Desk";
            }



    }
        private void OnTriggerExit2D(Collider2D collision)
        {
        if(GameManager.instance.Enter != null)
        {
            GameManager.instance.Enter.gameObject.SetActive(false);
        }
        
        //for home
        if (collision.CompareTag("Home"))
            {
                if (collision.gameObject == homeTeleport)
                {

                    homeTeleport = null;
                }
            }

            if (collision.CompareTag("Room"))
            {
                if (collision.gameObject == homeTeleport)
                {

                    homeTeleport = null;
                }
            }

        //otherworlds
        if (collision.CompareTag("Worlds"))
        {
            if (collision.gameObject == homeTeleport)
            {

                homeTeleport = null;
            }
            BackToHomeWorld = false;
        }

        //otherworlds houses
        if (collision.CompareTag("CPUExchanger"))
        {
            if (collision.gameObject == homeTeleport)
            {

                homeTeleport = null;
            }
            
        }
        if (collision.CompareTag("RAMExchanger"))
        {
            if (collision.gameObject == homeTeleport)
            {

                homeTeleport = null;
            }

        }
        if (collision.CompareTag("CPUFExchanger"))
        {
            if (collision.gameObject == homeTeleport)
            {

                homeTeleport = null;
            }

        }
        if (collision.CompareTag("GPUExchanger"))
        {
            if (collision.gameObject == homeTeleport)
            {

                homeTeleport = null;
            }

        }
        if (collision.CompareTag("StorageExchanger"))
        {
            if (collision.gameObject == homeTeleport)
            {

                homeTeleport = null;
            }

        }
        if (collision.CompareTag("PSUExchanger"))
        {
            if (collision.gameObject == homeTeleport)
            {

                homeTeleport = null;
            }

        }
        if (collision.CompareTag("MBExchanger"))
        {
            if (collision.gameObject == homeTeleport)
            {

                homeTeleport = null;
            }

        }
        if (collision.CompareTag("CaseExchanger"))
        {
            if (collision.gameObject == homeTeleport)
            {

                homeTeleport = null;
            }

        }

        ///////////////////////////////////
        if (collision.CompareTag("Desk"))
            {
                OpenDesk = false;

            

        }
            if (collision.CompareTag("Editor"))
            {
               GameManager.instance.OpenEditor = false;
               EditorOpen = false;
        }
        if (collision.CompareTag("Build"))
        {
            OpenBuild = false;
        }



    }
    }

