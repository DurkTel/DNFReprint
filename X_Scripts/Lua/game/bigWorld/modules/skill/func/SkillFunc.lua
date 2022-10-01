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
}

--仅处理本机角色手操技能
function SkillFunc.skill_discharge_handle(action)
    local localPlayer = GEntityManager.localPlayer
    local skillCode = localPlayer.skillTree:get_bind_skill(action)
    -- local ready = localPlayer.skillTree:get_skill_is_ready(skillCode)
    -- if not ready then
        
    -- else
        local func = skillBindMap[action]
        if func then
            func()
        end
        localPlayer.skillTree:set_skill_release_time(skillCode) --进入冷却
    -- end
end

return SkillFunc