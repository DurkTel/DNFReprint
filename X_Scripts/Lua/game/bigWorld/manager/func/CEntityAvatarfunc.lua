local CEntityAvatarfunc = {}
--实体模型的C#方法

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

--设置sort
function CEntityAvatarfunc:enable_sort_sprite(floorHeight)
    self.gmentity:EnableSortSprite(floorHeight)
end

--设置面朝
function CEntityAvatarfunc:set_flip(isLeft)
    self.gmentity:SetSpriteFilp(isLeft)
end

--添加骨骼特效
function CEntityAvatarfunc:add_bone_effect(bone, effect, pos)
    pos = pos or Vector3.zero
    self.gmentity:Add_BoneEffect(bone, effect, pos)
end

--移除骨骼特效
function CEntityAvatarfunc:remove_bone_effect(bone, effect)
    self.gmentity:Remove_BoneEffect(bone, effect)
end

--获得骨骼特效
function CEntityAvatarfunc:get_bone_effect(bone)
    self.gmentity:Get_BoneEffect(bone)
end

--当前是否播放这个动画
function CEntityAvatarfunc:in_animation(animationName)
    return self.gmentity:IsInThisAni(animationName)
end

--当前是否播放带这个标签的动画
function CEntityAvatarfunc:in_tag_animation(tag)
    return self.gmentity:IsInThisTagAni(tag)
end

--获取当前播放的动画状态 动画名称 动画帧
function CEntityAvatarfunc:get_current_animation_state()
    local aniName = self.gmentity:GetCurrentAni()
    local frame = self.gmentity:GetCurrentFrame()
    return aniName, frame
end

return CEntityAvatarfunc