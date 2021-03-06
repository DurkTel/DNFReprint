local EntityDefine = require("game.bigWorld.defines.EntityDefine")

local EntityData = class()


function EntityData:set_srv_data(SentityInfo)
    self.srvInfo    = SentityInfo
    self.etype      = SentityInfo.type
    self.dbcfg      = SentityInfo.cfg  --配置
    self:init_status()
end

function EntityData:init_status()
    self.status = nil
end

function EntityData:set_entity_status(state)
    self.status = state
end

function EntityData:get_entityType()
    return self.etype
end

function EntityData:is_localPlayer()
    return self.etype == EntityDefine.EntityType.localPlayer
end

function EntityData:is_otherPlayer()
    return self.etype == EntityDefine.EntityType.otherPlayer
end

function EntityData:is_monster()
    return self.etype == EntityDefine.EntityType.monster
end

function EntityData:is_robot()
    return self.etype == EntityDefine.EntityType.robot
end

function EntityData:is_npc()
    return self.etype == EntityDefine.EntityType.npc
end

function EntityData:is_portal()
    return self.etype == EntityDefine.EntityType.portal
end

function EntityData:get_career()
    return self.SentityInfo and self.SentityInfo.career or 0
end

return EntityData