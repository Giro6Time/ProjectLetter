using UnityEngine;

public class Door : MonoBehaviour
{
    [Header("The room to go to:")]
    public int ToFloor;
    public int ToRoom;
    public int ToRoomNo;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        InitDoorClickEvent();
        EventManager.EventTrigger("DoorClicked");
    }
    public void InitDoorClickEvent()
    {
        ActionInRoom.SetDoorClickable(false);
        EventManager.AddEventListener("DoorClickEventEnd", MoveToDoor);
    }
    void MoveToDoor()
    {
        Protagonist.Instance.moveTriggerer.ClearArriveListener();
        //Debug.Log("ToFloor" + ToFloor + " ToRoom:" + ToRoom + " ToRoomNo:" + ToRoomNo);
        Protagonist.Instance.moveTriggerer.AddArriveListener(GenerateNewRoom);
        Protagonist.Instance.moveTriggerer.MoveTo(this.transform.position);
        TransitionMask.Instance.PlayFadeOutAnimation();
    }
    private void GenerateNewRoom()
    {
        MapGeneration mapGeneration = MapGeneration.Instance;
        mapGeneration.DestoryRoom(MainScript.S.floor,MainScript.S.room);
        MainScript.S.roomNo = mapGeneration.FromFloorToRoomNo(ToFloor, ToRoom);
        MainScript.S.room = ToRoom;
        MainScript.S.floor = ToFloor;
        if (mapGeneration.GenerateRoom(ToFloor, ToRoom) == false)
        {
            Debug.Log("Generation Failed!");
        }
        TransitionMask.Instance.PlayFadeInAnimation();
        MainScript.S.StartPlayAction();
        //MainScript.S.Layout(ToRoomNo);
    }

    public void SetDoorPatameter(int Floor,int Room)
    {
        this.ToFloor = Floor;
        this.ToRoom = Room;
    }

    public void SetDoorPatameter(int Floor, int Room,int RoomNo)
    {
        this.ToFloor = Floor;
        this.ToRoom = Room;
        this.ToRoomNo = RoomNo;
    }
    public void SetCollider(bool isTrue)
    {
        GetComponent<Collider2D>().enabled = isTrue;
    }
}
