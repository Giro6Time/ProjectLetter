using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 控制整个游戏进程
/// </summary>
public class MainScript : MonoBehaviour
{
    public static MainScript S;
    public Hero hero;
    public bool couldChooseDoor;  //这个变量决定此时鼠标点击出口是否有效
    public bool couldChooseEvent;  //这个变量决定此时鼠标点击事件房内的事件是否有效
    public bool whetherViolate;  //选择路线时角色是否违背
    public bool sameFloorMove;  //是否是同层移动

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
        Layout(roomNo);  //生成初始房间
    }

    void Update()
    {
        

    }

    ///布局函数，传入房间号，读取
    ///点击出口时调用此函数
    public void Layout(int RoomNo) 
    {
        InitRoom();  //移动旧房间的元素到对象池
        ///下面根据表格布置新房间


        Dialogue(RoomNo);  //
    }

    public void InitRoom()  ///清除旧房间
    {
        
    }    

    ///传入csv和行数，返回该行的stringp[]
    public string[] ReadCsv (TextAsset text,int no)
    {
        string[] Data = { "" };
        ///

        return Data;
    }

    ///信任度判定
    public bool TrustJudge()
    {
        bool judge = true;
        ///根据策划案设置判定
        
        return judge;
    }

    ///对话脚本，传入对话库编号
    public void Dialogue(int talkLibNo) 
    {

    }

}






