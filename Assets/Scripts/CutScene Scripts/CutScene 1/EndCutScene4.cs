using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCutScene4 : MonoBehaviour
{
    
    void Start()
    {
        GameManager.instance.CutScene4.SetActive(false);

        
        DialogueManager.GetInstance().EnterDialogueMode(GameManager.instance.MainStory);
        DialogueManager.GetInstance().TriggerSection("NineSecond");
        GameManager.instance.UIExplore.SetActive(true);
        GameManager.instance.pumpkinSoup.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
