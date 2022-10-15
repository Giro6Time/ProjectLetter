using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScrollMap : MonoBehaviour, IPointerDownHandler
{
    public GameObject ScrollMapBarObj;
    public GameObject MinimapObj;
    public void OnPointerDown(PointerEventData data)
    {
        if(ScrollMapBarObj != null)
            ScrollMapBarObj.SetActive(true);
        if(MinimapObj != null)
            MinimapObj.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
