--游戏实体 场景所有实体都由此派生

local EntityDefine = require("game.bigWorld.defines.EntityDefine")


local base = require("game.bigWorld.actors.entityClass.CEntity")
local GameEntity = class(base)

function GameEntity:init_data(entityData,gmentity)
    base.set_gmentity(self, gmentity)
    self.entityData = entityData
    self:on_init()
end

function GameEntity:on_init()
    local nameStr = ''
    if self.entityData:is_localPlayer() then
        nameStr = "localPlayer_"
    elseif self.entityData:is_portal() then
        nameStr = "portal_"
    end

    self:set_hotRadius()
    self.gmentity.gameObject.name = nameStr..self.entityData.entityId
end

function GameEntity:set_hotRadius()
    if self.entityData.dbcfg and self.entityData.dbcfg.radius then
        self.gmentity:Set_HotRadius(0, self.entityData.dbcfg.radius)
    end
end

function GameEntity:on_hotRadiusfunc(inOut, index)
    
end

function GameEntity:dispose()
    base.dispose(self)
    self.entityData = nil
end

return GameEntity