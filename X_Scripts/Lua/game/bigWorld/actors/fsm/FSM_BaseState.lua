local FSM_BaseState = class()


function FSM_BaseState:ctor(entity)
    self.entity = entity
    self.entityData = entity.entityData
end


function FSM_BaseState:on_enter()
    
end

function FSM_BaseState:on_action()
    
end

function FSM_BaseState:on_exit()
    
end

return FSM_BaseState