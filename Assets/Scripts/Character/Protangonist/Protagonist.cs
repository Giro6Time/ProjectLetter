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
public class Protagonist : Singleton<Protagonist>,ISpeecher
{
    
    // 最大生命值
    public float maxHealth;
    // 最大信任值
    public float maxTrust;
    /// 当前生命值，供外部使用的属性已添加在角色属性region
    private float health;
    /// 当前信任值，供外部使用的属性已添加在角色属性region
    private float trust;
    public ConfidenceType confident;
    public float Attack;
    // 升级总共需要的经验值
    public float ExpInNeed;
    // 当前经验值（每升一级重置为0）
    float Experience;

    // 主角携带的Animator组件
    Animator anim;
    // 主角携带的移动组件
    public  MoveTriggerer moveTriggerer;
    // 当主角死亡的时候触发的Action
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
    #region 角色属性
    /// <summary>
    /// 主角当前生命值
    /// 修改此值会同步修改UI中生命条的显示
    /// </summary>
    public float Health
    {
        get => health;
        set
        {
            health = value;
            Mathf.Clamp(health, 0, maxHealth);
            UI_Health.Instance.UpdateHPValue();
        }
    }
    public float Trust
    {
        get => trust;
        set
        {
            trust = value;
            Mathf.Clamp(trust, 0, maxTrust);
            UI_Trust.Instance.UpdateTrustValue();
        }
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
        anim.SetTrigger("Attack");

        return Attack;
    }
    /// <summary>
    /// 角色死亡
    /// </summary>
    public void ProtagonistDefeated()//TODO:触发死亡事件，或者由死亡事件来触发Die函数播放动画，总之需要一个事件
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
    }
    
    private void Start()
    {
        speechBubble.Init(this);
    }
    private void Update()
    {
        
    }
}
