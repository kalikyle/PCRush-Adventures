using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PasswordToggle : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TMP_InputField passwordInputField; // Assign your Input Field in the Inspector
    public Button toggleButton; // Assign your Button in the Inspector

    private bool isPasswordVisible = false;
    void Start()
    {
        // Add a listener to the EventTrigger component of the Button
        EventTrigger eventTrigger = toggleButton.GetComponent<EventTrigger>();
        EventTrigger.Entry enterEntry = new EventTrigger.Entry();
        enterEntry.eventID = EventTriggerType.PointerEnter;
        enterEntry.callback.AddListener((data) => { OnPointerEnter((PointerEventData)data); });

        EventTrigger.Entry exitEntry = new EventTrigger.Entry();
        exitEntry.eventID = EventTriggerType.PointerExit;
        exitEntry.callback.AddListener((data) => { OnPointerExit((PointerEventData)data); });

        eventTrigger.triggers.Add(enterEntry);
        eventTrigger.triggers.Add(exitEntry);
    }

    public void TogglePasswordVisibility()
    {
        isPasswordVisible = true;
        passwordInputField.contentType = TMP_InputField.ContentType.Standard; // Show password
        passwordInputField.ForceLabelUpdate();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isPasswordVisible = true;
        passwordInputField.contentType = TMP_InputField.ContentType.Standard; // Show password
        passwordInputField.ForceLabelUpdate();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isPasswordVisible = false;
        passwordInputField.contentType = TMP_InputField.ContentType.Password; // Hide password
        passwordInputField.ForceLabelUpdate();
    }
}
