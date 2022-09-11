local SkillConfig = {}
local db_tbskill = require("db.db_tbskill")
local db_tbdamage = require("db.db_tbdamage")


function SkillConfig.getSkillCfgById(id)
    return db_tbskill[id]
end

function SkillConfig.getDamageCfgById(id)
    return db_tbdamage[id]
end

return SkillConfig