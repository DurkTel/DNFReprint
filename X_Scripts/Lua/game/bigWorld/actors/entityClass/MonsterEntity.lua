local base = require("game.bigWorld.actors.entityClass.AIEntity")
local MonsterEntity = class(base)

function MonsterEntity:ctor()
    base.ctor(self)
    self.curTarget = nil
end

function MonsterEntity:on_updateAILogic(timeCount)
    --每5秒更新一次目标
    if timeCount == 1 or timeCount % 5 == 0 then
        self.curTarget = self:get_target()
    end

    --每2秒更新一次寻路点
    if timeCount == 1 or timeCount % 2 == 0 then
        local cuccess, path = self:get_path()
        if cuccess then
            self:start_pathMove(path)
        end
    end
end

function MonsterEntity:get_target()
    local player1 = GEntityManager.get_entitiyListByType(GEntityDefine.EntityType.otherPlayer)
    local player2 = GEntityManager.get_entitiyListByType(GEntityDefine.EntityType.localPlayer)

    local targets = table.connect(player1, player2)
    math.randomseed(self.entityData.entityId)
    local idx = math.random(1,#targets)
    local targetEntity = GEntityManager.get_luaEntityById(targets[idx])
    return targetEntity
end

function MonsterEntity:get_path()
    if not self.curTarget then return end

    local targetPos = self.curTarget:get_position()
    math.randomseed(self.entityData.entityId)
    local intX = math.floor(targetPos.x)
    local intY = math.floor(targetPos.y)
    local x = math.random(intX - 1, intX + 1)
    local y = math.random(intY - 1, intY + 1)
    local cuccess, path = self:calculatePathFormOwn(x, y)
    return cuccess, path
end

return MonsterEntity