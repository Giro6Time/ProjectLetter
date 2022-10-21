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

    private void OnMouseUp()
    {
        InitDoorClickEvent();
        EventManager.EventTrigger("DoorClicked");
    }
    public void InitDoorClickEvent()
    {
        ActionInRoom.SetDoorClickable(false);
        EventManager.RemoveEventListener("DoorClickEventEnd");
        ActionInRoom.DoorChosen = this;
        EventManager.AddEventListener("DoorClickEventEnd", Protagonist.Instance.MoveToDoor);
    }
    
    public void GenerateNewRoom()
    {
        MapGeneration mapGeneration = MapGeneration.Instance;
        //UI_MiniMap.Instance.UpdateMapTo(ToRoomNo);
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
