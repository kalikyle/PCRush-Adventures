using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public GameObject LoadingCanvas;

    public Slider loadingslider;
    // Start is called before the first frame update
    void Start()
    {
        

    }

    public void LoadScene()
    {
        LoadingCanvas.SetActive(true);

        StartCoroutine(LoadMainMenu());
    }

    IEnumerator LoadMainMenu() {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync("PCRush CharacterEditor", LoadSceneMode.Additive);

        while (!loadOperation.isDone)
        {
            float progress = Mathf.Clamp01(loadOperation.progress / 0.9f);
            loadingslider.value = progress;
            yield return null;

        }

        if (loadOperation.isDone)
        {
            LoadingCanvas.SetActive(false);
            loadingslider.value = 0;
        }





    }
    

    

    // Update is called once per frame
    void Update()
    {
        
    }
}
