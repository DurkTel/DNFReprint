_G.GInputUtility = CS.InputUtility

GInputUtility.Init()
--测试快捷键 Ctrl+F1 切换到debug模式
GInputUtility.onInputEvent = function (key)
    if key == "F1" then
    elseif key == "F2" then
        local monsterEntity = GEntityManager.create_monster(10000)
        monsterEntity:set_avatarPosition(Vector3(4.7,1.09,0))
        
    elseif key == "F3" then
        GAudioManager.play("CityLoopAudio", "sounds/music/gate_new")
    elseif key == "F4" then

    elseif key == "F5" then

    elseif key == "F6" then

    elseif key == "F7" then

    elseif key == "F8" then

    elseif key == "F9" then
    
    end
end