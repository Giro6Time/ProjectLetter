using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ConfidenceType
{
    timid = 0,
    confident = 1
}
public class Hero : Singleton<Hero>
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
    /// 当前经验值
    float Experience;
    

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

    #region 角色行为
    //妹说我就不写
#endregion

    protected override void Init()//把Unity界面中预先配置的数值更新到游戏内
    {
        base.Init();
    }
    private void Update()
    {
        //暂用于测试，当运行时修改value的text也能让bar变化
        Trust = Trust;
        HP = HP;
    }
}
