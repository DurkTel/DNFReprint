
function table.nums(t)
    local count = 0
    for k, v in pairs(t) do
        count = count + 1
    end
    return count
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