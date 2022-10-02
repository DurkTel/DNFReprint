local EntitiyTypeToClassfunc = {}

local LocalPlayerEntityClassName        = 'game.bigWorld.actors.entityClass.LocalPlayerEntity'
local PortalEntityClassName       = 'game.bigWorld.actors.entityClass.PortalEntity'
local MonsterEntityClassName       = 'game.bigWorld.actors.entityClass.MonsterEntity'
local EffectEntityClassName       = 'game.bigWorld.actors.entityClass.SkillEntity'


function EntitiyTypeToClassfunc.GetClassByType(entityData)
    local className = nil
    if entityData:is_localPlayer() then
        className = LocalPlayerEntityClassName
    elseif entityData:is_monster() then
        className = MonsterEntityClassName
    elseif entityData:is_portal() then
        className = PortalEntityClassName
    elseif entityData:is_effect() then
        className = EffectEntityClassName
    end

    return className
end

return EntitiyTypeToClassfunc