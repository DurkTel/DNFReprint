local RoleConfig = {}
local db_tbrole = require("db.db_tbrole")

function RoleConfig.getRoleCfgByCareer(career)
    return db_tbrole[career]
end


return RoleConfig