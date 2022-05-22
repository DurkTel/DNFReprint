using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

namespace LuaEvent
{
    //public delegate void LuaAction();

    [CSharpCallLua]
    public delegate void LuaAction(int obj);

    [CSharpCallLua]
    public delegate void LuaActionint(int arg1, int arg2);

    //[CSharpCallLua]
    //public delegate void LuaAction<T1, T2, T3>(T1 arg1, T2 arg2, T3 arg3);

    //[CSharpCallLua]
    //public delegate T LuaCallBack<T>();
}