local EntitiyTypeToClassfunc = {}

local LocalPlayerEntityClassName        = 'game.bigWorld.actors.entityClass.LocalPlayerEntity'


function EntitiyTypeToClassfunc.GetClassByType(entityData)
    local className = nil
    if entityData:is_LocalPlayer() then
        className = LocalPlayerEntityClassName
    end

    return className
end

return EntitiyTypeToClassfunc