using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ����������Ϸ����
/// </summary>
public class MainScript : MonoBehaviour
{
    public static MainScript S;
    public Protangonist hero;
    public bool couldChooseDoor;  //�������������ʱ����������Ƿ���Ч
    public bool couldChooseEvent;  //�������������ʱ������¼����ڵ��¼��Ƿ���Ч
    public bool whetherViolate;  //ѡ��·��ʱ��ɫ�Ƿ�Υ��,-1��Υ����0��û��ѡ��1����ѭ
    public bool sameFloorMove;  //�Ƿ���ͬ���ƶ�

    public int floor;
    public int room;
    public int roomNo;


    void Awake()
    {
        if (S == null)
            S = this;
        floor = 1;
        room = 1;
        roomNo = 1;
        hero = GameObject.Find("Wizard").GetComponent<Protangonist>();
        GetComponent<MapGeneration>().GenerateRoom(1, 1);  //���ɳ�ʼ����
        
    }

    void Update()
    {
        

    }


    ///���ζ��ж�
    public bool TrustJudge()
    {
        bool judge = true;
        ///���ݲ߻��������ж�
        
        return judge;
    }

    

}






