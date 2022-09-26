using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class UI_Trust : Singleton<UI_Trust>//�Ǹ�����
{
    Hero hero;
    [SerializeField]    TMP_Text Value;
    [SerializeField]    TMP_Text Title;//Title�ƺ�ûʲô��Ҫ��̵�-��-�Ͼ�������Localization
    [SerializeField]    Image Bar;//bar�����õ�sprite���Ǻ��ʺϣ�������ʱ��һ��

    public void UpdateTrustValue()
    { 
        Value.text = hero.Trust.ToString();
        UpdateBar(); 
    }
    private void UpdateBar()//����bar�ĳ���(Ŀǰ)
    {
        //��Ȼ����Ϊ���������������һ�����Ļ���ܵ������������ھ�����ôд��-��-
        Bar.fillAmount = (int)(((float)hero.Trust / hero.maxTrust)*10)/(float)10;
    }
    protected override void Init()
    {
        base.Init();
        hero = Hero.Instance;
    }
}
