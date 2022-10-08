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
        get;set;
    }
    String SpeecherName
    {
        get;set;
    }
    public void RegisterDialogue()
    {
        DialogueManager.Instance.SpeecherDic.Add(SpeecherName, this);
    }
}
/// <summary>
/// 对话系统控制器
/// 关于这个器怎么用，我在character里面添加了一个test用的预设，可以参考，并且这个系统已经用在主角身上了
/// </summary>
public class DialogueManager : Singleton<DialogueManager>
{
    //对话点击事件触发用的mask
    [SerializeField]    DialogueMask dialogueMask;


    /// <summary>
    /// 所有speecher保存于此
    /// </summary>
    public Dictionary<string,ISpeecher> SpeecherDic;

    //是否有人正在说话
    public bool isSpeeching;
    //这一句对话进行到了表格中哪一列（总是指向接下来发生的语句的发言者）
    int currSpeechIndex;
    //这一句对话进行到了哪一句
    public int CurrSpeechIndex
    {
        get => (currSpeechIndex + 1) / 2;
        set => currSpeechIndex = value * 2 - 1;
    }


    //保存整个对话文本库
    List<string[]> textTable;
    //当前行
    string[] line;
    //所有事件名称集
    List<string> eventsName = new List<string>();
    

    /// <summary>
    /// 设置当前正在读的对话
    /// 在触发对话前调用，确保读行不会读歪
    /// </summary>
    /// <param name="contentIndex">这段对话在表格中的行数（excel上写多少就是多少）</param>
    public void SetLine(int contentIndex)
    {
        line = textTable[contentIndex-1];
    }
    /// <summary>
    /// 设置当前正在读的对话
    /// 在触发对话前调用，确保读行不会读歪
    /// </summary>
    /// <param name="eventName"></param>
    public void SetLine(string eventName)
    {
        int index = eventsName.IndexOf(eventName);
        SetLine(index+1);//index比行号少1
    }
    /// <summary>
    /// 播放对话
    /// </summary>
    public void PlayDialogue()
    {
        if (!isSpeeching)//如果当前还没有开始对话，则进入对话，开启mask禁止其他动作
        {
            if (checkSpeechIndex())//礼貌性的检查一下越界
            {
                SpeecherDic[line[currSpeechIndex]].Speak(line[currSpeechIndex + 1]);
                dialogueMask.gameObject.SetActive(true);
                isSpeeching = true;
                ActionInRoom.SetDoorClickable(false);
            }
        }
        else
        {
            currSpeechIndex += 2;
            if(checkSpeechIndex())//如果对话没有结束
            {
                if (SpeecherDic[line[currSpeechIndex]] != SpeecherDic[line[currSpeechIndex - 2]]) //如果下一个发言者和这一个不一样
                    SpeecherDic[line[currSpeechIndex - 2]].ShutUp();//就让这一个闭嘴
                SpeecherDic[line[currSpeechIndex]].Speak(line[currSpeechIndex + 1]);//下一个人讲话
            }
            else//这段对话结束了
            {
                SpeecherDic[line[currSpeechIndex - 2]].ShutUp();//让最后一个人闭嘴
                dialogueMask.gameObject.SetActive(false);//取消mask
                currSpeechIndex = 1;//准备下一次对话   
                isSpeeching = false;
                EventManager.EventTrigger("DialogueEnd");
            }
        }
    }
    

    bool checkSpeechIndex()
    {
        //if (line[currSpeechIndex] == "\r") return false;
        return currSpeechIndex < line.Length && line[currSpeechIndex] != "";
    }
    protected override void Awake()
    {
        base.Awake();
        csvController.GetInstance().loadFile(Application.dataPath + "/Scripts/Dialogue", "Dialogue_Lib.csv");
        textTable = new List<string[]>(csvController.GetInstance().arrayData);
        GetEventsName();

        SpeecherDic = new Dictionary<string, ISpeecher>();
        dialogueMask.transform.SetParent(GameObject.Find("UI").transform);
        dialogueMask.transform.SetSiblingIndex(GameObject.Find("Data").transform.GetSiblingIndex() - 1);//在显示属性的框下一层遮挡，这句可能在后期做UI的时候需要更改
        dialogueMask.gameObject.SetActive(false);
        isSpeeching = false;
        currSpeechIndex = 1;
    }
    void GetEventsName()
    {
        for(int i = 0; i < textTable.Count; ++i)
        {
            eventsName.Add(textTable[i][0]);
        }
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