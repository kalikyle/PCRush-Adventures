using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControls : MonoBehaviour
{

    public SettingsScript Settings;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            OpenSettings();
        }
    }
    

    public void OpenSettings()
    {
        if (Settings.isActiveAndEnabled == false)
        {
            Settings.gameObject.SetActive(true);
        }
        else
        {

            Settings.gameObject.SetActive(false);

        }
    }
}
