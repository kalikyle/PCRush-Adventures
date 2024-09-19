using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RamGameLogic : MonoBehaviour
{
    public static RamGameLogic instance;
    public List<RamMoving> RamSticks;
    public SpriteRenderer ramImage;// List of RAM sticks
    public SpriteRenderer ramImage2;// List of RAM sticks
    //public TextMeshProUGUI textNotice;
    //public GameObject panel;

    void ShuffleRamSticks()
    {
        List<Vector3> ramPositions = new List<Vector3>();
        foreach (RamMoving ram in RamSticks)
        {
            ramPositions.Add(ram.transform.position);
        }
        foreach (RamMoving ram in RamSticks)
        {
            int randomIndex = Random.Range(0, ramPositions.Count);
            ram.transform.position = ramPositions[randomIndex];
            ramPositions.RemoveAt(randomIndex);
        }
    }

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
        if (scene.name == "RAMMiniGame")
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
        ShuffleRamSticks();

        if (GameManager2.Instance != null)
        {
            ramImage.sprite = GameManager2.Instance.UsedImagesNeeds["RAM"].item.ItemImage;
            ramImage2.sprite = GameManager2.Instance.UsedImagesNeeds["RAM"].item.ItemImage;
        }
        else if (GameManager.instance != null)
        {
            ramImage.sprite = GameManager.instance.UsedImagesNeeds["RAM"].item.ItemImage;
            ramImage2.sprite = GameManager.instance.UsedImagesNeeds["RAM"].item.ItemImage;
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
        int connectedRamSticks = 0;
        foreach (RamMoving ram in RamSticks)
        {
            if (ram.IsConnected())
            {
                connectedRamSticks++;
            }
        }

        if (connectedRamSticks == RamSticks.Count)
        {
            EndGame();
        }
    }

    public void ResetRamSticks()
    {
        foreach (RamMoving ram in RamSticks)
        {
            ram.SetConnected(false);
        }
        ShuffleRamSticks();
    }

    void EndGame()
    {
        // ShowEndNotice("All RAM sticks connected.");
        if (GameManager2.Instance != null)
        {

            GameManager2.Instance.MainCamera.gameObject.SetActive(true);
            GameManager2.Instance.BuildScene.gameObject.SetActive(true);

        }

        else if (GameManager.instance != null)
        {

            GameManager.instance.MainCamera.gameObject.SetActive(true);

        }
        SceneManager.UnloadSceneAsync("RAMMiniGame");
        
    }

    public void CancelButton()
    {
        //ShowEndNotice("GPU connected.");
        if (GameManager2.Instance != null)
        {

            GameManager2.Instance.MainCamera.gameObject.SetActive(true);
            GameManager2.Instance.BuildScene.gameObject.SetActive(true);

            GameManager2.Instance.BackSingleItem("RAM");
        }

        else if (GameManager.instance != null)
        {
            GameManager.instance.MainCamera.gameObject.SetActive(true);
            GameManager.instance.BackSingleItem("RAM");
        }

        SceneManager.UnloadSceneAsync("RAMMiniGame");

    }

    //void ShowEndNotice(string text)
    //{
    //    //if (panel != null)
    //    //{
    //    //    panel.SetActive(true); // Show the panel
    //    //}

    //    //if (textNotice != null)
    //    //{
    //    //    textNotice.text = text;
    //    //}
    //}
}
