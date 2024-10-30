using HeneGames.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;
    


    [Header("Ink JSON")]
    /*[SerializeField]*/ public TextAsset inkJSON;

    private bool playerInRange;


    private void Awake()
    {
        playerInRange = false;
        visualCue.SetActive(false);
    }

   

    public void OnEnable()
    {
       
            DialogueManager.GetInstance().talktoBTN.onClick.AddListener(EnterDialogue);
        
       
    }

    private void Update()
    {
        if (playerInRange && !DialogueManager.GetInstance().dialogueIsPlaying)
        {
            visualCue.SetActive(true);
            //DialogueManager.GetInstance().talktoBTN.gameObject.SetActive(true);
            GameManager.instance.CurrentNPC = transform.parent.parent.name;
            GameManager.instance.LTA.OpenTalkBTN();
            DialogueManager.GetInstance().NPCName.text = transform.parent.parent.name;
            

            if (Input.GetKeyDown(KeyCode.Space))
            {
                EnterDialogue();
            }
        }
        else
        {
            visualCue.SetActive(false);
            // DialogueManager.GetInstance().talktoBTN.gameObject.SetActive(false);
            GameManager.instance.LTA.CloseTalkBTN();

        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            playerInRange = true;
           
        }
    }

    public void EnterDialogue()
    {
        if (GameManager.instance.OnQuest == false)
        {
            DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            playerInRange = false;
            
            DialogueManager.GetInstance().ExitDialogueMode();
        }
    }
}
