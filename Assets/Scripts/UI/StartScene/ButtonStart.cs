using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ButtonStart : MonoBehaviour
{
    [SerializeField]
    Button button;
    private void Start()
    {
        button.onClick.AddListener(EnterGame);
    }
    void EnterGame()
    {
        SceneManager.LoadScene("Game");
    }
}
