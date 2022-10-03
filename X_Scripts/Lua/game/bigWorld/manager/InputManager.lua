local InputManager = {} _G.GInputManager = InputManager

_G.GInputUtility = CS.InputUtility
GInputUtility.Init()

GInputManager.INPUT_TYPE =
{
    PRESS = 1,      --按下
    RELEASE = 2,    --松开
    HOLD = 3,       --长按
    MULTI = 4,      --双击
}

GInputUtility.onInputPressEvent = function (action)
    Dispatcher.dispatchEvent(EventDefine.ON_INPUT_UPDATE, action, GInputManager.INPUT_TYPE.PRESS)
end

GInputUtility.onInputReleaseEvent = function (action)
    Dispatcher.dispatchEvent(EventDefine.ON_INPUT_UPDATE, action, GInputManager.INPUT_TYPE.RELEASE)
end

GInputUtility.onInputHoldEvent = function (action)
    Dispatcher.dispatchEvent(EventDefine.ON_INPUT_UPDATE, action, GInputManager.INPUT_TYPE.HOLD)
end

GInputUtility.onInputMultiEvent = function (action)
    Dispatcher.dispatchEvent(EventDefine.ON_INPUT_UPDATE, action, GInputManager.INPUT_TYPE.MULTI)
end


return InputManager