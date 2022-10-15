local ModuleRegister = {}

local allModules = {}
local allViews = {}
local allModuleViews = {}

local function get_name(path)
    return string.match( path, "(%w+)$" )
end

-- 注册模块
local function register(path)
    local name = get_name(path)
	--模块注册过了
	if allModules[name] then return end
	
    --实例化模块
	local mclass = require( path )()
	allModules[name] = mclass

    --注册本地事件
	local eventList = {mclass:get_local_evnets()}
    for i, v in ipairs(eventList) do
        Dispatcher.addEventListener(v, mclass.on_local_event, mclass)
    end

    local viewsList = {mclass:get_views()}
    for i, v in ipairs(viewsList) do
        local name = get_name(v)
        allViews[name] = v
        allModuleViews[name] = mclass
    end

	mclass:on_register()
end

function ModuleRegister.registerModule(path)
    register(path)
end

function ModuleRegister.get_module_by_name(name)
    return allModuleViews[name]
end

function ModuleRegister.get_view_by_name(name)
    return allViews[name]
end

return ModuleRegister