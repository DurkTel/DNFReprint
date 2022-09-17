local Loaderfunc = {}
_G.GLoaderfunc = Loaderfunc

local CGMPoolManager = CS.GMPoolManager.Instance
local CGameObjectPool = CS.GameObjectPool.Instance

Loaderfunc.game_poolType = 
{
    effect = "effect"
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

return Loaderfunc