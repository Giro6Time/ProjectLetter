using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_MiniMap : MonoBehaviour, IPointerDownHandler
{
    public GameObject ScrollMapObj;
    public GameObject ScrollMapBarObj;
    ScrollMap scrollMap;
    ScrollMapBar scrollMapBar;

    void Start()
    {
        if(ScrollMapObj != null)
        {
            scrollMap = ScrollMapObj.GetComponent<ScrollMap>();
        }
        if(ScrollMapBarObj != null)
        {
            scrollMapBar = ScrollMapBarObj.GetComponent<ScrollMapBar>();
        }
    }

    public void OnPointerDown(PointerEventData data)
    {
        ScrollMapBarObj.SetActive(true);
        ScrollMapObj.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
