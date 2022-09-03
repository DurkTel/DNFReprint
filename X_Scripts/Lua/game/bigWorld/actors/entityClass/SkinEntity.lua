--场景上所有有皮肤外观的实体 都由此派生

local EntitySkinUtility = require("game.bigWorld.utility.EntitySkinUtility")
local EntityAnimatorUtility = require("game.bigWorld.utility.EntityAnimatorUtility")
local AvatarPartType = GEntityDefine.AvatarPartType

local base = require("game.bigWorld.actors.entityClass.GameEntity")
local SkinEntity = class(base)


function SkinEntity:on_init()
    base.on_init(self)
    self.skinInitialized = nil
    self.skinIsComplete = nil
end

function SkinEntity:dispose()
    base.dispose(self)
    self.skinInitialized = nil
    self.skinIsComplete = nil
end

function SkinEntity:onCreateEvent()
    self:init_skin()
    self.skinInitialized = true
end

function SkinEntity:onAvatarLoadComplete()
    base.onAvatarLoadComplete(self)
    self.skinIsComplete = true
    self:init_animator()
end

function SkinEntity:init_animator()
    local cfgAssetName = EntityAnimatorUtility.get_animatorCfg(self.entityData.etype)
    self:add_entityAnimator(cfgAssetName)
end

function SkinEntity:init_skin()
    self:refresh_allskin()
end

function SkinEntity:refresh_allskin()
    self:refresh_skeleton()
    self:refresh_skin_body()
    self:refresh_skin_shirt()
    self:refresh_skin_weapon()
    self:refresh_skin_pants()
    self:refresh_skin_shoes()
    self:refresh_skin_hair()
end

--刷新骨骼
function SkinEntity:refresh_skeleton()
    local career = self.entityData:get_career()
    if not career then
        -- print_err("")
        return
    end
    local skeletonAssetName = EntitySkinUtility.get_skeleton_assetName(career)
    self:set_avatarSkeleton(skeletonAssetName)
end


--刷新身体皮肤
function SkinEntity:refresh_skin_body()
    local assetName, boneName, scale, sort = EntitySkinUtility.get_skinPart_Cfg(self.entityData, AvatarPartType.body)
    self:set_avatarPart(AvatarPartType.body, assetName, boneName)
    self:set_avatarPartScale(AvatarPartType.body, scale)
    self:set_avatarPartSort(AvatarPartType.body, sort)
end

--刷新上衣皮肤
function SkinEntity:refresh_skin_shirt()
    local assetName, boneName, scale, sort = EntitySkinUtility.get_skinPart_Cfg(self.entityData, AvatarPartType.shirt)
    self:set_avatarPart(AvatarPartType.shirt, assetName, boneName)
    self:set_avatarPartScale(AvatarPartType.shirt, scale)
    self:set_avatarPartSort(AvatarPartType.shirt, sort)
end

--刷新武器皮肤
function SkinEntity:refresh_skin_weapon()
    local assetName, boneName, scale, sort = EntitySkinUtility.get_skinPart_Cfg(self.entityData, AvatarPartType.weapon)
    self:set_avatarPart(AvatarPartType.weapon, assetName, boneName)
    self:set_avatarPartScale(AvatarPartType.weapon, scale)
    self:set_avatarPartSort(AvatarPartType.weapon, sort)

    assetName, boneName, scale, sort = EntitySkinUtility.get_skinPart_Cfg(self.entityData, AvatarPartType.weaponEx)
    self:set_avatarPart(AvatarPartType.weaponEx, assetName, boneName)
    self:set_avatarPartScale(AvatarPartType.weaponEx, scale)
    self:set_avatarPartSort(AvatarPartType.weaponEx, sort)
end

--刷新裤子皮肤
function SkinEntity:refresh_skin_pants()
    local assetName, boneName, scale, sort = EntitySkinUtility.get_skinPart_Cfg(self.entityData, AvatarPartType.pants)
    self:set_avatarPart(AvatarPartType.pants, assetName, boneName)
    self:set_avatarPartScale(AvatarPartType.pants, scale)
    self:set_avatarPartSort(AvatarPartType.pants, sort)

    assetName, boneName, scale, sort = EntitySkinUtility.get_skinPart_Cfg(self.entityData, AvatarPartType.pantsEx)
    self:set_avatarPart(AvatarPartType.pantsEx, assetName, boneName)
    self:set_avatarPartScale(AvatarPartType.pantsEx, scale)
    self:set_avatarPartSort(AvatarPartType.pantsEx, sort)
end

--刷新鞋子皮肤
function SkinEntity:refresh_skin_shoes()
    local assetName, boneName, scale, sort = EntitySkinUtility.get_skinPart_Cfg(self.entityData, AvatarPartType.shoes)
    self:set_avatarPart(AvatarPartType.shoes, assetName, boneName)
    self:set_avatarPartScale(AvatarPartType.shoes, scale)
    self:set_avatarPartSort(AvatarPartType.shoes, sort)

    assetName, boneName, scale, sort = EntitySkinUtility.get_skinPart_Cfg(self.entityData, AvatarPartType.shoesEx)
    self:set_avatarPart(AvatarPartType.shoesEx, assetName, boneName)
    self:set_avatarPartScale(AvatarPartType.shoesEx, scale)
    self:set_avatarPartSort(AvatarPartType.shoesEx, sort)
end

--刷新头发皮肤
function SkinEntity:refresh_skin_hair()
    local assetName, boneName, scale, sort = EntitySkinUtility.get_skinPart_Cfg(self.entityData, AvatarPartType.hair)
    self:set_avatarPart(AvatarPartType.hair, assetName, boneName)
    self:set_avatarPartScale(AvatarPartType.hair, scale)
    self:set_avatarPartSort(AvatarPartType.hair, sort)
end


return SkinEntity