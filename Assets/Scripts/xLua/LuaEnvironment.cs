﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using XLua;

public class LuaEnvironment : MonoBehaviour
{
    string m_init = @"
    local logger = CS.XLogger
    _G.print_info = function(str)logger.INFO('[Lua]: '..str)end
    _G.print_err  = function(str)logger.ERROR('[Lua]: '..str)end
    _G.print_war  = function(str)logger.WARNING('[Lua]: '..str)end
    _G.reportException  = function(name,message,stackTrace)logger.ReportException(name,message,stackTrace)end

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

        //启动lua断点
        m_luaEnv.DoString(@"require('LuaPanda').start('127.0.0.1',8818)");

        //实现lua的类结构
        m_luaEnv.DoString(m_init);

    }

    void Update()
    {
        
    }

    public static void Initialize()
    {
        //初始化lua环境
        m_luaEnv = new LuaEnv();
        //重定向
        m_luaEnv.AddLoader(LuaLoader);

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

    public void LuaMain()
    {
        StartCoroutine(LuaInitLoading());
    }

    private IEnumerator LuaInitLoading()
    {
        m_luaEnv.DoString(@"require 'xlua.util' ");
        yield return null;

        LuaTable require = m_luaEnv.DoString(@"return require 'game.require' ")[0] as LuaTable;
        require.GetInPath<LuaFunction>("start").Call();

        float requireProgress = 0f;
        while (requireProgress < 1)
        {
            requireProgress = require.GetInPath<float>("progress");

            //Debug.Log(string.Format("requireProgress : {0}", requireProgress));

            yield return null;
        }

        //lua加载完成 启动lua主入口
        m_luaEnv.DoString(@"require 'game.main' ");
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
