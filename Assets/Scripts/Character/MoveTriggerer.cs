using System.Collections;
using UnityEngine;
using System;
/// <summary>
/// ���ƽ�ɫ�ƶ���ͬʱ�����ƶ�����
/// ��ҪЯ����ӵ��Animator���
/// </summary>
public class MoveTriggerer : MonoBehaviour
{
    //�ƶ��ٶ�
    public float movingSpeed;
    public string movingParameter;
    Animator anim;
    Action ArriveTarget;
    /// <summary>
    /// �ƶ���Ŀ��λ��
    /// ע�����ƶ�ǰ��Ҫ���ö���״̬
    /// </summary>
    /// <param name="target"></param>
    public void MoveTo(Vector3 target)
    {
        StartCoroutine(DoMove(target));
    }
    IEnumerator DoMove(Vector3 target)
    {
        anim.SetBool(movingParameter, true);
        while (true)
        {
            Vector3 dir = target - transform.position;
            if (dir.magnitude <= 0.1f)
            {
                anim.SetBool(movingParameter, false);
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
    /// <summary>
    /// ����ƶ�����ʱ�������¼�
    /// </summary>
    /// <param name="action"></param>
    public void AddArriveListener(Action action)
    {
        ArriveTarget += action;
    }
    /// <summary>
    /// ǿ�ƴ����ƶ�����ʱ���¼����ƶ��������
    /// </summary>
    public void TriggerArrive()
    {
        ArriveTarget?.Invoke();
    }
    /// <summary>
    /// ɾ��һ���ƶ�����ʱ�������¼�
    /// </summary>
    /// <param name="action"></param>
    public void RemoveArriveListener(Action action)
    {
        ArriveTarget -= action;
    }
    /// <summary>
    /// ����ƶ�����ʱ�������¼�
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
