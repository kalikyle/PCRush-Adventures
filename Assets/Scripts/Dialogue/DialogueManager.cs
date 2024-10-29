using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
using Shop.UI;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;
using UnityEditor.Rendering;


public class DialogueManager : MonoBehaviour
{
    private static DialogueManager instance;


    [Header("Dialogue UI")]
    [SerializeField] public GameObject dialoguePanel;
    [SerializeField] private GameObject continueIcon;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] public TextMeshProUGUI displayNameText;
    public TMP_Text NPCName;
    [SerializeField] private Image portraitImage;

    public Button talktoBTN;

    [Header("Load Global Ink File")]
    [SerializeField] private TextAsset LoadInkGlobal;


    public Sprite PlayerSprite;
    [SerializeField] private List<Sprite> sprites = new List<Sprite>();
    //private List<Sprite> runtimeSprites = new List<Sprite>();




    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;


    private const string SPEAKER_TAG = "speaker";
    private const string PORTRAIT_TAG = "portrait";
    private const string CHOICE_TAG = "choice";
    private const string RANGE_TAG = "range";
    private const string SCENE_TAG = "CutScene";

    private DialogueVariables dialogueVariables;


    private Story currentStory;
    public bool dialogueIsPlaying { get; private set; }
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one Dialogue Manager in the scene");

        }
        instance = this;

        dialogueVariables = new DialogueVariables(LoadInkGlobal);
        
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

        //talktoBTN.gameObject.SetActive(false);

       
    }
    
    private void Update()
    {
        
        // return right away if dialogue isn't playing
        if (!dialogueIsPlaying)
        {
            return;
        }
        
        

        if (canContinuetoNextLine && Input.GetKeyDown(KeyCode.Space)){
            ContinueStory();
        }
       
    }

    public void EnterDialogueMode(TextAsset inkJSON)
    {
       
            currentStory = new Story(inkJSON.text);
            dialogueIsPlaying = true;
            dialoguePanel.SetActive(true);

            dialogueVariables.StartListening(currentStory);
            SetPlayerNameVariable(GameManager.instance.PlayerName);

        // Handle external function calls from the Ink script
        //currentStory.BindExternalFunction("open_shop_panel", () => OpenShopPanel());
        ContinueStory();
        

    }
    private void SetPlayerNameVariable(string playerName)
    {
        // Set the Ink variable "player_Name" to the playerName
        currentStory.variablesState["player_Name"] = playerName;
    }
    private bool canContinuetoNextLine = false;
    private Coroutine displayLineCoRoutine;
    private bool typing;
    //private string currentText; // Store current text being typed
    public void ContinueStory()
    {
        
        if (currentStory.canContinue)
        {

            if (displayLineCoRoutine != null) {
                StopCoroutine(displayLineCoRoutine);
            
            }
            displayLineCoRoutine = StartCoroutine(TypeText(currentStory.Continue())); //dialogueText.text = currentStory.Continue();

            DisplayChoices();
            HandleTags(currentStory.currentTags);
        }
        else
        {
            ExitDialogueMode();
        }


      

    }

    private IEnumerator TypeText(string text)
    {
        //SoundManager.instance.PlayDialogueSound();
        canContinuetoNextLine = false;

        string plainText = Regex.Replace(text, "<.*?>", "");
        dialogueText.text = "";

        int visibleCount = 0;
        while (visibleCount <= plainText.Length)
        {

            if (Input.GetMouseButtonDown(0))
            {
                // Show the full text immediately and end typing
                dialogueText.text = text;
                canContinuetoNextLine = true; // Allow continuation
                yield break; // Exit the coroutine
            }
            // Use the plain text count to reveal characters in the rich-text version
            dialogueText.text = text.Substring(0, FindRichTextEndIndex(text, visibleCount));
            visibleCount++;


            yield return new WaitForSeconds(0.05f); // Adjust typing speed here
        }

        canContinuetoNextLine = true;
        
    }

    private int FindRichTextEndIndex(string richText, int plainTextLength)
    {
        int count = 0;
        int index = 0;
        bool insideTag = false;

        while (index < richText.Length && count < plainTextLength)
        {
            if (richText[index] == '<') insideTag = true;
            if (!insideTag) count++; // Only count visible characters
            if (richText[index] == '>') insideTag = false;

            index++;
        }

        return index;
    }

    int from = 0;
    int to = 2;


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

            //if(tagKey == RANGE_TAG)
            //{
            //    
            //}

            // handle the tag
            switch (tagKey)
            {
                case RANGE_TAG:
                    string[] values = tagValue.Split(',');

                        from = int.Parse(values[0]);
                        to = int.Parse(values[1]);

                    break;

                case SPEAKER_TAG:
                    displayNameText.text = tagValue;
                    


                    break;
                case PORTRAIT_TAG:
                    int index = Convert.ToInt32(tagValue);
                    

                    if(index == 0)
                    {
                        portraitImage.sprite = PlayerSprite;
                    }
                    else
                    {
                        portraitImage.sprite = sprites[index];
                    }
                    break;

                case CHOICE_TAG:

                    if (tagValue.Equals("swordyes"))
                    {
                       
                            GameManager.instance.SwordDealerPanel.SetActive(true);
                            GameManager.instance.OpenSwordShop(from, to);
                        
                        
                    }else if (tagValue.Equals("Armoryes"))
                    {
                        GameManager.instance.ArmorDealerPanel.SetActive(true);
                        GameManager.instance.OpenArmorShop(from, to);

                    }

                    else if (tagValue.Equals("helmetyes"))
                    {
                        GameManager.instance.HelmetDealerPanel.SetActive(true);
                        GameManager.instance.OpenHelmetShop(from, to);

                    }
                    else if (tagValue.Equals("shieldyes"))
                    {
                        GameManager.instance.ShieldDealerPanel.SetActive(true);
                        GameManager.instance.OpenShieldShop(from, to);

                    }


                    //FOR EXCHANGERS
                    else if (tagValue.Equals("exchangeyes"))
                    {
                        GameManager.instance.CPUExhangerPanel.SetActive(true);
                        GameManager.instance.OpenCPUWorldExhanger();
                    }
                    else if (tagValue.Equals("ramexchangeyes"))
                    {
                        GameManager.instance.RAMExhangerPanel.SetActive(true);
                        GameManager.instance.OpenRAMWorldExhanger();
                    }
                    else if (tagValue.Equals("cpufexchangeyes"))
                    {
                        GameManager.instance.CPUFExhangerPanel.SetActive(true);
                        GameManager.instance.OpenCPUFWorldExhanger();
                    }
                    else if (tagValue.Equals("gpuexchangeyes"))
                    {
                        GameManager.instance.GPUExhangerPanel.SetActive(true);
                        GameManager.instance.OpenGPUWorldExhanger();
                    }
                    else if (tagValue.Equals("storageexchangeyes"))
                    {
                        GameManager.instance.StorageExhangerPanel.SetActive(true);
                        GameManager.instance.OpenStorageWorldExhanger();
                    }
                    else if (tagValue.Equals("psuexchangeyes"))
                    {
                        GameManager.instance.PSUExhangerPanel.SetActive(true);
                        GameManager.instance.OpenPSUWorldExhanger();
                    }
                    else if (tagValue.Equals("mbexchangeyes"))
                    {
                        GameManager.instance.MBExhangerPanel.SetActive(true);
                        GameManager.instance.OpenMBWorldExhanger();
                    }
                    else if (tagValue.Equals("caseexchangeyes"))
                    {
                        GameManager.instance.CaseExhangerPanel.SetActive(true);
                        GameManager.instance.OpenCaseWorldExhanger();
                    }

                    //others
                    else if (tagValue.Equals("fixarmor"))
                    {
                        GameManager.instance.ArmorFixer.SetActive(true);
                    }
                    else if (tagValue.Equals("sellyes"))
                    {
                        GameManager.instance.PartsOpenSell();
                    } 
                    else if (tagValue.Equals("sellPCsyes"))
                    {
                        GameManager.instance.OpenBuyers();
                    }

                    break;
                case SCENE_TAG:
                    CutScene(tagValue); // Handle the scene change

                    break;

                default:
                    Debug.LogWarning("Tag came in but is not currently being handled: " + tag);
                    break;
            }
        }
    }
    private void CutScene(string sceneName)
    {
        Debug.Log("Changing to scene: " + sceneName);
        //SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);

        if(sceneName == "CutScene 1")
        {
            GameManager.instance.CutScene1.SetActive(true);
        }

        if(sceneName == "CutScene 2")
        {
            GameManager.instance.CutScene2.SetActive(true);
        }

        if (sceneName == "CutScene 5")
        {
            GameManager.instance.CutScene5.SetActive(true);
        }

        if (sceneName == "CutScene 6")
        {
            GameManager.instance.CutScene6.SetActive(true);
        }

        if(sceneName == "CutScene 8")
        {
            GameManager.instance.LTA.OpenTeleAnim();
            GameManager.instance.CutScene8.SetActive(true);
        }

        if(sceneName == "CutScene 9")
        {
            GameManager.instance.CutScene9.SetActive(true);
        }
    }
    public void ExitDialogueMode()
    {

        dialogueVariables.StopListening(currentStory);
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
        }else if (currentChoices.Count == 0)
        {
            continueIcon.SetActive(true);
        }
        else
        {
            continueIcon.SetActive(false);
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
        if (canContinuetoNextLine)
        {
            currentStory.ChooseChoiceIndex(choiceindex);
            ContinueStory();
        }
    }

    public Ink.Runtime.Object GetVariableState(string variableName)
    {
        Ink.Runtime.Object variableValue = null;
        dialogueVariables.variables.TryGetValue(variableName, out variableValue);
        if (variableValue == null)
        {
            Debug.LogWarning("Ink Variable was found to be null: " + variableName);
        }
        return variableValue;
    }
    public void OnApplicationQuit()
    {
        if(dialogueVariables != null)
        {
            dialogueVariables.SaveVariables();
        }
    }

    public void TriggerSection(string section)
    {
        // Start the Ink story from the beginning



        if (section == "Thank" && GameManager.instance.OnBuySwordQuest == true)
        {
            GameManager.instance.OnBuyDone = true;
        }
        else if (GameManager.instance.OnHeadBackQuest == true && section == "ThankCPU")
        {
            return;
        }
        else if (GameManager.instance.ExchangeToCaseQuest == true && section == "ThankCase")
        {
            return;
        }
        else
        {
            currentStory.ChoosePathString(section);
            ContinueStory();
        }
        // Continue the story until reaching the desired knot

    }
  
}
