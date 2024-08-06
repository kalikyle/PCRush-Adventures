using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OpenBuildingDesk : QuestStep
{
    private GameObject Ian;
    public Vector3 targetPosition;
    public string StepInfo = "Go to the Desk and Click the Build Button";
    // Start is called before the first frame update
    void Start()
    {
        if (Ian == null)
        {
            // Try to find the GameObject by name if it's not assigned in the inspector
            Ian = GameObject.Find("Ian");

            if (Ian == null)
            {
                Debug.LogError("Ian GameObject not found. Make sure it's assigned or exists in the scene.");
                return;
            }
        }

        Ian.transform.position = targetPosition;

        Transform childTransform = Ian.transform.Find("BoxCollideTrigger");

        if (childTransform != null)
        {
            // Disable the child GameObject
            childTransform.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogError("Child GameObject with name  BoxCollideTrigger  not found.");
        }

        GameManager.instance.HouseDoor.SetActive(true);
        GameManager.instance.packagescollected = 8;// need to have this in the rest of the quest step
        GameManager.instance.Enter.onClick.AddListener(OnClickEnterButton);
    }
    public void OnClickEnterButton()
    {
       if(GameManager.instance.OpenBuild == true)
        {
            FinishQuestStep();
            ChangeState("finish", "finish");
            GameManager.instance.OpenBuild = false;
        }
       
    }
    // Update is called once per frame
    void Update()
    {

    }
    protected override void SetQuestStepState(string state)
    {


    }
}
