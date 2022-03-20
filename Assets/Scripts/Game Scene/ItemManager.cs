using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    //*****Copyright MAPLELEAF3659*****
    public int itemIndex;
    public string itemName, itemInfo;
    public SpriteRenderer itemSprite;
    public Animator itemAni;
    public AudioClip scanningAudio;
    Image interactionMark;
    GameObject infoDialog;
    Animator infoDialogAni;
    InfoDialogController infoDialogController;
    GameModel model;
    public int upgradeLimit;

    void Start()
    {
        interactionMark = GameObject.FindGameObjectWithTag("interactionMark").GetComponent<Image>();
        infoDialog = GameObject.FindGameObjectWithTag("infoDialog");
        infoDialogAni = GameObject.FindGameObjectWithTag("infoDialog").GetComponent<Animator>();
        infoDialogController = GameObject.FindGameObjectWithTag("infoDialog").GetComponent<InfoDialogController>();
        model = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameModel>();
    }

    private void OnMouseEnter()
    {
        interactionMark.CrossFadeAlpha(1f, 0f, false);
        itemAni.SetBool("isHovered", true);
    }

    private void OnMouseUp()
    {
        infoDialog.transform.position = Camera.main.WorldToScreenPoint(transform.position);

        if (infoDialogAni.GetBool("isOpen"))
        {
            infoDialogAni.SetBool("isOpen", false);
        }
        else
        {
            GameObject.FindGameObjectWithTag("soundManager").GetComponent<SoundManager>().CreateSound(scanningAudio);
            if (upgradeLimit == -1 || model.upgraded[upgradeLimit] == true)
                infoDialogController.PlayInfoTextAni(itemName, itemInfo, Input.mousePosition);
            else if (model.upgraded[upgradeLimit] == false)
            {
                switch (upgradeLimit)
                {
                    case 0:
                        infoDialogController.PlayInfoTextAni("無法分析文件", "請至升級頁購買文件分析工具", Input.mousePosition);
                        break;
                    case 1:
                        infoDialogController.PlayInfoTextAni("無法分析電子產品", "請至升級頁購買電子產品分析工具", Input.mousePosition);
                        break;
                    case 2:
                        infoDialogController.PlayInfoTextAni("無法分析痕跡", "請至升級頁購買痕跡分析工具", Input.mousePosition);
                        break;
                    case 3:
                        infoDialogController.PlayInfoTextAni("無法分析寵物", "請至升級頁購買寵物分析工具", Input.mousePosition);
                        break;
                    case 4:
                        infoDialogController.PlayInfoTextAni("無法分析便利貼", "請至升級頁購買便利貼分析工具", Input.mousePosition);
                        break;
                }
            }
        }
    }

    private void OnMouseExit()
    {
        interactionMark.CrossFadeAlpha(0f, 0f, false);
        infoDialogAni.SetBool("isOpen", false);
        itemAni.SetBool("isHovered", false);
    }
}
