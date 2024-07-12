using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeanTweenAnimate : MonoBehaviour
{
    public GameObject  RenamePanel, CLI1, CLI2, CLI3, BIOS, LOADING, loadingcircle,Install, Installing, chck1, chck2, chck3, chck4,chck5, rename, teleanim, circling, successpanel, PCimage, PCname, successlbl, btnclose;
    public TMP_Text Plusexp;
    bool open = true;
    //bool close = false;


    void Start()
    {
        //LeanTween.scale(playBTN, new Vector3(1f, 1f, 1f), 2f).setDelay(.5f).setEase(LeanTweenType.easeOutElastic);
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
    }

    public void Circling()
    {
        LeanTween.alpha(circling.GetComponent<RectTransform>(), 1f, .8f).setDelay(1f);
        LeanTween.rotateAround(circling, Vector3.forward, -360, 5f).setLoopClamp();
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
}
