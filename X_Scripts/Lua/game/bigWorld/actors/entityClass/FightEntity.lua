--所有能战斗的实体都由此派生

local base = require("game.bigWorld.actors.entityClass.AIEntity")
local EntitySkillTree = require("game.bigWorld.fight.EntitySkillTree")
local FightEntity = class(base)

function FightEntity:on_avatar_loadComplete()
    base.on_avatar_loadComplete(self)
    -- self:add_entitySkill()
    self.skillTree = EntitySkillTree()
    self.skillTree:init_tree(self.totalSkillList or {})
end

function FightEntity:chang_status(state)
    self.entityData:set_entity_status(state)
    self.gmentity:ChangeStatus(state)
end

--请求攻击
function FightEntity:request_attack()
    local limit = self:limit_attack()
    if limit then return end
    self:execute_attack()
end

--攻击限制
function FightEntity:limit_attack()
    return not self:in_animation("IDLE_ANIM")
end

--执行攻击
function FightEntity:execute_attack()
    self:enter_state(GEntityDefine.ai_stateType.combat)
    self:play_sprite_animation("ATTACK_1_ANIM")
    if self.entityData.dbcfg.attackAudio then
        math.randomseed(os.time())
        local random = math.random(1, #self.entityData.dbcfg.attackAudio)
        GAudioManager.play_hit(self.entityData.dbcfg.attackAudio[random])
    end
end

--攻击表现（击中后）
function FightEntity:attacker_performance(attackerTrigger, victimTrigger, skillCfg)
    local damageCfg = MDefine.cfg.skill.getDamageCfgById(skillCfg.damageCode)
    --卡肉
    self:set_haltFrame(damageCfg.haltFrame_self)

    --特效
    if not string.isEmptyOrNull(damageCfg.effectName) then
        math.randomseed(os.time())
        local effects = string.split(damageCfg.effectName, ',', true)
        local random = math.random(1, #effects)
        local name = effects[random]
        --计算接触点
        local attackerBounds = attackerTrigger.collider2d.bounds
        local victimBounds = victimTrigger.collider2d.bounds

        local contactPoint = attackerBounds:ClosestPoint(victimBounds.center)
        GLoaderfunc.load_effect(name, contactPoint, true)
    end
end

--请求受击
function FightEntity:request_victim(attackerTrigger, victimTrigger, skillCfg)
    local entity = GEntityManager.get_luaEntityById(attackerTrigger.entity.entityId)
    local damageCfg = MDefine.cfg.skill.getDamageCfgById(skillCfg.damageCode)

    local isDefenseing = self.entityData:get_defenseing()
    local effect = true
    if isDefenseing then
        local faceToFace = self:calculate_orientation(entity)
        if faceToFace then --格挡成功
            effect = false
            self:play_sprite_animation("GEDANGEFFECT_ANIM")
            GAudioManager.play_hit("swd_eff_01.ogg")
            self:set_mvoe_force(-3)
        end
    end

    if effect then --进行受击表现
        self:victim_performance(entity, damageCfg)
    end
end

--受击表现
function FightEntity:victim_performance(entity, damageCfg)
    if not damageCfg then return end
    self:enter_state(GEntityDefine.ai_stateType.hurt)
    --被击动画
    self:move_hurt_start(entity:get_transform(), damageCfg.lookAttacker, damageCfg.velocityX, damageCfg.velocityXY, damageCfg.heightY, damageCfg.acceleration, damageCfg.recoverTime)

    --硬直
    self:wait_timer(function () --下一帧再去设置
        self:set_haltFrame(damageCfg.haltFrame_target)
    end)

    --扣血
    local curLife = self.entityData:get_life()
    Dispatcher.dispatchEvent(EventDefine.ON_ENTIT_ATTRIBUTE_UPDATE, self, GEntityDefine.entity_attribute.attribute_life, curLife - damageCfg.hurt)

    --音效
    -- if self.entityData.dbcfg.hurtAudio then
    --     math.randomseed(os.time())
    --     local random = math.random(1, #self.entityData.dbcfg.hurtAudio)
    --     GAudioManager.play_hurt(self.entityData.dbcfg.hurtAudio[random])
    -- end
end

function FightEntity:dispose()
    base.dispose(self)
    self.attackHitMarks = nil
end

return FightEntity