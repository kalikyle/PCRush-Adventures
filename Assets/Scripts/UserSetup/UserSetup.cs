using Firebase.Auth;
using Firebase.Extensions;
using Firebase.Firestore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserSetup : MonoBehaviour
{
    public Button googleSignInButton;
    public Button facebookSignInButton;
    public Button anonymousSignInButton;

    public GameObject CharEditor;

    FirebaseAuth auth;
    FirebaseFirestore db;

    void Start()
    {
        // Initialize Firebase Auth and Firestore
        auth = FirebaseAuth.DefaultInstance;
        db = FirebaseFirestore.DefaultInstance;

        // Assign click listeners to the sign-in buttons
        googleSignInButton.onClick.AddListener(SignInWithGoogle);
        facebookSignInButton.onClick.AddListener(SignInWithFacebook);
        anonymousSignInButton.onClick.AddListener(SignInAnonymously);

       


    }
    private void Awake()
    {
        if (!string.IsNullOrEmpty(GameManager.instance.UserID))
        {
            this.gameObject.SetActive(false);
            CharEditor.gameObject.SetActive(true);
        }
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

        // Authenticate anonymously
        auth.SignInAnonymouslyAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                Debug.LogError("Anonymous sign-in failed: " + task.Exception);
                return;
            }

            FirebaseUser user = task.Result.User;

            // Automatically create a Firestore collection for the user
            
            CreateUserDataCollection(user.UserId);
            GameManager.instance.UserID = user.UserId;
            GameManager.instance.SetUserID(user.UserId);
        });
    }

    void CreateUserDataCollection(string userId)
    {
        // Reference to the user's collection
        CollectionReference userCollection = db.Collection("users");

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
                    this.gameObject.SetActive(false);
                    CharEditor.gameObject.SetActive(true);
                }
                else if (task.IsFaulted)
                {
                    Debug.LogError("Error creating user data collection: " + task.Exception);
                }
            });
    }
}
