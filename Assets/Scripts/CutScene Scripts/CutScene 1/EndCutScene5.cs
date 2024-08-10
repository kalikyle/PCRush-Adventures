using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCutScene5 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.CutScene5.SetActive(false);

        DialogueManager.GetInstance().EnterDialogueMode(GameManager.instance.MainStory);
        DialogueManager.GetInstance().TriggerSection("NiceThird");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
