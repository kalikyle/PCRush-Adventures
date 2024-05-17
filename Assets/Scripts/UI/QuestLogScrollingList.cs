using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class QuestLogScrollingList : MonoBehaviour
{

    [Header("Components")]
    [SerializeField] private GameObject contentParent;



    [Header("Rect Transform")]
    [SerializeField] private RectTransform scrollRectTransform;
    [SerializeField] private RectTransform contentRectTransform;



    [Header("Quest Log Button")]
    [SerializeField] private GameObject questLogButtonPrefab;

    private Dictionary<string, QuestLogButton> idToButtonMap = new Dictionary<string, QuestLogButton>();



    //private void Start()
    //{
    //    for (int i = 0; i< 3; i++)
    //    {
    //        QuestInfoSO questInfoTest = ScriptableObject.CreateInstance<QuestInfoSO>();
    //        questInfoTest.id = "test_ " + i;
    //        questInfoTest.displayName = "Test " + i;
    //        questInfoTest.questStepsPrefab = new GameObject[0];
    //        Quest quest = new Quest(questInfoTest);


    //        QuestLogButton questLogButton = CreateButtonIfNotExists(quest, () =>
    //        {
    //            Debug.Log("SELECTED: " + questInfoTest.displayName);
    //        });

    //        if(i == 0)
    //        {
    //            questLogButton.button.Select();
    //        }
    //    }
    //}


    public QuestLogButton CreateButtonIfNotExists(Quest quest, UnityAction selectAction)
    {
        QuestLogButton questLogButton = null;


        if (!idToButtonMap.ContainsKey(quest.info.id))
        {
            questLogButton = InstantiateQuestLogButton(quest, selectAction);
        }
        else
        {
            questLogButton = idToButtonMap[quest.info.id];
        }
        return questLogButton;
    }

  private QuestLogButton InstantiateQuestLogButton(Quest quest, UnityAction selectAction)
    {
        QuestLogButton questLogButton = Instantiate(questLogButtonPrefab, contentParent.transform).GetComponent<QuestLogButton>();

        questLogButton.gameObject.name = quest.info.id + "_button";


        RectTransform buttonRectTransform = questLogButton.GetComponent<RectTransform>();

        questLogButton.Initialize(quest.info.displayName, () => { 
               selectAction();
            UpdateScrolling(buttonRectTransform);
        
        });

        idToButtonMap[quest.info.id] = questLogButton;
        return questLogButton;
    }

    private void UpdateScrolling(RectTransform buttonRectTransform)
    {
        float buttonYMin = Mathf.Abs(buttonRectTransform.anchoredPosition.y);
        float buttonYMax = buttonYMin + buttonRectTransform.rect.height;

        float contentYMin = contentRectTransform.anchoredPosition.y;
        float contentYMax = contentYMin + scrollRectTransform.rect.height;
        


        if(buttonYMax > contentYMax)
        {
            contentRectTransform.anchoredPosition = new Vector2(contentRectTransform.anchoredPosition.x, buttonYMax - scrollRectTransform.rect.height);
        }else if (buttonYMax < contentYMin)
        {

            contentRectTransform.anchoredPosition = new Vector2(contentRectTransform.anchoredPosition.x, buttonYMin);
        }
    }
}
