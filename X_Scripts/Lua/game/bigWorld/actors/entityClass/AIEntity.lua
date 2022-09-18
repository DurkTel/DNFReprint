--所有能移动的实体
local base = require("game.bigWorld.actors.entityClass.SkinEntity")
local AIEntity = class(base)

local function getClassByAiState(stateId)
    local stateClass = GEntityDefine.ai_stateClass[stateId]
    if not stateClass then
        stateClass = GEntityDefine.ai_stateClass[1]
        print_err("无该状态的脚本！！"..stateId)
    end

    if type(stateClass) == "string" then
        stateClass = require(stateClass)
        GEntityDefine.ai_stateClass[stateId] = stateClass
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
    self:set_moveSeed(self.entityData:get_move_seep())
    self:set_jumpHeight(self.entityData:get_jump_height())
end

function AIEntity:on_avatar_loadComplete()
    base.on_avatar_loadComplete(self)
    --皮肤加载完成后进入出生状态
    self:enter_state(GEntityDefine.ai_stateType.born)
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
    if self.curstate then
        self.curstate:on_action()
    end
end

function AIEntity:on_stopAiLogic()
    self:del_timer(self.logicAI)
    self.logicAI = nil
end

function AIEntity:start_inputMove()
    
end

function AIEntity:stop_inputMove()
    
end

function AIEntity:start_pathMove(path)
    self.pathList = path
    self:enter_state(GEntityDefine.ai_stateType.move)
    self:move_entityNavigationPath(path)
end

function AIEntity:stop_pathMove()
    self.pathList = nil
    self:enter_state(GEntityDefine.ai_stateType.idle)
    self:moveStop_entityNavigationPath()
end

function AIEntity:enter_defaultState()
    self:enter_state(GEntityDefine.ai_stateType.idle)
end

function AIEntity:enter_state(stateId)

    if self.curstate then
        if self.curstate.stateId == stateId then
            self.curstate:on_enter()
            return
        else
            self.curstate:on_exit()
        end
    end

    local state = self.status[stateId]
    if not state then
        local stateClass = getClassByAiState(stateId)
        state = stateClass(self)
        self.status[stateId] = state --添加lua状态
    end
    
    self.lastState = self.curstate
    self.curstate = state
    self.curstate:on_enter()
end

function AIEntity:dispose()
    base.dispose(self)
    self:on_stopAiLogic()
end

return AIEntity