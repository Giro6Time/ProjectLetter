using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class UI_Trust : Singleton<UI_Trust>//是个单例
{
    Hero hero;
    [SerializeField]    TMP_Text Value;
    [SerializeField]    TMP_Text Title;//Title似乎没什么需要编程的-，-毕竟不会做Localization
    [SerializeField]    Image Bar;//bar现在用的sprite不是很适合，但是暂时用一用

    public void UpdateTrustValue()
    { 
        Value.text = hero.Trust.ToString();
        UpdateBar(); 
    }
    private void UpdateBar()//更新bar的长度(目前)
    {
        //虽然我认为这个信任条仅仅是一个条的话会很单调，但是现在就先这么写吧-，-
        Bar.fillAmount = (int)(((float)hero.Trust / hero.maxTrust)*10)/(float)10;
    }
    protected override void Init()
    {
        base.Init();
        hero = Hero.Instance;
    }
}
