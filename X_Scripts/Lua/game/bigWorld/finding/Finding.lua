local Finding = {} _G.GFinding = Finding

--计算路径
function Finding.calculatePathByPos(sx, sy, ex, ey)
    local navigation2D = GScenesManager.get_curNavigation()
    if not navigation2D then
        return
    end
    local success, path = navigation2D:CalculatePath(sx, sy, ex, ey)

    return success, path
end

--计算路径
function Finding.calculatePathByNode(sx, sy, node)
    local navigation2D = GScenesManager.get_curNavigation()
    if not navigation2D then
        return
    end
    local success, path = navigation2D:CalculatePath(sx, sy, node)

    return success, path
end

--获取寻路可用的点
function Finding.getNavBorder(sx, sy, radius)
    local navigation2D = GScenesManager.get_curNavigation()
    if not navigation2D then
        return
    end
    local area = navigation2D:CalculateArea(sx, sy, radius)

    return area
end

return Finding