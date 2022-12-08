local db = MDefine.db
local RoleConfig = {}
db.db_tbrole = "db.db_tbrole"

function RoleConfig.getRoleCfgByCareer(career)
    return db.db_tbrole[career]
end


return RoleConfig