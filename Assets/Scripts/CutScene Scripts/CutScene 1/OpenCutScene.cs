using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCutScene : MonoBehaviour
{
    // Start is called before the first frame update


    private GameObject targetGameObject;
    void Start()
    {
        GameManager.instance.UIExplore.SetActive(false);
        GameManager.instance.SquareBars.SetActive(true);

        if (!string.IsNullOrEmpty("Ian"))
        {
            targetGameObject = GameObject.Find("Ian");
            if (targetGameObject == null)
            {
                Debug.LogError("Target GameObject '" + "Ian" + "' not found in the scene.");

            }
        }
        else
        {
            Debug.LogError("Target GameObject name is not specified in NavigateToGameObjectStep.");

        }

        Transform childTransform = targetGameObject.transform.Find("BoxCollideTrigger");

        if (childTransform != null)
        {
            // Disable the child GameObject
            childTransform.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogError("Child GameObject with name  BoxCollideTrigger  not found.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
