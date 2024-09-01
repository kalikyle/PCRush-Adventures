using Inventory;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class ApplyThermalPaste : MonoBehaviour //, IPointerClickHandler
{
    [SerializeField]
    public UnityEngine.UI.Image CPU;

    [SerializeField]
    public UnityEngine.UI.Image CPUMask;

   [SerializeField]
    public Button doneBTN;

    [SerializeField]
    public Button CancelBTN;

    public TMP_Text paintedPercentageText;

    private Color originalColor;

    public Texture2D maskTexture;


    void Start()
    {
        originalColor = CPU.color;
        doneBTN.gameObject.SetActive(false);
        doneBTN.onClick.AddListener(Reset);

        //CancelBTN.onClick.AddListener(() => IC.HandleBackItem("CPU"));
    }
    
    void OnEnable()
    {
        InitializeMask();
        SetCPUImage(GameManager2.Instance.UsedImagesNeeds["CPU"].item.ItemImage);
    }
    public void SetCPUImage(Sprite cpuImage)
    {
        if (cpuImage != null)
        {
            CPU.sprite = cpuImage;

            Debug.Log("CPU Sprite Renderer is assigned!");
        }
        else
        {
            Debug.Log("CPU Sprite Renderer is not assigned!");
        }
    }// Reference to your CPU SpriteRenderer.

    //public void OnPointerClick(PointerEventData eventData)
    //{
    //    //Debug.Log("Clicked on CPUImage!");

    //    //// Get the texture coordinate where the click happened (if needed)
    //    //Vector2 localCursor;

    //    //if (RectTransformUtility.ScreenPointToLocalPointInRectangle(CPUMask.rectTransform, eventData.position, null, out localCursor))
    //    //{
    //    //    // Implement the logic to apply thermal paste or modify the CPU image.
    //    //    // For example, change the color of the CPU image:
    //    //    //CPU.color = Color.gray;

    //    //    PaintCPU(localCursor);
    //    //    doneBTN.gameObject.SetActive(true);
    //    //}
    //}
    bool isPainting = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isPainting = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isPainting = false;
        }

        if (isPainting)
        {
            Vector2 localCursor;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(CPUMask.rectTransform, Input.mousePosition, null, out localCursor);

            int x = Mathf.FloorToInt((localCursor.x + CPUMask.rectTransform.rect.width / 2) * (maskTexture.width / CPUMask.rectTransform.rect.width));
            int y = Mathf.FloorToInt((localCursor.y + CPUMask.rectTransform.rect.height / 2) * (maskTexture.height / CPUMask.rectTransform.rect.height));

            PaintArea(x, y);
            
        }
    }
    public void Reset()
    {
        CPU.color = originalColor;
        doneBTN.gameObject.SetActive(false);
        isPainting = false;
        paintedPercentageText.text = "0.0%";

    }
    void InitializeMask()
    {
        // Initialize the mask texture to transparent
        maskTexture = new Texture2D((int)CPUMask.rectTransform.rect.width, (int)CPUMask.rectTransform.rect.height);
        Color[] transparentColors = new Color[maskTexture.width * maskTexture.height];

        for (int i = 0; i < transparentColors.Length; i++)
        {
            transparentColors[i] = new Color(1f, 1f, 1f, 0f); // Transparent white (full alpha)
        }

        maskTexture.SetPixels(transparentColors);
        maskTexture.Apply();

        // Apply the mask texture to the CPUImage
        CPUMask.sprite = Sprite.Create(maskTexture, new Rect(0, 0, maskTexture.width, maskTexture.height), Vector2.one * 0.15f);
    }

    //void PaintCPU(Vector2 position)
    //{
    //    // Get the pixel coordinates within the mask texture
    //    int x = (int)(position.x + CPUMask.rectTransform.rect.width/2);
    //    int y = (int)(position.y + CPUMask.rectTransform.rect.height/2);

    //    // Update the mask texture to simulate painting at the clicked position
    //    for (int i = -10; i <= 10; i++)
    //    {
    //        for (int j = -10; j <= 10; j++)
    //        {
    //            int pixelX = x + i;
    //            int pixelY = y + j;

    //            if (pixelX >= 0 && pixelX < maskTexture.width && pixelY >= 0 && pixelY < maskTexture.height)
    //            {
    //                maskTexture.SetPixel(pixelX, pixelY, Color.black);
    //            }
    //        }
    //    }
    //    maskTexture.Apply();

    //    // Apply the updated mask texture to the CPUImage
    //    CPUMask.sprite = Sprite.Create(maskTexture, new Rect(0, 0, maskTexture.width, maskTexture.height), Vector2.one * 0.5f);
    //}
    //void PaintArea(int startX, int startY)
    //{
    //    for (int i = -15; i <= 15; i++)
    //    {
    //        for (int j = -15; j <= 15; j++)//adjust size
    //        {
    //            int pixelX = startX + i;
    //            int pixelY = startY + j;

    //            if (pixelX >= 0 && pixelX < maskTexture.width && pixelY >= 0 && pixelY < maskTexture.height)
    //            {
    //                maskTexture.SetPixel(pixelX, pixelY, Color.grey);
    //            }
    //        }
    //    }

    //    maskTexture.Apply();
    //    UpdatePaintedPercentage();
    //    CPUMask.sprite = Sprite.Create(maskTexture, new Rect(0, 0, maskTexture.width, maskTexture.height), Vector2.one * 0.15f);
    //}

    void PaintArea(int startX, int startY)
    {
        int radius = 25; // Adjust this to change the size of the circle

        for (int i = -radius; i <= radius; i++)
        {
            for (int j = -radius; j <= radius; j++)
            {
                int pixelX = startX + i;
                int pixelY = startY + j;

                // Check if the point is within the circle
                if (pixelX >= 0 && pixelX < maskTexture.width && pixelY >= 0 && pixelY < maskTexture.height &&
                    i * i + j * j <= radius * radius)
                {
                    maskTexture.SetPixel(pixelX, pixelY, Color.grey);
                }
            }
        }

        maskTexture.Apply();
        UpdatePaintedPercentage();
        CPUMask.sprite = Sprite.Create(maskTexture, new Rect(0, 0, maskTexture.width, maskTexture.height), Vector2.one * 0.15f);
    }





    void UpdatePaintedPercentage()
    {
        int paintedPixels = 0;
        Color targetColor = Color.grey;

        for (int y = 0; y < maskTexture.height; y++)
        {
            for (int x = 0; x < maskTexture.width; x++)
            {
                Color pixelColor = maskTexture.GetPixel(x, y);

                // Compare the colors using a tolerance (e.g., within 0.01)
                if (ColorsAreSimilar(pixelColor, targetColor, 0.01f))
                {
                    paintedPixels++;
                }
            }
        }
        Debug.Log("Painted Pixels: " + paintedPixels);

        float paintedPercentage = (float)paintedPixels / (maskTexture.width * maskTexture.height) * 100f;

        Debug.Log("Percentage: " + paintedPercentage.ToString("F1") + "%");
        paintedPercentageText.text = paintedPercentage.ToString("F2") + "%";


        //finish painting
        if(paintedPercentage == 100f)
        {
            //doneBTN.gameObject.SetActive(true);
            Reset();
            GameManager2.Instance.MainCamera.gameObject.SetActive(true);
            GameManager2.Instance.BuildScene.gameObject.SetActive(true);
            SceneManager.UnloadSceneAsync("CPUMiniGame");
           
        }
    }
    bool ColorsAreSimilar(Color colorA, Color colorB, float tolerance)
    {
        return Mathf.Abs(colorA.r - colorB.r) <= tolerance &&
               Mathf.Abs(colorA.g - colorB.g) <= tolerance &&
               Mathf.Abs(colorA.b - colorB.b) <= tolerance &&
               Mathf.Abs(colorA.a - colorB.a) <= tolerance;
    }

    public void CancelButton()
    {
        Reset();
        GameManager2.Instance.MainCamera.gameObject.SetActive(true);
        GameManager2.Instance.BuildScene.gameObject.SetActive(true);
        SceneManager.UnloadSceneAsync("CPUMiniGame");
        GameManager2.Instance.BackSingleItem("CPU");
    }

}
