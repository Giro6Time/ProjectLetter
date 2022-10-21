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

    public void SetYPos(float yPos)
    {
        yPos = Mathf.Clamp(yPos, -4644, 4644);
        RectTransform rectTransform = GetComponent<RectTransform>();
        Vector3 pos = rectTransform.localPosition;
        pos.y = yPos;
        rectTransform.localPosition = pos;

        Vector3 indicatorPos = Indicator.transform.localPosition;
        indicatorPos.y = -yPos;
        Indicator.transform.localPosition = indicatorPos;
    }
}
