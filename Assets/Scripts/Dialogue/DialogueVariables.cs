
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using Firebase.Firestore;
using System;
using System.Threading.Tasks;

public class DialogueVariables 
{
    public Dictionary<string, Ink.Runtime.Object> variables { get; private set; }

    private Story globalVariablesStory;
    private const string saveVariablesKey = "INK_VARIABLES";

    public void StartListening(Story story)
    {
        // it's important that VariablesToStory is before assigning the listener!
        VariablesToStory(story);
        story.variablesState.variableChangedEvent += VariableChanged;
    }

    public async void LoadVariableData()
    {
        await LoadVariables();
    }


    public DialogueVariables(TextAsset loadGlobalInk)
    {
        globalVariablesStory = new Story(loadGlobalInk.text);
        LoadVariableData();
        // initialize the dictionary
        variables = new Dictionary<string, Ink.Runtime.Object>();
        foreach (string name in globalVariablesStory.variablesState)
        {
            Ink.Runtime.Object value = globalVariablesStory.variablesState.GetVariableWithName(name);
            variables.Add(name, value);
            Debug.Log("Initialized global dialogue variable: " + name + " = " + value);
        }
    }

    public async Task LoadVariables()
    {
        try
        {
            if(GameManager.instance.UserID != "")
            {
                DocumentReference docRef = FirebaseFirestore.DefaultInstance
                .Collection(GameManager.instance.UserCollection)
                .Document(GameManager.instance.UserID)
                .Collection("SaveDialogueVariables")
                .Document(saveVariablesKey); // Using saveVariablesKey as document ID

                // Fetch the document snapshot
                DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

                // Check if the document exists
                if (snapshot.Exists)
                {
                    // Deserialize the JSON data from Firestore
                    string jsonState = snapshot.GetValue<string>("globalVariablesState");

                    // Load the JSON data into globalVariablesStory.state
                    globalVariablesStory.state.LoadJson(jsonState);

                    Debug.Log("Variables loaded from Firestore successfully.");
                }
                else
                {
                    Debug.LogWarning("Document does not exist for loading variables.");
                }
            }
           
            
        }
        catch (Exception e)
        {
            Debug.LogError("Failed to load variables from Firestore: " + e.Message);
        }
    }

    public void StopListening(Story story)
    {
        story.variablesState.variableChangedEvent -= VariableChanged;
    }

    private void VariableChanged(string name, Ink.Runtime.Object value)
    {
        // only maintain variables that were initialized from the globals ink file
        if (variables.ContainsKey(name))
        {
            variables.Remove(name);
            variables.Add(name, value);
        }
    }
    private void VariablesToStory(Story story)
    {
        foreach (KeyValuePair<string, Ink.Runtime.Object> variable in variables)
        {
            story.variablesState.SetGlobal(variable.Key, variable.Value);
        }
    }


    public async void SaveVariables()
    {
        if (globalVariablesStory != null)
        {
            try
            {
                // Load the current state of all of our variables to the globals story
                VariablesToStory(globalVariablesStory);

                // Serialize the global variables story state to JSON
                string json = globalVariablesStory.state.ToJson();

                // Get a reference to the Firestore document
                DocumentReference docRef = FirebaseFirestore.DefaultInstance
                    .Collection(GameManager.instance.UserCollection)
                    .Document(GameManager.instance.UserID)
                    .Collection("SaveDialogueVariables")
                    .Document(saveVariablesKey); // Using saveVariablesKey as document ID

                // Create a dictionary to store the data
                Dictionary<string, object> dataDict = new Dictionary<string, object>
            {
                { "globalVariablesState", json }
            };

                // Set the data of the document
                await docRef.SetAsync(dataDict);

                Debug.Log("Variables saved to Firestore successfully.");
            }
            catch (Exception e)
            {
                Debug.LogError("Failed to save variables to Firestore: " + e.Message);
            }
        }


    }
    

    

}
