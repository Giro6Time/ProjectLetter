using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class MapDetail : MonoBehaviour, IPointerClickHandler
{
    GameObject instance;
    private void Init()
    {
        instance = this.gameObject;
    }
    public void OnPointerClick(PointerEventData data)
    {
        ShowDetail(false);
    }
    public void ShowDetail(bool open)
    {
        if(instance == null)
        {
            Init();
        }
        if(open && !instance.activeSelf)
        {
            instance.SetActive(true);
        }
        else if(!open && instance.activeSelf)
        {
            instance.SetActive(false);
        }
    }

}
