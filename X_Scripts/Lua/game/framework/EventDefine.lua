_G.EventDefine = {}
local eventID = 0
local function get_id()
    eventID = eventID + 1
    return eventID
end

EventDefine.ON_SCENE_LOAD                                         = get_id()        --场景开始加载
EventDefine.ON_SCENE_ACTIVITE                                     = get_id()        --场景激活