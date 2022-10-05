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
    [SerializeField]    MoveTriggerer moveTriggerer;
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

    #region ��ɫ����
    /// <summary>
    /// ��ɫ����Ŀ��
    /// </summary>
    /// <returns></returns>
    public float Attack()//TODO:�ȴ�Enemy�ű�д���˾Ͱ�Enemy��Ϊ��������ȥ������Ѫ������
    {
        ResetAnim();
        anim.SetTrigger("Attack");
        return atk;
    }
    /// <summary>
    /// ��ɫ����
    /// </summary>
    public void Die()//TODO:���������¼��������������¼�������Die�������Ŷ�������֮��Ҫһ���¼�
    {
        ResetAnim();
        anim.SetTrigger("Die");
    }
    void ResetAnim()//��������״̬Ϊ��ʼ״̬��վ����
    {
        anim.SetBool("Moving", false);
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
        set {
            if(value>maxHP)
                hp = maxHP;
            else
                hp = value;
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
            if (value > maxTrust)
                trust = maxTrust;
            else
                trust = value;
            UI_Trust.Instance.UpdateTrustValue();
        }
    }
    /// <summary>
    /// ��ǰ���ŵȼ�
    /// </summary>
    public ConfidenceType Confident
    {
        get => confident;
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
                UpdateConfident();
            }
        }
    }
    void UpdateConfident()
    {
        //TODO: ���������ļ�����Ӧconfident�ȼ������������辭��͹���������ֵ
    }
    #endregion

    protected override void Init()//��Unity������Ԥ�����õ���ֵ���µ���Ϸ��
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
        //�����ڲ��ԣ�������ʱֱ����Unity�������޸�value��textҲ����bar�仯
        Trust = Trust;
        HP = HP;
    }
}
