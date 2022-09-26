using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����
/// ����Ϸ�����ҽ���һ���ĵ�λ�Ļ���
/// </summary>
abstract public class Singleton<T> :MonoBehaviour where T: class 
{
    public static T Instance;
    protected virtual void Init()
    {
        Instance = this as T;
    }
    protected virtual void Awake()
    {
        Init();
    }
}
