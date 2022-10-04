using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MapGeneration : MonoBehaviour
{
    csvController ToRoomNo;//该脚本负责转换(Floor,Room)到RoomNo
    csvController ToDoors;//该脚本负责记录门通往的房间
    List<GameObject> SceneDoorsObjects;

    private static volatile MapGeneration instance = new MapGeneration();
    private MapGeneration() { }
    public static MapGeneration Instance { get { return instance; } }

    // Start is called before the first frame update
    void Start()
    {
        ToRoomNo = csvController.GetInstance();
        string AssetPath;
        string[] path = AssetDatabase.FindAssets("MapGeneration");
        if (path.Length > 1) return;
        AssetPath = AssetDatabase.GUIDToAssetPath(path[0]).Replace((@"/"+ "MapGeneration"+".cs"),"");
        ToRoomNo.loadFile(AssetPath , "ToRoomNo.csv");

        ToDoors = csvController.GetInstance();
        ToDoors.loadFile(AssetPath, "RoomNoToDoors.csv");

        SceneDoorsObjects = new List<GameObject>();
        SceneDoorsObjects.Add(GameObject.Find("DoorToSameFloor1"));
        SceneDoorsObjects.Add(GameObject.Find("DoorToSameFloor2"));
        SceneDoorsObjects.Add(GameObject.Find("DoorToNextFloor1"));
        SceneDoorsObjects.Add(GameObject.Find("DoorToNextFloor2"));
        SceneDoorsObjects.Add(GameObject.Find("DoorToNextFloor3"));

        GenerateRoom(1, 0);
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// 调用函数会生成房间内的门
    /// </summary>
    /// <param name="Floor">房间的层数</param>
    /// <param name="Room">房间的号码数</param>
    /// <returns>是否生成成功</returns>
    public bool GenerateRoom(int Floor, int Room)
    {

        int thisRoomNo = FromFloorToRoomNo(Floor, Room);
        if(GenerateDoors(thisRoomNo) == false)
            return false;

        return true;
    }

    /// <summary>
    /// 生成房间内的门
    /// </summary>
    /// <param name="RoomNo">房间的编号</param>
    /// <returns>是否生成成功</returns>
    bool GenerateDoors(int RoomNo)
    {
        if(RoomNo <= 0) return false;

        for(int i = 0; i < 5; i++)
        {
            int tempToFloor = ToDoors.getInt(RoomNo, 2*i+1);
            int tempToRoom = ToDoors.getInt(RoomNo, 2*i+2);
            //int tempToRoomNo = FromFloorToRoomNo(tempToFloor, tempToRoom);

            if(tempToRoom <=0 || tempToFloor <= 0)
            {
                SceneDoorsObjects[i].SetActive(false);
            }
            else
            {
                SceneDoorsObjects[i].SetActive(true);
                Door thisDoorScript = SceneDoorsObjects[i].GetComponent<Door>();
                thisDoorScript.SetDoorPatameter(tempToFloor, tempToRoom);
            }
        }

        return true;
    }

    int FromFloorToRoomNo(int Floor, int Room)
    {
        return ToRoomNo.getInt(Floor, Room);
    }
}
