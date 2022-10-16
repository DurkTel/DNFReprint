using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using XLua;

public static class LuaWarp
{
    [CSharpCallLua]
    public static List<Type> CSharpCallLuaList = new List<Type>()
    {
        typeof(Action<int>),
        typeof(Action<int,int>),
        typeof(Action<int,int,int>),
        typeof(Action<float>),
        typeof(Action<float,float>),
        typeof(Action<float,float,float>),
        typeof(Action<bool>),
        typeof(Action<bool,bool>),
        typeof(UnityAction<int>),
        typeof(UnityAction<int,int>),
        typeof(UnityAction<int,int,int>),
        typeof(UnityAction<int,int,string>),
        typeof(UnityAction<float>),
        typeof(UnityAction<float,float>),
        typeof(UnityAction<float,float,float>),
        typeof(UnityAction<bool>),
        typeof(UnityAction<bool,bool>),
        typeof(UnityAction<ColliderTrigger,ColliderTrigger,int>),
    };

    [LuaCallCSharp]
    public static List<Type> LuaCallCSharpList = new List<Type>()
    {
        typeof(ToolExtensions),
        typeof(GUIExtensions),
        typeof(GMEntityManager),
        typeof(GMScenesManager),
        typeof(Entity),
        typeof(OrbitCamera),
        typeof(WaitForEndOfFrame),
        typeof(WaitForSeconds),
    };
}
