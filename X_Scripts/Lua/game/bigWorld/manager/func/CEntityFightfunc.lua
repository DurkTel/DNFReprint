local CEntityFightfunc = {}
--实体战斗的C#方法

--设置顿帧
function CEntityFightfunc:set_haltFrame(time)
    self.gmentity.haltFrame = time
end

--开始受击
function CEntityFightfunc:move_hurt_start(transform, lookAt, velocityX, velocityXY, heightY, acceleration, recoverTime)
    self.gmentity:MoveHurt_OnStart(transform, lookAt, velocityX, velocityXY, heightY, acceleration, recoverTime)
end

--设置顿帧
function CEntityFightfunc:set_input_enable(enable)
    self.gmentity:Set_InputEnable(enable)
end

--施加X轴移动力
function CEntityFightfunc:set_mvoe_force(force)
    self.gmentity:Add_MoveForce(force)
end

--施加加速下落的力
function CEntityFightfunc:set_drop_force(force)
    self.gmentity:Add_DropForce(force)
end

--计算该实体是否和自己的朝向 面对 true/背对 false
function CEntityFightfunc:calculate_orientation(entity)
    local entityPos = entity:get_local_position()
    local flip = self:get_flip()
    local offsetX = self:get_local_position().x - entityPos.x
    return flip * offsetX < 0
end


return CEntityFightfunc