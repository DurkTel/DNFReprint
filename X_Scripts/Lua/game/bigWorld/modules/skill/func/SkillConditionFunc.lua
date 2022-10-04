--技能释放条件方法
local SkillConditionFunc = {}

local funcMap = {}

SkillConditionFunc.funcMap = funcMap

function SkillConditionFunc.condition_limit(entity, skillCode, ...)
    local func = funcMap[skillCode]
    if type(entity) == "number" then
        entity = GEntityManager.get_luaEntityById(entity)
    end
    local ready = entity.skillTree:get_skill_is_ready(skillCode)
    local customCondition = true
    local customInfo = {} --条件判断返回的结果数据
    if func then
        customCondition, customInfo = func(entity, ...)
    end
    return ready and customCondition, customInfo --冷却好且特殊限制满足（如果有）
end

--男剑普通攻击
funcMap[10000] = function (entity) 
    local isHurt = entity:in_tag_animation(8)
    if isHurt then return false end
    local curAniName, curFrame = entity:get_current_animation_state()
    local isJump = entity:in_tag_animation(2)
    local limitFlag = false
    local skillCode = 10000
    if isJump then
        if curAniName ~= "JUMP_ATTACK_ANIM" and (curAniName ~= "BACKJUMP_ANIM" or curFrame >= 2) then
            skillCode = 10003
            limitFlag = true
        end
    elseif curAniName == "RUN_ANIM" then
        skillCode = 10011
        limitFlag = true
    elseif curAniName == "ATTACK_1_ANIM" then
        if curFrame >= 6 then --后摇
            skillCode = 10001
            limitFlag = true
        end
    elseif curAniName == "ATTACK_2_ANIM" then
        if curFrame >= 5 then --后摇
            skillCode = 10002
            limitFlag = true
        end
    elseif curAniName == "IDLE_ANIM" then
        limitFlag = true
    end
    return limitFlag, {skillCode = skillCode}
end

--上挑
funcMap[10004] = function (entity)
    local isHurt = entity:in_tag_animation(8)
    if isHurt then return false end
    local curAniName, curFrame = entity:get_current_animation_state()
    local inJump = entity:in_tag_animation(2)
    local inSkill = entity:in_tag_animation(64)
    local skillCode = 10004
    local limitFlag = false

    if inJump then
        if curAniName ~= "BACKJUMP_ANIM" or curFrame >= 2 then
            skillCode = 10010
            limitFlag = true
        end
    else
        limitFlag = not inSkill
    end

    return limitFlag, {skillCode = skillCode}
end

--后跳
funcMap[10007] = function (entity)
    local isHurt = entity:in_tag_animation(8)
    if isHurt then return false end
    local inJump = entity:in_tag_animation(2)
    local inSkill = entity:in_tag_animation(64)

    return not inJump and not inSkill
end

--格挡
funcMap[10008] = function (entity)
    local isHurt = entity:in_tag_animation(8)
    if isHurt then return false end
    local inJump = entity:in_tag_animation(2)
    local inSkill = entity:in_tag_animation(64)

    return not inJump and not inSkill
end


return SkillConditionFunc