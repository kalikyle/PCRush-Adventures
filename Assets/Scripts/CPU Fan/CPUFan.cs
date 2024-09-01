using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPUFan : MonoBehaviour
{
    public Transform designatedSpot; // Transform of the designated spot for the CPU fan
    public SpriteRenderer fanRenderer; // Reference to the SpriteRenderer of the CPU fan
    public GameObject fanSprite; // GameObject for the CPU fan when placed correctly

    private Vector3 startPosition;
    private Vector3 difference = Vector3.zero;

    private bool isDragging = false;
    private bool isConnected = false;
    private float snapDistance = 0.75f; // Distance threshold for snapping

    void Start()
    {
        startPosition = transform.position;
        if (fanRenderer != null)
        {
            fanRenderer.enabled = true; // Ensure the initial sprite renderer is enabled at the start
        }
        if (fanSprite != null)
        {
            fanSprite.SetActive(false); // Ensure the connected sprite is inactive at the start
        }
        else
        {
            Debug.LogError("fanRenderer or fanSprite is not assigned in the Inspector!");
        }
    }

    private void OnMouseDown()
    {
        if (!isConnected)
        {
            difference = (Vector3)Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            isDragging = true;
        }
    }

    private void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - difference;
            newPosition.z = 0;
            transform.position = newPosition;
        }
    }

    private void OnMouseUp()
    {
        isDragging = false;
        if (!isConnected)
        {
            CheckClosestSpot();
        }
    }

    void CheckClosestSpot()
    {
        float distance = Vector3.Distance(transform.position, designatedSpot.position);

        if (distance < snapDistance)
        {
            SetPosition(designatedSpot.position);
            isConnected = true;
            ChangeFanSprite(); // Change fan sprite to connected state
            if (designatedSpot != null)
            {
                designatedSpot.gameObject.SetActive(false); // Deactivate the designated spot
            }
        }
        else
        {
            ResetPosition();
        }
    }

    void SetPosition(Vector3 pPosition)
    {
        transform.position = pPosition;
    }

    void ResetPosition()
    {
        SetPosition(startPosition);
        isConnected = false;
        if (fanRenderer != null)
        {
            fanRenderer.enabled = true; // Reactivate the initial sprite renderer
        }
        if (fanSprite != null)
        {
            fanSprite.SetActive(false); // Deactivate the connected sprite
        }
        if (designatedSpot != null)
        {
            designatedSpot.gameObject.SetActive(true); // Reactivate the designated spot
        }
        else
        {
            Debug.LogError("fanRenderer or fanSprite is not assigned in the Inspector!");
        }
    }

    void ChangeFanSprite()
    {
        if (fanRenderer != null)
        {
            fanRenderer.enabled = false; // Deactivate the initial sprite renderer
        }
        if (fanSprite != null)
        {
            fanSprite.SetActive(true); // Activate the connected sprite
            Debug.Log("CPU fan connected and sprite changed.");
        }
        else
        {
            Debug.LogWarning("fanRenderer or fanSprite is not assigned or has been destroyed!");
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
            ChangeFanSprite(); // Ensure the sprite is changed when setting to connected
            if (designatedSpot != null)
            {
                designatedSpot.gameObject.SetActive(false); // Deactivate the designated spot
            }
        }
        else
        {
            ResetPosition();
        }
    }
}
