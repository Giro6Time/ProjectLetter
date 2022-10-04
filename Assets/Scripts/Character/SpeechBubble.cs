using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

/// <summary>
/// ������Ҫʹ�öԻ����������Ҫʹ������ӿ�
/// ע����Speecher���ϵ���SpeechBubble��Init������
/// </summary>
public class SpeechBubble : MonoBehaviour
{
    // �Ի����Я����
    [SerializeField]    ISpeecher parent;
    //���ݱ���ͼƬ
    [SerializeField]    Image bubble;
    //�����е��ı�
    public              TMP_Text text;

    LayoutElement bubbleLayoutEle;


    #region �ⲿ����
    /// <summary>
    /// �ڶԻ����ݵ�Я�������ϵ���Init
    /// </summary>
    /// <param name="parent">�ݽ����Լ�</param>
    public void Init(ISpeecher parent) 
    {
        this.parent = parent;
        bubbleLayoutEle = bubble.GetComponent<LayoutElement>();
        parent.Register();
    }
    /// <summary>
    /// �����ı����ݣ��������ݴ�С
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
