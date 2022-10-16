local base = require("game.framework.Module")
local EntityUpdateAttributefunc = require("game.bigWorld.func.EntityUpdateAttributefunc")
local BigworldModule = class(base)

--注册模块
function BigworldModule:on_register()
    base.on_register(self)
    MDefine.cfg.bigworld = "game.bigWorld.modules.bigworld.BigworldConfig"
    MDefine.data.bigworld = "game.bigWorld.modules.bigworld.BigworldData"
end

--移除模块
function BigworldModule:on_unregister()
    base.on_unregister(self)
    
end

--注册该模块下的视图
function BigworldModule:get_views()
end

--注册本地事件
function BigworldModule:get_local_evnets()
    return EventDefine.ON_ENTIT_ATTRIBUTE_UPDATE
end

--监听本地事件
function BigworldModule:on_local_event(cmd, ...)
    if cmd == EventDefine.ON_ENTIT_ATTRIBUTE_UPDATE then
        EntityUpdateAttributefunc.update_attribute(...)
    end
end

return BigworldModule