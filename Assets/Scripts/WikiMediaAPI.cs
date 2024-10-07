using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Newtonsoft.Json;

public class WikiMediaAPI : MonoBehaviour
{
    public Image triviaImage;
    public TMP_Text triviaTitle;
    public TMP_Text triviasnippet;
    public Button nextButton;
    public Button refreshButton;
    public GameObject loadingIcon;
    public GameObject loadingScreen;
    public GameObject circling;

    private int currentIndex = 0;
    private List<string> imageUrls = new List<string>();
    private List<string> descriptions = new List<string>();
    List<WikiSearchResult> searchItems = new List<WikiSearchResult>();
    List<WikiTrivia> triviaItems = new List<WikiTrivia>();

    void Start()
    {
        nextButton.onClick.AddListener(GetNextTrivia);
        refreshButton.onClick.AddListener(RefreshTrivia);
        triviaItems.Clear();
        searchItems.Clear();
        imageUrls.Clear();
        descriptions.Clear();
        ShowLoadingScreen(true);
        LeanTween.rotateAround(circling, Vector3.forward, 360f, 5f).setLoopClamp();
        string searchQuery = GetRandomSearchQuery();
        Debug.Log("Selected Search Query: " + searchQuery);

        StartCoroutine(FetchTriviaData(searchQuery));
    }

    string GetRandomSearchQuery()
    {
        // Define an array of search queries
        string[] queries = {
     "computer hardwares",
     "computer parts",
     "technology",
     "programming languages",
     "softwares",
     "Computer Storage",
     "Video games",
     "Computers"
 };

        // Pick a random query from the array
        return queries[Random.Range(0, queries.Length)];
    }


    void RefreshTrivia()
    {
        // Change the search query or you can set it to a new random query here
        triviaItems.Clear();
        searchItems.Clear();
        imageUrls.Clear();
        descriptions.Clear();
        currentIndex = 0;
        ShowLoadingScreen(true);
        string searchQuery = GetRandomSearchQuery();
        Debug.Log("Selected Search Query: " + searchQuery);// Assuming you have a method to get a random query
        StartCoroutine(FetchTriviaData(searchQuery)); // Fetch new trivia data
    }

    private bool IsEnglish(string input)
    {
        return System.Text.RegularExpressions.Regex.IsMatch(input, @"^[\x20-\x7E]+$");
    }

    IEnumerator FetchTriviaData(string searchQuery)
    {
        triviaItems.Clear();
        searchItems.Clear();
        imageUrls.Clear();
        descriptions.Clear();
        currentIndex = 0;

        string searchUrl = $"https://commons.wikimedia.org/w/api.php?action=query&format=json&list=search&srsearch={System.Uri.EscapeDataString(searchQuery)}&srwhat=text&srlimit=10&uselang=en";

        string continueToken = null;

        do
        {
            if (continueToken != null)
            {
                searchUrl += $"&sroffset={continueToken}";
            }

            using (UnityWebRequest request = UnityWebRequest.Get(searchUrl))
            {
                yield return request.SendWebRequest();

                if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
                {
                    Debug.LogError(request.error);
                    ShowLoadingScreen(true);
                    yield break; // Exit the coroutine on error
                }
                else
                {
                    var jsonResponse = request.downloadHandler.text;
                    //Debug.Log("Search JSON Response: " + jsonResponse);  // Log the raw JSON response

                    var searchResponse = JsonConvert.DeserializeObject<WikiSearchResponse>(jsonResponse);
                    if (searchResponse?.query?.search != null)
                    {
                        foreach (var searchItem in searchResponse.query.search)
                        {


                            if (triviaItems.Count >= 15)
                            {
                                // Stop fetching more data if we already have 20 items
                                continueToken = null;
                                break;
                            }

                            // Decode HTML entities for title and snippet
                            string decodedTitle = System.Net.WebUtility.HtmlDecode(searchItem.title);
                            string decodedSnippet = System.Net.WebUtility.HtmlDecode(searchItem.snippet);

                            if (!IsEnglish(decodedTitle))
                            {
                                continue; // Skip this snippet if it's not in English
                            }

                            if (!IsEnglish(decodedSnippet))
                            {
                                continue; // Skip this snippet if it's not in English
                            }

                            triviaItems.Add(new WikiTrivia
                            {
                                title = decodedTitle,
                                snippet = decodedSnippet,
                                imageUrl = null // Placeholder, will be updated later
                            });

                            // Fetch image info based on search result
                            StartCoroutine(FetchImageInfo(decodedTitle)); // Pass the decoded title
                        }

                        // Check for continuation token
                        continueToken = searchResponse.@continue?.sroffset;
                    }
                    else
                    {
                        Debug.LogError("Search response is null or does not contain search results.");
                        continueToken = null; // Exit loop
                    }
                }
            }
        } while (!string.IsNullOrEmpty(continueToken) && triviaItems.Count < 15); // Continue fetching if there is more data and we haven't reached 30 items

        ShowLoadingScreen(false);
        // Optionally, trigger a method to process all the fetched data
        DisplayTrivia(); // Ensure this is called after all items have been fetched and image URLs have been set
    }

    IEnumerator FetchImageInfo(string title)
    {
        string infoUrl = $"https://commons.wikimedia.org/w/api.php?action=query&format=json&titles={System.Uri.EscapeDataString(title)}&prop=images&imlimit=1";

        using (UnityWebRequest request = UnityWebRequest.Get(infoUrl))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError(request.error);
            }
            else
            {
                var jsonResponse = request.downloadHandler.text;
                //Debug.Log("Image Info JSON Response: " + jsonResponse);  // Log the raw JSON response

                var infoResponse = JsonConvert.DeserializeObject<WikiInfoResponse>(jsonResponse);
                if (infoResponse?.query?.pages != null)
                {
                    foreach (var pagePair in infoResponse.query.pages)
                    {
                        var page = pagePair.Value;
                        if (page?.images != null && page.images.Length > 0)
                        {
                            string imageUrl = $"https://commons.wikimedia.org/wiki/Special:FilePath/{page.images[0].title}";
                            var triviaItem = triviaItems.Find(item => item.title == title);
                            if (triviaItem != null)
                            {
                                triviaItem.imageUrl = imageUrl;
                            }
                        }
                    }

                    // Display trivia once images are loaded
                    DisplayTrivia();
                }
                else
                {
                    Debug.LogError("Info response is null or does not contain pages.");
                }
            }
        }
    }


    void DisplayTrivia()
    {
        if (currentIndex < triviaItems.Count)
        {
            var triviaItem = triviaItems[currentIndex];
            if (!string.IsNullOrEmpty(triviaItem.imageUrl))
            {
                loadingIcon.SetActive(true); // Show loading icon
                StartCoroutine(LoadImage(triviaItem.imageUrl));
            }

            triviaTitle.text = triviaItem.title;
            triviasnippet.text = RemoveHtmlTags(triviaItem.snippet);
        }
    }

    IEnumerator LoadImage(string imageUrl)
    {
        using (UnityWebRequest request = UnityWebRequestTexture.GetTexture(imageUrl))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError(request.error);
            }
            else
            {
                Texture2D texture = ((DownloadHandlerTexture)request.downloadHandler).texture;

                // Check if texture is null
                if (texture != null)
                {
                    triviaImage.gameObject.SetActive(true);
                    // Create and assign sprite to the Image component
                    triviaImage.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                }
                else
                {

                    triviaImage.gameObject.SetActive(false);
                }
            }
        }
        loadingIcon.SetActive(false);
    }

    void GetNextTrivia()
    {
        currentIndex++;
        if (currentIndex >= triviaItems.Count)
        {
            currentIndex = 0; // Loop back to the start
        }
        DisplayTrivia();
    }

    void ShowLoadingScreen(bool show)
    {
        if (loadingScreen != null)
        {
            loadingScreen.SetActive(show);
        }
    }


    string RemoveHtmlTags(string input)
    {
        return Regex.Replace(input, "<.*?>", string.Empty);
    }


}
[System.Serializable]
public class WikiSearchResponse
{
    public WikiSearchQuery query;
    public WikiContinue @continue;
}

[System.Serializable]
public class WikiSearchQuery
{
    public WikiSearchResult[] search;
}

[System.Serializable]
public class WikiSearchResult
{
    public int pageid;
    public string title;
    public string snippet;
}
[System.Serializable]
public class WikiContinue
{
    public string sroffset;
}

[System.Serializable]
public class WikiInfoResponse
{
    public WikiInfoQuery query;
}

[System.Serializable]
public class WikiInfoQuery
{
    public Dictionary<string, WikiInfoPage> pages;
}

[System.Serializable]
public class WikiInfoPage
{
    public WikiImage[] images;
    public string extract; // Keep this if needed, otherwise remove
}

[System.Serializable]
public class WikiImage
{
    public string title;
}
[System.Serializable]
public class WikiTrivia
{
    public string title;
    public string snippet;
    public string imageUrl;
}
