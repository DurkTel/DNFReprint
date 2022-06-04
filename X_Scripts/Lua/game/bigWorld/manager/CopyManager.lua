local CopyManager = {}
_G.GCopyManager = CopyManager
local ScenesDefine = require("game.bigWorld.defines.ScenesDefine")
local CopyConfig = MDefine.cfg.copy

local copyLogic = nil
local copyMapType = nil


local function get_login(mapId)
    local cfg = CopyConfig.getCfgById(mapId)
    local mapType = cfg.mapType
    return ScenesDefine.copyLogin[mapType]
end

local function clear()
    copyLogic:remove_event_listener()
    copyLogic:on_copy_out()
    copyMapType = nil
    copyLogic = nil
end

local function begin_copy()
    copyLogic:add_event_listener()
    copyLogic:on_copy_in()
    
end

local function switch_scene(mapId)
    local cfg = CopyConfig.getCfgById(mapId)
    local mapType = cfg.mapType
    --新的地图并不是该副本类型的地图 退出副本
    if mapType ~= copyMapType then
        CopyManager.copy_complete()
    else
        copyLogic:on_copy_update(mapId)
    end

end

--加载副本逻辑
function CopyManager.create_copy_login(mapId)
    local class = get_login(mapId)
    if not class then return end
    copyLogic = require(class)()
end


function CopyManager.scene_complete(mapId)
    if not copyLogic then return end
    --进入副本的第一张图
    if not copyMapType then
        copyMapType = GScenesManager.get_curMapType()
        begin_copy()
    else
        switch_scene(mapId)
    end
end

function CopyManager.copy_complete(success)
    print("副本结算！！！是否胜利："..tostring(success))
    if success then
        
    else
        
    end
    clear()
end


function CopyManager.get_curCopyMapTyep()
    return copyMapType
end


return CopyManager