local CopyConfig = {}
local db_tbmap = require("db.db_tbmap")

function CopyConfig.getCfgById(id)
    return db_tbmap[id]
end


return CopyConfig