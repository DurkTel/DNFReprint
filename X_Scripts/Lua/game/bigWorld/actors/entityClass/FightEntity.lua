--所有能战斗的实体都由此派生

local base = require("game.bigWorld.actors.entityClass.SkinEntity")
local FightEntity = class(base)


function FightEntity:onAvatarLoadComplete()
    base.onAvatarLoadComplete(self)
    self:add_entitySkill()
    self:add_entityAttribute("Assets/ScriptableObjects/Character/SaberAttr.asset")
end

function FightEntity:changStatus(state)
    self.entityData:set_entity_status(state)
    self.gmentity:ChangeStatus(state)
end

function FightEntity:calculatePathFormOwn(x, y)
    local pos = self:get_position()
    local cuccess, path = GFinding.calculatePath(pos.x, pos.y, x, y)
    return cuccess, path
end


return FightEntity