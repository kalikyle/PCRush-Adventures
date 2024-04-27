using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CopyText : MonoBehaviour
{
    public TMP_Text textToCopy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void CopyID()
    {
        // Check if the text to copy is assigned
        if (textToCopy != null)
        {
            // Copy the text to the clipboard
            GUIUtility.systemCopyBuffer = textToCopy.text;
            Debug.Log("Text copied to clipboard: " + textToCopy.text);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
