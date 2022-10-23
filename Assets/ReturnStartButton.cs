using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnStartButton : MonoBehaviour
{
    
    public void EnterGame()
    {
        SceneManager.LoadScene("StartScene");
    }
}
