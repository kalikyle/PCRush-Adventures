using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CollectPackageStep1 : QuestStep
{
    private int packagecollected = 0;
    private int packagetobecollected = 10;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;
    public void Start()
    {
        //packageCollected();
       
        DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
        DialogueManager.GetInstance().TriggerSection("second");
    }

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
        string status = packagecollected + " / " + packagetobecollected;
        ChangeState(state, status);
    }

    protected override void SetQuestStepState(string state)
    {
        //this.packagecollected = System.Int32.Parse(state);
        GameManager.instance.packagescollected = System.Int32.Parse(state);
        packagecollected = GameManager.instance.packagescollected;
        UpdateState();
    }
   
}
