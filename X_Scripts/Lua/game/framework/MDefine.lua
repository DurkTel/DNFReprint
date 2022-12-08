local MDefine = {}
_G.MDefine = MDefine


local allValues = {}

local function gen(name)
    local tab = {}
    setmetatable(tab, 
    {
        __index = function (tab, key)
            if type(allValues[name][key]) == "string" then
                allValues[name][key] = require(allValues[name][key])
            end
            return allValues[name][key]
        end,

        __newindex = function (tab, key, value)
            if type(key) ~= "string" then
                print_err("MDefine的key值必须为字符串！！！！")
            end
            if not allValues[name] then allValues[name] = {} end
            allValues[name][key] = value
        end
    })
    return tab
end

MDefine.cfg = gen("cfg")
MDefine.data = gen("data")
MDefine.db = gen("db")


return MDefine