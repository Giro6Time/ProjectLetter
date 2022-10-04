using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
public class DialogueMask : MonoBehaviour,IPointerClickHandler
{
    Action DialogueEnd;
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        DialogueManager.Instance.PlayDialogue();
    }
}
