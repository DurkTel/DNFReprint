local Loaderfunc = {}
_G.GLoaderfunc = Loaderfunc

local CGMPoolManager = CS.GMPoolManager.Instance
local CGameObjectPool = CS.GameObjectPool.Instance

Loaderfunc.game_poolType = 
{
    effect = "effect",
    viewPrefab = "viewPrefab"
}

--通过对象池来加载游戏对象
function Loaderfunc.load_object_fromPool(assetName, poolName, callBack)
    local pool = CGMPoolManager:TryGet(poolName)
    pool:Get(assetName, callBack)
end

--通过对象池来回收游戏对象
function Loaderfunc.release_object_fromPool(assetName, poolName, obj)
    local pool = CGMPoolManager:TryGet(poolName)
    pool:Release(assetName, obj)
end

--加载一个特效 播放完自动回收
function Loaderfunc.load_effect(assetName, pos, world)
    Loaderfunc.load_object_fromPool(assetName, Loaderfunc.game_poolType.effect, function (obj)
        if world then
            obj.transform.position = pos
        else
            obj.transform.localPosition = pos
        end
        local spriteAnimator = obj:GetComponent(typeof(CS.SpiteAnimator))
        if spriteAnimator then
            spriteAnimator:Play()
            spriteAnimator.onFinish = function ()
                Loaderfunc.release_object_fromPool(assetName, Loaderfunc.game_poolType.effect, obj)
            end
        end
    end)
end

--加载视图预制
function Loaderfunc.load_ui_prefab(assetName, func)
    Loaderfunc.load_object_fromPool("prefabs/gui/"..assetName, Loaderfunc.game_poolType.viewPrefab, func)
end

return Loaderfunc