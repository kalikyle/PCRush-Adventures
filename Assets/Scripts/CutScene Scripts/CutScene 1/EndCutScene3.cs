using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCutScene3 : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject cutscene3;
    void Start()
    {
        cutscene3.gameObject.SetActive(false);

        DialogueManager.GetInstance().EnterDialogueMode(GameManager.instance.MainStory);
        DialogueManager.GetInstance().TriggerSection("Eight");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
