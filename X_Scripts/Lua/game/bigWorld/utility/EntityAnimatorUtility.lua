local EntityAnimatorUtility = {}

function EntityAnimatorUtility.get_animatorCfg(etype)
    local assetName = nil
    if etype == 0 then
        assetName = "Assets/ScriptableObjects/AnimationConfig/Character/Player/SaberAnimConfig.asset"
    elseif etype == 2 then
        assetName = "Assets/ScriptableObjects/AnimationConfig/Character/Player/SaberAnimConfig.asset"
    end

    return assetName
end


return EntityAnimatorUtility