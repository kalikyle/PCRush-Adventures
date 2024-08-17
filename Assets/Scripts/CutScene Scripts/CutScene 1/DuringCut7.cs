using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuringCut7 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.LTA.Onteleport();
        GameManager.instance.CPUSpawn.SetActive(true);
        DialogueManager.GetInstance().EnterDialogueMode(GameManager.instance.MainStory);
        DialogueManager.GetInstance().TriggerSection("NineteenThree");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
