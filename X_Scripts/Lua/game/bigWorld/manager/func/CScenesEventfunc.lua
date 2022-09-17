local CGMScenesManager = CS.GMScenesManager
local ScenesDefine = require("game.bigWorld.defines.ScenesDefine")

local CScenesEventfunc = {}

local portalList = {}


function CScenesEventfunc.init()
    CGMScenesManager.on_LoadEvent = CScenesEventfunc.onLoadEvent
    CGMScenesManager.on_CompleteEvent = CScenesEventfunc.onCompleteEvent
    CGMScenesManager.on_ActivateEvent = CScenesEventfunc.onActivateEvent
    CGMScenesManager.on_ReleaseEvent = CScenesEventfunc.onReleaseEvent
end


local function create_scense_entity(mapCfg)
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
            table.insert(portalList,portalEntity.cguid)
        end
    end
end

local function release_scense_entity()
    if next(portalList) then
        for i,id in ipairs(portalList) do
            GEntityManager.release_entity(id)
        end
    end
    portalList = {}
end

--场景开始加载
function CScenesEventfunc.onLoadEvent(mapId)
    local scene = GScenesManager.get_sceneById(mapId)
    Dispatcher.dispatchEvent(EventDefine.ON_SCENE_LOAD, mapId)

end

--场景加载完成
function CScenesEventfunc.onCompleteEvent(mapId)
    local scene = GScenesManager.get_sceneById(mapId)

end

--激活场景
function CScenesEventfunc.onActivateEvent(mapId)
    release_scense_entity()
    local sceneData = GScenesManager.get_sceneById(mapId)
    local mapCfg = sceneData.dbcfg
    local localPlayer = GEntityManager.localPlayer

    if localPlayer then
        --切换和平或战斗状态
        local state = mapCfg.mapType == ScenesDefine.mapType.unique and GEntityDefine.status.peace or GEntityDefine.status.fight
        localPlayer:chang_status(state)
        --如果时传送门切换的话 优先重置为转送门配置的位置
        local pos = sceneData:get_portalPos() or Vector3(mapCfg.CharacterPosX,mapCfg.CharacterPosY,0)
        sceneData.portalPos = nil
        print("地图切换——"..sceneData.dbcfg.mapName.."——"..sceneData.mapId.."——"..sceneData.mapType)
        localPlayer:set_avatarPosition(pos)
        GGameCamera.set_limit(mapCfg.cameraMinHeight, mapCfg.cameraMaxHeight, mapCfg.cameraMinWidth, mapCfg.cameraMaxWidth)

        create_scense_entity(mapCfg)

        if localPlayer.skinIsComplete then
            localPlayer:set_inputEnable(true)
        end
    end
    Dispatcher.dispatchEvent(EventDefine.ON_SCENE_ACTIVITE, mapId)
end

--回收场景
function CScenesEventfunc.onReleaseEvent(mapId)
    local scene = GScenesManager.get_sceneById(mapId)
    
end

return CScenesEventfunc