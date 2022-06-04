local base = require("game.framework.Module")
local CopyModule = class(base)

--注册模块
function CopyModule:on_register()
    base.on_register(self)
    MDefine.cfg.copy = "game.bigWorld.modules.copy.CopyConfig"
    MDefine.data.copy = "game.bigWorld.modules.copy.CopyData"
end

--移除模块
function CopyModule:on_unregister()
    base.on_unregister(self)
    
end

--注册该模块下的视图
function CopyModule:get_views()
    
end

--注册本地事件
function CopyModule:get_local_evnets()
    return EventDefine.ON_SCENE_LOAD,
    EventDefine.ON_SCENE_ACTIVITE
end

--监听本地事件
function CopyModule:on_local_event(cmd, data)
    if cmd == EventDefine.ON_SCENE_LOAD then
        GCopyManager.create_copy_login(data)
    elseif cmd == EventDefine.ON_SCENE_ACTIVITE then
        GCopyManager.scene_complete(data)
    end
end

return CopyModule