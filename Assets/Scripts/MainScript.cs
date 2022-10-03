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

    void Start()
    {
        if (S == null)
            S = this;
        hero = GameObject.Find("Wizard").GetComponent<Hero>();
        Layout(1);  //生成初始房间
    }

    void Update()
    {
        

    }

    //布局函数，传入房间号，读取
    //点击出口时调用此函数
    public void Layout(int RoomNo) 
    {
        InitRoom();


        
    }

    public void InitRoom()  //清除旧房间
    {
        
    }    

    //传入csv和行数，返回该行的stringp[]
    public string[] ReadCsv (TextAsset text,int no)
    {
        string[] Data = { "" };
        return Data;
    }




}






