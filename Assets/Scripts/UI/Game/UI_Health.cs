using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Health : UI_Property<UI_Health>//�Ǹ�����
{
    Protangonist hero;

    public void UpdateHPValue()
    {
        Value.text = hero.HP.ToString();
        UpdateBar();
    }
    protected override void UpdateBar()//����bar�ĳ���(Ŀǰ)
    {
        //��Ȼ����Ϊ���������������һ�����Ļ���ܵ������������ھ�����ôд��-��-
        Bar.fillAmount = (int)(((float)hero.HP / hero.maxHP) * 10) / (float)10;
    }
    private void Start()
    {
        hero = Protangonist.Instance;
    }
}
