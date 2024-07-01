using Inventory.Model;
using PartsInventory.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartsCollect : MonoBehaviour

{
    public Sprite packageimage;

    [field: SerializeField]

    public PartsSO parts { get; set; }

    [field: SerializeField]

    public int Quantity { get; set; }

    [SerializeField]

    private float duration = 0.3f;

    [SerializeField]
    private float changeInterval = 3f;

    private PartsSO currentParts;
    private int currentQuantity;

    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = packageimage;   //parts.ItemImage;
        //StartCoroutine(DropFromPartsList());
        //FirstDrop();
    }

    internal void DestroyItem()
    {
        GetComponent<Collider2D>().enabled = false;
        StartCoroutine(AnimatePickUp());
    }

    private IEnumerator ChangePartsAndQuantity() // orig
    {
        while (true)
        {
            currentParts = GetRandomPartsSO();

            // Choose a random quantity (for example, between 1 and 5)
            currentQuantity = UnityEngine.Random.Range(1, 6); // Random range from 1 to 5

            // Assign the new PartsSO and Quantity
            parts = currentParts;
            Quantity = currentQuantity;

            // Set the sprite of the object based on the new PartsSO
            GetComponent<SpriteRenderer>().sprite = packageimage;//currentParts.ItemImage;

            // Wait for the specified interval before changing again
            yield return new WaitForSeconds(changeInterval);
        }
    }

        private PartsSO GetRandomPartsSO() // orig
    {
        if ( GameManager.instance.partsList != null && GameManager.instance.partsList.Count > 0)
        {
            // Choose a random PartsSO from the partsManager's partsList
            int randomIndex = UnityEngine.Random.Range(0, GameManager.instance.partsList.Count);
            return GameManager.instance.partsList[randomIndex];
        }
        else
        {
            Debug.LogWarning("PartsManager or partsList is not assigned or empty.");
            return null;
        }
    }

    private IEnumerator AnimatePickUp()
    {
        Vector3 startScale = transform.localScale;
        Vector3 endScale = Vector3.zero;
        float currentTime = 0;
        while(currentTime < duration)
        {
            currentTime += Time.deltaTime;
            transform.localScale = Vector3.Lerp(startScale, endScale, currentTime /duration);
            yield return null;
        }
        
        Destroy(gameObject);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
