using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCutScene9 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.CutScene9.SetActive(false);

        if (GameManager.instance.GoBackHomeQuest == true)
        {
            GameManager.instance.OnCutScene9Finish = true;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
