local CGMScenesManager = CS.GMScenesManager
local ScenesDefine = require("game.bigWorld.defines.ScenesDefine")
local EntityDefine = require("game.bigWorld.defines.EntityDefine")
local t_mapInfo = require("db.db_tbmap")

local CScenesEventfunc = {}


function CScenesEventfunc.init()
    CGMScenesManager.on_LoadEvent = CScenesEventfunc.onLoadEvent
    CGMScenesManager.on_CompleteEvent = CScenesEventfunc.onCompleteEvent
    CGMScenesManager.on_ActivateEvent = CScenesEventfunc.onActivateEvent
    CGMScenesManager.on_ReleaseEvent = CScenesEventfunc.onReleaseEvent
end

--场景开始加载
function CScenesEventfunc.onLoadEvent(mapId)
    local scene = GScenesManager.get_sceneById(mapId)

end

--场景加载完成
function CScenesEventfunc.onCompleteEvent(mapId)
    local scene = GScenesManager.get_sceneById(mapId)

end

--激活场景
function CScenesEventfunc.onActivateEvent(mapId)
    local scene = GScenesManager.get_sceneById(mapId)
    local mapCfg = t_mapInfo[mapId]
    local localPlayer = GEntityManager.localPlayer
    if localPlayer then
        --切换和平或战斗状态
        local state = mapCfg.mapType == ScenesDefine.mapType.unique and EntityDefine.Status.peace or EntityDefine.Status.fight
        localPlayer:changStatus(state)
        local pos = Vector3(mapCfg.CharacterPosX,mapCfg.CharacterPosY,0)
        localPlayer:set_avatarPosition(pos)
        GGameCamera.set_limit(mapCfg.cameraMinHeight, mapCfg.cameraMaxHeight, mapCfg.cameraMinWidth, mapCfg.cameraMaxWidth)

        CScenesEventfunc.create_scense_entity(mapCfg)
    end
end

--回收场景
function CScenesEventfunc.onReleaseEvent(mapId)
    local scene = GScenesManager.get_sceneById(mapId)
    
end

function CScenesEventfunc.create_scense_entity(mapCfg)
    --传送门实体
    if not mapCfg.Portals or next(mapCfg.Portals) then
        for k,cfg in ipairs(mapCfg.Portals) do
            local srvInfo =
            {
                type = 4,
                cfg = cfg
            }
            local portalEntity = GEntityManager.create_entity(srvInfo)
            portalEntity:set_avatarPosition(Vector3(cfg.x,cfg.y,cfg.z))
        end
    end
end

return CScenesEventfunc