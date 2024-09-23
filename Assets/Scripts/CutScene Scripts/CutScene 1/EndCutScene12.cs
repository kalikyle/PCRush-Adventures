using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCutScene12 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.CutScene12.SetActive(false);

        if(GameManager.instance.OpenYourDesktopFinish == true && GameManager.instance.DesktopOpenedFinish == true)
        {
            GameManager.instance.OverallFinish = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
