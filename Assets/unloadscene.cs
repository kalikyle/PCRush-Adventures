using Assets.PixelHeroes.Scripts.CharacterScrips;
using Assets.PixelHeroes.Scripts.EditorScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class unloadscene : MonoBehaviour
{
    public CharacterEditor charedit;
    public CharacterBuilder character;
    public UserSetup userSetup;
    void Start()
    {
        SceneManager.UnloadSceneAsync(1);
        GameManager.instance.LoadCharacter();
        GameManager.instance.SaveCharInfo(GameManager.instance.UserID, charedit.playerName.text);
        GameManager.instance.UIExplore.SetActive(true);
        character.CombineHeadAndHairSprites();
        userSetup.IntroCanvas.SetActive(false);

        GameManager.instance.scene.manualLoading();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
