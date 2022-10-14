using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class SpecialEventManager : Singleton<SpecialEventManager>
{
    GameObject charactorInEvent;
    IPointerClickHandler charactor;
    public void ToChoose(string name)
    {
        //算了还没想好咋写代码，
        charactorInEvent = GameObject.Find(name);
    }
}
