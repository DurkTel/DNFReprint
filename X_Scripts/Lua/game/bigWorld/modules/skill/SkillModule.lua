local SkillFunc = require("game.bigWorld.modules.skill.func.SkillFunc")

local base = require("game.framework.Module")
local SkillModule = class(base)

--注册模块
function SkillModule:on_register()
    base.on_register(self)
    MDefine.cfg.skill = "game.bigWorld.modules.skill.SkillConfig"
    MDefine.data.skill = "game.bigWorld.modules.skill.SkillData"
end

--注册该模块下的视图
function SkillModule:get_views()
    
end

--注册本地事件
function SkillModule:get_local_evnets()
   return EventDefine.ON_INPUT_UPDATE
end

--监听本地事件
function SkillModule:on_local_event(cmd, data, data1)
    if cmd == EventDefine.ON_INPUT_UPDATE then
        SkillFunc.skill_discharge_handle_local(data, data1)
    end
end

return SkillModule