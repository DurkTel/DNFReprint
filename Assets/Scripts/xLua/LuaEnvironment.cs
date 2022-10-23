using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using XLua;

public class LuaEnvironment : MonoBehaviour
{
    string m_init = @"
    _G.Debug = CS.UnityEngine.Debug
    _G.print_info = function(str)Debug.Log('[Lua]: '..str)end
    _G.print_err  = function(str)Debug.LogError('[Lua]: '..str)end
    _G.print_war  = function(str)Debug.LogWarning('[Lua]: '..str)end

    function class( base ,className , ...)
            local c = {}
            if type(base) == 'table' then
                for k, v in pairs( base ) do
                    c[k] = v
                end
                c.base = base
                c.__className = className
                -- funcs
                if ... then
                    for _, v in ipairs({...}) do
                        for k, v in pairs( v ) do
                            print(k,v)
                            c[k] = v
                        end
                    end
                end
            elseif type(base) == 'string' then
                c.__className = base
            end 

            c.__index = c

            local mt = {}
            mt.__call = function( class_tbl , ... )
                local obj = {}
                setmetatable( obj, c ) 
                if obj.ctor then
                    obj:ctor(...)
                end
                return obj
            end 
            setmetatable( c, mt )
       return c
    end";

    private static LuaEnvironment m_Instance;
    public static LuaEnvironment Instance { get { return m_Instance; } }

    private static LuaEnv m_luaEnv;

    private static string luaPath;

    private void Awake()
    {
        luaPath = Application.dataPath + "/../X_Scripts/Lua/";

        Initialize();
        
        //实现lua的类结构
        m_luaEnv.DoString(m_init);

    }

    public static void Initialize()
    {
        //初始化lua环境
        m_luaEnv = new LuaEnv();

        //重定向
#if UNITY_EDITOR
        if (UnityEditor.EditorPrefs.GetBool("QuickMenuKey_LoadModeABTag"))
            m_luaEnv.AddLoader(LuaLoaderByAB);
        else
            m_luaEnv.AddLoader(LuaLoader);
#else
            m_luaEnv.AddLoader(LuaLoaderByAB);
#endif

    }

    private static byte[] LuaLoader(ref string filePath)
    {
        filePath = filePath.Replace('.', '/');
        string path = luaPath + filePath + ".lua";

        if (File.Exists(path))
        {
            return File.ReadAllBytes(path);
        }

        return null;
    }

    private static byte[] LuaLoaderByAB(ref string filePath)
    {
        filePath = filePath.Replace('.', '/');
        TextAsset luaTxt = AssetBundleManager.Instance.Load<TextAsset>("lua", "assets/luatemp/" + filePath + ".lua.txt");
        if (luaTxt != null)
            return luaTxt.bytes;

        return null;
    }

    public void LuaMain()
    {
        StartCoroutine(LuaInitLoading());
    }

    private IEnumerator LuaInitLoading()
    {
        m_luaEnv.DoString(@"require 'xlua.util' ", "util");
        yield return null;

        LuaTable require = m_luaEnv.DoString(@"return require 'game.require' ", "require")[0] as LuaTable;
        require.GetInPath<LuaFunction>("start").Call();

        float requireProgress = 0f;
        while (requireProgress < 1)
        {
            requireProgress = require.GetInPath<float>("progress");

            Debug.Log(string.Format("lua require progress : {0}%", requireProgress * 100));

            yield return null;
        }

        //print("lua require完成");
        //lua加载完成 启动lua主入口

        yield return new WaitForSeconds(3);
        m_luaEnv.DoString(@"require 'game.main' ", "main");
        LoadingDefaultGUI.LoadComplete();
    }

    private void Update()
    {
        if (m_luaEnv != null)
        {
            m_luaEnv.Tick();
        }
    }

    private void OnDestroy()
    {
        //if (m_luaEnv != null)
        //{
        //    m_luaEnv.Dispose();
        //    m_luaEnv = null;
        //}
    }
}
