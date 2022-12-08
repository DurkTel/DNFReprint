local db = MDefine.db
local SkillConfig = {}
db.db_tbskill = "db.db_tbskill"
db.db_tbdamage = "db.db_tbdamage"
db.db_tbeffect = "db.db_tbeffect"


function SkillConfig.getSkillCfgById(id)
    return db.db_tbskill[id]
end

function SkillConfig.getDamageCfgById(id)
    return db.db_tbdamage[id]
end

function SkillConfig.getEffectCfgById(id)
    return db.db_tbeffect[id]
end

return SkillConfig