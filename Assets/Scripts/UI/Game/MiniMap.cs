using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class MiniMap : MonoBehaviour,IPointerClickHandler
{
    [SerializeField]    Image Minimap;
    [SerializeField]    MapDetail MapDetail;//��Ȼ���ڻ�û��

    
    private void Awake()
    {
        
    }

    public void OnPointerClick(PointerEventData data)
    {
        MapDetail.ShowDetail(true);
    }
}
