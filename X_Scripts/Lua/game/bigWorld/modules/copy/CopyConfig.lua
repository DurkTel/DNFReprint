local db = MDefine.db
local CopyConfig = {}
db.db_tbmap = "db.db_tbmap"

function CopyConfig.getCfgById(id)
    return db.db_tbmap[id]
end


return CopyConfig