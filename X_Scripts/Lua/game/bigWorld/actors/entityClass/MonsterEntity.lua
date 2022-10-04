local base = require("game.bigWorld.actors.entityClass.FightEntity")
local MonsterEntity = class(base)

function MonsterEntity:ctor()
    base.ctor(self)
    self.curTarget = nil
end

function MonsterEntity:set_sort()
    self:enable_sort_sprite(0.3)
end

function MonsterEntity:on_updateAILogic(timeCount)
    --每3秒更新一次目标
    if timeCount == 1 or timeCount % 3 == 0 then
        self.curTarget = self:get_target()
    end

    --每2秒更新一次决策
    if timeCount == 1 or timeCount % 2 == 0 then
        local decision = self:update_decision()
        self:execute_decision(decision)
    end
end

function MonsterEntity:get_target()
    local targets = GEntityManager.get_playerEntityId()
    --如果多个目标之间距离相差过大 优先选择最近的 反之随机一个相差不多的目标
    local targetId = nil
    local sameDistances = {}
    local minDistance = nil
    local distanceMap = {}
    for k, id in ipairs(targets) do
        local dis = self:get_distance(id)
        distanceMap[id] = dis
        if not minDistance or dis < minDistance then
            minDistance = dis
            targetId = id
        end
    end

    --如果有可最近的目标差不多距离的目标
    for id, dis in pairs(distanceMap) do
        if math.abs(dis - minDistance) <= 0.15 then
            table.insert(sameDistances, id)
        end
    end

    if not table.isNull(sameDistances) then
        math.randomseed(self.entityData.entityId + os.time()) --防止实体都是同一秒创建出来
        local idx = math.random(1,#targets)
        targetId = targets[idx]
    end
    
    return GEntityManager.get_luaEntityById(targetId)
end

function MonsterEntity:get_path(target, offset)
    target = target or self.curTarget
    if not target then return end
    offset = offset or 1
    local targetPos = target:get_position()
    math.randomseed(self.entityData.entityId + os.time())
    local radius = math.random(1, 2) * offset
    local area = GFinding.getNavBorder(targetPos.x, targetPos.y, radius)
    if area.Count == 0 then return false end
    local node = area[math.random(0, area.Count - 1)]

    local selfPos = self:get_position()
    local cuccess, path = GFinding.calculatePathByNode(selfPos.x, selfPos.y, node)
    return cuccess, path
end

--根据目标距离更新决策
function MonsterEntity:update_decision()
    if not self.curTarget then return 3 end
    local dis = self:get_distance(self.curTarget.entityData.entityId)

    local hotRadius = self.hotRadius or {0.3, 2, 4} --没有配置热半径 用默认值
    for i, rad in ipairs(hotRadius) do
        if dis <= rad then
            return i
        end
    end

    return 3
end

--执行决策
function MonsterEntity:execute_decision(decision)
    if decision == 1 then
        self:request_attack()
    elseif decision == 2 then
        self:execute_pursuit()
    else 
        self:execute_patrol()
    end
end

--执行追击
function MonsterEntity:execute_pursuit()
    local cuccess, path = self:get_path() --计算目标的路径
    if cuccess then
        self:start_pathMove(path)
    end
end

--执行巡逻
function MonsterEntity:execute_patrol()
    local cuccess, path = self:get_path(self, 3) --计算一条自己附近的路径
    if cuccess then
        self:start_pathMove(path)
    end
end

return MonsterEntity