using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MapGeneration : MonoBehaviour
{
    csvController MapData;//�ýű�Ҫ��Ҫ��ȡ�ĵ�ͼ���ݷ���һ���ļ�����
    // Start is called before the first frame update
    void Start()
    {
        MapData = csvController.GetInstance();
        string AssetPath;
        string[] path = AssetDatabase.FindAssets("MapGeneration");
        if (path.Length > 1) return;
        AssetPath = AssetDatabase.GUIDToAssetPath(path[0]).Replace((@"/"+ "MapGeneration"+".cs"),"");
        Debug.Log(AssetPath);
        MapData.loadFile(AssetPath , "LevelLayer.csv");

        GenerateRoom(0, 0);
    }

    // Update is called once per frame
    void Update()
    {
    }

    /// <summary>
    /// ���ú��������ɷ����ڵĹ���¼��Լ���Ӧ����
    /// </summary>
    /// <param name="Layer">����Ĳ���</param>
    /// <param name="RoomNo">����ĺ�����</param>
    /// <returns>�Ƿ����ɳɹ�</returns>
    bool GenerateRoom(int Layer,int RoomNo)
    {
        string test = MapData.getString(0, 1);
        Debug.Log(test);
        return false;
    }

}
