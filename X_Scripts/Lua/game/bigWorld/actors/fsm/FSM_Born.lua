local base = require("game.bigWorld.actors.fsm.FSM_BaseState")
local FSM_Born = class(base)

function FSM_Born:on_enter()
    --添加退出计时器
    if self.entity.bornTimer then 
        self.entity:del_timer(self.entity.bornTimer) 
        self.entity.bornTimer = nil 
    end
    
    self.entity.bornTimer = self.entity:add_timer(function ()
        self.entity:enter_defaultState()
        self.entity:on_initAI()
    end, self.entityData.dbcfg.bronTime or 0)
end

function FSM_Born:on_action()
    
end

function FSM_Born:on_exit()
    if self.entity.bornTimer then 
        self.entity:del_timer(self.entity.bornTimer) 
        self.entity.bornTimer = nil 
    end
end

return FSM_Born