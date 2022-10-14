using UnityEngine;
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
