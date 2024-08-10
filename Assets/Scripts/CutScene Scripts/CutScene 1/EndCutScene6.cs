using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCutScene6 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.CutScene6.SetActive(false);

        GameManager.instance.DoneDownStairsQuets = true;
        GameManager.instance.pumpkinSoup.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
