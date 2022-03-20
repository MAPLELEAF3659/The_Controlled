using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ADSelctionManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    //*****Copyright MAPLELEAF3659*****
    GameModel model;
    public InteracionText interactionText;
    public int index;

    void Start()
    {
        model = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameModel>();
        //interactionText = GameObject.FindGameObjectWithTag("interactionText").GetComponent<InteracionText>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        interactionText.ShowText(model.adInfos[model.currentADNum[index]].isVideo?"影片/新聞":"廣告", model.adInfos[model.currentADNum[index]].name);
        //if (model.adInfos[model.currentADNum[index]].isVideo)
        //{
        //    interactionText.ShowText("影片：可控制目標思想");
        //}
        //else if (!model.adInfos[model.currentADNum[index]].isVideo)
        //{
        //    interactionText.ShowText("廣告：依照當前目標性格給予獎勵");
        //}
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        interactionText.HideText();
    }
}
