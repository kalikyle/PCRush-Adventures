using Firebase.Firestore;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardManager : MonoBehaviour
{
    public GameObject leaderboardEntryPrefab;
    public Transform leaderboardContent;

    private FirebaseFirestore db;

    public Sprite DefaultImage;
    public Sprite UserImage;

    void Start()
    {
        //db = FirebaseFirestore.DefaultInstance;
        //LoadLeaderboard();
    }
    private void OnEnable()
    {
        db = FirebaseFirestore.DefaultInstance;
        LoadLeaderboard();
    }
    async void LoadLeaderboard()
    {
        // Clear previous leaderboard entries
        foreach (Transform child in leaderboardContent)
        {
            Destroy(child.gameObject);
        }

        Query userQuery = db.Collection("users").OrderByDescending("playerLevel");
        QuerySnapshot querySnapshot = await userQuery.GetSnapshotAsync();

        int rank = 1; // Initialize rank counter

        foreach (DocumentSnapshot documentSnapshot in querySnapshot.Documents)
        {
            Dictionary<string, object> user = documentSnapshot.ToDictionary();
            string userId = documentSnapshot.Id;
            string playerName = user.ContainsKey("playerName") ? user["playerName"].ToString() : "Unknown";
            int playerLevel = user.ContainsKey("playerLevel") ? int.Parse(user["playerLevel"].ToString()) : 0;

            GameObject leaderboardEntry = Instantiate(leaderboardEntryPrefab, leaderboardContent);

            // Set the ranking number

            if(userId == GameManager.instance.UserID)
            {
                leaderboardEntry.GetComponent<Image>().sprite = UserImage;
            }
            else
            {
                leaderboardEntry.GetComponent<Image>().sprite = DefaultImage;
            }

            TMP_Text rankText = leaderboardEntry.transform.Find("PlayerRanking").GetComponent<TMP_Text>();
            rankText.text = rank.ToString()+".";

            if (rank == 1)
            {
                rankText.color = Color.green; // Rank 1 = Green
                rankText.gameObject.transform.localScale = new Vector2(1.5f, 1.5f);
            }
            else if (rank == 2)
            {
                rankText.color = Color.yellow; // Rank 2 = Yellow
                rankText.gameObject.transform.localScale = new Vector2(1.2f, 1.2f);
            }
            else if (rank == 3)
            {
                rankText.color = new Color(1f, 0.64f, 0f); // Rank 3 = Orange (RGB: 255, 165, 0)
                rankText.gameObject.transform.localScale = new Vector2(1f, 1f);
            }
            else
            {
                rankText.color = Color.white; // Rank 4 and up = White
                rankText.gameObject.transform.localScale = new Vector2(0.8f, 0.8f);
            }

            // Set the player's name and level
            leaderboardEntry.transform.Find("PlayerName").GetComponent<TMP_Text>().text = playerName;
            leaderboardEntry.transform.Find("PlayerLevel").GetComponent<TMP_Text>().text = playerLevel.ToString();
            leaderboardEntry.transform.Find("PlayerUserID").GetComponent<TMP_Text>().text = userId;
            // Optionally, set the player's avatar if you have it in your database
            // leaderboardEntry.transform.Find("PlayerAvatar").GetComponent<Image>().sprite = ...

            rank++; // Increment the rank for the next player
        }
    }

    
}
