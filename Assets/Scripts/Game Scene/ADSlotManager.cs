using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ADSlotManager : MonoBehaviour
{
    //*****Copyright MAPLELEAF3659*****
    public Animator adSlotAni;
    public SpriteRenderer adSlotSpriteRenderer;
    public InteracionText interactionText;
    public InteracionText floatingInteractionText;
    public AudioClip successAudio;
    public AudioClip coinAudio;
    public Animator motherboardAni;
    GameController controller;
    GameModel model;
    GameView view;

    void Start()
    {
        adSlotAni = GetComponent<Animator>();
        //adSlotSpriteRenderer = GetComponent<SpriteRenderer>();
        model = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameModel>();
        view = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameView>();
        controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        //interactionText = GameObject.FindGameObjectWithTag("interactionText").GetComponent<InteracionText>();
    }
    private void OnMouseOver()
    {
        //Debug.Log("over");
        if (view.isGetAD && Input.GetKeyUp(KeyCode.Mouse0))
        {
            GameObject adForPut = GameObject.FindGameObjectWithTag("ADForPut");
            int index = adForPut.GetComponent<ADForPut>().index;
            StartCoroutine(Play(2f, index));
            ADInfo tempADInfo = model.adInfos[index];
            adSlotSpriteRenderer.sprite = tempADInfo.adSprite;
            model.nextDayItemsIndex.Add(index);
            model.adInfos[index].isPlayed = true;
            Destroy(adForPut);
            if (tempADInfo.isVideo)
            {
                //int[] values = new int[] { tempADInfo.reason, tempADInfo.angry, tempADInfo.concerned };
                controller.soundManager.CreateSound(successAudio);
                floatingInteractionText.ShowJumpText("成功控制目標思想", Camera.main.WorldToScreenPoint(transform.position));
            }
            else if (!tempADInfo.isVideo)
            {
                controller.soundManager.CreateSound(coinAudio);
                floatingInteractionText.ShowJumpText("你賺到了$" + model.calculateIncome(index), Camera.main.WorldToScreenPoint(transform.position));
            }
            view.isGetAD = false;
        }
    }

    IEnumerator Play(float delayTime, int index)
    {
        motherboardAni.SetTrigger("put");
        if (!model.adInfos[index].isVideo)
        {
            for (int i = 0; i < 4; i++)
            {
                view.CreateFlyingMoney(transform.position, model.calculateIncome(index) / 4);
                yield return new WaitForSeconds(Random.Range(0.1f,0.2f));
            }
        }
        adSlotAni.SetTrigger("flash");
        yield return new WaitForSeconds(delayTime);

        if (model.adInfos[index].isVideo)
        {
            model.AddValue(model.adInfos[index].reason, model.adInfos[index].angry, model.adInfos[index].concerned);
        }
        else
        {
            model.AddMoneyByWeight(index);
        }
        model.todayADCount++;
        //yield return new WaitForSeconds(1f);
        //model.ChangeCamPos(0,false);
    }

    string ResultMessage(int[] personalityValues)
    {
        string message = "";
        switch (judge_max(personalityValues))
        {
            case 0:
                message += "理性值++";
                break;
            case 1:
                message += "怒氣值++";
                break;
            case 2:
                message += "憂慮值++";
                break;
        }
        switch (find_negative(personalityValues))
        {
            case 0:
                message += " / 理性值--";
                break;
            case 1:
                message += " / 怒氣值--";
                break;
            case 2:
                message += " / 憂慮值--";
                break;
            case -1:
                break;
        }
        return message;
    }

    int judge_max(int[] arr)
    {
        int index = 0;
        int max = arr[0];
        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i] > max)
            {
                max = arr[i];
                index = i;
            }
        }
        return index;
    }

    int find_negative(int[] arr)
    {
        int index = -1;
        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i] < 0)
            {
                index = i;
                break;
            }
        }
        return index;
    }

    public void GeneratePopupText(GameObject targetPrefab,string text)
    {
        Instantiate(targetPrefab);
        targetPrefab.transform.parent = GameObject.FindGameObjectWithTag("Canvas").transform;
        targetPrefab.GetComponent<InteracionText>().ShowJumpText(text, Camera.main.WorldToScreenPoint(transform.position));
        Destroy(targetPrefab, 1.8f);
    }
}
