using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
/// <summary>
/// 控制整个游戏进程
/// </summary>
public class MainScript : MonoBehaviour
{
    public static MainScript S;
    public Protagonist hero;
    public bool couldChooseDoor;  //这个变量决定此时鼠标点击出口是否有效
    public bool couldChooseEvent;  //这个变量决定此时鼠标点击事件房内的事件是否有效
    public BetrayType whetherBetray;  //选择路线时角色是否违背,-1是违背，0是没得选，1是遵循
    public bool sameFloorMove;  //是否是同层移动

    public int floor;
    public int room;
    public int roomNo;
    //事件管理
    List<string[]> ToEvents;
    int eventIndex;//现在正在进行的事件是这个房间的第几个事件
    string[] eventsNow;

    void Awake()
    {
        if (S == null)
            S = this;
        floor = 1;
        room = 1;
        roomNo = 1;

        //载入事件
        csvController.GetInstance().loadFile(Application.dataPath + "/Scripts/Maps", "RoomNoToEvents.csv");
        ToEvents = new List<string[]>(csvController.GetInstance().arrayData);
    }
    private void Start()
    {
        MapGeneration.Instance.GenerateRoom(1, 1);  //生成初始房间
        ActionInRoom.SetDoorClickable(false);
        StartPlayAction();
    }
    
    
    void Update()
    {
        

    }
    public void StartPlayAction()
    {
        EventManager.Clear();
        eventIndex = 2;
        eventsNow = GetRoomEvent(roomNo);
        PlayActionOnce();
    }
    void PlayActionOnce()
    {
        EventManager.Clear();
        EventManager.AddEventListener(eventsNow[eventIndex] + "End", PlayNext);
        GetAction(eventsNow[eventIndex])?.Invoke(eventsNow[eventIndex+1]);
    }
    void PlayNext()
    {
        eventIndex += 2;
        if(eventIndex<eventsNow.Length && eventsNow[eventIndex]!="")
        {
            PlayActionOnce();
        }
        else
        {
            ActionInRoom.SetDoorClickable(true);
        }
    }
    Action<string> GetAction(string actionName)
    {
        switch (actionName)
        {
            case "Dialogue": return ActionInRoom.Dialogue;
            case "React": return ActionInRoom.React;
            case "Move": return ActionInRoom.Move;
            case "Combat": return ActionInRoom.Combat;
            default:Debug.Log("Fail to get action"); return null;
        }
    }
    
    string[] GetRoomEvent(int roomNo)
    {
        return ToEvents[roomNo];
    }
    ///信任度判定
    public bool TrustJudge()
    {
        bool judge = true;
        ///根据策划案设置判定

        return judge;
    }
    void SetRoom(int floor,int room)
    {
        this.floor = floor;
        this.room = room;
        roomNo = MapGeneration.Instance.FromFloorToRoomNo(floor, room);
    }

}

public enum BetrayType
{
    betray = -1,//违背
    oneway = 0,
    follow = 1

}

public static class ActionInRoom//所有事情都只能有一个string参数，这个参数总是RoomNoToEvent里面对应事件的下一个格里的字符串
{
    public static void Dialogue(string contentType)
    {
        //DialogueManager.Instance.ClearEndListener();
        DialogueManager.Instance.SetLine(contentType);
        DialogueManager.Instance.PlayDialogue();
    }
    public static void React(string content)//这个是否要直接开启对应的对话？
    {
        EventManager.AddEventListener("DialogueEnd", UpdateTrust);
        switch (MainScript.S.whetherBetray)
        {
            case BetrayType.betray:
                DialogueManager.Instance.SetLine(RoomNoToRoomType(MainScript.S.roomNo) + "_betray");
                break;
            case BetrayType.oneway:
                DialogueManager.Instance.SetLine(RoomNoToRoomType(MainScript.S.roomNo) + "_oneway");
                break;
            case BetrayType.follow:
                DialogueManager.Instance.SetLine(RoomNoToRoomType(MainScript.S.roomNo) + "_follow");
                break;
            default:
                return;
        }
        DialogueManager.Instance.PlayDialogue();
        
    }
    public static void Combat(string enemy)
    {
        string[] tmp = enemy.Split("|");
        CombatSystem.Instance.SetEnemyByName(tmp[0]);
        CombatSystem.Instance.enemy.Attack = float.Parse(tmp[1]);
        CombatSystem.Instance.enemy.Health = float.Parse(tmp[2]);
        CombatSystem.Instance.StartCombat();
    }
    public static string RoomNoToRoomType(int RoomNo)
    {
        csvController.GetInstance().loadFile(Application.dataPath + "/Scripts/Maps", "Layout.csv");
        return csvController.GetInstance().getString(RoomNo , 1);
    }
    public static void Move(string xy)
    {
        string[] tmp = xy.Split("|");
        Protagonist.Instance.moveTriggerer.MoveTo(new Vector3(float.Parse(tmp[0]), float.Parse(tmp[1]), 0));
    }
    public static void Animation(string inputStr)
    {
        string[] tmp = inputStr.Split("|");
        Animator anim = GameObject.Find(tmp[0]).GetComponent<Animator>();
        anim.SetTrigger(tmp[1]);
        
    }
    public static void SetDoorClickable(bool clickable)
    {
        for (int i = 0; i < 5; i++)
        {
            MapGeneration.Instance.SceneDoorsObjects[i].GetComponent<Door>().SetCollider(clickable);
        } 
    }
    static void UpdateTrust()
    {
        EventManager.EventTrigger("ReactEnd");
    }

    
}
