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
        public bool EditorOpen = false;
    

    


    void Update()
        {
            //for doors
            if (Input.GetKeyDown(KeyCode.E))
            {
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
               EditorOpen = false;
        }
        if (collision.CompareTag("Build"))
        {
            OpenBuild = false;
        }



    }
    }

