--技能通用方法
local SkillConditionFunc = require("game.bigWorld.modules.skill.func.SkillConditionFunc")
local SkillEffectFunc = require("game.bigWorld.modules.skill.func.SkillEffectFunc")
local SkillFunc = {}

--实体释放技能
function SkillFunc.skill_discharge_handle(entity, skillCode, cancel)
    if cancel then
        SkillEffectFunc.reset_skill(entity, skillCode)
    else
        local ready, customInfo = SkillConditionFunc.condition_limit(entity, skillCode)
        if not ready then
            
        else
            SkillEffectFunc.perform_skill(entity, skillCode, customInfo or {})
            entity.skillTree:set_skill_release_time(skillCode) --进入冷却
        end
    end
end

--仅处理本机角色手操技能
function SkillFunc.skill_discharge_handle_local(action, actionType)
    if actionType == GInputManager.INPUT_TYPE.HOLD or actionType == GInputManager.INPUT_TYPE.MULTI then
        return
    end
    local localPlayer = GEntityManager.localPlayer
    local skillCode = localPlayer.skillTree:get_bind_skill(action)
    SkillFunc.skill_discharge_handle(localPlayer, skillCode, actionType == GInputManager.INPUT_TYPE.RELEASE)
end

return SkillFunc