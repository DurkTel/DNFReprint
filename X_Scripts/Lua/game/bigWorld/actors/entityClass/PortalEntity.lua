
local base = require("game.bigWorld.actors.entityClass.GameEntity")
local PortalEntity = class(base)

function PortalEntity:on_init()
    base.on_init(self)
end

function PortalEntity:on_hotRadiusfunc(inOut, index)
    if inOut then
        local mapCfg = self.entityData.dbcfg
        if mapCfg then
            GScenesManager.switch_scene(mapCfg.mapId, Vector3(mapCfg.toX,mapCfg.toY,mapCfg.toZ))
        end
    end
end

return PortalEntity