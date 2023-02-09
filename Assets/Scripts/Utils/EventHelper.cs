//using UnityEngine;
//using System;
//using static Define;

//public delegate void EventDelegate(int param1);

//public delegate void EventDelegateObj(object param1);

//static public class EventHelper
//{
//    static EventDelegate[] _eventDelegates = new EventDelegate[(int)EventTypeInt.Count];
   
//    static EventDelegateObj[] _eventDelegatesObj = new EventDelegateObj[(int)EventTypeObj.Count];

//    /// <summary>
//    /// 이벤트 등록
//    /// EventHelper.RegisterEvent(evtType, dele);
//    /// </summary>
//    /// <param name="evtType">(int)EventTypeInt.enum</param>
//    /// <param name="dele">new EventDelegate(FunctionName)</param>
//    public static void RegisterEvent(int evtType, EventDelegate dele)
//    {
//        bool bExist = false;
//        if (_eventDelegates[evtType] != null)
//        {
//            Delegate[] arrDele = _eventDelegates[evtType].GetInvocationList();

//            for (int i = 0; i < arrDele.Length; ++i)
//            {
//                if (arrDele[i].Target == dele.Target && arrDele[i].Method == dele.Method)
//                {
//                    bExist = true;
//                    break;
//                }
//            }
//        }

//        if (bExist == false)
//        {
//            _eventDelegates[evtType] += dele;
//        }
//    }

//    /// <summary>
//    /// 이벤트 해제 
//    /// EventHelper.UnregisterEvent(evtType, dele);
//    /// </summary>
//    /// <param name="evtType">(int)EventTypeInt.enum</param>
//    /// <param name="dele">new EventDelegate(FunctionName)</param>
//    public static void UnregisterEvent(int evtType, EventDelegate dele)
//    {
//        _eventDelegates[evtType] -= dele;
//    }

//    /// <summary>
//    /// 이벤트 호출
//    /// </summary>
//    /// <param name="evtType"></param>
//    /// <param name="param"></param>
//    public static void CallEvent(int evtType, int param)
//    {
//        if (_eventDelegates[evtType] != null)
//        {
//            _eventDelegates[evtType](param);
//        }
//    }

//    /// <summary>
//    /// 이벤트 등록
//    /// EventHelper.RegisterEvent(evtType, dele);
//    /// </summary>
//    /// <param name="evtType">(int)EventTypeInt.enum</param>
//    /// <param name="dele">new EventDelegate(FunctionName)</param>
//    public static void RegisterEventObj(int evtType, EventDelegateObj dele)
//    {
//        bool bExist = false;
//        if (_eventDelegatesObj[evtType] != null)
//        {
//            Delegate[] arrDele = _eventDelegatesObj[evtType].GetInvocationList();

//            for (int i = 0; i < arrDele.Length; ++i)
//            {
//                if (arrDele[i].Target == dele.Target && arrDele[i].Method == dele.Method)
//                {
//                    bExist = true;
//                    break;
//                }
//            }
//        }

//        if (bExist == false)
//        {
//            _eventDelegatesObj[evtType] += dele;
//        }
//    }

//    /// <summary>
//    /// 이벤트 해제 
//    /// EventHelper.UnregisterEvent(evtType, dele);
//    /// </summary>
//    /// <param name="evtType">(int)EventTypeInt.enum</param>
//    /// <param name="dele">new EventDelegate(FunctionName)</param>
//    public static void UnregisterEventObj(int evtType, EventDelegateObj dele)
//    {
//        _eventDelegatesObj[evtType] -= dele;
//    }

//    /// <summary>
//    /// 이벤트 호출
//    /// </summary>
//    /// <param name="evtType"></param>
//    /// <param name="param"></param>
//    public static void CallEventObj(int evtType, object param)
//    {
//        if (_eventDelegatesObj[evtType] != null)
//        {
//            _eventDelegatesObj[evtType](param);
//        }
//    }

//}