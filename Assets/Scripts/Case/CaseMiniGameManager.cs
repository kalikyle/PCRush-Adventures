using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class CaseMiniGameManager2 : MonoBehaviour
{
    public static CaseMiniGameManager2 instance;
    private int screwCount;

    //public TextMeshProUGUI textNotice;
    //public GameObject panel; // Reference to the panel GameObject

    void Awake()
    {
        // Ensure that only one instance of the CaseMiniGameManager2 exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            SceneManager.sceneUnloaded += OnSceneUnloaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void OnDestroy()
    {
        // Unsubscribe from scene unloaded event to avoid memory leaks
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }

    private void OnSceneUnloaded(Scene scene)
    {
        if (scene.name == "CaseMiniGame")
        {
            // Destroy the GameManager2 instance when PCRush scene is unloaded
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Count all the screws in the scene
        screwCount = FindObjectsOfType<RemoveScrew>().Length;
        // Ensure the panel is hidden at the start
        //if (panel != null)
        //{
        //    panel.SetActive(false);
        //}

        //if (textNotice != null)
        //{
        //    textNotice.text = "";
        //}
    }

    public void RemoveScrew()
    {
        screwCount--;

        if (screwCount <= 0)
        {
            EndGame();
        }
    }

    public void EndGame()
    {
        if (GameManager2.Instance != null)
        {

            GameManager2.Instance.MainCamera.gameObject.SetActive(true);
            GameManager2.Instance.BuildScene.gameObject.SetActive(true);
            
        }

       else if (GameManager.instance != null) {

            GameManager.instance.MainCamera.gameObject.SetActive(true);
           
        }

        SceneManager.UnloadSceneAsync("CaseMiniGame");
        //ShowEndNotice("Removing Screws Complete.");
    }
    public void cancelButton()
    {

        if (GameManager2.Instance != null)
        {

            GameManager2.Instance.MainCamera.gameObject.SetActive(true);
            GameManager2.Instance.BuildScene.gameObject.SetActive(true);

            GameManager2.Instance.BackSingleItem("Case");
        }

        else if(GameManager.instance != null)
        {
            GameManager.instance.MainCamera.gameObject.SetActive(true);
            GameManager.instance.BackSingleItem("Case");
        }

        SceneManager.UnloadSceneAsync("CaseMiniGame");
    }


    //void ShowEndNotice(string text)
    //{
    //    if (panel != null)
    //    {
    //        panel.SetActive(true); // Show the panel
    //    }

    //    if (textNotice != null)
    //    {
    //        textNotice.text = text;
    //    }
    //}
}
