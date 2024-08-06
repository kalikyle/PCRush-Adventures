using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class EndCutScenes1 : MonoBehaviour
{
    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;
    // Start is called before the first frame update
    async void Start()
    {
        GameManager.instance.LTA.OpenTeleAnim();
        //GameManager.instance.UIExplore.SetActive(true);
        GameManager.instance.SquareBars.SetActive(false);

        await Task.Delay(1000);
        DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
        DialogueManager.GetInstance().TriggerSection("fourth");

        await Task.Delay(1000);
        GameManager.instance.CutScene1.SetActive(false);
        //GameManager.instance.CutScene1.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
