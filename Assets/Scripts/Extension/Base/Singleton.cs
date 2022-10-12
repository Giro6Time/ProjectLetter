using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����
/// ����Ϸ�����ҽ���һ���ĵ�λ�Ļ���
/// </summary>
abstract public class Singleton<T> :MonoBehaviour where T: class ,new()
{
    private static T instance ;
    public static T Instance
    {
        get
        {
            return instance;
        }
    }
    protected virtual void Init()
    {
        if(instance == null)
            instance = this as T; 
    }
    protected virtual void Awake()
    {
        Init();
    }
}
