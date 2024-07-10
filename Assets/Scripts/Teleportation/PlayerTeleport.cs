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
        public Button Enter;
        public TMP_Text Name;
        public TMP_Text WorldName;

        public LeanTweenAnimate LTA;

        public bool OpenDesk = false;
        public bool OpenBuild = false;
        public bool EditorOpen = false;
        public bool BackToHomeWorld = false;


    public void Start()
    {
        Enter.onClick.AddListener(() =>
        {
            TheTeleporter();
        });
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
        GameManager.instance.InHomeWorld = false;
        Vector3 destination = new Vector3(216.45f, -46.33f, 0);
        transform.position = destination;
        WorldName.text = "CPU World";
        LTA.OpenTeleAnim();
        GameManager.instance.LoadOtherWorldInventory();
        InvBTN.gameObject.SetActive(true);
        EquippedStats.gameObject.SetActive(true);
    }
    public void TheTeleporter()
    {

        Enter.gameObject.SetActive(false);
        if (homeTeleport != null)
        {
            transform.position = homeTeleport.GetComponent<Teleporter>().HomeDestination().position;

            if(BackToHomeWorld == true)
            {
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
            Debug.Log("Press E to Enter");
            if (collision.CompareTag("Home"))
            {
                Enter.gameObject.SetActive(true);
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
                    Enter.gameObject.SetActive(true);
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
            Enter.gameObject.SetActive(true);

            Name.text = "Back To Home";
        }

        //otherworldhouses

        if (collision.CompareTag("CPUExchanger"))
        {
            homeTeleport = collision.gameObject;
            
        }

        ///////////////////////////////////
        if (collision.CompareTag("Desk"))
            {
            Enter.gameObject.SetActive(true);
            OpenDesk = true;
                Name.text = "Enter Your Desk";
           
        }
            if (collision.CompareTag("Editor"))
            {
            Enter.gameObject.SetActive(true);
            GameManager.instance.OpenEditor = true;
                Name.text = "Open Player Editor";
            }
            if (collision.CompareTag("Build"))
            {
            Enter.gameObject.SetActive(true);
            OpenBuild = true;
               Name.text = "Enter Building Room";
            }



    }
        private void OnTriggerExit2D(Collider2D collision)
        {
        if(Enter != null)
        {
            Enter.gameObject.SetActive(false);
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

