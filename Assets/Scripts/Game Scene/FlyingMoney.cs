using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlyingMoney : MonoBehaviour
{
    //*****Copyright MAPLELEAF3659*****
    public GameView view;
    public float flyingSpeed;
    public Vector2 startPos;
    public Vector2 endPos;
    public float moneyEarned = 0;

    void Start()
    {
        gameObject.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform);
        endPos = GameObject.FindGameObjectWithTag("moneySystemCoinIcon").transform.position;
        view = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameView>();
        flyingSpeed = Random.Range(500f, 600f);
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, endPos) > 50f)
            transform.position = Vector2.MoveTowards(transform.position, endPos, flyingSpeed * Time.deltaTime);
        else
        {
            view.AddMoneyText(moneyEarned);
            Destroy(gameObject);
        }
    }
}
