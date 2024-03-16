using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DeskController : MonoBehaviour
{
    public GameObject DeskPanel;
    public GameObject UIPanel;


    public Button BackButton;
    
    void Start()
    {

        BackButton.onClick.AddListener(OnBackButtonClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnBackButtonClick()
    {
       
        DeskPanel.gameObject.SetActive(false);
        UIPanel.gameObject.SetActive(false);
    }
}
