using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class MapGeneration : MonoBehaviour
{
    List<string[]> ToRoomNo;//�ýű�����ת��(Floor,Room)��RoomNo
    List<string[]> ToDoors;//�ýű������¼��ͨ���ķ���
    List<string[]> Layout;//�ýű������¼ÿ�������ڲ�������
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
    /// ���ú��������ɷ����ڵ���Ʒ
    /// </summary>
    /// <param name="Floor">����Ĳ���</param>
    /// <param name="Room">����ĺ�����</param>
    /// <returns>�Ƿ����ɳɹ�</returns>
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
    /// ���ɷ����ڵ���
    /// </summary>
    /// <param name="RoomNo">����ı��</param>
    /// <returns>�Ƿ����ɳɹ�</returns>
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
    /// ���ú�����ݻٷ����ڵ���Ʒ
    /// </summary>
    /// <param name="Floor">����Ĳ���</param>
    /// <param name="Room">����ĺ�����</param>
    /// <returns>�Ƿ�ݻٳɹ�</returns>
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
