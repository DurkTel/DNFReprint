--本机实体  唯一
local SkillConditionFunc = require("game.bigWorld.modules.skill.func.SkillConditionFunc")
local base = require("game.bigWorld.actors.entityClass.FightEntity")
local LocalPlayerEntity = class(base)

function LocalPlayerEntity:on_avatar_loadComplete()
    self.totalSkillList = 
    {
        [10000] = 1,
        [10001] = 1,
        [10002] = 1,
        [10003] = 1,
        [10004] = 1,
        [10006] = 1,
        [10007] = 1,
        [10008] = 1,
        [10009] = 1,
        [10010] = 1,
        [10011] = 1,
    }
    base.on_avatar_loadComplete(self)
    self:set_inputEnable(true)
    -- print("启动游戏结束"..Time.realtimeSinceStartup)
    self.skillTree:bind_skill("Attack_1", 10000)
    self.skillTree:bind_skill("Attack_2", 10004)
    self.skillTree:bind_skill("Skill_1", 10006)
    self.skillTree:bind_skill("Skill_3", 10007)
    self.skillTree:bind_skill("Skill_5", 10008)
    self.skillTree:bind_skill("Skill_2", 10009)

    self.entityData:set_life(100000000, 100000000)
end

local tempAudio = 
{
    [10000] = '',
    [10001] = '',
    [10002] = '',
}

function LocalPlayerEntity:attacker_performance(attackerTrigger, victimTrigger, skillCfg)
    base.attacker_performance(self, attackerTrigger, victimTrigger, skillCfg)
    local audio = tempAudio[skillCfg.id]
    if audio then
        GAudioManager.play_hit("katb_hit.ogg")
    end
end

return LocalPlayerEntity