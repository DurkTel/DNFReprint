local base = require("game.framework.Injection")

local test = class(base)

test.prefab = "test"

function test:on_initView()
    print("on_initView")
end

return test