using Coffee.UIExtensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RamMoving : MonoBehaviour
{
    public GameObject firstSprite; // GameObject for the initial state of the RAM stick
    public GameObject ConnectedSprite; // GameObject for connected state of the RAM stick

    Vector3 resetPos;
    Vector3 originalPosition;
    Vector3 difference = Vector3.zero;
    private Transform currentSlot = null;

    bool isDragging = false;   
    bool isConnected = false;    

    private float snapDistance = 0.5f; // Distance threshold for snapping

    void Start()
    {
        resetPos = transform.parent.position;
        originalPosition = transform.position;        
        if (firstSprite != null && ConnectedSprite != null)
        {
            firstSprite.SetActive(true); // Ensure the initial sprite is active at the start
            ConnectedSprite.SetActive(false); // Ensure the connected sprite is inactive at the start
        }
        else
        {
            Debug.LogError("firstSprite or ConnectedSprite is not assigned in the Inspector!");
        }
    }

    void Update()
    {
        if (isDragging)
        {
            //Vector3 mousePosition = Input.mousePosition;
            
        }
    }

    void SetPosition(Vector3 pPosition)
    {
        transform.position = pPosition;
    }

    private void OnMouseDrag()
    {
        Vector3 convertedMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - difference;
        convertedMousePosition.z = 0;
        SetPosition(convertedMousePosition);
    }

    public void ResetPosition()
    {
        SetPosition(originalPosition);
        isConnected = false;
        if (firstSprite != null && ConnectedSprite != null)
        {
            firstSprite.SetActive(true); // Reactivate the initial sprite
            ConnectedSprite.SetActive(false); // Deactivate the connected sprite
        }
        else
        {
            Debug.LogError("firstSprite or ConnectedSprite is not assigned in the Inspector!");
        }
        //this.transform.localPosition = new Vector3(resetPos.x, resetPos.y, resetPos.z);
        currentSlot = null; // Reset the current slot
    }

    private void OnMouseDown()
    {
        difference = (Vector3)Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        if (!isConnected) // Only allow dragging if the RAM stick is not already connected
        {
            isDragging = true;
        }
    }

    private void OnMouseUp()
    {
        isDragging = false;
        if (!isConnected)
        {
            CheckClosestSlot();
        }
    }

    void CheckClosestSlot()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, snapDistance);
        Transform closestSlot = null;
        float closestDistance = float.MaxValue;

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("RamSlot")) // Ensure that the slot has a tag "RamSlot"
            {
                float distance = Vector3.Distance(transform.position, collider.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestSlot = collider.transform;
                }
            }
        }

        if (closestSlot != null)
        {
            SetPosition(closestSlot.position);
            currentSlot = closestSlot;
            isConnected = true;
            ChangeRamSprite(); // Change RAM sprite to connected state
        }
        else
        {
            ResetPosition();
        }
    }

    void ChangeRamSprite()
    {
        if (firstSprite != null && ConnectedSprite != null)
        {
            firstSprite.SetActive(false); // Deactivate the initial sprite
            ConnectedSprite.SetActive(true); // Activate the connected sprite
            Debug.Log("RAM connected and sprite changed to connected sprite.");
        }
        else
        {
            Debug.LogWarning("firstSprite or ConnectedSprite is not assigned or has been destroyed!");
        }
    }

    public bool IsConnected()
    {
        return isConnected;
    }

    public void SetConnected(bool connected)
    {
        isConnected = connected;
        if (isConnected)
        {
            ChangeRamSprite(); // Ensure the sprite is changed when setting to connected
        }
        else
        {
            ResetPosition();
        }
    }
}
