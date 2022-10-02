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
    public class EntityWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(Entity);
			Utils.BeginObjectRegister(type, L, translator, 0, 46, 47, 21);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Init", _m_Init);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "FixedUpdate", _m_FixedUpdate);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Update", _m_Update);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "LateUpdate", _m_LateUpdate);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "WaitCreate", _m_WaitCreate);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Release", _m_Release);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Dispose", _m_Dispose);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ChangeStatus", _m_ChangeStatus);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetEntityPosition", _m_SetEntityPosition);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetInputEnable", _m_SetInputEnable);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "TickSpriteAnimation", _m_TickSpriteAnimation);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "PauseAni", _m_PauseAni);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "PlayHurtAnimation", _m_PlayHurtAnimation);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "PlayAirHurtAnimation", _m_PlayAirHurtAnimation);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DOSpriteAnimation", _m_DOSpriteAnimation);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DOSpriteAnimationCondition", _m_DOSpriteAnimationCondition);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DOSpriteAnimationNext", _m_DOSpriteAnimationNext);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ForceDOSkillAnimation", _m_ForceDOSkillAnimation);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetCurAnimationLength", _m_GetCurAnimationLength);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "IsInThisAni", _m_IsInThisAni);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "IsInThisTagAni", _m_IsInThisTagAni);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetCurrentAni", _m_GetCurrentAni);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetCurrentFrame", _m_GetCurrentFrame);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "AddEntityAnimator", _m_AddEntityAnimator);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ContactHandle", _m_ContactHandle);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "AddEntitySkill", _m_AddEntitySkill);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnGMCullingDistance", _m_OnGMCullingDistance);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnGMCullingVisible", _m_OnGMCullingVisible);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "CullGroupUpdate", _m_CullGroupUpdate);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Set_HotRadius", _m_Set_HotRadius);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Move_Stop", _m_Move_Stop);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Set_MoveSeed", _m_Set_MoveSeed);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Move_NavigationPath", _m_Move_NavigationPath);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "MoveStop_NavigationPath", _m_MoveStop_NavigationPath);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetSpriteFilp", _m_SetSpriteFilp);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "MoveHurt_OnStart", _m_MoveHurt_OnStart);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "MoveHurt_Y", _m_MoveHurt_Y);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Move_Jump", _m_Move_Jump);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Set_JumpHeight", _m_Set_JumpHeight);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Skin_SetAvatarSkeleton", _m_Skin_SetAvatarSkeleton);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Skin_SetAvatarPart", _m_Skin_SetAvatarPart);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Skin_SetAvatarPartScale", _m_Skin_SetAvatarPartScale);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Skin_SetAvatarPartPosition", _m_Skin_SetAvatarPartPosition);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Skin_SetAvatarPartSort", _m_Skin_SetAvatarPartSort);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Skin_SetAvatarPosition", _m_Skin_SetAvatarPosition);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Skin_SetVisible", _m_Skin_SetVisible);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "entityId", _g_get_entityId);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "entityType", _g_get_entityType);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "careerType", _g_get_careerType);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "status", _g_get_status);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "skinInitialized", _g_get_skinInitialized);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "skinIniting", _g_get_skinIniting);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "inputReader", _g_get_inputReader);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "haltFrame", _g_get_haltFrame);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "curFlip", _g_get_curFlip);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "updateColliderEnabled", _g_get_updateColliderEnabled);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "frameCollInfo", _g_get_frameCollInfo);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "colliderUpdate", _g_get_colliderUpdate);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "own_colliderInfo", _g_get_own_colliderInfo);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "triggerZ", _g_get_triggerZ);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "triggerXY", _g_get_triggerXY);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "collidersZ", _g_get_collidersZ);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "collidersXY", _g_get_collidersXY);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "collidersXY_parent", _g_get_collidersXY_parent);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "collidersZ_parent", _g_get_collidersZ_parent);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "cullingGroupEnabled", _g_get_cullingGroupEnabled);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "cullingRadius", _g_get_cullingRadius);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "cullingLod", _g_get_cullingLod);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "cullingGroup", _g_get_cullingGroup);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "cullingVisible", _g_get_cullingVisible);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "hotRadius", _g_get_hotRadius);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "isHitRecover", _g_get_isHitRecover);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "fallDown", _g_get_fallDown);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "airborne", _g_get_airborne);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "skinNode", _g_get_skinNode);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "rootBone", _g_get_rootBone);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "boxCollider", _g_get_boxCollider);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "rigidbody", _g_get_rigidbody);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "mainAvatar", _g_get_mainAvatar);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "allBones", _g_get_allBones);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "gameObject", _g_get_gameObject);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "transform", _g_get_transform);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "current_animationData", _g_get_current_animationData);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "last_animationData", _g_get_last_animationData);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "animationMap", _g_get_animationMap);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "updateSpriteEvent", _g_get_updateSpriteEvent);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "updateAnimationEvent", _g_get_updateAnimationEvent);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "animationFinish", _g_get_animationFinish);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "maxHotRadiusNum", _g_get_maxHotRadiusNum);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "movePhase", _g_get_movePhase);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onMoveEvent", _g_get_onMoveEvent);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "physicsEvent_HitStop", _g_get_physicsEvent_HitStop);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onJumpEvent", _g_get_onJumpEvent);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "haltFrame", _s_set_haltFrame);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "updateColliderEnabled", _s_set_updateColliderEnabled);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "colliderUpdate", _s_set_colliderUpdate);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "own_colliderInfo", _s_set_own_colliderInfo);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "cullingGroupEnabled", _s_set_cullingGroupEnabled);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "cullingRadius", _s_set_cullingRadius);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "cullingGroup", _s_set_cullingGroup);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "isHitRecover", _s_set_isHitRecover);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "gameObject", _s_set_gameObject);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "transform", _s_set_transform);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "current_animationData", _s_set_current_animationData);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "last_animationData", _s_set_last_animationData);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "animationMap", _s_set_animationMap);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "updateSpriteEvent", _s_set_updateSpriteEvent);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "updateAnimationEvent", _s_set_updateAnimationEvent);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "animationFinish", _s_set_animationFinish);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "maxHotRadiusNum", _s_set_maxHotRadiusNum);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "movePhase", _s_set_movePhase);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onMoveEvent", _s_set_onMoveEvent);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "physicsEvent_HitStop", _s_set_physicsEvent_HitStop);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onJumpEvent", _s_set_onJumpEvent);
            
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 1, 5, 5);
			
			
            
			Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "onCreateEvent", _g_get_onCreateEvent);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "onDestroyEvent", _g_get_onDestroyEvent);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "onLuaAvatarLoadComplete", _g_get_onLuaAvatarLoadComplete);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "attackFinishEvent", _g_get_attackFinishEvent);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "onContactHandlerEvent", _g_get_onContactHandlerEvent);
            
			Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "onCreateEvent", _s_set_onCreateEvent);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "onDestroyEvent", _s_set_onDestroyEvent);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "onLuaAvatarLoadComplete", _s_set_onLuaAvatarLoadComplete);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "attackFinishEvent", _s_set_attackFinishEvent);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "onContactHandlerEvent", _s_set_onContactHandlerEvent);
            
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 1)
				{
					
					var gen_ret = new Entity();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to Entity constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Init(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Entity gen_to_be_invoked = (Entity)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _uid = LuaAPI.xlua_tointeger(L, 2);
                    int _type = LuaAPI.xlua_tointeger(L, 3);
                    CommonUtility.Career _career;translator.Get(L, 4, out _career);
                    UnityEngine.GameObject _go = (UnityEngine.GameObject)translator.GetObject(L, 5, typeof(UnityEngine.GameObject));
                    
                    gen_to_be_invoked.Init( _uid, _type, _career, _go );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_FixedUpdate(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Entity gen_to_be_invoked = (Entity)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    float _deltaTime = (float)LuaAPI.lua_tonumber(L, 2);
                    
                    gen_to_be_invoked.FixedUpdate( _deltaTime );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Update(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Entity gen_to_be_invoked = (Entity)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    float _deltaTime = (float)LuaAPI.lua_tonumber(L, 2);
                    
                    gen_to_be_invoked.Update( _deltaTime );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_LateUpdate(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Entity gen_to_be_invoked = (Entity)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.LateUpdate(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_WaitCreate(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Entity gen_to_be_invoked = (Entity)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.WaitCreate(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Release(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Entity gen_to_be_invoked = (Entity)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.Release(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Dispose(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Entity gen_to_be_invoked = (Entity)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.Dispose(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ChangeStatus(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Entity gen_to_be_invoked = (Entity)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _status = LuaAPI.xlua_tointeger(L, 2);
                    
                    gen_to_be_invoked.ChangeStatus( _status );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetEntityPosition(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Entity gen_to_be_invoked = (Entity)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.Vector3 _vector;translator.Get(L, 2, out _vector);
                    
                    gen_to_be_invoked.SetEntityPosition( _vector );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetInputEnable(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Entity gen_to_be_invoked = (Entity)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    bool _enable = LuaAPI.lua_toboolean(L, 2);
                    
                    gen_to_be_invoked.SetInputEnable( _enable );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_TickSpriteAnimation(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Entity gen_to_be_invoked = (Entity)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    float _deltaTime = (float)LuaAPI.lua_tonumber(L, 2);
                    
                    gen_to_be_invoked.TickSpriteAnimation( _deltaTime );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_PauseAni(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Entity gen_to_be_invoked = (Entity)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    bool _pause = LuaAPI.lua_toboolean(L, 2);
                    
                    gen_to_be_invoked.PauseAni( _pause );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_PlayHurtAnimation(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Entity gen_to_be_invoked = (Entity)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.PlayHurtAnimation(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_PlayAirHurtAnimation(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Entity gen_to_be_invoked = (Entity)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.PlayAirHurtAnimation(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DOSpriteAnimation(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Entity gen_to_be_invoked = (Entity)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& translator.Assignable<AnimationData>(L, 2)) 
                {
                    AnimationData _animationData = (AnimationData)translator.GetObject(L, 2, typeof(AnimationData));
                    
                    gen_to_be_invoked.DOSpriteAnimation( _animationData );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string _aniName = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.DOSpriteAnimation( _aniName );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 4&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<UnityEngine.Events.UnityAction<int>>(L, 3)&& translator.Assignable<UnityEngine.Events.UnityAction>(L, 4)) 
                {
                    string _aniName = LuaAPI.lua_tostring(L, 2);
                    UnityEngine.Events.UnityAction<int> _frameAction = translator.GetDelegate<UnityEngine.Events.UnityAction<int>>(L, 3);
                    UnityEngine.Events.UnityAction _finishAction = translator.GetDelegate<UnityEngine.Events.UnityAction>(L, 4);
                    
                    gen_to_be_invoked.DOSpriteAnimation( _aniName, _frameAction, _finishAction );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to Entity.DOSpriteAnimation!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DOSpriteAnimationCondition(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Entity gen_to_be_invoked = (Entity)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _condAniName = LuaAPI.lua_tostring(L, 2);
                    string _aniName = LuaAPI.lua_tostring(L, 3);
                    
                    gen_to_be_invoked.DOSpriteAnimationCondition( _condAniName, _aniName );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DOSpriteAnimationNext(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Entity gen_to_be_invoked = (Entity)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    AnimationData _animationData = (AnimationData)translator.GetObject(L, 2, typeof(AnimationData));
                    
                    gen_to_be_invoked.DOSpriteAnimationNext( _animationData );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ForceDOSkillAnimation(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Entity gen_to_be_invoked = (Entity)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& translator.Assignable<AnimationData>(L, 2)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)) 
                {
                    AnimationData _animationData = (AnimationData)translator.GetObject(L, 2, typeof(AnimationData));
                    bool _onlyOnCommon = LuaAPI.lua_toboolean(L, 3);
                    
                    gen_to_be_invoked.ForceDOSkillAnimation( _animationData, _onlyOnCommon );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& translator.Assignable<AnimationData>(L, 2)) 
                {
                    AnimationData _animationData = (AnimationData)translator.GetObject(L, 2, typeof(AnimationData));
                    
                    gen_to_be_invoked.ForceDOSkillAnimation( _animationData );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 4&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<CommonUtility.Career>(L, 3)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 4)) 
                {
                    string _animationDataName = LuaAPI.lua_tostring(L, 2);
                    CommonUtility.Career _career;translator.Get(L, 3, out _career);
                    bool _onlyOnCommon = LuaAPI.lua_toboolean(L, 4);
                    
                    gen_to_be_invoked.ForceDOSkillAnimation( _animationDataName, _career, _onlyOnCommon );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<CommonUtility.Career>(L, 3)) 
                {
                    string _animationDataName = LuaAPI.lua_tostring(L, 2);
                    CommonUtility.Career _career;translator.Get(L, 3, out _career);
                    
                    gen_to_be_invoked.ForceDOSkillAnimation( _animationDataName, _career );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to Entity.ForceDOSkillAnimation!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetCurAnimationLength(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Entity gen_to_be_invoked = (Entity)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        var gen_ret = gen_to_be_invoked.GetCurAnimationLength(  );
                        LuaAPI.lua_pushnumber(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_IsInThisAni(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Entity gen_to_be_invoked = (Entity)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& translator.Assignable<AnimationData>(L, 2)) 
                {
                    AnimationData _animationData = (AnimationData)translator.GetObject(L, 2, typeof(AnimationData));
                    
                        var gen_ret = gen_to_be_invoked.IsInThisAni( _animationData );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string _aniName = LuaAPI.lua_tostring(L, 2);
                    
                        var gen_ret = gen_to_be_invoked.IsInThisAni( _aniName );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to Entity.IsInThisAni!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_IsInThisTagAni(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Entity gen_to_be_invoked = (Entity)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    AnimationMap.AniType _aniType;translator.Get(L, 2, out _aniType);
                    
                        var gen_ret = gen_to_be_invoked.IsInThisTagAni( _aniType );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetCurrentAni(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Entity gen_to_be_invoked = (Entity)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        var gen_ret = gen_to_be_invoked.GetCurrentAni(  );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetCurrentFrame(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Entity gen_to_be_invoked = (Entity)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        var gen_ret = gen_to_be_invoked.GetCurrentFrame(  );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_AddEntityAnimator(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Entity gen_to_be_invoked = (Entity)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _aniCfg = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.AddEntityAnimator( _aniCfg );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ContactHandle(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Entity gen_to_be_invoked = (Entity)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    GMUpdateCollider.ContactPair _contact;translator.Get(L, 2, out _contact);
                    ColliderInfos _collInfo = (ColliderInfos)translator.GetObject(L, 3, typeof(ColliderInfos));
                    
                    gen_to_be_invoked.ContactHandle( _contact, _collInfo );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_AddEntitySkill(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Entity gen_to_be_invoked = (Entity)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.AddEntitySkill(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OnGMCullingDistance(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Entity gen_to_be_invoked = (Entity)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _lod = LuaAPI.xlua_tointeger(L, 2);
                    int _lodMax = LuaAPI.xlua_tointeger(L, 3);
                    
                    gen_to_be_invoked.OnGMCullingDistance( _lod, _lodMax );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OnGMCullingVisible(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Entity gen_to_be_invoked = (Entity)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    bool _visible = LuaAPI.lua_toboolean(L, 2);
                    
                    gen_to_be_invoked.OnGMCullingVisible( _visible );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CullGroupUpdate(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Entity gen_to_be_invoked = (Entity)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    float _deltaTime = (float)LuaAPI.lua_tonumber(L, 2);
                    
                    gen_to_be_invoked.CullGroupUpdate( _deltaTime );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Set_HotRadius(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Entity gen_to_be_invoked = (Entity)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _hotIndex = LuaAPI.xlua_tointeger(L, 2);
                    float _radius = (float)LuaAPI.lua_tonumber(L, 3);
                    
                    gen_to_be_invoked.Set_HotRadius( _hotIndex, _radius );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Move_Stop(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Entity gen_to_be_invoked = (Entity)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.Move_Stop(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Set_MoveSeed(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Entity gen_to_be_invoked = (Entity)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    float _moveSeed = (float)LuaAPI.lua_tonumber(L, 2);
                    
                    gen_to_be_invoked.Set_MoveSeed( _moveSeed );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Move_NavigationPath(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Entity gen_to_be_invoked = (Entity)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    System.Collections.Generic.List<AI.PathNode> _path = (System.Collections.Generic.List<AI.PathNode>)translator.GetObject(L, 2, typeof(System.Collections.Generic.List<AI.PathNode>));
                    
                    gen_to_be_invoked.Move_NavigationPath( _path );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_MoveStop_NavigationPath(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Entity gen_to_be_invoked = (Entity)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.MoveStop_NavigationPath(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetSpriteFilp(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Entity gen_to_be_invoked = (Entity)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    bool _isLeft = LuaAPI.lua_toboolean(L, 2);
                    
                    gen_to_be_invoked.SetSpriteFilp( _isLeft );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_MoveHurt_OnStart(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Entity gen_to_be_invoked = (Entity)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.Transform _tarn = (UnityEngine.Transform)translator.GetObject(L, 2, typeof(UnityEngine.Transform));
                    bool _lookAt = LuaAPI.lua_toboolean(L, 3);
                    float _velocityX = (float)LuaAPI.lua_tonumber(L, 4);
                    float _velocityXY = (float)LuaAPI.lua_tonumber(L, 5);
                    float _heightY = (float)LuaAPI.lua_tonumber(L, 6);
                    float _acceleration = (float)LuaAPI.lua_tonumber(L, 7);
                    float _recoverTime = (float)LuaAPI.lua_tonumber(L, 8);
                    
                    gen_to_be_invoked.MoveHurt_OnStart( _tarn, _lookAt, _velocityX, _velocityXY, _heightY, _acceleration, _recoverTime );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_MoveHurt_Y(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Entity gen_to_be_invoked = (Entity)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    float _height = (float)LuaAPI.lua_tonumber(L, 2);
                    float _speedX = (float)LuaAPI.lua_tonumber(L, 3);
                    
                    gen_to_be_invoked.MoveHurt_Y( _height, _speedX );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Move_Jump(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Entity gen_to_be_invoked = (Entity)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.Move_Jump(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Set_JumpHeight(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Entity gen_to_be_invoked = (Entity)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    float _height = (float)LuaAPI.lua_tonumber(L, 2);
                    
                    gen_to_be_invoked.Set_JumpHeight( _height );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Skin_SetAvatarSkeleton(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Entity gen_to_be_invoked = (Entity)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _boneAssetName = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.Skin_SetAvatarSkeleton( _boneAssetName );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Skin_SetAvatarPart(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Entity gen_to_be_invoked = (Entity)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _partType = LuaAPI.xlua_tointeger(L, 2);
                    string _modelAssetName = LuaAPI.lua_tostring(L, 3);
                    string _boneName = LuaAPI.lua_tostring(L, 4);
                    
                    gen_to_be_invoked.Skin_SetAvatarPart( _partType, _modelAssetName, _boneName );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Skin_SetAvatarPartScale(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Entity gen_to_be_invoked = (Entity)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _partType = LuaAPI.xlua_tointeger(L, 2);
                    float _scale = (float)LuaAPI.lua_tonumber(L, 3);
                    
                    gen_to_be_invoked.Skin_SetAvatarPartScale( _partType, _scale );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Skin_SetAvatarPartPosition(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Entity gen_to_be_invoked = (Entity)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _partType = LuaAPI.xlua_tointeger(L, 2);
                    UnityEngine.Vector3 _position;translator.Get(L, 3, out _position);
                    
                    gen_to_be_invoked.Skin_SetAvatarPartPosition( _partType, _position );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Skin_SetAvatarPartSort(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Entity gen_to_be_invoked = (Entity)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _partType = LuaAPI.xlua_tointeger(L, 2);
                    int _sort = LuaAPI.xlua_tointeger(L, 3);
                    
                    gen_to_be_invoked.Skin_SetAvatarPartSort( _partType, _sort );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Skin_SetAvatarPosition(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Entity gen_to_be_invoked = (Entity)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.Vector3 _position;translator.Get(L, 2, out _position);
                    
                    gen_to_be_invoked.Skin_SetAvatarPosition( _position );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Skin_SetVisible(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Entity gen_to_be_invoked = (Entity)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    bool _visible = LuaAPI.lua_toboolean(L, 2);
                    
                    gen_to_be_invoked.Skin_SetVisible( _visible );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_entityId(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Entity gen_to_be_invoked = (Entity)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.entityId);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_entityType(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Entity gen_to_be_invoked = (Entity)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.entityType);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_careerType(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Entity gen_to_be_invoked = (Entity)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.careerType);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_status(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Entity gen_to_be_invoked = (Entity)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.status);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_skinInitialized(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Entity gen_to_be_invoked = (Entity)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.skinInitialized);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_skinIniting(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Entity gen_to_be_invoked = (Entity)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.skinIniting);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_inputReader(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Entity gen_to_be_invoked = (Entity)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.inputReader);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_haltFrame(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Entity gen_to_be_invoked = (Entity)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, gen_to_be_invoked.haltFrame);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onCreateEvent(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    translator.Push(L, Entity.onCreateEvent);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onDestroyEvent(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    translator.Push(L, Entity.onDestroyEvent);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onLuaAvatarLoadComplete(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    translator.Push(L, Entity.onLuaAvatarLoadComplete);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_curFlip(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Entity gen_to_be_invoked = (Entity)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.curFlip);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_updateColliderEnabled(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Entity gen_to_be_invoked = (Entity)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.updateColliderEnabled);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_frameCollInfo(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Entity gen_to_be_invoked = (Entity)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.frameCollInfo);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_colliderUpdate(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Entity gen_to_be_invoked = (Entity)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.colliderUpdate);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_own_colliderInfo(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Entity gen_to_be_invoked = (Entity)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.own_colliderInfo);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_triggerZ(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Entity gen_to_be_invoked = (Entity)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.triggerZ);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_triggerXY(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Entity gen_to_be_invoked = (Entity)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.triggerXY);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_collidersZ(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Entity gen_to_be_invoked = (Entity)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.collidersZ);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_collidersXY(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Entity gen_to_be_invoked = (Entity)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.collidersXY);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_collidersXY_parent(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Entity gen_to_be_invoked = (Entity)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.collidersXY_parent);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_collidersZ_parent(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Entity gen_to_be_invoked = (Entity)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.collidersZ_parent);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_cullingGroupEnabled(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Entity gen_to_be_invoked = (Entity)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.cullingGroupEnabled);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_cullingRadius(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Entity gen_to_be_invoked = (Entity)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, gen_to_be_invoked.cullingRadius);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_cullingLod(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Entity gen_to_be_invoked = (Entity)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.cullingLod);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_cullingGroup(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Entity gen_to_be_invoked = (Entity)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.cullingGroup);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_cullingVisible(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Entity gen_to_be_invoked = (Entity)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.cullingVisible);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_hotRadius(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Entity gen_to_be_invoked = (Entity)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.hotRadius);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_isHitRecover(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Entity gen_to_be_invoked = (Entity)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.isHitRecover);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_fallDown(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Entity gen_to_be_invoked = (Entity)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.fallDown);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_airborne(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Entity gen_to_be_invoked = (Entity)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.airborne);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_skinNode(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Entity gen_to_be_invoked = (Entity)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.skinNode);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_rootBone(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Entity gen_to_be_invoked = (Entity)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.rootBone);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_boxCollider(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Entity gen_to_be_invoked = (Entity)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.boxCollider);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_rigidbody(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Entity gen_to_be_invoked = (Entity)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.rigidbody);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_mainAvatar(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Entity gen_to_be_invoked = (Entity)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.mainAvatar);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_allBones(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Entity gen_to_be_invoked = (Entity)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.allBones);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_gameObject(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Entity gen_to_be_invoked = (Entity)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.gameObject);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_transform(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Entity gen_to_be_invoked = (Entity)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.transform);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_current_animationData(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Entity gen_to_be_invoked = (Entity)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.current_animationData);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_last_animationData(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Entity gen_to_be_invoked = (Entity)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.last_animationData);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_animationMap(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Entity gen_to_be_invoked = (Entity)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.animationMap);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_updateSpriteEvent(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Entity gen_to_be_invoked = (Entity)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.updateSpriteEvent);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_updateAnimationEvent(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Entity gen_to_be_invoked = (Entity)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.updateAnimationEvent);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_animationFinish(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Entity gen_to_be_invoked = (Entity)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.animationFinish);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_attackFinishEvent(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    translator.Push(L, Entity.attackFinishEvent);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onContactHandlerEvent(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    translator.Push(L, Entity.onContactHandlerEvent);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_maxHotRadiusNum(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Entity gen_to_be_invoked = (Entity)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.maxHotRadiusNum);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_movePhase(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Entity gen_to_be_invoked = (Entity)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.movePhase);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onMoveEvent(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Entity gen_to_be_invoked = (Entity)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.onMoveEvent);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_physicsEvent_HitStop(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Entity gen_to_be_invoked = (Entity)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.physicsEvent_HitStop);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onJumpEvent(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Entity gen_to_be_invoked = (Entity)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.onJumpEvent);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_haltFrame(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Entity gen_to_be_invoked = (Entity)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.haltFrame = (float)LuaAPI.lua_tonumber(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onCreateEvent(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    Entity.onCreateEvent = translator.GetDelegate<System.Action<int>>(L, 1);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onDestroyEvent(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    Entity.onDestroyEvent = translator.GetDelegate<System.Action<int>>(L, 1);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onLuaAvatarLoadComplete(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    Entity.onLuaAvatarLoadComplete = translator.GetDelegate<System.Action<int>>(L, 1);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_updateColliderEnabled(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Entity gen_to_be_invoked = (Entity)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.updateColliderEnabled = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_colliderUpdate(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Entity gen_to_be_invoked = (Entity)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.colliderUpdate = (GMUpdateCollider)translator.GetObject(L, 2, typeof(GMUpdateCollider));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_own_colliderInfo(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Entity gen_to_be_invoked = (Entity)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.own_colliderInfo = (ColliderInfos)translator.GetObject(L, 2, typeof(ColliderInfos));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_cullingGroupEnabled(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Entity gen_to_be_invoked = (Entity)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.cullingGroupEnabled = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_cullingRadius(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Entity gen_to_be_invoked = (Entity)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.cullingRadius = (float)LuaAPI.lua_tonumber(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_cullingGroup(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Entity gen_to_be_invoked = (Entity)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.cullingGroup = (GMCullingGroup)translator.GetObject(L, 2, typeof(GMCullingGroup));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_isHitRecover(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Entity gen_to_be_invoked = (Entity)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.isHitRecover = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_gameObject(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Entity gen_to_be_invoked = (Entity)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.gameObject = (UnityEngine.GameObject)translator.GetObject(L, 2, typeof(UnityEngine.GameObject));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_transform(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Entity gen_to_be_invoked = (Entity)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.transform = (UnityEngine.Transform)translator.GetObject(L, 2, typeof(UnityEngine.Transform));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_current_animationData(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Entity gen_to_be_invoked = (Entity)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.current_animationData = (AnimationData)translator.GetObject(L, 2, typeof(AnimationData));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_last_animationData(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Entity gen_to_be_invoked = (Entity)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.last_animationData = (AnimationData)translator.GetObject(L, 2, typeof(AnimationData));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_animationMap(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Entity gen_to_be_invoked = (Entity)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.animationMap = (AnimationMap)translator.GetObject(L, 2, typeof(AnimationMap));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_updateSpriteEvent(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Entity gen_to_be_invoked = (Entity)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.updateSpriteEvent = translator.GetDelegate<UnityEngine.Events.UnityAction<int>>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_updateAnimationEvent(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Entity gen_to_be_invoked = (Entity)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.updateAnimationEvent = translator.GetDelegate<UnityEngine.Events.UnityAction<AnimationData>>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_animationFinish(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Entity gen_to_be_invoked = (Entity)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.animationFinish = translator.GetDelegate<UnityEngine.Events.UnityAction>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_attackFinishEvent(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    Entity.attackFinishEvent = translator.GetDelegate<UnityEngine.Events.UnityAction<int, int>>(L, 1);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onContactHandlerEvent(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    Entity.onContactHandlerEvent = translator.GetDelegate<UnityEngine.Events.UnityAction<ColliderTrigger, ColliderTrigger, int>>(L, 1);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_maxHotRadiusNum(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Entity gen_to_be_invoked = (Entity)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.maxHotRadiusNum = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_movePhase(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Entity gen_to_be_invoked = (Entity)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.movePhase = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onMoveEvent(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Entity gen_to_be_invoked = (Entity)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.onMoveEvent = translator.GetDelegate<UnityEngine.Events.UnityAction<int, int>>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_physicsEvent_HitStop(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Entity gen_to_be_invoked = (Entity)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.physicsEvent_HitStop = (System.Collections.Generic.List<UnityEngine.Events.UnityAction>)translator.GetObject(L, 2, typeof(System.Collections.Generic.List<UnityEngine.Events.UnityAction>));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onJumpEvent(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Entity gen_to_be_invoked = (Entity)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.onJumpEvent = translator.GetDelegate<UnityEngine.Events.UnityAction<int, int>>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
