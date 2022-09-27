using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class UI_Property<T> : Singleton<T> where T:class
{
    [SerializeField] protected TMP_Text Value;
    [SerializeField] protected TMP_Text Title;//Title似乎没什么需要编程的-，-毕竟不会做Localization
    [SerializeField] protected Image Bar;//bar现在用的sprite不是很适合，但是暂时用一用
    protected virtual void UpdateBar()//更新bar
    {
    }
}
