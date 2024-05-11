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
    }
    public void Start()
    {
        
    }
}
