local CGMEntity = CS.Entity
local CGEntityManager = CS.GMEntityManager

--该脚本主要是关联起C#的实体回调方法
local CEntityEventfunc = {}

function CEntityEventfunc.init()
    CGMEntity.onCreateEvent = CEntityEventfunc.onCreateEvent
    CGMEntity.onDestroyEvent = CEntityEventfunc.onDestroyEvent
    CGMEntity.onLuaAvatarLoadComplete = CEntityEventfunc.onLuaAvatarLoadComplete
end


function CEntityEventfunc.onCreateEvent(entityId)
    local entity = GEntityManager.get_luaEntityById(entityId)
    entity:onCreateEvent()
end

function CEntityEventfunc.onDestroyEvent()
    
end

function CEntityEventfunc.onLuaAvatarLoadComplete(entityId)
    local entity = GEntityManager.get_luaEntityById(entityId)
    entity:onAvatarLoadComplete()
end

return CEntityEventfunc