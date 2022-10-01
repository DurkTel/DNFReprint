_G.EventDefine = {}
local eventID = 0
local function get_id()
    eventID = eventID + 1
    return eventID
end

EventDefine.ON_SCENE_LOAD                                         = get_id()        --场景开始加载
EventDefine.ON_SCENE_ACTIVITE                                     = get_id()        --场景激活

EventDefine.ON_INPUT_UPDATE                                       = get_id()        --输入更新

EventDefine.ON_ENTIT_ATTRIBUTE_UPDATE                             = get_id()        --实体属性更新