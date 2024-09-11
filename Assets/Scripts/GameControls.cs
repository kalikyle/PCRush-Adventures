using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControls : MonoBehaviour
{

    public SettingsScript Settings;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            bool isMySceneActive = IsSceneActive("PCRush CharacterEditor");

            if (!isMySceneActive) {

                OpenSettings();
            }
           
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

    public bool IsSceneActive(string sceneName)
    {
        // Get the currently active scene
        Scene activeScene = SceneManager.GetActiveScene();

        // Check if the active scene's name matches the provided scene name
        return activeScene.name == sceneName;
    }
}
