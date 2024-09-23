using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCutScene11 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.CutScene11.SetActive(false);
        GameManager.instance.CutScene12.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
