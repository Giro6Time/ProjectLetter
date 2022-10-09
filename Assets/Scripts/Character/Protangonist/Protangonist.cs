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
    /// 最大生命值
    /// </summary>
    public float MaxHealth;
    /// <summary>
    /// 最大信任值
    /// </summary>
    public float maxTrust;
    public float Health;
    public float Trust;
    public ConfidenceType confident;
    public float Attack;
    /// 升级总共需要的经验值
    public float ExpInNeed;
    /// 当前经验值（每升一级重置为0）
    float Experience;
    /// 角色移动速度

    Animator anim;
    public  MoveTriggerer moveTriggerer;
    public Action OnProtangonistDie;
    #region 对话气泡接口使用
    // 对话框气泡
    [SerializeField]    SpeechBubble speechBubble;
    [SerializeField]    string speecherName;
    public SpeechBubble Bubble { get => speechBubble;
                                 set => speechBubble = value;}
    public string SpeecherName { get => speecherName;
                                 set => speecherName = value;
    }
    #endregion

    #region 角色行为
    /// <summary>
    /// 角色攻击目标
    /// </summary>
    /// <returns></returns>
    public float AttackAnimation()
    {
        ResetAnim();
        anim.SetTrigger("attack");

        return Attack;
    }
    /// <summary>
    /// 角色死亡
    /// </summary>
    public void ProtangonistDefeated()//TODO:触发死亡事件，或者由死亡事件来触发Die函数播放动画，总之需要一个事件
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
    void ResetAnim()//重置主角状态为初始状态（站立）
    {
        anim.SetBool("moving", false);
        //anim.SetBool("Talking", false);
    }
    #endregion

    protected override void Awake()//把Unity界面中预先配置的数值更新到游戏内
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
