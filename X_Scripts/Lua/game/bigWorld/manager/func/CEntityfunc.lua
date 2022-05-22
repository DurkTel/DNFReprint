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

return CEntityfunc