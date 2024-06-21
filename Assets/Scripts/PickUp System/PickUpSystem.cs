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
        Coin coins = collision.GetComponent<Coin>();

        if(parts != null)
        {
            int reminder = partsData.AddItem(parts.parts, parts.Quantity);
            if(reminder == 0)
            {
                parts.DestroyItem();
                GameManager.instance.packagescollected++;
            }
            else
            {
                parts.Quantity = reminder;
            }

            if (GameManager.instance.ComputerInv.activeSelf)
            {

                GameManager.instance.ComputerInv.SetActive(false);

            }

            partsData.PartsSaveItems();
        }


        if(coins != null)
        {
            coins.DestroyItem();
            Debug.LogError(coins.coinValue);
        }
    }




    void Start()
    {
        //Debug.Log("wow");  
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
