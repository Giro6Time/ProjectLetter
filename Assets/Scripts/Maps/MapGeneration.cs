using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MapGeneration : MonoBehaviour
{
    csvController MapData;//�ýű�Ҫ��Ҫ��ȡ�ĵ�ͼ���ݷ���һ���ļ�����
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
    /// ���ú��������ɷ����ڵĹ���¼��Լ���Ӧ����
    /// </summary>
    /// <param name="Floor">����Ĳ���</param>
    /// <param name="RoomNo">����ĺ�����</param>
    /// <returns>�Ƿ����ɳɹ�</returns>
    bool GenerateRoom(int Floor, int RoomNo)
    {

        return false;
    }

    /// <summary>
    /// �����Ŷ�Ӧ������
    /// </summary>
    /// <param name="ToFloor">���뷿��Ĳ���</param>
    /// <param name="ToRoomNo">���뷿��ĺ�����</param>
    /// <param name="PositionIndex">�ŵ�λ�ã���Ϊ1����Ϊ2����Ϊ3</param>
    /// <returns>�Ƿ����ɳɹ�</returns>
    bool GenerateDoor(int ToFloor, int ToRoomNo, int PositionIndex)
    {
        
        return true;    
    }
}
