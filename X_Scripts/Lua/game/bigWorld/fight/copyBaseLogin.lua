local Timerfunc = require("game.framework.func.Timerfunc")


local copyBaseLogin = class()

copyBaseLogin.add_timer = Timerfunc.add_timer
copyBaseLogin.add_framer = Timerfunc.add_framer
copyBaseLogin.wait_timer = Timerfunc.wait_timer
copyBaseLogin.reset_timer = Timerfunc.reset_timer
copyBaseLogin.del_timer = Timerfunc.del_timer
copyBaseLogin.del_all_timer = Timerfunc.del_all_timer

function copyBaseLogin:add_event_listener()

end

function copyBaseLogin:remove_event_listener()

end

function copyBaseLogin:on_copy_in()
    print("进入副本！！！")
end

function copyBaseLogin:on_copy_update(mapId)
    print("更新副本进度！！！")
end

function copyBaseLogin:on_copy_out()
    print("退出副本！！！")
end


return copyBaseLogin