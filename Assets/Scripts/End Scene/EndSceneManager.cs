using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndSceneManager : MonoBehaviour
{
    //*****Copyright MAPLELEAF3659*****
    public GameObject dataPassObj;
    public DataPassToNextScene dataPass;
    public Text totalMoneyText;
    public Text newsTitle;
    public Image newsImg;
    public string[] resultTitle;
    public Sprite[] resultImgs;

    void Start()
    {
        dataPassObj = GameObject.FindGameObjectWithTag("dataPass");
        dataPass = dataPassObj.GetComponent<DataPassToNextScene>();
        totalMoneyText.text = "結餘\n$" + dataPass.money.ToString("0");
        newsTitle.text = resultTitle[dataPass.resultNum];
        newsImg.sprite = resultImgs[dataPass.resultNum];
    }

    public void OnReplayClicked()
    {
        Destroy(dataPassObj);
        SceneManager.LoadScene("Start");
    }
}
