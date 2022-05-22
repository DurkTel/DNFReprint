local EntitySkinUtility = {}
local t_modelInfo = require("db.db_tbmodelinfo")

function EntitySkinUtility.get_skeleton_assetName(career)
    local assetName = nil
    if career == 0 then
        assetName = "Model/Bone/common_character_bone"
    end

    return assetName
end

function EntitySkinUtility.get_skinPart_Cfg(entityData, skinPart)
    local models = entityData.srvInfo.models
    local code = models[skinPart]
    local cfg = t_modelInfo[code]
    local assetName = nil
    local boneName = nil
    local sort = nil
    local scale = nil

    if cfg then
        assetName = cfg.modelPath.."/"..cfg.id
        boneName = cfg.boneName
        sort = cfg.sort
        scale = cfg.modelScale
    end

    return assetName, boneName, scale, sort
end

return EntitySkinUtility