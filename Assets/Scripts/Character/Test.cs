using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Test : MonoBehaviour ,ISpeecher

{
    // ʵ�ֽӿ�����Ҫ�ļ�������
    [SerializeField]SpeechBubble bubble;
    [SerializeField] string speecherName;
    public SpeechBubble Bubble//����speecher����Ҫһ���Ի�����
    {
        get => bubble;
        set => bubble = value;
    }
    public string SpeecherName//����speecher����Ҫһ��SpeecherName���ṩ�ı�����
    {
        get => "Jack";
        set => speecherName = value;
    }
    // Start is called before the first frame update
    //����˵����һ��speecher���ɵ�ʱ����Ҫ��������bubble.init
    //�ڴ����Ի���ʱ��Ҫ���������а���ʹ�õĶԻ����ݣ�Ȼ�����������п�ʼplay
    void Start()
    {
        bubble.Init(this);//�Ի����ݳ�ʼ������parent�Լ���parentע�ᵽDialogueManager�������б��У�
        DialogueManager.Instance.SetLine(15);//�󶨵�ǰ�Ի�������
        DialogueManager.Instance.PlayDialogue();//��ʼ�Ի����������һ�Σ�����ͨ��dialoguemask�н��жԻ����������Ļ������
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) DialogueManager.Instance.AddEndListener(outHello);
        if (Input.GetKeyDown(KeyCode.B)) DialogueManager.Instance.TriggerEndEvent();//����¼��ڶԻ����������mask����ͬʱҲ��triggerһ�Σ���֪���ò��õ��ϣ������ṩ����
    }
    void outHello()
    {
        Debug.Log("hello");
    }
}
