using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
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
    public float maxHP;
    /// <summary>
    /// 最大信任值
    /// </summary>
    public float maxTrust;
    [SerializeField]    float hp;
    [SerializeField]    float trust;
    [SerializeField]    ConfidenceType confident;
    [SerializeField]    float atk;
    /// 升级总共需要的经验值
    [SerializeField]    float ExpInNeed;
    /// 当前经验值（每升一级重置为0）
    float Experience;
    /// 角色移动速度

    Animator anim;
    public              MoveTriggerer moveTriggerer;
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
    public float Attack()//TODO:等待Enemy脚本写完了就吧Enemy作为参数传进去，进行血量计算
    {
        ResetAnim();
        anim.SetTrigger("attack");
        return atk;
    }
    /// <summary>
    /// 角色死亡
    /// </summary>
    public void Die()//TODO:触发死亡事件，或者由死亡事件来触发Die函数播放动画，总之需要一个事件
    {
        ResetAnim();
        anim.SetTrigger("die");
    }
    public void Recover(float recoverNum)
    {
        HP += recoverNum;
        ResetAnim();
        anim.SetTrigger("recover");
    }
    void ResetAnim()//重置主角状态为初始状态（站立）
    {
        anim.SetBool("moving", false);
        //anim.SetBool("Talking", false);
    }
    #endregion

    #region 外部访问的属性
    /// <summary>
    /// 角色生命值，小于等于角色最大生命值
    /// </summary>
    public float HP
    {
        get => hp;
        set 
        {
            hp = value;
            Mathf.Clamp(hp, 0, maxHP);
            UI_Health.Instance.UpdateHPValue();  //更新UI中的血量显示
        }
    }
    /// <summary>
    /// 角色信任值，小于等于角色最大信任值
    /// </summary>
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
    /// <summary>
    /// 当前自信等级
    /// </summary>
    public ConfidenceType Confident
    {
        get => confident;
        set 
        { 
            confident = value;
            UpdateProperty();
        }
    }
    /// <summary>
    /// 角色的自信等级经验值
    /// </summary>
    public float EXP
    {
        set
        {
            Experience = value;
            if (Experience > ExpInNeed)
            {
                Experience -= ExpInNeed;
                confident++;
                UpdateProperty();
            }
        }
    }
    void UpdateProperty()
    {
        //TODO: 计算属性然后更新（比如根据信任度修改攻击力什么的）
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
        //暂用于测试，当运行时直接在Unity界面中修改value的text也能让bar变化
        Trust = Trust;
        HP = HP;
    }
}
