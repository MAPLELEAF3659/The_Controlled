using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldItemManager : MonoBehaviour
{
    //*****Copyright MAPLELEAF3659*****
    GameController controller;
    GameView view;
    public InteracionText interactionText;
    public Animator computerAni;
    public string methodName;
    public string description;

    private void Start()
    {
        controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        view = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameView>();
        interactionText = GameObject.FindGameObjectWithTag("interactionText").GetComponent<InteracionText>();
    }

    private void OnMouseEnter()
    {
        if (!view.isDisplayingResult)
        {
            interactionText.ShowText("提示",description);
            computerAni.SetBool("isHovered", true);
        }
    }

    private void OnMouseExit()
    {
        if (!view.isDisplayingResult)
        {
            interactionText.HideText();
            computerAni.SetBool("isHovered", false);
        }
    }

    private void OnMouseUp()
    {
        if (!view.isDisplayingResult)
        {
            interactionText.HideText();
            controller.WorldItemOnClicked(methodName);
        }
    }
}
