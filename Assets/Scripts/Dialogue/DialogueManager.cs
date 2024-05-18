using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class DialogueManager : MonoBehaviour
{
    private static DialogueManager instance;


    [Header("Dialogue UI")]
    [SerializeField] public GameObject dialoguePanel;
    [SerializeField] private GameObject continueIcon;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI displayNameText;
    [SerializeField] private Image portraitImage;

    [SerializeField] public Button talktoBTN;


    public List<Sprite> sprites = new List<Sprite>();




    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;


    private const string SPEAKER_TAG = "speaker";
    private const string PORTRAIT_TAG = "portrait";



    private Story currentStory;
    public bool dialogueIsPlaying { get; private set; }
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one Dialogue Manager in the scene");

        }
        instance = this;

        
    }

    public static DialogueManager GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);


        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach (GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
    }

    private void Update()
    {
        // return right away if dialogue isn't playing
        if (!dialogueIsPlaying)
        {
            return;
        }
       

        if (Input.GetKeyDown(KeyCode.F)){
            ContinueStory();
        }
    }

    public void EnterDialogueMode(TextAsset inkJSON)
    {
        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);

        ContinueStory();

    }

    public void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            dialogueText.text = currentStory.Continue();

            DisplayChoices();

            HandleTags(currentStory.currentTags);
        }
        else
        {
            ExitDialogueMode();
        }
    }


    private void HandleTags(List<string> currentTags)
    {
        // loop through each tag and handle it accordingly
        foreach (string tag in currentTags)
        {
            // parse the tag
            string[] splitTag = tag.Split(':');
            if (splitTag.Length != 2)
            {
                Debug.LogError("Tag could not be appropriately parsed: " + tag);
            }
            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();

            // handle the tag
            switch (tagKey)
            {
                case SPEAKER_TAG:
                    displayNameText.text = tagValue;
                    
                    break;
                case PORTRAIT_TAG:
                    int index = Convert.ToInt32(tagValue);
                    portraitImage.sprite = sprites[index];
                    break;
                //case LAYOUT_TAG:
                //    layoutAnimator.Play(tagValue);
                //    break;
                //case AUDIO_TAG:
                //    SetCurrentAudioInfo(tagValue);
                //    break;
                default:
                    Debug.LogWarning("Tag came in but is not currently being handled: " + tag);
                    break;
            }
        }
    }
    public void ExitDialogueMode()
    {
      

       

        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";

       
    }

    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        // defensive check to make sure our UI can support the number of choices coming in
        if (currentChoices.Count > choices.Length)
        {
            Debug.LogError("More choices were given than the UI can support. Number of choices given: "
                + currentChoices.Count);
        }

        int index = 0;
        // enable and initialize the choices up to the amount of choices for this line of dialogue
        foreach (Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }
        // go through the remaining choices the UI supports and make sure they're hidden
        for (int i = index; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }

      // StartCoroutine(SelectFirstChoice());
    }


    //private IEnumerator SelectFirstChoice()
    //{
    //    EventSystem.current.SetSelectedGameObject(null);
    //    yield return new WaitForEndOfFrame();
    //    EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
    //}


    public void MakeChoice(int choiceindex)
    {
        currentStory.ChooseChoiceIndex(choiceindex);
        ContinueStory();
    }
}
