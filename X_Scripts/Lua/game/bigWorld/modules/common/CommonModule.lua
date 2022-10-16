local base = require("game.framework.Module")
local CommonModule = class(base)

--注册模块
function CommonModule:on_register()
    base.on_register(self)
    MDefine.cfg.common = "game.bigWorld.modules.common.CommonConfig"
    MDefine.data.common = "game.bigWorld.modules.common.CommonData"
end

--注册该模块下的视图
function CommonModule:get_views()
    return "game.framework.gui.GUIView",
    "game.bigWorld.modules.common.views.CommonView"
end

--注册本地事件
function CommonModule:get_local_evnets()
   
end

--监听本地事件
function CommonModule:on_local_event(cmd, data)
    
end

return CommonModule