local base = require("game.bigWorld.actors.fsm.FSM_BaseState")
local FSM_Hurt = class(base)

function FSM_Hurt:on_enter()
    if self.entityData.dbcfg.hurtAudio then
        math.randomseed(os.time())
        local random = math.random(1, #self.entityData.dbcfg.hurtAudio)
        GAudioManager.play_hit(self.entityData.dbcfg.hurtAudio[random])
    end
end

return FSM_Hurt