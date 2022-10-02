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
    elseif self.entityData:is_effect() then
        nameStr = "effect_"
    end

    self:set_hotRadius()
    self.gmentity.gameObject.name = nameStr..self.entityData.entityId

    if self.entityData.dbcfg and self.entityData.dbcfg.bornPos then
        local pos = self.entityData.dbcfg.bornPos
        self:set_avatarPosition(Vector3(pos.x, pos.y, pos.z))
    end
end

function GameEntity:set_hotRadius()
    if self.entityData.dbcfg and not string.isEmptyOrNull(self.entityData.dbcfg.radius) then
        local hotRadius = string.split(self.entityData.dbcfg.radius, ',', true)
        self.hotRadius = hotRadius
        table.sort(self.hotRadius, function (a, b)
            return a < b
        end)
        for i, r in ipairs(self.hotRadius) do
            self.gmentity:Set_HotRadius(i, tonumber(r))
        end
    end
end

function GameEntity:on_hotRadiusfunc(inOut, index)
    
end

function GameEntity:get_distance(entitiyId)
    local entity = GEntityManager.get_luaEntityById(entitiyId)
    local distance = -1
    if entity then
        local pos1 = entity:get_position()
        local pos2 = self:get_position()
        distance = Vector2.Distance(Vector2(pos1.x, pos1.y), Vector2(pos2.x, pos2.y));
    end

    return distance
end

function GameEntity:dispose()
    base.dispose(self)
    self.entityData = nil
    self.hotRadius = nil
end

return GameEntity