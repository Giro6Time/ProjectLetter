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

    [SerializeField]    string halvingLine = "------Я�������------";
    #region �Ի����ݽӿ�ʹ��ʾ��
    // �Ի�������
    [SerializeField]    SpeechBubble speechBubble;
    public SpeechBubble Bubble { get => speechBubble; }
    public string SpeecherName { get => "protagonist"; }
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
        speechBubble.Init(this);
    }
    private void Update()
    {
        //�����ڲ��ԣ�������ʱ�޸�value��textҲ����bar�仯
        Trust = Trust;
        HP = HP;
    }

}
