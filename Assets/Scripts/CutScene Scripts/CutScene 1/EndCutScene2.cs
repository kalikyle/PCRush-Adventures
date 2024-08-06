using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class EndCutScene2 : MonoBehaviour
{
    // Start is called before the first frame update
    async void Start()
    {
        GameManager.instance.LTA.OpenTeleAnim();
        GameManager.instance.UIExplore.SetActive(true);
        GameManager.instance.SquareBars.SetActive(false);

        await Task.Delay(1500);
        GameManager.instance.CutScene2.SetActive(false);
        GameManager.instance.CutScene2Open = true;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
