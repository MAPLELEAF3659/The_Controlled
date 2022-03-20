using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameModel : MonoBehaviour
{
    //*****Copyright MAPLELEAF3659*****
    GameView view;
    GameController controller;

    [Header("Game Status")]
    [Range(1, 7)]
    public int day = 1;
    [Range(0, 2)]
    public int mode = 0;//easy=0,normal=1,hard=2
    public int currentCamPos = 0;

    [Header("Player Status")]
    public float money;
    public float todayIncomeTemp;

    [Header("NPC Status")]
    public int reasonValue;
    public int angryValue;
    public int concernedValue;

    [Header("AD")]
    public ADInfo[] adInfos;//0~20
    public int[] currentADNum;
    public int todayADCount = 0;

    [Header("Upgrade system")]
    public bool[] upgraded;
    public int[] upgradeCost;

    [Header("Item Status")]
    public List<int> nextDayItemsIndex;

    [Header("Records")]
    public List<float> incomeRecord = new List<float>();
    public List<int> adIndexRecord = new List<int>();
    public List<int> adCountRecord = new List<int>();

    public DataPassToNextScene dataPass;

    void Start()
    {
        view = GetComponent<GameView>();
        controller = GetComponent<GameController>();
        mode = PlayerPrefs.GetInt("mode");
        ChangeCamPos(1, false, 0f);
        SetSelectionAD();
    }

    void Update()
    {
        if (todayADCount >= 2)
        {
            DisplayResultToView();
            todayADCount = 0;
        }
    }

    public void DisplayResultToView()
    {
        incomeRecord.Add(todayIncomeTemp);
        todayIncomeTemp = 0;
        view.DisplayResult(incomeRecord[incomeRecord.Count - 1], money);
        controller.soundManager.CreateSound(controller.resultAudio);
        //Invoke("SetSelectionAD", 2f);
    }

    public void ChangeDay()
    {
        day++;
        if (day >= 7)
        {
            if (adCountRecord.Count >= 4)
                dataPass.SetDataForPass(money, incomeRecord, adIndexRecord, 1);
            else if((angryValue + concernedValue) * (2 / 3) < reasonValue)
                dataPass.SetDataForPass(money, incomeRecord, adIndexRecord, 0);
            else if ((angryValue + concernedValue) * (2 / 3) > reasonValue)
                dataPass.SetDataForPass(money, incomeRecord, adIndexRecord, 2);
            view.ChangeToEndScene();
        }
        else
        {
            view.SetDayCounter(day);
            SetNextDayItems();
            SetSelectionAD();
        }
    }

    public void ChangeCamPos(int camNum, bool hideDay, float delayTime)
    {
        currentCamPos = camNum;
        view.StartCoroutine(view.ChangeCam(camNum, hideDay, delayTime));
    }

    public void AddValue(int reason, int angry, int concerned)
    {
        reasonValue += reason;
        angryValue += angry;
        concernedValue += concerned;
    }

    public void AddMoneyByWeight(int index)
    {
        float tempIncome = calculateIncome(index);
        money += tempIncome;
        todayIncomeTemp += tempIncome;
    }

    public void SetSelectionAD()
    {
        int lastIndexForToday = 0;
        switch (day)
        {
            case 1:
                lastIndexForToday = 3;
                break;
            case 2:
                lastIndexForToday = 7;
                break;
            case 3:
                lastIndexForToday = 10;
                break;
            case 4:
                lastIndexForToday = 14;
                break;
            case 5:
                lastIndexForToday = 17;
                break;
            case 6:
                lastIndexForToday = 20;
                break;
        }
        //for (int i = 0; i < 4; i++)
        //{
        //    currentADNum[i] = Random.Range(0, lastIndexForToday + 1);
        //    if (adInfos[currentADNum[i]].isPlayed == true)
        //    {
        //        i--;
        //        continue;
        //    }
        //    for (int j = 0; j < i; j++)
        //    {
        //        if (currentADNum[i] == currentADNum[j])
        //        {
        //            i--;
        //            break;
        //        }
        //    }
        //}
        //switch (day)
        //{
        //    case 1:
        //        currentADNum[0] = 0;
        //        currentADNum[1] = 1;
        //        break;
        //    case 2:
        //        currentADNum[0] = 4;
        //        currentADNum[1] = 5;
        //        break;
        //    case 3:
        //        currentADNum[0] = 8;
        //        currentADNum[1] = 9;
        //        break;
        //    case 4:
        //        currentADNum[0] = 11;
        //        currentADNum[1] = 12;
        //        break;
        //    case 5:
        //        currentADNum[0] = 15;
        //        currentADNum[1] = 16;
        //        break;
        //    case 6:
        //        currentADNum[0] = 18;
        //        currentADNum[1] = 19;
        //        break;
        //}
        for (int i = 0; i < 4; i++)
        {
            currentADNum[i] = Random.Range(0, lastIndexForToday + 1);
            if (adInfos[currentADNum[i]].isPlayed == true)
            {
                i--;
                continue;
            }
            for (int j = 0; j < i; j++)
            {
                if (currentADNum[i] == currentADNum[j])
                {
                    i--;
                    break;
                }
            }
        }
        view.SetCurrentAD(currentADNum);
    }

    public void Upgrade(int index)
    {
        upgraded[index] = true;
        money -= upgradeCost[index];
        view.UpdateUpgradePanel();
        view.UpdateMoneyText(money);
    }

    public float calculateIncome(int index)
    {
        float tempIncome;
        tempIncome = (reasonValue * adInfos[index].reason + angryValue * adInfos[index].angry + concernedValue * adInfos[index].concerned) * 10;
        return tempIncome;
    }
    public void SetNextDayItems()
    {
        view.UpdateDisplayItem(nextDayItemsIndex);
        foreach (int indexToday in nextDayItemsIndex)
        {
            if (adInfos[indexToday].isVideo == false)
            {
                adCountRecord.Add(indexToday);
            }
            adIndexRecord.Add(indexToday);
        }
        nextDayItemsIndex.Clear();
    }

    //public void ChangeCurrentAD(int adNum)
    //{
    //    currentADPos = adNum;
    //    view.ChangeCurrentAD(adNum);
    //}
}
