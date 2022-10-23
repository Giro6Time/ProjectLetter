using UnityEngine;

/// <summary>
/// 单例
/// 在游戏中有且仅有一个的单位的基类
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
        set => instance = value;
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
