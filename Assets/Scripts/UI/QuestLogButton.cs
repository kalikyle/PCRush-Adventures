using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class QuestLogButton : MonoBehaviour, ISelectHandler
{
    public Button button;
    public TMP_Text buttonText;
    private UnityAction onSelectAction;


    public void Initialize(string displayname,UnityAction selectAction)
    {
        this.button = this.GetComponent<Button>();
       
        this.buttonText.text = displayname;
        this.onSelectAction = selectAction;
    }


    public void OnSelect(BaseEventData eventData)
    {
        try { onSelectAction(); 
        
        } catch
        { }
       
    }


    public void SetState(QuestState state)
    {
        switch (state)
        {
            case QuestState.REQUIREMENT_NOT_MET:
            case QuestState.CAN_START:
                buttonText.color = Color.red;
                break;


            case QuestState.IN_PROGRESS: case QuestState.CAN_FINISH:
                buttonText.color = Color.yellow;
                break;


            case QuestState.FINISHED:
                buttonText.color = Color.green;
                break;
            default:
                buttonText.color = Color.black;
                Debug.Log("Quest statenot Reqcognize");
                break;
        }
    }

}
