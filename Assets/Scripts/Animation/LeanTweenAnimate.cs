using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeanTweenAnimate : MonoBehaviour
{
    public GameObject RenamePanel, CLI1, CLI2, CLI3, BIOS, LOADING, loadingcircle,Install, Installing, chck1, chck2, chck3, chck4,chck5, rename, teleanim;
    bool open = true;
    //bool close = false;
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



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
