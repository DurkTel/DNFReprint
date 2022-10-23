
local base = require("game.framework.Module")
local RoleModule = class(base)

--注册模块
function RoleModule:on_register()
    base.on_register(self)
    MDefine.cfg.role = "game.bigWorld.modules.role.RoleConfig"
    MDefine.data.role = "game.bigWorld.modules.role.RoleData"
end

--注册该模块下的视图
function RoleModule:get_views()
    return 'game.bigWorld.modules.role.views.CreateRoleView'
end

--注册本地事件
function RoleModule:get_local_evnets()
end

--监听本地事件
function RoleModule:on_local_event(cmd, data, data1)
    
end

return RoleModule