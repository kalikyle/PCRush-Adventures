using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCutScene8 : MonoBehaviour
{
   
    void Start()
    {
        StartCoroutine(HandleDialogueAndQuestCompletion());

        GameManager.instance.CutScene8.SetActive(false);
        GameManager.instance.LTA.OpenCPUTwirl();
        
    }
    private IEnumerator HandleDialogueAndQuestCompletion()
    {
        // Start the dialogue section "ThirteenTwo"
        DialogueManager.GetInstance().EnterDialogueMode(GameManager.instance.MainStory);
        DialogueManager.GetInstance().TriggerSection("NineteenTwo");

        // Wait until the dialogue is no longer playing
        yield return new WaitUntil(() => !DialogueManager.GetInstance().dialogueIsPlaying);

       
        this.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
