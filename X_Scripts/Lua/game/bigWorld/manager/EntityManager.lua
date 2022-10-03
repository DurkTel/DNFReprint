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
local entityTypeMap = {}
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
    entityData:set_total_data(SentityData)
    local type = entityData:get_entityType()
    --创建C#实体
    local cEntity = CGEntityManager.CreateEntity(type)
    --创建lua实体
    local luaEntity = EntityManager.create_lua_entity(entityData)

    entityData.entityId = cEntity.entityId
    luaEntity:init_data(entityData,cEntity)
    
    if entityData:is_localPlayer() then
        CGEntityManager.localPlayer = cEntity
        EntityManager.localPlayer = luaEntity
        GGameCamera.set_focus(cEntity)
        luaEntity:set_skinVisible(true)
    end

    if not entityMap[cEntity.entityId] then
        entityMap[cEntity.entityId] = luaEntity
    end

    if not entityTypeMap[entityData.etype] then
        entityTypeMap[entityData.etype] = {}
    end

    table.insert(entityTypeMap[entityData.etype], entityData.entityId)

    return luaEntity, cEntity
end

function EntityManager.release_entity(entityId)
    local luaEntity = entityMap[entityId]

    local typeMap = entityTypeMap[luaEntity.entityData.etype]
    for i = #typeMap, 1, -1 do
        if typeMap[i] == entityId then
            table.remove(typeMap, i)
        end
    end
    if #typeMap == 0 then typeMap = nil end

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

function EntityManager.get_entitiyIdListByType(etype)
    return entityTypeMap[etype] or {}
end

function EntityManager.get_playerEntityId()
    local player1 = GEntityManager.get_entitiyIdListByType(GEntityDefine.entityType.otherPlayer)
    local player2 = GEntityManager.get_entitiyIdListByType(GEntityDefine.entityType.localPlayer)

    local players = table.connect(player1, player2)
    return players
end

--获取玩家是否在这个实体的热半径中 返回热半径范围
function EntityManager.get_entitiyEnterHotLevel(entityId)
    local enterHotIds = CEntityHotRadiusfunc.get_enterHotIds()

    for level, group in pairs(enterHotIds) do
        for id, change in pairs(group.enterIds) do
            if entityId == id then
                return level
            end
        end
    end

    return -1
end

function EntityManager.create_local_player(career)
    local cfg = MDefine.cfg.role.getRoleCfgByCareer(career)
    if not table.isNull(cfg) then
        local SentityData =
        {
            type = GEntityDefine.entityType.localPlayer,
            code = career,
            cfg = cfg,
        }
    
        local entity = GEntityManager.create_entity(SentityData)
        return entity
    end
end

function EntityManager.create_monster(refreshId)
    local refreshCfg = MDefine.cfg.monster.getMonsterRefreshCfgById(refreshId)
    local monsterCfg = MDefine.cfg.monster.getMonsterCfgById(refreshId)
    if not table.isNull(refreshCfg) and not table.isNull(monsterCfg) then
        local SentityData =
        {
            type = GEntityDefine.entityType.monster,
            code = refreshId,
            cfg = refreshCfg,
            models = 
            {
                [0] = monsterCfg.modelCode,
            }
        }
    
        local entity = GEntityManager.create_entity(SentityData)
        return entity
    end
end


function EntityManager.create_skill_effect(skillCode, bindEntityId)
    local skillCfg = MDefine.cfg.skill.getSkillCfgById(skillCode)
    if not table.isNull(skillCfg) then
        local master = bindEntityId
        if type(bindEntityId) == "number" then
            master = EntityManager.get_luaEntityById(bindEntityId)
        end
        local effectTab = string.split(skillCfg.effect, ',', true)
        local effectCfg = MDefine.cfg.skill.getEffectCfgById(tonumber(effectTab[1]))
        local SentityData =
        {
            type = GEntityDefine.entityType.effect,
            code = skillCode,
            master = master,
            cfg = 
            {
                animation = skillCfg.animationDataName,
                bornPos = master:get_local_position(),
                offsetPos = Vector3(effectCfg.modelPositionX, effectCfg.modelPositionY, 0)
            },
            models = 
            {
                [2] = tonumber(effectTab[1]),
                [3] = tonumber(effectTab[2]),
            }
        }
    
        local entity = GEntityManager.create_entity(SentityData)
        return entity
    end
end

return EntityManager