local EntitySkillTree = class()
local EntitySkillData = require("game.bigWorld.fight.EntitySkillData")

local skillCount = 0

function EntitySkillTree:ctor()
    self.skillTreeMap = {}
end

function EntitySkillTree:init_tree(treeData) --拥有的技能table
    for k, v in pairs(treeData) do
        local temp = 
        {
            code = k,
            level = v
        }
        self:add_skill(temp)
    end    
end

function EntitySkillTree:add_skill(skillData)
    local skill = self:get_skill(skillData.code)
    if skill and skill.level == skillData.level then return end
    if skill then
        skill.level = skillData.level
    else
        local skillData = EntitySkillData()
        skillData:set_data(skillData)
        self.skillTreeMap[skillData.code] = skillData
        skillCount = skillCount + 1
    end
end

function EntitySkillTree:remove_skill(skillCode)
    if not self.skillTreeMap[skillCode] then return end
    self.skillTreeMap[skillCode] = nil
end

function EntitySkillTree:get_skill(skillCode)
    return self.skillTreeMap[skillCode] or nil
end

function EntitySkillTree:get_skill_level(skillCode)
    return self.skillTreeMap[skillCode] and self.skillTreeMap[skillCode].level or nil
end

function EntitySkillTree:get_skill_release_time(skillCode)
    return self.skillTreeMap[skillCode] and self.skillTreeMap[skillCode].lastRelease or 0
end

function EntitySkillTree:get_skill_is_ready(skillCode)
    return self.skillTreeMap[skillCode] and self.skillTreeMap[skillCode]:is_ready() or false
end

return EntitySkillTree