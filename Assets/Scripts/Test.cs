using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 绑在Camera上用来测试的
/// </summary>
public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        CSVReader.ReadLine("/Scripts/Maps/LevelLayer.csv");
        CSVReader.ReadTable("/Scripts/test.csv");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
