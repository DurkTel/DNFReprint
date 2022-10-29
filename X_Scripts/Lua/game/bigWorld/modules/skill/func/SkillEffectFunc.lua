--技能效果方法
local SkillEffectFunc = {}

local funcMap = {}

SkillEffectFunc.funcMap = funcMap

--取消技能
function SkillEffectFunc.reset_skill(entity, skillCode)
    local func = funcMap[skillCode]
    if type(entity) == "number" then
        entity = GEntityManager.get_luaEntityById(entity)
    end
    local skillCfg = MDefine.cfg.skill.getSkillCfgById(skillCode)
    if not skillCfg then return end
    local animationName = skillCfg.animationDataName
    local inAnimation = entity:in_animation(animationName)
    if inAnimation and func and func.cancel then
        func.cancel(entity, skillCode)
    end
end

--释放技能
function SkillEffectFunc.perform_skill(entity, skillCode, customInfo)
    if type(entity) == "number" then
        entity = GEntityManager.get_luaEntityById(entity)
    end
    skillCode = customInfo.skillCode or skillCode
    local func = funcMap[skillCode]
    local skillCfg = MDefine.cfg.skill.getSkillCfgById(skillCode)

    local animationName = skillCfg.animationDataName
    entity:play_sprite_animation(animationName)

    GAudioManager.play_hit(skillCfg.audio)
    if func and func.started then
        func.started(entity, skillCode, customInfo)
    end

    entity.gmentity.updateEventLua = function (frame)
        if not string.isEmptyOrNull(skillCfg.effect) and frame == skillCfg.effectShowFrame then
            GEntityManager.create_skill_effect(skillCode, entity)
        end
        if func and func.performed then
            func.performed(frame, entity, skillCode, customInfo)
        end
    end

    entity.gmentity.finsihEventLua = function ()
        if func and func.finish then
            func.finish(entity, skillCode, customInfo)
        end
    end
end

--模板
--[[
    funcMap[10000] = 
    {
        started = function (entity, skillCode, customInfo) --开始技能时
        end,
        performed = function (frame, entity, skillCode, customInfo) --进行技能中
        end,
        finish = function (entity, skillCode, customInfo) --技能释放完成时
        end,
        cancel = function (entity, skillCode, customInfo) --取消技能时
        end
    }
]]

--格挡
funcMap[10008] = 
{
    started = function (entity, skillCode, customInfo)
        entity.entityData:set_defenseing(true)
    end,
    cancel = function (entity, skillCode)
        entity.entityData:set_defenseing(false)
        entity:play_sprite_animation("IDLE_ANIM")
    end
}

--银光落刃
funcMap[10010] = 
{
    started = function (entity, skillCode, customInfo)
        customInfo.startHeight = entity.gmentity.skinNode.localPosition.y --记录释放技能时的高度
        entity:set_drop_force(15)
        entity:add_bone_effect(GEntityDefine.avatarPartType.weapon, "yinGuangLuoRenEffect.prefab", Vector3(-1.75, 22, 0))
    end,
    performed = function (frame, entity, skillCode, customInfo) --进行技能中
        if entity.gmentity.skinNode.localPosition.y <= 0 then --着地
            entity:remove_bone_effect(GEntityDefine.avatarPartType.weapon, "yinGuangLuoRenEffect.prefab")
            if customInfo.startHeight >= 0.5 then --大于一定高度释放的 播放下蹲动画进行缓冲
                GLoaderfunc.load_effect("earthquake01.prefab", entity:get_position(), true)
                GLoaderfunc.load_effect("earthquake02.prefab", entity:get_position(), true)
                GAudioManager.play_hit("sjh.ogg")
                entity:play_sprite_animation("SIT_ANIM")
                entity:add_timer(function ()
                    entity:play_sprite_animation("IDLE_ANIM")
                end, 0.3)
            else
                entity:play_sprite_animation("IDLE_ANIM")
            end
        end
    end
}


return SkillEffectFunc