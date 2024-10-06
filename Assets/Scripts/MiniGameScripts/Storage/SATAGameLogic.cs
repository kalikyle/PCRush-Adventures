using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SATAGameLogic : MonoBehaviour
{
    public static SATAGameLogic instance;
    public List<SataWire> SataWires; // List of SATA wires
    //public TextMeshProUGUI textNotice;
    //public GameObject panel;

    void ShuffleSataWires()
    {
        List<Vector3> wirePositions = new List<Vector3>();
        foreach (SataWire wire in SataWires)
        {
            wirePositions.Add(wire.transform.position);
        }
        foreach (SataWire wire in SataWires)
        {
            int randomIndex = Random.Range(0, wirePositions.Count);
            wire.transform.position = wirePositions[randomIndex];
            wirePositions.RemoveAt(randomIndex);
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
        if (scene.name == "StorageMiniGame")
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
        ShuffleSataWires();

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
        int connectedSataWires = 0;
        foreach (SataWire wire in SataWires)
        {
            if (wire.IsConnected())
            {
                connectedSataWires++;
            }
        }

        if (connectedSataWires == SataWires.Count)
        {
            EndGame();
        }
    }

    public void ResetSataWires()
    {
        foreach (SataWire wire in SataWires)
        {
            wire.SetConnected(false);
        }
        ShuffleSataWires();
    }

    void EndGame()
    {
        //ShowEndNotice("All SATA wires connected.");
        if (GameManager2.Instance != null)
        {

            GameManager2.Instance.MainCamera.gameObject.SetActive(true);
            GameManager2.Instance.BuildScene.gameObject.SetActive(true);

        }

        else if (GameManager.instance != null)
        {

            GameManager.instance.MainCamera.gameObject.SetActive(true);

        }
        SceneManager.UnloadSceneAsync("StorageMiniGame");
       
    }

   public void CancelButton()
    {
        //ShowEndNotice("GPU connected.");
        if (GameManager2.Instance != null)
        {

            GameManager2.Instance.MainCamera.gameObject.SetActive(true);
            GameManager2.Instance.BuildScene.gameObject.SetActive(true);

            GameManager2.Instance.BackSingleItem("Storage");
        }

        else if (GameManager.instance != null)
        {
            GameManager.instance.MainCamera.gameObject.SetActive(true);
            GameManager.instance.BackSingleItem("Storage");
        }

        SceneManager.UnloadSceneAsync("StorageMiniGame");

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
