local EntitySkinUtility = {}
local t_modelInfo = require("db.db_tbmodelinfo")

EntitySkinUtility.skeletonAssetPath = 
{
    [0] = "model/Bone/common_character_bone",
    [2] = "model/Bone/common_single_bone"
}

function EntitySkinUtility.get_skeleton_assetName(entityType)
    local assetName = EntitySkinUtility.skeletonAssetPath[entityType]
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
        assetName = cfg.modelPath
        boneName = cfg.boneName
        sort = cfg.sort
        scale = cfg.modelScale
    end

    return assetName, boneName, scale, sort
end

return EntitySkinUtility