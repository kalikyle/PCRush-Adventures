using Assets.PixelHeroes.Scripts.ExampleScripts;
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
        Heart hearts = collision.GetComponent<Heart>();

        if (parts != null)
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
            //add coins
            Debug.LogError(coins.coinValue);
        }


        if (hearts != null)
        {
           
            hearts.DestroyItem();
            Debug.LogError(hearts.HeartValue);
            Health health;
            CharacterControls charcon;


            if (health = this.GetComponent<Health>())
            {
                //add the health
                health.currentHealth += hearts.HeartValue;
               
            }

            if(charcon = this.GetComponent<CharacterControls>())
            {
                charcon._animator.SetBool("Heal", true);
            }
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
