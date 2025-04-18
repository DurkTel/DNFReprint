﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using XLua;

public class Launcher : MonoBehaviour
{

    void Start()
    {
        DontDestroyOnLoad(gameObject);

        bool useABLoadMode;

#if UNITY_EDITOR
        useABLoadMode = UnityEditor.EditorPrefs.GetBool("QuickMenuKey_LoadModeABTag", false);
#else
        useABLoadMode = true;
#endif
        AssetManager.Initialize(useABLoadMode);

        //打开加载界面
        LoadingDefaultGUI.Open(transform);

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

        //print("当前资源加载模式为：" + AssetLoader.loadMode);

        GMGUIManager.Instance.Initialize();
        yield return null;

        GMAudioManager.Initialize();
        yield return GMAudioManager.instance.Init();

        GMScenesManager.Initialize();
        yield return null;

        LuaEnvironment luaEnv = gameObject.AddComponent<LuaEnvironment>();

        luaEnv.LuaMain();

        callback?.Invoke();

    }

    //测试代码
    //private void Update()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        Vector3 pos = OrbitCamera.regularCamera.ScreenToWorldPoint(Input.mousePosition);
    //        Vector3 curPost = GMEntityManager.localPlayer.transform.localPosition;
    //        List<AI.PathNode> path = new List<AI.PathNode>();
    //        if (GMScenesManager.Instance.navigation2D.CalculatePath(curPost.x, curPost.y, pos.x, pos.y, out path))
    //        {
    //            GMEntityManager.localPlayer.Move_NavigationPath(path);
    //        }
    //    }
    //}

    [CSharpCallLua]
    public static List<Type> CSharpCallLua = new List<Type>()
    {
        typeof(Action<int>),
        typeof(Action<int,int>),
    };
}
