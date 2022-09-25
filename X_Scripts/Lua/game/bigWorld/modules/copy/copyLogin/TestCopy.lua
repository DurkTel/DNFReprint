local base = require("game.bigWorld.fight.CopyBaseLogin")
local TestCopy = class(base)

function TestCopy:on_copy_update(mapId)
    base.on_copy_update(self,mapId)
    if mapId == 10002 then
        GCopyManager.copy_complete(true)
    end
end


return TestCopy