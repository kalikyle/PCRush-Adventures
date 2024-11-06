using Exchanger.Model.CaseWorld;
using Firebase.Auth;
using Firebase.Extensions;
using Firebase.Firestore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEditor;
//using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class FirebaseController : MonoBehaviour
{
    public static FirebaseController Instance;

    public GameObject MainPanel, loginPanel, signupPanel, forgetPasswordPanel, notificationPanel;

    public TMP_InputField loginEmail, loginPassword, signupEmail, signupPassword, signupCPassword, forgetPassEmail;

    public TMP_Text notif_Title_Text, notif_Message_Text, strengthText;

    public Toggle rememberMe;

    public Firebase.Auth.FirebaseAuth auth;
    Firebase.Auth.FirebaseUser user;

   public bool isSignIn = false;
 
    private void Awake()
    {
        if (Instance == null)
        {

            Instance = this;
            DontDestroyOnLoad(gameObject);

        }
        else
        {
            // If another instance already exists, destroy this one
            Destroy(gameObject);
        }
    }

    void Start()
    { 

        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                // Create and hold a reference to your FirebaseApp.
                // where app is a Firebase.Firebaselpp property of your application class. 
                //LogOut();
                
                InitializeFirebase();
                CheckUserExistsAndSignOutIfNot();



                //set a Flag here to indicate whether firebase is ready to use by your app.
            }
            else
            {
                UnityEngine.Debug.LogError(System.String.Format(
                "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                // Firebase Unity SDK is not safe to use here.
            }
        });

        
        
    }

    public void OpenLoginPanel()
    {
        MainPanel.SetActive(true);
        loginPanel.SetActive(true);
        signupPanel.SetActive(false);
        forgetPasswordPanel.SetActive(false);
    }


    public void OpenSignupPanel()
    {
        MainPanel.SetActive(true);
        loginPanel.SetActive(false);
        signupPanel.SetActive(true);
        forgetPasswordPanel.SetActive(false);
    }

    public void OpenForgetPassPanel()
    {
        MainPanel.SetActive(true);
        loginPanel.SetActive(false);
        signupPanel.SetActive(false);
        forgetPasswordPanel.SetActive(true);
    }

    //public void OpenProfilePanel()
    //{
    //    loginPanel.SetActive(false);
    //    signupPanel.SetActive(false);
    //    profilePanel.SetActive(true);
    //    forgetPasswordPanel.SetActive(false);
    //}

    public void CloseAll()
    {
        MainPanel.SetActive(false);
        loginPanel.SetActive(false);
        signupPanel.SetActive(false);
        forgetPasswordPanel.SetActive(false);
    }

    public void LoginUser()
    {
        if (string.IsNullOrEmpty(loginEmail.text) && string.IsNullOrEmpty(loginPassword.text))
        {
            showNotificationMessage("Error", "Fields Empty! Please Input Details in All Fields");
            return;
        }
        //Do Login

        SignInUser(loginEmail.text, loginPassword.text);
    }

    public void SignUpUser()
    {
        if (string.IsNullOrEmpty(signupEmail.text) && string.IsNullOrEmpty(signupPassword.text) && string.IsNullOrEmpty(signupCPassword.text))
        {
            showNotificationMessage("Error", "Fields Empty! Please Input Details in All Fields");
            return;
        }

        if (signupPassword.text != signupCPassword.text)
        {
            showNotificationMessage("Error", "Passwords do not match! Please make sure both password fields are identical.");
            return;
        }

        CreateUser(signupEmail.text, signupPassword.text);
    }
    public void forgetPass()
    {
        if (string.IsNullOrEmpty(forgetPassEmail.text))
        {
            showNotificationMessage("Error", "Fields Empty! Please Input Details in All Fields");
            return;
        }
        forgetPasswordSubmit(forgetPassEmail.text);
    }

    public void showNotificationMessage(string title, string message)
    {
        notif_Title_Text.text = "" + title;
        notif_Message_Text.text = "" + message;

        notificationPanel.SetActive(true);
    }

    public void CloseNotif_Panel()
    {

        notif_Title_Text.text = "";
        notif_Message_Text.text = "";

        notificationPanel.SetActive(false);
    }
    public async void LogOut()
    {
        QuestManager.Instance.SaveQuests();
        auth.SignOut();
        isSignIn = false;
        auth.StateChanged -= AuthStateChange;
        auth = null;
        GameManager.instance.UserID = "";
        GameManager.instance.ResetPlayer();
        await Task.Delay(500);
        //OpenLoginPanel();
        showNotificationMessage("Goodbye", "Logging out...\nPlease Wait to while the Game is Restarting");
        //profileUserEmail_Text.text = "";

        await Task.Delay(2000);
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        System.Diagnostics.Process.Start(Application.dataPath.Replace("_Data", ".exe"));
    }

    public async void LogOutExit()
    {
        QuestManager.Instance.SaveQuests();
        auth.SignOut();
        isSignIn = false;
        auth.StateChanged -= AuthStateChange;
        auth = null;
        GameManager.instance.UserID = "";
        GameManager.instance.ResetPlayer();
        await Task.Delay(500);
        //OpenLoginPanel();
        showNotificationMessage("Goodbye", "Logging Out and Exiting the Game");
        await Task.Delay(2000);
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }


    void CreateUser(string email, string password)
    {
        // First, check the password strength
        EvaluatePasswordStrength(password);

        // If the password is weak, do not proceed
        if (strengthText.text == "Weak Password" || strengthText.text == "Password cannot be empty.")
        {
            showNotificationMessage("Error", "Please enter a stronger password.");
            return;
        }

        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(async task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync ecountered an error: " + task.Exception);
                foreach (Exception exception in task.Exception.Flatten().InnerExceptions)
                {
                    Firebase.FirebaseException firebaseEx = exception as Firebase.FirebaseException;
                    if (firebaseEx != null)
                    {
                        var errorCode = (AuthError)firebaseEx.ErrorCode;
                        showNotificationMessage("Error", GetErrorMessage(errorCode));
                    }
                }
                return;
            }

            // Firebase user has been created.
            FirebaseUser newUser = task.Result.User;
            //await Task.Delay(1000);

            if (task.IsCompleted)
            {
                await CreateUserDataCollection(user.UserId);
                GameManager.instance.UserID = user.UserId;
                //await Task.Delay(1000);
                //GameManager.instance.SaveCharInfo(user.UserId, "Player1");
                GameManager.instance.ShowFloatingText("<color=green>User Successfully Created</color>");
                await Task.Delay(1000);
                await OpenNewGame();
                isSigned = true;
            }
            //await Task.Delay(1000);
            ////UpdateUserProfile();
        });

    }

    public async Task OpenNewGame()
    {
        InternetChecker.Instance.StartCheck();
        QuestManager.Instance.ForNewUsers();
        await Task.Delay(1000);
        GameManager.instance.SaveSoldItems();
        await Task.Delay(1000);
        GameManager.instance.SaveGameObjectsToFirestore(GameManager.instance.PartsToCollect);
        AtNewUser();
        SoundManager.instance.ChangeMusic(SoundManager.instance.homeWorldBackground);
        //await Task.Delay(1000);
        //GameManager.instance.PartsController.LoadPartsItems();
        //await Task.Delay(1000);
        //GameManager.instance.StartQuest();
    }

    public void SignInUser(string email, string password)
    {
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(async task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                foreach (Exception exception in task.Exception.Flatten().InnerExceptions)
                {
                    Firebase.FirebaseException firebaseEx = exception as Firebase.FirebaseException;
                    if (firebaseEx != null)
                    {
                        var errorCode = (AuthError)firebaseEx.ErrorCode;
                        showNotificationMessage("Error", GetErrorMessage(errorCode));
                    }
                }
                return;
            }
            Firebase.Auth.FirebaseUser newUser = task.Result.User;
            //Debug.LogFormat("User signed in successfully: {0} ({1})",
            //newUser.DisplayName, newUser.UserId);
            //profileUserName_Text.text = "" + newUser.DisplayName;
            //profileUserEmail_Text.text = "" + newUser.Email;

            //openTheGame --- HERE
            GameManager.instance.UserID = user.UserId;
            isSigned = true;
            await OpenGame();


        });
    }

    //public void SignInUser(string email, string password)
    //{
    //    // Step 1: Authenticate with Firebase
    //    auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(authTask =>
    //    {
    //        if (authTask.IsCanceled)
    //        {
    //            Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
    //            return;
    //        }
    //        if (authTask.IsFaulted)
    //        {
    //            foreach (Exception exception in authTask.Exception.Flatten().InnerExceptions)
    //            {
    //                Firebase.FirebaseException firebaseEx = exception as Firebase.FirebaseException;
    //                if (firebaseEx != null)
    //                {
    //                    var errorCode = (AuthError)firebaseEx.ErrorCode;
    //                    showNotificationMessage("Error", GetErrorMessage(errorCode));
    //                }
    //            }
    //            return;
    //        }

    //        Firebase.Auth.FirebaseUser newUser = authTask.Result.User;
    //        string userId = newUser.UserId;

    //        // Step 2: Check if the user is already logged in on another device
    //        var userDoc = FirebaseFirestore.DefaultInstance.Collection("users").Document(userId);
    //        userDoc.GetSnapshotAsync().ContinueWithOnMainThread(snapshotTask =>
    //        {
    //            if (snapshotTask.IsCanceled || snapshotTask.IsFaulted)
    //            {
    //                Debug.LogError("Error checking user login status.");
    //                showNotificationMessage("Error", "Unable to verify login status. Please try again.");
    //                return;
    //            }

    //            var snapshot = snapshotTask.Result;
    //            if (snapshot.Exists && snapshot.ContainsField("loggedIn"))
    //            {
    //                bool isLoggedIn = snapshot.GetValue<bool>("loggedIn");
    //                if (isLoggedIn)
    //                {
    //                    Debug.Log("User is already logged in on another device.");
    //                    showNotificationMessage("Error", "User is already logged in on another device.");
    //                    return;
    //                }
    //            }

    //                // Step 4: Proceed with opening the game
    //                GameManager.instance.UserID = userId;
    //                OpenGame();
    //        });
    //    });
    //}


    public async Task OpenGame()
    {
       
        InternetChecker.Instance.StartCheck();
        QuestManager.Instance.ForExistingUsers();
        await Task.Delay(1000);
        UnloadThisSceneForExist();
        GameManager.instance.scene.manualLoading();
        await Task.Delay(1000);
        GameManager.instance.AtTheStart();
        await Task.Delay(1000);
        GameManager.instance.PartsController.LoadPartsItems();
        await Task.Delay(1000);
        AchievementManager.instance.LoadAchievementsFromFirebase();
        SoundManager.instance.ChangeMusic(SoundManager.instance.homeWorldBackground);
        GameManager.instance.ShowFloatingText("<color=green>Successful User Login</color>");
    }


    void InitializeFirebase()
    {
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        auth.StateChanged += AuthStateChange;
        AuthStateChange(this, null);
    }
    
    void AuthStateChange(object sender, System.EventArgs eventArgs)
    {
        if (auth.CurrentUser != user)
        {
            bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null;
            if (!signedIn && user != null)
            {
                Debug.Log("Signed out" + user.UserId);
                GameManager.instance.UserID = "";
               
            }
            user = auth.CurrentUser;
            //Debug.LogError(auth.CurrentUser.UserId);
            if (signedIn)
            {
                Debug.Log("Signed in " + user.UserId);
                isSignIn = true;
                GameManager.instance.UserID = user.UserId;
               

            }
        }
    }

   
    //public void OnDestroy()
    //{
    //    auth.StateChanged -= AuthStateChange;
    //    auth = null;
    //}

    public void UpdateUserProfile()
    {
        Firebase.Auth.FirebaseUser user = auth.CurrentUser;
        if (user != null)
        {
            Firebase.Auth.UserProfile profile = new Firebase.Auth.UserProfile();
            //{
            //    //DisplayName = UserName,
            //    PhotoUrl = new System.Uri("https://via.placeholder.com/150C/0%20https://placeholder.com/"),
            //};
            user.UpdateUserProfileAsync(profile).ContinueWith(task => {
                if (task.IsCanceled)
                {
                    Debug.LogError("UpdateUserProfileAsync was canceled.");
                    return;
                }
                if (task.IsFaulted)
                {
                    Debug.LogError("UpdateUserProfileAsync encountered an error: " + task.Exception);
                    return;
                }
               

                Debug.Log("User profile updated successfully.");


                showNotificationMessage("Alert", "Account Successfully Created");
            });
        }

    }
    public bool isSigned = false;
    void Update()
    {
        //if (isSignIn)
        //{
        //    if (!isSigned)
        //    {
        //        isSigned = true;
        //        //profileUserName_Text.text = "" + user.DisplayName;
        //        //profileUserEmail_Text.text = "" + user.Email;

        //        //opengame -- HERE
        //        //OpenProfilePanel();
        //        //SIGNED
        //        //UnloadThisScene();
        //        //GameManager.instance.AtTheStart();
        //        //GameManager.instance.scene.manualLoading();
        //    }
        //}

    }

    private static string GetErrorMessage(AuthError errorcode)
    {
        var message = "";
        switch (errorcode)
        {

            case Firebase.Auth.AuthError.AccountExistsWithDifferentCredentials:
                message = "Account Not Exist";
                break;
            case Firebase.Auth.AuthError.MissingPassword:
                message = "Missing Password";
                break;
            case Firebase.Auth.AuthError.WeakPassword:
                message = "Password So Weak";
                break;
            case Firebase.Auth.AuthError.WrongPassword:
                message = "Wrong Password";
                break;
            case Firebase.Auth.AuthError.EmailAlreadyInUse:
                message = "Your Email Already in Use";
                break;
            case Firebase.Auth.AuthError.InvalidEmail:
                message = "Your Email Invalid";
                break;
            case Firebase.Auth.AuthError.MissingEmail:
                message = "Your Email Missing";
                break;
            case Firebase.Auth.AuthError.NetworkRequestFailed:
                message = "No Internet Connection, \n Please Check Your Internet Connection and Try Again";
                break;

            default:
                message = "Invalid Error";
                break;
        }
        return message;
    }

    void forgetPasswordSubmit(string forgetPasswordEmail)
    {
        auth.SendPasswordResetEmailAsync(forgetPasswordEmail).ContinueWithOnMainThread(Task => {

            if (Task.IsCanceled)
            {
                Debug.LogError("SendPasswordResetEmailAsync was canceled");
            }
            if (Task.IsFaulted)
            {
                foreach (Exception exception in Task.Exception.Flatten().InnerExceptions)
                {
                    Firebase.FirebaseException firebaseEx = exception as Firebase.FirebaseException;
                    if (firebaseEx != null)
                    {
                        var errorCode = (AuthError)firebaseEx.ErrorCode;
                        showNotificationMessage("Error", GetErrorMessage(errorCode));
                    }
                }
            }

            showNotificationMessage("Alert", "Successfully Send Email For Reset Password");
        }
        );
    }

    public void SignInAnonymously()
    {

        //Authenticate anonymously
        FirebaseAuth.DefaultInstance.SignInAnonymouslyAsync().ContinueWithOnMainThread(async task =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                foreach (Exception exception in task.Exception.Flatten().InnerExceptions)
                {
                    Firebase.FirebaseException firebaseEx = exception as Firebase.FirebaseException;
                    if (firebaseEx != null)
                    {
                        var errorCode = (AuthError)firebaseEx.ErrorCode;
                        showNotificationMessage("Error", GetErrorMessage(errorCode));
                    }
                }
            }

            FirebaseUser user = task.Result.User;
            // Automatically create a Firestore collection for the user
            Debug.Log("Anonymous sign-in successful! UID: " + user.UserId);
            ///await Task.Delay(1200);
            ///

            if (task.IsCompleted)
            {
                await CreateUserDataCollection(user.UserId);
                GameManager.instance.UserID = user.UserId;
                //await Task.Delay(1000);
                //GameManager.instance.SaveCharInfo(user.UserId, "Player1");
                GameManager.instance.ShowFloatingText("<color=green>User Successfully Created</color>");
                InternetChecker.Instance.StartCheck();
                await Task.Delay(1000);
                QuestManager.Instance.ForNewUsers();
                await Task.Delay(1000);
                GameManager.instance.SaveSoldItems();
                await Task.Delay(1000);
                GameManager.instance.SaveGameObjectsToFirestore(GameManager.instance.PartsToCollect);
                isSigned = true;
                AtNewUser();
                SoundManager.instance.ChangeMusic(SoundManager.instance.homeWorldBackground);

            }

           
            //await Task.Delay(1000);
            //GameManager.instance.PartsController.LoadPartsItems();
            //await Task.Delay(1000);
            //GameManager.instance.StartQuest();
            //await Task.Delay(1000);
        });
    }

    public Task CreateUserDataCollection(string userId)
    {
        // Reference to the user's collection
        CollectionReference userCollection = FirebaseFirestore.DefaultInstance.Collection("users");

        // Create a new document with the user's ID
        DocumentReference userDoc = userCollection.Document(userId);

        // Set initial data for the user (optional)
        // For example, you might set default values for user settings
        Dictionary<string, object> userData = new Dictionary<string, object>
        {
            //player info
            { "playerName", "Player1" },
            { "playerMoney", GameManager.instance.PlayerMoney },
            //{ "playerGems", PlayerGems },
            { "playerLevel", GameManager.instance.PlayerLevel },
            {"playerEXP", GameManager.instance.PlayerEXP},
            {"playerEXPNeedtoLevelUp", GameManager.instance.PlayerExpToLevelUp },
            {"playerTotalMoneyEarned", GameManager.instance.PlayerTotalMoney },
            //player stats

            {"playerAttack", GameManager.instance.PlayerAttackDamage },
            { "playerHealth", GameManager.instance.PlayerHealth },
            { "playerMana", GameManager.instance.PlayerMana },
            { "playerHealthRegen", GameManager.instance.PlayerHealthRegen },
            { "playerWalkSpeed", GameManager.instance.PlayerWalkSpeed },
            { "playerArmor", GameManager.instance.PlayerArmor },
            { "playerManaRegen", GameManager.instance.PlayerManaRegen },
            { "playerCriticalChance", GameManager.instance.PlayerCriticalChance },

            //for lan
            { "playerEasyModeWin", GameManager.instance.PlayerEasyModeWins },
            { "playerNormalModeWin",GameManager.instance.PlayerNormalModeWins },
            { "playerHardModeWin", GameManager.instance.PlayerHardModeWins },
            { "playerBestTimeEasy", GameManager.instance.PlayerBestTimeEasyMode },
            { "playerBestTimeNormal", GameManager.instance.PlayerBestTimeNormalMode },
            { "playerBestTimeHard", GameManager.instance.PlayerBestTimeHardMode },
        };

        // Set the data in the document
        userDoc.SetAsync(userData)
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsCompleted)
                {
                    //Debug.Log("User data collection created.");
                    UserSetup.instance.OpenCharEditor();
                }
                else if (task.IsFaulted)
                {
                    Debug.LogError("Error creating user data collection: " + task.Exception);
                }
            });
        return Task.CompletedTask;
    }

    public async void AtNewUser()
    {
        GameManager.instance.charBuilder.SaveChanges();
        GameManager.instance.SaveCharInfo(GameManager.instance.UserID, "Player1");
        GameManager.instance.LoadCharacter();
        GameManager.instance.charBuilder.CombineHeadAndHairSprites();
        await Task.Delay(500);
        GameManager.instance.StartQuest();
        
    }

    public void UnloadThisSceneForExist()
    {
        SceneManager.UnloadSceneAsync(1);
        GameManager.instance.LoadCharacter();
        //GameManager.instance.StartQuest();
        //GameManager.instance.StartQuest();

    }

    public void OnApplicationQuit()
    {
        //auth.SignOut();
    }
    public void deleteLogin()
    {
        auth.CurrentUser.DeleteAsync().ContinueWith(task => {
            if (task.IsCompleted)
            {
                // After the account is deleted, sign out the user
                auth.SignOut();
                //profileUserEmail_Text.text = ""
                GameManager.instance.UserID = "";
#if UNITY_EDITOR
                EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
                System.Diagnostics.Process.Start(Application.dataPath.Replace("_Data", ".exe"));
                Debug.Log("User account deleted and signed out.");
            }
            else
            {
                Debug.LogError("Failed to delete the user account: " + task.Exception);
            }
        });
    }

    void CheckUserExistsAndSignOutIfNot()
    {
        if (auth.CurrentUser == null)
        {
            Debug.Log("No user currently signed in.");
            return;
        }

        string userId = auth.CurrentUser.UserId;
        DocumentReference userDoc = FirebaseFirestore.DefaultInstance.Collection("users").Document(userId);

        userDoc.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("CheckUserExistsAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("CheckUserExistsAsync encountered an error: " + task.Exception);
                return;
            }

            DocumentSnapshot snapshot = task.Result;
            if (snapshot.Exists)
            {
                Debug.Log("User exists in Firestore.");
                return;
            }
            else
            {
                Debug.Log("User does not exist. Signing out...");
                //OpenLoginPanel();
                showNotificationMessage("Error", "Logged IN User doesn't Exist, Restarting...");
                deleteLogin();
            }
        });
    }

    public void CheckPasswordStrength()
    {
        string password = signupPassword.text;
        EvaluatePasswordStrength(password);
    }

    private void EvaluatePasswordStrength(string password)
    {
        if (string.IsNullOrEmpty(password))
        {
            strengthText.text = "Password cannot be empty.";
            strengthText.color = Color.white; // Default color for empty message
            return;
        }

        int score = 0;

        // Check password length
        if (password.Length >= 8)
        {
            score++;
        }
        if (password.Length >= 12)
        {
            score++;
        }

        // Check for digits
        if (System.Text.RegularExpressions.Regex.IsMatch(password, @"\d"))
        {
            score++;
        }

        // Check for uppercase letters
        if (System.Text.RegularExpressions.Regex.IsMatch(password, @"[A-Z]"))
        {
            score++;
        }

        // Check for special characters
        if (System.Text.RegularExpressions.Regex.IsMatch(password, @"[\W_]"))
        {
            score++;
        }

        // Determine strength based on score
        switch (score)
        {
            case 0:
            case 1:
                strengthText.text = "Weak Password";
                strengthText.color = Color.red; // Weak = Red
                break;
            case 2:
                strengthText.text = "Good Password";
                strengthText.color = Color.green; // Good = Yellow
                break;
            case 3:
            case 4:
                strengthText.text = "Very Good Password";
                strengthText.color = Color.cyan; // Very Good = Green
                break;
            default:
                strengthText.text = "Very Good Password";
                strengthText.color = Color.cyan; // Very Good = Green
                break;
        }
    }
}
