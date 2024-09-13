using Firebase.Firestore;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardManager : MonoBehaviour
{
    public GameObject leaderboardEntryPrefab;
    public Transform leaderboardContent;

    //private FirebaseFirestore db;

    public Sprite DefaultImage;
    public Sprite UserImage;

    private List<GameObject> leaderboardEntryPool = new List<GameObject>();

    //async void Start()
    //{
    //    //db = FirebaseFirestore.DefaultInstance;
    //    //LoadLeaderboard();
    //    ///await LoadLeaderboard();
    //}
    GameObject GetLeaderboardEntry()
    {
        GameObject entry = leaderboardEntryPool.FirstOrDefault(e => !e.activeInHierarchy);
        if (entry == null)
        {
            entry = Instantiate(leaderboardEntryPrefab, leaderboardContent);
            leaderboardEntryPool.Add(entry);
        }
        entry.SetActive(true);
        return entry;
    }

    private async void OnEnable()
    {
        await LoadLeaderboard();
    }
    async Task LoadLeaderboard()
    {
        // Clear previous leaderboard entries
        foreach (Transform child in leaderboardContent)
        {
            child.gameObject.SetActive(false);
        }

        Query userQuery = FirebaseFirestore.DefaultInstance.Collection("users").OrderByDescending("playerLevel").Limit(20);
        QuerySnapshot querySnapshot = await userQuery.GetSnapshotAsync();

        int rank = 1; // Initialize rank counter

        foreach (DocumentSnapshot documentSnapshot in querySnapshot.Documents)
        {
            Dictionary<string, object> user = documentSnapshot.ToDictionary();
            string userId = documentSnapshot.Id;
            string playerName = user.ContainsKey("playerName") ? user["playerName"].ToString() : "Unknown";
            int playerLevel = user.ContainsKey("playerLevel") ? int.Parse(user["playerLevel"].ToString()) : 0;

            int playerEasyWins = user.ContainsKey("playerEasyModeWin") ? int.Parse(user["playerEasyModeWin"].ToString()) : 0;
            int playerNormalWins = user.ContainsKey("playerNormalModeWin") ? int.Parse(user["playerNormalModeWin"].ToString()) : 0;
            int playerHardWins = user.ContainsKey("playerHardModeWin") ? int.Parse(user["playerHardModeWin"].ToString()) : 0;

            float playerBestTimeEasy = user.ContainsKey("playerBestTimeEasy") ? float.Parse(user["playerBestTimeEasy"].ToString()) : 0;
            float playerBestTimeNormal = user.ContainsKey("playerBestTimeNormal") ? float.Parse(user["playerBestTimeNormal"].ToString()) : 0;
            float playerBestTimeHard = user.ContainsKey("playerBestTimeHard") ? float.Parse(user["playerBestTimeHard"].ToString()) : 0;


            GameObject leaderboardEntry = GetLeaderboardEntry();

            // Set the ranking number

            if (userId == GameManager.instance.UserID)
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

            //lan games
            leaderboardEntry.transform.Find("EasyWins").GetComponent<TMP_Text>().text = playerEasyWins.ToString();
            leaderboardEntry.transform.Find("NormalWins").GetComponent<TMP_Text>().text = playerNormalWins.ToString();
            leaderboardEntry.transform.Find("HardWins").GetComponent<TMP_Text>().text = playerHardWins.ToString();

            leaderboardEntry.transform.Find("EasyTime").GetComponent<TMP_Text>().text = convertTime(playerBestTimeEasy);
            leaderboardEntry.transform.Find("NormalTime").GetComponent<TMP_Text>().text = convertTime(playerBestTimeNormal);
            leaderboardEntry.transform.Find("HardTime").GetComponent<TMP_Text>().text = convertTime(playerBestTimeHard);
            // Optionally, set the player's avatar if you have it in your database
            // leaderboardEntry.transform.Find("PlayerAvatar").GetComponent<Image>().sprite = ...
            rank++; // Increment the rank for the next player
        }
    }

    public string convertTime(float time) {

        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60);
        int milliseconds = Mathf.FloorToInt((time * 1000f) % 1000);

        // Update the TMP_Text component with the formatted time
        return $"{minutes:00}:{seconds:00}:{milliseconds:00}";

    }

    
}
