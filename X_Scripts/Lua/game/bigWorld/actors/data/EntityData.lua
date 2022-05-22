local EntityData = class()


function EntityData:set_srv_data(SentityInfo)
    self.srvInfo    = SentityInfo
    self.etype      = SentityInfo.type
    self.dbcfg      = nil  --配置
    self:init_status()
end

function EntityData:init_status()

end


function EntityData:get_entityType()
    return self.etype
end

function EntityData:is_LocalPlayer()
    return self.etype == 0
end

function EntityData:get_career()
    return self.SentityInfo and self.SentityInfo.career or 0
end

return EntityData