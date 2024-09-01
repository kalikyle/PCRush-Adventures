using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wire : MonoBehaviour
{
    public LineRenderer Line;
    public Transform Port;

    public GameObject firstSprite; // GameObject for the initial state of the wire
    public Sprite ConnectedSprite; // Sprite for connected state of the wire
    public SpriteRenderer WireRenderer; // Reference to the SpriteRenderer component of the wire

    //private Vector3 resetPos;

    bool isDragging = false;
    Vector3 originalPosition;
    bool isConnected = false;

    void Start()
    {
        originalPosition = transform.position;
        if (WireRenderer != null && firstSprite != null)
        {
            firstSprite.SetActive(true); // Ensure the initial sprite is active at the start
            WireRenderer.sprite = null; // Ensure the wire renderer is empty at the start
        }
        UpdateLineRendererPositions();
    }

    void Update()
    {
        if (isDragging)
        {
            Vector3 mousePosition = Input.mousePosition;
            Vector3 convertedMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            convertedMousePosition.z = 0;
            SetPosition(convertedMousePosition);

            Vector3 portDifference = convertedMousePosition - Port.position;
            if (portDifference.sqrMagnitude < .75f)
            {
                SetPosition(Port.position);
                isDragging = false;
                isConnected = true;
                ChangeWireSprite(); // Change wire sprite to connected state
                Line.enabled = false;
            }
        }

        if (isConnected && WireRenderer.sprite != ConnectedSprite)
        {
            ChangeWireSprite(); // Ensure the wire sprite is changed to connected state
        }
    }

    void SetPosition(Vector3 pPosition)
    {
        transform.position = pPosition;
        UpdateLineRendererPositions();
    }

    void UpdateLineRendererPositions()
    {
        if (Line != null)
        {
            Vector3 positionDifference = transform.position - Line.transform.position;
            Line.SetPosition(0, positionDifference - new Vector3(.5f, 0, 0));
            Line.SetPosition(1, positionDifference - new Vector3(.15f, 0, 0));
        }
    }

    public void ResetPosition()
    {
        SetPosition(originalPosition);
        isConnected = false;
        if (WireRenderer != null && firstSprite != null)
        {
            firstSprite.SetActive(true); // Reactivate the initial sprite
            WireRenderer.sprite = null; // Reset the wire renderer sprite
        }
        //this.transform.localPosition = new Vector3(resetPos.x, resetPos.y, resetPos.z);
    }

    private void OnMouseDown()
    {
        isDragging = true;
    }

    private void OnMouseUp()
    {
        isDragging = false;
        if (isConnected)
        {
            ResetPosition();
        }        
    }

    void ChangeWireSprite()
    {
        if (WireRenderer != null && ConnectedSprite != null)
        {
            firstSprite.SetActive(false); // Deactivate the initial sprite
            WireRenderer.sprite = ConnectedSprite; // Change wire sprite to connected sprite
            Debug.Log("Wire connected and sprite changed to connected sprite.");
        }
        else
        {
            Debug.LogWarning("WireRenderer or ConnectedSprite is not assigned!");
        }
    }

    public bool isItConnected()
    {
        return isConnected;
    }

    public void setConnected(bool connected)
    {
        isConnected = connected;
        if (!isConnected)
        {
            ChangeWireSprite(); // Ensure the sprite is changed when setting to connected
        }
        else
        {
            ResetPosition();
        }
    }
}
