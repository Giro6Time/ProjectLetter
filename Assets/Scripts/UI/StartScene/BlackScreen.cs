using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackScreen : MonoBehaviour
{

    public void SetAnimationPlayEnd()
    {
        Camera cam = Camera.main;
        ScenePlay s = cam.GetComponent<ScenePlay>();
        s.ScenePlayStatus = 4;
    }
}
