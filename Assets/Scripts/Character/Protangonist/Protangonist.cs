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
    /// �������ֵ
    /// </summary>
    public float maxHP;
    /// <summary>
    /// �������ֵ
    /// </summary>
    public float maxTrust;
    [SerializeField]    float hp;
    [SerializeField]    float trust;
    [SerializeField]    ConfidenceType confident;
    [SerializeField]    float atk;
    /// �����ܹ���Ҫ�ľ���ֵ
    [SerializeField]    float ExpInNeed;
    /// ��ǰ����ֵ��ÿ��һ������Ϊ0��
    float Experience;
    /// ��ɫ�ƶ��ٶ�

    Animator anim;
    public              MoveTriggerer moveTriggerer;
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
    public float Attack()//TODO:�ȴ�Enemy�ű�д���˾Ͱ�Enemy��Ϊ��������ȥ������Ѫ������
    {
        ResetAnim();
        anim.SetTrigger("attack");
        return atk;
    }
    /// <summary>
    /// ��ɫ����
    /// </summary>
    public void Die()//TODO:���������¼��������������¼�������Die�������Ŷ�������֮��Ҫһ���¼�
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
    void ResetAnim()//��������״̬Ϊ��ʼ״̬��վ����
    {
        anim.SetBool("moving", false);
        //anim.SetBool("Talking", false);
    }
    #endregion

    #region �ⲿ���ʵ�����
    /// <summary>
    /// ��ɫ����ֵ��С�ڵ��ڽ�ɫ�������ֵ
    /// </summary>
    public float HP
    {
        get => hp;
        set 
        {
            hp = value;
            Mathf.Clamp(hp, 0, maxHP);
            UI_Health.Instance.UpdateHPValue();  //����UI�е�Ѫ����ʾ
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
            trust = value;
            Mathf.Clamp(trust, 0, maxTrust);
            UI_Trust.Instance.UpdateTrustValue();
        }
    }
    /// <summary>
    /// ��ǰ���ŵȼ�
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
    /// ��ɫ�����ŵȼ�����ֵ
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
        //TODO: ��������Ȼ����£�����������ζ��޸Ĺ�����ʲô�ģ�
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
        //�����ڲ��ԣ�������ʱֱ����Unity�������޸�value��textҲ����bar�仯
        Trust = Trust;
        HP = HP;
    }
}
