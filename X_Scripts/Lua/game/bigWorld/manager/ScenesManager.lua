local CScenesEventfunc = require("game.bigWorld.manager.func.CScenesEventfunc")
local ScenesData = require("game.bigWorld.actors.data.ScenesData")
local CopyConfig = MDefine.cfg.copy

local ScenesManager = {}
_G.GScenesManager = ScenesManager

local CGMScenesManager = CS.GMScenesManager

CScenesEventfunc.init()

local curMapID = nil
local lastMapID = nil

local curMapType = nil
local lastMapType = nil

local sceneMap = {}

--切换场景
function ScenesManager.switch_scene(mapId, portalPos)
    --加载场景时本机不能操控
    local localPlayer = GEntityManager.localPlayer
    if localPlayer then
        localPlayer:set_inputEnable(false)
    end
    if not sceneMap[mapId] then
        local scenesData = ScenesData()
        local cfg = CopyConfig.getCfgById(mapId)
        scenesData:set_srv_data(cfg)
        scenesData:set_portalPos(portalPos)
        sceneMap[mapId] = scenesData
    else
        sceneMap[mapId]:set_portalPos(portalPos)
    end
    lastMapID = curMapID
    curMapID = mapId
    
    lastMapType = curMapType
    curMapType = sceneMap[mapId].mapType
    local scene = CGMScenesManager.Instance:SwitchScene(mapId, sceneMap[mapId].dbcfg.assetName)
    GAudioManager.play_bg_loop_city(sceneMap[mapId].dbcfg.bgMusic)
end

function ScenesManager.get_sceneById(mapId)
    if sceneMap[mapId] then
        return sceneMap[mapId]
    end

    return nil
end

function ScenesManager.get_curMapId()
    return curMapID
end

function ScenesManager.get_lastMapId()
    return lastMapID
end

function ScenesManager.get_curMapType()
    return curMapType
end

function ScenesManager.get_lastMapType()
    return lastMapType
end

function ScenesManager.get_curNavigation()
    return CGMScenesManager.Instance.navigation2D
end

return ScenesManager