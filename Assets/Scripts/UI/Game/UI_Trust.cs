using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class UI_Trust : UI_Property<UI_Trust>//是个单例
{
    Protagonist hero;
    
    public void UpdateTrustValue()
    {
        if(!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }
        float trust = hero.Trust;
        if (trust <= 2) Value.text = "否定";
        else if (trust <= 4) Value.text = "怀疑";
        else if (trust <= 6) Value.text = "半信半疑";
        else if (trust <= 8) Value.text = "相信";
        else Value.text = "服从";
    }
    protected override void UpdateBar()//更新bar的长度(目前)
    {
        
    }
    private void Start()
    {
        hero = Protagonist.Instance;
    }
}
