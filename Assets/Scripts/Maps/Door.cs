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
        Debug.Log("ToFloor" + ToFloor + "ToRoom:" + ToRoom + "ToRoomNo:" + ToRoomNo);
        MapGeneration mapGeneration = MapGeneration.Instance;
        if(mapGeneration.GenerateRoom(ToFloor, ToRoomNo) == false)
        {
            Debug.Log("Generation Failed!");
        }
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

}
