using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndSceneController : MonoBehaviour
{
    public AnimationClip CameraSizeUp;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartPlayAnimation());
    }
    IEnumerator StartPlayAnimation()
    {
        float len = CameraSizeUp.length;
        yield return new WaitForSeconds(len);
        EventManager.AddEventListener("DialogueEnd", ShowThanks);
        if(MainScript.isTrust)
        {
            DialogueManager.Instance.SetLine("End_2_1");
        }
        else
        {
            DialogueManager.Instance.SetLine("End_2_2");
        }
        DialogueManager.Instance.PlayDialogue();
    }
    void ShowThanks()
    {
        TransitionMask.Instance.PlayFadeOutAnimation();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
