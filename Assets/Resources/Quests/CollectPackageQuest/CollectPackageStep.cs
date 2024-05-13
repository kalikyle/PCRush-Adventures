using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectPackageStep : QuestStep
{
    private int packagecollected = 0;
    private int packagetobecollected = 8;

    public void Update()
    {
        packagecollected = GameManager.instance.packagescollected;
        packageCollected();
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
        string state = packagecollected.ToString();
        ChangeState(state);
    }

    protected override void SetQuestStepState(string state)
    {
        GameManager.instance.packagescollected = System.Int32.Parse(state);
        packagecollected = GameManager.instance.packagescollected;
        UpdateState();
    }

    public void Start()
    {
        
    }
}
