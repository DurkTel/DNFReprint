

local main = function()
    local breakSocketHandle,debugXpCall = require("LuaDebug")("localhost",7003)
    local Timerfunc = require("game.framework.func.Timerfunc")
    Timerfunc:add_timer(function ()
        breakSocketHandle()
    end, 1, -1)

    local SentityData =
    {
        type = 0,
        models = {
            [0] = 10000,
            [1] = 10001,
            [2] = 10002,
            [3] = 10003,
            [4] = 10004,
            [5] = 10005,
            [6] = 10006,
            [7] = 10007,
            [8] = 10008,
        }
    }

    GEntityManager.create_entity(SentityData)

    GScenesManager.switch_scene(10000)
end


main()