require("game.bigWorld.defines.EntityDefine")
local EntityUpdateAttributefunc = {}

local funcMap = {}

EntityUpdateAttributefunc.funcMap = funcMap

function EntityUpdateAttributefunc.update_attribute(entity, update_attribute, ...)
    local fun = funcMap[update_attribute]
    if type(entity) == "number" then
        entity = GEntityManager.get_luaEntityById(entity)
    end
    if fun then fun(entity, ...) end
end

--血量更新
funcMap[GEntityDefine.entity_attribute.attribute_life] = function (entity, life, maxLife)
    entity.entityData:set_life(life, maxLife)
    if life <= 0 then --血量归0 进入死亡状态
        entity:enter_state(GEntityDefine.ai_stateType.death)
    end
end

funcMap[GEntityDefine.entity_attribute.attribute_magic] = function (entity, magic)
    
end

funcMap[GEntityDefine.entity_attribute.attribute_move_seed] = function (entity, seed)
    
end

funcMap[GEntityDefine.entity_attribute.attribute_jump_height] = function (entity, height)
    
end


return EntityUpdateAttributefunc