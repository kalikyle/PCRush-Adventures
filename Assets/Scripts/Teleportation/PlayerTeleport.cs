using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleport : MonoBehaviour
{
    private GameObject homeTeleport;
    public GameObject Deskcanvas;

    private bool OpenDesk = false;


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
                Deskcanvas.gameObject.SetActive(true);
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
