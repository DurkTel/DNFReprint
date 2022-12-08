local db = MDefine.db
local MonsterConfig = {}
db.db_tbmonster = "db.db_tbmonster"
db.db_tbmonster_refresh = "db.db_tbmonster_refresh"


function MonsterConfig.getMonsterCfgById(id)
    return db.db_tbmonster[id]
end

function MonsterConfig.getMonsterRefreshCfgById(id)
    return db.db_tbmonster_refresh[id]
end


return MonsterConfig