using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LeanTweenAnimate : MonoBehaviour
{
    public GameObject RenamePanel, CLI1, CLI2, CLI3, BIOS, LOADING, loadingcircle, Install, Installing, chck1, chck2, chck3, chck4, chck5, rename, teleanim, circling, successpanel, PCimage, PCname, successlbl, btnclose, circling2, hordefinishpanl, coinscollectedtxt, materialscollecttxt, expcollectedtxt, hordeImage, horderfinish, btnnice, GameMenu, GameMap, Diedpanel, youdiedText, diedtextsub, skull
        , MinimapHomeWorld, MinimapCPUWorld, MinimapRAMWorld, MinimapCPUFWorld, MinimapGPUWorld, MinimapSTORAGEWorld, MinimapPSUWorld, MinimapMBWorld, MinimapCASEWorld, TalkBTN, blackBack, Twirl, characterImage, cputwirl, ramtwirl, cpufantwirl, gputwirl, storagetwirl, psutwirl, mbtwirl, casetwirl;
    public TMP_Text Plusexp, hordeworld, hordenum, coinscollected, materialscollect , expcollected, showkills;
    bool open = true;
    public bool HordeDone = false;
    public UIImageAnimation imageAnim;
    //bool close = false;


    void Start()
    {
        //LeanTween.scale(playBTN, new Vector3(1f, 1f, 1f), 2f).setDelay(.5f).setEase(LeanTweenType.easeOutElastic);
        cpuTwirl();
        ramTwirl();
        cpufanTwirl();
        gpuTwirl();
        storageTwirl();
        psuTwirl();
        mbTwirl();
        caseTwirl();

    }
    public void RenamePanelOpen()
    {
        RenamePanel.SetActive(open);
    }

    public void CLI1Open()
    {
        CLI1.SetActive(open);
    }

    public void CLI2Open()
    {
        CLI2.SetActive(open);
    }

    public void CLI3Open()
    {
        CLI3.SetActive(open);
    }

    public void BIOSOpen()
    {
        BIOS.SetActive(open);
    }

    public void LOADINGOpen()
    {
        LOADING.SetActive(open);
    }

    public void INSTALLOpen()
    {
        Install.SetActive(open);
    }

    public void installingOpen()
    {
        Installing.SetActive(open);
    }

    public void chck1Open()
    {
        chck1.SetActive(open);
    }

    public void chck2Open()
    {
        chck2.SetActive(open);
    }

    public void chck3Open()
    {
        chck3.SetActive(open);
    }

    public void chck4Open()
    {
        chck4.SetActive(open);
    }

    public void chck5Open()
    {
        chck5.SetActive(open);
    }

    public void renameOpen()
    {
        rename.SetActive(open);
    }

    public void TeleClose()
    {
        teleanim.SetActive(false);
    }

    public void OpenTeleAnim()
    {
        //teleanim.SetActive(open);
        //LeanTween.scale(teleanim, new Vector3(41f, 41f, 41f), .5f);
        LeanTween.scale(teleanim, new Vector3(1f, 1f, 1f), .08f).setEaseInOutSine().setOnComplete(CloseTeleAnim);
        
    }

    public void OpenGameMenu()
    {
        LeanTween.scale(GameMenu, new Vector3(0.5f, 0.5f, 0.5f), .2f).setEaseInOutSine();

    }
    public void HideGameMenu()
    {
        LeanTween.scale(GameMenu, new Vector3(0f, 0f, 0f), .08f).setEaseInOutSine();

    }
    public void OpenGameMap()
    {
        LeanTween.scale(GameMap, new Vector3(0.8f, 0.8f, 0.8f), .2f).setEaseInOutSine();
        imageAnim.Func_PlayUIAnim();

    }
    public void HideGameMap()
    {
        LeanTween.scale(GameMap, new Vector3(0f, 0f, 0f), .08f).setEaseInOutSine();
        imageAnim.Func_StopUIAnim();

    }
    public void CloseTeleAnim()
    {
        LeanTween.scale(teleanim, new Vector3(150f, 150f, 150f), .08f).setDelay(1f).setEaseInOutSine();
        //TeleClose();
    }
    public void InstallOS()
    {
        RenamePanelOpen();
        CLI1Open();

        LeanTween.scale(CLI1, new Vector3(1f, 1f, 1f), .5f).setOnComplete(CLI2Open);
        LeanTween.scale(CLI2, new Vector3(1f, 1f, 1f), 1f).setOnComplete(CLI3Open);
        LeanTween.scale(CLI3, new Vector3(1f, 1f, 1f), 1.5f).setOnComplete(BIOSOpen);
        LeanTween.scale(BIOS, new Vector3(1f, 1f, 1f), 3f).setOnComplete(LOADINGOpen);
        LeanTween.rotateAround(loadingcircle, Vector3.forward, 360, 5f).setOnComplete(INSTALLOpen);
    }

    public void InstallingOS()
    {
        installingOpen();
        LeanTween.scale(Install, new Vector3(1f, 1f, 1f), .5f).setOnComplete(chck1Open);
        LeanTween.scale(chck1, new Vector3(1f, 1f, 1f), 1f).setOnComplete(chck2Open);
        LeanTween.scale(chck2, new Vector3(1f, 1f, 1f), 1.5f).setOnComplete(chck3Open);
        LeanTween.scale(chck3, new Vector3(1f, 1f, 1f), 2f).setOnComplete(chck4Open);
        LeanTween.scale(chck4, new Vector3(1f, 1f, 1f), 3f).setOnComplete(chck5Open);
        LeanTween.scale(chck5, new Vector3(1f, 1f, 1f), 3.5f).setOnComplete(renameOpen);

        GameManager.instance.PartsController.OpenStoryDialogues("OnDoneInstall");
    }

    public void Circling()
    {
        LeanTween.alpha(circling.GetComponent<RectTransform>(), 1f, .8f).setDelay(1f);
        LeanTween.rotateAround(circling, Vector3.forward, -360, 5f).setLoopClamp();
    }
    public void Circlingskull()
    {
        LeanTween.alpha(skull.GetComponent<RectTransform>(), 1f, .8f).setDelay(1f);
        LeanTween.rotateAround(skull, Vector3.forward, -360, 5f).setLoopClamp();
    }

    public void ShowSuccessPC()
    {
        Circling();
        LeanTween.scale(successpanel, new Vector3(1f, 1f, 1f), 1f).setEase(LeanTweenType.easeOutBounce);
        LeanTween.scale(successlbl, new Vector3(1.5f, 1.5f, 1.5f), 1f).setDelay(.5f).setEase(LeanTweenType.easeOutBounce);
        LeanTween.moveLocal(successlbl, new Vector3(6.18f, 142.55f, 0f), .7f).setDelay(2f).setEase(LeanTweenType.easeInCubic);
        LeanTween.scale(successlbl, new Vector3(1f, 1f, 1f), 1f).setDelay(.5f).setEase(LeanTweenType.easeInCubic).setOnComplete(ShowSuccess);

    }

    public void ShowSuccess()
    {

        LeanTween.alpha(PCimage.GetComponent<RectTransform>(), 1f, .8f).setDelay(1f);
        LeanTween.scale(PCname, new Vector3(1f, 1f, 1f), .8f).setDelay(1f).setEase(LeanTweenType.easeOutElastic);
        btnclose.gameObject.SetActive(true);
       
        LeanTween.moveLocal(btnclose, new Vector3(284.67f, -150.3f, 0f), .7f).setDelay(1f).setEase(LeanTweenType.easeInCubic);

    }

    public void HideSuccess()
    {
        btnclose.gameObject.SetActive(false);
        LeanTween.scale(successpanel, new Vector3(0f, 0f, 0f), 1f).setEase(LeanTweenType.easeOutQuint);
        LeanTween.scale(successlbl, new Vector3(0f, 0f, 0f), 1f).setDelay(.5f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.moveLocal(successlbl, new Vector3(6.18f, 2f, 0f), .7f).setDelay(2f).setEase(LeanTweenType.easeInCubic);
        LeanTween.alpha(PCimage.GetComponent<RectTransform>(), 0f, .8f).setDelay(.5f);
        LeanTween.scale(PCname, new Vector3(0f, 0f, 0f), 1f).setDelay(.5f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.alpha(circling.GetComponent<RectTransform>(), 0f, .8f).setDelay(1f);
        LeanTween.moveLocal(btnclose, new Vector3(504f, -150.3f, 0f), .7f).setDelay(2f).setEase(LeanTweenType.easeInCubic);
        Plusexp.gameObject.SetActive(false);

    }
    public void Hides()
    {
        successpanel.gameObject.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        
    }


    public void YouDied()
    {
        Circlingskull();
        LeanTween.scale(Diedpanel, new Vector3(1f, 1f, 1f), .8f).setEase(LeanTweenType.easeOutElastic);

        LeanTween.scale(youdiedText, new Vector3(3f, 3f, 3f), 1f).setDelay(.5f).setEase(LeanTweenType.easeOutBounce);
        LeanTween.moveLocal(youdiedText, new Vector3(0f, 133f, 0f), .7f).setDelay(1f).setEase(LeanTweenType.easeInCubic);
        LeanTween.scale(youdiedText, new Vector3(1f, 1f, 1f), 1f).setDelay(.5f).setEase(LeanTweenType.easeInCubic);


        LeanTween.moveLocal(diedtextsub, new Vector3(0f, -162f, 0f), .7f).setEase(LeanTweenType.easeInCubic).setOnComplete(HideYouDied);
    }
    public void HideYouDied()
    {
        LeanTween.scale(Diedpanel, new Vector3(0f, 0f, 0f), .8f).setDelay(4f).setEase(LeanTweenType.easeOutElastic);


        LeanTween.moveLocal(youdiedText, new Vector3(0f, 4f, 0f), .7f).setDelay(4f).setEase(LeanTweenType.easeInCubic);
        LeanTween.scale(youdiedText, new Vector3(0f, 0f, 0f), 1f).setDelay(4f).setEase(LeanTweenType.easeOutBounce);


        LeanTween.moveLocal(diedtextsub, new Vector3(0f, -268f, 0f), .7f).setDelay(4f).setEase(LeanTweenType.easeInCubic);
    }
   
    // animation for finish horde
    public void HordeFinish()
    {
        HordeDone = true;
        Circling2();
        LeanTween.scale(hordefinishpanl, new Vector3(1f, 1f, 1f), .8f).setEase(LeanTweenType.easeOutElastic);


        LeanTween.scale(hordeImage, new Vector3(10f, 10f, 10f), 1f).setDelay(.5f).setEase(LeanTweenType.easeOutBounce);
        LeanTween.moveLocal(hordeImage, new Vector3(0f, 134.7f, 0f), .7f).setDelay(2f).setEase(LeanTweenType.easeInCubic);
        LeanTween.scale(hordeImage, new Vector3(1f, 1f, 1f), 1f).setDelay(.5f).setEase(LeanTweenType.easeInCubic).setOnComplete(showLabels);

    }

    public void Circling2()
    {
        LeanTween.alpha(circling2.GetComponent<RectTransform>(), 1f, .8f).setDelay(1f);
        LeanTween.rotateAround(circling2, Vector3.forward, -360, 5f).setLoopClamp();
    }

    public void showLabels()
    {

        LeanTween.scale(horderfinish, new Vector3(1f, 1f, 1f), 1f).setDelay(.5f).setEase(LeanTweenType.easeOutBounce);
        LeanTween.scale(hordeworld.gameObject, new Vector3(1f, 1f, 1f), 1f).setDelay(1f).setEase(LeanTweenType.easeOutBounce);
        LeanTween.scale(hordenum.gameObject, new Vector3(1f, 1f, 1f), 1f).setDelay(1.5f).setEase(LeanTweenType.easeOutBounce).setOnComplete(showCollected);
       
    }
    public void showCollected() {

        LeanTween.moveLocal(coinscollectedtxt, new Vector3(-24.4f, -28f, 0f), .7f).setDelay(.5f).setEase(LeanTweenType.easeInCubic);
        LeanTween.moveLocal(coinscollected.gameObject, new Vector3(88.9f, -12.9f, 0f), .7f).setDelay(.5f).setEase(LeanTweenType.easeInCubic);

        LeanTween.moveLocal(materialscollecttxt, new Vector3(-24.4f, -68f, 0f), .7f).setDelay(1.5f).setEase(LeanTweenType.easeInCubic);
        LeanTween.moveLocal(materialscollect.gameObject, new Vector3(106.6f, -56.6f, 0f), .7f).setDelay(1.5f).setEase(LeanTweenType.easeInCubic);

        LeanTween.moveLocal(expcollectedtxt, new Vector3(-24.4f, -112.7f, 0f), .7f).setDelay(2f).setEase(LeanTweenType.easeInCubic);
        LeanTween.moveLocal(expcollected.gameObject, new Vector3(130f, -103f, 0f), .7f).setDelay(2f).setEase(LeanTweenType.easeInCubic).setOnComplete(showKills);
    }

    public void showKills()
    {
        LeanTween.moveLocal(showkills.gameObject, new Vector3(252.9f, -96f, 0f), .7f).setDelay(.5f).setEase(LeanTweenType.easeInCubic).setOnComplete(niceButton);
    }
    public void niceButton()
    {
        LeanTween.scale(btnnice, new Vector3(1f, 1f, 1f), 2f).setDelay(.5f).setEase(LeanTweenType.easeOutElastic);
    }

    public void HideFinishHorde()
    {
        HordeDone = false;
        LeanTween.scale(btnnice, new Vector3(0f, 0f, 0f), 2f).setDelay(.5f).setEase(LeanTweenType.easeOutElastic);

        LeanTween.scale(hordefinishpanl, new Vector3(0f, 0f, 0f), 0f).setEase(LeanTweenType.easeOutBounce);
        LeanTween.scale(hordeImage, new Vector3(1f, 1f, 1f), 1f).setDelay(.5f).setEase(LeanTweenType.easeInCubic);
        LeanTween.moveLocal(hordeImage.gameObject, new Vector3(0f, -2f, 0f), .7f).setDelay(2f).setEase(LeanTweenType.easeInCubic);

        LeanTween.scale(horderfinish, new Vector3(0f, 0f, 0f), 1f).setDelay(1f).setEase(LeanTweenType.easeOutBounce);
        LeanTween.scale(hordeworld.gameObject, new Vector3(0f, 0f, 0f), 1f).setDelay(1.5f).setEase(LeanTweenType.easeOutBounce);
        LeanTween.scale(hordenum.gameObject, new Vector3(0f, 0f, 0f), 1f).setDelay(2f).setEase(LeanTweenType.easeOutBounce);

        LeanTween.moveLocal(coinscollectedtxt, new Vector3(-465f, -28f, 0f), .7f).setDelay(1f).setEase(LeanTweenType.easeInCubic);
        LeanTween.moveLocal(coinscollected.gameObject, new Vector3(434f, -12.9f, 0f), .7f).setDelay(1f).setEase(LeanTweenType.easeInCubic);

        LeanTween.moveLocal(materialscollecttxt, new Vector3(-522f, -68f, 0f), .7f).setDelay(1f).setEase(LeanTweenType.easeInCubic);
        LeanTween.moveLocal(materialscollect.gameObject, new Vector3(449f, -56.6f, 0f), .7f).setDelay(1f).setEase(LeanTweenType.easeInCubic);

        LeanTween.moveLocal(expcollectedtxt, new Vector3(-395f, -112.7f, 0f), .7f).setDelay(1f).setEase(LeanTweenType.easeInCubic);
        LeanTween.moveLocal(expcollected.gameObject, new Vector3(474f, -103f, 0f), .7f).setDelay(1f).setEase(LeanTweenType.easeInCubic);

        LeanTween.moveLocal(showkills.gameObject, new Vector3(252.9f, -257f, 0f), .7f).setDelay(1f).setEase(LeanTweenType.easeInCubic);

    }
    //OLD new Vector3(1.361929f, 1.361929f, 1.361929f)
    public void HomeWorldMinimapOpen()
    {
        LeanTween.scale(MinimapHomeWorld.gameObject, new Vector3(1f, 1f, 1f), .2f).setEase(LeanTweenType.easeInCubic);
        GameManager.instance.MinimapOpened = true;
    }
    public void HomeWorldMinimapClose()
    {
        LeanTween.scale(MinimapHomeWorld.gameObject, new Vector3(0f, 0f, 0f), .2f).setEase(LeanTweenType.easeInCubic);
        GameManager.instance.MinimapOpened = false;
    }
    public void CPUMinimapOpen()
    {
        LeanTween.scale(MinimapCPUWorld.gameObject, new Vector3(1f, 1f, 1f), .2f).setEase(LeanTweenType.easeInCubic);
        GameManager.instance.MinimapOpened = true;
    }
    public void CPUMinimapClose()
    {
        LeanTween.scale(MinimapCPUWorld.gameObject, new Vector3(0f, 0f, 0f), .2f).setEase(LeanTweenType.easeInCubic);
        GameManager.instance.MinimapOpened = false;
    }
    public void RAMMinimapOpen()
    {
        LeanTween.scale(MinimapRAMWorld.gameObject, new Vector3(1f, 1f, 1f), .2f).setEase(LeanTweenType.easeInCubic);
        GameManager.instance.MinimapOpened = true;
    }
    public void RAMMinimapClose()
    {
        LeanTween.scale(MinimapRAMWorld.gameObject, new Vector3(0f, 0f, 0f), .2f).setEase(LeanTweenType.easeInCubic);
        GameManager.instance.MinimapOpened = false;
    }
    public void CPUFMinimapOpen()
    {
        LeanTween.scale(MinimapCPUFWorld.gameObject, new Vector3(1f, 1f, 1f), .2f).setEase(LeanTweenType.easeInCubic);
        GameManager.instance.MinimapOpened = true;
    }
    public void CPUFMinimapClose()
    {
        LeanTween.scale(MinimapCPUFWorld.gameObject, new Vector3(0f, 0f, 0f), .2f).setEase(LeanTweenType.easeInCubic);
        GameManager.instance.MinimapOpened = false;
    }
    public void GPUMinimapOpen()
    {
        LeanTween.scale(MinimapGPUWorld.gameObject, new Vector3(1f, 1f, 1f), .2f).setEase(LeanTweenType.easeInCubic);
        GameManager.instance.MinimapOpened = true;
    }
    public void GPUMinimapClose()
    {
        LeanTween.scale(MinimapGPUWorld.gameObject, new Vector3(0f, 0f, 0f), .2f).setEase(LeanTweenType.easeInCubic);
        GameManager.instance.MinimapOpened = false;
    }
    public void STORAGEMinimapOpen()
    {
        LeanTween.scale(MinimapSTORAGEWorld.gameObject, new Vector3(1f, 1f, 1f), .2f).setEase(LeanTweenType.easeInCubic);
        GameManager.instance.MinimapOpened = true;
    }
    public void STORAGEMinimapClose()
    {
        LeanTween.scale(MinimapSTORAGEWorld.gameObject, new Vector3(0f, 0f, 0f), .2f).setEase(LeanTweenType.easeInCubic);
        GameManager.instance.MinimapOpened = false;
    }
    public void PSUMinimapOpen()
    {
        LeanTween.scale(MinimapPSUWorld.gameObject, new Vector3(1f, 1f, 1f), .2f).setEase(LeanTweenType.easeInCubic);
        GameManager.instance.MinimapOpened = true;
    }
    public void PSUMinimapClose()
    {
        LeanTween.scale(MinimapPSUWorld.gameObject, new Vector3(0f, 0f, 0f), .2f).setEase(LeanTweenType.easeInCubic);
        GameManager.instance.MinimapOpened = false;
    }
    public void MBMinimapOpen()
    {
        LeanTween.scale(MinimapMBWorld.gameObject, new Vector3(1f, 1f, 1f), .2f).setEase(LeanTweenType.easeInCubic);
        GameManager.instance.MinimapOpened = true;
    }
    public void MBMinimapClose()
    {
        LeanTween.scale(MinimapMBWorld.gameObject, new Vector3(0f, 0f, 0f), .2f).setEase(LeanTweenType.easeInCubic);
        GameManager.instance.MinimapOpened = false;
    }
    public void CASEMinimapOpen()
    {
        LeanTween.scale(MinimapCASEWorld.gameObject, new Vector3(1f, 1f, 1f), .2f).setEase(LeanTweenType.easeInCubic);
        GameManager.instance.MinimapOpened = true;
    }
    public void CASEMinimapClose()
    {
        LeanTween.scale(MinimapCASEWorld.gameObject, new Vector3(0f, 0f, 0f), .2f).setEase(LeanTweenType.easeInCubic);
        GameManager.instance.MinimapOpened = false;
    }

    public void OpenTalkBTN()
    {
        LeanTween.scale(TalkBTN.gameObject, new Vector3(0.7525f, 0.7525f, 0.7525f), .1f).setEase(LeanTweenType.easeOutElastic);
    }
    public void CloseTalkBTN()
    {
        LeanTween.scale(TalkBTN.gameObject, new Vector3(0f, 0f, 0f), .1f).setEase(LeanTweenType.easeOutElastic);
    }

    public void Onteleport()
    {
        characterImage.GetComponent<Image>().sprite = GameManager.instance.PlayerImage.sprite; 

        GameManager.instance.CutScene7.SetActive(true);
        LeanTween.alpha(blackBack.GetComponent<RectTransform>(), 1f, .2f);
        LeanTween.scale(characterImage, new Vector3(1f, 1f, 1f), 2f).setDelay(.2f).setEase(LeanTweenType.easeOutCubic);
        LeanTween.alpha(characterImage.GetComponent<RectTransform>(), 1f, .8f).setDelay(1f);
        LeanTween.scale(Twirl, new Vector3(1f, 1f, 1f), .1f).setEase(LeanTweenType.easeOutElastic).setOnComplete(OnTeleportClose);
        LeanTween.rotateAround(Twirl, Vector3.forward, -360, 5f).setLoopClamp();
        LeanTween.rotateAround(characterImage, Vector3.forward, -360, 5f).setLoopClamp();

        DialogueManager.GetInstance().EnterDialogueMode(GameManager.instance.MainStory);
        DialogueManager.GetInstance().TriggerSection("TwelveTeleporting");
    }

    public void OnTeleportClose()
    {
        LeanTween.scale(Twirl, new Vector3(0f, 0f, 0f), .1f).setDelay(8f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.scale(characterImage, new Vector3(0f, 0f, 0f),2f).setDelay(7f).setEase(LeanTweenType.easeOutCubic);
        LeanTween.alpha(blackBack.GetComponent<RectTransform>(), 0f, .8f).setDelay(8f).setOnComplete(HideCutScene7);
        

    }

    public void HideCutScene7()
    {
        GameManager.instance.CutScene7.SetActive(false);
        GameManager.instance.OnCutScene7Open = true;
    }
    
    public void OpenCPUTwirl()
    {
        
        LeanTween.scale(cputwirl, new Vector3(0.4044699f, 0.4044699f, 0.4044699f), 1.5f).setDelay(1f).setEase(LeanTweenType.easeOutCubic);
    }
    public void CloseCPUTwirl()
    {
        LeanTween.scale(cputwirl, new Vector3(0f, 0f, 0f), 1.5f).setDelay(1f).setEase(LeanTweenType.easeInCubic);
        
    }
    public void cpuTwirl()
    {
        LeanTween.rotateAround(cputwirl, Vector3.forward, -360, 5f).setLoopClamp();
    }

    public void ramTwirl()
    {
        LeanTween.rotateAround(ramtwirl, Vector3.forward, -360, 5f).setLoopClamp();
    }

    public void cpufanTwirl()
    {
        LeanTween.rotateAround(cpufantwirl, Vector3.forward, -360, 5f).setLoopClamp();
    }
    public void gpuTwirl()
    {
        LeanTween.rotateAround(gputwirl, Vector3.forward, -360, 5f).setLoopClamp();
    }
    public void storageTwirl()
    {
        LeanTween.rotateAround(storagetwirl, Vector3.forward, -360, 5f).setLoopClamp();
    }
    public void psuTwirl()
    {
        LeanTween.rotateAround(psutwirl, Vector3.forward, -360, 5f).setLoopClamp();
    }
    public void mbTwirl()
    {
        LeanTween.rotateAround(mbtwirl, Vector3.forward, -360, 5f).setLoopClamp();
    }
    public void caseTwirl()
    {
        LeanTween.rotateAround(casetwirl, Vector3.forward, -360, 5f).setLoopClamp();
    }


    public GameObject LevelUpUI, LevelUpCircle, LevelText, LevelUpText, NiceBTN, YouUnlockPartsText, MapImage, MapText, KeepUpText,addedstats;


    public void PlayLevelUp()
    {
        // Reset the objects before starting the sequence
        //ResetLevelUpAnimations();
        LevelUpUI.SetActive(true);
        // Spin the LevelUpCircle and set alpha to 1
        LeanTween.rotateAround(LevelUpCircle, Vector3.forward, -360, 5f).setLoopClamp();
        LeanTween.alpha(LevelUpCircle.GetComponent<RectTransform>(), 1f, 0.5f);

        // Show LevelText with scale animation (scale from 0 to 1)
        LeanTween.scale(LevelText, new Vector3(1.1f, 1.1f, 1.1f), 0.5f).setEase(LeanTweenType.easeOutElastic).setDelay(0.5f);

        // Show LevelUpText with scale animation (elastic out)
        LeanTween.scale(LevelUpText, new Vector3(1.4f, 1.4f, 1.1f), 0.7f).setEase(LeanTweenType.easeOutElastic).setDelay(1f);

        // Show YouUnlockPartsText with scale animation
        LeanTween.scale(YouUnlockPartsText, new Vector3(1.1f, 1.1f, 1.1f), 0.5f).setEase(LeanTweenType.easeOutCubic).setDelay(1.5f);
        LeanTween.scale(addedstats, new Vector3(1.1f, 1.1f, 1.1f), 0.5f).setEase(LeanTweenType.easeOutCubic).setDelay(1.5f);

        // Fade in MapImage and show MapText with scale animation
        LeanTween.alpha(MapImage.GetComponent<RectTransform>(), 1f, 0.5f).setDelay(2f);
        LeanTween.scale(MapText, new Vector3(0.5f, 0.5f, 0.5f), 0.5f).setEase(LeanTweenType.easeOutCubic).setDelay(2f);

        // Show KeepUpText with scale animation
        LeanTween.scale(KeepUpText, new Vector3(1.8f, 1.8f, 1.8f), 0.5f).setEase(LeanTweenType.easeOutCubic).setDelay(2.5f);

        // Animate NiceBTN with elastic out
        LeanTween.scale(NiceBTN, new Vector3(1f, 1f, 1f), 0.7f).setEase(LeanTweenType.easeOutElastic).setDelay(3f);
    }

    public void ResetLevelUpAnimations()
    {
        // Reset scales and alpha values
        LeanTween.alpha(LevelUpCircle.GetComponent<RectTransform>(), 0f, 0.5f);
        //LeanTween.cancel(LevelUpCircle);

        LevelText.transform.localScale = Vector3.zero;
        LevelUpText.transform.localScale = Vector3.zero;
        YouUnlockPartsText.transform.localScale = Vector3.zero;
        addedstats.transform.localScale = Vector3.zero;
        LeanTween.alpha(MapImage.GetComponent<RectTransform>(), 0f, 0.5f);
        MapText.transform.localScale = Vector3.zero;
        KeepUpText.transform.localScale = Vector3.zero;
        NiceBTN.transform.localScale = Vector3.zero;


        MapImage.SetActive(false);
        LevelUpUI.SetActive(false);
    }

    public GameObject CongratsPanel, circling3, ComputerSoldText, Comparison, comparisonText, BuyerImage, PCImage, Feedback, Total, PCPanel, Reward, Buyer, ThanksBTN;

    public void PlaySoldPCAnimation()
    {

        LeanTween.scale(CongratsPanel, new Vector3(1f, 1f, 1f), 0.5f).setEase(LeanTweenType.easeOutCubic);
        // circling3 spins continuously
        LeanTween.rotateAround(circling3, Vector3.forward, 360f, 5f).setLoopClamp();

        // ComputerSoldText first scales to 2 in the middle, then moves to the top
        LeanTween.scale(ComputerSoldText, new Vector3(1.5f, 1.5f, 1.5f), 1f).setEase(LeanTweenType.easeOutCubic).setOnComplete(() => {

            LeanTween.scale(ComputerSoldText, new Vector3(1f, 1f, 1f), 0.5f).setEase(LeanTweenType.easeOutCubic);

                // After ComputerSoldText moves to the top, Comparison fades in
                LeanTween.alpha(Comparison.GetComponent<RectTransform>(), 1f, 0.5f).setDelay(0.2f);

                // Then comparisonText scales in with an elastic effect
                LeanTween.scale(comparisonText, Vector3.one, 0.7f).setEase(LeanTweenType.easeOutElastic).setDelay(0.7f);

                // BuyerImage and PCImage fade in at the same time
                LeanTween.alpha(BuyerImage.GetComponent<RectTransform>(), 1f, 0.5f).setDelay(1.2f);
                LeanTween.alpha(PCImage.GetComponent<RectTransform>(), 1f, 0.5f).setDelay(1.2f);

                // Feedback scales in
                LeanTween.scale(Feedback, Vector3.one, 0.5f).setEase(LeanTweenType.easeOutCubic).setDelay(1.7f);

                // Total scales in with an elastic effect
                LeanTween.scale(Total, Vector3.one, 0.7f).setEase(LeanTweenType.easeOutElastic).setDelay(2.2f);

                // PCPanel scales in with an elastic effect
                LeanTween.scale(Buyer, new Vector3(0.8f, 0.8f, 0.8f), 0.7f).setEase(LeanTweenType.easeOutElastic).setDelay(2.7f);

                // Reward scales in with an elastic effect
                LeanTween.scale(Reward, new Vector3(0.8f, 0.8f, 0.8f), 0.7f).setEase(LeanTweenType.easeOutElastic).setDelay(3.2f);

                // Buyer scales in with an elastic effect
                LeanTween.scale(PCPanel, new Vector3(0.8f, 0.8f, 0.8f), 0.7f).setEase(LeanTweenType.easeOutElastic).setDelay(3.7f);

                // ThanksBTN scales in last with an elastic effect
                LeanTween.scale(ThanksBTN, Vector3.one, 0.7f).setEase(LeanTweenType.easeOutElastic).setDelay(4.2f);
            });
    }

    public void ResetSoldPCAnimation()
    {
        // Reset all the game objects' scales to zero and alphas to 0
        LeanTween.scale(CongratsPanel, new Vector3(0f, 0f, 0f), 0.5f).setEase(LeanTweenType.easeOutCubic);
        ComputerSoldText.transform.localScale = Vector3.zero;
        LeanTween.alpha(Comparison.GetComponent<RectTransform>(), 0f, 0.5f);
        comparisonText.transform.localScale = Vector3.zero;
        LeanTween.alpha(BuyerImage.GetComponent<RectTransform>(), 0f, 0.5f);
        LeanTween.alpha(PCImage.GetComponent<RectTransform>(), 0f, 0.5f);
        Feedback.transform.localScale = Vector3.zero;
        Total.transform.localScale = Vector3.zero;
        PCPanel.transform.localScale = Vector3.zero;
        Reward.transform.localScale = Vector3.zero;
        Buyer.transform.localScale = Vector3.zero;
        ThanksBTN.transform.localScale = Vector3.zero;

        // Optional: Reset ComputerSoldText position to its starting middle position (if necessary)
        //ComputerSoldText.transform.localPosition = Vector3.zero;  // This assumes the middle is the origin (0,0,0)
    }



}
