using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPassToNextScene : MonoBehaviour
{
    //*****Copyright MAPLELEAF3659*****
    public float money = 0;
    public List<float> incomeRecord = new List<float>();
    public List<int> adIndexRecord = new List<int>();
    public int resultNum = 0;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void SetDataForPass(float money, List<float> incomeRecord, List<int> adIndexRecord, int resultNum)
    {
        this.money = money;
        this.incomeRecord = incomeRecord;
        this.adIndexRecord = adIndexRecord;
        this.resultNum = resultNum;
    }
}
