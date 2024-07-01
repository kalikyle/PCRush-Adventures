using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CollectPackageStep : QuestStep
{
    private int packagecollected = 0;
    private int packagetobecollected = 8;
    private int previousPackageCollected = 0;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    public async void Start()
    {
       
        if (packagecollected == 0)
        {
            await Task.Delay(1500);
            DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
            DialogueManager.GetInstance().TriggerSection("first");
        }

        packageCollected();
    }
    public void Update()
    {
        packagecollected = GameManager.instance.packagescollected;

        if (packagecollected != previousPackageCollected)
        {
            // If it has changed, call packageCollected method
            packageCollected();
            // Update the previousPackageCollected to the new value
            previousPackageCollected = packagecollected;
        }

        
    }

   

    public void packageCollected()
    {
        if(packagecollected >= packagetobecollected)
        {
            FinishQuestStep();
            
        }
        UpdateState();
    }



    private void UpdateState()
    {
       
        string status = packagecollected + " / " + packagetobecollected;
        string state = packagecollected.ToString();
        ChangeState(state, status);
    }

    protected override void SetQuestStepState(string state)
    {
        //Debug.LogError(state);
        GameManager.instance.packagescollected = System.Int32.Parse(state);
        packagecollected = GameManager.instance.packagescollected;
        UpdateState();
    }

}
