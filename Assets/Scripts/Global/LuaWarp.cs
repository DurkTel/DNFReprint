using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        typeof(Action<bool,bool>)
    };

    [LuaCallCSharp]
    public static List<Type> LuaCallCSharpList = new List<Type>()
    {
        typeof(ToolExtensions),
        typeof(GMEntityManager),
        typeof(GMScenesManager),
        typeof(Entity),
        typeof(OrbitCamera),
    };
}
