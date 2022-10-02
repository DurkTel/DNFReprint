local EntitySkillTree = class()
local EntitySkillData = require("game.bigWorld.fight.EntitySkillData")

local skillCount = 0

function EntitySkillTree:ctor()
    self.skillTreeMap = {} --技能树code - 数据
    self.skillBindMap = {} --技能绑定action - code
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
        local data = EntitySkillData()
        data:set_data(skillData)
        self.skillTreeMap[skillData.code] = data
        skillCount = skillCount + 1
    end
end

function EntitySkillTree:remove_skill(skillCode)
    if not self:get_skill(skillCode) then return end
    self.skillTreeMap[skillCode] = nil
end

function EntitySkillTree:bind_skill(action, skillCode)
    if not self:get_skill(skillCode) then return end --没学不能绑定
    self.skillBindMap[action] = skillCode
end

function EntitySkillTree:unbind_skill(action)
    self.skillBindMap[action] = nil
end

function EntitySkillTree:get_bind_skill(action)
    return self.skillBindMap[action]
end

function EntitySkillTree:get_skill(skillCode)
    return self.skillTreeMap[skillCode] or nil
end

function EntitySkillTree:get_skill_level(skillCode)
    return self.skillTreeMap[skillCode] and self.skillTreeMap[skillCode].level or nil
end

function EntitySkillTree:set_skill_release_time(skillCode)
    if not self:get_skill(skillCode) then return end
    self.skillTreeMap[skillCode]:record_release_time(Time.time)
end

function EntitySkillTree:get_skill_release_time(skillCode)
    return self.skillTreeMap[skillCode] and self.skillTreeMap[skillCode].lastRelease or 0
end

function EntitySkillTree:get_skill_is_ready(skillCode)
    return self.skillTreeMap[skillCode] and self.skillTreeMap[skillCode]:is_ready() or false
end

return EntitySkillTree