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

    public void manualLoading()
    {
        LoadingCanvas.SetActive(true);
        StartCoroutine(ManualLoading());
    }

    IEnumerator ManualLoading()
    {
        

        float loadingTime = 2f;
        float elapsedTime = 0f;

        // Start loading the scene
        

        while (elapsedTime < loadingTime)
        {
            // Calculate progress based on elapsed time
            float progress = Mathf.Clamp01(elapsedTime / loadingTime);
            loadingslider.value = progress;

            // Increment elapsed time
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        // Ensure the loading slider reaches 100% at the end
        loadingslider.value = 1;

        // Deactivate loading canvas and reset slider
        LoadingCanvas.SetActive(false);
        loadingslider.value = 0;
        GameManager.instance.LTA.OpenTeleAnim();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
