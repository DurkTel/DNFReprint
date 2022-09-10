local base = require("game.framework.Module")
local MonsterModule = class(base)

--注册模块
function MonsterModule:on_register()
    base.on_register(self)
    MDefine.cfg.monster = "game.bigWorld.modules.monster.MonsterConfig"
    MDefine.data.monster = "game.bigWorld.modules.monster.MonsterData"
end

--注册该模块下的视图
function MonsterModule:get_views()
    
end

--注册本地事件
function MonsterModule:get_local_evnets()
   
end

--监听本地事件
function MonsterModule:on_local_event(cmd, data)
    
end

return MonsterModule