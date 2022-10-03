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
    entity:on_create()
end

function CEntityEventfunc.onDestroyEvent()
    
end

function CEntityEventfunc.onLuaAvatarLoadComplete(entityId)
    local entity = GEntityManager.get_luaEntityById(entityId)
    entity:on_avatar_loadComplete()
end

function CEntityEventfunc.attackFinishEvent(entityId, skillCode)
    local entity = GEntityManager.get_luaEntityById(entityId)
    if entity and not table.isNull(entity.attackHitMarks) then
        entity.attackHitMarks[entityId.."_"..skillCode] = nil --攻击完成 清空计数
    end
end

function CEntityEventfunc.onContactHandlerEvent(attackerTrigger, victimTrigger, skillCode)
    local attackerId = attackerTrigger.entity.entityId
    local victimId = victimTrigger.entity.entityId

    local attacker = GEntityManager.get_luaEntityById(attackerId)
    if attacker.entityData:is_effect() then --攻击特效
        attacker = attacker.entityData.totalInfo.master
    end
    local victim = GEntityManager.get_luaEntityById(victimId)

    if victim.entityData:get_life() <= 0 then return end

    if (attacker.entityData:is_localPlayer() and victim.entityData:is_otherPlayer()) 
    or (attacker.entityData:is_monster() and victim.entityData:is_monster())
    or (attacker.entityData:is_localPlayer() and victim.entityData:is_localPlayer()) then
        return
    end
    local cfg = MDefine.cfg.skill.getSkillCfgById(skillCode)
    if not attacker.attackHitMarks then
        attacker.attackHitMarks = {}
    end
    local colliderFlag = attackerId.."_"..skillCode
    if not attacker.attackHitMarks[colliderFlag] then
        attacker.attackHitMarks[colliderFlag] = {}
    end
    attacker.attackHitMarks[colliderFlag][victimId] = (attacker.attackHitMarks[colliderFlag][victimId] or 0) + 1

    if not cfg or attacker.attackHitMarks[colliderFlag][victimId] > cfg.numbeOfAttacks then --计算攻击次数
        return 
    end
    
    attacker:attacker_performance(attackerTrigger, victimTrigger, cfg)
    victim:request_victim(attackerTrigger, victimTrigger, cfg)
end

return CEntityEventfunc