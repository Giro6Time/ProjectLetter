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
    /// 最大生命值
    /// </summary>
    public float maxHP;
    /// <summary>
    /// 最大信任值
    /// </summary>
    public float maxTrust;
    [SerializeField]    float hp;
    [SerializeField]    float trust;
    [SerializeField]    ConfidenceType confident;
    [SerializeField]    float atk;
    /// 升级总共需要的经验值
    [SerializeField]    float ExpInNeed;
    /// 当前经验值（每升一级重置为0）
    float Experience;

    [SerializeField]    string halvingLine = "------携带组件↓------";
    // 对话框气泡
    [SerializeField]    SpeechBubble speechBubble;

    #region 外部访问的属性
    /// <summary>
    /// 角色生命值，小于等于角色最大生命值
    /// </summary>
    public float HP
    {
        get => hp;
        set {
            if(value>maxHP)
                hp = maxHP;
            else
                hp = value;
            UI_Health.Instance.UpdateHPValue();  //更新UI中的血量显示
        }
    }
    /// <summary>
    /// 角色信任值，小于等于角色最大信任值
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
    /// 当前自信等级
    /// </summary>
    public ConfidenceType Confident
    {
        get => confident;
    }
    /// <summary>
    /// 角色的自信等级经验值
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
        //TODO: 根据配置文件给对应confident等级设置升级所需经验和攻击力等数值
    }
    #endregion

    #region 角色行为
    /// <summary>
    /// 让主角使用一个对话气泡说多次话（点击鼠标下一段）
    /// </summary>
    /// <param name="contents"></param>
    public void Speak(string[] contents)
    {
        contents[0] = "哼啊啊啊啊啊啊啊";
        //TODO: 好复杂，不想写
        //高情商：考虑到如果有多个角色交叉对话的话，用这个speak就会很鸡肋，所以我决定晚点写
    }
    /// <summary>
    /// 使用对话气泡让主角说一次话
    /// </summary>
    /// <param name="content"></param>
    public void Speak(string content)
    {
        if(!speechBubble.isActiveAndEnabled) speechBubble.gameObject.SetActive(true);
        speechBubble.SetContent(content);
    }
    /// <summary>
    /// 结束发言
    /// </summary>
    public void ShutUp()//函数名有待商榷（
    {
        if(speechBubble.isActiveAndEnabled)
        speechBubble.gameObject.SetActive(false);
    }
#endregion

    protected override void Init()//把Unity界面中预先配置的数值更新到游戏内
    {
        base.Init();
        speechBubble.Init(this);
    }
    private void Update()
    {
        //暂用于测试，当运行时修改value的text也能让bar变化
        Trust = Trust;
        HP = HP;
        if (Input.GetKeyDown(KeyCode.A)) Speak("我是个傻逼");
        if (Input.GetKeyDown(KeyCode.B)) Speak("长度测试啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊");
        if (Input.GetKeyDown(KeyCode.C)) ShutUp();
    }

}
