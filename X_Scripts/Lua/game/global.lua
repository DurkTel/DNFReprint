
--测试快捷键 Ctrl+F1 切换到debug模式
GInputUtility.onInputPressEvent = GInputUtility.onInputPressEvent + function (key)
    if key == "F1" then
    elseif key == "F2" then
        local entity = GEntityManager.create_skill_effect(10006, GEntityManager.localPlayer.entityData.entityId)
    elseif key == "F3" then
        GUIManager.open_view("GUIView")

    elseif key == "F4" then
        GUIManager.close_view("GUIView")

    elseif key == "F5" then

    elseif key == "F6" then

    elseif key == "F7" then

    elseif key == "F8" then

    elseif key == "F9" then
    
    end
end