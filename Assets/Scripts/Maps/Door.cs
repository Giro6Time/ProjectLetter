using System.Collections;
using System.Collections.Generic;
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
        ActionInRoom.SetDoorClickable(false);
        Protangonist.Instance.moveTriggerer.ClearArriveListener();
        Debug.Log("ToFloor" + ToFloor + " ToRoom:" + ToRoom + " ToRoomNo:" + ToRoomNo);
        Protangonist.Instance.moveTriggerer.AddArriveListener(GenerateNewRoom);
        Protangonist.Instance.moveTriggerer.MoveTo(this.transform.position);
    }
    private void GenerateNewRoom()
    {
        MapGeneration mapGeneration = MapGeneration.Instance;
        if(mapGeneration.GenerateRoom(ToFloor, ToRoom) == false)
        {
            Debug.Log("Generation Failed!");
        }
        MainScript.S.room = ToRoom;
        MainScript.S.floor = ToFloor;
        MainScript.S.roomNo = ToRoomNo;
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
