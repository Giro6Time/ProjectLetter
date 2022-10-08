using System;
using System.Collections.Generic;
    public static class EventManager
    {
        #region �ڲ��ӿڡ��ڲ�����
        /// <summary>
        /// �¼���Ϣ�ӿ�
        /// </summary>
        private interface IEventInfo
        {
            void Destroy();
        }
        /// <summary>
        /// �޲�����-�¼���Ϣ
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
        /// 1������-�¼���Ϣ
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
        /// 2������-�¼���Ϣ
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
        /// 3������-�¼���Ϣ
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
        #region ����¼��ļ���
        /// <summary>
        /// ����޲��¼�
        /// </summary>
        public static void AddEventListener(string eventName, Action action)
        {
            if (eventInfoDic.ContainsKey(eventName))//��û�ж�Ӧ���¼����Լ���
            {
                (eventInfoDic[eventName] as EventInfo).action += action;
            }
            else//�����ֵ����������eventName����������Ӧ��Action
            {
                EventInfo eventInfo = new EventInfo();
                eventInfo.Init(action);
                eventInfoDic.Add(eventName, eventInfo);
            }
        }
        /// <summary>
        /// ���1���¼�
        /// </summary>
        public static void AddEventListener<T>(string eventName, Action<T> action)
        {
            if (eventInfoDic.ContainsKey(eventName))//��û�ж�Ӧ���¼����Լ���
            {
                (eventInfoDic[eventName] as EventInfo<T>).action += action;
            }
            else//�����ֵ����������eventName����������Ӧ��Action
            {
                EventInfo<T> eventInfo = new EventInfo<T>();
                eventInfo.Init(action);
                eventInfoDic.Add(eventName, eventInfo);
            }
        }
        /// <summary>
        /// ���2���¼�
        /// </summary>
        public static void AddEventListener<T, K>(string eventName, Action<T, K> action)
        {
            if (eventInfoDic.ContainsKey(eventName))//��û�ж�Ӧ���¼����Լ���
            {
                (eventInfoDic[eventName] as EventInfo<T, K>).action += action;
            }
            else//�����ֵ����������eventName����������Ӧ��Action
            {
                EventInfo<T, K> eventInfo = new EventInfo<T,K>();
                eventInfo.Init(action);
                eventInfoDic.Add(eventName, eventInfo);
            }
        }
        /// <summary>
        /// ���3���¼�
        /// </summary>
        public static void AddEventListener<T, K, L>(string eventName, Action<T, K, L> action)
        {
            if (eventInfoDic.ContainsKey(eventName))//��û�ж�Ӧ���¼����Լ���
            {
                (eventInfoDic[eventName] as EventInfo<T, K, L>).action += action;
            }
            else//�����ֵ����������eventName����������Ӧ��Action
            {
                EventInfo<T, K, L> eventInfo = new EventInfo<T,K,L>();
                eventInfo.Init(action);
                eventInfoDic.Add(eventName, eventInfo);
            }
        }
        #endregion
        #region �����¼�
        /// <summary>
        /// ����һ���޲��¼�
        /// </summary>
        public static void EventTrigger(string eventName)
        {
            if (eventInfoDic.ContainsKey(eventName))
            {
                (eventInfoDic[eventName] as EventInfo).action?.Invoke();
            }
        }
        /// <summary>
        /// ����һ��1���¼�
        /// </summary>
        public static void EventTrigger<T>(string eventName, T arg)
        {
            if (eventInfoDic.ContainsKey(eventName))
            {
                (eventInfoDic[eventName] as EventInfo<T>).action?.Invoke(arg);
            }
        }
        /// <summary>
        /// ����һ��2���¼�
        /// </summary>
        public static void EventTrigger<T, K>(string eventName, T arg1, K arg2)
        {
            if (eventInfoDic.ContainsKey(eventName))
            {
                (eventInfoDic[eventName] as EventInfo<T, K>).action?.Invoke(arg1, arg2);
            }
        }
        /// <summary>
        /// ����һ��3���¼�
        /// </summary>
        public static void EventTrigger<T, K, L>(string eventName, T arg1, K arg2, L arg3)
        {
            if (eventInfoDic.ContainsKey(eventName))
            {
                (eventInfoDic[eventName] as EventInfo<T, K, L>).action?.Invoke(arg1, arg2, arg3);
            }
        }
        #endregion
        #region ȡ���¼��ļ���
        /// <summary>
        /// ȡ��һ���޲��¼��ļ���  
        /// </summary>
        public static void RemoveEventListener(string eventName, Action action)
        {
            if (eventInfoDic.ContainsKey(eventName))
            {
                (eventInfoDic[eventName] as EventInfo).action -= action;
            }
        }
        /// <summary>
        /// ȡ��һ��1���¼��ļ���  
        /// </summary>
        public static void RemoveEventListener<T>(string eventName, Action<T> action)
        {
            if (eventInfoDic.ContainsKey(eventName))
            {
                (eventInfoDic[eventName] as EventInfo<T>).action -= action;
            }
        }
        /// <summary>
        /// ȡ��һ��2���¼��ļ���  
        /// </summary>
        public static void RemoveEventListener<T, K>(string eventName, Action<T, K> action)
        {
            if (eventInfoDic.ContainsKey(eventName))
            {
                (eventInfoDic[eventName] as EventInfo<T, K>).action -= action;
            }
        }
        /// <summary>
        /// ȡ��һ��3���¼��ļ���  
        /// </summary>
        public static void RemoveEventListener<T, K, L>(string eventName, Action<T, K, L> action)
        {
            if (eventInfoDic.ContainsKey(eventName))
            {
                (eventInfoDic[eventName] as EventInfo<T, K, L>).action -= action;
            }
        }
        #endregion
        #region  �Ƴ��¼�
        /// <summary>
        /// �Ƴ�/ɾ��һ���¼�
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
        /// �������¼�����
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
