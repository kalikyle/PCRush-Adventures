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
        public GameObject BuildRoom;

        public Button Enter;
        public TMP_Text Name;

    public bool OpenDesk = false;
        public bool OpenBuild = false;
        public bool EditorOpen = false;



    public void Start()
    {
        Enter.onClick.AddListener(() =>
        {
            Enter.gameObject.SetActive(false);
            if (homeTeleport != null)
            {
                transform.position = homeTeleport.GetComponent<Teleporter>().HomeDestination().position;
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

                SceneManager.LoadScene(1, LoadSceneMode.Additive);
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
        });
    }

    void Update()
        {
            //for doors
            if (Input.GetKeyDown(KeyCode.E))
            {
            Enter.gameObject.SetActive(false);
            if (homeTeleport != null)
                {
                    transform.position = homeTeleport.GetComponent<Teleporter>().HomeDestination().position;
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
                    
                    SceneManager.LoadScene(1, LoadSceneMode.Additive);
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
            //for desk
            if (Input.GetKeyDown(KeyCode.Space))
            {
               
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
        Enter.gameObject.SetActive(false);
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

