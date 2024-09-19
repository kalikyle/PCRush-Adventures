using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NotifText : MonoBehaviour
{
    private float floatDuration = 5f;
    private float floatDistance = 30f;
    public TMP_Text textComponent; // Use TextMeshProUGUI if using TextMeshPro

    private void Start()
    {
        StartCoroutine(FloatUpAndFade());
    }

    public void SetText(string text, Color color)
    {
        textComponent.text = text;
        textComponent.color = color;
    }

    private IEnumerator FloatUpAndFade()
    {
        Vector3 startPosition = new Vector3 (450f, 30f, 0f);
        Vector3 endPosition = startPosition + new Vector3(0, floatDistance, 0f);

        float elapsedTime = 0;
        Color startColor = textComponent.color;
        Color endColor = Color.black;
        endColor.a = 0;

        while (elapsedTime < floatDuration)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / floatDuration);
            textComponent.color = Color.Lerp(startColor, endColor, elapsedTime / floatDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }
}
