local base = require("game.bigWorld.actors.entityClass.FightEntity")
local AIEntity = class(base)

local function getClassByAiState(stateId)
    local stateClass = GEntityDefine.AIStateClass[stateId]
    if not stateClass then
        stateClass = GEntityDefine.AIStateClass[1]
        print_err("无该状态的脚本！！"..stateId)
    end

    if type(stateClass) == "string" then
        stateClass = require(stateClass)
        GEntityDefine.AIStateClass[stateId] = stateClass
    end

    return stateClass
end

function AIEntity:ctor()
    self.status = {}
    self.curstate = nil
    self.lastState = nil
    base.ctor(self)
end

function AIEntity:on_init()
    base.on_init(self)
    self:add_entityStateMachine()
end

function AIEntity:enter_state(stateId)

    if self.curstate then

        if self.curstate.stateId == stateId then
            return
        end
    end

    local state = self.status[stateId]
    if not state then
        local gmstate = self:add_entityState(stateId) --添加C#状态
        local stateClass = getClassByAiState(stateId)
        state = stateClass(self, gmstate)
        self.status[stateId] = state --添加lua状态
    end
    
    self:change_entityState(stateId)
    self.lastState = self.curstate
    self.curstate = state
    
end

return AIEntity