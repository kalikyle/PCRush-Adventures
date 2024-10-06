using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPUMoving : MonoBehaviour
{
    public Transform gpuSlot; // Transform of the GPU slot
    public GameObject firstSprite; // GameObject for the initial state of the GPU
    public GameObject connectedSprite; // GameObject for connected state of the GPU

    private Vector3 resetPos;
    private Transform currentSlot = null;

    bool isDragging = false;
    Vector3 originalPosition;
    bool isConnected = false;

    public float snapDistance = 5f; // Distance threshold for snapping

    void Start()
    {
        originalPosition = transform.position;
        resetPos = transform.localPosition;
        if (firstSprite != null && connectedSprite != null)
        {
            firstSprite.SetActive(true); // Ensure the initial sprite is active at the start
            connectedSprite.SetActive(false); // Ensure the connected sprite is inactive at the start
        }
        else
        {
            Debug.LogError("firstSprite or connectedSprite is not assigned in the Inspector!");
        }
    }

    void Update()
    {
        if (isDragging)
        {
            Vector3 mousePosition = Input.mousePosition;
            Vector3 convertedMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            convertedMousePosition.z = 0;
            SetPosition(convertedMousePosition);
        }
    }

    void SetPosition(Vector3 pPosition)
    {
        transform.position = pPosition;
    }

    public void ResetPosition()
    {
        SetPosition(originalPosition);
        isConnected = false;
        if (firstSprite != null && connectedSprite != null)
        {
            firstSprite.SetActive(true); // Reactivate the initial sprite
            connectedSprite.SetActive(false); // Deactivate the connected sprite
        }
        else
        {
            Debug.LogError("firstSprite or connectedSprite is not assigned in the Inspector!");
        }
        this.transform.localPosition = new Vector3(resetPos.x, resetPos.y, resetPos.z);
        currentSlot = null; // Reset the current slot
    }

    private void OnMouseDown()
    {
        if (!isConnected) // Only allow dragging if the GPU is not already connected
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
        float distance = Vector3.Distance(transform.position, gpuSlot.position);

        if (distance < snapDistance)
        {
            SetPosition(gpuSlot.position);
            currentSlot = gpuSlot;
            isConnected = true;
            ChangeGPUSprite(); // Change GPU sprite to connected state
        }
        else
        {
            ResetPosition();
        }
    }

    void ChangeGPUSprite()
    {
        if (firstSprite != null && connectedSprite != null)
        {
            firstSprite.SetActive(false); // Deactivate the initial sprite
            connectedSprite.SetActive(true); // Activate the connected sprite
            Debug.Log("GPU connected and sprite changed to connected sprite.");
        }
        else
        {
            Debug.LogWarning("firstSprite or connectedSprite is not assigned or has been destroyed!");
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
            ChangeGPUSprite(); // Ensure the sprite is changed when setting to connected
        }
        else
        {
            ResetPosition();
        }
    }
}
