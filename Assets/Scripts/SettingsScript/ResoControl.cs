using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ResoControl : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown resoDropdown;

    private Resolution[] resolutions;
    private List<Resolution> filteredResolutions;

    private float currentRefreshrate;
    private int currentResolutionIndex = 0;

    [System.Obsolete]
    void Start()
    {
        resolutions = Screen.resolutions;
        filteredResolutions = new List<Resolution>();

        resoDropdown.ClearOptions();
        currentRefreshrate = Screen.currentResolution.refreshRate;

        Debug.Log("RefreshRate: " + currentRefreshrate);

        for (int i = 0; i < resolutions.Length; i++)
        {
            Debug.Log("Resolution: " + resolutions[i]);
            if (resolutions[i].refreshRate == currentRefreshrate)
            {
                filteredResolutions.Add(resolutions[i]);
            }
        }



        List<string> options = new List<string>();
        for (int i = 0; i < filteredResolutions.Count; i++)
        {
            string resolutionOtions = filteredResolutions[i].width + "x" + filteredResolutions[i].height + " " + filteredResolutions[i].refreshRate + "Hz";
            options.Add(resolutionOtions);

            if (filteredResolutions[i].width == Screen.width && filteredResolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }


        resoDropdown.AddOptions(options);
        resoDropdown.value = currentResolutionIndex;
        resoDropdown.RefreshShownValue();
    }


    public void SetResolution(int resolutionindex)
    {
        Resolution resolution = filteredResolutions[resolutionindex];
        Screen.SetResolution(resolution.width, resolution.height, true);
    }

   
}
