using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Health : UI_Property<UI_Health>//是个单例
{
    Protagonist hero;

    public void UpdateHPValue()
    {
        Value.text = hero.Health.ToString();
        UpdateBar();
    }
    protected override void UpdateBar()//更新bar的长度(目前)
    {
        //虽然我认为这个信任条仅仅是一个条的话会很单调，但是现在就先这么写吧-，-
        Bar.fillAmount = (int)(((float)hero.Health / hero.maxHealth) * 10) / (float)10;
    }
    private void Start()
    {
        hero = Protagonist.Instance;
    }
}
