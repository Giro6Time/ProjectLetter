using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class MapGeneration : MonoBehaviour
{
    csvController ToRoomNo;//该脚本负责转换(Floor,Room)到RoomNo
    csvController ToDoors;//该脚本负责记录门通往的房间
    csvController Layout;//该脚本负责记录每个房间内部的物体
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

        Layout = csvController.GetInstance();
        Layout.loadFile(AssetPath, "Layout.csv");

        SceneDoorsObjects = new List<GameObject>();
        SceneDoorsObjects.Add(GameObject.Find("DoorToSameFloor1"));
        SceneDoorsObjects.Add(GameObject.Find("DoorToSameFloor2"));
        SceneDoorsObjects.Add(GameObject.Find("DoorToNextFloor1"));
        SceneDoorsObjects.Add(GameObject.Find("DoorToNextFloor2"));
        SceneDoorsObjects.Add(GameObject.Find("DoorToNextFloor3"));

        GenerateLayoutObjects(1);
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// 调用函数会生成房间内的物品
    /// </summary>
    /// <param name="Floor">房间的层数</param>
    /// <param name="Room">房间的号码数</param>
    /// <returns>是否生成成功</returns>
    public bool GenerateRoom(int Floor, int Room)
    {

        int thisRoomNo = FromFloorToRoomNo(Floor, Room);
        if(GenerateDoors(thisRoomNo) == false)
            return false;

        if (GenerateLayoutObjects(thisRoomNo) == false)
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

    bool GenerateLayoutObjects(int RoomNo)
    {
        int RowCount = Layout.getRowLength(RoomNo);
        int ObjCount = (RowCount - 2) / 3;

        for (int i = 0; i < ObjCount; i++)
        {
            string objName = Layout.getString(RoomNo, 3 * i + 2);
            float xPos = Layout.getFloat(RoomNo, 3 * i + 3);
            float yPos = Layout.getFloat(RoomNo, 3 * i + 4);

            GameObject tempObj = GameObject.Find(objName);
            tempObj.SetActive(true);
            Vector3 objPos = new Vector3(xPos, yPos, 0);
            tempObj.transform.position = objPos;

            Debug.Log(objName + " " + xPos + " " + yPos + "Initialized");
        }

        Vector3 camPos = Camera.main.transform.position;
        string roomType = Layout.getString(RoomNo, 1);
        GameObject floorObj = GameObject.Find(roomType);
        floorObj.SetActive(true);
        floorObj.transform.position = camPos;

        return true;
    }

    /// <summary>
    /// 调用函数会摧毁房间内的物品
    /// </summary>
    /// <param name="Floor">房间的层数</param>
    /// <param name="Room">房间的号码数</param>
    /// <returns>是否摧毁成功</returns>
    public bool DestoryRoom(int Floor, int Room)
    {

        int thisRoomNo = FromFloorToRoomNo(Floor, Room);
        if (DestroyDoors() == false)
            return false;

        if (DestroyLayoutObjects(thisRoomNo) == false)
            return false;

        return true;
    }

    bool DestroyDoors()
    {
        for (int i = 0; i < 5; i++)
            SceneDoorsObjects[i].SetActive(false);
        return true;
    }

    bool DestroyLayoutObjects(int RoomNo)
    {
        int RowCount = Layout.getRowLength(RoomNo);
        int ObjCount = (RowCount - 2) / 3;

        for (int i = 0; i < ObjCount; i++)
        {
            string objName = Layout.getString(RoomNo, 3 * i + 2);
            GameObject tempObj = GameObject.Find(objName);
            tempObj.SetActive(false);
            Debug.Log(objName +"Disabled");
        }

        string roomType = Layout.getString(RoomNo, 1);
        GameObject floorObj = GameObject.Find(roomType);
        floorObj.SetActive(false);

        return true;
    }

    int FromFloorToRoomNo(int Floor, int Room)
    {
        return ToRoomNo.getInt(Floor, Room);
    }
}
