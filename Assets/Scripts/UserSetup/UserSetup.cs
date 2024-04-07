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
    public Button googleSignInButton;
    public Button facebookSignInButton;
    public Button anonymousSignInButton;

    public Button PlayButton;

    public GameObject CharEditor;
    public GameObject Login;
    public GameObject PlayPanel;
    public GameObject LogInCanvas;

    public TMP_InputField playerName;



    void Start()
    {
        

        // Assign click listeners to the sign-in buttons
        googleSignInButton.onClick.AddListener(SignInWithGoogle);
        facebookSignInButton.onClick.AddListener(SignInWithFacebook);
        anonymousSignInButton.onClick.AddListener(SignInAnonymously);
        PlayButton.onClick.AddListener(PlayClick);

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
                Login.gameObject.SetActive(false);
                PlayPanel.gameObject.SetActive(true);
                CharEditor.gameObject.SetActive(false);
            }

        }
        else
        {
            
            LogInCanvas.gameObject.SetActive(true);
            Login.gameObject.SetActive(false);
            PlayPanel.gameObject.SetActive(true);
            CharEditor.gameObject.SetActive(false);
        }
      
    }
    private void Awake()
    {
        
        //this is for character edit during play
       
       
        
    }
   
    public void OpenCharacterEditor()
    {
        
    }
    public void PlayClick()
    {
        PlayPanel.gameObject.SetActive(false);
        if (string.IsNullOrEmpty(GameManager.instance.UserID))
        {
            //CharEditor.gameObject.SetActive(true);
            
            Login.gameObject.SetActive(true);
        }
        else
        {

            //CharEditor.gameObject.SetActive(false);
            Login.gameObject.SetActive(false);
            UnloadThisScene();
            GameManager.instance.AtTheStart();
        }
    }
    public void UnloadThisScene()
    {
        SceneManager.UnloadSceneAsync(1);
        GameManager.instance.LoadCharacter();
        //SceneManager.LoadSceneAsync(0);
    }
    void SignInWithGoogle()
    {
        // Authenticate using Google Play
        // (Implement your Google Play sign-in code here)
    }

    void SignInWithFacebook()
    {
        // Authenticate using Facebook
        // (Implement your Facebook sign-in code here)
    }

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
                   
                    Login.gameObject.SetActive(false);
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
