using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    //*****Copyright MAPLELEAF3659*****
    GameModel model;
    GameView view;

    [Header("Sounds")]
    public SoundManager soundManager;
    public AudioClip btnClickAudio;
    public AudioClip confirmBtnClickAudio;
    public AudioClip adClickAudio;
    public AudioClip upgradeBtnAudio;
    public AudioClip resultAudio;
    public AudioClip dayChangeAudio;

    void Start()
    {
        model = GetComponent<GameModel>();
        view = GetComponent<GameView>();
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            SceneManager.LoadScene("start ");
        }
    }

    public void OnCameraBtnNextClicked()
    {
        soundManager.CreateSound(btnClickAudio);
        if(model.currentCamPos == 1)
        {
            model.ChangeCamPos(3, true, 0f);
            return;
        }
        if (model.currentCamPos < view.camScreen.Length - 1)
            model.ChangeCamPos(model.currentCamPos + 1, true,0f);
        else
            model.ChangeCamPos(0, true, 0f);
    }

    public void OnCameraBtnPreviousClicked()
    {
        soundManager.CreateSound(btnClickAudio);
        if (model.currentCamPos == 3)
        {
            model.ChangeCamPos(1, true, 0f);
            return;
        }
        if (model.currentCamPos > 0)
            model.ChangeCamPos(model.currentCamPos - 1, true, 0f);
        else
            model.ChangeCamPos(view.camScreen.Length - 1, true, 0f);
    }

    public void WorldItemOnClicked(string methodName)
    {
        soundManager.CreateSound(btnClickAudio);
        Invoke(methodName, 0f);
    }

    public void OnComputerBtnClicked()
    {

        model.ChangeCamPos(2, true, 0f);
    }

    //public void OnADNextBtnClicked()
    //{
    //    if (model.currentADPos < view.adSprite.Length - 1)
    //    {
    //        model.ChangeCurrentAD(model.currentADPos + 1);
    //    }
    //    else
    //    {
    //        model.ChangeCurrentAD(0);
    //    }
    //}

    //public void OnADPreviousBtnClicked()
    //{
    //    if (model.currentADPos > 0)
    //    {
    //        model.ChangeCurrentAD(model.currentADPos - 1);
    //    }
    //    else
    //    {
    //        model.ChangeCurrentAD(view.adSprite.Length - 1);
    //    }
    //}

    public void OnDayCounterConfirmedBtnClicked()
    {
        soundManager.CreateSound(confirmBtnClickAudio);
        model.ChangeDay();
    }

    public void OnADClicked(int index)
    {
        soundManager.CreateSound(adClickAudio);
        view.CreateFollowingAD(Input.mousePosition, view.adInSelection[index].sprite, model.currentADNum[index]);
    }

    public void OnUpgradeBtnClicked(int index)
    {
        soundManager.CreateSound(upgradeBtnAudio);
        model.Upgrade(index);
    }
}
