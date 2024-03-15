using Assets.PixelHeroes.Scripts.CharacterScrips;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameManager instance;
    public CharacterBuilder charBuilder;
    public int ShopSize = 20;



    public Image Monitor;
    public Image Keyboard;
    public Image Mouse;
    public Image Desk;


    void Start()
    {
        if(PlayerPrefs.GetInt("CharChanged") == 0)
        {
            SceneManager.LoadScene(1, LoadSceneMode.Additive);
        }
        charBuilder.LoadSavedData();
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // If another instance already exists, destroy this one
            Destroy(gameObject);
        }
    }
    public void LoadCharacter()
    {
        charBuilder.LoadSavedData();
    }
}
