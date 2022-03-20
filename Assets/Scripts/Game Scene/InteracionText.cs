using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteracionText : MonoBehaviour
{
    //*****Copyright MAPLELEAF3659*****
    Vector2 currentMousePos;
    public Text text,title;
    public bool isFollowingMode;
    public Animator textAnimator;
    void Start()
    {
        //text = GetComponent<Text>();
        HideText();
        //textAnimator.ResetTrigger("hide");
    }

    void Update()
    {
        if (isFollowingMode)
        {
            currentMousePos = Input.mousePosition;
            transform.position = currentMousePos +
                new Vector2(currentMousePos.x < (Screen.width - 50f) ? 50f : -50f, currentMousePos.y < (Screen.height - 100f) ? 50f : -50f);
            textAnimator.SetBool("isUp", currentMousePos.y > Screen.height / 2 ? false : true);
        }
    }

    public void ShowJumpText(string txt, Vector3 pos)
    {
        isFollowingMode = false;
        transform.position = pos;
        StartCoroutine(JumpTextAni(txt));
    }

    IEnumerator JumpTextAni(string txt)
    {
        textAnimator.SetTrigger("jump");
        text.text = txt;
        yield return new WaitForSeconds(1.75f);
        text.text = "";
        isFollowingMode = true;
    }

    public void ShowText(string title, string txt)
    {
        isFollowingMode = true;
        this.title.text = title;
        text.text = txt;
        textAnimator.SetBool("isShow", true);
    }

    public void HideText()
    {
        isFollowingMode = false;
        transform.position = new Vector2(Screen.width + 1000f, 0);
        //text.text = "";
        textAnimator.SetBool("isShow", false);
    }
}
