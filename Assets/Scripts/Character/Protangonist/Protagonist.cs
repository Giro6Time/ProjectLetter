using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using System;
public enum ConfidenceType
{
    timid = 0,
    confident = 1
}
public class Protagonist : Singleton<Protagonist>,ISpeecher
{
    
    // 鏈€澶х敓鍛藉€�
    public float maxHealth;
    // 鏈€澶т俊浠诲€�
    public float maxTrust;
    /// 褰撳墠鐢熷懡鍊硷紝渚涘�栭儴浣跨敤鐨勫睘鎬у凡娣诲姞鍦ㄨ�掕壊灞炴€�region
    private float health;
    /// 褰撳墠淇′换鍊硷紝渚涘�栭儴浣跨敤鐨勫睘鎬у凡娣诲姞鍦ㄨ�掕壊灞炴€�region
    private float trust;
    public ConfidenceType confident;
    public float Attack;
    // 鍗囩骇鎬诲叡闇€瑕佺殑缁忛獙鍊�
    public float ExpInNeed;
    // 褰撳墠缁忛獙鍊硷紙姣忓崌涓€绾ч噸缃�涓�0锛�
    float Experience;

    // 涓昏�掓惡甯︾殑Animator缁勪欢
    Animator anim;
    // 涓昏�掓惡甯︾殑绉诲姩缁勪欢
    public  MoveTriggerer moveTriggerer;
    // 褰撲富瑙掓�讳骸鐨勬椂鍊欒Е鍙戠殑Action
    public Action OnProtangonistDie;
    #region 瀵硅瘽姘旀场鎺ュ彛浣跨敤
    // 瀵硅瘽妗嗘皵娉�
    [SerializeField]    SpeechBubble speechBubble;
    [SerializeField]    string speecherName;
    public SpeechBubble Bubble { get => speechBubble;
                                 set => speechBubble = value;}
    public string SpeecherName { get => speecherName;
                                 set => speecherName = value;
    }
    #endregion
    #region 瑙掕壊灞炴€�
    /// <summary>
    /// 涓昏�掑綋鍓嶇敓鍛藉€�
    /// 淇�鏀规�ゅ€间細鍚屾�ヤ慨鏀筓I涓�鐢熷懡鏉＄殑鏄剧ず
    /// </summary>
    public float Health
    {
        get => health;
        set
        {
            health = value;
            Mathf.Clamp(health, 0, maxHealth);
            UI_Health.Instance.UpdateHPValue();
        }
    }
    public float Trust
    {
        get => trust;
        set
        {
            trust = value;
            Mathf.Clamp(trust, 0, maxTrust);
            UI_Trust.Instance.UpdateTrustValue();
        }
    }
    #endregion
    #region 瑙掕壊琛屼负
    /// <summary>
    /// 瑙掕壊鏀诲嚮鐩�鏍�
    /// </summary>
    /// <returns></returns>
    public float AttackAnimation()
    {
        ResetAnim();
        anim.SetTrigger("Attack");

        return Attack;
    }
    /// <summary>
    /// 瑙掕壊姝讳骸
    /// </summary>
    public void ProtagonistDefeated()//TODO:瑙﹀彂姝讳骸浜嬩欢锛屾垨鑰呯敱姝讳骸浜嬩欢鏉ヨЕ鍙慏ie鍑芥暟鎾�鏀惧姩鐢伙紝鎬讳箣闇€瑕佷竴涓�浜嬩欢
    {
        ResetAnim();
        anim.SetTrigger("die");
    }
    public void Recover(float recoverNum)
    {
        Health += recoverNum;
        ResetAnim();
        anim.SetTrigger("recover");
    }
    void ResetAnim()//閲嶇疆涓昏�掔姸鎬佷负鍒濆�嬬姸鎬侊紙绔欑珛锛�
    {
        anim.SetBool("moving", false);
        //anim.SetBool("Talking", false);
    }
    #endregion

    protected override void Awake()//鎶奤nity鐣岄潰涓�棰勫厛閰嶇疆鐨勬暟鍊兼洿鏂板埌娓告垙鍐�
    {
        base.Awake();
        anim = GetComponent<Animator>();
    }
    
    private void Start()
    {
        speechBubble.Init(this);
    }
    private void Update()
    {
        
    }
}
