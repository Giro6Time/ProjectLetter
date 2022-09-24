using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class Trust : MonoBehaviour
{
    [SerializeField]    TMP_Text Value;
    [SerializeField]    TMP_Text Title;//Title�ƺ�ûʲô��Ҫ��̵�-��-�Ͼ�������Localization
    [SerializeField]    Image Bar;//bar�����õ�sprite���Ǻ��ʺϣ�������ʱ��һ��
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
    private void UpdateBar()//����bar�ĳ���(Ŀǰ)
    {
        //��Ȼ����Ϊ���������������һ�����Ļ���ܵ������������ھ�����ôд��-��-
        Bar.fillAmount = (int)(((float)trustValue / maxValue)*10)/(float)10;
    }

    private void Update()
    {
        //�����ڲ��ԣ�������ʱ�޸�value��textҲ����bar�仯
        //��value��textΪ�յ�ʱ��������Ȼ�Ļᱨ��-��-�벻Ҫ����
        
        TrustValue = int.Parse(Value.text); 
    }

}
