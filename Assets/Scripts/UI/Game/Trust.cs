using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class Trust : MonoBehaviour
{
    [SerializeField]    TMP_Text Value;
    [SerializeField]    TMP_Text Title;//Title似乎没什么需要编程的-，-毕竟不会做Localization
    [SerializeField]    Image Bar;//bar现在用的sprite不是很适合，但是暂时用一用
    [SerializeField]    int maxValue = 10;

    private int trustValue;
    public int TrustValue
    {
        set 
        { 
            trustValue = value;
            Value.text = value.ToString();
            UpdateBar();
        }
        get => trustValue;
    }
    private void Awake()
    {
        TrustValue = 0;
    }
    private void UpdateBar()//更新bar的长度(目前)
    {
        //虽然我认为这个信任条仅仅是一个条的话会很单调，但是现在就先这么写吧-，-
        Bar.fillAmount = (int)(((float)trustValue / maxValue)*10)/(float)10;
    }

    private void Update()
    {
        //暂用于测试，当运行时修改value的text也能让bar变化
        //当value的text为空的时候理所当然的会报错-，-请不要介意
        
        TrustValue = int.Parse(Value.text); 
    }

}
