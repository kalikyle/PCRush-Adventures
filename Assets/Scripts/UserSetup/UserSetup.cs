using Firebase.Auth;
using Firebase.Extensions;
using Firebase.Firestore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UserSetup : MonoBehaviour
{
    //public Button googleSignInButton;
    //public Button facebookSignInButton;
    //public Button anonymousSignInButton;

    public static UserSetup instance;

    public Button PlayButton;
    public Button PlayOnLan;

    public GameObject CharEditor;
    public GameObject PlayPanel;
    public GameObject LogInCanvas;
    public GameObject IntroCanvas;
    public GameObject BackGround;
    public TMP_InputField playerName;

    public GameObject MainPanel, loginPanel, signupPanel, forgetPasswordPanel, notificationPanel;

    public TMP_InputField loginEmail, loginPassword, signupEmail, signupPassword, signupCPassword, forgetPassEmail;

    public TMP_Text notif_Title_Text, notif_Message_Text;

    public Toggle rememberMe;

    public Button loginButton, createaccbtn, forgotbutton, signinbtn , guestbtn, closenotifbtn, forgotpassbtn, logoutButton;

    public Slider BackGroundMainMenu;
    public Slider EffectsMainMenu;


    public Button Facebook;
    public Button Youtube;
    public Button Itch;
    public Button Survey;
    public TMP_Text VersionText;

    public TMP_Text InternetText;

    [SerializeField]
    private string FacebookUrl = "";

    [SerializeField]
    private string YoutubeUrl = "";

    [SerializeField]
    private string ItchUrl = "";
    
    [SerializeField]
    private string SurveyUrl = "";

    private void Awake()
    {
       
        instance = this;
        VersionText.text = "Ver. " + Application.version;
        
    }

    void Start()
    {
        StartCoroutine(CheckInternetConnection());
        SoundManager.instance.PlayMainMenuBackground();
        FirebaseController.Instance.MainPanel = MainPanel;
        FirebaseController.Instance.MainPanel = MainPanel;
        FirebaseController.Instance.loginPanel = loginPanel;
        FirebaseController.Instance.signupPanel = signupPanel;
        FirebaseController.Instance.forgetPasswordPanel = forgetPasswordPanel;
        FirebaseController.Instance.notificationPanel = notificationPanel;

        FirebaseController.Instance.loginEmail = loginEmail;
        FirebaseController.Instance.loginPassword = loginPassword;
        FirebaseController.Instance.signupEmail = signupEmail;
        FirebaseController.Instance.signupPassword = signupPassword;
        FirebaseController.Instance.signupCPassword = signupCPassword;
        FirebaseController.Instance.forgetPassEmail = forgetPassEmail;

        FirebaseController.Instance.notif_Title_Text = notif_Title_Text;
        FirebaseController.Instance.notif_Message_Text = notif_Message_Text;


        FirebaseController.Instance.rememberMe = rememberMe;


        LeanTween.scale(PlayButton.gameObject, new Vector3(1f, 1f, 1f), 2f).setDelay(.5f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.scale(PlayOnLan.gameObject, new Vector3(1f, 1f, 1f), 2f).setDelay(.5f).setEase(LeanTweenType.easeOutElastic);

        //FOR BACKGROUND EASTHETIC
        if (GameManager.instance.HasOpened)
        {
            BackGround.SetActive(true);
        }

        GameManager.instance.HasOpened = true;
        // Assign click listeners to the sign-in buttons
        //anonymousSignInButton.onClick.AddListener(SignInAnonymously);
        PlayButton.onClick.AddListener(PlayClick);
        PlayOnLan.onClick.AddListener(OnPlayLanClick);


        loginButton.onClick.AddListener(FirebaseController.Instance.LoginUser);
        createaccbtn.onClick.AddListener(FirebaseController.Instance.OpenSignupPanel);
        forgotbutton.onClick.AddListener(FirebaseController.Instance.OpenForgetPassPanel);
        signinbtn.onClick.AddListener(FirebaseController.Instance.SignUpUser);
        guestbtn.onClick.AddListener(FirebaseController.Instance.SignInAnonymously);
        closenotifbtn.onClick.AddListener(FirebaseController.Instance.CloseNotif_Panel);
        forgotpassbtn.onClick.AddListener(FirebaseController.Instance.forgetPass);
        logoutButton.onClick.AddListener(FirebaseController.Instance.LogOut);
        //if (!string.IsNullOrEmpty(GameManager.instance.UserID))
        //{


        //    //FOR IN GAME EDIT
        if (GameManager.instance.OpenEditor == true)
        {
            LogInCanvas.gameObject.SetActive(false);
            CharEditor.gameObject.SetActive(true);
        }
        else
        {
            LogInCanvas.gameObject.SetActive(true);
            PlayPanel.gameObject.SetActive(true);
            CharEditor.gameObject.SetActive(false);
        }

        //}
        //else
        //{

        //    LogInCanvas.gameObject.SetActive(true);
        //    PlayPanel.gameObject.SetActive(true);
        //    CharEditor.gameObject.SetActive(false);
        //}
        BackGroundMainMenu.value = SoundManager.instance.backgroundVolume;
        EffectsMainMenu.value = SoundManager.instance.effectsVolume;

        Facebook.onClick.AddListener(() => OpenBrowser(FacebookUrl));
        Youtube.onClick.AddListener(() => OpenBrowser(YoutubeUrl));
        Itch.onClick.AddListener(() => OpenBrowser(ItchUrl));
        Survey.onClick.AddListener(() => OpenBrowser(SurveyUrl));
    }

    IEnumerator CheckInternetConnection()
    {
        while (true)
        {
            internetcheck();
            yield return new WaitForSeconds(1f); // Check every 1 second
        }
    }

    public void internetcheck()
    {
        const string GOOGLE_DNS = "8.8.8.8";
        try
        {
            using (System.Net.NetworkInformation.Ping ping = new System.Net.NetworkInformation.Ping())
            {
                PingReply reply = ping.Send(GOOGLE_DNS);

                if (reply.Status == IPStatus.Success)
                {
                    if (reply.RoundtripTime > 150)
                    {
                        InternetText.text = $"{reply.RoundtripTime}ms";
                        InternetText.color = Color.red;
                       
                    }
                    else
                    {
                        InternetText.text = $"{reply.RoundtripTime}ms";
                        InternetText.color = Color.green;
                       
                    }
                }
                else
                {
                    InternetText.text = "No Internet";
                    InternetText.color = Color.red;
                    
                }
            }
        }
        catch (Exception)
        {
            //Debug.LogError("Error checking internet connection: " + e.Message);
            InternetText.text = "No Internet";
            InternetText.color = Color.red; // Indicate an error
           
        }
    }

    private void OpenBrowser(string URL)
    {
        // Open the URL
        Application.OpenURL(URL);
    }


    public void MainMenuBackgroundVolume(float volume)
    {
        SoundManager.instance.SetBackgroundVolume(volume);
        SoundManager.instance.BackGround.value = volume;
    }

    public void MainEffectsVolume(float volume)
    {
        SoundManager.instance.SetEffectsVolume(volume);
        SoundManager.instance.Effects.value = volume;
    }
    public async void PlayClick()
    {

        //if (string.IsNullOrEmpty(GameManager.instance.UserID))
        //{
        //    //NOT SIGNED
        //    //CharEditor.gameObject.SetActive(true);

        //    //Login.gameObject.SetActive(true);
        //    //SignInAnonymously();
        //}
        //else
        //{
        //    //SIGNED
        //    CharEditor.gameObject.SetActive(false);

        //    UnloadThisScene();

        //    GameManager.instance.AtTheStart();
        //    GameManager.instance.scene.manualLoading();


        //}
        SoundManager.instance.PlayButtonClick();
        
        //SoundManager.instance.ChangeMusic(SoundManager.instance.homeWorldBackground);
        if (FirebaseController.Instance.isSignIn)
        {
            if (InternetChecker.Instance.TryStartGame() == true)
            {

                if (!FirebaseController.Instance.isSigned)
                {
                    FirebaseController.Instance.isSigned = true;

                    //SIGNED
                    // Debug.LogError("SCENE UNLOADEDSSSSS");
                    InternetChecker.Instance.StartCheck();
                    QuestManager.Instance.ForExistingUsers();
                    await Task.Delay(1500);
                    UnloadThisScene();
                    GameManager.instance.scene.manualLoading();
                    await Task.Delay(1500);
                    GameManager.instance.AtTheStart();
                    await Task.Delay(1500);
                    GameManager.instance.PartsController.LoadPartsItems();
                    await Task.Delay(1000);
                    AchievementManager.instance.LoadAchievementsFromFirebase();
                    SoundManager.instance.ChangeMusic(SoundManager.instance.homeWorldBackground);


                }
                else
                {
                    //during signin main menu
                    //Debug.LogError("SCENE UNLOADED");
                    SceneManager.UnloadSceneAsync(1);
                    GameManager.instance.scene.manualLoading();
                    SoundManager.instance.backgroundMusicSource.Stop();
                }
            }
            else
            {
                FirebaseController.Instance.showNotificationMessage("Error", "No Internet Connection, \n Please Check Your Internet Connection and Try Again");
            }
        }
        else
        {
            FirebaseController.Instance.OpenLoginPanel();
        }
        

    }
    public async void OnPlayLanClick()
    {
        SoundManager.instance.PlayButtonClick();
        //if no user
        if (string.IsNullOrEmpty(GameManager.instance.UserID))
        {
            SceneManager.UnloadSceneAsync(1);
            GameManager.instance.scene.manualLoading();
            GameManager.instance.PlayOnLanCanvas.SetActive(true);
        }
        else
        {
            //QuestManager.Instance.ForExistingUsers();
            //await Task.Delay(1500);
            //UnloadThisScene();
            //await Task.Delay(1500);
            //GameManager.instance.AtTheStart();
            //await Task.Delay(1500);
            //GameManager.instance.PartsController.LoadPartsItems();
            //GameManager.instance.PlayOnLanCanvas.SetActive(true);
            if (!FirebaseController.Instance.isSigned)
            {
                FirebaseController.Instance.isSigned = true;

                //SIGNED
                
                QuestManager.Instance.ForExistingUsers();
                await Task.Delay(1000);
                UnloadThisScene();
                GameManager.instance.scene.manualLoading();
                GameManager.instance.PlayOnLanCanvas.SetActive(true);
                await Task.Delay(1000);
                GameManager.instance.AtTheStart();
                await Task.Delay(1000);
                GameManager.instance.PartsController.LoadPartsItems();
                
            }
            else
            {
                //during signedin main menu
                UnloadThisScene();
                GameManager.instance.PlayOnLanCanvas.SetActive(true);
                //GameManager.instance.scene.manualLoading();
            }


        }

    }
    public void UnloadThisScene()
    {
        SceneManager.UnloadSceneAsync(1);
        GameManager.instance.LoadCharacter();
        GameManager.instance.StartQuest();
        SoundManager.instance.backgroundMusicSource.Stop();
        //SceneManager.LoadSceneAsync(0);
    }

    public void Quit()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    //void SignInWithGoogle()
    //{

    //}

    //void SignInWithFacebook()
    //{
    //    // Authenticate using Facebook
    //    // (Implement your Facebook sign-in code here)
    //}

    //void SignInAnonymously()
    //{

    //    //Authenticate anonymously
    //    FirebaseAuth.DefaultInstance.SignInAnonymouslyAsync().ContinueWithOnMainThread(task =>
    //    {
    //        if (task.IsCanceled || task.IsFaulted)
    //        {
    //            Debug.LogError("Anonymous sign-in failed: " + task.Exception);
    //            return;
    //        }

    //        FirebaseUser user = task.Result.User;

    //        // Automatically create a Firestore collection for the user

    //            CreateUserDataCollection(user.UserId);
    //            Debug.Log("Anonymous sign-in successful! UID: " + user.UserId);
    //            GameManager.instance.UserID = user.UserId;
    //            GameManager.instance.SetUserID(user.UserId);


    //        //initials
    //        GameManager.instance.SaveSoldItems();
    //        GameManager.instance.SaveCharInfo(user.UserId, "Player1");
    //        GameManager.instance.SaveGameObjectsToFirestore(GameManager.instance.PartsToCollect);

    //    });
    //}

    //void CreateUserDataCollection(string userId)
    //{
    //    // Reference to the user's collection
    //    CollectionReference userCollection = FirebaseFirestore.DefaultInstance.Collection("users");

    //    // Create a new document with the user's ID
    //    DocumentReference userDoc = userCollection.Document(userId);

    //    // Set initial data for the user (optional)
    //    // For example, you might set default values for user settings
    //    var userData = new
    //    {
    //        // Add initial user data here
    //    };

    //    // Set the data in the document
    //    userDoc.SetAsync(userData)
    //        .ContinueWithOnMainThread(task =>
    //        {
    //            if (task.IsCompleted)
    //            {
    //               Debug.Log("User data collection created.");

    //                PlayPanel.gameObject.SetActive(false);
    //                LogInCanvas.gameObject.SetActive(false);
    //                CharEditor.gameObject.SetActive(true);
    //            }
    //            else if (task.IsFaulted)
    //            {
    //                Debug.LogError("Error creating user data collection: " + task.Exception);
    //            }
    //        });
    //}

    public void OpenCharEditor()
    {
        PlayPanel.gameObject.SetActive(false);
        LogInCanvas.gameObject.SetActive(false);
        CharEditor.gameObject.SetActive(true);
    }
}
