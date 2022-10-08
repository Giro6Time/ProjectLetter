using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
/// <summary>
/// ����������Ϸ����
/// </summary>
public class MainScript : MonoBehaviour
{
    public static MainScript S;
    public Protangonist hero;
    public bool couldChooseDoor;  //�������������ʱ����������Ƿ���Ч
    public bool couldChooseEvent;  //�������������ʱ������¼����ڵ��¼��Ƿ���Ч
    public BetrayType whetherBetray;  //ѡ��·��ʱ��ɫ�Ƿ�Υ��,-1��Υ����0��û��ѡ��1����ѭ
    public bool sameFloorMove;  //�Ƿ���ͬ���ƶ�

    public int floor;
    public int room;
    public int roomNo;
    //�¼�����
    List<string[]> ToEvents;
    int eventIndex;//�������ڽ��е��¼����������ĵڼ����¼�
    string[] eventsNow;

    void Awake()
    {
        if (S == null)
            S = this;
        floor = 1;
        room = 1;
        roomNo = 1;

        //�����¼�
        csvController.GetInstance().loadFile(Application.dataPath + "/Scripts/Maps", "RoomNoToEvents.csv");
        ToEvents = new List<string[]>(csvController.GetInstance().arrayData);
    }
    private void Start()
    {
        MapGeneration.Instance.GenerateRoom(1, 1);  //���ɳ�ʼ����
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
            default:Debug.Log("Fail to get action"); return null;
        }
    }
    
    string[] GetRoomEvent(int roomNo)
    {
        return ToEvents[roomNo];
    }
    ///���ζ��ж�
    public bool TrustJudge()
    {
        bool judge = true;
        ///���ݲ߻��������ж�

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
    betray = -1,//Υ��
    oneway = 0,
    follow = 1

}

public static class ActionInRoom//�������鶼ֻ����һ��string�����������������RoomNoToEvent�����Ӧ�¼�����һ��������ַ���
{
    public static void Dialogue(string contentType)
    {
        //DialogueManager.Instance.ClearEndListener();
        DialogueManager.Instance.SetLine(contentType);
        DialogueManager.Instance.PlayDialogue();
    }
    public static string React(string content)
    {
        switch (MainScript.S.whetherBetray)
        {
            case BetrayType.betray:
                return RoomNoToRoomType(MainScript.S.roomNo) + "_betray";
            case BetrayType.oneway:
                return RoomNoToRoomType(MainScript.S.roomNo) + "_oneway";
            case BetrayType.follow:
                return RoomNoToRoomType(MainScript.S.roomNo) + "_follow";
            default:
                return null;
        }
    }
    public static string RoomNoToRoomType(int RoomNo)
    {
        csvController.GetInstance().loadFile(Application.dataPath + "/Scripts/Maps", "Layout.csv");
        return csvController.GetInstance().getString(RoomNo + 1, 1);
    }
    public static void Move(string xy)
    {
        string[] tmp = xy.Split("|");
        Protangonist.Instance.moveTriggerer.MoveTo(new Vector3(float.Parse(tmp[0]), float.Parse(tmp[1]), 0));
    }
    public static void Animation(string animName)
    {
        switch (animName)
        {
            case "recover":
                Protangonist.Instance.Recover(100);
                //��������
                break;
            default:
                Debug.Log("Fail to find this animation!");
                break;
        }
    }
    public static void SetDoorClickable(bool clickable)
    {
        for (int i = 0; i < 5; i++)
        {
            MapGeneration.Instance.SceneDoorsObjects[i].GetComponent<Door>().SetCollider(clickable);
        } 
    }


    
}
