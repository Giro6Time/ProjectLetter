using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ����������Ϸ����
/// </summary>
public class MainScript : MonoBehaviour
{
    public static MainScript S;
    public Hero hero;
    public bool couldChooseDoor;  //�������������ʱ����������Ƿ���Ч
    public bool couldChooseEvent;  //�������������ʱ������¼����ڵ��¼��Ƿ���Ч
    public bool whetherViolate;  //ѡ��·��ʱ��ɫ�Ƿ�Υ��
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
        hero = GameObject.Find("Wizard").GetComponent<Hero>();
        Layout(roomNo);  //���ɳ�ʼ����
    }

    void Update()
    {
        

    }

    ///���ֺ��������뷿��ţ���ȡ
    ///�������ʱ���ô˺���
    public void Layout(int RoomNo) 
    {
        InitRoom();  //�ƶ��ɷ����Ԫ�ص������
        ///������ݱ�����·���


        Dialogue(RoomNo);  //
    }

    public void InitRoom()  ///����ɷ���
    {
        
    }    

    ///����csv�����������ظ��е�stringp[]
    public string[] ReadCsv (TextAsset text,int no)
    {
        string[] Data = { "" };
        ///

        return Data;
    }

    ///���ζ��ж�
    public bool TrustJudge()
    {
        bool judge = true;
        ///���ݲ߻��������ж�
        
        return judge;
    }

    ///�Ի��ű�������Ի�����
    public void Dialogue(int talkLibNo) 
    {

    }

}






