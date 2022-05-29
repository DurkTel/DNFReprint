local Timerfunc = require("game.framework.func.Timerfunc")


local copyBaseLogin = class()

copyBaseLogin.add_timer = Timerfunc.add_timer
copyBaseLogin.add_framer = Timerfunc.add_framer
copyBaseLogin.wait_timer = Timerfunc.wait_timer
copyBaseLogin.reset_timer = Timerfunc.reset_timer
copyBaseLogin.del_timer = Timerfunc.del_timer
copyBaseLogin.del_all_timer = Timerfunc.del_all_timer


return copyBaseLogin