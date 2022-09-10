function table.isNull(tab)
    return tab == nil or next(tab) == nil
end

function table.nums(t)
    local count = 0
    for k, v in pairs(t) do
        count = count + 1
    end
    return count
end

function table.connect(...)
	local tabs = {...}
    local re = {}
    
    local flag = 0
    for k, tab in ipairs(tabs) do
        local num = #tab
        for i = 1, num do
            re[flag + i] = tab[i]
        end
        flag = flag + num
    end
    return re
end

function table.removeByfunc(tab,func)
    if table.isNull(tab) then return end
    for i = #tab, 1, -1 do
        if func(i, tab[i]) then
            table.remove(tab,i)
        end
    end
end

function table.removeItem(tab,a)
    if(tab and a)then
        for i = #tab, 1, -1 do
            if tab[i] == a then
                table.remove(tab, i)
                break
            end
        end

    end
end


function string.isEmptyOrNull(str)
    return str == nil or str ==''
end

function string.split(str, delimiter, ignoreEmpty)
    if str == nil or str == '' or delimiter == nil then
        return nil
    end
    
    local result = {}
    for match in (str..delimiter):gmatch("(.-)"..delimiter) do
        if not ignoreEmpty or match ~= "" then
            table.insert(result, match)
        end
    end
    return result
end