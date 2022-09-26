using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Singleton<Hero>//�Ǹ�����
{
    public float maxHP;
    public float maxTrust;
    [SerializeField]    float hp;
    [SerializeField]    float trust;
    [SerializeField]    float confident;
    public int level;
    float Experience;

    /// <summary>
    /// ��ɫ����ֵ��С�ڵ��ڽ�ɫ�������ֵ
    /// </summary>
    public float HP
    {
        get => hp;
        set {
            if(value>maxHP)
                hp = maxHP;
            else
                hp = value;
            // UI_Health.Instance.UpdateHP();  //����UI�е�Ѫ����ʾ
        }
    }
    /// <summary>
    /// ��ɫ����ֵ��С�ڵ��ڽ�ɫ�������ֵ
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
        //�����ڲ��ԣ�������ʱ�޸�value��textҲ����bar�仯
        Trust = Trust;
    }
}
