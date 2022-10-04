using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class UI_Trust : UI_Property<UI_Trust>//是个单例
{
    Protangonist hero;
    
    public void UpdateTrustValue()
    {
        Value.text = hero.Trust.ToString();
        UpdateBar(); 
    }
    protected override void UpdateBar()//更新bar的长度(目前)
    {
        //虽然我认为这个信任条仅仅是一个条的话会很单调，但是现在就先这么写吧-，-
        Bar.fillAmount = (int)(((float)hero.Trust / hero.maxTrust)*10)/(float)10;
    }
    private void Start()
    {
        hero = Protangonist.Instance;
    }
}
