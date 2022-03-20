using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameView : MonoBehaviour
{
    //*****Copyright MAPLELEAF3659*****
    GameModel model;
    GameController controller;

    [Header("Day Counter")]
    public Text dayText, dayInWindowText;
    public Animator dayAni;
    //public bool isChangingDay;

    [Header("Cameras")]
    public GameObject[] camScreen;
    public Animator[] camAni;//0=room,1=kitchen,2=screen
    public GameObject switchNoise;

    [Header("ADs")]
    public GameObject adWindow;
    public Image[] adInSelection;
    public ADSlotManager[] adSlots;
    public Sprite emptyAD;
    public GameObject adForPut;
    public bool isGetAD;

    [Header("Items")]
    public GameObject[] items;//0~20

    [Header("Result")]
    public Text todayIncomeText, balancyText;
    public Button[] upgradeBtn;
    public Text[] purchasedText;

    [Header("Money")]
    public Text moneyText;
    public GameObject flyingMoney;

    void Start()
    {
        //adCurrent.sprite = adSprite[0];
        model = GetComponent<GameModel>();
        controller = GetComponent<GameController>();
        StartCoroutine(ChangeDayNum(1, 2.23f));
    }

    void Update()
    {

    }

    public bool isDisplayingResult;
    public void DisplayResult(float todayIncome, float balancy)
    {
        isDisplayingResult = true;
        todayIncomeText.text = "本日收入：\t\t\t$" + todayIncome.ToString("0");
        balancyText.text = "目前餘額：\t\t\t$" + balancy.ToString("0");
        dayAni.SetTrigger("change");
        UpdateUpgradePanel();
        model.ChangeCamPos(1, false, 2f);
    }

    public void SetDayCounter(int day)
    {
        StartCoroutine(ChangeDayNum(day, 1.1f));
        dayAni.SetTrigger("close");
        isDisplayingResult = false;
    }

    public IEnumerator ChangeDayNum(int num, float waitSeconds)
    {
        yield return new WaitForSeconds(waitSeconds);
        dayText.text = "Day " + num.ToString();
        controller.soundManager.CreateSound(controller.dayChangeAudio);
        //dayInWindowText.text = num.ToString();
    }

    public void ChangeToEndScene()
    {
        StartCoroutine(ChangeDayNum(7, 1.1f));
        dayAni.SetTrigger("end");
        Invoke("LoadEndScene",4.2f);
    }

    void LoadEndScene()
    {
        SceneManager.LoadScene("End");
    }

    public IEnumerator ChangeCam(int camNum, bool hideDay, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        if (hideDay)
            dayText.CrossFadeAlpha(0f, 0f, false);
        switchNoise.SetActive(true);

        adWindow.SetActive(camNum == 2 ? true : false);
        //dayInWindowText.enabled = (camNum == 2 ? true : false);

        for (int i = 0; i < camAni.Length; i++)
        {
            if (camNum == i)
            {
                camScreen[i].SetActive(true);
            }
            else
            {
                camScreen[i].SetActive(false);
            }
        }
        yield return new WaitForSeconds(1f);
        switchNoise.SetActive(false);
        if (hideDay)
            dayText.CrossFadeAlpha(1f, 0f, false);
    }

    public void SetCurrentAD(int[] adNums)
    {
        foreach (ADSlotManager adSlot in adSlots)
        {
            adSlot.adSlotSpriteRenderer.sprite = emptyAD;
        }
        for (int i = 0; i < adInSelection.Length; i++)
        {
            adInSelection[i].sprite = model.adInfos[adNums[i]].adSprite;
        }
    }

    public void CreateFollowingAD(Vector2 mousePos, Sprite adSprite, int index)
    {
        adForPut.GetComponent<ADForPut>().adImage.sprite = adSprite;
        adForPut.GetComponent<ADForPut>().index = index;
        Instantiate(adForPut, mousePos, Quaternion.identity);
        isGetAD = true;
    }

    public void UpdateUpgradePanel()
    {
        balancyText.text = "目前餘額：\t\t\t$" + model.money;
        for (int i = 0; i < model.upgraded.Length; i++)
        {
            if (model.upgraded[i])
            {
                upgradeBtn[i].interactable = false;
                purchasedText[i].enabled = true;
            }
            else if (model.money < model.upgradeCost[i])
            {
                upgradeBtn[i].interactable = false;
                purchasedText[i].enabled = false;
            }
            else
            {
                upgradeBtn[i].interactable = true;
            }
        }
    }

    public void UpdateDisplayItem(List<int> itemsIndex)
    {
        foreach (GameObject item in items)
        {
            item.SetActive(false);
        }
        foreach (int i in itemsIndex)
        {
            items[i].SetActive(true);
        }
        //for (int i = 0; i < isItemUsedArray.Length; i++)
        //{
        //    if (model.adInfos[i].isPlayed == true && isItemUsedArray[i] == false)
        //    {
        //        items[i].SetActive(true);
        //        isItemUsedArray[i] = true;
        //    }
        //    else
        //    {
        //        items[i].SetActive(false);
        //    }
        //}
    }

    public void CreateFlyingMoney(Vector2 startPos, float moneyEarned)
    {
        flyingMoney.GetComponent<FlyingMoney>().moneyEarned = moneyEarned;
        Instantiate(flyingMoney, Camera.main.WorldToScreenPoint(startPos), Quaternion.identity);
    }

    float addMoneyAniCount;
    public void AddMoneyText(float moneyForAdd)
    {
        addMoneyAniCount++;
        moneyText.text = "$" + (model.money + moneyForAdd * addMoneyAniCount).ToString("0");
        if (addMoneyAniCount >= 4)
        {
            addMoneyAniCount = 0;
            //moneyText.text = "$" + (model.money).ToString("0");
        }
    }

    public void UpdateMoneyText(float money)
    {
        moneyText.text = "$" + money.ToString("0");
    }
}
