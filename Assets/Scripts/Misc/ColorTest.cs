using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ColorTest : MonoBehaviour
{
    //public float duration = 5f;

    //[SerializeField] private Gradient gradient;
    //private Light2D _light;
    //private float _startTime;

    //private void Awake()
    //{
    //    _light = GetComponent<Light2D>();
    //    _startTime = Time.time;
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    // Calculate the time elapsed since the start time
    //    var timeElapsed = Time.time - _startTime;
    //    // Calculate the percentage based on the sine of the time elapsed
    //    var percentage = Mathf.Sin(timeElapsed / duration * Mathf.PI * 2) * 0.5f + 0.5f;
    //    // Clamp the percentage to be between 0 and 1
    //    percentage = Mathf.Clamp01(percentage);

    //    _light.color = gradient.Evaluate(percentage);
    //}

    //public float duration = 86400f; // 24 hours in seconds
    [SerializeField] private Gradient gradient;
    [SerializeField] private TMP_Text timeText;
    private Light2D _light;

    private void Awake()
    {
        _light = GetComponent<Light2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the percentage based on the current time
        var currentTime = System.DateTime.Now;
        var percentage = (currentTime.Hour + currentTime.Minute / 60f) / 24f;

        _light.color = gradient.Evaluate(percentage);

        // Format the time as hh:mm am/pm
        var timeString = currentTime.ToString("h:mm tt");

        timeText.text = timeString;
    }
}
