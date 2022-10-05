using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Test : MonoBehaviour ,ISpeecher

{
    // 实现接口中需要的几个属性
    [SerializeField]SpeechBubble bubble;
    [SerializeField] string speecherName;
    public SpeechBubble Bubble//所有speecher都需要一个对话气泡
    {
        get => bubble;
        set => bubble = value;
    }
    public string SpeecherName//所有speecher都需要一个SpeecherName来提供文本搜索
    {
        get => "Jack";
        set => speecherName = value;
    }
    // Start is called before the first frame update
    //简单来说，在一个speecher生成的时候，需要调用他的bubble.init
    //在触发对话的时候，要在主程序中绑定他使用的对话内容，然后在主程序中开始play
    void Start()
    {
        bubble.Init(this);//对话气泡初始化（绑定parent以及将parent注册到DialogueManager的搜索列表中）
        DialogueManager.Instance.SetLine(15);//绑定当前对话的行数
        DialogueManager.Instance.PlayDialogue();//开始对话（仅需调用一次，后续通过dialoguemask中进行对话，即点击屏幕继续）
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) DialogueManager.Instance.AddEndListener(outHello);
        if (Input.GetKeyDown(KeyCode.B)) DialogueManager.Instance.TriggerEndEvent();//这个事件在对话结束（解除mask）的同时也会trigger一次，不知道用不用的上，反正提供了先
    }
    void outHello()
    {
        Debug.Log("hello");
    }
}
