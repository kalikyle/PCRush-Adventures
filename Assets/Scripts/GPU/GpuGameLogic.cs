using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GPUGameLogic : MonoBehaviour
{
    public static GPUGameLogic instance;
    public GPUMoving gpu;
    public SpriteRenderer gpuImage;
    public Image gpuImage2;// Reference to the GPU
    //public TextMeshProUGUI textNotice;
    //public GameObject panel;

    void Awake()
    {
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
    private void OnSceneUnloaded(Scene scene)
    {
        if (scene.name == "GPUMiniGame")
        {
            // Destroy the GameManager2 instance when PCRush scene is unloaded
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        // Unsubscribe from scene unloaded event to avoid memory leaks
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }
    void Start()
    {
        

        if (GameManager2.Instance.UsedImagesNeeds["Video Card"].item.ItemImage != null)
        {
            gpuImage.sprite = GameManager2.Instance.UsedImagesNeeds["Video Card"].item.ItemImage;
            gpuImage2.sprite = GameManager2.Instance.UsedImagesNeeds["Video Card"].item.ItemImage;
        }

        //if (panel != null)
        //{
        //    panel.SetActive(false);
        //}

        //if (textNotice != null)
        //{
        //    textNotice.text = "";
        //}
    }

    void Update()
    {
        if (gpu.IsConnected())
        {
            EndGame();
        }
    }

    public void ResetGPU()
    {
        gpu.SetConnected(false);
    }

    void EndGame()
    {
        //ShowEndNotice("GPU connected.");
        GameManager2.Instance.MainCamera.gameObject.SetActive(true);
        GameManager2.Instance.BuildScene.gameObject.SetActive(true);
        SceneManager.UnloadSceneAsync("GPUMiniGame");
        
    }

    public void CancelButton()
    {
        //ShowEndNotice("GPU connected.");
        GameManager2.Instance.MainCamera.gameObject.SetActive(true);
        GameManager2.Instance.BuildScene.gameObject.SetActive(true);
        SceneManager.UnloadSceneAsync("GPUMiniGame");
        GameManager2.Instance.BackSingleItem("Video Card");

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
