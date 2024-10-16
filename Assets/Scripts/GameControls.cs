using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControls : MonoBehaviour
{

    public GameObject Settings;
    // Update is called once per frame
    void Update()
    {
        bool isMySceneActive = IsSceneActive(1);
        Debug.LogError(isMySceneActive);
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            
            if (isMySceneActive == false) {

                OpenSettings();
            }
           
        }
    }
    

    public void OpenSettings()
    {
        if (Settings.activeSelf == false)
        {
            Settings.gameObject.SetActive(true);
        }
        else
        {

            Settings.gameObject.SetActive(false);

        }
    }

    public bool IsSceneActive(int sceneIndex)
    {
        // Check if the scene at the provided index is active
        if (SceneManager.GetSceneByBuildIndex(sceneIndex).IsValid())
        {
            // Get the name of the scene
            string sceneName = SceneManager.GetSceneByBuildIndex(sceneIndex).name;

            // Print the scene name to the debug console
            Debug.LogError("Active scene: " + sceneName);

            // Return true if the scene is active
            return true;
        }
        else
        {
            // Return false if the scene is not active
            return false;
        }
    }
}
