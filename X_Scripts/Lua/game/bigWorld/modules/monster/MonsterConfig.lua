local MonsterConfig = {}
local db_tbmonster = require("db.db_tbmonster")
local db_tbmonster_refresh = require("db.db_tbmonster_refresh")


function MonsterConfig.getMonsterCfgById(id)
    return db_tbmonster[id]
end

function MonsterConfig.getMonsterRefreshCfgById(id)
    return db_tbmonster_refresh[id]
end


return MonsterConfig