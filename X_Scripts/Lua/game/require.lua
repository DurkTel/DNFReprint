--异步require lua脚本

local util = require( 'xlua.util' )
local cs_coroutine = require( 'game.cs_coroutine' ) 
_G.cs_coroutine = cs_coroutine
local requireData = {}

local loadTab =
{
    function ()
        _G.Vector3 = CS.UnityEngine.Vector3
        _G.Vector2 = CS.UnityEngine.Vector2
        _G.GameObject = CS.UnityEngine.GameObject
        _G.Transform = CS.UnityEngine.Transform
        _G.RectTransform = CS.UnityEngine.RectTransform
        _G.Time = CS.UnityEngine.Time
    end,
    'game.bigWorld.manager.InputManager',
    'game.global',
    'game.framework.SystemClass',
    'game.framework.MDefine',
    'game.framework.EventDefine',
    'game.framework.EventDispatcher',
    'game.framework.Define',
    function ()
        require('game.framework.gui.GUIManager')
        GUIManager.init_layer()
    end,
    'game.framework.func.Loaderfunc',
    'game.bigWorld.manager.GameCamera',
    'game.bigWorld.manager.EntityManager',
    'game.bigWorld.manager.AudioManager',
    'game.bigWorld.manager.ScenesManager',
    'game.bigWorld.manager.CopyManager',
    'game.bigWorld.finding.Finding',
    
}

local clock = os.clock
local ltime, ctime = clock(), 0
local __asyncMaxTime = 0.025 --一帧最大时间 超过跳到下一帧执行

requireData.progress = 0

local function async_func(ltime, ctime, cb)
    if ctime - ltime < __asyncMaxTime then
        cb()
    else
        cs_coroutine.yield(0, cb)
    end
end

local async_yield = util.async_to_sync(async_func)

local asyncFunc = util.coroutine_call(function ()
    local count = #loadTab
    for i ,v in ipairs(loadTab) do
        ltime = clock()
        if type(v) == 'function' then
            v()
        else
            require(v)
        end
        requireData.progress = i / count
        ctime = clock()
        async_yield(ltime, ctime)
    end
    requireData.progress = 1
end)

requireData.start = function() asyncFunc() end

return requireData