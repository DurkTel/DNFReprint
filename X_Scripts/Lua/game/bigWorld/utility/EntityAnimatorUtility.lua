local EntityAnimatorUtility = {}

function EntityAnimatorUtility.get_animatorCfg(etype)
    local assetName = nil
    if etype == 0 then
        assetName = "SwordManAnimMap.asset"
    elseif etype == 2 then
        assetName = "GoblinAnimationMap.asset"
    elseif etype == 5 then
        assetName = "CommonSkillAnimMap.asset"
    end

    return assetName
end


return EntityAnimatorUtility