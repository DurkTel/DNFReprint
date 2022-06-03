local CopyManager = {}
_G.GCopyManager = CopyManager
local ScenesDefine = require("game.bigWorld.defines.ScenesDefine")

local copyLogic = nil
local copyMapType = nil


local function get_login(mapId)
    local mapType = 1
    return ScenesDefine.copyLogin[mapType]
end

local function begin_copy()
    copyLogic:add_event_listener()
    copyLogic:on_copy_in()
    
end

local function switch_scene(mapId)

end

local function clear()
    copyLogic:remove_event_listener()
    copyLogic:on_copy_out()
    copyMapType = nil
    copyLogic = nil
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


function CopyManager.get_curCopyMapTyep()
    return copyMapType
end


return CopyManager