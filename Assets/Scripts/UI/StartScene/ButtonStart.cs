using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ButtonStart : MonoBehaviour
{
    public Button button;

    bool StartButton = false;
    private void Start()
    {
        button.onClick.AddListener(EnterGame);
    }

    private void FixedUpdate()
    {
        if (StartButton)
        {

        }
    }

    void EnterGame()
    {
        StartButton = true;
        float EnterStartTime = Time.time;


        SceneManager.LoadScene("Game");
    }
}
