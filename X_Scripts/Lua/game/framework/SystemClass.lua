
-- 把[a,b,c,d ...][1000,3,...] 转成table {{a,b,c,d ...},{1000,3 ...}}
local formatToArrayCacge = {}
string.___formatToArrayCacge = formatToArrayCacge
local cacheData = nil
function string.formatToArray(str)
    -- 增加缓存
    cacheData = formatToArrayCacge[str]
    if cacheData then return cacheData end

    local r = {}
    if str and #str > 0 then
        for k in string.gmatch(str,"%b[]") do
            k = string.sub(k,2,-2)
            table.insert(r,string.split(k,","))
        end
        -- 存放缓存
        formatToArrayCacge[str] = r
    end
    return r
end

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