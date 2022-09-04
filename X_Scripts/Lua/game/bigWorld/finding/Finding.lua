local Finding = {} _G.GFinding = Finding

--计算路径
function Finding.calculatePath(sx, sy, ex, ey)
    local navigation2D = GScenesManager.get_curNavigation()
    if not navigation2D then
        return
    end
    local success, path = navigation2D:CalculatePath(sx, sy, ex, ey)

    return success, path
end

return Finding