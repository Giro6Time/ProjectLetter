using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

/// <summary>
/// 所有需要使用对话框的物体需要使用这个接口
/// 注意在Speecher身上调用SpeechBubble的Init！！！
/// </summary>
public interface Speecher { }
public class SpeechBubble : MonoBehaviour
{
    // 对话框的携带者
    [SerializeField]    Speecher parent;
    //气泡背景图片
    [SerializeField]    Image bubble;
    //气泡中的文本
    public              TMP_Text text;

    LayoutElement bubbleLayoutEle;


    #region 外部调用
    /// <summary>
    /// 在对话气泡的携带者身上调用Init
    /// </summary>
    /// <param name="parent">演讲者自己</param>
    public void Init(Speecher parent) 
    {
        this.parent = parent;
        bubbleLayoutEle = bubble.GetComponent<LayoutElement>();
    }
    /// <summary>
    /// 设置文本内容，更新气泡大小
    /// </summary>
    /// <param name="content"></param>
    public void SetContent(string content)
    {
        text.text = content;
        UpdateBubbleSize();
    }
    #endregion

    void UpdateBubbleSize()
    {
        bubbleLayoutEle.preferredHeight = text.preferredHeight+0.5f;
    }
   
}
