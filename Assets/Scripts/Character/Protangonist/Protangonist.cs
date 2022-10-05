using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    [SerializeField]    MoveTriggerer moveTriggerer;
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

    #region 角色动作
    /// <summary>
    /// 角色攻击目标
    /// </summary>
    /// <returns></returns>
    public float Attack()//TODO:等待Enemy脚本写完了就吧Enemy作为参数传进去，进行血量计算
    {
        ResetAnim();
        anim.SetTrigger("Attack");
        return atk;
    }
    /// <summary>
    /// 角色死亡
    /// </summary>
    public void Die()//TODO:触发死亡事件，或者由死亡事件来触发Die函数播放动画，总之需要一个事件
    {
        ResetAnim();
        anim.SetTrigger("Die");
    }
    void ResetAnim()//重置主角状态为初始状态（站立）
    {
        anim.SetBool("Moving", false);
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
        set {
            if(value>maxHP)
                hp = maxHP;
            else
                hp = value;
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
            if (value > maxTrust)
                trust = maxTrust;
            else
                trust = value;
            UI_Trust.Instance.UpdateTrustValue();
        }
    }
    /// <summary>
    /// 当前自信等级
    /// </summary>
    public ConfidenceType Confident
    {
        get => confident;
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
                UpdateConfident();
            }
        }
    }
    void UpdateConfident()
    {
        //TODO: 根据配置文件给对应confident等级设置升级所需经验和攻击力等数值
    }
    #endregion

    protected override void Init()//把Unity界面中预先配置的数值更新到游戏内
    {
        base.Init();
        anim = GetComponent<Animator>();
    }
    private void Start()
    {
        speechBubble.Init(this);
        moveTriggerer.MoveTo(new Vector3(1, 1, 1));
    }
    private void Update()
    {
        //暂用于测试，当运行时直接在Unity界面中修改value的text也能让bar变化
        Trust = Trust;
        HP = HP;
    }
}
