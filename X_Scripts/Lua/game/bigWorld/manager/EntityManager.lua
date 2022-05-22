local CEntityEventfunc = require("game.bigWorld.manager.func.CEntityEventfunc")

local EntityManager = {}
_G.GEntityManager = EntityManager

local EntitiyTypeToClassfunc                = require("game.bigWorld.func.EntitiyTypeToClassfunc")

--初始化C#的实体管理器
local CGEntityManager = CS.GMEntityManager
CGEntityManager.Initialize()
CEntityEventfunc.init()

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
    --创建C#实体
    local cEntity = CGEntityManager.CreateEntity(entityData:get_entityType())
    --创建lua实体
    local luaEntity = EntityManager.create_lua_entity(entityData)

    CGEntityManager.localPlayer = cEntity
    EntityManager.localPlayer = luaEntity
    entityData.entityId = cEntity.entityId
    luaEntity:init_data(entityData,cEntity)


    if not entityMap[cEntity.entityId] then
        entityMap[cEntity.entityId] = luaEntity
    end
end


function EntityManager.get_luaEntityById(entityId)
    if entityMap[entityId] then
        return entityMap[entityId]
    end

    return nil
end


return EntityManager