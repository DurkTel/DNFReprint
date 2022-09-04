--游戏实体 场景所有实体都由此派生

local base = require("game.bigWorld.actors.entityClass.CEntity")
local Timerfunc = require("game.framework.func.Timerfunc")
local GameEntity = class(base)

GameEntity.add_timer = Timerfunc.add_timer
GameEntity.add_framer = Timerfunc.add_framer
GameEntity.wait_timer = Timerfunc.wait_timer
GameEntity.reset_timer = Timerfunc.reset_timer
GameEntity.del_timer = Timerfunc.del_timer
GameEntity.del_all_timer = Timerfunc.del_all_timer

function GameEntity:init_data(entityData,gmentity)
    base.set_gmentity(self, gmentity)
    self.entityData = entityData
    self:on_init()
end

function GameEntity:on_init()
    local nameStr = ''
    if self.entityData:is_localPlayer() then
        nameStr = "localPlayer_"
    elseif self.entityData:is_monster() then
        nameStr = "monster_"
    elseif self.entityData:is_portal() then
        nameStr = "portal_"
    end

    self:set_hotRadius()
    self.gmentity.gameObject.name = nameStr..self.entityData.entityId

    if self.entityData.dbcfg and self.entityData.dbcfg.bornPos then
        local pos = self.entityData.dbcfg.bornPos
        self:set_avatarPosition(Vector3(pos.x, pos.y, pos.z))
    end
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