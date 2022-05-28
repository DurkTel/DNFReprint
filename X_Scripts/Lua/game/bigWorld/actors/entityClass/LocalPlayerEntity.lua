--本机实体  唯一

local base = require("game.bigWorld.actors.entityClass.FightEntity")
local LocalPlayerEntity = class(base)

function LocalPlayerEntity:onAvatarLoadComplete()
    base.onAvatarLoadComplete(self)
    self:set_inputEnable(true)
    GGameCamera.set_focus(self.gmentity)

end

return LocalPlayerEntity