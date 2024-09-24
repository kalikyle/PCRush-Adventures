using Inventory.Model;
using PC.Model;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class SettingsScript : MonoBehaviour
{
    [SerializeField]
    public UnityEngine.UI.Image DialogBox;
    [SerializeField]
    public TMP_Text DialogText;
    [SerializeField]
    public UnityEngine.UI.Button NoButton;
    [SerializeField]
    public UnityEngine.UI.Button YesButton;

   




    // Start is called before the first frame update
    void Start()
    {
        YesButton.onClick.AddListener(Yes);
        NoButton.onClick.AddListener(No);
    }
  
    public void No()
    {
        DialogBox.gameObject.SetActive(false);
    }
    public void Yes()
    {
      
       
        QuitGame();
        
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    public void DontQuitGame()
    {

        DialogBox.gameObject.SetActive(false);
    }

    public void ShowDialog()
    {
        DialogBox.gameObject.SetActive(true);
        DialogText.text = "Are You Sure you want to Quit?";
    }
    public void BacktoMainMenu()
    {
        InternetChecker.Instance.StopCheck();
        GameManager.instance.scene.LoadScene();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
