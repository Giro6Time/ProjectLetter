using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class UI_Trust : UI_Property<UI_Trust>//�Ǹ�����
{
    Protangonist hero;
    
    public void UpdateTrustValue()
    {
        Value.text = hero.Trust.ToString();
        UpdateBar(); 
    }
    protected override void UpdateBar()//����bar�ĳ���(Ŀǰ)
    {
        //��Ȼ����Ϊ���������������һ�����Ļ���ܵ������������ھ�����ôд��-��-
        Bar.fillAmount = (int)(((float)hero.Trust / hero.maxTrust)*10)/(float)10;
    }
    private void Start()
    {
        hero = Protangonist.Instance;
    }
}
