using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCollideTrigger : MonoBehaviour
{
    public GameObject boxCollider;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            boxCollider.SetActive(true);

        }
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            boxCollider.SetActive(false);
        }
    }
}
