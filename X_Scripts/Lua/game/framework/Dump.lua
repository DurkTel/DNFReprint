--带打印位置的print
-- function xprint(value, desciption, colorKey)
--     if not G_DEBUG_VISIBLE then return end
--     local valueStr = value
--     local desciptionStr = desciption

--     valueStr = valueStr ~= nil and tostring(valueStr) or ""
--     desciptionStr = desciptionStr ~= nil and tostring(desciptionStr) or ""

--     local luaPath, line = getDebugInfo(2)

--     local colorStr = colors[colorKey]
--     if colorStr then
--         valueStr = string.format("<color=#%s>%s</color>", colorStr, valueStr)
--         desciptionStr = string.format("<color=#%s>%s</color>", colorStr, desciptionStr)
--     end

--     local content = valueStr .. "   " .. desciptionStr .. '\n\n' .. luaPath .. ":" .. line
--     print(content)
-- end