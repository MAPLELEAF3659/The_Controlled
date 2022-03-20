using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ADForPut : MonoBehaviour
{
    //*****Copyright MAPLELEAF3659*****
    //public GameObject adSlot;
    public Image adImage;
    public int index;

    void Start()
    {
        transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform);
        transform.localScale = new Vector2(1,1);
        //adSlot = GameObject.FindGameObjectWithTag("AD_slot");
        adImage = GetComponent<Image>();
    }

    void Update()
    {
        transform.position = Input.mousePosition;
        if (Input.GetMouseButtonUp(0))
        {
            Destroy(gameObject,0.5f);
        }
    }
}
