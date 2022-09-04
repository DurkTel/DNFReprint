local EntityAnimatorUtility = {}

function EntityAnimatorUtility.get_animatorCfg(etype)
    local assetName = nil
    if etype == 0 then
        assetName = "SwordManAnimConfig"
    elseif etype == 2 then
        assetName = "GoblinAnimationConfig"
    end

    return assetName
end


return EntityAnimatorUtility