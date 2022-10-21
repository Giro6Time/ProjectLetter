using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_MiniMap : MonoBehaviour, IPointerDownHandler
{
    public GameObject ScrollMapObj;
    public GameObject IndicatorObj;
    public GameObject ScrollMapBarObj;
    ScrollMap scrollMap;
    ScrollMapBar scrollMapBar;
    UI_Indicator uI_Indicator;
    RectTransform thisRectTransform;
    UI_ScrollMap uI_ScrollMap;

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
        
        thisRectTransform = GetComponent<RectTransform>();
        uI_ScrollMap = scrollMap.GetComponent<UI_ScrollMap>();
    }

    public void OnPointerDown(PointerEventData data)
    {
        ScrollMapBarObj.SetActive(true);
        ScrollMapObj.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void UpdateMapTo(int RoomNo)
    {
        Vector3 idicatorPos = uI_Indicator.SetIndicatorPosition(RoomNo);

        Vector3 thisRectTransformPos = thisRectTransform.position;
        thisRectTransformPos.y = Mathf.Clamp(-idicatorPos.y, -1550, 1550);
        thisRectTransform.localPosition = thisRectTransformPos;

        float ScrollY = (float)((9775.0 / 3293.0) * thisRectTransformPos.y + (11144.0 / 3293.0));
        uI_ScrollMap.SetYPos(ScrollY);

    }
}
