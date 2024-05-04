using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DesktopController : MonoBehaviour
{
    public TMP_Text time;
    public TMP_Text date;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DateTime now = DateTime.Now;

        time.text = now.ToString("h:mm tt"); // 12-hour format with AM/PM
        date.text = now.ToString("M/d/yyyy"); // custom date format

    }
}
