local EntityData = class()


function EntityData:set_total_data(SentityInfo)
    self.totalInfo    = SentityInfo
    self.etype        = SentityInfo.type
    self.dbcfg        = SentityInfo.cfg or {}  --配置
    self.code         = SentityInfo.code       --code不一定是唯一的 大多数时表示同一类的同一种实体
    self:init_cfg()
    self:init_status()
end

function EntityData:init_status()
    self.status = nil
end

function EntityData:init_cfg()
    if self.etype == GEntityDefine.entityType.monster then
        --这个code是refrehid
        local refreshCfg = MDefine.cfg.monster.getMonsterRefreshCfgById(self.code)
        local monsterCfg = MDefine.cfg.monster.getMonsterCfgById(refreshCfg.monsterCode)

        --得到皮肤配置
        self.totalInfo.models = 
        {
            [0] = monsterCfg.modelCode,
        }
        --得到等级
        self.totalInfo.level = refreshCfg.level
        --得到血量
        self.totalInfo.life = refreshCfg.hp
        self.totalInfo.maxLife = refreshCfg.hp
        --得到移动速度
        self.totalInfo.moveSeep = refreshCfg.speed
        --得到攻击力
        self.totalInfo.aggressivity = refreshCfg.aggressivity
        --得到防御力
        self.totalInfo.defense = refreshCfg.defense
        --配置音效
        self.dbcfg.attackAudio = string.split(monsterCfg.attackAudio, ',', true) or {}
        self.dbcfg.hitAudio = string.split(monsterCfg.hitAudio, ',', true) or {}
        self.dbcfg.hurtAudio = string.split(monsterCfg.hurtAudio, ',', true) or {}
        self.dbcfg.dieAudio = string.split(monsterCfg.dieAudio, ',', true) or {}
        self.dbcfg.talkAudio = string.split(monsterCfg.talkAudio, ',', true) or {}
    end
end

function EntityData:set_entity_status(state)
    self.status = state
end

function EntityData:set_life(cur, max)
    if cur < 0 then cur = 0 end
    if cur then self.totalInfo.life = cur end
    if max then self.totalInfo.maxLife = max end
end

function EntityData:get_code() return self.code end

function EntityData:get_level() return self.totalInfo.level or 1 end

function EntityData:get_move_seep() return self.totalInfo.moveSeep or 5 end

function EntityData:get_jump_height() return self.totalInfo.jumpHeight or 0 end

function EntityData:get_aggressivity() return self.totalInfo.aggressivity or 0 end

function EntityData:get_defense() return self.totalInfo.defense or 0 end

function EntityData:get_life() return self.totalInfo.life or 0, self.totalInfo.maxLife or 0 end

function EntityData:get_entityType() return self.etype end

function EntityData:is_localPlayer() return self.etype == GEntityDefine.entityType.localPlayer end

function EntityData:is_otherPlayer() return self.etype == GEntityDefine.entityType.otherPlayer end

function EntityData:is_monster() return self.etype == GEntityDefine.entityType.monster end

function EntityData:is_robot() return self.etype == GEntityDefine.entityType.robot end

function EntityData:is_npc() return self.etype == GEntityDefine.entityType.npc end

function EntityData:is_portal() return self.etype == GEntityDefine.entityType.portal end

function EntityData:is_effect() return self.etype == GEntityDefine.entityType.effect end

function EntityData:get_career() return self.SentityInfo and self.SentityInfo.career or 0 end

return EntityData