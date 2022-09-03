local FSM_BaseState = class()


function FSM_BaseState:ctor(entity, gmstate)
    self.entity = entity
    self.gmstate = gmstate
    self:init_wrapper()
end


function FSM_BaseState:init_wrapper()
    self.gmstate.onAction = function (gmstate)
        self:on_action()
    end

    self.gmstate.onEnter = function (gmstate)
        self:on_enter()
    end

    self.gmstate.onExit = function (gmstate)
        self:on_exit()
    end
end

function FSM_BaseState:on_enter(gmstate)
    
end

function FSM_BaseState:on_action(gmstate)
    
end

function FSM_BaseState:on_exit(gmstate)
    
end

return FSM_BaseState