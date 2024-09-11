using Assets.PixelHeroes.Scripts.CharacterScrips;
using Assets.PixelHeroes.Scripts.EditorScripts;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class unloadscene : MonoBehaviour
{
    public CharacterEditor charedit;
    public CharacterBuilder character;
    public UserSetup userSetup;
    void Start()
    {
        GameManager.instance.StartQuest();
        SceneManager.UnloadSceneAsync(1);
        GameManager.instance.LoadCharacter();
        GameManager.instance.SaveCharInfo(GameManager.instance.UserID, charedit.playerName.text);
        GameManager.instance.UIExplore.SetActive(true);
        character.CombineHeadAndHairSprites();
        userSetup.IntroCanvas.SetActive(false);
        
        GameManager.instance.scene.manualLoading();
       
        //await Task.Delay(1000);
        //QuestManager.Instance.ForExistingUsers();
        //GameManager.instance.StartQuest();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
