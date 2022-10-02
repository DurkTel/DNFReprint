local SkillFunc = {}

local skillBindMap = 
{
    ["Attack_1"] = function ()
        local localPlayer = GEntityManager.localPlayer
        localPlayer:request_attack()
    end,
    ["Attack_2"] = function ()
        local localPlayer = GEntityManager.localPlayer
        localPlayer:request_levitation_attack()
    end,
    ["Skill_3"] = function ()
        local localPlayer = GEntityManager.localPlayer
        localPlayer:request_back_jump()
    end,
}

--仅处理本机角色手操技能
function SkillFunc.skill_discharge_handle(action)
    local localPlayer = GEntityManager.localPlayer
    local skillCode = localPlayer.skillTree:get_bind_skill(action)
    local func = skillBindMap[action]
    -- if not func then return end
    local ready = localPlayer.skillTree:get_skill_is_ready(skillCode)
    if not ready then
        
    else
        if func then
            func()
        else
            SkillFunc.request_release_skill(skillCode, GEntityManager.localPlayer)
        end
        localPlayer.skillTree:set_skill_release_time(skillCode) --进入冷却
    end
end

function SkillFunc.request_release_skill(skillCode, entity)
    local cfg = MDefine.cfg.skill.getSkillCfgById(skillCode)
    if string.isEmptyOrNull(cfg.animationDataName) then return end

    local effectTab = string.split(cfg.effect, ',', true)
    
    local function effect(frame)
        GEntityManager.create_skill_effect(skillCode, entity)
    end

    entity:play_sprite_animation(cfg.animationDataName,function (frame)
        if effect and effectTab and frame >= tonumber(effectTab[2]) then
            effect(frame)
            effect = nil
        end
    end)
end

return SkillFunc