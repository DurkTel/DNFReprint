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
    self.pathList = nil
    base.ctor(self)
end

function AIEntity:on_init()
    base.on_init(self)
    self:add_entityStateMachine()
end

function AIEntity:onAvatarLoadComplete()
    base.onAvatarLoadComplete(self)
    self:on_initAI()
end

function AIEntity:on_initAI()
    local delayAI = self.entityData.dbcfg.delayAI or 1 --ai的启动时间
    local timeCount = 0
    self.logicAI = self:add_timer(function ()
        timeCount = timeCount + 1
        self:on_updateAILogic(timeCount)
    end,1, -1, delayAI)
end

function AIEntity:on_updateAILogic(timeCount)

end

function AIEntity:start_pathMove(path)
    self.pathList = path
    self:enter_state(GEntityDefine.AIStateType.move)
    self:move_entityNavigationPath(path)
end

function AIEntity:stop_pathMove()
    self.pathList = nil
    self:enter_state(GEntityDefine.AIStateType.idle)
    self:moveStop_entityNavigationPath()
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

function AIEntity:dispose()
    base.dispose(self)
    self:del_timer(self.logicAI)
    self.logicAI = nil
end

return AIEntity