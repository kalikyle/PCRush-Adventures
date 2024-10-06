using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SataWire : MonoBehaviour
{
    public Transform designatedPort; // Transform of the designated port
    public GameObject firstSprite; // GameObject for the initial state of the cable
    public GameObject connectedSprite; // GameObject for connected state of the cable
    public GameObject portWireImage; // GameObject for the port wire image

    private Vector3 startPoint;
    private Vector3 startPosition;
    private Vector3 difference = Vector3.zero;

    private bool isDragging = false;
    private bool isConnected = false;
    private float snapDistance = 0.75f; // Distance threshold for snapping

    void Start()
    {
        startPoint = transform.parent.position;
        startPosition = transform.position;

        if (firstSprite != null && connectedSprite != null)
        {
            firstSprite.SetActive(true); // Ensure the initial sprite is active at the start
            connectedSprite.SetActive(false); // Ensure the connected sprite is inactive at the start
        }
        else
        {
            Debug.LogError("firstSprite or connectedSprite is not assigned in the Inspector!");
        }

        if (portWireImage != null)
        {
            portWireImage.SetActive(false); // Ensure the port wire image is inactive at the start
        }
        else
        {
            Debug.LogError("portWireImage is not assigned in the Inspector!");
        }
    }

    private void OnMouseDown()
    {
        if (!isConnected)
        {
            difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            difference.z = 0; // Ensure the z-component is zero to avoid unintended behavior
            isDragging = true;
        }
    }

    private void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - difference;
            newPosition.z = 0; // Keep the z-component zero to avoid unintended behavior

            UpdateWire(newPosition);
        }
    }

    private void OnMouseUp()
    {
        isDragging = false;
        if (!isConnected)
        {
            CheckClosestPort();
        }
        else
        {
            // Reset to start position if not connected
            UpdateWire(startPosition);
        }
    }

    void UpdateWire(Vector3 newPosition)
    {
        transform.position = newPosition;
    }

    void CheckClosestPort()
    {
        float distance = Vector3.Distance(transform.position, designatedPort.position);
        Debug.Log($"Distance to port: {distance}");

        if (distance < snapDistance)
        {
            Debug.Log("Snap detected! Connecting to port.");
            SetPosition(designatedPort.position);
            isConnected = true;
            ChangeWireSprite(); // Change wire sprite to connected state
            if (portWireImage != null)
            {
                portWireImage.SetActive(true); // Activate the port wire image
            }
        }
        else
        {
            Debug.Log("No valid port found for snapping. Resetting position.");
            ResetPosition();
        }
    }

    void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    void ResetPosition()
    {
        SetPosition(startPosition);
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

        if (portWireImage != null)
        {
            portWireImage.SetActive(false); // Deactivate the port wire image
        }
    }

    void ChangeWireSprite()
    {
        if (firstSprite != null && connectedSprite != null)
        {
            firstSprite.SetActive(false); // Deactivate the initial sprite
            connectedSprite.SetActive(true); // Activate the connected sprite
            Debug.Log("Cable connected and sprite changed to connected sprite.");
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
            ChangeWireSprite(); // Ensure the sprite is changed when setting to connected
            if (portWireImage != null)
            {
                portWireImage.SetActive(true); // Activate the port wire image
            }
        }
        else
        {
            ResetPosition();
        }
    }
}
