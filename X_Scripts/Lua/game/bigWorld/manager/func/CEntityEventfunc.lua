local CGMEntity = CS.Entity
local CGEntityManager = CS.GMEntityManager

--该脚本主要是关联起C#的实体回调方法
local CEntityEventfunc = {}

function CEntityEventfunc.init()
    CGMEntity.onCreateEvent = CEntityEventfunc.onCreateEvent
    CGMEntity.onDestroyEvent = CEntityEventfunc.onDestroyEvent
    CGMEntity.onLuaAvatarLoadComplete = CEntityEventfunc.onLuaAvatarLoadComplete
    CGMEntity.attackFinishEvent = CEntityEventfunc.attackFinishEvent
    CGMEntity.onContactHandlerEvent = CEntityEventfunc.onContactHandlerEvent
end


function CEntityEventfunc.onCreateEvent(entityId)
    local entity = GEntityManager.get_luaEntityById(entityId)
    entity:onCreateEvent()
end

function CEntityEventfunc.onDestroyEvent()
    
end

function CEntityEventfunc.onLuaAvatarLoadComplete(entityId)
    local entity = GEntityManager.get_luaEntityById(entityId)
    entity:onAvatarLoadComplete()
end

function CEntityEventfunc.attackFinishEvent(entityId, skillCode)
    local entity = GEntityManager.get_luaEntityById(entityId)
    if entity and not table.isNull(entity.attackHitMarks) then
        entity.attackHitMarks[skillCode] = nil --攻击完成 清空计数
    end
end

function CEntityEventfunc.onContactHandlerEvent(attackerEntityId, victimEneityId, skillCode)
    local attacker = GEntityManager.get_luaEntityById(attackerEntityId)
    local victim = GEntityManager.get_luaEntityById(victimEneityId)
    if (attacker.entityData:is_localPlayer() and victim.entityData:is_otherPlayer()) or (attacker.entityData:is_monster() and victim.entityData:is_monster()) then
        return
    end
    local cfg = MDefine.cfg.skill.getSkillCfgById(skillCode)
    if not attacker.attackHitMarks then
        attacker.attackHitMarks = {}
    end
    if not attacker.attackHitMarks[skillCode] then
        attacker.attackHitMarks[skillCode] = {}
    end
    attacker.attackHitMarks[skillCode][victimEneityId] = (attacker.attackHitMarks[skillCode][victimEneityId] or 0) + 1

    if not cfg or attacker.attackHitMarks[skillCode][victimEneityId] > cfg.numbeOfAttacks then --计算攻击次数
        return 
    end
    
    attacker:attacker_performance(victimEneityId, cfg)
    victim:victim_performance(attackerEntityId, cfg)
end

return CEntityEventfunc