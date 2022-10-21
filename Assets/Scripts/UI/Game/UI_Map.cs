using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Map : MonoBehaviour
{
    UI_Indicator uI_Indicator;
    RectTransform thisRectTransform;
    private void Start()
    {
        uI_Indicator = transform.Find("Indicator").GetComponent<UI_Indicator>();
        thisRectTransform = GetComponent<RectTransform>();
    }
    public void UpdateMapTo(int RoomNo)
    {
        Vector3 inicatorPos = uI_Indicator.SetIndicatorPosition(RoomNo);

        Vector3 thisRectTransformPos = thisRectTransform.position;
        thisRectTransformPos.y = Mathf.Clamp(-inicatorPos.y, -1550, 1550);
        thisRectTransform.position = thisRectTransformPos;
    }
}
