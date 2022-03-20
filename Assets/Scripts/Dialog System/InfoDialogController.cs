using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoDialogController : MonoBehaviour
{
    //*****Copyright MAPLELEAF3659*****
    public Text titleTextRightUp, contentTextRightUp, titleTextRightDown, contentTextRightDown,
        titleTextLeftUp, contentTextLeftUp, titleTextLeftDown, contentTextLeftDown;
    public char[] titleWords, contentWords;
    public float titlePlaySpeed, contentPlaySpeed, analyzeSpeed;
    public Animator infoDialogAni;
    Text titleText, contentText;
    char[] analyzeWords = "分析中...".ToCharArray();

    void Start()
    {
        //ClearText();
    }

    public void PlayInfoTextAni(string title, string content, Vector2 mousePos)
    {
        StopAllCoroutines();
        this.titleWords = title.ToCharArray();
        this.contentWords = content.ToCharArray();
        if (mousePos.x > Screen.width / 2)//right
        {
            if (mousePos.y > Screen.height / 2)//up
            {
                infoDialogAni.SetInteger("direction", 2);
                titleText = titleTextLeftDown;
                contentText = contentTextLeftDown;
            }
            else if (mousePos.y <= Screen.height / 2)//down
            {
                infoDialogAni.SetInteger("direction", 3);
                titleText = titleTextLeftUp;
                contentText = contentTextLeftUp;
            }
        }
        else if (mousePos.x <= Screen.width / 2)//left
        {
            if (mousePos.y > Screen.height / 2)//up
            {
                infoDialogAni.SetInteger("direction", 1);
                titleText = titleTextRightDown;
                contentText = contentTextRightDown;
            }
            else if (mousePos.y <= Screen.height / 2)//down
            {
                infoDialogAni.SetInteger("direction", 0);
                titleText = titleTextRightUp;
                contentText = contentTextRightUp;
            }
        }
        ClearText();
        infoDialogAni.SetBool("isOpen", true);
        StartCoroutine(Analyze(titlePlaySpeed, analyzeSpeed));
    }

    void ClearText()
    {
        titleText.text = "";
        contentText.text = "";
    }

    IEnumerator Analyze(float analyzeTextSpeed, float analyzeSpeed)
    {
        foreach (char c in analyzeWords)
        {
            titleText.text += c;
            yield return new WaitForSeconds(analyzeTextSpeed);
        }
        float percent = 0f;
        while (percent < 100)
        {
            percent += Random.Range(1f, 10f);
            titleText.text = percent <= 100 ? "分析中..." + percent.ToString("0.") + "%" : "分析中...100%";
            yield return new WaitForSeconds(analyzeSpeed);
        }
        ClearText();
        StartCoroutine(ShowTitle(titleWords, titlePlaySpeed));
        StartCoroutine(ShowContent(contentWords, contentPlaySpeed));
    }

    IEnumerator ShowTitle(char[] titleWords, float speed)
    {
        foreach (char c in titleWords)
        {
            titleText.text += c;
            yield return new WaitForSeconds(speed);
        }
    }

    IEnumerator ShowContent(char[] contentWords, float speed)
    {
        foreach (char c in contentWords)
        {
            contentText.text += c;
            yield return new WaitForSeconds(speed);
        }
    }
}
