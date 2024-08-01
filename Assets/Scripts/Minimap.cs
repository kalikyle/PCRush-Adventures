using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Minimap : MonoBehaviour
{
    [Header("References")]
    public RectTransform minimapPoint_1;
    public RectTransform minimapPoint_2;
    public Transform worldPoint_1;
    public Transform worldPoint_2;

    [Header("Player")]
    public RectTransform playerMinimap;
    public Transform playerWorld;


    private float minimapRatio;



    public RawImage minimapImage;
    public float revealRadius = 5f;
    private Texture2D maskTexture;
    private string textureKey;

    /**/



    private void Awake()
    {
        CalculateMapRatio();
        

    }

    public void Start()
    {
        GameManager.instance.MinimapButton.onClick.AddListener(OpenMaps);
        SetTextureKey();
        // Create a new Texture2D with RGBA32 format to support transparency
        //int width = (int)minimapImage.rectTransform.rect.width;
        //int height = (int)minimapImage.rectTransform.rect.height;
        //maskTexture = new Texture2D(width, height, TextureFormat.RGBA32, false);


        //// Initialize the mask texture to fully opaque black
        //Color[] initialPixels = new Color[width * height];
        //for (int i = 0; i < initialPixels.Length; i++)
        //{
        //    initialPixels[i] = new Color(0, 0, 0, 1); // Fully opaque black
        //}
        //maskTexture.SetPixels(initialPixels);
        //maskTexture.Apply();

        //// Set the mask texture as the texture of the RawImage
        //minimapImage.texture = maskTexture;

        if (maskTexture == null || maskTexture.width == 0 || maskTexture.height == 0)
        {
            //Texture2D cloudyTexture = Resources.Load<Texture2D>("CloudyPhoto");

            int width = (int)minimapImage.rectTransform.rect.width;
            int height = (int)minimapImage.rectTransform.rect.height;
            maskTexture = new Texture2D(width, height, TextureFormat.RGBA32, false);

            // Initialize the mask texture to fully opaque black
            Color[] initialPixels = new Color[width * height];
            for (int i = 0; i < initialPixels.Length; i++)
            {
                initialPixels[i] = new Color(0, 0, 0, 1); // Fully opaque black
            }

            //TextureScale.Bilinear(cloudyTexture, width, height);
            //maskTexture.SetPixels(cloudyTexture.GetPixels());
            maskTexture.SetPixels(initialPixels);
            maskTexture.Apply();
        }

        minimapImage.texture = maskTexture;

        LoadTexture();
    }

   
    private void Update()
    {
        playerMinimap.anchoredPosition = minimapPoint_1.anchoredPosition + new Vector2((playerWorld.position.x - worldPoint_1.position.x) * minimapRatio,
                                         (playerWorld.position.y - worldPoint_1.position.y) * minimapRatio);

        RevealArea();

    }


    public void CalculateMapRatio()
    {
        //distance world ignoring Y axis
        Vector3 distanceWorldVector = worldPoint_1.position - worldPoint_2.position;
        distanceWorldVector.z = 0f;
        float distanceWorld = distanceWorldVector.magnitude;


        //distance minimap
        float distanceMinimap = Mathf.Sqrt(
                                Mathf.Pow((minimapPoint_1.anchoredPosition.x - minimapPoint_2.anchoredPosition.x), 2) +
                                Mathf.Pow((minimapPoint_1.anchoredPosition.y - minimapPoint_2.anchoredPosition.y), 2));


        minimapRatio = distanceMinimap / distanceWorld;
    }

    void RevealArea()
    {

        Vector2 playerPos = playerMinimap.anchoredPosition;
        Vector2 sizeDelta = minimapImage.rectTransform.sizeDelta;

        // Ensure sizeDelta has valid values
        if (sizeDelta.x <= 0 || sizeDelta.y <= 0)
        {
            Debug.LogError("Invalid sizeDelta values: " + sizeDelta);
            return;
        }

        // Normalize player position to texture coordinates
        Vector2 normalizedPlayerPos = new Vector2(
            Mathf.InverseLerp(-sizeDelta.x * 0.5f, sizeDelta.x * 0.5f, playerPos.x) * maskTexture.width,
            Mathf.InverseLerp(-sizeDelta.y * 0.5f, sizeDelta.y * 0.5f, playerPos.y) * maskTexture.height
        );

        Vector2Int maskPos = new Vector2Int(
            Mathf.Clamp((int)normalizedPlayerPos.x, 0, maskTexture.width - 1),
            Mathf.Clamp((int)normalizedPlayerPos.y, 0, maskTexture.height - 1)
        );

        //Debug.Log($"Player Position: {playerPos}");
        //Debug.Log($"Normalized Position: {normalizedPlayerPos}");
        //Debug.Log($"Mask Position: {maskPos}");

        int intRevealRadius = Mathf.CeilToInt(revealRadius);

        for (int y = -intRevealRadius; y <= intRevealRadius; y++)
        {
            for (int x = -intRevealRadius; x <= intRevealRadius; x++)
            {
                if (x * x + y * y <= revealRadius * revealRadius)
                {
                    int posX = Mathf.Clamp(maskPos.x + x, 0, maskTexture.width - 1);
                    int posY = Mathf.Clamp(maskPos.y + y, 0, maskTexture.height - 1);
                    maskTexture.SetPixel(posX, posY, new Color(0, 0, 0, 0)); // Fully transparent
                }
            }
        }
        maskTexture.Apply();
        SaveTexture();
    }
    public void OpenMaps()
    {
        if (GameManager.instance.HomeWorld == true)
        {

            GameManager.instance.LTA.HomeWorldMinimapOpen();
        }
        else if (GameManager.instance.CPUWorld == true)
        {

           GameManager.instance.LTA.CPUMinimapOpen();
        }
        else if (GameManager.instance.RAMWorld == true) {

            GameManager.instance.LTA.RAMMinimapOpen();
        }
        else if (GameManager.instance.CPUFWorld == true) { }
        else if (GameManager.instance.GPUWorld == true) { }
        else if (GameManager.instance.StorageWorld == true) { }
        else if (GameManager.instance.PSUWorld == true) { }
        else if (GameManager.instance.MBWorld == true) { }
        else if (GameManager.instance.CaseWorld == true) { }
    }
    void SaveTexture()
    {
        if (maskTexture == null)
        {
            Debug.LogError("Cannot save texture because maskTexture is null.");
            return;
        }

        byte[] textureBytes = maskTexture.EncodeToPNG();
        string base64String = Convert.ToBase64String(textureBytes);
        PlayerPrefs.SetString(textureKey, base64String);
        PlayerPrefs.Save();
        //Debug.Log("Texture saved successfully.");
    }

    void LoadTexture()
    {
        if (PlayerPrefs.HasKey(textureKey))
        {
            string base64String = PlayerPrefs.GetString(textureKey);
            if (string.IsNullOrEmpty(base64String))
            {
                Debug.LogWarning("Saved texture data is empty for key: " + textureKey);
                return;
            }

            byte[] textureBytes = Convert.FromBase64String(base64String);
            if (textureBytes.Length == 0)
            {
                Debug.LogWarning("Loaded texture data is empty for key: " + textureKey);
                return;
            }

            if (maskTexture == null || maskTexture.width != (int)minimapImage.rectTransform.rect.width || maskTexture.height != (int)minimapImage.rectTransform.rect.height)
            {
                // Initialize texture with correct size if not already set or size mismatch
                int width = (int)minimapImage.rectTransform.rect.width;
                int height = (int)minimapImage.rectTransform.rect.height;
                maskTexture = new Texture2D(width, height, TextureFormat.RGBA32, false);
            }

            bool isLoaded = maskTexture.LoadImage(textureBytes);
            if (!isLoaded)
            {
                Debug.LogError("Failed to load texture from data.");
                return;
            }

            maskTexture.Apply();
        }
        else
        {
            // No saved texture found, initialize with default settings
            Debug.LogWarning("No saved texture found for key: " + textureKey);
        }
    }

    void SetTextureKey()
    {
        if (GameManager.instance.HomeWorld)
        {
            textureKey = "HomeWorldMinimapTexture";
        }
        else if (GameManager.instance.CPUWorld)
        {
            textureKey = "CPUWorldMinimapTexture";
        }
        else if (GameManager.instance.RAMWorld)
        {
            textureKey = "RAMWorldMinimapTexture";
        }
        else if (GameManager.instance.CPUFWorld)
        {
            textureKey = "CPUFWorldMinimapTexture";
        }
        else if (GameManager.instance.GPUWorld)
        {
            textureKey = "GPUWorldMinimapTexture";
        }
        else if (GameManager.instance.StorageWorld)
        {
            textureKey = "StorageWorldMinimapTexture";
        }
        else if (GameManager.instance.PSUWorld)
        {
            textureKey = "PSUWorldMinimapTexture";
        }
        else if (GameManager.instance.MBWorld)
        {
            textureKey = "MBWorldMinimapTexture";
        }
        else if (GameManager.instance.CaseWorld)
        {
            textureKey = "CaseWorldMinimapTexture";
        }
    }

}

