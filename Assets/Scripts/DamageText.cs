using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    public float floatDuration = 1f;
    public float floatDistance = 1f;
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
        Vector3 startPosition = transform.position;
        Vector3 endPosition = startPosition + new Vector3(0, floatDistance, 0);

        float elapsedTime = 0;
        Color startColor = textComponent.color;
        Color endColor = startColor;
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
