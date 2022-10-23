using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionMask : Singleton<TransitionMask>
{
    Animator animator;
    // Start is called before the first frame update

    void Start()
    {
        
        animator = GetComponent<Animator>();
    }

    public void PlayFadeInAnimation()
    {
        animator.SetBool("isFadeOut", false);
        animator.SetBool("isFadeIn", true);
        //StartCoroutine(WaitOneSecond());
        //this.gameObject.SetActive(false);
    }
    public IEnumerator WaitOneSecond()
    {
        yield return new WaitForSeconds(1);
    }

    public void PlayFadeOutAnimation()
    {
        //this.gameObject.SetActive(true);
        animator.SetBool("isFadeIn", false);
        animator.SetBool("isFadeOut", true);
        //StartCoroutine(WaitOneSecond());
    }
    void ReturnToStart()
    {
        transform.Find("ReturnStartButton").gameObject.SetActive(true);
    }
}
