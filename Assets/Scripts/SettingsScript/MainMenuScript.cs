using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    public GameObject TopScene;
    public GameObject PlayerName;
    public GameObject HomeScene;
    public GameObject ChangeName;
    public TMP_Text Label;
    public TMP_Text BottomName;

    public Button playbutton;
    public AudioSource playsound;

    public Button donebuttonset;
    public Button Changedonebuttonset;
    public Button NameButton;
    public TMP_InputField nameset;

    [SerializeField]
    private LeanTweenAnimate LTA;

   

    //// Start is called before the first frame update
    //public void Start()
    //{
    //    playbutton.onClick.AddListener(() =>
    //    {

    //        ONPlayerPlay();
    //    });
    //    donebuttonset.onClick.AddListener(() =>
    //    {

    //        OnSetName();
    //    });
    //    NameButton.onClick.AddListener(() =>
    //    {
    //        ChangeName.gameObject.SetActive(true);
    //        Label.text = "Change Player Name:";
    //        nameset.text = GameManager.Instance.PlayerName;
    //        Changedonebuttonset.gameObject.SetActive(true);
    //        donebuttonset.gameObject.SetActive(false);
    //    });
    //    Changedonebuttonset.onClick.AddListener(() =>
    //    {
    //        OnUpdateName();
    //    });
    //}
    //public void ONPlayerPlay()
    //{
    //    if (PlayerPrefs.GetInt("TutorialDone") == 1)
    //    {
    //        TopScene.SetActive(true);
    //        HomeScene.SetActive(true);
    //        LTA.hideMainMenu();
    //        LTA.hideBuild();
    //        playsound.Play();

    //    }
    //    else if (PlayerPrefs.GetInt("TutorialDone") == 0 /*|| PlayerPrefs.GetInt("TutorialDone") == null*/)
    //    {
    //        Changedonebuttonset.gameObject.SetActive(false);
    //        donebuttonset.gameObject.SetActive(true);
    //        PlayerName.SetActive(true);
    //        nameset.text = "Player";

    //    }

    //}
    //public void OnSetName()
    //{
    //    if (nameset.text == null || nameset.text == "")
    //    {
    //        nameset.text = "Invalid";
    //    }
    //    else
    //    {
    //        Label.text = "Set Player Name:";
    //        GameManager.Instance.PlayerName = nameset.text;
    //        GameManager.Instance.SaveData();
    //    }
    //}
    //public void OnUpdateName()
    //{
    //    if (nameset.text == null || nameset.text == "")
    //    {
    //        nameset.text = "Invalid";
    //    }
    //    else
    //    {
    //        BottomName.text = nameset.text;
    //        GameManager.Instance.PlayerName = nameset.text;
    //        GameManager.Instance.SaveData();
    //        ChangeName.gameObject.SetActive(false);
    //    }
       
    //}
    

    //// Update is called once per frame
    //public void Update()
    //{
       
    //}
}
