using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class Quit : MonoBehaviour
{

    [SerializeField]
    public UnityEngine.UI.Image DialogBox;
    [SerializeField]
    public TMP_Text DialogText;

    

    public void Start()
    {
        
    }
    public void ShowDialog()
    {
        DialogBox.gameObject.SetActive(true);
        DialogText.text = "Are You Sure you want to Quit? \n All Items on your Build Room will be back to your Inventory";
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

   
}
