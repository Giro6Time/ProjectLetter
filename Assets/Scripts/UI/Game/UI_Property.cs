using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class UI_Property<T> : Singleton<T> where T:class
{
    [SerializeField] protected TMP_Text Value;
    [SerializeField] protected TMP_Text Title;//Title�ƺ�ûʲô��Ҫ��̵�-��-�Ͼ�������Localization
    [SerializeField] protected Image Bar;//bar�����õ�sprite���Ǻ��ʺϣ�������ʱ��һ��
    protected virtual void UpdateBar()//����bar
    {
    }
}
