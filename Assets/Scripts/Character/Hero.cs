using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Singleton<Hero>//是个单例
{
    public float maxHP;
    public float maxTrust;
    [SerializeField]    float hp;
    [SerializeField]    float trust;
    [SerializeField]    float confident;
    public int level;
    float Experience;

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
            // UI_Health.Instance.UpdateHP();  //更新UI中的血量显示
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
    private void Start()
    {
        Trust = trust;
    }
    private void Update()
    {
        //暂用于测试，当运行时修改value的text也能让bar变化
        Trust = Trust;
    }
}
