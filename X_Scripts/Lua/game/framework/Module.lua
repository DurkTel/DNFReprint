local Timerfunc = require("game.framework.func.Timerfunc")

local Module = class()

Module.add_timer = Timerfunc.add_timer
Module.add_framer = Timerfunc.add_framer
Module.wait_timer = Timerfunc.wait_timer
Module.reset_timer = Timerfunc.reset_timer
Module.del_timer = Timerfunc.del_timer
Module.del_all_timer = Timerfunc.del_all_timer

--注册模块
function Module:on_register()
    
end

--移除模块
function Module:on_unregister()
    self:del_all_timer()
end

--注册该模块下的视图
function Module:get_views()
    
end

--注册本地事件
function Module:get_local_evnets()
    
end

--监听本地事件
function Module:on_local_event(cmd, data)
    
end

return Module