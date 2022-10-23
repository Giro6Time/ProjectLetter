using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenePlay : MonoBehaviour
{
    public int ScenePlayStatus = 0;
    public float buttonClickedTime;
    CanvasGroup thisCanvasGroup;
    Animator thisAnimator;

    public GameObject ButtonObj;
    public GameObject BlackScreenObj;

    [Header("按钮隐藏时间")]
    public float buttonHideTime = 0.5f;

    public void SetAnimationPlayEnd()
    {
        ScenePlayStatus = 3;
    }

    private void Start()
    {
        thisCanvasGroup = ButtonObj.GetComponent<CanvasGroup>();
        thisAnimator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        float currentTime = Time.time;
        switch (ScenePlayStatus)
        {
            case 1:
                float t = currentTime - buttonClickedTime;
                float buttonAlpha = Mathf.Clamp((1.0f / buttonHideTime) * -t + 1, 0, 1);
                thisCanvasGroup.alpha = buttonAlpha;
                if (t > buttonHideTime)
                {
                    ScenePlayStatus = 2;
                    ButtonObj.SetActive(false);
                } 
                break;
            case 2:
                thisAnimator.SetBool("StartPlay", true);
                break;
            case 3:
                BlackScreenObj.SetActive(true);
                break;
            case 4:
                SceneManager.LoadScene("Game");
                break;
            default:
                break;
        }

    }
}
