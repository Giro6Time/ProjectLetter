using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// �ݽ��߽ӿڣ�ʹ�÷�ʽ���Բο������еĴ���
/// </summary>
public interface ISpeecher
{
    /// <summary>
    /// ����speecher��Ҫӵ��һ���Ի����ݵ�����
    /// </summary>
    SpeechBubble Bubble
    {
        get;
    }
    String SpeecherName
    {
        get;
    }
    void Register()
    {
        DialogueManager.Instance.SpeecherDic.Add(SpeecherName, this);
    }
}

public class DialogueManager : Singleton<DialogueManager>
{
    /// <summary>
    /// ����speecher�����ڴ�
    /// </summary>
    public Dictionary<string,ISpeecher> SpeecherDic;
    [SerializeField]    Image dialogueMask;
    bool isSpeeching;
    string dialogueLibPath = Application.dataPath + "Scripts/Dialogue/Dialugue_Lib.txt";
    string[][] textTable;
    protected override void Init()
    {
        base.Init();
        textTable = CSVReader.ReadTable(dialogueLibPath);

        dialogueMask.transform.SetParent(GameObject.Find("UI").transform);
        dialogueMask.transform.SetSiblingIndex(2);//��һ����data���ڶ����ǶԻ����mask//����UI�ṹ���������
        dialogueMask.gameObject.SetActive(false);
        isSpeeching = false;
    }
    
    public void Play()
    {

    }

    
}
/// <summary>
/// �ݽ��ߵ���Ϊ
/// </summary>
public static class SpeecherBehavior
{
    /// <summary>
    /// ʹ�öԻ������ý�ɫ˵һ�λ�
    /// </summary>
    /// <param name="content"></param>
    public static void Speak(this ISpeecher speecher, string content)
    {
        if (!speecher.Bubble.isActiveAndEnabled) speecher.Bubble.gameObject.SetActive(true);
        speecher.Bubble.SetContent(content);
    }
    /// <summary>
    /// ��������
    /// </summary>
    public static void ShutUp(this ISpeecher speecher)//�������д���ȶ��
    {
        if (speecher.Bubble.isActiveAndEnabled)
            speecher.Bubble.gameObject.SetActive(false);
    }
}