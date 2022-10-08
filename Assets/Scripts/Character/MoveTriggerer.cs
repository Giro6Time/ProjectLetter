using System.Collections;
using UnityEngine;
using System;
/// <summary>
/// 控制角色移动的同时播放移动动画
/// 需要携带者拥有Animator组件
/// </summary>
public class MoveTriggerer : MonoBehaviour
{
    //移动速度
    public float movingSpeed;
    public string movingParameter;
    Animator anim;
    Action ArriveTarget;
    /// <summary>
    /// 移动到目标位置
    /// 注意在移动前需要重置动画状态
    /// </summary>
    /// <param name="target"></param>
    public void MoveTo(Vector3 target)
    {
        StartCoroutine(DoMove(target));
    }
    IEnumerator DoMove(Vector3 target)
    {
        if (target.x < gameObject.transform.position.x) Flip(true);
        anim.SetBool(movingParameter, true);
        while (true)
        {
            Vector3 dir = target - transform.position;
            if (dir.magnitude <= 0.1f)
            {
                anim.SetBool(movingParameter, false);
                Flip(false);
                StopCoroutine(DoMove(target));
                ArriveTarget?.Invoke();
                yield return null;
                break;
            }
            dir = dir.normalized;
            transform.Translate(dir * movingSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
    }
    void Flip(bool isFlip)
    {
        Vector3 sc = transform.localScale;
        if (isFlip)
            transform.localScale = new Vector3(-Math.Abs( sc.x), sc.y, sc.z);
        else
            transform.localScale = new Vector3(Math.Abs(sc.x), sc.y, sc.z);
        
    }
    
    /// <summary>
    /// 添加移动结束时触发的事件
    /// </summary>
    /// <param name="action"></param>
    public void AddArriveListener(Action action)
    {
        ArriveTarget += action;
    }
    /// <summary>
    /// 强制触发移动结束时的事件，移动还会继续
    /// </summary>
    public void TriggerArrive()
    {
        ArriveTarget?.Invoke();
    }
    /// <summary>
    /// 删除一个移动结束时触发的事件
    /// </summary>
    /// <param name="action"></param>
    public void RemoveArriveListener(Action action)
    {
        ArriveTarget -= action;
    }
    /// <summary>
    /// 清空移动结束时的所有事件
    /// </summary>
    public void ClearArriveListener()
    {
        ArriveTarget = null;
    }
    private void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
    }

}
