using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 单例
/// 在游戏中有且仅有一个的单位的基类
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
