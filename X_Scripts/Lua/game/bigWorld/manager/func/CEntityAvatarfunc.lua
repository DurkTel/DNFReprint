local CEntityAvatarfunc = {}

--设置骨骼
function CEntityAvatarfunc:set_avatarSkeleton(boneAssetName)
    if not boneAssetName then return end
    self.gmentity:Skin_SetAvatarSkeleton(boneAssetName)
end

--设置模型
function CEntityAvatarfunc:set_avatarPart(partType, modelAssetName, boneName)
    if not partType or not modelAssetName or not boneName then return end
    self.gmentity:Skin_SetAvatarPart(partType, modelAssetName, boneName)
end

--设置模型缩放
function CEntityAvatarfunc:set_avatarPartScale(partType, scale)
    if not partType or not scale then return end
    self.gmentity:Skin_SetAvatarPartScale(partType, scale)
end

--设置模型层级
function CEntityAvatarfunc:set_avatarPartSort(partType, sort)
    if not partType or not sort then return end
    self.gmentity:Skin_SetAvatarPartSort(partType, sort)
end

--设置模型部位位置
function CEntityAvatarfunc:set_avatarPartPosition(partType)
    if not partType then return end
    self.gmentity:Skin_SetAvatarPartPosition(partType, Vector3.zero)
end

--设置模型位置
function CEntityAvatarfunc:set_avatarPosition(position)
    self.gmentity:Skin_SetAvatarPosition(position)
end

--设置显隐
function CEntityAvatarfunc:set_skinVisible(visible)
    self.gmentity:Skin_SetVisible(visible)
end

return CEntityAvatarfunc