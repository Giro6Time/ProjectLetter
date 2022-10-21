using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ScrollMap : MonoBehaviour
{
    public GameObject minimap;
    public GameObject Indicator;
    UI_Map uI_Map;

    private void Start()
    {
        if (minimap != null)
        {
            uI_Map = minimap.GetComponent<UI_Map>();
        }
    }

    public void SetPos(Vector2 PosToSet)
    {
        PosToSet.y = Mathf.Clamp(PosToSet.y, -4644, 4644);
        RectTransform rectTransform = GetComponent<RectTransform>();
        Vector3 pos = rectTransform.localPosition;
        pos.y = PosToSet.y;
        rectTransform.localPosition = pos;

        Vector3 indicatorPos = Indicator.transform.localPosition;
        indicatorPos.y = -PosToSet.y;
        indicatorPos.x = PosToSet.x;
        
        Indicator.transform.localPosition = indicatorPos;


    }
}
