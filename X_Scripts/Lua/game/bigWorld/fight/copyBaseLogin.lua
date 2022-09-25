local Timerfunc = require("game.framework.func.Timerfunc")


local CopyBaseLogin = class()

CopyBaseLogin.add_timer = Timerfunc.add_timer
CopyBaseLogin.add_framer = Timerfunc.add_framer
CopyBaseLogin.wait_timer = Timerfunc.wait_timer
CopyBaseLogin.reset_timer = Timerfunc.reset_timer
CopyBaseLogin.del_timer = Timerfunc.del_timer
CopyBaseLogin.del_all_timer = Timerfunc.del_all_timer

function CopyBaseLogin:add_event_listener()

end

function CopyBaseLogin:remove_event_listener()

end

function CopyBaseLogin:on_copy_in()
    print("进入副本！！！")
end

function CopyBaseLogin:on_copy_update(mapId)
    print("更新副本进度！！！")
end

function CopyBaseLogin:on_copy_out()
    print("退出副本！！！")
end


return CopyBaseLogin