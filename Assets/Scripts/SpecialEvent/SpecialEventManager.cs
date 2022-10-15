using UnityEngine;
using System;
using UnityEngine.EventSystems;
public enum SpecialEventType
{
    Choose
}

public class SpecialEventManager : Singleton<SpecialEventManager>
{
    GameObject charactorInEvent;
    SpecialEventObject eventObject;
    string Name;
    public void StartSpecialEvent(SpecialEventType type,string name)
    {
        Name = name;
        charactorInEvent = GameObject.Find(name);
        eventObject = charactorInEvent.GetComponent<SpecialEventObject>();
        switch (type)
        {
            case SpecialEventType.Choose:
                Choose(); break;
        }

    }
    void Choose()
    {
        ActionInRoom.SetDoorClickable(true);
        eventObject.SetCollider(true);
        switch (Name)
        {
            case "Jack":
                EventManager.AddEventListener("CharactorClicked", GoOn);
                EventManager.AddEventListener("DoorClicked", ToNextDoor);
                break;
        }
    }
    
    void GoOn()
    {
        bool isTrust = Protagonist.Instance.isTrust;
        switch(Name)
        {
            case "Jack":
                ActionInRoom.SetDoorClickable(false);
                eventObject.SetCollider(false);
                if(isTrust)
                    EventManager.EventTrigger("ChooseEnd");
                else
                {
                    int gotoDoor = UnityEngine.Random.Range(0, 4);
                    MapGeneration.Instance.SceneDoorsObjects[gotoDoor].GetComponent<Door>().InitDoorClickEvent();
                    NothingToDoOnDoorClick();
                }
                break;
        }
    }
    void ToNextDoor()
    {
        bool isTrust = Protagonist.Instance.isTrust;

        switch (Name)
        {
            case "Jack":
                if (isTrust)
                    EventManager.EventTrigger("DoorClickEventEnd");
                else
                    EventManager.EventTrigger("ChooseEnd");
                break;
                
        }
    }

    void NothingToDoOnDoorClick()
    {
        EventManager.EventTrigger("DoorClickEventEnd");
    }
}
