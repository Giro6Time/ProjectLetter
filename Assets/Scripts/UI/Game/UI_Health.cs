using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Health : UI_Property<UI_Health>//�Ǹ�����
{
    Protagonist hero;

    public void UpdateHPValue()
    {
        Value.text = hero.Health.ToString();
        UpdateBar();
    }
    protected override void UpdateBar()//����bar�ĳ���(Ŀǰ)
    {
        //��Ȼ����Ϊ���������������һ�����Ļ���ܵ������������ھ�����ôд��-��-
        Bar.fillAmount = (int)(((float)hero.Health / hero.MaxHealth) * 10) / (float)10;
    }
    private void Start()
    {
        hero = Protagonist.Instance;
    }
}
