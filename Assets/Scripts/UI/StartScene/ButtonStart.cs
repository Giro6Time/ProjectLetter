using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonStart : MonoBehaviour
{
    public ScenePlay scenePlay;

    [Header("镜头拉近参数")]
    public float EndViewSize = 2;
    public float PullInSpeedPerSecond = 2;

    [Header("按钮隐藏时间")]
    public float buttonHideTime = 0.5f;

    Button button;


    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(EnterGame);
        scenePlay = Camera.main.GetComponent<ScenePlay>();
    }

    void EnterGame()
    {
        scenePlay.ScenePlayStatus = 1;
        scenePlay.buttonClickedTime = Time.time;
    }


}
