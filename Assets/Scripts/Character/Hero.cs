using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ConfidenceType
{
    timid = 0,
    confident = 1
}
public class Hero : Singleton<Hero>,Speecher
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
    // �Ի�������
    [SerializeField]    SpeechBubble speechBubble;

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

    #region ��ɫ��Ϊ
    /// <summary>
    /// ������ʹ��һ���Ի�����˵��λ�����������һ�Σ�
    /// </summary>
    /// <param name="contents"></param>
    public void Speak(string[] contents)
    {
        contents[0] = "�߰�������������";
        //TODO: �ø��ӣ�����д
        //�����̣����ǵ�����ж����ɫ����Ի��Ļ��������speak�ͻ�ܼ��ߣ������Ҿ������д
    }
    /// <summary>
    /// ʹ�öԻ�����������˵һ�λ�
    /// </summary>
    /// <param name="content"></param>
    public void Speak(string content)
    {
        if(!speechBubble.isActiveAndEnabled) speechBubble.gameObject.SetActive(true);
        speechBubble.SetContent(content);
    }
    /// <summary>
    /// ��������
    /// </summary>
    public void ShutUp()//�������д���ȶ��
    {
        if(speechBubble.isActiveAndEnabled)
        speechBubble.gameObject.SetActive(false);
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
        if (Input.GetKeyDown(KeyCode.A)) Speak("���Ǹ�ɵ��");
        if (Input.GetKeyDown(KeyCode.B)) Speak("���Ȳ��԰���������������������������������������������������������������������");
        if (Input.GetKeyDown(KeyCode.C)) ShutUp();
    }

}
