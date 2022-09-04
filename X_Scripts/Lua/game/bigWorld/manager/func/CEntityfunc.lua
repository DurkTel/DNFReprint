local CEntityfunc = {}

--实体启用输入
function CEntityfunc:set_inputEnable(enable)
    self.gmentity:SetInputEnable(enable)
end

--设置动画机
function CEntityfunc:add_entityAnimator(aniCfg)
    self.gmentity:AddEntityAnimator(aniCfg)
end

--初始化技能树
function CEntityfunc:add_entitySkill()
    self.gmentity:AddEntitySkill()
end

--初始化属性
function CEntityfunc:add_entityAttribute(name)
    self.gmentity:AddEntityAttribute(name)
end

--添加状态机
function CEntityfunc:add_entityStateMachine()
    self.gmentity:AddStateMachine()
end

--进行实体路径移动
function CEntityfunc:move_entityNavigationPath(path)
    self.gmentity:Move_NavigationPath(path)
end

--停止实体路径移动
function CEntityfunc:moveStop_entityNavigationPath()
    self.gmentity:MoveStop_NavigationPath()
end

--添加状态
function CEntityfunc:add_entityState(stateId)
    return self.gmentity:AddState(stateId)
end

--切换状态
function CEntityfunc:change_entityState(stateId)
    self.gmentity:ChangeState(stateId)
end

return CEntityfunc