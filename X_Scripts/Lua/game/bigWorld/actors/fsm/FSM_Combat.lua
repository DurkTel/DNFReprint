local base = require("game.bigWorld.actors.fsm.FSM_BaseState")
local FSM_Combat = class(base)

function FSM_Combat:on_enter()
    self.entity:play_sprite_animation("ATTACK_1_ANIM")
    if self.entityData.dbcfg.attackAudio then
        math.randomseed(os.time())
        local random = math.random(1, #self.entityData.dbcfg.attackAudio)
        GAudioManager.play_hit(self.entityData.dbcfg.attackAudio[random])
    end
end

return FSM_Combat