using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ����Camera���������Ե�
/// </summary>
public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        csvController.GetInstance().loadFile(Application.dataPath + "/Scripts", "test.csv");
        Debug.Log(csvController.GetInstance().getString(2, 0));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
