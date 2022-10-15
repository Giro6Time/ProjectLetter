using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class MiniMap : MonoBehaviour,IPointerClickHandler
{
    public MapDetail MapDetail;//显然现在还没有
    
    private void Awake()
    {
        
    }

    public void OnPointerClick(PointerEventData data)
    {
        MapDetail.ShowDetail(true);
    }
}
