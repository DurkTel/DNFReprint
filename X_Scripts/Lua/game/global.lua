_G.GDebugUtility = CS.DebugUtility

GDebugUtility.Init()
--测试快捷键 Ctrl+F1 切换到debug模式
GDebugUtility.onInputEvent = function (key)
    if key == "F1" then
        
        local SentityData =
        {
            type = 2,
            cfg = {bornPos = {x = 4.7, y = 1.09, z= 0}},
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
    elseif key == "F2" then

    elseif key == "F3" then

    elseif key == "F4" then

    elseif key == "F5" then

    elseif key == "F6" then

    elseif key == "F7" then

    elseif key == "F8" then

    elseif key == "F9" then
    
    end
end