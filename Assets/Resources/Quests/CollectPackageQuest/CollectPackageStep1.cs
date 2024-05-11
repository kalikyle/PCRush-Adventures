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
        if(packagecollected >= packagetobecollected)
        {
            FinishQuestStep();
        }
    }
    public void Start()
    {
        
    }
}
