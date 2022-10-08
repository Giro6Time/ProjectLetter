using System;
using System.Collections.Generic;
    public static class EventManager
    {
        #region 内部接口、内部类型
        /// <summary>
        /// 事件信息接口
        /// </summary>
        private interface IEventInfo
        {
            void Destroy();
        }
        /// <summary>
        /// 无参类型-事件信息
        /// </summary>
        private class EventInfo : IEventInfo
        {
            public Action action;
            public void Init(Action action)
            {
                this.action = action;
            }
            public void Destroy()
            {
                action = null;
            }
        }
        /// <summary>
        /// 1参类型-事件信息
        /// </summary>
        private class EventInfo<T> : IEventInfo
        {
            public Action<T> action;
            public void Init(Action<T> action)
            {
                this.action += action;
            }
            public void Destroy()
            {
                action = null;
            }
        }
        /// <summary>
        /// 2参类型-事件信息
        /// </summary>
        private class EventInfo<T, K> : IEventInfo
        {
            public Action<T, K> action;
            public void Init(Action<T, K> action)
            {
                this.action += action;
            }
            public void Destroy()
            {
                action = null;
            }
        }
        /// <summary>
        /// 3参类型-事件信息
        /// </summary>
        private class EventInfo<T, K, L> : IEventInfo
        {
            public Action<T, K, L> action;
            public void Init(Action<T, K, L> action)
            {
                this.action += action;
            }
            public void Destroy()
            {
                action = null;
            }
        }

        #endregion
        private static Dictionary<string, IEventInfo> eventInfoDic = new Dictionary<string, IEventInfo>();
        #region 添加事件的监听
        /// <summary>
        /// 添加无参事件
        /// </summary>
        public static void AddEventListener(string eventName, Action action)
        {
            if (eventInfoDic.ContainsKey(eventName))//有没有对应的事件可以监听
            {
                (eventInfoDic[eventName] as EventInfo).action += action;
            }
            else//否则字典中新增这个eventName并且新增对应的Action
            {
                EventInfo eventInfo = new EventInfo();
                eventInfo.Init(action);
                eventInfoDic.Add(eventName, eventInfo);
            }
        }
        /// <summary>
        /// 添加1参事件
        /// </summary>
        public static void AddEventListener<T>(string eventName, Action<T> action)
        {
            if (eventInfoDic.ContainsKey(eventName))//有没有对应的事件可以监听
            {
                (eventInfoDic[eventName] as EventInfo<T>).action += action;
            }
            else//否则字典中新增这个eventName并且新增对应的Action
            {
                EventInfo<T> eventInfo = new EventInfo<T>();
                eventInfo.Init(action);
                eventInfoDic.Add(eventName, eventInfo);
            }
        }
        /// <summary>
        /// 添加2参事件
        /// </summary>
        public static void AddEventListener<T, K>(string eventName, Action<T, K> action)
        {
            if (eventInfoDic.ContainsKey(eventName))//有没有对应的事件可以监听
            {
                (eventInfoDic[eventName] as EventInfo<T, K>).action += action;
            }
            else//否则字典中新增这个eventName并且新增对应的Action
            {
                EventInfo<T, K> eventInfo = new EventInfo<T,K>();
                eventInfo.Init(action);
                eventInfoDic.Add(eventName, eventInfo);
            }
        }
        /// <summary>
        /// 添加3参事件
        /// </summary>
        public static void AddEventListener<T, K, L>(string eventName, Action<T, K, L> action)
        {
            if (eventInfoDic.ContainsKey(eventName))//有没有对应的事件可以监听
            {
                (eventInfoDic[eventName] as EventInfo<T, K, L>).action += action;
            }
            else//否则字典中新增这个eventName并且新增对应的Action
            {
                EventInfo<T, K, L> eventInfo = new EventInfo<T,K,L>();
                eventInfo.Init(action);
                eventInfoDic.Add(eventName, eventInfo);
            }
        }
        #endregion
        #region 触发事件
        /// <summary>
        /// 触发一个无参事件
        /// </summary>
        public static void EventTrigger(string eventName)
        {
            if (eventInfoDic.ContainsKey(eventName))
            {
                (eventInfoDic[eventName] as EventInfo).action?.Invoke();
            }
        }
        /// <summary>
        /// 触发一个1参事件
        /// </summary>
        public static void EventTrigger<T>(string eventName, T arg)
        {
            if (eventInfoDic.ContainsKey(eventName))
            {
                (eventInfoDic[eventName] as EventInfo<T>).action?.Invoke(arg);
            }
        }
        /// <summary>
        /// 触发一个2参事件
        /// </summary>
        public static void EventTrigger<T, K>(string eventName, T arg1, K arg2)
        {
            if (eventInfoDic.ContainsKey(eventName))
            {
                (eventInfoDic[eventName] as EventInfo<T, K>).action?.Invoke(arg1, arg2);
            }
        }
        /// <summary>
        /// 触发一个3参事件
        /// </summary>
        public static void EventTrigger<T, K, L>(string eventName, T arg1, K arg2, L arg3)
        {
            if (eventInfoDic.ContainsKey(eventName))
            {
                (eventInfoDic[eventName] as EventInfo<T, K, L>).action?.Invoke(arg1, arg2, arg3);
            }
        }
        #endregion
        #region 取消事件的监听
        /// <summary>
        /// 取消一个无参事件的监听  
        /// </summary>
        public static void RemoveEventListener(string eventName, Action action)
        {
            if (eventInfoDic.ContainsKey(eventName))
            {
                (eventInfoDic[eventName] as EventInfo).action -= action;
            }
        }
        /// <summary>
        /// 取消一个1参事件的监听  
        /// </summary>
        public static void RemoveEventListener<T>(string eventName, Action<T> action)
        {
            if (eventInfoDic.ContainsKey(eventName))
            {
                (eventInfoDic[eventName] as EventInfo<T>).action -= action;
            }
        }
        /// <summary>
        /// 取消一个2参事件的监听  
        /// </summary>
        public static void RemoveEventListener<T, K>(string eventName, Action<T, K> action)
        {
            if (eventInfoDic.ContainsKey(eventName))
            {
                (eventInfoDic[eventName] as EventInfo<T, K>).action -= action;
            }
        }
        /// <summary>
        /// 取消一个3参事件的监听  
        /// </summary>
        public static void RemoveEventListener<T, K, L>(string eventName, Action<T, K, L> action)
        {
            if (eventInfoDic.ContainsKey(eventName))
            {
                (eventInfoDic[eventName] as EventInfo<T, K, L>).action -= action;
            }
        }
        #endregion
        #region  移除事件
        /// <summary>
        /// 移除/删除一个事件
        /// </summary>
        public static void RemoveEventListener(string eventName)
        {
            if (eventInfoDic.ContainsKey(eventName))
            {
                eventInfoDic[eventName].Destroy();
                eventInfoDic.Remove(eventName);
            }
        }
        /// <summary>
        /// 清空你的事件中心
        /// </summary>
        public static void Clear()
        {
            foreach (string eventName in eventInfoDic.Keys)
            {
                eventInfoDic[eventName].Destroy();
            }
            eventInfoDic.Clear();
        }
        #endregion
    }
