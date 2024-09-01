using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLogic : MonoBehaviour
{
    public static GameLogic instance;
    public List<Wire> Wires;
    //public GameObject EndMessage;
    //public TextMeshProUGUI textNotice;
    //public GameObject panel;    

    private Vector3 resetPos;

    void ShuffleWires()
    {
        List<Vector3> wirePosition = new List<Vector3>();
        foreach (Wire w in Wires)
        {
            wirePosition.Add(w.transform.position);
        }
        foreach (Wire w in Wires)
        {
            int randomIndex = Random.Range(0, wirePosition.Count);
            w.transform.position = wirePosition[randomIndex];
            wirePosition.RemoveAt(randomIndex);
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
        if (scene.name == "MotherboardMiniGame")
        {
            // Destroy the GameManager2 instance when PCRush scene is unloaded
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {        
        ShuffleWires();

        //if (panel != null)
        //{
        //    panel.SetActive(false);
        //}

        //if (textNotice != null)
        //{
        //    textNotice.text = "";
        //}
    }
    private void OnDestroy()
    {
        // Unsubscribe from scene unloaded event to avoid memory leaks
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }
    // Update is called once per frame

    void Update()
    {
        int connectedWires = 0;
        bool connected = false;
        foreach (Wire w in Wires)
        {
            if (w.isItConnected())
            {
                connectedWires++;
            }
        }

        if (connectedWires == Wires.Count && connected == false)
        {
            //EndMessage.SetActive(true);
            EndGame();
            connected = true;
        }
    }

    public void ResetWires()
    {
        foreach (Wire w in Wires)
        {
            w.setConnected(true);
        }
        ShuffleWires();        
    }

    public void EndGame()
    {
        //ShowEndNotice("Front Panel Headers Connected.");
        GameManager2.Instance.MainCamera.gameObject.SetActive(true);
        GameManager2.Instance.BuildScene.gameObject.SetActive(true);
        SceneManager.UnloadSceneAsync("MotherboardMiniGame");

    }

    public void cancelButton()
    {
        GameManager2.Instance.MainCamera.gameObject.SetActive(true);
        GameManager2.Instance.BuildScene.gameObject.SetActive(true);
        SceneManager.UnloadSceneAsync("MotherboardMiniGame");
        GameManager2.Instance.BackSingleItem("Motherboard");
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