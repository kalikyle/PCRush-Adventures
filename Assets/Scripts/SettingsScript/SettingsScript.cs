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

    public Button Facebook;
    public Button Youtube;
    public Button Itch;
    public Button Survey;

    [SerializeField]
    private string FacebookUrl = "";

    [SerializeField]
    private string YoutubeUrl = "";

    [SerializeField]
    private string ItchUrl = ""; 
    
    [SerializeField]
    private string SurveyUrl = "";





    // Start is called before the first frame update
    void Start()
    {
        YesButton.onClick.AddListener(Yes);
        NoButton.onClick.AddListener(No);

        Facebook.onClick.AddListener(() => OpenBrowser(FacebookUrl));
        Youtube.onClick.AddListener(() => OpenBrowser(YoutubeUrl));
        Itch.onClick.AddListener(() => OpenBrowser(ItchUrl));
        Survey.onClick.AddListener(() => OpenBrowser(SurveyUrl));
    }

    private void OpenBrowser(string URL)
    {
        // Open the URL
        Application.OpenURL(URL);
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
