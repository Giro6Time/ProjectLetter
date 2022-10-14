using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class SpecialEventObject : MonoBehaviour
{
    public void SetCollider(bool isTrue)
    {
        GetComponent<Collider2D>().enabled = isTrue;
    }
    private void OnMouseDown()
    {

    }
}
