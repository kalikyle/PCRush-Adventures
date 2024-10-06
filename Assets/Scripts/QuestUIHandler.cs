using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class QuestUIHandler : MonoBehaviour, IPointerClickHandler
{
    private bool isQuestPopupVisible = true;
    public GameObject currentQuestStepUI;

    public void OnPointerClick(PointerEventData eventData)
    {
        // Toggle the visibility of the popup when clicked
        if (isQuestPopupVisible)
        {
            // Hide the popup but leave part of it visible
            LeanTween.moveLocal(currentQuestStepUI, new Vector3(-460f, 60f, 0f), 1f)
                .setEase(LeanTweenType.easeOutExpo)
                .setOnComplete(() => isQuestPopupVisible = false);
        }
        else
        {
            // Show the popup fully
            LeanTween.moveLocal(currentQuestStepUI, new Vector3(-350f, 60f, 0f), 1f)
                .setEase(LeanTweenType.easeOutExpo)
                .setOnComplete(() => isQuestPopupVisible = true);
        }
    }
}
