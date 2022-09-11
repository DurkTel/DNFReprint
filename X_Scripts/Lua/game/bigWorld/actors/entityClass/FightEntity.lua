--所有能战斗的实体都由此派生

local base = require("game.bigWorld.actors.entityClass.SkinEntity")
local FightEntity = class(base)


function FightEntity:onAvatarLoadComplete()
    base.onAvatarLoadComplete(self)
    self:add_entitySkill()
    self:add_entityAttribute("Assets/ScriptableObjects/Character/SaberAttr.asset")
end

function FightEntity:changStatus(state)
    self.entityData:set_entity_status(state)
    self.gmentity:ChangeStatus(state)
end

--攻击表现
function FightEntity:attacker_performance(victimId, skillCfg)
    local damageCfg = MDefine.cfg.skill.getDamageCfgById(skillCfg.damageCode)
    --卡肉
    self:set_haltFrame(damageCfg.haltFrame_self)

    --特效
    if not string.isEmptyOrNull(damageCfg.effectName) then

    end
end

--受击表现
function FightEntity:victim_performance(attackerId, skillCfg)
    local entity = GEntityManager.get_luaEntityById(attackerId)

    local damageCfg = MDefine.cfg.skill.getDamageCfgById(skillCfg.damageCode)
    if not damageCfg then return end
    --被击动画
    self:move_hurt_start(entity:get_transform(), damageCfg.lookAttacker, damageCfg.velocityX, damageCfg.velocityXY, damageCfg.heightY, damageCfg.acceleration, damageCfg.recoverTime)
    --硬直
    self:wait_timer(function () --下一帧再去设置
        self:set_haltFrame(damageCfg.haltFrame_target)
    end)
end

function FightEntity:dispose()
    base.dispose(self)
    self.attackHitMarks = nil
end

return FightEntity