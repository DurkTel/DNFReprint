local EntityAnimatorUtility = {}

function EntityAnimatorUtility.get_animatorCfg(etype)
    local assetName = nil
    if etype == 0 then
        assetName = "SwordManAnimMap"
    elseif etype == 2 then
        assetName = "GoblinAnimationMap"
    end

    return assetName
end


return EntityAnimatorUtility