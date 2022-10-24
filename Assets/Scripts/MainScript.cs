using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections;
using UnityEngine.SceneManagement;
/// <summary>
/// 控制整个游戏进程
/// </summary>
public class MainScript : MonoBehaviour
{
    public static MainScript S;
    public Protagonist hero;
    public BetrayType whetherBetray;  //选择路线时角色是否违背,-1是违背，0是没得选，1是遵循
    public bool sameFloorMove;  //是否是同层移动
    public TransitionMask tranMask;
    public DialogueMask dialMask;

    public int floor;
    public int room;
    public int roomNo;
    //事件管理
    List<string[]> ToEvents;
    int eventIndex;//现在正在进行的事件是这个房间的第几个事件
    string[] eventsNow;
    public static bool isTrust;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    void Awake()
    {
        if (S == null)
            S = this;
        floor = 1;
        room = 1;
        roomNo = 1;
        //载入事件
        csvController.GetInstance().loadFile(Application.streamingAssetsPath, "RoomNoToEvents.csv");
        ToEvents = new List<string[]>(csvController.GetInstance().arrayData);
    }
    private void Start()
    {
        TransitionMask.Instance = tranMask;
        DialogueManager.Instance.dialogueMask = dialMask;
        MapGeneration.Instance.GenerateRoom(1, 1);  //生成初始房间
        ActionInRoom.SetDoorClickable(false);
        StartPlayAction();
        Protagonist.Instance.Health = 100;
        Protagonist.Instance.Trust = 4;
        UI_Trust.Instance.gameObject.SetActive(false);
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
        Debug.Log("eventIndex:"+eventIndex+"roomNo:"+roomNo);
        EventManager.Clear();
        EventManager.AddEventListener(eventsNow[eventIndex] + "End", PlayNext);
        GetAction(eventsNow[eventIndex])?.Invoke(eventsNow[eventIndex+1]);
    }
    public void PlayNext()
    {
        eventIndex += 2;
        if(eventIndex<eventsNow.Length && eventsNow[eventIndex]!="")
        {
            PlayActionOnce();
        }
        else
        {
            EventManager.Clear();
            EventManager.AddEventListener("DoorClicked",ActionInRoom.SelectDoor);
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
            case "Choose": return ActionInRoom.Choose;
            case "Animation": return ActionInRoom.Animation;
            case "Recover": return ActionInRoom.Recover;
            case "TrustUp": return ActionInRoom.TrustUp;
            case "End": return End;
            
            default:Debug.Log("Fail to get action"); return null;
        }
    }
    
    string[] GetRoomEvent(int roomNo)
    {
        return ToEvents[roomNo];
    }
    public void End(string qwq)
    {
        EventManager.Clear();
        isTrust = Protagonist.Instance.isTrust;
        TransitionMask.Instance.PlayFadeOutAnimation();
        StartCoroutine(LoadEndScene());
    }
    public IEnumerator LoadEndScene()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("EndScene");
    }
}

public enum BetrayType
{
    betray = -1,//违背
    oneway = 0,
    follow = 1
}
