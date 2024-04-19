using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSystem : MonoBehaviour
{
    [SerializeField]
    private PartsInventorySO partsData;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        PartsCollect parts = collision.GetComponent<PartsCollect>();
        if(parts != null)
        {
            int reminder = partsData.AddItem(parts.parts, parts.Quantity);
            if(reminder == 0)
            {
                parts.DestroyItem();
            }
            else
            {
                parts.Quantity = reminder;
            }
        }
    }




    void Start()
    {
        Debug.Log("wow");  
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
