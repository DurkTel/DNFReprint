--本机实体  唯一

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
    }
    base.on_avatar_loadComplete(self)
    self:set_inputEnable(true)
    -- print("启动游戏结束"..Time.realtimeSinceStartup)
    self.skillTree:bind_skill("Attack_1", 10000)
    self.skillTree:bind_skill("Attack_2", 10004)
end

--请求攻击
function LocalPlayerEntity:request_attack()
    local limit, attackName = self:limit_attack()
    if limit then return end
    self:execute_attack(attackName)
end

--攻击限制
function LocalPlayerEntity:limit_attack()
    -- return not self:in_animation("IDLE_ANIM")
    local curAniName, curFrame = self:get_current_animation_state()
    local isJump = self:in_tag_animation(2)
    local limitFlag = true
    local attackName = ""
    if isJump then
        attackName = "JUMP_ATTACK_ANIM"
        limitFlag = false
    elseif curAniName == "ATTACK_1_ANIM" then
        if curFrame >= 6 then --后摇
            attackName = "ATTACK_2_ANIM"
            limitFlag = false
        end
    elseif curAniName == "ATTACK_2_ANIM" then
        if curFrame >= 5 then --后摇
            attackName = "ATTACK_3_ANIM"
            limitFlag = false
        end
    elseif curAniName ~= "ATTACK_3_ANIM" then --后摇不能取消
        attackName = "ATTACK_1_ANIM"
        limitFlag = false
    end
    return limitFlag, attackName
end

--执行攻击
function LocalPlayerEntity:execute_attack(attackName)
    self:enter_state(GEntityDefine.ai_stateType.combat)
    self:play_sprite_animation(attackName)
    if self.entityData.dbcfg.attackAudio then
        math.randomseed(os.time())
        local random = math.random(1, #self.entityData.dbcfg.attackAudio)
        GAudioManager.play_hit(self.entityData.dbcfg.attackAudio[random])
    end
end

--请求浮空攻击
function LocalPlayerEntity:request_levitation_attack()
    local limit = self:limit_levitation_attack()
    if limit then return end
    self:execute_levitation_attack()
end

--浮空攻击限制
function LocalPlayerEntity:limit_levitation_attack()
    local curAniName, curFrame = self:get_current_animation_state()

    return curAniName == "LEVITATION_ANIM"
end

--执行浮空攻击
function LocalPlayerEntity:execute_levitation_attack()
    self:play_sprite_animation("LEVITATION_ANIM")
    
end

return LocalPlayerEntity