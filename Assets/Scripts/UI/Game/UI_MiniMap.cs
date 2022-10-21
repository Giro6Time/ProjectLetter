using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_MiniMap : Singleton<UI_MiniMap>, IPointerDownHandler
{
    public GameObject ScrollMapObj;
    public GameObject IndicatorObj;
    public GameObject ScrollMapBarObj;
    public GameObject ThisMapObj;
    ScrollMap scrollMap;
    ScrollMapBar scrollMapBar;
    UI_Indicator uI_Indicator;
    UI_ScrollMap uI_ScrollMap;
    RectTransform thisRectTransform;
    public GameObject uI_ScrollMapObj;

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
        if(IndicatorObj != null)
        {
            uI_Indicator = IndicatorObj.GetComponent<UI_Indicator>();
        }
        
        thisRectTransform = ThisMapObj.GetComponent<RectTransform>();
        uI_ScrollMap = uI_ScrollMapObj.GetComponent<UI_ScrollMap>();
    }

    public void OnPointerDown(PointerEventData data)
    {
        ScrollMapBarObj.SetActive(true);
        ScrollMapObj.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void UpdateMapTo(int RoomNo)
    {
        Vector3 indicatorPos = uI_Indicator.SetIndicatorPosition(RoomNo);

        Vector3 thisRectTransformPos = thisRectTransform.localPosition;
        thisRectTransformPos.y = Mathf.Clamp(-indicatorPos.y, -1550, 1550);
        thisRectTransform.localPosition = thisRectTransformPos;

        float ScrollX = (float)((31.0 / 11.0) * indicatorPos.x + (100.0 / 11.0));
        float ScrollY = (float)((9775.0 / 3293.0) * thisRectTransformPos.y + (11144.0 / 3293.0));
        //uI_ScrollMap.SetYPos(ScrollY);
        Vector2 ScPos = new Vector2(ScrollX, ScrollY);
        uI_ScrollMap.SetPos(ScPos);
    }
}
