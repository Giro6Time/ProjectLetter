using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class MapGeneration : MonoBehaviour
{
    List<string[]> ToRoomNo;//该脚本负责转换(Floor,Room)到RoomNo
    List<string[]> ToDoors;//该脚本负责记录门通往的房间
    List<string[]> Layout;//该脚本负责记录每个房间内部的物体
    public List<GameObject> SceneDoorsObjects;

    private static volatile MapGeneration instance;
    private MapGeneration() { }
    public static MapGeneration Instance { get { return instance; } }

    // Start is called before the first frame update
    void Awake()
    {
        instance = GetComponent<MapGeneration>();
        string AssetPath;
        string[] path = AssetDatabase.FindAssets("MapGeneration");
        if (path.Length > 1) return;
        AssetPath = AssetDatabase.GUIDToAssetPath(path[0]).Replace((@"/"+ "MapGeneration"+".cs"),"");
        csvController.GetInstance().loadFile(AssetPath , "ToRoomNo.csv");
        ToRoomNo = new List<string[]>(csvController.GetInstance().arrayData);
        
        csvController.GetInstance().loadFile(AssetPath, "RoomNoToDoors.csv");
        ToDoors = new List<string[]>(csvController.GetInstance().arrayData);

        csvController.GetInstance().loadFile(AssetPath, "Layout.csv");
        Layout = new List<string[]>(csvController.GetInstance().arrayData);

        SceneDoorsObjects = new List<GameObject>();
        SceneDoorsObjects.Add(GameObject.Find("DoorToSameFloor1"));
        SceneDoorsObjects.Add(GameObject.Find("DoorToSameFloor2"));
        SceneDoorsObjects.Add(GameObject.Find("DoorToNextFloor1"));
        SceneDoorsObjects.Add(GameObject.Find("DoorToNextFloor2"));
        SceneDoorsObjects.Add(GameObject.Find("DoorToNextFloor3"));
        for(int i = 0; i < 5; i++)
        {
            SceneDoorsObjects[i].SetActive(false);
        }
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
        if (ToRoomNo[Floor][Room - 1] == "") return false;
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
            int tempToFloor = int.Parse(ToDoors[RoomNo][2 * i + 1]);
            int tempToRoom = int.Parse(ToDoors[RoomNo][2 * i + 2]);
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
        int RowCount = Layout[RoomNo].Length;
        int ObjCount = (RowCount - 2) / 3;

        for (int i = 0; i < ObjCount; i++)
        {
            string objName = Layout[RoomNo][3 * i + 2];
            if (objName == "") continue;
            float xPos = float.Parse(Layout[RoomNo][3 * i + 3]);
            float yPos = float.Parse(Layout[RoomNo][3 * i + 4]);

            GameObject tempObj = GameObject.Find(objName);
            tempObj.SetActive(true);
            Vector3 objPos = new Vector3(xPos, yPos, 0);
            tempObj.transform.position = objPos;

            Debug.Log(objName + " " + xPos + " " + yPos + "Initialized");
        }

        Vector3 camPos = Camera.main.transform.position;
        string roomType = Layout[RoomNo][1];
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
        if (ToRoomNo[Floor][Room] == "") return false;
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
        int RowCount = Layout[RoomNo].Length;
        int ObjCount = (RowCount - 2) / 3;

        for (int i = 0; i < ObjCount; i++)
        {
            string objName = Layout[RoomNo][3 * i + 2];
            if (objName == "") continue;
            GameObject tempObj = GameObject.Find(objName);
            tempObj.SetActive(false);
            Debug.Log(objName +"Disabled");
        }

        string roomType = Layout[RoomNo][1];
        GameObject floorObj = GameObject.Find(roomType);
        floorObj.SetActive(false);

        return true;
    }

    public int FromFloorToRoomNo(int Floor, int Room)
    {
        return int.Parse(ToRoomNo[Floor][Room-1]);
    }
}
