local CScenesEventfunc = require("game.bigWorld.manager.func.CScenesEventfunc")
local ScenesData = require("game.bigWorld.actors.data.ScenesData")
local t_mapInfo = require("db.db_tbmap")

local ScenesManager = {}
_G.GScenesManager = ScenesManager

local CGMScenesManager = CS.GMScenesManager

CScenesEventfunc.init()

local curMapID = nil
local lastMapID = nil

local sceneMap = {}

--切换场景
function GScenesManager.switch_scene(mapId, portalPos)
    --加载场景时本机不能操控
    local localPlayer = GEntityManager.localPlayer
    if localPlayer then
        localPlayer:set_inputEnable(false)
    end
    if not sceneMap[mapId] then
        local scenesData = ScenesData()
        local cfg = t_mapInfo[mapId]
        scenesData:set_srv_data(cfg)
        scenesData:set_portalPos(portalPos)
        sceneMap[mapId] = scenesData
    else
        sceneMap[mapId]:set_portalPos(portalPos)
    end
    local scene = CGMScenesManager.Instance:SwitchScene(mapId)
    lastMapID = curMapID
    curMapID = mapId
end

function GScenesManager.get_sceneById(mapId)
    if sceneMap[mapId] then
        return sceneMap[mapId]
    end

    return nil
end

function GScenesManager.get_curMapId()
    return curMapID
end

function GScenesManager.get_lastMapId()
    return lastMapID
end

return GScenesManager