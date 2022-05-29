local CEntityHotRadiusfunc = {}
local CGMEntityManager = CS.GMEntityManager

local enterIds = {}

function CEntityHotRadiusfunc.init()
    CGMEntityManager.entityHotRadius.onHotGroupChange = CEntityHotRadiusfunc.onHotGroupChange
end


local function groupHandle(idx, group)
    enterIds[idx] = group

    for id,change in pairs(group.enterIds) do
        if change then
            local entity = GEntityManager.get_luaEntityById(id)
            entity:on_hotRadiusfunc(true, idx)
        end
    end

    for id,change in pairs(group.exitIds) do
        if change then
            local entity = GEntityManager.get_luaEntityById(id)
            entity:on_hotRadiusfunc(false, idx)
        end
    end
end

function CEntityHotRadiusfunc.onHotGroupChange()
    local hotRadiusMap = CGMEntityManager.entityHotRadius.hotRadiusMap
    for k,group in pairs(hotRadiusMap) do
        if group.change then
            groupHandle(k, group)
        end
    end
end


return CEntityHotRadiusfunc