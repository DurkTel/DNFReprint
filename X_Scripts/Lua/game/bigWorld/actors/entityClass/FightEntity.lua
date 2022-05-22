--所有能战斗的实体都由此派生

local base = require("game.bigWorld.actors.entityClass.SkinEntity")
local FightEntity = class(base)


function FightEntity:onAvatarLoadComplete()
    base.onAvatarLoadComplete(self)
    self:add_entitySkill()
    self:add_entityAttribute("Assets/ScriptableObjects/Character/SaberAttr.asset")
end


return FightEntity