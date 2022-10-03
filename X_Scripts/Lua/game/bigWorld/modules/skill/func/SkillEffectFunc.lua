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
    local func = funcMap[skillCode]
    if type(entity) == "number" then
        entity = GEntityManager.get_luaEntityById(entity)
    end
    skillCode = customInfo.skillCode or skillCode
    local skillCfg = MDefine.cfg.skill.getSkillCfgById(skillCode)
    local animationName = skillCfg.animationDataName
    entity:play_sprite_animation(animationName)
    customInfo.effectShowFrame = not string.isEmptyOrNull(skillCfg.effect) and skillCfg.effectShowFrame or nil
    GAudioManager.play_hit(skillCfg.audio)
    if func then --除动画外的特殊效果
        if func.started then
            func.started(entity, skillCode, customInfo)
        end
        if func.performed then
            entity.gmentity.updateEventLua = function (frame)
                func.performed(frame, entity, skillCode, customInfo)
            end
        end
        if func.finish then
            entity.gmentity.finsihEventLua = function ()
                func.finish(entity, skillCode, customInfo)
            end
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

funcMap[10006] = 
{
    performed = function (frame, entity, skillCode, customInfo)
        if customInfo.effectShowFrame and frame == customInfo.effectShowFrame then
            GEntityManager.create_skill_effect(skillCode, entity)
        end
    end
}

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

funcMap[10009] = 
{
    performed = function (frame, entity, skillCode, customInfo)
        if customInfo.effectShowFrame and frame == customInfo.effectShowFrame then
            GEntityManager.create_skill_effect(skillCode, entity)
        end
    end
}

return SkillEffectFunc