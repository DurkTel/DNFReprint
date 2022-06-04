local CEntityEventfunc = require("game.bigWorld.manager.func.CEntityEventfunc")
local CEntityHotRadiusfunc = require("game.bigWorld.manager.func.CEntityHotRadiusfunc")
local EntitiyTypeToClassfunc = require("game.bigWorld.func.EntitiyTypeToClassfunc")

local EntityManager = {}
_G.GEntityManager = EntityManager


--初始化C#的实体管理器
local CGEntityManager = CS.GMEntityManager
CGEntityManager.Initialize()
CEntityEventfunc.init()
CEntityHotRadiusfunc.init()

local entityClassMap = {}
local entityMap = {}

--创建lua实体
function EntityManager.create_lua_entity(entityData)
    local className = EntitiyTypeToClassfunc.GetClassByType(entityData)
    if not className then
        return ""
    end

    if not entityClassMap[className] then
        entityClassMap[className] = require(className)
    end

    --实例化lua实体
    local luaEntiy = entityClassMap[className]()

    return luaEntiy
end


function EntityManager.create_entity(SentityData)
    --以后可以做对象池
    local entityData = require("game.bigWorld.actors.data.EntityData")()
    entityData:set_srv_data(SentityData)
    local type = entityData:get_entityType()
    --创建C#实体
    local cEntity = CGEntityManager.CreateEntity(type)
    --创建lua实体
    local luaEntity = EntityManager.create_lua_entity(entityData)

    if entityData:is_localPlayer() then
        CGEntityManager.localPlayer = cEntity
        EntityManager.localPlayer = luaEntity
        GGameCamera.set_focus(cEntity)
    end
    entityData.entityId = cEntity.entityId
    luaEntity:init_data(entityData,cEntity)


    if not entityMap[cEntity.entityId] then
        entityMap[cEntity.entityId] = luaEntity
    end

    return luaEntity, cEntity
end

function EntityManager.release_entity(entityId)
    local luaEntity = entityMap[entityId]
    if luaEntity then
        luaEntity:dispose()
    end

    CGEntityManager.ReleaseEntity(entityId)
end


function EntityManager.get_luaEntityById(entityId)
    if entityMap[entityId] then
        return entityMap[entityId]
    end

    return nil
end

return EntityManager