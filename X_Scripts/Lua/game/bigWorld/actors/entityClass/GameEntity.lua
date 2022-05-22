--游戏实体 场景所有实体都由此派生

local base = require("game.bigWorld.actors.entityClass.CEntity")
local GameEntity = class(base)

function GameEntity:init_data(entityData,gmentity)
    base.set_gmentity(self, gmentity)
    self.entityData = entityData
    self:on_init()
end

function GameEntity:on_init()
    local nameStr = ''
    if self.entityData.etype == 0 then
        nameStr = "localPlayer_"
    end

    self.gmentity.gameObject.name = nameStr..self.entityData.entityId
end

return GameEntity