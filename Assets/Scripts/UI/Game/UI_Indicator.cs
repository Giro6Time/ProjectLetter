using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class UI_Indicator : MonoBehaviour
{
    List<string[]> IndicatorPosition;
    RectTransform indicatorRectTransform;

    private void Start()
    {
        string AssetPath;
        //string[] path = AssetDatabase.FindAssets("MapGeneration");
        //if (path.Length > 1) return;
        //AssetPath = AssetDatabase.GUIDToAssetPath(path[0]).Replace((@"/" + "MapGeneration" + ".cs"), "");
        //AssetPath = Application.dataPath + "/Scripts/Maps";
        AssetPath = Application.streamingAssetsPath;
        csvController.GetInstance().loadFile(AssetPath, "ToIndicatorPosition.csv");
        IndicatorPosition = new List<string[]>(csvController.GetInstance().arrayData);

        indicatorRectTransform = GetComponent<RectTransform>();
    }

    public Vector3 SetIndicatorPosition(int RoomNo)
    {
        Vector3 indicatorPos = Vector3.zero;
        if(IndicatorPosition[RoomNo][1]!=""&& IndicatorPosition[RoomNo][2] != "")
        {
            indicatorPos.x = float.Parse(IndicatorPosition[RoomNo][1]);
            indicatorPos.y = float.Parse(IndicatorPosition[RoomNo][2]);
            indicatorPos.z = 0;
        }
        indicatorRectTransform.localPosition = indicatorPos;
        //indicatorRectTransform.SetPositionAndRotation(indicatorPos, Quaternion.Euler(Vector3.zero));
        return indicatorPos;
    }


}
