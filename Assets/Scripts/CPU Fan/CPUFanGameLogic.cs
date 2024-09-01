using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CPUFanGameLogic : MonoBehaviour
{
    public static CPUFanGameLogic instance;
    public CPUFan cpuFan;
    public SpriteRenderer cpufanImage;// Reference to the CPU fan
    public SpriteRenderer cpufanImage2;// Reference to the CPU fan
    public Image cpuImage;// Reference to the CPU fan
    //public TextMeshProUGUI textNotice;
    //public GameObject panel;

    private Vector3 startPosition;

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

   

    private void OnDestroy()
    {
        // Unsubscribe from scene unloaded event to avoid memory leaks
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }

    private void OnSceneUnloaded(Scene scene)
    {
        if (scene.name == "CPUFanMiniGame")
        {
            // Destroy the GameManager2 instance when PCRush scene is unloaded
            Destroy(gameObject);
        }
    }

    void Start()
    {
        startPosition = cpuFan.transform.position;

        if (GameManager2.Instance.UsedImagesNeeds["CPU Fan"].item.ItemImage != null)
        {
            cpufanImage.sprite = GameManager2.Instance.UsedImagesNeeds["CPU Fan"].item.ItemImage;
            cpufanImage2.sprite = GameManager2.Instance.UsedImagesNeeds["CPU Fan"].item.ItemImage;
        }
        
        if (GameManager2.Instance.UsedImagesNeeds["CPU"].item.ItemImage != null)
        {
            cpuImage.sprite = GameManager2.Instance.UsedImagesNeeds["CPU"].item.ItemImage;
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
        if (cpuFan.IsConnected())
        {
            EndGame();
        }
    }

    public void ResetCPUFan()
    {
        cpuFan.SetConnected(false);
        cpuFan.transform.position = startPosition;
    }

    void EndGame()
    {
        GameManager2.Instance.MainCamera.gameObject.SetActive(true);
        GameManager2.Instance.BuildScene.gameObject.SetActive(true);
        SceneManager.UnloadSceneAsync("CPUFanMiniGame");
        
    }

    public void CancelButton()
    {
        GameManager2.Instance.MainCamera.gameObject.SetActive(true);
        GameManager2.Instance.BuildScene.gameObject.SetActive(true);
        SceneManager.UnloadSceneAsync("CPUFanMiniGame");
        GameManager2.Instance.BackSingleItem("CPU Fan");
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
