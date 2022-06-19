--本机实体  唯一

local base = require("game.bigWorld.actors.entityClass.FightEntity")
local LocalPlayerEntity = class(base)

function LocalPlayerEntity:onAvatarLoadComplete()
    base.onAvatarLoadComplete(self)
    self:set_inputEnable(true)
    -- print("启动游戏结束"..Time.realtimeSinceStartup)
end

return LocalPlayerEntity