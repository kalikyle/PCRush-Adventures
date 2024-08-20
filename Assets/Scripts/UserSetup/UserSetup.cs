using Firebase.Auth;
using Firebase.Extensions;
using Firebase.Firestore;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UserSetup : MonoBehaviour
{
    //public Button googleSignInButton;
    //public Button facebookSignInButton;
    //public Button anonymousSignInButton;

    public Button PlayButton;
    public Button PlayOnLan;

    public GameObject CharEditor;
    public GameObject PlayPanel;
    public GameObject LogInCanvas;
    public GameObject IntroCanvas;

    public TMP_InputField playerName;



    void Start()
    {
        

        // Assign click listeners to the sign-in buttons
        //anonymousSignInButton.onClick.AddListener(SignInAnonymously);
        PlayButton.onClick.AddListener(PlayClick);
        PlayOnLan.onClick.AddListener(OnPlayLanClick);

        if (!string.IsNullOrEmpty(GameManager.instance.UserID))
        {
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

        }
        else
        {
            
            LogInCanvas.gameObject.SetActive(true);
            PlayPanel.gameObject.SetActive(true);
            CharEditor.gameObject.SetActive(false);
        }
      
    }
    public void PlayClick()
    {
        if (string.IsNullOrEmpty(GameManager.instance.UserID))
        {
            //CharEditor.gameObject.SetActive(true);

            //Login.gameObject.SetActive(true);
            SignInAnonymously();
        }
        else
        {

            //CharEditor.gameObject.SetActive(false);
            
            UnloadThisScene();
           
            GameManager.instance.AtTheStart();
            GameManager.instance.scene.manualLoading();
            

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
    //void SignInWithGoogle()
    //{
        
    //}

    //void SignInWithFacebook()
    //{
    //    // Authenticate using Facebook
    //    // (Implement your Facebook sign-in code here)
    //}

    void SignInAnonymously()
    {

        //Authenticate anonymously
        FirebaseAuth.DefaultInstance.SignInAnonymouslyAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                Debug.LogError("Anonymous sign-in failed: " + task.Exception);
                return;
            }

            FirebaseUser user = task.Result.User;

            // Automatically create a Firestore collection for the user
           
                CreateUserDataCollection(user.UserId);
                Debug.Log("Anonymous sign-in successful! UID: " + user.UserId);
                GameManager.instance.UserID = user.UserId;
                GameManager.instance.SetUserID(user.UserId);


            //initials
            GameManager.instance.SaveSoldItems();
            GameManager.instance.SaveCharInfo(user.UserId, "Player1");
            GameManager.instance.SaveGameObjectsToFirestore(GameManager.instance.PartsToCollect);

        });
    }

    void CreateUserDataCollection(string userId)
    {
        // Reference to the user's collection
        CollectionReference userCollection = FirebaseFirestore.DefaultInstance.Collection("users");

        // Create a new document with the user's ID
        DocumentReference userDoc = userCollection.Document(userId);

        // Set initial data for the user (optional)
        // For example, you might set default values for user settings
        var userData = new
        {
            // Add initial user data here
        };

        // Set the data in the document
        userDoc.SetAsync(userData)
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsCompleted)
                {
                   Debug.Log("User data collection created.");
                   
                    PlayPanel.gameObject.SetActive(false);
                    LogInCanvas.gameObject.SetActive(false);
                    CharEditor.gameObject.SetActive(true);
                }
                else if (task.IsFaulted)
                {
                    Debug.LogError("Error creating user data collection: " + task.Exception);
                }
            });
    }
}
