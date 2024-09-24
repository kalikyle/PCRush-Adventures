using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCredit : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Front;
    public GameObject creditScene;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EndCredits()
    {
        Front.SetActive(true);
        creditScene.SetActive(false);
    }
}
