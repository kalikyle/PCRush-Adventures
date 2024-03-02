using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerTeleport : MonoBehaviour
{
    private GameObject homeTeleport;
    public GameObject DeskPanel;

    public bool OpenDesk = false;
    public bool OpenEditor = false;


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
                OpenDesk = false;
            }
            if (OpenEditor == true && homeTeleport == null)
            {
                SceneManager.LoadScene(1, LoadSceneMode.Additive);
                OpenEditor = false;
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
            OpenEditor = true;
        }



    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //for home
        if (collision.CompareTag("Home"))
        {
            if(collision.gameObject == homeTeleport) {

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



    }
}
