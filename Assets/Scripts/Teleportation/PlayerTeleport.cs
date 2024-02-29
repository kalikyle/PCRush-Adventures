using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleport : MonoBehaviour
{
    private GameObject homeTeleport;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (homeTeleport != null)
            {
                transform.position = homeTeleport.GetComponent<Teleporter>().HomeDestination().position;
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
