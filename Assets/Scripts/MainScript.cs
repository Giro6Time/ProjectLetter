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

    void Start()
    {
        if (S == null)
            S = this;
        hero = GameObject.Find("Wizard").GetComponent<Hero>();
        Layout(1);  //���ɳ�ʼ����
    }

    void Update()
    {
        

    }

    //���ֺ��������뷿��ţ���ȡ
    //�������ʱ���ô˺���
    public void Layout(int RoomNo) 
    {
        InitRoom();


        
    }

    public void InitRoom()  //����ɷ���
    {
        
    }    

    //����csv�����������ظ��е�stringp[]
    public string[] ReadCsv (TextAsset text,int no)
    {
        string[] Data = { "" };
        return Data;
    }




}






