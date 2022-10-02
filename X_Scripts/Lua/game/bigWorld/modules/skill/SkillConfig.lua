local SkillConfig = {}
local db_tbskill = require("db.db_tbskill")
local db_tbdamage = require("db.db_tbdamage")
local db_tbeffect = require("db.db_tbeffect")


function SkillConfig.getSkillCfgById(id)
    return db_tbskill[id]
end

function SkillConfig.getDamageCfgById(id)
    return db_tbdamage[id]
end

function SkillConfig.getEffectCfgById(id)
    return db_tbeffect[id]
end

return SkillConfig