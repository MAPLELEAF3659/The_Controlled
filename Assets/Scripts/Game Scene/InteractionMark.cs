using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionMark : MonoBehaviour
{
    //*****Copyright MAPLELEAF3659*****
    Vector2 currentMousePos;
    public Image mark;
    void Start()
    {
        mark = GetComponent<Image>();
        mark.CrossFadeAlpha(0f, 0f, false);
    }

    void Update()
    {
        currentMousePos = Input.mousePosition;
        transform.position = currentMousePos +
            new Vector2(currentMousePos.x < (Screen.width - 50f) ? 50f : -50f, currentMousePos.y < (Screen.height - 100f) ? 50f : -50f);
    }
}
