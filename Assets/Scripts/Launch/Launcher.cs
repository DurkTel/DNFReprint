using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using cfg.db;
using System;
using XLua;

public class Launcher : MonoBehaviour
{
    public bool useAb;

    void Start()
    {
        DontDestroyOnLoad(gameObject);

        print("游戏启动！进入加载流程");

        StartCoroutine(LaunchGame());


    }

    /// <summary>
    /// 异步加载运行游戏所需的组件
    /// </summary>
    /// <returns></returns>
    private IEnumerator LaunchGame(UnityAction callback = null)
    {
        AssetLoader.loadMode = useAb ? AssetLoader.LoadMode.AssetBundle : AssetLoader.LoadMode.Resources;

        print("当前资源加载模式为：" + AssetLoader.loadMode);

        MDefine.Initialize();
        yield return null;

        GMScenesManager.Initialize();
        yield return null;

        LuaEnvironment luaEnv = gameObject.AddComponent<LuaEnvironment>();

        luaEnv.LuaMain();

        callback?.Invoke();

    }

    [CSharpCallLua]
    public static List<Type> CSharpCallLua = new List<Type>()
    {
        typeof(Action<int>),
        typeof(Action<int,int>),
    };
}
