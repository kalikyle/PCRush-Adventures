using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectPackageStep1 : QuestStep
{
    private int packagecollected = 0;
    private int packagetobecollected = 10;

    public void Update()
    {
        packagecollected = GameManager.instance.packagescollected;
               
        packageCollected();

    }

    public void packageCollected()
    {
        
        if (packagecollected >= packagetobecollected)
        {
            FinishQuestStep();
        }

        UpdateState();

    }

    private void UpdateState()
    {
        string state = packagecollected.ToString();
        ChangeState(state);
    }

    protected override void SetQuestStepState(string state)
    {
        //this.packagecollected = System.Int32.Parse(state);
        GameManager.instance.packagescollected = System.Int32.Parse(state);
        packagecollected = GameManager.instance.packagescollected;
        UpdateState();
    }
    public void Start()
    {
        
    }
}
