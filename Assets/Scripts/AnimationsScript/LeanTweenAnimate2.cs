using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class LeanTweenAnimate2 : MonoBehaviour
{
    public GameObject playBTN, mainmenu, buildscene, homescene, ordersscene, circling, successpanel, PCimage, PCname, successlbl, btntest, CongratsPanel, PCpanel, pricepanel, clientPanel, congratslbl, circling2, pcimage, coinimage, clientimage, closeBTn, shopscene, LevelUpScene, _2exp,Computation, glossary, glossarypanel
                      ,levelcircling1, levelcircling2, lvlUplbl, lvlnum, newshoplbl, itempanel,scrollingbackground;
    public TMP_Text clear;
    void Start()
    {
        if (playBTN != null)
        {
            LeanTween.scale(playBTN, new Vector3(1f, 1f, 1f), 2f).setDelay(.5f).setEase(LeanTweenType.easeOutElastic);
        }
        //gameobject        //the size you want   //delay //and animation to be use

    }
    public void hideMainMenu()
    {
        //LeanTween.scale(mainmenu, new Vector3(0f, 0f, 0f), 1f).setEase(LeanTweenType.easeInElastic);
        LeanTween.scale(playBTN, new Vector3(0f, 0f, 0f), 2f).setDelay(.5f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.move(mainmenu, new Vector3(-19.3f, -0.06f, 0f), 1f).setEase(LeanTweenType.easeOutBounce).setOnComplete(disableMenu);
        LeanTween.scale(scrollingbackground, new Vector3(0f, 0f, 0f), .5f).setDelay(1f).setEase(LeanTweenType.easeOutExpo).setOnComplete(ScrollingDisable);

    }
    public void disableMenu()
    {
        mainmenu.SetActive(false);
    }
    public void enableMenu()
    {
        mainmenu.SetActive(true);
    }

    public void showMainMenu()
    {
        //LeanTween.scale(mainmenu, new Vector3(0f, 0f, 0f), 1f).setEase(LeanTweenType.easeInElastic);
        scrollingbackground.SetActive(true);
        LeanTween.scale(scrollingbackground, new Vector3(1.2025f, 1f, 1f),0f).setDelay(.5f).setEase(LeanTweenType.easeOutExpo);
        LeanTween.scale(playBTN, new Vector3(1f, 1f, 1f), 2f).setDelay(.5f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.move(mainmenu, new Vector3(0.12f, -0.06f, 0f), 1f).setEase(LeanTweenType.easeOutBounce).setOnComplete(enableMenu);
        

    }
    public void ScrollingDisable()
    {
        scrollingbackground.SetActive(false);
    }
   


    public void showBuild()
    {
        LeanTween.move(homescene, new Vector3(-23.11f, -0.05f, 0f), 1f).setEase(LeanTweenType.easeOutExpo);
        LeanTween.move(buildscene, new Vector3(-0.0067f, -0.05f, 0f), 1f).setEase(LeanTweenType.easeOutExpo);
    }

    public void hideBuild()
    {

        LeanTween.move(homescene, new Vector3(-0.0067f, -0.05f, 0f), 1f).setEase(LeanTweenType.easeOutExpo);
        LeanTween.move(buildscene, new Vector3(23.26f, -0.05f, 0f), 1.3f).setEase(LeanTweenType.easeOutExpo);
    }


    public void showOrders()
    {

        LeanTween.move(ordersscene, new Vector3(0f, -0.06f, 0f), 1f).setEase(LeanTweenType.easeOutExpo);


    }

    public void HideOrders()
    {
        LeanTween.move(ordersscene, new Vector3(-19.18f, -10.43f, 0f), 1f).setEase(LeanTweenType.easeOutExpo);


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

        btntest.gameObject.SetActive(true);
        //btnclose.gameObject.SetActive(true);
        LeanTween.moveLocal(btntest, new Vector3(-282.3f, -150.3f, 0f), .7f).setDelay(1f).setEase(LeanTweenType.easeInCubic);
        //LeanTween.moveLocal(btnclose, new Vector3(284.67f, -150.3f, 0f), .7f).setDelay(1f).setEase(LeanTweenType.easeInCubic);

    }

    public void HideSuccess()
    {
        btntest.gameObject.SetActive(false);
        //btnclose.gameObject.SetActive(false);
        LeanTween.scale(successpanel, new Vector3(0f, 0f, 0f), 1f).setEase(LeanTweenType.easeOutQuint).setOnComplete(Hides);
        LeanTween.scale(successlbl, new Vector3(0f, 0f, 0f), 1f).setDelay(.5f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.moveLocal(successlbl, new Vector3(6.18f, 2f, 0f), .7f).setDelay(2f).setEase(LeanTweenType.easeInCubic);
        LeanTween.alpha(PCimage.GetComponent<RectTransform>(), 0f, .8f).setDelay(.5f);
        LeanTween.scale(PCname, new Vector3(0f, 0f, 0f), 1f).setDelay(.5f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.alpha(circling.GetComponent<RectTransform>(), 0f, .8f).setDelay(1f);




        //LeanTween.moveLocal(btnclose, new Vector3(504f, -150.3f, 0f), .7f).setDelay(2f).setEase(LeanTweenType.easeInCubic);
        LeanTween.moveLocal(btntest, new Vector3(-504f, -150.3f, 0f), .7f).setDelay(2f).setEase(LeanTweenType.easeInCubic);
        _2exp.gameObject.SetActive(false);

    }

    public void Hides()
    {
        successpanel.gameObject.SetActive(false);
    }

    public void Circling2()
    {
        LeanTween.alpha(circling2.GetComponent<RectTransform>(), 1f, .8f).setDelay(1f);
        LeanTween.rotateAround(circling2, Vector3.forward, -360, 5f).setLoopClamp();
    }
    public void ShowSubmitPC()
    {
        Circling2();
        LeanTween.scale(CongratsPanel, new Vector3(1f, 1f, 1f), 1f).setEase(LeanTweenType.easeOutBounce);
        LeanTween.scale(congratslbl, new Vector3(1.5f, 1.5f, 1.5f), 1f).setDelay(.5f).setEase(LeanTweenType.easeOutBounce);
        LeanTween.moveLocal(congratslbl, new Vector3(6.18f, 142.55f, 0f), .7f).setDelay(2f).setEase(LeanTweenType.easeInCubic).setOnComplete(showComputation);
        LeanTween.scale(congratslbl, new Vector3(1f, 1f, 1f), 1f).setDelay(.5f).setEase(LeanTweenType.easeInCubic).setOnComplete(ShowSubmit);

    }
    public void showComputation()
    {
        LeanTween.scale(Computation, new Vector3(3.75f, 1.57f, 1.57f), 2f).setDelay(.5f).setEase(LeanTweenType.easeOutElastic);
    }

    public void ShowSubmit()
    {
        LeanTween.scale(PCpanel, new Vector3(.7f, .7f, .7f), 1f).setEase(LeanTweenType.easeOutBounce);
        LeanTween.scale(pricepanel, new Vector3(.7f, .7f, .7f), 1.5f).setEase(LeanTweenType.easeOutBounce);
        LeanTween.scale(clientPanel, new Vector3(.7f, .7f, .7f), 2f).setEase(LeanTweenType.easeOutBounce);

        LeanTween.scale(pcimage, new Vector3(.7f, .7f, .7f), 2f).setEase(LeanTweenType.easeOutExpo);
        LeanTween.scale(coinimage, new Vector3(.7f, .7f, .7f), 2.5f).setEase(LeanTweenType.easeOutExpo);
        LeanTween.scale(clientimage, new Vector3(.7f, .7f, .7f), 3f).setEase(LeanTweenType.easeOutExpo);

        LeanTween.alpha(PCpanel.GetComponent<RectTransform>(), .5f, .8f).setDelay(1f);
        LeanTween.alpha(pricepanel.GetComponent<RectTransform>(), .5f, .8f).setDelay(1.5f);
        LeanTween.alpha(clientPanel.GetComponent<RectTransform>(), .5f, .8f).setDelay(2f);

        LeanTween.scale(closeBTn, new Vector3(1f, 1f, 1f), 2f).setDelay(.5f).setEase(LeanTweenType.easeOutElastic);
    }

    public void HideSubmit()
    {
        LeanTween.scale(PCpanel, new Vector3(0f, 0f, 0f), 1f).setEase(LeanTweenType.easeOutBounce);
        LeanTween.scale(pricepanel, new Vector3(0f, 0f, 0f), 1f).setEase(LeanTweenType.easeOutBounce);
        LeanTween.scale(clientPanel, new Vector3(0f, 0f, 0f), 1f).setEase(LeanTweenType.easeOutBounce);
        LeanTween.scale(Computation, new Vector3(0f, 0f, 0f), 1f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.scale(pcimage, new Vector3(0f, 0f, 0f), 1f).setEase(LeanTweenType.easeOutExpo);
        LeanTween.scale(coinimage, new Vector3(0f, 0f, 0f), 1f).setEase(LeanTweenType.easeOutExpo);
        LeanTween.scale(clientimage, new Vector3(0f, 0f, 0f), 1).setEase(LeanTweenType.easeOutExpo);

        LeanTween.alpha(PCpanel.GetComponent<RectTransform>(), 0f, .8f).setDelay(1f);
        LeanTween.alpha(pricepanel.GetComponent<RectTransform>(), 0f, .8f).setDelay(1f);
        LeanTween.alpha(clientPanel.GetComponent<RectTransform>(), 0f, .8f).setDelay(1f);


        LeanTween.scale(congratslbl, new Vector3(0f, 0f, 0f), 1f).setDelay(.5f).setEase(LeanTweenType.easeOutBounce);
        LeanTween.moveLocal(congratslbl, new Vector3(2.3f, -1f, 0f), .7f).setDelay(2f).setEase(LeanTweenType.easeInCubic);


        LeanTween.scale(CongratsPanel, new Vector3(0f, 0f, 0f), .5f).setDelay(1f).setEase(LeanTweenType.easeOutExpo);

        LeanTween.scale(closeBTn, new Vector3(0f, 0f, 0f), 2f).setDelay(.5f).setEase(LeanTweenType.easeOutElastic);
        clear.text = "";


    }
    public void ShowShop()
    {
        LeanTween.move(shopscene, new Vector3(-0f, -0.01f, 0f), 1f).setEase(LeanTweenType.easeOutExpo);
    }
    public void HideShop()
    {
        LeanTween.move(shopscene, new Vector3(19.18f, -11.28f, 0f), 1f).setEase(LeanTweenType.easeOutExpo);
    }

    public void showLevelUP()
    {
        LeanTween.move(LevelUpScene, new Vector3(0.04f, 0.38f, 0f), 1f).setEase(LeanTweenType.easeOutBounce);
        LeanTween.rotateAround(levelcircling1, Vector3.forward, -360, 5f).setLoopClamp();
        LeanTween.rotateAround(levelcircling2, Vector3.forward, -360, 5f).setLoopClamp();
        LeanTween.scale(lvlUplbl, new Vector3(1.5f, 1.5f, 1.5f), 1f).setDelay(.5f).setEase(LeanTweenType.easeOutBounce);
        LeanTween.moveLocal(lvlUplbl, new Vector3(5.11f, 75.33f, 0f), .7f).setDelay(2f).setEase(LeanTweenType.easeInCubic);
        LeanTween.scale(lvlUplbl, new Vector3(0.8f, 0.8f, 0.8f), 1f).setDelay(.5f).setEase(LeanTweenType.easeInCubic).setOnComplete(showLevelNum);
    }
    public void showLevelNum()
    {
        LeanTween.scale(lvlnum, new Vector3(2f, 2f, 2f), 1f).setDelay(1f).setEase(LeanTweenType.easeOutBounce);
        LeanTween.scale(newshoplbl, new Vector3(1f, 1f, 1f), 1f).setDelay(.5f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.scale(itempanel, new Vector3(0.4f, 0.4f, 0.4f), 1f).setDelay(.5f).setEase(LeanTweenType.easeOutElastic);
    }
    public void HideLevelUP()
    {
        LeanTween.move(LevelUpScene, new Vector3(0.04f, 10.82f, 0f), 1f).setEase(LeanTweenType.easeOutBounce);
        LeanTween.scale(lvlUplbl, new Vector3(0f, 0f, 0f), 1f).setDelay(.5f).setEase(LeanTweenType.easeOutBounce);
        LeanTween.moveLocal(lvlUplbl, new Vector3(5.11f, 7.3f, 0f), .7f).setDelay(2f).setEase(LeanTweenType.easeInCubic);
        LeanTween.scale(lvlnum, new Vector3(0f, 0f, 0f), 1f).setDelay(1f).setEase(LeanTweenType.easeOutBounce);
        LeanTween.scale(newshoplbl, new Vector3(0f, 0f, 0f), 1f).setDelay(.5f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.scale(itempanel, new Vector3(0f, 0f, 0f), 1f).setDelay(.5f).setEase(LeanTweenType.easeOutElastic);
    }
    public void ShowGlossary()
    {
        glossary.SetActive(true);
        LeanTween.scale(glossarypanel, new Vector3(1f, 1f, 1f), 1f).setEase(LeanTweenType.easeOutExpo);
    }
    public void HideGlossary()
    {
        LeanTween.scale(glossarypanel, new Vector3(0f, 0f, 0f), .5f).setEase(LeanTweenType.easeOutCubic).setOnComplete(HideGlo);
       
    }
    public void HideGlo()
    {
        glossary.SetActive(false);
    }


}
