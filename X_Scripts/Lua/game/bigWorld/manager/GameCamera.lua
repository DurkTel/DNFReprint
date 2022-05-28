local GameCamera = {}
_G.GGameCamera = GameCamera

local COrbitCamera = CS.OrbitCamera

local function init()
    COrbitCamera.Initialize()
end

function GameCamera.set_focus(centity)
    COrbitCamera.Instance:SetFocus(centity)
end

function GameCamera.set_limit(minHeight, maxHeight, minWidth, maxWidth)
    COrbitCamera.Instance:SetCameraLimit(minHeight, maxHeight, minWidth, maxWidth)
end

init()

return GameCamera