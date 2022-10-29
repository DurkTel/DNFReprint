

local main = function()
    local breakSocketHandle,debugXpCall = require("LuaDebug")("localhost",7003)
    local Timerfunc = require("game.framework.func.Timerfunc")
    Timerfunc:add_timer(function ()
        breakSocketHandle()
    end, 1, -1)

    GEntityManager.create_local_player(0)
    local monsterEntity = GEntityManager.create_monster(10000)
    monsterEntity:set_avatarPosition(Vector3(4.7,1.09,0))
    GScenesManager.switch_scene(10000)
end


main()