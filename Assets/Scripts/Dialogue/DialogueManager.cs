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
    public void RegisterDialogue()
    {
        DialogueManager.Instance.SpeecherDic.Add(SpeecherName, this);
    }
}
/// <summary>
/// �Ի�ϵͳ������
/// �����������ô�ã�����character����������һ��test�õ�Ԥ�裬���Բο����������ϵͳ�Ѿ���������������
/// </summary>
public class DialogueManager : Singleton<DialogueManager>
{
    //�Ի�����¼������õ�mask
    [SerializeField]    DialogueMask dialogueMask;


    /// <summary>
    /// ����speecher�����ڴ�
    /// </summary>
    public Dictionary<string,ISpeecher> SpeecherDic;

    //�Ƿ���������˵��
    public bool isSpeeching;
    //��һ��Ի����е��˱�������һ�У�����ָ����������������ķ����ߣ�
    int currSpeechIndex;
    //��һ��Ի����е�����һ��
    public int CurrSpeechIndex
    {
        get => (currSpeechIndex + 1) / 2;
        set => currSpeechIndex = value * 2 - 1;
    }


    //dialog��ĵ�ַ
    string dialogueLibPath = "/Scripts/Dialogue";
    //���������Ի��ı���
    List<string[]> textTable;
    //��ǰ��
    string[] line;

    Action DialogueEnd;
    

    /// <summary>
    /// ���õ�ǰ���ڶ��ĶԻ�
    /// �ڴ����Ի�ǰ��һ˲����ã�ȷ�����в������
    /// </summary>
    /// <param name="contentIndex">��ζԻ��ڱ����е�������excel��д���پ��Ƕ��٣�</param>
    public void SetLine(int contentIndex)
    {
        line = textTable[contentIndex-1];
    }

    /// <summary>
    /// ���ŶԻ�
    /// </summary>
    public void PlayDialogue()
    {
        if (!isSpeeching)//�����ǰ��û�п�ʼ�Ի��������Ի�������mask��ֹ��������
        {
            if (checkSpeechIndex())//��ò�Եļ��һ��Խ��
            {
                SpeecherDic[line[currSpeechIndex]].Speak(line[currSpeechIndex + 1]);
                dialogueMask.gameObject.SetActive(true);
                isSpeeching = true;
            }
        }
        else
        {
            currSpeechIndex += 2;
            if(checkSpeechIndex())//����Ի�û�н���
            {
                if (SpeecherDic[line[currSpeechIndex]] != SpeecherDic[line[currSpeechIndex - 2]]) //�����һ�������ߺ���һ����һ��
                    SpeecherDic[line[currSpeechIndex - 2]].ShutUp();//������һ������
                SpeecherDic[line[currSpeechIndex]].Speak(line[currSpeechIndex + 1]);//��һ���˽���
            }
            else//��ζԻ�������
            {
                SpeecherDic[line[currSpeechIndex - 2]].ShutUp();//�����һ���˱���
                dialogueMask.gameObject.SetActive(false);//ȡ��mask
                currSpeechIndex = 1;//׼����һ�ζԻ�   
                TriggerEndEvent();

            }
        }
    }
    #region �����Ի�ʱ���¼�����
    public void AddEndListener(Action action)
    {
        DialogueEnd += action;
    }
    public void TriggerEndEvent()
    {
        DialogueEnd?.Invoke();
    }
    public void RemoveEndListener(Action action)
    {
        DialogueEnd -= action;
    }
    public void ClearEndListener()
    {
        DialogueEnd = null;
    }
    #endregion

    bool checkSpeechIndex()
    {
        //if (line[currSpeechIndex] == "\r") return false;
        return currSpeechIndex < line.Length && line[currSpeechIndex] != "";
    }

    protected override void Init()
    {
        base.Init();
        csvController.GetInstance().loadFile(Application.dataPath + dialogueLibPath, "Dialogue_Lib.txt");
        textTable = csvController.GetInstance().arrayData;

        SpeecherDic = new Dictionary<string, ISpeecher>();
        dialogueMask.transform.SetParent(GameObject.Find("UI").transform);
        dialogueMask.transform.SetSiblingIndex(GameObject.Find("Data").transform.GetSiblingIndex()-1);//����ʾ���ԵĿ���һ���ڵ����������ں�����UI��ʱ����Ҫ����
        dialogueMask.gameObject.SetActive(false);
        isSpeeching = false;
        currSpeechIndex = 1;
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