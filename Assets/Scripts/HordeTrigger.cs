using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HordeTrigger : MonoBehaviour
{
    public TMP_Text timerText; // Reference to the Text component for displaying the timer
    public Button startButton; // Reference to the Button to start the timer
    public Button stopButton;
    public Canvas CPUWorldCanvas;
    public GameObject ONHordeUI;
    public GameObject ExploreUI;
    public GameObject QuestUI;
    public GameObject TopPanelUI;
    public GameObject ButtonsPanelUI;
    public GameObject Wall;

    private float countdownTime = 120f; // Countdown time in seconds (2 minutes)
    private bool isTimerRunning = false;

    private void Start()
    {
        if (timerText == null)
        {
            Debug.LogError("Timer Text is not assigned.");
        }
        if (startButton == null)
        {
            Debug.LogError("Start Button is not assigned.");
        }
        else
        {
            startButton.onClick.AddListener(StartTimer);
        }


        if (stopButton == null)
        {
            Debug.LogError("Stop Button is not assigned.");
        }
        else
        {
            stopButton.onClick.AddListener(StopTimer);
        }
       



        UpdateTimerText(countdownTime); // Initialize the timer text
    }

    private void Update()
    {
        if (isTimerRunning)
        {
            countdownTime -= Time.deltaTime;

            if (countdownTime <= 0f)
            {
                countdownTime = 0f;
                isTimerRunning = false;
                CPUWorldCanvas.gameObject.SetActive(true);
                ExploreUI.SetActive(true);
                QuestUI.SetActive(true);
                TopPanelUI.SetActive(true);
                ButtonsPanelUI.SetActive(true);
                ONHordeUI.SetActive(false);
                Wall.gameObject.SetActive(false);
            }

            UpdateTimerText(countdownTime);
        }
    }

    private void StartTimer()
    {
        countdownTime = 120f; // Reset the countdown time to 2 minutes
        isTimerRunning = true; // Start the timer

        CPUWorldCanvas.gameObject.SetActive(false);
        ExploreUI.SetActive(true);
        QuestUI.SetActive(false);
        TopPanelUI.SetActive(false);
        ButtonsPanelUI.SetActive(false);
        ONHordeUI.SetActive(true);
        Wall.gameObject.SetActive(true);
    }

    private void UpdateTimerText(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void StopTimer()
    {
        isTimerRunning = false; // Stop the timer
        CPUWorldCanvas.gameObject.SetActive(true);
        ExploreUI.SetActive(true);
        ONHordeUI.SetActive(false);
        Wall.gameObject.SetActive(false);
    }
}
