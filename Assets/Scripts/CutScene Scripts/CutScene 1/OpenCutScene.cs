using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCutScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.UIExplore.SetActive(false);
        GameManager.instance.SquareBars.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
