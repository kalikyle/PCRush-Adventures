using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



    public class PlayerTeleport : MonoBehaviour
    {
        private GameObject homeTeleport;
        public GameObject DeskPanel;
        public GameObject UIPanel;
        public GameObject BuildRoom;

        public bool OpenDesk = false;
        public bool OpenBuild = false;



    void Update()
        {
            //for doors
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (homeTeleport != null)
                {
                    transform.position = homeTeleport.GetComponent<Teleporter>().HomeDestination().position;
                }

            }
            //for desk
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (OpenDesk == true && homeTeleport == null)
                {
                    DeskPanel.gameObject.SetActive(true);
                    GameManager.instance.PlayerDeskName();
                    UIPanel.gameObject.SetActive(true);
                    OpenDesk = false;
                }
                else if (GameManager.instance.OpenEditor == true && homeTeleport == null)
                {
                    
                    SceneManager.LoadScene(1, LoadSceneMode.Additive);
                    //GameManager.instance.OpenEditor = false;
                }
            else if (OpenBuild == true  && homeTeleport == null)
            {
                BuildRoom.gameObject.SetActive(true);
                OpenBuild = false;
                
            }
        }
        }
        //IMPORTANT: for other teleports, you just need to place game objects, create another tag and then just copy the code here
        private void OnTriggerEnter2D(Collider2D collision)
        {
            //for home
            Debug.Log("Press E to Enter");
            if (collision.CompareTag("Home"))
            {
                homeTeleport = collision.gameObject;
            }

            if (collision.CompareTag("Room"))
            {
                homeTeleport = collision.gameObject;
            }

            ///////////////////////////////////
            if (collision.CompareTag("Desk"))
            {
                OpenDesk = true;
            }
            if (collision.CompareTag("Editor"))
            {
                GameManager.instance.OpenEditor = true;
            }
            if (collision.CompareTag("Build"))
        {
            OpenBuild = true;
        }



    }
        private void OnTriggerExit2D(Collider2D collision)
        {
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
            }
        if (collision.CompareTag("Build"))
        {
            OpenBuild = false;
        }



    }
    }

