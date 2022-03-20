using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragItem : MonoBehaviour,IDragHandler,IBeginDragHandler
{
    //*****Copyright MAPLELEAF3659*****
    Vector2 dis;

    public void OnBeginDrag(PointerEventData eventData)
    {
        dis = transform.position - Input.mousePosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition + new Vector3(dis.x, dis.y);
    }
}
