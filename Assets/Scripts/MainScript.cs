using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 控制整个游戏进程
/// </summary>
public class MainScript : MonoBehaviour
{
    public static MainScript S;
    public Protangonist hero;
    public bool couldChooseDoor;  //这个变量决定此时鼠标点击出口是否有效
    public bool couldChooseEvent;  //这个变量决定此时鼠标点击事件房内的事件是否有效
    public BetrayType whetherBetray;  //选择路线时角色是否违背,-1是违背，0是没得选，1是遵循
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
        
    }
    private void Start()
    {
        MapGeneration.Instance.GenerateRoom(1, 1);  //生成初始房间   
    }
    void Update()
    {
        

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
                //其他动画
                break;
            default:
                Debug.Log("Fail to find this animation!");
                break;
        }
    }
}
