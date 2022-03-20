using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public class ADInfo
{
    //*****Copyright MAPLELEAF3659*****
    public int num;
    public string name;
    public bool isVideo,isPlayed;
    public Sprite adSprite;
    public int reason, angry, concerned;

    //public ADInfo(Sprite adSprite,float reasonValue,float angryValue,float concernedValue)
    //{
    //    this.adSprite = adSprite;
    //    this.reasonValue = reasonValue;
    //    this.angryValue = angryValue;
    //    this.concernedValue = concernedValue;
    //}
}
