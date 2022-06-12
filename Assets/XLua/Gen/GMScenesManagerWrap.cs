#if USE_UNI_LUA
using LuaAPI = UniLua.Lua;
using RealStatePtr = UniLua.ILuaState;
using LuaCSFunction = UniLua.CSharpFunctionDelegate;
#else
using LuaAPI = XLua.LuaDLL.Lua;
using RealStatePtr = System.IntPtr;
using LuaCSFunction = XLua.LuaDLL.lua_CSFunction;
#endif

using XLua;
using System.Collections.Generic;


namespace XLua.CSObjectWrap
{
    using Utils = XLua.Utils;
    public class GMScenesManagerWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(GMScenesManager);
			Utils.BeginObjectRegister(type, L, translator, 0, 3, 2, 0);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "LoadScene", _m_LoadScene);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "LoadSceneAsyn", _m_LoadSceneAsyn);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SwitchScene", _m_SwitchScene);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "curScene", _g_get_curScene);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "lastScene", _g_get_lastScene);
            
			
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 2, 5, 5);
			Utils.RegisterFunc(L, Utils.CLS_IDX, "Initialize", _m_Initialize_xlua_st_);
            
			
            
			Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "on_LoadEvent", _g_get_on_LoadEvent);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "on_CompleteEvent", _g_get_on_CompleteEvent);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "on_ActivateEvent", _g_get_on_ActivateEvent);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "on_UnActivateEvent", _g_get_on_UnActivateEvent);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "on_ReleaseEvent", _g_get_on_ReleaseEvent);
            
			Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "on_LoadEvent", _s_set_on_LoadEvent);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "on_CompleteEvent", _s_set_on_CompleteEvent);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "on_ActivateEvent", _s_set_on_ActivateEvent);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "on_UnActivateEvent", _s_set_on_UnActivateEvent);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "on_ReleaseEvent", _s_set_on_ReleaseEvent);
            
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 1)
				{
					
					var gen_ret = new GMScenesManager();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to GMScenesManager constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Initialize_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                    GMScenesManager.Initialize(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_LoadScene(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                GMScenesManager gen_to_be_invoked = (GMScenesManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _mapId = LuaAPI.xlua_tointeger(L, 2);
                    
                        var gen_ret = gen_to_be_invoked.LoadScene( _mapId );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_LoadSceneAsyn(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                GMScenesManager gen_to_be_invoked = (GMScenesManager)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 4&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING)&& translator.Assignable<UnityEngine.Events.UnityAction>(L, 4)) 
                {
                    int _mapId = LuaAPI.xlua_tointeger(L, 2);
                    string _path = LuaAPI.lua_tostring(L, 3);
                    UnityEngine.Events.UnityAction _callBack = translator.GetDelegate<UnityEngine.Events.UnityAction>(L, 4);
                    
                        var gen_ret = gen_to_be_invoked.LoadSceneAsyn( _mapId, _path, _callBack );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING)) 
                {
                    int _mapId = LuaAPI.xlua_tointeger(L, 2);
                    string _path = LuaAPI.lua_tostring(L, 3);
                    
                        var gen_ret = gen_to_be_invoked.LoadSceneAsyn( _mapId, _path );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to GMScenesManager.LoadSceneAsyn!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SwitchScene(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                GMScenesManager gen_to_be_invoked = (GMScenesManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _mapId = LuaAPI.xlua_tointeger(L, 2);
                    string _path = LuaAPI.lua_tostring(L, 3);
                    
                    gen_to_be_invoked.SwitchScene( _mapId, _path );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_curScene(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                GMScenesManager gen_to_be_invoked = (GMScenesManager)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.curScene);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_lastScene(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                GMScenesManager gen_to_be_invoked = (GMScenesManager)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.lastScene);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_on_LoadEvent(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    translator.Push(L, GMScenesManager.on_LoadEvent);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_on_CompleteEvent(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    translator.Push(L, GMScenesManager.on_CompleteEvent);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_on_ActivateEvent(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    translator.Push(L, GMScenesManager.on_ActivateEvent);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_on_UnActivateEvent(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    translator.Push(L, GMScenesManager.on_UnActivateEvent);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_on_ReleaseEvent(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    translator.Push(L, GMScenesManager.on_ReleaseEvent);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_on_LoadEvent(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    GMScenesManager.on_LoadEvent = translator.GetDelegate<System.Action<int>>(L, 1);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_on_CompleteEvent(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    GMScenesManager.on_CompleteEvent = translator.GetDelegate<System.Action<int>>(L, 1);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_on_ActivateEvent(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    GMScenesManager.on_ActivateEvent = translator.GetDelegate<System.Action<int>>(L, 1);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_on_UnActivateEvent(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    GMScenesManager.on_UnActivateEvent = translator.GetDelegate<System.Action<int>>(L, 1);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_on_ReleaseEvent(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    GMScenesManager.on_ReleaseEvent = translator.GetDelegate<System.Action<int>>(L, 1);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
