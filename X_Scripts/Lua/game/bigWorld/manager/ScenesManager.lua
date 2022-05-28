local CScenesEventfunc = require("game.bigWorld.manager.func.CScenesEventfunc")

local ScenesManager = {}
_G.GScenesManager = ScenesManager

local CGMScenesManager = CS.GMScenesManager

CScenesEventfunc.init()

GScenesManager.curMapID = nil
GScenesManager.lastMapID = nil

local sceneMap = {}

--切换场景
function GScenesManager.switch_scene(mapId)
    local scene = CGMScenesManager.Instance:SwitchScene(mapId)
    if not sceneMap[mapId] then
        sceneMap[mapId] = scene
    end
end

function GScenesManager.get_sceneById(mapId)
    if sceneMap[mapId] then
        return sceneMap[mapId]
    end

    return nil
end

return GScenesManager