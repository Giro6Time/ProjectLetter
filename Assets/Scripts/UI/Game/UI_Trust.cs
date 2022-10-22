using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class UI_Trust : UI_Property<UI_Trust>//�Ǹ�����
{
    Protagonist hero;
    
    public void UpdateTrustValue()
    {
        if(!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }
        float trust = hero.Trust;
        if (trust <= 2) Value.text = "��";
        else if (trust <= 4) Value.text = "����";
        else if (trust <= 6) Value.text = "���Ű���";
        else if (trust <= 8) Value.text = "����";
        else Value.text = "����";
    }
    protected override void UpdateBar()//����bar�ĳ���(Ŀǰ)
    {
        
    }
    private void Start()
    {
        hero = Protagonist.Instance;
    }
}
