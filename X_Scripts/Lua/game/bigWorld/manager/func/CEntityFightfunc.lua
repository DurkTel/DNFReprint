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

--发起攻击
function CEntityFightfunc:attack_start()
    self.gmentity:DOSpriteAnimation("ATTACK_1_ANIM")
end

return CEntityFightfunc