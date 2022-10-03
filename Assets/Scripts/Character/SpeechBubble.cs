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
public interface Speecher { }
public class SpeechBubble : MonoBehaviour
{
    // �Ի����Я����
    [SerializeField]    Speecher parent;
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
    public void Init(Speecher parent) 
    {
        this.parent = parent;
        bubbleLayoutEle = bubble.GetComponent<LayoutElement>();
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
