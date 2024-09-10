using Firebase.Auth;
using Firebase.Extensions;
using Firebase.Firestore;
using System.Collections;
using System.Collections.Generic;
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

    public Button loginButton, createaccbtn, forgotbutton, signinbtn , guestbtn, closenotifbtn, forgotpassbtn;

    private void Awake()
    {
       
        instance = this;
    }

    void Start()
    {
       
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

       

        if (FirebaseController.Instance.isSignIn)
        {
            if (!FirebaseController.Instance.isSigned)
            {
                FirebaseController.Instance.isSigned = true;

                //profileUserName_Text.text = "" + user.DisplayName;
                //profileUserEmail_Text.text = "" + user.Email;

                //opengame -- HERE
                //OpenProfilePanel();
                //SIGNED
                QuestManager.Instance.ForExistingUsers();
                await Task.Delay(1500);
                UnloadThisScene();
                GameManager.instance.scene.manualLoading();
                await Task.Delay(1500);
                GameManager.instance.AtTheStart();
                
                //GameManager.instance.SaveGameObjectsToFirestore(GameManager.instance.PartsToCollect);
            }
        }
        else
        {
            FirebaseController.Instance.OpenLoginPanel();
        }


    }
    public void OnPlayLanClick()
    {
        
        if (string.IsNullOrEmpty(GameManager.instance.UserID))
        {
            SceneManager.UnloadSceneAsync(1);
            GameManager.instance.scene.manualLoading();
            GameManager.instance.PlayOnLanCanvas.SetActive(true);
        }
        else
        {
            UnloadThisScene();
            GameManager.instance.AtTheStart();
            GameManager.instance.PlayOnLanCanvas.SetActive(true);
        }

    }
    public void UnloadThisScene()
    {
        SceneManager.UnloadSceneAsync(1);
        GameManager.instance.LoadCharacter();
        GameManager.instance.StartQuest();
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
