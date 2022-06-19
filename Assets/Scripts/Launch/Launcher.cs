using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using cfg.db;
using System;
using XLua;

public class Launcher : MonoBehaviour
{
    private bool m_useABLoadMode;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
#if UNITY_EDITOR
        m_useABLoadMode = UnityEditor.EditorPrefs.GetBool("QuickMenuKey_LoadModeABTag", false);
#else
        m_useABLoadMode = true;
#endif

        LaunchUpdate update = gameObject.AddComponent<LaunchUpdate>();
        update.updateComplete = () =>
        {
            //print("启动游戏计时" + Time.realtimeSinceStartup);
            print("游戏更新完成！进入加载流程");
            StartCoroutine(LaunchGame());
        };


}

    /// <summary>
    /// 异步加载运行游戏所需的组件
    /// </summary>
    /// <returns></returns>
    private IEnumerator LaunchGame(UnityAction callback = null)
    {
        AssetLoader.loadMode = m_useABLoadMode ? AssetLoader.LoadMode.AssetBundle : AssetLoader.LoadMode.Resources;

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
