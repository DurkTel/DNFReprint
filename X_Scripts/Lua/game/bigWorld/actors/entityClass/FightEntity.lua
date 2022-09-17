--所有能战斗的实体都由此派生

local base = require("game.bigWorld.actors.entityClass.AIEntity")
local FightEntity = class(base)


function FightEntity:on_avatar_loadComplete()
    base.on_avatar_loadComplete(self)
    self:add_entitySkill()
    self:add_entityAttribute("Assets/ScriptableObjects/Character/SaberAttr.asset")
end

function FightEntity:chang_status(state)
    self.entityData:set_entity_status(state)
    self.gmentity:ChangeStatus(state)
end

--攻击表现
function FightEntity:attacker_performance(attackerTrigger, victimTrigger, skillCfg)
    local damageCfg = MDefine.cfg.skill.getDamageCfgById(skillCfg.damageCode)
    --卡肉
    self:set_haltFrame(damageCfg.haltFrame_self)

    --特效
    if not string.isEmptyOrNull(damageCfg.effectName) then
        --计算接触点
        local attackerBounds = attackerTrigger.collider2d.bounds
        local victimBounds = victimTrigger.collider2d.bounds

        local contactPoint = attackerBounds:ClosestPoint(victimBounds.center)
        GLoaderfunc.load_object_fromPool(damageCfg.effectName, GLoaderfunc.game_poolType.effect, function (obj)
            obj.transform.position = contactPoint;
            local spriteAnimator = obj:GetComponent(typeof(CS.SpiteAnimator))
            if spriteAnimator then
                spriteAnimator:Play()
                spriteAnimator.onFinish = function ()
                    GLoaderfunc.release_object_fromPool(damageCfg.effectName, GLoaderfunc.game_poolType.effect, obj)
                end
            end
        end)
    end
end

--受击表现
function FightEntity:victim_performance(attackerTrigger, victimTrigger, skillCfg)
    local entity = GEntityManager.get_luaEntityById(attackerTrigger.entity.entityId)

    local damageCfg = MDefine.cfg.skill.getDamageCfgById(skillCfg.damageCode)
    if not damageCfg then return end
    --被击动画
    self:move_hurt_start(entity:get_transform(), damageCfg.lookAttacker, damageCfg.velocityX, damageCfg.velocityXY, damageCfg.heightY, damageCfg.acceleration, damageCfg.recoverTime)

    --硬直
    self:wait_timer(function () --下一帧再去设置
        self:set_haltFrame(damageCfg.haltFrame_target)
    end)

    --扣血
    local curLife = self.entityData:get_life()
    Dispatcher.dispatchEvent(EventDefine.ON_ENTIT_ATTRIBUTE_UPDATE, self, GEntityDefine.entity_attribute.attribute_life, curLife - damageCfg.hurt)
end

function FightEntity:dispose()
    base.dispose(self)
    self.attackHitMarks = nil
end

return FightEntity