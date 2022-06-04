local util = require( 'xlua.util' )
local requireData = {}

requireData.progress = 0

local loadTab =
{
    function ()
        _G.Vector3 = CS.UnityEngine.Vector3
        _G.Vector2 = CS.UnityEngine.Vector2
        _G.GameObject = CS.UnityEngine.GameObject
        _G.Transform = CS.UnityEngine.Transform
        _G.Time = CS.UnityEngine.Time
    end,
    'game.global',
    'game.framework.SystemClass',
    'game.framework.MDefine',
    'game.framework.EventDefine',
    'game.framework.EventDispatcher',
    'game.framework.Define',
    'game.bigWorld.manager.EntityManager',
    'game.bigWorld.manager.ScenesManager',
    'game.bigWorld.manager.CopyManager',
    'game.bigWorld.manager.GameCamera',
}



local asyncFunc = util.coroutine_call(function ()
    for _ ,v in ipairs(loadTab) do
        if type(v) == 'function' then
            v()
        else
            require(v)
        end
    end
    requireData.progress = 1
end)


requireData.start = function() asyncFunc() end

return requireData