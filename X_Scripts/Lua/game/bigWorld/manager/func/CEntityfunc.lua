local CEntityfunc = {}
--实体通用的C#方法

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

--设置移动速度
function CEntityfunc:set_moveSeed(seed)
    self.gmentity:Set_MoveSeed(seed)
end

--设置跳跃高度
function CEntityfunc:set_jumpHeight(height)
    self.gmentity:Set_JumpHeight(height)
end

--进行实体路径移动
function CEntityfunc:move_entityNavigationPath(path)
    self.gmentity:Move_NavigationPath(path)
end

--停止实体路径移动
function CEntityfunc:moveStop_entityNavigationPath()
    self.gmentity:MoveStop_NavigationPath()
end

--实体播放动画
function CEntityfunc:play_sprite_animation(animationName, frameFunc, finishFunc)
    self.gmentity:DOSpriteAnimation(animationName, frameFunc, finishFunc)
end

return CEntityfunc