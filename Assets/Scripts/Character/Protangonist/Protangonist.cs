using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using System;
public enum ConfidenceType
{
    timid = 0,
    confident = 1
}
public class Protangonist : Singleton<Protangonist>,ISpeecher
{
    
    /// <summary>
    /// �������ֵ
    /// </summary>
    public float MaxHealth;
    /// <summary>
    /// �������ֵ
    /// </summary>
    public float maxTrust;
    public float Health;
    public float Trust;
    public ConfidenceType confident;
    public float Attack;
    /// �����ܹ���Ҫ�ľ���ֵ
    public float ExpInNeed;
    /// ��ǰ����ֵ��ÿ��һ������Ϊ0��
    float Experience;
    /// ��ɫ�ƶ��ٶ�

    Animator anim;
    public  MoveTriggerer moveTriggerer;
    public Action OnProtangonistDie;
    #region �Ի����ݽӿ�ʹ��
    // �Ի�������
    [SerializeField]    SpeechBubble speechBubble;
    [SerializeField]    string speecherName;
    public SpeechBubble Bubble { get => speechBubble;
                                 set => speechBubble = value;}
    public string SpeecherName { get => speecherName;
                                 set => speecherName = value;
    }
    #endregion

    #region ��ɫ��Ϊ
    /// <summary>
    /// ��ɫ����Ŀ��
    /// </summary>
    /// <returns></returns>
    public float AttackAnimation()
    {
        ResetAnim();
        anim.SetTrigger("attack");

        return Attack;
    }
    /// <summary>
    /// ��ɫ����
    /// </summary>
    public void ProtangonistDefeated()//TODO:���������¼��������������¼�������Die�������Ŷ�������֮��Ҫһ���¼�
    {
        ResetAnim();
        anim.SetTrigger("die");
    }
    public void Recover(float recoverNum)
    {
        Health += recoverNum;
        ResetAnim();
        anim.SetTrigger("recover");
    }
    void ResetAnim()//��������״̬Ϊ��ʼ״̬��վ����
    {
        anim.SetBool("moving", false);
        //anim.SetBool("Talking", false);
    }
    #endregion

    protected override void Awake()//��Unity������Ԥ�����õ���ֵ���µ���Ϸ��
    {
        base.Awake();
        anim = GetComponent<Animator>();
        speechBubble.Init(this);
    }
    
    private void Start()
    {
    }
    private void Update()
    {
        
    }
}
