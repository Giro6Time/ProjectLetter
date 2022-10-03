using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MapGeneration : MonoBehaviour
{
    csvController MapData;//该脚本要和要读取的地图数据放在一个文件夹内
    public GameObject DoorPrefab;

    // Start is called before the first frame update
    void Start()
    {
        MapData = csvController.GetInstance();
        string AssetPath;
        string[] path = AssetDatabase.FindAssets("MapGeneration");
        if (path.Length > 1) return;
        AssetPath = AssetDatabase.GUIDToAssetPath(path[0]).Replace((@"/"+ "MapGeneration"+".cs"),"");
        MapData.loadFile(AssetPath , "LevelLayer.csv");

    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// 调用函数会生成房间内的怪物，事件以及对应的门
    /// </summary>
    /// <param name="Floor">房间的层数</param>
    /// <param name="RoomNo">房间的号码数</param>
    /// <returns>是否生成成功</returns>
    bool GenerateRoom(int Floor, int RoomNo)
    {

        return false;
    }

    /// <summary>
    /// 生成门对应的物体
    /// </summary>
    /// <param name="ToFloor">进入房间的层数</param>
    /// <param name="ToRoomNo">进入房间的号码数</param>
    /// <param name="PositionIndex">门的位置，左为1，中为2，右为3</param>
    /// <returns>是否生成成功</returns>
    bool GenerateDoor(int ToFloor, int ToRoomNo, int PositionIndex)
    {
        
        return true;    
    }
}
