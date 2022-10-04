using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 演讲者接口，使用方式可以参考主角中的代码
/// </summary>
public interface ISpeecher
{
    /// <summary>
    /// 所有speecher需要拥有一个对话气泡的属性
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
    /// 所有speecher保存于此
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
        dialogueMask.transform.SetSiblingIndex(2);//第一层是data，第二层是对话框的mask//具体UI结构可以再设计
        dialogueMask.gameObject.SetActive(false);
        isSpeeching = false;
    }
    
    public void Play()
    {

    }

    
}
/// <summary>
/// 演讲者的行为
/// </summary>
public static class SpeecherBehavior
{
    /// <summary>
    /// 使用对话气泡让角色说一次话
    /// </summary>
    /// <param name="content"></param>
    public static void Speak(this ISpeecher speecher, string content)
    {
        if (!speecher.Bubble.isActiveAndEnabled) speecher.Bubble.gameObject.SetActive(true);
        speecher.Bubble.SetContent(content);
    }
    /// <summary>
    /// 结束发言
    /// </summary>
    public static void ShutUp(this ISpeecher speecher)//函数名有待商榷（
    {
        if (speecher.Bubble.isActiveAndEnabled)
            speecher.Bubble.gameObject.SetActive(false);
    }
}