local base = require("game.bigWorld.actors.fsm.FSM_BaseState")
local FSM_Death = class(base)

function FSM_Death:on_enter()
    --死亡停止ai逻辑
    self.entity:on_stopAiLogic()
    --播放死亡动画
    self.entity:play_sprite_animation("DEATH_ANIM", function ()
        --播完动画回收实体
        self.entity:add_timer(function ()
            GEntityManager.release_entity(self.entityData.entityId)
        end)
    end)
    --播放死亡音效

    --播放死亡特效

end

return FSM_Death