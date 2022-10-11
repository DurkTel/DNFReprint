local util = require 'xlua.util'

local gameobject = CS.UnityEngine.GameObject('LuaCoroutineRunner')
CS.UnityEngine.Object.DontDestroyOnLoad(gameobject)
local cs_coroutine_runner = gameobject:AddComponent(typeof(CS.LuaCoroutineRunner))
local function async_yield_return(to_yield, cb)
    cs_coroutine_runner:YieldAndCallback(to_yield, cb)
end

return {
    start = function(...) --开启协程
	    return cs_coroutine_runner:StartCoroutine(util.cs_generator(...))
	end,

	stop = function(coroutine) --停止协程
	    cs_coroutine_runner:StopCoroutine(coroutine)
	end,

	yield = function (to_yield, cb) --用于lua协程yield
		async_yield_return(to_yield, cb)
	end,

    async_yield = util.async_to_sync(async_yield_return) --直接等待
}