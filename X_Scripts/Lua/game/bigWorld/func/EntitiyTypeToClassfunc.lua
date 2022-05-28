local EntitiyTypeToClassfunc = {}

local LocalPlayerEntityClassName        = 'game.bigWorld.actors.entityClass.LocalPlayerEntity'
local PortalEntityEntityClassName       = 'game.bigWorld.actors.entityClass.PortalEntity'


function EntitiyTypeToClassfunc.GetClassByType(entityData)
    local className = nil
    if entityData:is_localPlayer() then
        className = LocalPlayerEntityClassName
    elseif entityData:is_portal() then
        className = PortalEntityEntityClassName
    end

    return className
end

return EntitiyTypeToClassfunc