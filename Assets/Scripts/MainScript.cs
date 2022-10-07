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
    public BetrayType whetherBetray;  //ѡ��·��ʱ��ɫ�Ƿ�Υ��,-1��Υ����0��û��ѡ��1����ѭ
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
        
    }
    private void Start()
    {
        MapGeneration.Instance.GenerateRoom(1, 1);  //���ɳ�ʼ����   
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

public class ActionInRoom
{
    public void Dialogue(string contentType)
    {
        DialogueManager.Instance.ClearEndListener();
        DialogueManager.Instance.SetLine("contentType");
        DialogueManager.Instance.PlayDialogue();
    }
    public string React(BetrayType whetherViolate, int RoomNo)
    {
        switch (whetherViolate)
        {
            case BetrayType.betray:
                return RoomNoToRoomType(RoomNo) + "_betray";
            case BetrayType.oneway:
                return RoomNoToRoomType(RoomNo) + "_oneway";
            case BetrayType.follow:
                return RoomNoToRoomType(RoomNo) + "_follow";
            default:
                return null;
        }
    }
    public string RoomNoToRoomType(int RoomNo)
    {
        csvController.GetInstance().loadFile(Application.dataPath + "/Scripts/Maps", "Layout.csv");
        return csvController.GetInstance().getString(RoomNo + 1, 1);
    }
    public void Move(float x, float y)
    {
        Protangonist.Instance.moveTriggerer.MoveTo(new Vector3(x, y, 0));
    }
    public void Animation(string animName)
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
}
