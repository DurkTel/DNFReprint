
local ScenesData = class()

ScenesData.mapType = nil
ScenesData.mapId = nil
ScenesData.dbcfg = nil

function ScenesData:set_srv_data(cfg)
    self.mapType    = cfg.mapType
    self.mapId      = cfg.id
    self.dbcfg      = cfg  --配置
end

function ScenesData:set_portalPos(pos)
    self.portalPos = pos
end

function ScenesData:get_portalPos()
    local pos = self.portalPos
    self.portalPos = nil
    return pos
end

return ScenesData