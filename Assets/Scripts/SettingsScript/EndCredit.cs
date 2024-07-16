using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCredit : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Mainmenu;
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
        Mainmenu.SetActive(true);
        creditScene.SetActive(false);
    }
}
