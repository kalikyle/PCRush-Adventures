using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCutScene10 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.CutScene10.SetActive(false);

        if(GameManager.instance.OnSleepQuest == true)
        {
            GameManager.instance.OnSleepFinish = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
